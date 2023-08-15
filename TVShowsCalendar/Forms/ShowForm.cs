using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Extensions;
using SlickControls.Enums;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.Forms;
using TVShowsCalendar.HandlerClasses;

namespace TVShowsCalendar
{
	public enum SortingType { Name, LastAirDate, FirstAirDate, Rating }

	public partial class ShowForm : BaseForm
	{
		public bool ShowsLoading { get; private set; }
		private SortingType SortType => Data.Preferences.ShowSortType;
		private bool Ascending => Data.Preferences.ShowSortAscending;
		private ShowTile LastTile;

		public ShowForm()
		{
			InitializeComponent();
			ML_Clear.Click += (s, e) => Clear_Click();

			ShowManager.LoadAllShows();
			ShowManager.ShowLoaded += ShowLoaded;

			FLP_Tiles.SuspendLayout();
			FLP_Tiles.ResumeLayout(true);
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

			if (Data.Preferences.ShowsBounds != Rectangle.Empty)
				Bounds = Data.Preferences.ShowsBounds;
			else
				StartPosition = FormStartPosition.CenterScreen;
			WindowState = Data.Preferences.ShowMax ? FormWindowState.Maximized : FormWindowState.Normal;

			SlickTip.SetTo(ML_Clear, "Clear Search");
			SlickTip.SetTo(B_Sort, "Sort Shows");
			SlickTip.SetTo(B_Discover, "Discover Trending Shows");
			SlickTip.SetTo(B_Folders, "Local Library Folders");

			if (ShowManager.Shows.Any(x => !x.Loaded))
			{
				StartLoader();
				//Data.MainForm.StartLoader();
				ShowsLoading = true;
			}
		}

		#region General Methods

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			L_NoShows.ForeColor = design.LabelColor;
		}

		private OneWayTask NotifyIdentifier = new OneWayTask();

		private void ShowLoaded(Show show)
		{
			var loaded = ShowManager.Shows.All(x => x.Loaded);

			if (loaded)
			{
				//if (!Data.MoviesForm?.MoviesLoading ?? false)
				//	Data.MainForm.StopLoader();
				StopLoader();
				LocalShowHandler.LoadFiles();
				ShowsLoading = false;
				NotifyIdentifier.Run(() =>
				{
					Thread.Sleep(12500);
					Data.MainForm.TryInvoke(ShowNewEpisodes);
				});
			}

			this.TryInvoke(() =>
			{
				SortShows();
				if (LastTile != null && LastTile.LinkedShow == show)
				{
					verticalScroll.SetPerc(100D * LastTile.Location.Y / (FLP_Tiles.Height - P_ContentShows.Height));
					LastTile = null;
				}
			});
		}

		private void ShowNewEpisodes()
		{
			ISave.Load(out Dictionary<int, DateTime> remindLog, "RemindLog.tf");

			var eps = ShowManager.Shows.Convert(x => x.LastEpisode)
				.Where(x => x != null
					&& !x.Empty
					&& x.TMDbData.AirDate > DateTime.Today.AddDays(-7)
					&& !remindLog.ContainsKey(x.TMDbData.Id)
					&& Data.Options.EpisodeNotification
					&& x.VidFile == null);

			if (eps.Count() > 0)
				(new NotifyForm(eps)).ShowUp();

			foreach (var item in eps)
				remindLog.Add(item.TMDbData.Id.ToString(), DateTime.Now);

			var oldEps = remindLog.Where(x => x.Key != ""
									 && x.Value < DateTime.Now.AddDays(-60)).ToArray();

			foreach (var item in oldEps)
				remindLog.Remove(item.Key);

			ISave.Save(remindLog, "RemindLog.tf");
		}

		private void SortShows()
		{
			if (FLP_Tiles.InvokeRequired)
			{
				FLP_Tiles.Invoke(new Action(SortShows));
				return;
			}

			switch (SortType)
			{
				case SortingType.Name:
					if (Ascending)
						FLP_Tiles.OrderBy(x => (x as ShowTile).LinkedShow.Name);
					else
						FLP_Tiles.OrderByDescending(x => (x as ShowTile).LinkedShow.Name);
					break;

				case SortingType.LastAirDate:
					if (Ascending)
						FLP_Tiles.OrderBy(x => (x as ShowTile).LinkedShow.LastEpisode?.TMDbData?.AirDate ?? DateTime.MaxValue);
					else
						FLP_Tiles.OrderByDescending(x => (x as ShowTile).LinkedShow.LastEpisode?.TMDbData?.AirDate ?? DateTime.MinValue);
					break;

				case SortingType.FirstAirDate:
					if (Ascending)
						FLP_Tiles.OrderBy(x => (x as ShowTile).LinkedShow.TMDbData.FirstAirDate ?? DateTime.MaxValue);
					else
						FLP_Tiles.OrderByDescending(x => (x as ShowTile).LinkedShow.TMDbData.FirstAirDate ?? DateTime.MinValue);
					break;

				case SortingType.Rating:
					if (Ascending)
						FLP_Tiles.OrderBy(x => (x as ShowTile).LinkedShow.TMDbData.VoteAverage);
					else
						FLP_Tiles.OrderByDescending(x => (x as ShowTile).LinkedShow.TMDbData.VoteAverage);
					break;

				default:
					break;
			}
		}

