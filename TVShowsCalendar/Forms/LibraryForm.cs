using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.HandlerClasses;

namespace TVShowsCalendar.Forms
{
	public partial class LibraryForm : BaseForm
	{
		public LibraryForm() : base()
		{
			InitializeComponent();

			verticalScroll.LinkedControl = P_Tabs;
			verticalScroll.SizeSource = () => P_Tabs.Controls.Cast<Control>().Sum(c => c.If(x => x.Visible, x => x.Height, 0));

			TLP_Controls.MouseDown += Form_MouseDown;

			if (Data.Preferences.LibraryBounds != Rectangle.Empty)
				Bounds = Data.Preferences.LibraryBounds;
			else
				StartPosition = FormStartPosition.CenterScreen;
			WindowState = Data.Preferences.LibraryMax ? FormWindowState.Maximized : FormWindowState.Normal;

			LoadShowLibrary();
			LoadMovieLibrary();

			SlickTip.SetTo(B_Folders, "Local Library Folders");
			SlickTip.SetTo(B_Refresh, "Refresh Library");

			LocalShowHandler.FolderChanged += (s) => new Action(() => LoadShowLibrary(s)).RunInBackground();
			LocalShowHandler.EpisodeWatchChanged += (s) => new Action(() => LoadShowLibrary(s)).RunInBackground();

			LocalMovieHandler.FolderChanged += (s) => new Action(() => LoadMovieLibrary(s)).RunInBackground();
			LocalMovieHandler.MovieWatchChanged += (s) => new Action(() => LoadMovieLibrary(s)).RunInBackground();

			SetFlavorText();
		}

		private void LoadShowLibrary(Show show = null)
		{
         LocalShowHandler.LoadLibrary(out var onDeck, out var continueWatching, out var startWatching, out var lastWatched, show);

			if (InvokeRequired)
				Invoke(new Action(() => GenerateEpisodes(onDeck, continueWatching, startWatching, lastWatched, show)));
			else
				GenerateEpisodes(onDeck, continueWatching, startWatching, lastWatched, show);
		}

		private void LoadMovieLibrary(Movie movie = null)
		{
			LocalMovieHandler.LoadLibrary(out var onDeck, out var continueWatching, out var startWatching, movie);

			if (InvokeRequired)
				Invoke(new Action(() => GenerateMovies(onDeck, continueWatching, startWatching, movie)));
			else
				GenerateMovies(onDeck, continueWatching, startWatching, movie);
		}

		private void GenerateMovies(List<Movie> onDeck, List<Movie> continueWatching, List<Movie> startWatching, Movie movie)
		{
			FLP_OnDeck.SuspendDrawing();
			FLP_ContinueMovies.SuspendDrawing();
			FLP_StartMovies.SuspendDrawing();

			FLP_OnDeck.Controls.Clear(true, x => (x as UserControls.WatchControl).Movie != null && (movie == null || (x as UserControls.WatchControl).Movie == movie));
			FLP_ContinueMovies.Controls.Clear(true, x => movie == null || (x as UserControls.WatchControl).Movie == movie);
			FLP_StartMovies.Controls.Clear(true, x => movie == null || (x as UserControls.WatchControl).Movie == movie);

			foreach (var item in onDeck)
				FLP_OnDeck.Controls.Add(new UserControls.WatchControl(item));
			FLP_OnDeck.OrderByDescending(c => (c as UserControls.WatchControl).If(x => x.Episode != null, x => x.Episode.GetDateOrder(), x => x.Movie.WatchDate));

			foreach (var item in continueWatching)
				FLP_ContinueMovies.Controls.Add(new UserControls.WatchControl(item, false));
			FLP_ContinueMovies.OrderByDescending(x => (x as UserControls.WatchControl).Movie.WatchDate);

			foreach (var item in startWatching)
				FLP_StartMovies.Controls.Add(new UserControls.WatchControl(item, false));
			FLP_StartMovies.OrderByDescending(x => (x as UserControls.WatchControl).Movie.WatchDate);

			TLP_OnDeck.Visible = FLP_OnDeck.Controls.Count > 0;
			TLP_ContinueMovies.Visible = FLP_ContinueMovies.Controls.Count > 0;
			TLP_StartMovies.Visible = FLP_StartMovies.Controls.Count > 0;

			FLP_OnDeck.ResumeDrawing();
			FLP_ContinueMovies.ResumeDrawing();
			FLP_StartMovies.ResumeDrawing();

			StopLoader();
		}

