using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PC_Dashboard : PanelContent
	{
		private readonly SlickSectionPanel[] panels;

		public PC_Dashboard()
		{
			InitializeComponent();

			panels = new[]
			{
				SP_OnDeck,
				SP_ContinueWatching,
				SP_RecentEps,
				SP_RecentMovies,
				SP_UpcomingEps,
				SP_UpcomingMovies,
				SP_Curation,
				SP_SimilarContent,
			};

			panels.Foreach(x => x.Parent = null);

			PB_FirstLoad.Visible = false;
			FirstFocusedControl = SP_OnDeck;

			var shows = ShowManager.Shows.ToList();
			var movies = MovieManager.Movies.ToList();

			foreach (var item in shows)
				ManageShow(item);

			foreach (var item in movies)
				ManageMovie(item);

			var tags =	shows.SelectMany(x => x.Keywords).GroupBy(x => x.ToLower()).Where(x => x.Count() > shows.Count() / 15).Select(x => x.Key).Concat(
						movies.SelectMany(x => x.Keywords).GroupBy(x => x.ToLower()).Where(x => x.Count() > movies.Count() / 15).Select(x => x.Key)).ToList();

			var similarShows = shows.SelectMany(x => x.SimilarShows ?? Array.Empty<LightContent>()).Where(x => !shows.Any(y => y.Id == x.Id) && !ShowManager.IsDisliked(x.Id)).GroupBy(x => x.Id).OrderByDescending(x => x.Count()).Select(x => (x.Count(), x.First()));
			var similarMovies = movies.SelectMany(x => x.SimilarMovies ?? Array.Empty<LightContent>()).Where(x => !movies.Any(y => y.Id == x.Id) && !MovieManager.IsDisliked(x.Id)).GroupBy(x => x.Id).OrderByDescending(x => x.Count()).Select(x => (x.Count(), x.First()));

			var similar = new List<MediaViewer>();
			foreach (var item in similarShows.Where(x => x.Item1 > shows.Count() / 15).Shuffle().Take(6))
				similar.Add(new MediaViewer(item.Item2));
			foreach (var item in similarMovies.Where(x => x.Item1 > movies.Count() / 15).Shuffle().Take(6))
				similar.Add(new MediaViewer(item.Item2));

			new BackgroundAction(async () =>
			{
				var extramovies = new List<LightContent>();

				if (ConnectionHandler.IsConnected)
					foreach (var t in tags.Shuffle().Take(3))
						extramovies.AddRange((await Data.TMDbHandler.FindMoviesByTag(t, ExtensionClass.RNG.Next(0, 3))).Where(x => !movies.Any(y => y.Id == x.Id) && !similarMovies.Any(y => y.Item2.Id == x.Id)).Select(x => LightContent.Convert(x)));

				this.TryInvoke(() =>
				{
					similar.AddRange(extramovies.Shuffle().Take(12).Select(x => new MediaViewer(x)));

					SP_SimilarContent.Add(similar.Shuffle()); 
				});
			}).Run();

			LocalShowHandler.WatchInfoChanged += ManageShowChange;
			LocalMovieHandler.WatchInfoChanged += ManageMovieChange;
			ShowManager.ShowAdded += ManageShow;
			MovieManager.MovieAdded += ManageMovie;

			StartLoader();
		}

		private void ManageMovieChange(object sender, Movie movie) =>
			new BackgroundAction(() =>
			{
				LocalMovieHandler.LoadLibrary(out var onDeck, movie);
				LocalMovieHandler.GetCuration(out var curation, movie);
				this.TryInvoke(() =>
				{
					PB_FirstLoad.Visible = false;
					var controls = new List<WatchControl<Movie>>();

					foreach (var item in onDeck)
					{
						var current = SP_OnDeck.Controls.OfType<WatchControl<Movie>>().FirstOrDefault(x => x.Content == item);

						if (current == null)
						{
							var c = new WatchControl<Movie>(item);

							controls.Add(c);
							SP_OnDeck.Controls.Add(c);
						}
						else
						{
							current.Invalidate();
							controls.Add(current);
						}
					}

					foreach (var item in SP_OnDeck.Controls.OfType<WatchControl<Movie>>().Where(x => !controls.Contains(x)))
					{
						if (movie == null || item.Content == movie)
							item.Dispose();
					}

					SP_OnDeck.OrderByDescending(c => c is WatchControl<Movie> wm ? wm.Content.WatchDate : ((c as WatchControl<Episode>).Content as Episode).GetDateOrder());
					controls.Clear();

					foreach (var item in curation.Where(x => !onDeck.Contains(x)))
					{
						var current = SP_Curation.Controls.OfType<WatchControl<Movie>>().FirstOrDefault(x => x.Content == item);

						if (current == null)
						{
							var c = new WatchControl<Movie>(item, false);

							controls.Add(c);
							SP_Curation.Controls.Add(c);
						}
						else
						{
							current.Invalidate();
							controls.Add(current);
						}
					}

					foreach (var item in SP_Curation.Controls.OfType<WatchControl<Movie>>().Where(x => x.Content != null && !controls.Contains(x)))
					{
						if (movie == null || item.Content == movie)
							item.Dispose();
					}

					SP_OnDeck.OrderByDescending(c => c is WatchControl<Movie> wm ? wm.Content.WatchDate : ((c as WatchControl<Episode>).Content as Episode).GetDateOrder());

					if (IsHandleCreated)
						P_Tabs.SuspendDrawing();

					foreach (var panel in panels)
					{
						panel.Parent = panel.Controls.Count == 0 ? null : P_Tabs;
						panel.BringToFront();
					}

					if (IsHandleCreated)
						P_Tabs.ResumeDrawing();
				});
			}).Run();

		private void ManageShowChange(object sender, TvShow show) =>
			new BackgroundAction(() =>
			{
				PB_FirstLoad.Visible = false;
				LocalShowHandler.LoadLibrary(out var onDeck, show);
				LocalShowHandler.GetCuration(out var curation, show);
				LocalShowHandler.LoadLibrary(out var downloads, show, true);
				this.TryInvoke(() =>
				{
					var controls = new List<WatchControl<Episode>>();

					foreach (var item in onDeck)
					{
						var current = SP_OnDeck.Controls.OfType<WatchControl<Episode>>().FirstOrDefault(x => x.Content == item);

						if (current == null)
						{
							var c = new WatchControl<Episode>(item);

							controls.Add(c);
							SP_OnDeck.Controls.Add(c);
						}
						else
						{
							current.Invalidate();
							controls.Add(current);
						}
					}

					foreach (var item in downloads)
					{
						var current = SP_ContinueWatching.Controls.OfType<WatchControl<Episode>>().FirstOrDefault(x => x.Content == item);

						if (current == null)
						{
							var c = new WatchControl<Episode>(item);

							controls.Add(c);
							SP_ContinueWatching.Controls.Add(c);
						}
						else
						{
							current.Invalidate();
							controls.Add(current);
						}
					}

					foreach (var item in SP_OnDeck.Controls.OfType<WatchControl<Episode>>().Where(x => x.Content != null && !controls.Contains(x)))
					{
						if (show == null || item.Content.Show == show)
							item.Dispose();
					}

					foreach (var item in SP_ContinueWatching.Controls.OfType<WatchControl<Episode>>().Where(x => x.Content != null && !controls.Contains(x)))
					{
						if (show == null || item.Content.Show == show)
							item.Dispose();
					}

					SP_OnDeck.OrderByDescending(c => c is WatchControl<Movie> wm ? wm.Content.WatchDate : ((c as WatchControl<Episode>).Content as Episode).GetDateOrder());
					controls.Clear();

					foreach (var item in curation.Where(x => !onDeck.Contains(x)))
					{
						var current = SP_Curation.Controls.OfType<WatchControl<Episode>>().FirstOrDefault(x => x.Content == item);

						if (current == null)
						{
							var c = new WatchControl<Episode>(item, false);

							controls.Add(c);
							SP_Curation.Controls.Add(c);
						}
						else
						{
							current.Invalidate();
							controls.Add(current);
						}
					}

					foreach (var item in SP_Curation.Controls.OfType<WatchControl<Episode>>().Where(x => x.Content != null && !controls.Contains(x)))
					{
						if (show == null || item.Content.Show == show)
							item.Dispose();
					}

					SP_Curation.OrderByDescending(c => c is WatchControl<Movie> wm ? wm.Content.WatchDate : ((c as WatchControl<Episode>).Content as Episode).GetDateOrder());

					if (IsHandleCreated)
						P_Tabs.SuspendDrawing();

					foreach (var panel in panels)
					{
						panel.Parent = panel.Controls.Count == 0 ? null : P_Tabs;
						panel.BringToFront();
					}

					if (IsHandleCreated)
						P_Tabs.ResumeDrawing();
				});

				StopLoader();
			}).Run();

		private void ManageMovie(Movie movie) => this.TryInvoke(() =>
		{
			var addedNew = false;
			var addedOld = false;

			if (movie == null) return;

			if ((movie.ReleaseDate ?? DateTime.MaxValue).If(x => x < DateTime.Today && x > DateTime.Today.AddMonths(-1), true, false))
			{
				var current = SP_RecentMovies.Controls.OfType<ContentControl<Movie>>().FirstOrDefault(x => x.Content == movie);

				if (current == null)
					SP_RecentMovies.Controls.Add(new ContentControl<Movie>(movie, true));
				else
					current.Invalidate();

				addedNew = true;
			}
			else if ((movie.ReleaseDate ?? DateTime.MinValue).If(x => x >= DateTime.Today && x < DateTime.Today.AddMonths(12), true, false))
			{
				var current = SP_UpcomingMovies.Controls.OfType<ContentControl<Movie>>().FirstOrDefault(x => x.Content == movie);

				if (current == null)
					SP_UpcomingMovies.Controls.Add(new ContentControl<Movie>(movie, true));
				else
					current.Invalidate();

				addedOld = true;
			}

			if (!addedNew)
				SP_RecentMovies.Controls.OfType<ContentControl<Movie>>().Where(x => x.Content == movie).Foreach(x => x.Dispose());

			if (!addedOld)
				SP_UpcomingMovies.Controls.OfType<ContentControl<Movie>>().Where(x => x.Content == movie).Foreach(x => x.Dispose());

			SP_RecentMovies.OrderByDescending(c => (c as ContentControl<Movie>).Content.ReleaseDate);
			SP_UpcomingMovies.OrderBy(c => (c as ContentControl<Movie>).Content.ReleaseDate);
			SP_OnDeck.OrderByDescending(c => c is WatchControl<Movie> wm ? wm.Content.WatchDate : ((c as WatchControl<Episode>).Content as Episode).GetDateOrder());

			if (IsHandleCreated)
				P_Tabs.SuspendDrawing();

			foreach (var panel in panels)
			{
				panel.Parent = panel.Controls.Count == 0 ? null : P_Tabs;
				panel.BringToFront();
			}

			if (IsHandleCreated)
				P_Tabs.ResumeDrawing();
		});

		private void ManageShow(TvShow show) => this.TryInvoke(() =>
		{
			var addedNew = false;
			var addedOld = false;

			if (show == null) return;

			if ((!show.LastEpisode?.Empty ?? false) && show.LastEpisode.AirDate > DateTime.Today.AddDays(-7))
			{
				var current = SP_RecentEps.Controls.OfType<ContentControl<Episode>>().FirstOrDefault(x => x.Content.Show == show);

				if (current == null)
				{
					SP_RecentEps.Controls.Add(new ContentControl<Episode>(show.LastEpisode, true));
				}
				else
				{
					current.UnInstall();
					current.Install(show.LastEpisode);
					current.Invalidate();
				}

				addedNew = true;
			}

			if ((!show.NextEpisode?.Empty ?? false) && show.NextEpisode.AirDate < DateTime.Today.AddDays(30))
			{
				var current = SP_UpcomingEps.Controls.OfType<ContentControl<Episode>>().FirstOrDefault(x => x.Content.Show == show);

				if (current == null)
				{
					SP_UpcomingEps.Controls.Add(new ContentControl<Episode>(show.NextEpisode, true));
				}
				else
				{
					current.UnInstall();
					current.Install(show.NextEpisode);
					current.Invalidate();
				}

				addedOld = true;
			}

			if (!addedNew)
				SP_RecentEps.Controls.OfType<ContentControl<Episode>>().Where(x => x.Content.Show == show).Foreach(x => x.Dispose());

			if (!addedOld)
				SP_UpcomingEps.Controls.OfType<ContentControl<Episode>>().Where(x => x.Content.Show == show).Foreach(x => x.Dispose());

			SP_RecentEps.OrderByDescending(c => (c as ContentControl<Episode>).Content.AirDate);
			SP_UpcomingEps.OrderBy(c => (c as ContentControl<Episode>).Content.AirDate);
			SP_OnDeck.OrderByDescending(c => c is WatchControl<Movie> wm ? wm.Content.WatchDate : ((c as WatchControl<Episode>).Content as Episode).GetDateOrder());

			if (IsHandleCreated)
				P_Tabs.SuspendDrawing();

			foreach (var panel in panels)
			{
				panel.Parent = panel.Controls.Count == 0 ? null : P_Tabs;
				panel.BringToFront();
			}

			if (IsHandleCreated)
				P_Tabs.ResumeDrawing();
		});

		private void verticalScroll_Scroll(object sender, ScrollEventArgs e) => spacer.Visible = verticalScroll.Percentage > 0;

		private void PC_Dashboard_Load(object sender, EventArgs e)
		{
			ManageShowChange(null, null);
			ManageMovieChange(null, null);
		}
	}
}