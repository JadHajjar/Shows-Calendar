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

namespace ShowsCalendar
{
	public static class ShowManager
	{
		public delegate void ShowChangeHandler(TvShow show);

		public delegate void SeasonChangeHandler(Season season);

		public delegate void EpisodeChangeHandler(Episode episode);

		private static readonly WaitIdentifier RemindLogIdentifier = new WaitIdentifier();

		private static readonly Dictionary<int, TvShow> shows = new Dictionary<int, TvShow>();

		private static readonly object lockObj = new object();

		public static IEnumerable<TvShow> Shows
		{
			get
			{
				var tempShows = shows.Values.ToList();

				return tempShows;
			}
		}

		public static TvShow Show(int id) => shows.TryGet(id);

		public static List<TvShow> TemporaryShows { get; } = new List<TvShow>();

		public static event ShowChangeHandler ShowAdded;

		public static event ShowChangeHandler ShowDataChanged;

		public static Episode GetEpisode(int id)
			=> Shows.SelectMany(x => x.Seasons).SelectMany(x => x.Episodes).FirstOrDefault(x => x.Id == id);

		public static bool IsDisliked(int id) => Data.Preferences.ShowsDisliked?.Contains(id) ?? false;

		public static Episode GetNextEpisode(Episode episode)
		{
			var found = false;

			foreach (var item in episode.Show.Seasons.SelectMany(x => x.Episodes))
			{
				if (found)
					return item;

				if (item == episode)
					found = true;
			}

			return null;
		}

		public static void OnShowDataChanged(TvShow show)
		{
			ShowDataChanged?.Invoke(show);

			RunReminder();
		}

		public static void Add(TvShow show, bool save = true)
		{
			lock (lockObj)
				if (!shows.ContainsKey(show.Id) && show.Id != 0)
				{
					if (TemporaryShows.Contains(show)) TemporaryShows.Remove(show);

					show.Temporary = false;
					shows.Add(show.Id, show);

					if (show.DateAdded == DateTime.MinValue)
						show.DateAdded = DateTime.Now;

					ShowAdded?.Invoke(show);

					show.ContentInfoChanged();

					RunReminder();

					if (save)
						show.Save(ChangeType.All);
				}
		}

		public static void Remove(TvShow show)
		{
			if (DialogResult.No == MessagePrompt.Show($"Are you sure you want to remove {show.Name}?", "Remove Show?", PromptButtons.YesNo, PromptIcons.Hand, Data.Mainform))
				return;

			start: try
			{
				show.Delete();
				show.DateAdded = DateTime.MinValue;
				lock (lockObj)
					shows.Remove(show.Id);
			}
			catch
			{
				if (DialogResult.Retry == MessagePrompt.Show($"Failed to delete {show.Name}, would you like to try again?", "Failed to Delete Show", PromptButtons.RetryCancel, PromptIcons.Warning, Data.Mainform))
					goto start;
			}
		}

		internal static void RunReminder()
		{
			if (Data.Options.EpisodeNotification)
				RemindLogIdentifier.Wait(() =>
				{
					var shows = Shows.ToList();
					foreach (var show in shows)
					{
						var ep = show.Episodes.FirstOrDefault(predicate);

						if (ep == null
							|| (ep.LastReminder.Ticks != 0 && ep.LastReminder > DateTime.Today.AddDays(-40))
							|| !(ep.Previous?.Watched ?? true)) continue;

						var episodes = ep.Season.Episodes.All(predicate) ? ep.Season.Episodes.ToArray() : new[] { ep };

						Data.Mainform.TryInvoke(() =>
						{
							Notification.Create(
								(f, x) => PaintEpNotification(f, episodes, x)
								, () => { Data.Mainform.ShowUp(); Data.Mainform.PushPanel(null, episodes.Length == 1 ? new PC_Download(ep) : new PC_Download(ep.Season)); }
								, NotificationSound.Long
								, new Size(220, 110))
								.Show(Data.Mainform)
								.PictureBox.GetImage(ep.BackdropPath, 220, false);
						});

						foreach (var e in episodes)
							e.LastReminder = DateTime.Today;
						show.Save(ChangeType.Preferences);
					}

					bool predicate(Episode x) =>
							x.EN > 0 &&
							x.AirState == AirStateEnum.Aired &&
							!x.Watched &&
							!x.Playable;
				}, 10000);
		}

