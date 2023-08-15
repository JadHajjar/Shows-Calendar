using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public enum AirStateEnum { Unknown, Aired, ToBeAired }

	public class Episode : IEpisodeData, IContent, IInteractableContent, IPlayableContent, IDownloadableContent
	{
		public event EventHandler ContentDeleted;

		public event EventHandler ContentRemoved
		{
			add
			{
				if (Season != null)
					((IContent)Season).ContentRemoved += value;
				ContentDeleted += value;
			}

			remove
			{
				if (Season != null)
					((IContent)Season).ContentRemoved -= value;
				ContentDeleted -= value;
			}
		}

		public event EventHandler InfoChanged;

		public static Episode None { get; } = new Episode(null) { Empty = true };

		public DateTime? AirDate { get; set; }
		public AirStateEnum AirState => AirDate == null ? AirStateEnum.Unknown : (AirDate < DateTime.Today ? AirStateEnum.Aired : AirStateEnum.ToBeAired);
		public string BackdropPath => StillPath.IfEmpty(Show?.BackdropPath);
		public Bitmap BigIcon => ProjectImages.Big_TVEmpty;
		public List<Crew> Crew { get; set; } = new List<Crew>();
		public bool Custom { get; set; }
		public bool Empty { get; set; }
		public int EN { get; set; }
		public List<Cast> GuestStars { get; set; } = new List<Cast>();
		public Bitmap HugeIcon => ProjectImages.Huge_TVPlay;
		public int Id { get; set; }
		public StillImages Images { get; set; }
		public string Name { get; set; }
		public Banner NewBanner => AirState == AirStateEnum.Aired && AirDate > DateTime.Today.AddDays(-8) ? new Banner("NEW", BannerStyle.Active, ProjectImages.Tiny_New) : null;
		public Episode Next => Show.Episodes.Next(this);
		public string Overview { get; set; }
		public string PosterPath => (Season?.PosterPath).IfEmpty(Show?.PosterPath);
		public Episode Previous => Show?.Episodes.Reverse().Next(this);
		public DateTime LastReminder { get; set; }
		public RatingInfo Rating { get; set; }
		public Season Season { get; set; }
		public TvShow Show => Season?.Show;
		public int SN => Season?.SeasonNumber ?? 0;
		public string StillPath { get; set; }
		public string SubInfo => $"Season {SN} • Episode {EN}";
		public Bitmap TinyIcon => ProjectImages.Tiny_TVEmpty;

		public TvSeasonEpisode TMDbData
		{
			set
			{
				if (value != null)
				{
					Id = value.Id;
					Name = value.Name.IfEmpty($"Episode {EN}");
					AirDate = value.AirDate;
					Overview = value.Overview;
					StillPath = value.StillPath.IfEmpty(StillPath ?? string.Empty);
					EN = value?.EpisodeNumber ?? 0;
					VoteAverage = value.VoteAverage;
					VoteCount = value.VoteCount;
					GuestStars = value.GuestStars;
					Crew = value.Crew;
					Custom = false;

					InfoChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public ContentType Type => ContentType.Episode;
		public List<Video> Videos { get; set; }
		public double VoteAverage { get; set; }
		public int VoteCount { get; set; }

		public Episode()
		{
		}

		public Episode(TvSeasonEpisode episode, Season season = null)
		{
			TMDbData = episode;
			Season = season;
			Empty = false;
		}

		public static bool operator !=(Episode episode1, Episode episode2) => !(episode1 == episode2);

		public static bool operator <(Episode l, Episode f) => l.SN < f.SN || (l.SN == f.SN && l.EN < f.EN);

		public static bool operator ==(Episode episode1, Episode episode2) => EqualityComparer<Episode>.Default.Equals(episode1, episode2);

		public static bool operator >(Episode l, Episode f) => l.SN > f.SN || (l.SN == f.SN && l.EN > f.EN);

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

		public void Download()
		{
			Cursor.Current = Cursors.WaitCursor;
			Data.Mainform.PushPanel(null, new PC_Download(this));
			Cursor.Current = Cursors.Default;
		}

		public override bool Equals(object obj)
		{
			return obj is Episode episode
				&& SN == episode.SN
				&& EN == episode.EN
				&& Show == episode.Show;
		}

		public override int GetHashCode()
		{
			return 1912459738 + Show.Id.GetHashCode() + SN.GetHashCode() + EN.GetHashCode();
		}

		public bool IsFinale()
		{
			if (Season.Episodes.Max(x => x.EN) != EN)
			{
				return false;
			}

			var prevSeason = Season.Previous;

			if (DateTime.Now - (AirDate ?? DateTime.MinValue) < TimeSpan.FromDays(7))
			{
				return false;
			}

			if ((prevSeason?.SeasonNumber ?? 0) == 0)
			{
				return true;
			}

			if (DateTime.Now - (AirDate ?? DateTime.MinValue) > TimeSpan.FromDays(14))
			{
				return true;
			}

			if (Math.Abs(Season.Episodes.Count - prevSeason.Episodes.Count) <= 3)
			{
				return true;
			}

			return false;
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
			InfoChanged?.Invoke(this, EventArgs.Empty);
			Next?.InfoChanged?.Invoke(this, EventArgs.Empty);
		}

		#region IPlayableContent Support

		public event EventHandler FilesChanged;

		public event EventHandler<double> WatchTimeChanged;

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

		public void SetThumbnail(Guid guid)
		{
			StillPath = $"/{guid}.jpg";
			InfoChanged?.Invoke(this, EventArgs.Empty);
			Save(ChangeType.Data);
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

		public string ToolTipText => Show.Name;

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
			if (Data.Options.FinaleWarning && IsFinale() && !Watched
				&& MessagePrompt.Show($"You are about to watch the finale for {Season} of {Show}" +
					 $"\n\nClick cancel if you are not mentally ready.",
					 "Finale Warning",
					 PromptButtons.OKCancel,
					 PromptIcons.Info,
					 Data.Mainform
					) == DialogResult.Cancel)
			{
				return true;
			}

			if (epFile == null && Playable)
			{
				if (VidFiles.Count() == 1)
				{
					epFile = VidFiles.FirstOrDefault()?.Info;
				}
				else
				{
					if ((Data.Mainform.CurrentPanel is PC_EpisodeView psc) && psc.Episode == this)
					{
						psc.PageTile.CurrentPage = EpisodePageTile.Page.VidFiles;
					}
					else
					{
						var pce = new PC_EpisodeView(this);
						pce.PageTile.currentPage = EpisodePageTile.Page.VidFiles;
						Data.Mainform.PushPanel(null, pce);
					}

					return true;
				}
			}

			if (epFile == null || !File.Exists(epFile.FullName))
			{
				LocalShowHandler.LoadFiles(Show);

				if (Playable)
				{
					return Play();
				}

				return false;
			}

			if (!epFile?.Exists ?? false)
			{
				MessagePrompt.Show($"Could not find the file associated with {Show} {this}\n\nCheck if the file exists, or if it was renamed into something Shows Calendar can't detect.", PromptButtons.OK, icon: PromptIcons.Hand, form: Data.Mainform);
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
					Data.ActivePlayer.SetEpisode(this);
				}
				else
				{
					pc = new PC_Player(this, epFile);
					pushed = true;
					if (pc.Episode != null)
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

				MessagePrompt.Show("Something went wrong while loading your episode.\n\nMake sure it's fully downloaded, or try another file.", PromptButtons.OK, icon: PromptIcons.Error, form: Data.Mainform);

				return false;
			}

			Data.Mainform.ShowUp();
			return true;
		}

		#endregion IPlayableContent Support

		public void Save(ChangeType change)
		{
			Show?.Save(change);
		}

		public void ShowInfoPage()
		{
			if (Data.Mainform.CurrentPanel is PC_SeasonView seasonView && seasonView.Season == Season)
			{
				Data.Mainform.PushPanel(null, new PC_EpisodeView(this));
			}
			else if (!Data.Options.OpenAllPagesForEp)
			{
				Data.Mainform.PushPanel(null, new PC_EpisodeView(this));
			}
			else if (!(Data.Mainform.CurrentPanel is PC_EpisodeView epView && epView.Episode == this))
			{
				Data.Mainform.PushPanel(null, new PC_ShowPage(this));
			}
		}

		public void ShowStrip(Point? location = null, bool fromInfoPage = false)
		{
			var firstEp = Show.Episodes.FirstOrDefault(x => !x.Watched && x.Playable);
			var lastEp = Show.Episodes.LastThat(x => x.Watched);
			var isOnDeck = firstEp != null && firstEp == this && ((lastEp != null && lastEp.WatchDate > DateTime.Now.AddDays(-10)) || (firstEp.Started && WatchDate > DateTime.Now.AddDays(-10)));

			SlickToolStrip.Show(Data.Mainform, location,
				new SlickStripItem("Play", () => Play()
				, image: ProjectImages.Tiny_Play
				, show: Playable),

				new SlickStripItem("Download Episode", Download, image: ProjectImages.Tiny_Download, show: CanBeDownloaded, fade: !ConnectionHandler.IsConnected),

				new SlickStripItem("Download Season", () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					Data.Mainform.PushPanel(null, new PC_Download(Season));
					Cursor.Current = Cursors.Default;
				}
				, image: ProjectImages.DownloadBatch
				, show: Season.Episodes.All(x => !x.Playable)
				, fade: !ConnectionHandler.IsConnected),

				new SlickStripItem("View File".Plural(VidFiles), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					if ((Data.Mainform.CurrentPanel is PC_EpisodeView psc) && psc.Episode == this)
					{
						psc.PageTile.CurrentPage = EpisodePageTile.Page.VidFiles;
					}
					else
					{
						var pce = new PC_EpisodeView(this);
						pce.PageTile.currentPage = EpisodePageTile.Page.VidFiles;
						Data.Mainform.PushPanel(null, pce);
					}
					Cursor.Current = Cursors.Default;
				}, image: ProjectImages.Tiny_Folder, show: Playable),

				new SlickStripItem("Reload Thumbnail", () =>
				{
					IO.Handler.LoadThumbnail(this, VidFiles.FirstOrDefault()?.Info);
				}, image: ProjectImages.Tiny_Image, show: Playable && (string.IsNullOrWhiteSpace(StillPath) || Guid.TryParse(StillPath.Substring(1, StillPath.Length - 5), out var guid))),

				new SlickStripItem("Remove from On-Deck", () =>
				{
					if (lastEp == null || lastEp.WatchDate <= DateTime.Now.AddDays(-10))
					{
						WatchDate = DateTime.MinValue;
					}
					else
					{
						lastEp.WatchDate = DateTime.MinValue;
					}

					new BackgroundAction(() =>
					{
						LocalShowHandler.OnWatchInfoChanged(Show, this);
						Save(ChangeType.Preferences);
					}).Run();
				}, image: ProjectImages.Tiny_RemovePlay, show: isOnDeck),

				SlickStripItem.Empty,

				new SlickStripItem("More Info", ShowInfoPage, image: ProjectImages.Tiny_Info, show: !fromInfoPage),

				new SlickStripItem("Season Info", () =>
				{
					if (Data.Options.OpenAllPagesForEp)
					{
						Data.Mainform.PushPanel(null, new PC_ShowPage(Season, this));
					}
					else
					{
						Data.Mainform.PushPanel(null, new PC_SeasonView(Season, this));
					}
				}, image: ProjectImages.Tiny_Season),

				new SlickStripItem("Show Page", () =>
				{
					Data.Mainform.PushPanel(null, new PC_ShowPage(Show));
				}, image: ProjectImages.Tiny_TV),

				new SlickStripItem("Watch Trailer", WatchTrailer, ProjectImages.Tiny_Trailer, Videos?.Any(x => x.Type == "Trailer" && x.Site == "YouTube" && x.Iso_639_1 == "en") ?? false),

				new SlickStripItem("", show: Custom),

				new SlickStripItem("Edit Name", () =>
				{
					var res = MessagePrompt.ShowInput($"Change the name of {Show} {SN}x{EN}", defaultValue: Name, form: Data.Mainform);
					if (res.DialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(res.Input))
					{
						Name = res.Input;
						InfoChanged?.Invoke(this, EventArgs.Empty);
						Show.Save(ChangeType.Data);
					}
				}, ProjectImages.Tiny_Edit, show: Custom),

				new SlickStripItem("Delete Episode", () =>
				{
					ShowManager.DeleteEpisode(this);
					ContentDeleted?.Invoke(this, EventArgs.Empty);
				}, ProjectImages.Tiny_Trash, show: Custom),

				SlickStripItem.Empty,

				new SlickStripItem("Mark", image: ProjectImages.Tiny_Ok, fade: true),

				new SlickStripItem("Ep. as Complete", () =>
				{
					new BackgroundAction(() =>
					{
						MarkAs(true);
						Save(ChangeType.Preferences);
						LocalShowHandler.OnWatchInfoChanged(Show, this);
					}).Run();
				}, image: ProjectImages.Tiny_Forward, show: Progress > 0 || WatchTime > 0, tab: 1),

				new SlickStripItem("Ep. as " + ((Watched || Progress > 0 || WatchTime > 0) ? "Un-watched" : "Watched"), () =>
				{
					new BackgroundAction(() =>
					{
						MarkAs(!(Watched || Progress > 0 || WatchTime > 0));
						Save(ChangeType.Preferences);
						Season.ContentInfoChanged();
						LocalShowHandler.OnWatchInfoChanged(Show, this);
					}).Run();
				}, image: (Watched || Progress > 0 || WatchTime > 0) ? ProjectImages.Tiny_Unwatched : ProjectImages.Tiny_Watched, tab: 1),

				new SlickStripItem("Season as " + (Season.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched) ? "Un-watched" : "Watched"), () =>
				{
					new BackgroundAction(() =>
					{
						Season.MarkAs(!Season.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched));
						Save(ChangeType.Preferences);
						Show.ContentInfoChanged();
						LocalShowHandler.OnWatchInfoChanged(Show, this);
					}).Run();
				}, image: ProjectImages.Tiny_TVEmpty, tab: 1),

				new SlickStripItem("Show as " + (Show.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched)) ? "Un-watched" : "Watched"), () =>
				{
					new BackgroundAction(() =>
					{
						Show.MarkAs(!Show.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched)));
						Save(ChangeType.Preferences);
						LocalShowHandler.OnWatchInfoChanged(Show, this);
					}).Run();
				}, image: ProjectImages.Tiny_TV, tab: 1),

				new SlickStripItem("", show: Previous != null, tab: 1),

				new SlickStripItem("Manage", image: ProjectImages.Tiny_ThumbsUpDown, fade: true),

				new SlickStripItem(Rating.Loved ? "Unlove" : "Love", () =>
				{
					Rating = Rating.SwitchLove();
					InfoChanged?.Invoke(this, EventArgs.Empty);
					Season.ContentInfoChanged();
					new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
				}, image: Rating.Loved ? ProjectImages.Tiny_Dislike : ProjectImages.Tiny_Love, show: !Show.Temporary, tab: 1),

				new SlickStripItem("Rate", () =>
				{
					var res = RateForm.Show(Rating.Rated ? Rating.Rating : 5);
					if (res.Item1)
					{
						Rating = Rating.Rate(res.Item2);
						InfoChanged?.Invoke(this, EventArgs.Empty);
						Season.ContentInfoChanged();
						new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
					}
				}, image: ProjectImages.Tiny_Rating, show: !Show.Temporary, tab: 1),

				new SlickStripItem("Remove Rating", () =>
				{
					Rating = Rating.UnRate();
					InfoChanged?.Invoke(this, EventArgs.Empty);
					Season.ContentInfoChanged();
					new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
				}, image: ProjectImages.Tiny_RemoveRating, show: !Show.Temporary && Rating.Rated, tab: 1),

				new SlickStripItem("Add Tag", () =>
				{
					var res = MessagePrompt.ShowInput("Write down the tag you'd like to add.", form: Data.Mainform);

					if (res.DialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(res.Input))
					{
						Rating = Rating.AddTag(res.Input.ToLower());
						InfoChanged?.Invoke(this, EventArgs.Empty);
						Season.ContentInfoChanged();
						new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
					}
				}, image: ProjectImages.Tiny_Label, show: !Show.Temporary, tab: 1),

				new SlickStripItem("Edit Categories", () =>
				{
					Data.Mainform.PushPanel(null, new PC_ManageCategory(Rating, (r) =>
					{
						Rating = r;
						InfoChanged?.Invoke(this, EventArgs.Empty);
						Season.ContentInfoChanged();
						new BackgroundAction(() => Save(ChangeType.Preferences)).Run();
					}));
				}, image: ProjectImages.Tiny_Categories, show: false && !Show.Temporary, tab: 1),

				new SlickStripItem(string.Empty, show: !Show.Temporary, tab: 1),

				new SlickStripItem("Previous Episode", image: ProjectImages.Tiny_Rewind, show: Previous != null, fade: true),

				new SlickStripItem("Play Previous", () => Previous.Play()
					, image: ProjectImages.Tiny_Play
					, show: Previous?.Playable ?? false
					, tab: 1),

				new SlickStripItem("Previous Ep. Info", () =>
				{
					if (Data.Mainform.CurrentPanel is PC_SeasonView seasonView && seasonView.Season == Season)
					{
						Data.Mainform.PushPanel(null, new PC_EpisodeView(Previous));
					}
					else if (!Data.Options.OpenAllPagesForEp)
					{
						Data.Mainform.PushPanel(null, new PC_EpisodeView(Previous));
					}
					else if (!(Data.Mainform.CurrentPanel is PC_EpisodeView epView && epView.Episode == Previous))
					{
						Data.Mainform.PushPanel(null, new PC_ShowPage(Previous));
					}
				}, image: ProjectImages.Tiny_Info, show: Previous != null, tab: 1),

				new SlickStripItem("Mark Previous as " + ((Previous == null || Previous.Watched || Previous.Progress > 0 || Previous.WatchTime > 0) ? "Un-watched" : "Watched"), () =>
				{
					new BackgroundAction(() =>
					{
						Previous.MarkAs(!(Previous.Watched || Previous.Progress > 0 || Previous.WatchTime > 0));
						Save(ChangeType.Preferences);
						Season.ContentInfoChanged();
						LocalShowHandler.OnWatchInfoChanged(Show, this);
					}).Run();
				}, image: (Previous == null || Previous.Watched || Previous.Progress > 0 || Previous.WatchTime > 0) ? ProjectImages.Tiny_Unwatched : ProjectImages.Tiny_Watched, show: Previous != null, tab: 1),

				new SlickStripItem("Mark All Previous Episodes as " + (Show.Episodes.Where(x => x < this).All(x => x.AirState != AirStateEnum.Aired || x.Watched) ? "Un-watched" : "Watched"), () =>
				{
					new BackgroundAction(() =>
					{
						var set = !Show.Episodes.Where(x => x < this).All(x => x.AirState != AirStateEnum.Aired || x.Watched);
						foreach (var e in Show.Episodes.Where(x => x < this && x.AirState == AirStateEnum.Aired))
						{
							e.MarkAs(set);
							e.Season.ContentInfoChanged();
						}
						Save(ChangeType.Preferences);
						LocalShowHandler.OnWatchInfoChanged(Show, this);
					}).Run();
				}, image: ProjectImages.Tiny_Rewind, Show.Episodes.Any(x => x < this), tab: 1)

			);
		}

		public override string ToString()
		{
			return string.IsNullOrEmpty(Name) ? $"{SN}x{EN}" : $"{SN}x{EN} • {Name}";
		}

		public void WatchTrailer()
		{
			var trailer = Videos?.FirstOrDefault(x => x.Type == "Trailer" && x.Site == "YouTube" && x.Iso_639_1 == "en");

			if (trailer != null)
			{
				YoutubeControl.Play(trailer.Key, trailer.Name, episode: this);
			}
		}
	}
}