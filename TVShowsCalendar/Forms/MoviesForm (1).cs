using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.Forms;
using ProjectImages = TVShowsCalendar.Properties.Resources;

namespace TVShowsCalendar
{
	public partial class MoviesForm : BaseForm
	{
		public bool MoviesLoading { get; private set; }
		private SortingType SortType => Data.Preferences.MovieSortType;
		private bool Ascending => Data.Preferences.MovieSortAscending;
		private MovieTile LastTile;

		public MoviesForm()
		{
			InitializeComponent();
			ML_Clear.Click += (s, e) => Clear_Click();

			MovieManager.LoadAllMovies();
			MovieManager.MovieLoaded += MovieLoaded;

			foreach (var movie in MovieManager.Movies)
			{
				movie.Tile = new MovieTile(movie);
				FLP_Tiles.Controls.Add(movie.Tile);
			}

			LastTile = null;
			verticalScroll.LinkedControl = TLP_TileContainer;

			if (FLP_Tiles.Controls.Count == 0)
			{
				L_NoShows.Visible = true;
				TLP_TileContainer.Height = 35;
			}
			else if (L_NoShows.Visible)
			{
				L_NoShows.Visible = false;
				TLP_TileContainer.Height = FLP_Tiles.Height + 6;
			}

			if (Data.Preferences.MoviesBounds != Rectangle.Empty)
				Bounds = Data.Preferences.MoviesBounds;
			else
				StartPosition = FormStartPosition.CenterScreen;
			WindowState = Data.Preferences.MovieMax ? FormWindowState.Maximized : FormWindowState.Normal;

			SlickTip.SetTo(ML_Clear, "Clear Search");
			SlickTip.SetTo(B_Sort, "Sort Movies");
			SlickTip.SetTo(B_Discover, "Discover Trending Movies");
			SlickTip.SetTo(B_Folders, "Local Library Folders");

			if (MovieManager.Movies.Any(x => !x.Loaded))
			{
				StartLoader();
				//Data.MainForm.StartLoader();
				MoviesLoading = true;
			}
		}

		#region General Methods

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			L_NoShows.ForeColor = design.LabelColor;
		}

		private OneWayTask NotifyIdentifier = new OneWayTask();

		private void MovieLoaded(Movie movie)
		{
			var loaded = MovieManager.Movies.All(x => x.Loaded);

			if (loaded)
			{
				//if (!Data.ShowForm.ShowsLoading)
				//	Data.MainForm.StopLoader();
				StopLoader();
				MoviesLoading = false;
				NotifyIdentifier.Run(() =>
				{
					Thread.Sleep(12500);
					Data.MainForm.TryInvoke(ShowNewMovies);
				});
			}

			this.TryInvoke(() =>
			{
				SortMovies();
				if (LastTile != null && LastTile.LinkedMovie == movie)
				{
					verticalScroll.SetPerc(100D * LastTile.Location.Y / (FLP_Tiles.Height - P_ContentShows.Height));
					LastTile = null;
				}
			});
		}

		private void ShowNewMovies()
		{
			ISave.Load(out Dictionary<string, DateTime> remindLog, "RemindLog.tf");

			var movies = MovieManager.Movies
				.Where(x => x != null
					&& x.TMDbData.ReleaseDate < DateTime.Today.AddDays(15)
					&& !remindLog.ContainsKey(x.TMDbData.Id.ToString()));

			if (movies.Any())
				(new NotifyForm(movies)).ShowUp();

			foreach (var item in movies)
				remindLog.Add(item.TMDbData.Id.ToString(), DateTime.Now);

			var oldEps = remindLog.Where(x => x.Key != "" && x.Value < DateTime.Now.AddDays(-60)).ToArray();

			foreach (var item in oldEps)
				remindLog.Remove(item.Key);

			ISave.Save(remindLog, "RemindLog.tf");
		}

