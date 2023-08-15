using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.Properties;

namespace TVShowsCalendar
{
	public partial class MainForm : BaseForm
	{
		public MainForm()
		{
			InitializeComponent();

			//Data.MainForm = this;
			Data.TaskIcon = notifyIcon;
			L_Version.Text = $"v. {ProductVersion}";
			B_Options.Click += (s, e) => B_Options_Click();
			B_Shows.Click += (s, e) => B_Shows_Click();
			B_Movies.Click += (s, e) => B_Movies_Click();
			B_Watch.Click += (s, e) => B_Watch_Click();
			TabScroll.LinkedControl = P_Tabs;
			SetTooltips();

			FormDesign.Initialize(this);
			ShowManager.ShowLoaded += ShowLoaded;
			ShowManager.ShowRemoved += ShowRemoved;
			LocalShowHandler.EpisodeWatchChanged += ReloadWatchEps;
			LocalShowHandler.FolderChanged += ReloadWatchEps;

			if (Data.FirstTimeSetup)
			{
				RunFirstTimeSetup();
				return;
			}

			Data.SettingsForm = new SettingsForm();
			Data.ShowForm = new ShowForm();
			Data.MoviesForm = new MoviesForm();

			Visible = Data.Options.StartupMode;

			if (Data.Preferences.MainBounds != Rectangle.Empty)
				Bounds = Data.Preferences.MainBounds;

			WindowState = Data.Preferences.MainMax ? FormWindowState.Maximized : FormWindowState.Normal;
		}

		private void ReloadWatchEps(Show show)
		{
			LocalShowHandler.LoadLibrary(out var onDeck, out var continueWatching, out var startWatching, out var lastWatched, show);

			while (!T_Play.TryInvoke(() =>
				{
					if (show == null)
						T_Play.ContainedControls.Clear(true);
					else
						T_Play.ContainedControls.Clear(true, x => (x as UserControls.WatchControl).Episode.Show == show);

					foreach (var item in onDeck)
						T_Play.ContainedControls.Add(new UserControls.WatchControl(item));

					T_Play.ContainedControls.OrderByDescending(x => (x as UserControls.WatchControl).Episode.GetDateOrder());
				}))
			{ System.Threading.Thread.Sleep(500); ReloadWatchEps(show); }
		}