		public static void PaintEpNotification(SlickPictureBox pb, Episode[] eps, Graphics g)
		{
			var imgRect = new Rectangle(Point.Empty, pb.Size);
			var ep = eps[0];

			if (pb.Loading)
				pb.DrawLoader(g, imgRect.CenterR(32, 32));
			else
				g.DrawImage(pb.Image ?? ep.HugeIcon.Color(FormDesign.Design.IconColor), imgRect, ImageSizeMode.Fill);

			if (Data.Options.AlwaysShowBanners || pb.HoverState.HasFlag(HoverState.Hovered))
				g.DrawBannersOverImage(pb, imgRect, new[]
				{
					new Banner(eps.Length == 1 ? $"{ep.Show.Name} {ep.SN}x{ep.EN}" : ep.Show.Name, BannerStyle.Text, ep.Show.TinyIcon),
					new Banner(eps.Length == 1 ? ep.Name : ep.Season.Name, BannerStyle.Text, eps.Length == 1 ? ep.TinyIcon : ep.Season.TinyIcon),
					new Banner($"Aired {ep.AirDate?.RelativeString()}", BannerStyle.Active, Properties.Resources.Tiny_Clock)
				});

			g.DrawRectangle(new Pen(FormDesign.Design.AccentColor, 1F), imgRect.Pad(0, 0, 1, 1));

			if (pb.HoverState.HasFlag(HoverState.Hovered))
				g.DrawIconsOverImage(imgRect.Pad(-1), pb.PointToClient(Cursor.Position), Properties.Resources.Huge_Download);

			g.FillRectangle(new SolidBrush(FormDesign.Design.ActiveColor), 0, 0, 2, pb.Height);
		}

		public static void CreateSeason(TvShow show, Season season)
		{
			if (show[season.SeasonNumber] != null) return;

			season.Custom = true;
			season.Credits = season.Credits ?? new Credits { Cast = show.Cast, Crew = show.Crew };
			season.Images = season.Images ?? new PosterImages() { Posters = new List<ImageData>() };
			season.Name = season.Name ?? $"Season {season.SeasonNumber}";
			season.Show = show;
			season.Videos = season.Videos ?? new List<Video>();

			show.Seasons.Add(season);
		}

		public static void DeleteSeason(Season season)
		{
			season.Show.Seasons.Remove(season);
			season.Show.Save(ChangeType.Data);

			LocalShowHandler.OnWatchInfoChanged(season.Show);
		}

		public static void CreateEpisode(TvShow show, Episode episode)
		{
			var season = show[episode.SN];

			if (season == null || season[episode.EN] != null) return;

			episode.Custom = true;
			episode.Crew = episode.Crew ?? new List<Crew>();
			episode.GuestStars = episode.GuestStars ?? new List<Cast>();
			episode.Videos = episode.Videos ?? new List<Video>();
			episode.Images = episode.Images ?? new StillImages { Stills = new List<ImageData>() };

			season.Episodes.Add(episode);
		}

		public static void DeleteEpisode(Episode episode)
		{
			episode.Season.Episodes.Remove(episode);
			episode.Show.Save(ChangeType.Data);

			LocalShowHandler.OnWatchInfoChanged(episode.Show);
		}

		public static async void LoadAllShows()
		{
			foreach (var showFiles in new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Shows")).GetFilesByExtensions(".data", ".pref").GroupBy(x => x.FileName()))
			{
				TvShow show = null;

				try
				{
					show = ISave.Load<TvShow>($"Shows\\{showFiles.Key}.data");
				}
				catch { }

				if (show == null || show.Id != showFiles.Key.SmartParse())
					try { show = await TvShow.Create(showFiles.Key.SmartParse()); } catch { continue; }

				try
				{
					ISave.Load(out ShowPref showPref, $"Shows\\{showFiles.Key}.pref");

					if (showPref != null)
					{
						show.Rating = showPref.Rating;
						show.DateAdded = showPref.DateAdded;
						show.SeasonPrefs = showPref.SeasonPrefs;
					}
				}
				catch { }

				Add(show, false);
			}

			if (Directory.Exists(Path.Combine(ISave.DocsFolder, "ShowsData")))
			{
				//Load Old Shows
				foreach (var file in new DirectoryInfo(Path.Combine(ISave.DocsFolder, "ShowsData"))
					.EnumerateFiles("*.scs").Where(x => x.Length > 0))
				{
					try
					{
						var show = ISave.Load<TvShow>($"ShowsData/{file.Name}");

						if (show.DateAdded == DateTime.MinValue)
							show.DateAdded = file.CreationTime;

						Add(show, false);

						show.Save(ChangeType.All);
						file.Delete();
					}
					catch { }
				}

				try { new DirectoryInfo(Path.Combine(ISave.DocsFolder, "ShowsData")).Delete(true); } catch { }
			}
		}
	}
}