		private void GenerateEpisodes(List<Episode> onDeck, List<Episode> continueWatching, List<Episode> startWatching, List<Episode> rewatch, Show refShow)
		{
			FLP_OnDeck.SuspendDrawing();
			FLP_Continue.SuspendDrawing();
			FLP_Start.SuspendDrawing();
			FLP_Rewatch.SuspendDrawing();

			FLP_OnDeck.Controls.Clear(true, x => (x as UserControls.WatchControl).Episode != null && (refShow == null || (x as UserControls.WatchControl).Episode.Show == refShow));
			FLP_Continue.Controls.Clear(true, x => refShow == null || (x as UserControls.WatchControl).Episode.Show == refShow);
			FLP_Start.Controls.Clear(true, x => refShow == null || (x as UserControls.WatchControl).Episode.Show == refShow);
			FLP_Rewatch.Controls.Clear(true, x => refShow == null || (x as UserControls.WatchControl).Episode.Show == refShow);

			foreach (var item in onDeck)
                FLP_OnDeck.Controls.Add(new UserControls.WatchControl(item));
			FLP_OnDeck.OrderByDescending(c => (c as UserControls.WatchControl).If(x => x.Episode != null, x => x.Episode.GetDateOrder(), x => x.Movie.WatchDate));

			foreach (var item in continueWatching)
				FLP_Continue.Controls.Add(new UserControls.WatchControl(item, false));
			FLP_Continue.OrderByDescending(x => (x as UserControls.WatchControl).Episode.GetDateOrder());

			foreach (var item in startWatching)
				FLP_Start.Controls.Add(new UserControls.WatchControl(item, false));
			FLP_Start.OrderByDescending(x => (x as UserControls.WatchControl).Episode.GetDateOrder());

			foreach (var item in rewatch)
				FLP_Rewatch.Controls.Add(new UserControls.WatchControl(item, false));
			FLP_Rewatch.OrderByDescending(x => (x as UserControls.WatchControl).Episode.GetDateOrder());

			if (LocalShowHandler.GeneralFolders.Count == 0)
               L_Status.Text = "Add Show folders using the button on the right to start.";
         else if ((0).AllOf(onDeck.Count(), continueWatching.Count()))
         {
            if (startWatching.Count() == 0)
               L_Status.Text = "Wait for more episodes to come or download episodes to watch";
            else
               L_Status.Text = "Re-Watch older episodes while newer ones Air";
         }
         else
            L_Status.Text = "Press on an Episode to start the Binge!";

			TLP_OnDeck.Visible = FLP_OnDeck.Controls.Count > 0;
			TLP_Continue.Visible = FLP_Continue.Controls.Count > 0;
			TLP_Start.Visible = FLP_Start.Controls.Count > 0;
			TLP_Rewatch.Visible = FLP_Rewatch.Controls.Count > 0;

			FLP_OnDeck.ResumeDrawing();
			FLP_Continue.ResumeDrawing();
			FLP_Start.ResumeDrawing();
			FLP_Rewatch.ResumeDrawing();
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			if (L_OnDeck == null)
				return;

			L_OnDeck.ForeColor = design.ActiveColor;

			FL_OnDeck.ForeColor = FL_ContinueEpisodes.ForeColor =
				FL_StartShows.ForeColor = FL_Rewatch.ForeColor =
				FL_ContinueMovies.ForeColor = FL_StartMovies.ForeColor = design.InfoColor;

			P_Line1.BackColor = P_Line2.BackColor =
				P_Line3.BackColor = P_Line4.BackColor =
				P_ContinueMovies.BackColor = P_StartMovies.BackColor = design.Type.If(FormDesignType.Dark, design.LightColor, design.DarkColor);

			L_Status.ForeColor = L_Continue.ForeColor =
				L_Rewatch.ForeColor = L_Start.ForeColor =
				L_ContinueMovies.ForeColor = L_StartMovies.ForeColor = design.LabelColor;

			PB_OnDeck.Color(design.ActiveColor);
			PB_Continue.Color(design.IconColor);
			PB_ContinueMovies.Color(design.IconColor);
			PB_StartMovies.Color(design.IconColor);
			PB_StartShows.Color(design.IconColor);
			PB_Rewatch.Color(design.IconColor);
		}

		private void B_Folders_Click(object sender, EventArgs e) => new ShowFolderSelection().ShowDialog();

		DisableIdentifier RefreshIdentifier = new DisableIdentifier();
		private void B_Refresh_Click(object sender, EventArgs e)
		{
			if (!RefreshIdentifier.Disable(2000))
				new Action(() =>
				{
					StartLoader();
					LocalShowHandler.LoadFiles();
					LocalMovieHandler.ReloadMovieFiles();
					Invoke(new Action(SetFlavorText));
				}).RunInBackground();
		}

		private void SetFlavorText()
		{
			FL_OnDeck.Text = new string[]
			{
				"Where were we?",
				"Must. Binge. More.",
				"Yes, those are the things you were watching.",
				"Gotcha covered, buddy!",
				"The temptation is small, but your will is weak, press on one."
			}.Random();

			FL_ContinueEpisodes.Text = new string[]
			{
				"Oh yeah.. Someone forgot about those..",
				"My miiiind is telling me noooo, but my body, my boooody is telling me YESS!",
				"Pick up that episode you started!",
				"Pick up that episode you started god damn-it!",
				"Remember.. mee?",
				"Oh shoot, someone fell off his schedule.."
			}.Random();

			FL_ContinueMovies.Text = new string[]
			{
				"Oh yeah.. Someone forgot about those..",
				"How does someone forget a movie?",
				"Pick up that movie you started!",
				"Pick up that movie you started god damn-it!",
				"Movies. Right. Gotta continue those."
			}.Random();

			FL_StartShows.Text = new string[]
			{
				"Oh boy, oh boy!",
				"Shiny!",
				"Start something new.",
				"You know those episodes above ain't got what it takes."
			}.Random();

			FL_StartMovies.Text = new string[]
			{
				"Laiche Vitrine.",
				"Start something new.",
			}.Random();

			FL_Rewatch.Text = new string[]
			{
				"Be sure you haven't missed anything"
			}.Random();
		}

		private void LibraryForm_ResizeEnd(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Normal)
				Data.Preferences.LibraryBounds = Bounds;
			Data.Preferences.LibraryMax = WindowState == FormWindowState.Maximized;
			Data.Preferences.Save();
		}

		private void base_P_Content_Resize(object sender, EventArgs e)
		{
			P_Tabs.MaximumSize = new Size(base_P_Content.Width - 12, 9999);
			P_Tabs.MinimumSize = new Size(base_P_Content.Width - 12, 0);
		}
	}
}