		private void RunFirstTimeSetup()
		{
			if (!GeneralMethods.IsAdministrator)
			{
				noadmin: const int ERROR_CANCELLED = 1223; //The operation was canceled by the user.
				Directory.Delete(ISave.DocsFolder, true);

				ProcessStartInfo info = new ProcessStartInfo(Path.Combine(Path.GetFullPath("."), "TVShowsCalendar.exe"))
				{
					UseShellExecute = true,
					Verb = "runas"
				};
				try
				{
					Process.Start(info);
				}
				catch (Win32Exception ex)
				{
					if (ex.NativeErrorCode == ERROR_CANCELLED)
						if (DialogResult.Retry == MessagePrompt.Show("The App must be ran in Administrator Mode for the first time setup, please run the app again as Administrator\n\nWould you like to run the App again?", "Privileges Missing", PromptButtons.RetryCancel, PromptIcons.Error))
							goto noadmin;
				}

				Close();
				return;
			}

			if (!File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Shows Calendar.lnk"))
				GeneralMethods.CreateShortcut(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Shows Calendar.lnk", Path.GetFullPath(".") + @"\TVShowsCalendar.exe");

			SetStartLocation();

			Data.ShowForm = new ShowForm();
			(Data.SettingsForm = new SettingsForm()).ShowUp();
		}

		public void B_Watch_Click()
		{
			Cursor.Current = Cursors.WaitCursor;
			Data.ShowLibrary = Data.ShowLibrary.ShowUp(true);
			Cursor.Current = Cursors.Default;
		}

		public void B_Shows_Click()
		{
			Cursor.Current = Cursors.WaitCursor;
			Data.ShowForm.ShowUp(true);
			Cursor.Current = Cursors.Default;
		}

		public void B_Movies_Click()
		{
			Cursor.Current = Cursors.WaitCursor;
			Data.MoviesForm.ShowUp(true);
			Cursor.Current = Cursors.Default;
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			P_Space_0.BackColor = P_Space_1.BackColor = P_Space_2.BackColor = P_Space_3.BackColor = design.LightColor;
			T_Today.L_Title.ForeColor = design.ActiveColor;
			T_Upcoming.ForeColor = design.LabelColor;
			PB_TMDb.Image = PB_TMDb.Image.Color(design.Type != FormDesignType.Light ? design.LightColor : design.LabelColor);
			L_Version.ForeColor = L_Icons8.ForeColor = design.LabelColor;
		}

		private void SetTooltips()
		{
			SlickTip.SetTo(PB_TMDb, "Visit The Movie DB");
			SlickTip.SetTo(L_Icons8, "Visit icons8.com");
			SlickTip.SetTo(L_Version, "View change-log");
		}

		private void SetStartLocation()
		{
			var w = SystemInformation.VirtualScreen.Width;
			var h = SystemInformation.VirtualScreen.Height;
			switch (GeneralMethods.GetTaskbarLocation())
			{
				case TaskbarLocation.Top:
					Location = new Point(50, h - Height - 50); break;
				case TaskbarLocation.Left:
					Location = new Point(w - Width - 50, 50); break;
				case TaskbarLocation.Bottom:
					Location = new Point(w - Width - 50, 50); break;
				case TaskbarLocation.Right:
					Location = new Point(50, 50); break;
				default:
					Location = new Point((w - Width) / 2, (h - Height) / 2); break;
			}
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (!Data.FirstTimeSetup && e.CloseReason == CloseReason.UserClosing && e.CloseReason != CloseReason.ApplicationExitCall)
				e.Cancel = MessagePrompt.Show("Are you sure you want to close Shows Calendar?", "Confirm Action", PromptButtons.YesNo, PromptIcons.Question) == DialogResult.No;

			if (!e.Cancel)
				notifyIcon.Visible = false;

			base.OnFormClosing(e);
		}

		public void Form1_Resize(object sender, EventArgs e)
		{
			var show = Width > 475;

			if (B_Options.HideText == show)
				B_Options.HideText = B_Shows.HideText = B_Movies.HideText = B_Watch.HideText = !show;
		}

		private void B_Options_Click()
		{
			Cursor.Current = Cursors.WaitCursor;
			Data.SettingsForm.ShowUp(true);
			Cursor.Current = Cursors.Default;
		}

		private void NotifyIcon_Click(object sender, EventArgs e)
		{
			if ((e as MouseEventArgs).Button == MouseButtons.Right)
			{
				new FlatToolStrip(new List<FlatStripItem>()
				{
					new FlatStripItem("Open", this.ShowUp, image: Resources.TVCalendar),
					new FlatStripItem("Shows", () => Data.ShowForm = Data.ShowForm.ShowUp(true), image: Resources.Icon_VTV),
					new FlatStripItem("Movies", () => Data.MoviesForm = Data.MoviesForm.ShowUp(true), image: Resources.Icon_Movie),
					new FlatStripItem("Library", () => Data.ShowLibrary = Data.ShowLibrary.ShowUp(true), image: Resources.Icon_Play),
					new FlatStripItem("Settings", () => Data.SettingsForm = Data.SettingsForm.ShowUp(true), image: Resources.Icon_Settings),
					new FlatStripItem("Exit App", Close, image: Resources.Icon_Cancel)
				}).ShowUp();
			}
			else
				this.ShowUp();
		}

		private void L_Version_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "TVShowsCalendar.ChangeLog.txt";
			var result = new string[0];

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
				result = reader.ReadToEnd().Split('\r', '\n');

			new ChangeLogForm(result, ProductVersion) { Icon = Icon }.ShowUp();
			Cursor.Current = Cursors.Default;
		}

		private void Form1_ResizeEnd(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Normal)
				Data.Preferences.MainBounds = Bounds;
			Data.Preferences.MainMax = WindowState == FormWindowState.Maximized;
			Data.Preferences.Save();
		}

