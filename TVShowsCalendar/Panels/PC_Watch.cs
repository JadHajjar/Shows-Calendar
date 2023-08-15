using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PC_Watch : PanelContent
	{
		private readonly SlickSectionPanel[] panels;

		public PC_Watch()
		{
			InitializeComponent();

			verticalScroll.LinkedControl = P_Tabs;

			panels = new[]
			{
				SP_OnDeck,
				SP_ContinueEps,
				SP_ContinueMovies,
				SP_StartShows,
				SP_StartMovies,
				SP_RewatchEps
			};

			panels.Foreach(x => x.Parent = null);

			LocalShowHandler.WatchInfoChanged += LocalShowHandler_FolderChanged;
			LocalMovieHandler.WatchInfoChanged += LocalMovieHandler_FolderChanged;
		}

		private void LocalMovieHandler_FolderChanged(object sender, Movie s)
			=> new BackgroundAction(() => LoadMovieLibrary(s)).Run();

		private void LocalShowHandler_FolderChanged(object sender, TvShow s)
			=> new BackgroundAction(() => LoadShowLibrary(s)).Run();

		private void LoadShowLibrary(TvShow show = null)
		{
			LocalShowHandler.LoadLibrary(out var onDeck, out var continueWatching, out var startWatching, out var lastWatched, show);

			this.TryInvoke(() => GenerateEpisodes(onDeck, continueWatching, startWatching, lastWatched, show));
		}

		private void LoadMovieLibrary(Movie movie = null)
		{
			LocalMovieHandler.LoadLibrary(out var onDeck, out var continueWatching, out var startWatching, movie);

			this.TryInvoke(() => GenerateMovies(onDeck, continueWatching, startWatching, movie));
		}

		private void GenerateMovies(List<Movie> onDeck, List<Movie> continueWatching, List<Movie> startWatching, Movie movie)
		{
			PB_FirstLoad.Visible = false;

			P_Tabs.SuspendDrawing();

			var currentControls = panels.SelectMany(c =>
				c.Controls.OfType<WatchControl<Movie>>().Where(x => movie == null || x.Content == movie))
				.ToList();

			var changes = new List<Tuple<SlickSectionPanel, Movie, bool>>();

			foreach (var item in onDeck)
				changes.Add(new Tuple<SlickSectionPanel, Movie, bool>(SP_OnDeck, item, true));
			foreach (var item in continueWatching)
				changes.Add(new Tuple<SlickSectionPanel, Movie, bool>(SP_ContinueMovies, item, false));
			foreach (var item in startWatching)
				changes.Add(new Tuple<SlickSectionPanel, Movie, bool>(SP_StartMovies, item, false));

			for (var i = 0; i < changes.Count; i++)
			{
				var item = changes[i];
				var current = currentControls.FirstOrDefault(x => x.Content == item.Item2);

				if (current != null && current.OnDeck == item.Item3)
				{
					if (current.Parent != item.Item1)
						current.Parent = item.Item1;

					current.Invalidate();
					changes.Remove(item);
					i--;
				}
				else
				{
					current?.Dispose();
					item.Item1.Add(new WatchControl<Movie>(item.Item2, item.Item3));
				}

				if (current != null)
					currentControls.Remove(current);
			}

			currentControls.ForEach(x => x.Dispose());

			SP_OnDeck.OrderByDescending(c => c is WatchControl<Movie> wm ? wm.Content.WatchDate : (c as WatchControl<Episode>).Content.GetDateOrder());
			SP_ContinueMovies.OrderByDescending(x => (x as WatchControl<Movie>).Content.WatchDate);
			SP_StartMovies.OrderByDescending(x => (x as WatchControl<Movie>).Content.WatchDate);

			foreach (var panel in panels)
			{
				panel.Parent = panel.Controls.Count == 0 ? null : P_Tabs;
				panel.BringToFront();
			}

			TLP_NoShows.Visible = !P_Tabs.Controls.OfType<SlickSectionPanel>().Any(x => x.Controls.Count > 0);

			P_Tabs.ResumeDrawing();

			StopLoader();
		}

		private void GenerateEpisodes(List<Episode> onDeck, List<Episode> continueWatching, List<Episode> startWatching, List<Episode> rewatch, TvShow refShow)
		{
			PB_FirstLoad.Visible = false;

			P_Tabs.SuspendDrawing();

			var currentControls = panels.SelectMany(c =>
				c.Controls.OfType<WatchControl<Episode>>().Where(x => refShow == null || x.Content.Show == refShow))
				.ToList();

			var changes = new List<Tuple<SlickSectionPanel, Episode, bool>>();

			foreach (var item in onDeck)
				changes.Add(new Tuple<SlickSectionPanel, Episode, bool>(SP_OnDeck, item, true));
			foreach (var item in continueWatching)
				changes.Add(new Tuple<SlickSectionPanel, Episode, bool>(SP_ContinueEps, item, false));
			foreach (var item in startWatching)
				changes.Add(new Tuple<SlickSectionPanel, Episode, bool>(SP_StartShows, item, false));
			foreach (var item in rewatch)
				changes.Add(new Tuple<SlickSectionPanel, Episode, bool>(SP_RewatchEps, item, false));

			for (var i = 0; i < changes.Count; i++)
			{
				var item = changes[i];
				var current = currentControls.FirstOrDefault(x => x.Content == item.Item2);

				if (current != null && current.OnDeck == item.Item3)
				{
					if (current.Parent != item.Item1)
						current.Parent = item.Item1;

					current.Invalidate();
					changes.Remove(item);
					i--;
				}
				else
				{
					current?.Dispose();
					item.Item1.Add(new WatchControl<Episode>(item.Item2, item.Item3));
				}

				if (current != null)
					currentControls.Remove(current);
			}

			currentControls.ForEach(x => x.Dispose());

			SP_OnDeck.OrderByDescending(c => c is WatchControl<Movie> wm ? wm.Content.WatchDate : (c as WatchControl<Episode>).Content.GetDateOrder());
			SP_ContinueEps.OrderByDescending(x => (x as WatchControl<Episode>).Content.GetDateOrder());
			SP_StartShows.OrderByDescending(x => (x as WatchControl<Episode>).Content.GetDateOrder());
			SP_RewatchEps.OrderByDescending(x => (x as WatchControl<Episode>).Content.GetDateOrder());

			foreach (var panel in panels)
			{
				panel.Parent = panel.Controls.Count == 0 ? null : P_Tabs;
				panel.BringToFront();
			}

			TLP_NoShows.Visible = !P_Tabs.Controls.ThatAre<SlickSectionPanel>().Any(x => x.Controls.Count > 0);

			P_Tabs.ResumeDrawing();
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			L_NoShowsInfo.ForeColor = design.InfoColor;

			L_NoShows.ForeColor = design.LabelColor;
		}

		private void verticalScroll_Scroll(object sender, ScrollEventArgs e) => spacer.Visible = verticalScroll.Percentage > 0;

		private void PC_Watch_Load(object sender, EventArgs e)
		{
			new BackgroundAction(() => LoadShowLibrary()).Run();
			new BackgroundAction(() => LoadMovieLibrary()).Run();
		}
	}
}