		private void ShowForm_Load(object sender, EventArgs e)
		{
			if (Data.FirstTimeSetup)
				new Action(() => Invoke(new Action(() =>
				{
					if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TV Show Calendar", "ShowsData")))
					{
						if (MessagePrompt.Show("It seems you've used TV Shows Calendar before, would you like to import your previous Shows?", "First Time Setup",
							PromptButtons.YesNo,
							PromptIcons.Question) == DialogResult.Yes)
						{
							ImportShows();
						}
					}
					else
						MessagePrompt.Show("Great! Now that this is done, it's time for the juicy part!\n" +
							"To add Shows to your library, just click on the 'Add Show' button\n" +
							"Note that you can also add Movies in the Movies Form", "First Time Setup",
							PromptButtons.OK,
							PromptIcons.Info);
					Data.MainForm.Show();
				}))).RunInBackground(100);
		}

		private void ImportShows()
		{
			foreach (var item in new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TV Show Calendar", "ShowsData")).EnumerateFiles("*.show"))
			{
				new Action(async () =>
				{
					var data = File.ReadAllLines(item.FullName);
					var results = await Data.TMDbHandler.SearchTvShow(data[2]);
					if (results.Count > 0)
					{
						var show = new Show(results[0])
						{
							ZooqleURL = data[1]
						};
						Invoke(new Action(() => FLP_Tiles.Controls.Add(new ShowTile(show))));
					}
				}).RunInBackground();
			}
		}

		private void TB_Search_TextChanged(object sender, EventArgs e)
		{
			ML_Clear.Visible = TB_Search.Text != string.Empty;

			FLP_Tiles.SuspendLayout();
			foreach (ShowTile tile in FLP_Tiles.Controls)
				tile.Visible = SearchMatch(tile.LinkedShow, TB_Search.Text);
			FLP_Tiles.ResumeLayout(true);
		}

		private bool SearchMatch(Show linkedShow, string query)
		{
			if (query == string.Empty)
				return true;

			if (Regex.IsMatch(linkedShow.Name, Regex.Escape(query), RegexOptions.IgnoreCase))
				return true;

			if (linkedShow.Name.AbbreviationCheck(query))
				return true;

			if (linkedShow.TMDbData.Genres.Any(x => Regex.IsMatch(x.Name, Regex.Escape(query), RegexOptions.IgnoreCase)))
				return true;

			if (linkedShow.TMDbData.FirstAirDate != null && Regex.IsMatch(linkedShow.TMDbData.FirstAirDate?.Year.ToString(), Regex.Escape(query), RegexOptions.IgnoreCase))
				return true;

			return false;
		}

		private void ShowForm_ResizeEnd(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Normal)
				Data.Preferences.ShowsBounds = Bounds;
			Data.Preferences.ShowMax = WindowState == FormWindowState.Maximized;
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
			LastTile = (ShowTile)e.Control;
			StartLoader();
			//Data.MainForm.StartLoader();
			ShowsLoading = true;
			ShowLoaded(null);
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
				(new AddMediaForm()).ShowDialog();
			}
			else
			{
				MessagePrompt.Show("You can not add Shows without Internet connection.\n\nCheck your connectivity then try again.",
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
				(new DiscoverMediaForm()).ShowDialog();
			}
			else
			{
				MessagePrompt.Show("You can not View Popular Shows without Internet connection.\n\nCheck your connectivity then try again.",
					"No Connection",
					 PromptButtons.OK,
					 PromptIcons.Hand);
			}
		}

		private void B_Sort_Click(object sender, EventArgs e)
		{
			(new SortingPopup()).ShowDialog();
			SortShows();
		}

		private void Clear_Click() => TB_Search.Text = string.Empty;

		#endregion Click Actions
	}
}