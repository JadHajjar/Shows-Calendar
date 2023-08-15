using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public static class MovieManager
	{
		public delegate void MovieChangeHandler(Movie movie);

		private static readonly WaitIdentifier RemindLogIdentifier = new WaitIdentifier();

		private static readonly Dictionary<int, Movie> movies = new Dictionary<int, Movie>();

		private static readonly object lockObj = new object();

		public static IEnumerable<Movie> Movies
		{
			get
			{
				var tempMovies = movies.Values.ToList();

				return tempMovies;
			}
		}

		public static Movie Movie(int id) => movies.TryGet(id);

		public static List<Movie> TemporaryMovies { get; } = new List<Movie>();

		public static event MovieChangeHandler MovieAdded;

		public static event MovieChangeHandler MovieDataChanged;

		public static void OnMovieDataChanged(Movie movie)
		{
			MovieDataChanged?.Invoke(movie);

			RunReminder();
		}

		public static void Add(Movie movie, bool save = true)
		{
			lock (lockObj)
				if (!movies.ContainsKey(movie.Id) && movie.Id != 0)
				{
					if (TemporaryMovies.Contains(movie)) TemporaryMovies.Remove(movie);

					movie.Temporary = false;
					movies.Add(movie.Id, movie);

					if (movie.DateAdded == DateTime.MinValue)
						movie.DateAdded = DateTime.Now;

					MovieAdded?.Invoke(movie);

					movie.ContentInfoChanged();

					if (save)
						movie.Save(ChangeType.All);
				}
		}

		public static bool IsDisliked(int id) => Data.Preferences.MoviesDisliked?.Contains(id) ?? false;

		public static void Remove(Movie movie)
		{
			if (DialogResult.No == MessagePrompt.Show($"Are you sure you want to remove {movie.Title}?", "Remove Movie?", PromptButtons.YesNo, PromptIcons.Hand, Data.Mainform))
				return;

			start: try
			{
				movie.Delete();
				movie.DateAdded = DateTime.MinValue;
				lock (lockObj)
					movies.Remove(movie.Id);
			}
			catch
			{
				if (DialogResult.Retry == MessagePrompt.Show($"Failed to remove {movie.Title}, would you like to try again?", "Failed to Remove Movie", PromptButtons.RetryCancel, PromptIcons.Warning, Data.Mainform))
					goto start;
			}
		}

		public static void RunReminder()
		{
			if (Data.Options.EpisodeNotification)
				RemindLogIdentifier.Wait(() =>
				{
					var movies = Movies.ToList();
					foreach (var mov in movies)
					{
						if (!mov.Watched && !mov.Playable)
						{
							if (mov.LastReminder < DateTime.Today.AddDays(-40) && mov.ReleaseDate < DateTime.Today)
								mov.LastReminder = DateTime.Today;
							else if (mov.LastUpcomingReminder < DateTime.Today.AddDays(-40) && mov.ReleaseDate < DateTime.Today.AddDays(20))
								mov.LastUpcomingReminder = DateTime.Today;
							else
								continue;

							Data.Mainform.TryInvoke(() =>
							{
								Notification.Create(
									(f, x) => PaintEpNotification(f, mov, x)
									, () => { Data.Mainform.ShowUp(); Data.Mainform.PushPanel(null, new PC_Download(mov)); }
									, NotificationSound.Long
									, new Size(220, 110))
									.Show(Data.Mainform)
									.PictureBox.GetImage(mov.BackdropPath, 220, false);
							});

							mov.Save(ChangeType.Preferences);
						}
					}
				}, 10000);
		}

		private static void PaintEpNotification(SlickPictureBox pb, Movie movie, Graphics g)
		{
			var imgRect = new Rectangle(Point.Empty, pb.Size);

			if (pb.Loading)
				pb.DrawLoader(g, imgRect.CenterR(32, 32));
			else
				g.DrawImage(pb.Image ?? movie.HugeIcon.Color(FormDesign.Design.IconColor), imgRect, ImageSizeMode.Fill);

			if (Data.Options.AlwaysShowBanners || pb.HoverState.HasFlag(HoverState.Hovered))
				g.DrawBannersOverImage(pb, imgRect, new[]
				{
					new Banner(movie.Name, BannerStyle.Text, movie.TinyIcon),
					new Banner($"Released {movie.AirDate?.RelativeString()}", BannerStyle.Active, Properties.Resources.Tiny_Clock)
				});

			g.DrawRectangle(new Pen(FormDesign.Design.AccentColor, 1F), imgRect.Pad(0, 0, 1, 1));

			if (pb.HoverState.HasFlag(HoverState.Hovered))
				g.DrawIconsOverImage(imgRect.Pad(-1), pb.PointToClient(Cursor.Position), Properties.Resources.Huge_Download);

			g.FillRectangle(new SolidBrush(FormDesign.Design.ActiveColor), 0, 0, 2, pb.Height);
		}

		public static async void LoadAllMovies()
		{
			foreach (var movieFiles in new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Movies")).GetFilesByExtensions(".data", ".pref").GroupBy(x => x.FileName()))
			{
				Movie movie = null;

				try
				{
					movie = ISave.Load<Movie>($"Movies\\{movieFiles.Key}.data");
				}
				catch { }

				if (movie == null || movie.Id != movieFiles.Key.SmartParse())
					try { movie = await ShowsCalendar.Movie.Create(movieFiles.Key.SmartParse()); } catch { continue; }

				try
				{
					ISave.Load(out MoviePref moviePref, $"Movies\\{movieFiles.Key}.pref");

					if (moviePref != null)
					{
						movie.Rating = moviePref.Rating;
						movie.DateAdded = moviePref.DateAdded;
						movie.Watched = moviePref.Watched;
						movie.Progress = moviePref.Progress;
						movie.WatchDate = moviePref.WatchDate;
						movie.WatchTime = moviePref.WatchTime;
						movie.RawVidFiles = moviePref.RawVidFiles;
						movie.LastReminder = moviePref.LastReminder;
						movie.LastUpcomingReminder = moviePref.LastUpcomingReminder;
					}
				}
				catch { }

				Add(movie, false);
			}

			if (Directory.Exists(Path.Combine(ISave.DocsFolder, "MoviesData")))
			{
				//Load Old Movies
				foreach (var file in new DirectoryInfo(Path.Combine(ISave.DocsFolder, "MoviesData"))
					.EnumerateFiles("*.scm").Where(x => x.Length > 0))
				{
					try
					{
						var movie = ISave.Load<Movie>($"MoviesData/{file.Name}");

						if (movie.DateAdded == DateTime.MinValue)
							movie.DateAdded = file.CreationTime;

						Add(movie, false);

						movie.Save(ChangeType.All);
						file.Delete();
					}
					catch { }
				}

				try { new DirectoryInfo(Path.Combine(ISave.DocsFolder, "MoviesData")).Delete(true); } catch { }
			}
		}
	}
}