		private void SortMovies()
		{
			if (FLP_Tiles.InvokeRequired)
			{
				FLP_Tiles.Invoke(new Action(SortMovies));
				return;
			}

			switch (SortType)
			{
				case SortingType.Name:
					if (Ascending)
						FLP_Tiles.OrderBy(x => (x as MovieTile).LinkedMovie.Tile);
					else
						FLP_Tiles.OrderByDescending(x => (x as MovieTile).LinkedMovie.Tile);
					break;

				case SortingType.LastAirDate:
					if (Ascending)
						FLP_Tiles.OrderBy(x => (x as MovieTile).LinkedMovie.TMDbData?.ReleaseDate ?? DateTime.MaxValue);
					else
						FLP_Tiles.OrderByDescending(x => (x as MovieTile).LinkedMovie.TMDbData?.ReleaseDate ?? DateTime.MinValue);
					break;

				case SortingType.Rating:
					if (Ascending)
						FLP_Tiles.OrderBy(x => (x as MovieTile).LinkedMovie.TMDbData.VoteAverage);
					else
						FLP_Tiles.OrderByDescending(x => (x as MovieTile).LinkedMovie.TMDbData.VoteAverage);
					break;

				default:
					break;
			}
		}

		private void ShowForm_Load(object sender, EventArgs e)
		{
		}

		private void TB_Search_TextChanged(object sender, EventArgs e)
		{
			ML_Clear.Visible = TB_Search.Text != string.Empty;

			FLP_Tiles.SuspendLayout();
			foreach (MovieTile tile in FLP_Tiles.Controls)
				tile.Visible = SearchMatch(tile.LinkedMovie, TB_Search.Text);
			FLP_Tiles.ResumeLayout(true);
		}

		private bool SearchMatch(Movie linkedMovie, string query)
		{
			if (query == string.Empty)
				return true;

			if (Regex.IsMatch(linkedMovie.Title, Regex.Escape(query), RegexOptions.IgnoreCase))
				return true;

			if (linkedMovie.Title.AbbreviationCheck(query))
				return true;

			if (linkedMovie.TMDbData.Genres.Any(x => Regex.IsMatch(x.Name, Regex.Escape(query), RegexOptions.IgnoreCase)))
				return true;

			return false;
		}

		private void ShowForm_ResizeEnd(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Normal)
				Data.Preferences.MoviesBounds = Bounds;
			Data.Preferences.MovieMax = WindowState == FormWindowState.Maximized;
			Data.Preferences.Save();
		}

		private void TLP_TileContainer_Layout(object sender, LayoutEventArgs e)
		{
			if (FLP_Tiles.Controls.Count == 0)
			{
				L_NoShows.Visible = true;
				TLP_TileContainer.MaximumSize = TLP_TileContainer.MinimumSize = new Size(P_ContentShows.Width, 35);
			}
			else
			{
				L_NoShows.Visible = false;
				TLP_TileContainer.MaximumSize = TLP_TileContainer.MinimumSize = new Size(P_ContentShows.Width, FLP_Tiles.Height + 6);
			}
		}

		private void FLP_Tiles_ControlAdded(object sender, ControlEventArgs e)
		{
			verticalScroll.SetPerc(100);
			LastTile = (MovieTile)e.Control;
			StartLoader();
			//Data.MainForm.StartLoader();
			MoviesLoading = true;
			MovieLoaded(null);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
				Visible = !(e.Cancel = true);

			base.OnFormClosing(e);
		}

		#endregion General Methods

		#region Click Actions

		private void B_AddShow_Click(object sender, EventArgs e)
		{
			if (ConnectionHandler.State == Extensions.ConnectionState.Connected)
			{
				(new AddMediaForm(true)).ShowDialog();
			}
			else
			{
				MessagePrompt.Show("You can not add Movies without Internet connection.\n\nCheck your connectivity then try again.",
					"No Connection",
					 PromptButtons.OK,
					 PromptIcons.Hand);
			}
		}

		private void B_Folders_Click(object sender, EventArgs e)
		{
			new ShowFolderSelection().ShowDialog();
		}

		private void B_Discover_Click(object sender, EventArgs e)
		{
			if (ConnectionHandler.State == Extensions.ConnectionState.Connected)
			{
				(new DiscoverMediaForm(true)).ShowDialog();
			}
			else
			{
				MessagePrompt.Show("You can not View Popular Movies without Internet connection.\n\nCheck your connectivity then try again.",
					"No Connection",
					 PromptButtons.OK,
					 PromptIcons.Hand);
			}
		}

		private void B_Sort_Click(object sender, EventArgs e)
		{
			(new SortingPopup(true)).ShowDialog();
			SortMovies();
		}

		private void Clear_Click() => TB_Search.Text = string.Empty;

		#endregion Click Actions
	}
}