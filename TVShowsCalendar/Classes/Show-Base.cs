using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public partial class TvShow : ISave, IShowData, IShowPref, IContent, IInteractableContent, IDownloadableContent, IParentContent
	{
		public event EventHandler ContentRemoved;

		public event EventHandler InfoChanged;

		public DateTime? AirDate => FirstAirDate;
		public string BackdropPath { get; set; }
		public ImageData[] Backdrops { get; set; }
		public Bitmap BigIcon => ProjectImages.Big_TV;
		public bool CanBeDownloaded => Episodes.Any(x => !x.Watched && x.AirState == AirStateEnum.Aired && !x.Playable);
		public List<Cast> Cast { get; set; }
		public ContentType ContentType => ContentType.Episode;
		public List<CreatedBy> CreatedBy { get; set; }
		public List<Crew> Crew { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime LastRefresh { get; set; }
		public List<int> EpisodeRunTime { get; set; }

		public IEnumerable<Episode> Episodes => Data.Options.IgnoreSpecialsSeason
			? Seasons.Where(x => x.SeasonNumber > 0).SelectMany(x => x.Episodes).OrderBy(x => x.SN).ThenBy(x => x.EN)
			: Seasons.SelectMany(x => x.Episodes).OrderBy(x => x.AirDate ?? DateTime.MaxValue).ThenBy(x => x.EN);

		public ExternalIdsTvShow ExternalIds { get; set; }
		public DateTime? FirstAirDate { get; set; }
		public List<string> FoundDirectories { get; } = new List<string>();
		public List<Genre> Genres { get; set; }
		public string Homepage { get; set; }
		public Bitmap HugeIcon => ProjectImages.Huge_TV;
		public int Id { get; set; }
		public bool InProduction { get; set; }
		public List<string> Keywords { get; set; }
		public List<string> Languages { get; set; }
		public DateTime? LastAirDate { get; set; }
		public Episode LastEpisode => Episodes.LastOrDefault(x => x.AirState == AirStateEnum.Aired) ?? Episode.None;
		public new string Name { get; set; }
		public List<NetworkWithLogo> Networks { get; set; }
		public Banner NewBanner => Episodes.Any(x => x.AirState == AirStateEnum.Aired && x.AirDate > DateTime.Today.AddDays(-8)) ? new Banner("NEW EPISODE", BannerStyle.Active, ProjectImages.Tiny_New) : null;
		public Episode NextEpisode => Episodes.FirstOrDefault(x => x.AirState == AirStateEnum.ToBeAired) ?? Episode.None;
		public string OriginalName { get; set; }
		public List<string> OriginCountry { get; set; }
		public string Overview { get; set; }
		public bool Playable => Episodes.Any(x => x.Playable);
		public string PosterPath { get; set; }
		public ImageData[] Posters { get; set; }
		public RatingInfo Rating { get; set; }

		public List<ISeasonPref> SeasonPrefs
		{
			get => Seasons.Select(x => new ISeasonPref { Rating = x.Rating, SeasonNumber = x.SeasonNumber, EpisodePrefs = x.EpisodePrefs }).ToList();
			set
			{
				if (value != null)
				{
					foreach (var sn in value)
					{
						var season = this[sn.SeasonNumber];

						if (season != null)
						{
							season.Rating = sn.Rating;
							season.EpisodePrefs = sn.EpisodePrefs;
						}
					}
				}
			}
		}

		public List<Season> Seasons { get; set; } = new List<Season>();
		public string ShowType { get; set; }
		public LightContent[] SimilarShows { get; set; }
		public string Status { get; set; }
		public string SubInfo => Years;
		public bool Temporary { get; set; }
		public Bitmap TinyIcon => ProjectImages.Tiny_TV;

		public TMDbLib.Objects.TvShows.TvShow TMDbData
		{
			set
			{
				if (value != null)
				{
					Name = value.Name.RemoveDoubleSpaces();
					OriginalName = value.OriginalName.RemoveDoubleSpaces().IfEmpty(Name);
					Status = value.Status;
					BackdropPath = value.BackdropPath;
					Overview = value.Overview;
					PosterPath = value.PosterPath;
					Networks = value.Networks;
					Homepage = value.Homepage;
					CreatedBy = value.CreatedBy;
					FirstAirDate = value.FirstAirDate;
					LastAirDate = value.LastAirDate;
					EpisodeRunTime = value.EpisodeRunTime;
					ExternalIds = value.ExternalIds;
					ShowType = value.Type;
					InProduction = value.InProduction;
					OriginCountry = value.OriginCountry;
					Languages = value.Languages.Select(x => { try { return new System.Globalization.CultureInfo(x).EnglishName; } catch { return null; } }).WhereNotEmpty().ToList();
					Genres = value.Genres;
					VoteAverage = value.VoteAverage;
					VoteCount = value.VoteCount;
					Backdrops = value.Images?.Backdrops?.Where(x => x.Iso_639_1 == null || x.Iso_639_1 == "en").ToArray() ?? Backdrops ?? Array.Empty<ImageData>();
					Posters = value.Images?.Posters?.Where(x => x.Iso_639_1 == null || x.Iso_639_1 == "en").ToArray() ?? Posters ?? Array.Empty<ImageData>();
					Cast = value.Credits?.Cast?.OrderBy(x => x.Order).ToList() ?? new List<Cast>();
					Crew = value.Credits?.Crew?.OrderBy(x => !string.IsNullOrWhiteSpace(x.ProfilePath) ? 0 : 1).ThenBy(x => x.Name).ToList() ?? new List<Crew>();
					SimilarShows = value.Similar?.Results?.Select(LightContent.Convert).ToArray() ?? Array.Empty<LightContent>();
					Videos = value.Videos?.Results?.Where(x => x.Site == "YouTube" && (x.Iso_639_1 == null || x.Iso_639_1 == "en")).OrderBy(x => x.Id).ToArray() ?? Videos ?? Array.Empty<Video>();
					Keywords = new[] { Name, OriginalName }.Concat(value.Keywords?.Results?.Select(x => x.Name) ?? Array.Empty<string>()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList() ?? new List<string>();
				}
			}
		}

		public string ToolTipText { get; }
		public ContentType Type => ContentType.TvShow;
		public int UnwatchedContent => Episodes.Count(x => !x.Watched && x.AirState == AirStateEnum.Aired);
		public Video[] Videos { get; set; }
		public double VoteAverage { get; set; }
		public int VoteCount { get; set; }

		public double ContentRating => Episodes.Any(x => x.Rating.Rated) ? Episodes.Where(x => x.Rating.Rated).Average(x => x.Rating.Rating) : 0;

		public string Years
		{
			get
			{
				var first = FirstAirDate?.Year;

				if (first == null)
				{
					return string.Empty;
				}

				if (!Status.AnyOf("Ended", "Canceled") || !(Episodes?.Any() ?? false))
				{
					return $"{first} - Present";
				}

				var last = Episodes.Max(x => x.AirDate ?? DateTime.Now).Year;

				return last != first ? $"{first} - {last}" : first.ToString();
			}
		}

		public Season this[int season]
			=> Seasons.FirstOrDefault(x => x.SeasonNumber == season);

		public Episode this[int season, int episode]
			=> Seasons.FirstOrDefault(x => x.SeasonNumber == season)?.Episodes.FirstOrDefault(x => x.EN == episode);

		public void Delete()
		{
			foreach (var item in Directory.GetFiles(Path.Combine(DocsFolder, "Shows"), $"{Id}.*"))
				File.Delete(item);
		}

		public void Download()
		{
			var ep = Episodes.FirstOrDefault(x => !x.Watched && x.AirState == AirStateEnum.Aired && !x.Playable);

			if (ep != null)
			{
				if (ep.Season.Episodes.All(x => !x.Watched && x.AirState == AirStateEnum.Aired && !x.Playable))
				{
					Cursor.Current = Cursors.WaitCursor;
					Data.Mainform.PushPanel(null, new PC_Download(ep.Season));
					Cursor.Current = Cursors.Default;
				}
				else
				{
					Cursor.Current = Cursors.WaitCursor;
					Data.Mainform.PushPanel(null, new PC_Download(ep));
					Cursor.Current = Cursors.Default;
				}
			}
		}

		public override bool Equals(object obj)
		{
			return obj is TvShow show && show.Id == Id;
		}

		public override int GetHashCode()
		{
			return Id;
		}

		public override void OnLoad()
		{
			Seasons = Seasons.Distinct(x => x.SeasonNumber).ToList();

			foreach (var se in Seasons)
			{
				se.Show = this;

				se.Episodes = se.Episodes.Distinct(x => x.EN).ToList();

				foreach (var ep in se.Episodes)
					ep.Season = se;
			}

			Keywords = Keywords ?? new List<string> { Name };
		}

		public void Play()
		{
			this.GetCurrentWatchEpisode().Play();
		}

		public void RePlay()
		{
			if (MessagePrompt.Show($"Are you sure you want to replay {Name}?", PromptButtons.YesNo, PromptIcons.Question, Data.Mainform) == DialogResult.Yes)
			{
				MarkAs(false);

				var ep = Episodes.FirstOrDefault();

				if (ep != null)
				{
					if (ep.Playable)
					{
						ep.Play();
					}
					else
					{
						if (ep.Season.Episodes.All(x => !x.Watched && x.AirState == AirStateEnum.Aired && !x.Playable))
						{
							Cursor.Current = Cursors.WaitCursor;
							Data.Mainform.PushPanel(null, new PC_Download(ep.Season));
							Cursor.Current = Cursors.Default;
						}
						else
						{
							Cursor.Current = Cursors.WaitCursor;
							Data.Mainform.PushPanel(null, new PC_Download(ep));
							Cursor.Current = Cursors.Default;
						}
					}
				}

				new BackgroundAction(() =>
				{
					Save(ChangeType.Preferences);
					LocalShowHandler.OnWatchInfoChanged(this, this);
				}).Run();
			}
		}

		public void Save(ChangeType change)
		{
			if (Id != 0 && !Temporary && ShowManager.Show(Id) != null)
			{
				if (change.HasFlag(ChangeType.Data))
				{
					Save(new[] { typeof(IShowData), typeof(ISeasonData), typeof(IEpisodeData) }, $"Shows/{Id}.data", true);
				}

				if (change.HasFlag(ChangeType.Preferences))
				{
					Save(new[] { typeof(IShowPref), typeof(ISeasonPref), typeof(IEpisodePref) }, $"Shows/{Id}.pref", true);
				}
			}
		}

		public void ShowInfoPage()
		{
			Data.Mainform.PushPanel(null, new PC_ShowPage(this));
		}

		public void ShowStrip(Point? location = null, bool fromInfoPage = false)
		{
			SlickToolStrip.Show(Data.Mainform, location,
				new SlickStripItem("Add To Library", () =>
				{
					ShowManager.Add(this);
				}, image: ProjectImages.Tiny_Add, show: Temporary),

				new SlickStripItem("Play", Play, image: ProjectImages.Tiny_Play, show: Playable),

				new SlickStripItem("Download", Download, image: ProjectImages.Tiny_Download, show: CanBeDownloaded, fade: !ConnectionHandler.IsConnected),

				SlickStripItem.Empty,

				new SlickStripItem("Show Page", ShowInfoPage, image: ProjectImages.Tiny_TV, show: !fromInfoPage),

				new SlickStripItem("Watch Trailer", WatchTrailer, ProjectImages.Tiny_Trailer, Videos?.Any(x => x.Type == "Trailer" && x.Site == "YouTube" && x.Iso_639_1 == "en") ?? false),

				new SlickStripItem("Refresh", Refresh, image: ProjectImages.Tiny_Refresh, show: !Temporary),

				SlickStripItem.Empty,

				new SlickStripItem("Mark as " + (Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched)) ? "Un-watched" : "Watched"), show: !Temporary, image: Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched)) ? ProjectImages.Tiny_Unwatched : ProjectImages.Tiny_Watched, action: () =>
				{
					new BackgroundAction(() =>
					{
						MarkAs(!Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched)));
						Save(ChangeType.Preferences);
						LocalShowHandler.OnWatchInfoChanged(this, this);
					}).Run();
				}),

				new SlickStripItem("Manage", image: ProjectImages.Tiny_ThumbsUpDown, fade: true),

				new SlickStripItem(Rating.Loved ? "Unlove" : "Love", () =>
				{
					Rating = Rating.SwitchLove();
					InfoChanged?.Invoke(this, EventArgs.Empty);
					new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
				}, image: Rating.Loved ? ProjectImages.Tiny_Dislike : ProjectImages.Tiny_Love, show: !Temporary, tab: 1),

				new SlickStripItem("Rate", () =>
				{
					var res = RateForm.Show(Rating.Rated ? Rating.Rating : 5);
					if (res.Item1)
					{
						Rating = Rating.Rate(res.Item2);
						InfoChanged?.Invoke(this, EventArgs.Empty);
						new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
					}
				}, image: ProjectImages.Tiny_Rating, show: !Temporary, tab: 1),

				new SlickStripItem("Remove Rating", () =>
				{
					Rating = Rating.UnRate();
					InfoChanged?.Invoke(this, EventArgs.Empty);
					new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
				}, image: ProjectImages.Tiny_RemoveRating, show: !Temporary && Rating.Rated, tab: 1),

				new SlickStripItem("Add Tag", () =>
				{
					var res = MessagePrompt.ShowInput("Write down the tag you'd like to add.", form: Data.Mainform);

					if (res.DialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(res.Input))
					{
						Rating = Rating.AddTag(res.Input.ToLower());
						InfoChanged?.Invoke(this, EventArgs.Empty);
						new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
					}
				}, image: ProjectImages.Tiny_Label, show: !Temporary, tab: 1),

				new SlickStripItem("Edit Categories", () =>
				{
					Data.Mainform.PushPanel(null, new PC_ManageCategory(Rating, (r) =>
					{
						Rating = r;
						InfoChanged?.Invoke(this, EventArgs.Empty);
						new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
					}));
				}, image: ProjectImages.Tiny_Categories, show: false && !Temporary, tab: 1),

				SlickStripItem.Empty,

				new SlickStripItem("Remove from library", () => { ShowManager.Remove(this); ContentRemoved?.Invoke(this, EventArgs.Empty); }, image: ProjectImages.Tiny_Trash, show: !Temporary)
			);
		}

		public override string ToString()
		{
			return Name;
		}

		public void MarkAs(bool watched)
		{
			foreach (var season in Seasons)
				season.MarkAs(watched);
			InfoChanged?.Invoke(this, EventArgs.Empty);
		}

		public void WatchTrailer()
		{
			var trailer = Videos?.FirstOrDefault(x => x.Type == "Trailer" && x.Site == "YouTube" && x.Iso_639_1 == "en");

			if (trailer != null)
			{
				YoutubeControl.Play(trailer.Key, trailer.Name, tvShow: this);
			}
		}

		public void ContentInfoChanged() => InfoChanged?.Invoke(this, EventArgs.Empty);
	}
}