		private void ShowLoaded(Show show)
		{
			this.TryInvoke(() =>
			{
				T_Today.SuspendDrawing();
				T_Missed.SuspendDrawing();
				T_Upcoming.SuspendDrawing();

				var tiles = GetShowTiles(show);

				if (show.LastEpisode != null && !show.LastEpisode.Empty)
				{
					var tab = show.LastEpisode.AssessTab();
					if (tab != null)
					{
						var t = tiles.FirstThat(x => x.Episode.SN == show.LastEpisode.SN && x.Episode.EN == show.LastEpisode.EN);

						if (t != null)
						{
							//t.SetData(show.LastEpisode);
							if (!tab.ContainedControls.Contains(t))
							{
								t.Parent.Controls.Remove(t);
								tab.AddControls(t);
							}
							 (t.Parent as Panel).OrderByDescending(x => (x as EpisodeTile).Episode.TMDbData.AirDate, false);
							tiles.Remove(t);
						}
						else
						{
							//var tile = new EpisodeTile(show.LastEpisode, new Size(300, 65));
							//show.LastEpisode.Tiles.Add(tile);
							//tab.AddControls(tile);
							//(tile.Parent as Panel).OrderByDescending(x => (x as EpisodeTile).Episode.TMDbData.AirDate, false);
						}
					}
				}

				if (show.NextEpisode != null && !show.NextEpisode.Empty)
				{
					var tab = show.NextEpisode.AssessTab();
					if (tab != null)
					{
						var t = tiles.FirstThat(x => x.Episode.SN == show.NextEpisode.SN && x.Episode.EN == show.NextEpisode.EN);

						if (t != null)
						{
							//t.SetData(show.NextEpisode);
							if (!tab.ContainedControls.Contains(t))
							{
								t.Parent.Controls.Remove(t);
								tab.AddControls(t);
							}
							 (t.Parent as Panel).OrderBy(x => (x as EpisodeTile).Episode.TMDbData.AirDate, false);
							tiles.Remove(t);
						}
						else
						{
							//var tile = new EpisodeTile(show.NextEpisode, new Size(300, 65));
							//show.NextEpisode.Tiles.Add(tile);
							//tab.AddControls(tile);
							//(tile.Parent as Panel).OrderBy(x => (x as EpisodeTile).Episode.TMDbData.AirDate, false);
						}
					}
				}

				tiles.ForEach(x =>
				{
					if (x.FindForm() == Data.MainForm)
					{
						if (x.InvokeRequired)
							x.Invoke(new Action(x.Dispose));
						else
							x.Dispose();
					}
				});

				T_Today.ResumeDrawing();
				T_Missed.ResumeDrawing();
				T_Upcoming.ResumeDrawing();
			});
		}

		private List<EpisodeTile> GetShowTiles(Show show)
		{
			var list = new List<EpisodeTile>();

			list.AddRange(T_Today.ContainedControls.Where(x => (x is EpisodeTile) && (x as EpisodeTile).Episode.Show.TMDbData.Id == show.TMDbData.Id).Convert(x => x as EpisodeTile));
			list.AddRange(T_Missed.ContainedControls.Where(x => (x is EpisodeTile) && (x as EpisodeTile).Episode.Show.TMDbData.Id == show.TMDbData.Id).Convert(x => x as EpisodeTile));
			list.AddRange(T_Upcoming.ContainedControls.Where(x => (x is EpisodeTile) && (x as EpisodeTile).Episode.Show.TMDbData.Id == show.TMDbData.Id).Convert(x => x as EpisodeTile));

			return list;
		}

		private void ShowRemoved(Show show)
		{
			if (InvokeRequired)
				Invoke(new Action(() => ShowRemoved(show)));
			else
			{
				foreach (Control item in T_Today.ContainedControls)
				{
					if (item is EpisodeTile)
					{
						var tile = item as EpisodeTile;
						if (tile.Episode.Show == show)
							tile.Dispose();
					}
				}

				foreach (Control item in T_Missed.ContainedControls)
				{
					if (item is EpisodeTile)
					{
						var tile = item as EpisodeTile;
						if (tile.Episode.Show == show)
							tile.Dispose();
					}
				}

				foreach (Control item in T_Upcoming.ContainedControls)
				{
					if (item is EpisodeTile)
					{
						var tile = item as EpisodeTile;
						if (tile.Episode.Show == show)
							tile.Dispose();
					}
				}
			}
		}

		private void Icons8_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try { Process.Start("https://icons8.com"); }
			catch (Exception) { Cursor.Current = Cursors.Default; MessagePrompt.Show("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
			Cursor.Current = Cursors.Default;
		}

		private void Icons8_MouseEnter(object sender, EventArgs e) => L_Icons8.Font = new Font(L_Icons8.Font, FontStyle.Italic | FontStyle.Underline);

		private void Icons8_MouseLeave(object sender, EventArgs e) => L_Icons8.Font = new Font(L_Icons8.Font, FontStyle.Italic | FontStyle.Regular);

		private void PB_Zooqle_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try { Process.Start("https://themoviedb.org/"); }
			catch (Exception) { Cursor.Current = Cursors.Default; MessagePrompt.Show("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
			Cursor.Current = Cursors.Default;
		}

		private void P_Tabs_Resize(object sender, EventArgs e)
		{
			try
			{
				P_Tabs.MaximumSize = new Size(P_FormContent.Width, 9999);
				P_Tabs.MinimumSize = new Size(P_FormContent.Width, 0);
			}
			catch { }
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			if (!Data.Options.StartupMode || Data.FirstTimeSetup)
				new Action(() => Invoke(new Action(Hide))).RunInBackground(5);
		}

		private void MainForm_WindowStateChanged(object sender, EventArgs e) => P_Tabs_Resize(sender, e);
	}
}