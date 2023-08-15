using Extensions;

using Newtonsoft.Json;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public class Season : ISeasonData, IContent, IInteractableContent, IDownloadableContent, IParentContent
	{
		public event EventHandler ContentDeleted;

		public event EventHandler ContentRemoved
		{
			add
			{
				if (Show != null)
					((IContent)Show).ContentRemoved += value;
				ContentDeleted += value;
			}

			remove
			{
				if (Show != null)
					((IContent)Show).ContentRemoved -= value;
				ContentDeleted -= value;
			}
		}

		public event EventHandler InfoChanged;

		public DateTime? AirDate { get; set; }
		public string BackdropPath => (Episodes.FirstOrDefault()?.BackdropPath).IfEmpty(Show?.BackdropPath);
		public Bitmap BigIcon => ProjectImages.Big_TVEmpty;
		public bool CanBeDownloaded => Episodes.Any(x => x.AirState == AirStateEnum.Aired);
		public ContentType ContentType => ContentType.Episode;
		public Credits Credits { get; set; }
		public bool Custom { get; set; }

		public List<IEpisodePref> EpisodePrefs
		{
			get => Episodes.Select(x => new IEpisodePref
			{
				EN = x.EN,
				Rating = x.Rating,
				Watched = x.Watched,
				Progress = x.Progress,
				WatchDate = x.WatchDate,
				WatchTime = x.WatchTime,
				LastReminder = x.LastReminder,
				RawVidFiles = x.RawVidFiles,
			}).ToList();
			set
			{
				foreach (var ep in value)
				{
					var episode = this[ep.EN];

					if (episode != null)
					{
						episode.Rating = ep.Rating;
						episode.Watched = ep.Watched;
						episode.Progress = ep.Progress;
						episode.WatchDate = ep.WatchDate;
						episode.WatchTime = ep.WatchTime;
						episode.LastReminder = ep.LastReminder;
						episode.RawVidFiles = ep.RawVidFiles;
					}
				}
			}
		}

		public List<Episode> Episodes { get; set; } = new List<Episode>();

		public Bitmap HugeIcon => ProjectImages.Huge_TVPlay;
		public PosterImages Images { get; set; }
		public string Name { get; set; }
		public Banner NewBanner => Episodes.Any(x => x.AirState == AirStateEnum.Aired && x.AirDate > DateTime.Today.AddDays(-8)) ? new Banner("NEW EPISODE", BannerStyle.Active, ProjectImages.Tiny_New) : null;
		public Season Next => Show.Seasons.Next(this);
		public string Overview { get; set; }
		public bool Playable => Episodes.Any(x => x.Playable);
		public Season Previous => (Show.Seasons as IEnumerable<Season>).Reverse().Next(this);
		public RatingInfo Rating { get; set; }
		public int SeasonNumber { get; set; }

		[JsonProperty("PosterPath")]
		public string SeasonPosterPath { get; set; }

		[JsonIgnore]
		public string PosterPath => SeasonPosterPath.IfEmpty(Show?.PosterPath);

		public TvShow Show { get; set; }

		public string Status
		{
			get
			{
				if (!(Episodes?.Any() ?? false))
				{
					return string.Empty;
				}

				if (Episodes.All(x => x.AirState == AirStateEnum.Aired && DateTime.Now - (x.AirDate ?? DateTime.MinValue) > TimeSpan.FromDays(14)))
				{
					return "Aired";
				}

				if (Episodes.All(x => x.AirState != AirStateEnum.Aired))
				{
					return "Coming Soon";
				}

				return "Airing";
			}
		}

		public string SubInfo => Episodes.Count > 0 ? ($"{Episodes.Count} Episode".Plural(Episodes.Count) + (Episodes?.FirstOrDefault()?.AirDate == null ? null : $" • {Episodes?.FirstOrDefault()?.AirDate?.Year.ToString()}")) : "TBA";
		public bool Temporary { get; set; }
		public Bitmap TinyIcon => ProjectImages.Tiny_Season;

		public TvSeason TMDbData
		{
			set
			{
				if (value != null)
				{
					SeasonNumber = value?.SeasonNumber ?? 0;
					Name = value?.Name ?? $"Season {SeasonNumber}";
					Credits = value?.Credits ?? new Credits() { Cast = new List<Cast>(), Crew = new List<Crew>() };
					Videos = value?.Videos?.Results;
					AirDate = value?.AirDate;
					Overview = value?.Overview;
					SeasonPosterPath = value?.PosterPath;
					Images = value?.Images;
					Custom = false;

					foreach (var e in Episodes)
					{
						e.TMDbData = value.Episodes.FirstOrDefault(y => e.EN == y.EpisodeNumber);
					}

					Episodes.AddRange(value.Episodes.Where(y => !Episodes.Any(x => x.EN == y.EpisodeNumber)).Select(ep => new Episode(ep, this)));

					Episodes = Episodes.Where(x => x.Custom || value.Episodes.Any(y => y.EpisodeNumber == x.EN)).OrderBy(x => x.EN).ToList();

					InfoChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public string ToolTipText => string.Empty;
		public ContentType Type => ContentType.Season;
		public int UnwatchedContent => Episodes.Count(x => !x.Watched && x.AirState == AirStateEnum.Aired);
		public List<Video> Videos { get; set; }
		public double VoteAverage { get; }
		public int VoteCount { get; }

		public double ContentRating => Episodes.Any(x => x.Rating.Rated) ? Episodes.Where(x => x.Rating.Rated).Average(x => x.Rating.Rating) : 0;

		public string Years
		{
			get
			{
				var first = Episodes.FirstOrDefault()?.AirDate?.Year;

				if (first == null)
				{
					return string.Empty;
				}

				if (!(Episodes?.Any() ?? false) || !Episodes.All(x => x.AirState == AirStateEnum.Aired && DateTime.Now - (x.AirDate ?? DateTime.MinValue) > TimeSpan.FromDays(14)))
				{
					return $"{first} - Present";
				}

				var last = Episodes.Max(x => x.AirDate ?? DateTime.Now).Year;

				return last != first ? $"{first} - {last}" : first.ToString();
			}
		}

		public Episode this[int episode]
			=> Episodes.FirstOrDefault(x => x.EN == episode);

		public Season()
		{
		}

		public Season(TvSeason tvSeason, TvShow show = null)
		{
			TMDbData = tvSeason;
			Show = show;
		}

		public Season(TMDbLib.Objects.Search.SearchTvSeason tvSeason, TvShow show = null)
		{
			TMDbData = new TvSeason
			{
				AirDate = tvSeason.AirDate,
				PosterPath = tvSeason.PosterPath,
				SeasonNumber = tvSeason.SeasonNumber,
				Episodes = new List<TMDbLib.Objects.Search.TvSeasonEpisode>(),
				Name = tvSeason.SeasonNumber == 0 ? "Extras" : $"Season {tvSeason.SeasonNumber}"
			};

			Temporary = true;
			Show = show;
		}

		public void Download()
		{
			Cursor.Current = Cursors.WaitCursor;
			Data.Mainform.PushPanel(null, new PC_Download(this));
			Cursor.Current = Cursors.Default;
		}

		public override bool Equals(object obj)
		{
			return obj is Season season &&
EqualityComparer<TvShow>.Default.Equals(Show, season.Show) &&
SeasonNumber == season.SeasonNumber;
		}

		public override int GetHashCode()
		{
			var hashCode = -1562695867;
			hashCode = hashCode * -1521134295 + EqualityComparer<TvShow>.Default.GetHashCode(Show);
			hashCode = hashCode * -1521134295 + SeasonNumber.GetHashCode();
			return hashCode;
		}

		public void Play()
		{
			Show.GetCurrentWatchEpisode(SeasonNumber)?.Play();
		}

		public void RePlay()
		{
			if (MessagePrompt.Show($"Are you sure you want to replay {Show} {Name}?", PromptButtons.YesNo, PromptIcons.Question, Data.Mainform) == DialogResult.Yes)
			{
				foreach (var e in Episodes)
				{
					e.MarkAs(false);
				}

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
					LocalShowHandler.OnWatchInfoChanged(Show, this);
				}).Run();
			}
		}

		public void Save(ChangeType change)
		{
			Show?.Save(change);
		}

		public void ShowInfoPage()
		{
			Data.Mainform.PushPanel(null, new PC_SeasonView(this));
		}

		public void ShowStrip(Point? location = null, bool fromInfoPage = false)
		{
			SlickToolStrip.Show(Data.Mainform, location,
				new SlickStripItem("Play", Play, image: ProjectImages.Tiny_Play, show: Playable),

				new SlickStripItem("Download", Download, image: ProjectImages.Tiny_Download, show: CanBeDownloaded, fade: !ConnectionHandler.IsConnected),

				new SlickStripItem("More Info", ShowInfoPage, image: ProjectImages.Tiny_Info, show: !fromInfoPage),

				new SlickStripItem("Watch Trailer", WatchTrailer, ProjectImages.Tiny_Trailer, Videos?.Any(x => x.Type == "Trailer" && x.Site == "YouTube" && x.Iso_639_1 == "en") ?? false),

				SlickStripItem.Empty,

				new SlickStripItem("Edit Name", () =>
				{
					var res = MessagePrompt.ShowInput($"Change the name of {Show} Season {SeasonNumber}", defaultValue: Name);
					if (res.DialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(res.Input))
					{
						Name = res.Input;
						InfoChanged?.Invoke(this, EventArgs.Empty);
						Show.Save(ChangeType.Data);
					}
				}, ProjectImages.Tiny_Edit, show: Custom),

				new SlickStripItem("Delete Season", () =>
				{
					ShowManager.DeleteSeason(this);
					ContentDeleted?.Invoke(this, EventArgs.Empty);
				}, ProjectImages.Tiny_Trash, show: Custom),

				new SlickStripItem("", show: Custom),

				new SlickStripItem("Mark as " + (Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched) ? "Un-watched" : "Watched"), show: !Show.Temporary, image: ProjectImages.Tiny_Ok, action: () =>
				{
					new BackgroundAction(() =>
					{
						MarkAs(!Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched));
						Show.ContentInfoChanged();
						Save(ChangeType.Preferences);
						LocalShowHandler.OnWatchInfoChanged(Show, this);
					}).Run();
				}),

				new SlickStripItem("Manage", image: ProjectImages.Tiny_ThumbsUpDown, fade: true),

				new SlickStripItem(Rating.Loved ? "Unlove" : "Love", () =>
				{
					Rating = Rating.SwitchLove();
					InfoChanged?.Invoke(this, EventArgs.Empty);
					Show.ContentInfoChanged();
					new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
				}, image: Rating.Loved ? ProjectImages.Tiny_Dislike : ProjectImages.Tiny_Love, show: !Show.Temporary, tab: 1),

				new SlickStripItem("Rate", () =>
				{
					var res = RateForm.Show(Rating.Rated ? Rating.Rating : 5);
					if (res.Item1)
					{
						Rating = Rating.Rate(res.Item2);
						InfoChanged?.Invoke(this, EventArgs.Empty);
						Show.ContentInfoChanged();
						new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
					}
				}, image: ProjectImages.Tiny_Rating, show: !Show.Temporary, tab: 1),

				new SlickStripItem("Remove Rating", () =>
				{
					Rating = Rating.UnRate();
					InfoChanged?.Invoke(this, EventArgs.Empty);
					Show.ContentInfoChanged();
					new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
				}, image: ProjectImages.Tiny_RemoveRating, show: !Show.Temporary && Rating.Rated, tab: 1),

				new SlickStripItem("Add Tag", () =>
				{
					var res = MessagePrompt.ShowInput("Write down the tag you'd like to add.", form: Data.Mainform);

					if (res.DialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(res.Input))
					{
						Rating = Rating.AddTag(res.Input.ToLower());
						InfoChanged?.Invoke(this, EventArgs.Empty);
						Show.ContentInfoChanged();
						new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
					}
				}, image: ProjectImages.Tiny_Label, show: !Show.Temporary, tab: 1),

				new SlickStripItem("Edit Categories", () =>
				{
					Data.Mainform.PushPanel(null, new PC_ManageCategory(Rating, (r) =>
					{
						Rating = r;
						InfoChanged?.Invoke(this, EventArgs.Empty);
						Show.ContentInfoChanged();
						new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
					}));
				}, image: ProjectImages.Tiny_Categories, show: false && !Show.Temporary, tab: 1)
				);
		}

		public override string ToString()
		{
			return Name;
		}

		public void WatchTrailer()
		{
			var trailer = Videos?.FirstOrDefault(x => x.Type == "Trailer" && x.Site == "YouTube" && x.Iso_639_1 == "en");

			if (trailer != null)
			{
				YoutubeControl.Play(trailer.Key, trailer.Name, season: this);
			}
		}

		public void MarkAs(bool watched)
		{
			Episodes.ForEach(e => e.MarkAs(watched));
			InfoChanged?.Invoke(this, EventArgs.Empty);
		}

		public void ContentInfoChanged()
		{
			InfoChanged?.Invoke(this, EventArgs.Empty);
			Show?.ContentInfoChanged();
		}
	}
}