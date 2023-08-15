using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

using ProjectImages = ShowsCalendar.Properties.Resources;
using TMDbMovie = TMDbLib.Objects.Movies.Movie;

namespace ShowsCalendar
{
#pragma warning disable CS4014

	public partial class Movie : ISave, IMovieData, IMoviePref, IContent, IInteractableContent, IPlayableContent, IDownloadableContent
	{
		public event EventHandler ContentRemoved;

		public event EventHandler FilesChanged;

		public event EventHandler InfoChanged;

		public event EventHandler<double> WatchTimeChanged;

		public DateTime? AirDate => ReleaseDate;

		public AirStateEnum AirState
		{
			get
			{
				if (ReleaseDate == null)
				{
					return AirStateEnum.Unknown;
				}

				if (ReleaseDate <= DateTime.Today)
				{
					return AirStateEnum.Aired;
				}

				return AirStateEnum.ToBeAired;
			}
		}

		public string BackdropPath { get; set; }
		public Bitmap BigIcon => ProjectImages.Big_Movie;
		public long Budget { get; set; }
		public List<Cast> Cast { get; set; }
		public List<Crew> Crew { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime LastReminder { get; set; }
		public DateTime LastRefresh { get; set; }
		public DateTime LastUpcomingReminder { get; set; }
		public ExternalIdsMovie ExternalIds { get; set; }
		public bool FirstLoad { get; set; } = false;
		public List<Genre> Genres { get; set; }
		public string Homepage { get; set; }
		public Bitmap HugeIcon => ProjectImages.Huge_Movie;
		public int Id { get; set; }
		public Images Images { get; set; }
		public List<string> Keywords { get; set; }
		public new string Name => Title;
		public Banner NewBanner => AirState == AirStateEnum.Aired && AirDate > DateTime.Today.AddDays(-20) ? new Banner("NEW", BannerStyle.Active, ProjectImages.Tiny_New) : null;
		public string OriginalTitle { get; set; }
		public string Overview { get; set; }
		public string PosterPath { get; set; }
		public List<ProductionCompany> ProductionCompanies { get; set; }
		public List<ProductionCountry> ProductionCountries { get; set; }
		public RatingInfo Rating { get; set; }
		public DateTime? ReleaseDate { get; set; }
		public long Revenue { get; set; }
		public int? Runtime { get; set; }
		public LightContent[] SimilarMovies { get; set; }
		public List<string> Languages { get; set; }
		public string Status { get; set; }
		public string SubInfo => Tagline;
		public string Tagline { get; set; }
		public bool Temporary { get; set; }
		public Bitmap TinyIcon => ProjectImages.Tiny_Movie;
		public string Title { get; set; }

		public TMDbMovie TMDbData
		{
			set
			{
				if (value != null)
				{
					Id = value.Id;
					Title = (value?.Title ?? base.Name).RemoveDoubleSpaces();
					OriginalTitle = (value?.OriginalTitle ?? base.Name).RemoveDoubleSpaces().IfEmpty(Title);
					ReleaseDate = value.ReleaseDate;
					Overview = value.Overview;
					Tagline = value.Tagline;
					Genres = value.Genres;
					BackdropPath = value.BackdropPath.IfEmpty(BackdropPath ?? string.Empty);
					PosterPath = value.PosterPath;
					ProductionCompanies = value.ProductionCompanies;
					Homepage = value.Homepage;
					ProductionCountries = value.ProductionCountries;
					Languages = value.SpokenLanguages.Select(x => { try { return new System.Globalization.CultureInfo(x.Iso_639_1).EnglishName; } catch { return x.Name; } }).WhereNotEmpty().ToList();
					Status = value.Status;
					ExternalIds = value.ExternalIds;
					VoteAverage = value.VoteAverage;
					VoteCount = value.VoteCount;
					Revenue = value.Revenue;
					Budget = value.Budget;
					Runtime = value.Runtime;
					Cast = value?.Credits?.Cast?.OrderBy(x => x.Order).ToList() ?? new List<Cast>();
					Crew = value?.Credits?.Crew?.OrderBy(x => !string.IsNullOrWhiteSpace(x.ProfilePath) ? 0 : 1).ThenBy(x => x.Name).ToList() ?? new List<Crew>();
					SimilarMovies = value?.Similar?.Results?.Select(LightContent.Convert).ToArray() ?? Array.Empty<LightContent>();
					Videos = value?.Videos?.Results?.Where(x => x.Site == "YouTube" && x.Iso_639_1 == "en").OrderBy(x => x.Id).ToArray() ?? Videos ?? Array.Empty<Video>();
					Images = value?.Images;
					Keywords = new[] { Title, OriginalTitle }.Concat(value.Keywords?.Keywords?.Select(x => x.Name) ?? Array.Empty<string>()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList() ?? new List<string>();
				}
			}
		}

		public ContentType Type => ContentType.Movie;
		public Video[] Videos { get; set; }
		public double VoteAverage { get; set; }
		public int VoteCount { get; set; }

		public void AddFile(IO.File file)
		{
			lock (vidFiles)
			{
				if (!vidFiles.Contains(file))
				{
					vidFiles.Add(file);
				}
			}

			FilesChanged?.Invoke(this, EventArgs.Empty);
		}

		public void Delete()
		{
			foreach (var item in Directory.GetFiles(Path.Combine(DocsFolder, "Movies"), $"{Id}.*"))
				File.Delete(item);
		}

		public void Download()
		{
			Cursor.Current = Cursors.WaitCursor;
			Data.Mainform.PushPanel(null, new PC_Download(this));
			Cursor.Current = Cursors.Default;
		}

		public override bool Equals(object obj)
		{
			return obj is Movie movie && movie.Id == Id;
		}

		public override int GetHashCode()
		{
			return Id;
		}

		public void MarkAs(bool @watched)
		{
			if (Watched = @watched)
			{
				WatchTime = 0;
				WatchDate = DateTime.Now;
			}
			else
			{
				WatchTime = -1;
				LastReminder = DateTime.MinValue;
			}

			Progress = 0;
			LocalMovieHandler.OnWatchInfoChanged(this, this);
		}

		public override void OnLoad()
		{
			Keywords = Keywords ?? new List<string> { Title };
		}

		public void Save(ChangeType change)
		{
			if (Id != 0 && !Temporary && MovieManager.Movie(Id) != null)
			{
				if (change.HasFlag(ChangeType.Data))
				{
					Save(new[] { typeof(IMovieData) }, $"Movies/{Id}.data", true);
				}

				if (change.HasFlag(ChangeType.Preferences))
				{
					Save(new[] { typeof(IMoviePref) }, $"Movies/{Id}.pref", true);
				}
			}
		}

		#region IPlayableContent Support

		private double progress;
		private List<IO.File> vidFiles = new List<IO.File>();
		private bool watched;
		public bool CanBeDownloaded => AirDate <= DateTime.Today;

		public bool Playable
		{
			get
			{
				lock (vidFiles)
				{
					foreach (var item in vidFiles)
					{
						if (item.Exists)
						{
							return true;
						}
					}
				}

				return false;
			}
		}

		public double Progress
		{
			get => progress;
			set
			{
				progress = value;
				WatchTimeChanged?.Invoke(this, value);
			}
		}

		public string[] RawVidFiles
		{
			get
			{
				lock (vidFiles)
				{
					return vidFiles.Select(x => x.Path).ToArray();
				}
			}
			set
			{
				lock (vidFiles)
				{
					vidFiles = value?.Select(x => new IO.File(x))?.ToList() ?? vidFiles;
				}
			}
		}

		public bool Started => WatchTime > 0;

		public string ToolTipText { get; }

		public IEnumerable<IO.File> VidFiles
		{
			get
			{
				lock (vidFiles)
				{
					foreach (var grp in vidFiles.Where(x => x.Exists).GroupBy(x => x.Info.Length).OrderByDescending(x => x.Key))
					{
						if (grp.Count() == 1 && grp.First().Exists)
						{
							yield return grp.First();
						}
						else
						{
							var item = grp
								   .OrderBy(x => x.Path)
								   .FirstOrDefault(x => !x.Parent.Info.IsNetwork() && x.Exists)
								   ?? grp.FirstOrDefault(x => x.Exists);
							if (item != null)
							{
								yield return item;
							}
						}
					}
				}
			}
		}

		public DateTime WatchDate { get; set; }

		public bool Watched
		{
			get => watched || WatchTime == 0;
			set
			{
				watched = value;
				WatchTimeChanged?.Invoke(this, Progress);
			}
		}

		public long WatchTime { get; set; } = -1;

		public Bitmap GetThumbnail()
		{
			var file = (FileInfo)null;

			lock (vidFiles)
			{
				foreach (var item in vidFiles)
				{
					try
					{
						file = new FileInfo(item.Path);

						if (file.Exists)
						{
							break;
						}
					}
					catch { }
				}
			}

			return file.GetThumbnail();
		}

		public bool Play(FileInfo epFile = null)
		{
			if (epFile == null && Playable)
			{
				if (VidFiles.Count() == 1)
				{
					epFile = VidFiles.FirstOrDefault()?.Info;
				}
				else
				{
					if ((Data.Mainform.CurrentPanel is PC_MoviePage psc) && psc.LinkedMovie == this)
					{
						psc.PageTile.CurrentPage = MoviePageTile.Page.VidFiles;
					}
					else
					{
						var pce = new PC_MoviePage(this);
						pce.PageTile.currentPage = MoviePageTile.Page.VidFiles;
						Data.Mainform.PushPanel(null, pce);
					}

					return true;
				}
			}

			if (epFile == null || !File.Exists(epFile.FullName))
			{
				LocalMovieHandler.LoadFiles(this);

				if (Playable)
				{
					return Play();
				}

				return false;
			}

			if (!epFile?.Exists ?? false)
			{
				MessagePrompt.Show($"Could not find the file associated with {this}\n\nCheck if the file exists, or if it was renamed into something Shows Calendar can't detect.", PromptButtons.OK, icon: PromptIcons.Hand, form: Data.Mainform);
				return false;
			}

			PC_Player pc = null;
			var pushed = false;

			try
			{
				if (Data.ActivePlayer != null)
				{
					Data.ActivePlayer.SaveWatchtime();
					Data.ActivePlayer.ClearMedia();
					Data.ActivePlayer.SetFile(epFile);
					Data.ActivePlayer.SetMovie(this);
				}
				else
				{
					pc = new PC_Player(this, epFile);
					pushed = true;
					if (pc.Movie != null)
					{
						Data.Mainform.PushPanel(null, pc);
					}
					else
					{
						pc.Dispose();
					}
				}
			}
			catch
			{
				pc?.Dispose();
				if (pushed)
				{
					Data.Mainform.PushBack();
				}

				MessagePrompt.Show("Something went wrong while loading your movie.\n\nMake sure it fully downloaded, or try another file.", PromptButtons.OK, icon: PromptIcons.Error, form: Data.Mainform);

				return false;
			}

			Data.Mainform.ShowUp();
			return true;
		}

		#endregion IPlayableContent Support

		public void ShowInfoPage()
		{
			Data.Mainform.PushPanel(null, new PC_MoviePage(this));
		}

		public void ShowStrip(Point? location = null, bool fromInfoPage = false)
		{
			SlickToolStrip.Show(Data.Mainform, location,
				new SlickStripItem("Add To Library", () =>
				{
					MovieManager.Add(this);
				}, image: ProjectImages.Tiny_Add, show: Temporary),
				new SlickStripItem("Play", () => Play(), image: ProjectImages.Tiny_Play, show: Playable),

				new SlickStripItem("Download", Download, image: ProjectImages.Tiny_Download, show: CanBeDownloaded, fade: !ConnectionHandler.IsConnected),

				new SlickStripItem("View File".Plural(VidFiles), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					if ((Data.Mainform.CurrentPanel is PC_MoviePage psc) && psc.LinkedMovie == this)
					{
						psc.PageTile.CurrentPage = MoviePageTile.Page.VidFiles;
					}
					else
					{
						var pce = new PC_MoviePage(this);
						pce.PageTile.currentPage = MoviePageTile.Page.VidFiles;
						Data.Mainform.PushPanel(null, pce);
					}
					Cursor.Current = Cursors.Default;
				}, image: ProjectImages.Tiny_Folder, show: Playable),

				new SlickStripItem("Reload Thumbnail", () =>
				{
					IO.Handler.LoadThumbnail(this, VidFiles.FirstOrDefault()?.Info);
				}, image: ProjectImages.Tiny_Image, show: Playable && (string.IsNullOrWhiteSpace(BackdropPath) || Guid.TryParse(BackdropPath.Substring(1, BackdropPath.Length - 5), out var guid))),

				new SlickStripItem("Remove from On-Deck", () =>
				{
					WatchDate = DateTime.MinValue;

					new BackgroundAction(() =>
					{
						LocalMovieHandler.OnWatchInfoChanged(this);
						Save(ChangeType.Preferences);
					}).Run();
				}, image: ProjectImages.Tiny_RemovePlay, show: Started && !Watched && WatchDate > DateTime.Now.AddDays(-10)),

				SlickStripItem.Empty,

				new SlickStripItem("Movie Page", ShowInfoPage, image: ProjectImages.Tiny_Info, show: !fromInfoPage),

				new SlickStripItem("Watch Trailer", WatchTrailer, ProjectImages.Tiny_Trailer, Videos?.Any(x => x.Type == "Trailer" && x.Site == "YouTube" && x.Iso_639_1 == "en") ?? false),

				new SlickStripItem("Refresh", () => Refresh(), image: ProjectImages.Tiny_Refresh, show: !Temporary),

				SlickStripItem.Empty,

				new SlickStripItem("Mark", image: ProjectImages.Tiny_Ok, fade: true, show: !Temporary && (Progress > 0 || WatchTime > 0)),

				new SlickStripItem("as Complete", tab: 1, show: !Temporary && (Progress > 0 || WatchTime > 0), action: () =>
				{
					new BackgroundAction(() =>
					{
						MarkAs(true);
						Save(ChangeType.Preferences);
					}).Run();
				}, image: ProjectImages.Tiny_Forward),

				new SlickStripItem("as " + ((Watched || Progress > 0 || WatchTime > 0) ? "Un-watched" : "Watched"), tab: 1, show: !Temporary && (Progress > 0 || WatchTime > 0), action: () =>
				{
					new BackgroundAction(() =>
					{
						MarkAs(!(Watched || Progress > 0 || WatchTime > 0));
						Save(ChangeType.Preferences);
					}).Run();
				}, image: (Watched || Progress > 0 || WatchTime > 0) ? ProjectImages.Tiny_Unwatched : ProjectImages.Tiny_Watched),

				new SlickStripItem("Mark as " + ((Watched || Progress > 0 || WatchTime > 0) ? "Un-watched" : "Watched"), show: !Temporary && !(Progress > 0 || WatchTime > 0), action: () =>
				{
					new BackgroundAction(() =>
					{
						MarkAs(!(Watched || Progress > 0 || WatchTime > 0));
						Save(ChangeType.Preferences);
					}).Run();
				}, image: (Watched || Progress > 0 || WatchTime > 0) ? ProjectImages.Tiny_Unwatched : ProjectImages.Tiny_Watched),

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

				new SlickStripItem("Remove from library", () => { MovieManager.Remove(this); ContentRemoved?.Invoke(this, EventArgs.Empty); }, image: ProjectImages.Tiny_Trash, show: !Temporary)
				);
		}

		public override string ToString()
		{
			return Title + (ReleaseDate == null ? string.Empty : $" • {ReleaseDate?.Year}");
		}

		public void WatchTrailer()
		{
			var trailer = Videos?.FirstOrDefault(x => x.Type == "Trailer" && x.Site == "YouTube" && x.Iso_639_1 == "en");

			if (trailer != null)
			{
				YoutubeControl.Play(trailer.Key, trailer.Name, movie: this);
			}
		}

		public void SetThumbnail(Guid guid)
		{
			BackdropPath = $"/{guid}.jpg";
			InfoChanged?.Invoke(this, EventArgs.Empty);
			Save(ChangeType.Data);
		}

		public void ContentInfoChanged() => InfoChanged?.Invoke(this, EventArgs.Empty);
	}

#pragma warning restore CS4014
}