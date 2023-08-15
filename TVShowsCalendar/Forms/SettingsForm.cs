using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using TVShowsCalendar.Properties;
using TVShowsCalendar.Classes;
using TVShowsCalendar.Forms;
using SlickControls.Enums;
using SlickControls.Forms;
using SlickControls.Classes;
using System.Drawing.Drawing2D;
using SlickControls.Controls;

namespace TVShowsCalendar
{
	public partial class SettingsForm : BaseForm
	{
		public SettingsForm()
		{
			InitializeComponent();

			if (!Data.FirstTimeSetup)
			{
				if (Data.Preferences.OptionsBounds != Rectangle.Empty)
					Bounds = Data.Preferences.OptionsBounds;
				else
					StartPosition = FormStartPosition.CenterScreen;

				WindowState = Data.Preferences.OptionsMax ? FormWindowState.Maximized : FormWindowState.Normal;

				OC_Quality.SelectedOption = GeneralMethods.QualityDicId2Txt[Data.Options.PrefferedQuality];
				OC_DownloadOption.Checked = Data.Options.ShowAllDownloads;
				OC_StartMode.Checked = Data.Options.StartupMode;
				OC_DownloadBehavior.Checked = Data.Options.DownloadBehavior;
				OC_EpBehavior.Checked = Data.Options.EpisodeBehavior;
				OC_EpNotification.Checked = Data.Options.EpisodeNotification;
				OC_NotificationSound.Checked = Data.Options.NotificationSound;
				OC_UnwatchedBanner.Checked = Data.Options.ShowUnwatchedOnThumb;
				OC_FinaleWarning.Checked = Data.Options.FinaleWarning;
				OC_FullScreenPlayer.Checked = Data.Options.FullScreenPlayer;
				OC_ForwardTime.SelectedOption = $"{Data.Options.ForwardTime} seconds";
				OC_BackwardTime.SelectedOption = $"{Data.Options.BackwardTime} seconds";
				OC_MoviesRefreshDays.SelectedOption = $"{Data.Options.MoviesRefreshDays} days";
				OC_ShowsRefreshDays.SelectedOption = $"{Data.Options.ShowsRefreshDays} days";

				OC_LaunchWithWindows.Checked = File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk");

				if (!GeneralMethods.IsAdministrator)
					OC_LaunchWithWindows.Disable();
			}
			else
			{
				StartPosition = FormStartPosition.CenterScreen;
			}

			P_General.Tag = T_General.Tag = Setting.General;
			P_App.Tag = T_App.Tag = Setting.App;
			P_Download.Tag = T_Downloads.Tag = Setting.Download;
			P_Watch.Tag = T_Watch.Tag = Setting.Watch;

			verticalScroll1.LinkedControl = P_Options;

			SlickTip.SetTo(B_Done, "Applies all the chosen settings and closes this window");
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			if (Data.FirstTimeSetup)
				new Action(() => MessagePrompt.Show("Hello and Welcome to Shows Calendar!\n" +
					"Let's first get your settings set up; tweak them as you please then hit 'Apply' when you're done.\n" +
					"Note you'll be able to come back here later."
					, "First Time Setup"
					, PromptButtons.OK
					, PromptIcons.Info)).RunInBackground(100);
		}

		private void B_Apply_Click(object sender, EventArgs e)
		{
			Data.Options.PrefferedQuality = GeneralMethods.QualityDicId2Txt.Where(x => x.Value == OC_Quality.SelectedOption).FirstOrDefault().Key;
			Data.Options.ShowAllDownloads = OC_DownloadOption.Checked;
			Data.Options.StartupMode = OC_StartMode.Checked;
			Data.Options.LaunchWithWindows = OC_LaunchWithWindows.Checked;
			Data.Options.DownloadBehavior = OC_DownloadBehavior.Checked;
			Data.Options.EpisodeBehavior = OC_EpBehavior.Checked;
			Data.Options.EpisodeNotification = OC_EpNotification.Checked;
			Data.Options.NotificationSound = OC_NotificationSound.Checked;
			Data.Options.ShowUnwatchedOnThumb = OC_UnwatchedBanner.Checked;
			Data.Options.FinaleWarning = OC_FinaleWarning.Checked;
			Data.Options.FullScreenPlayer = OC_FullScreenPlayer.Checked;
			Data.Options.ForwardTime = OC_ForwardTime.SelectedOption.SmartParse();
			Data.Options.BackwardTime = OC_BackwardTime.SelectedOption.SmartParse();
			Data.Options.MoviesRefreshDays = OC_MoviesRefreshDays.SelectedOption.SmartParse();
			Data.Options.ShowsRefreshDays = OC_ShowsRefreshDays.SelectedOption.SmartParse();

			Data.Options.Save();

			if (GeneralMethods.IsAdministrator)
			{
				if (OC_LaunchWithWindows.Checked)
					GeneralMethods.CreateShortcut(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk", Path.GetFullPath(".") + @"\TVShowsCalendar.exe");
				else if (File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk"))
					File.Delete(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk");
			}

			if (Data.FirstTimeSetup)
			{
				Data.MoviesForm = new MoviesForm();
				Data.FirstTimeSetup = false;
				Data.ShowForm.ShowUp();
				Data.MainForm.ShowUp();
			}
			else
			{
				foreach (var item in ShowManager.Shows)
				{
					item.LastEpisode = item.LastEpisode;
				}
			}

			Hide();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Enter)
			{
				B_Apply_Click(this, new EventArgs());
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void SettingsForm_ResizeEnd(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Normal)
				Data.Preferences.OptionsBounds = Bounds;
			Data.Preferences.OptionsMax = WindowState == FormWindowState.Maximized;
			Data.Preferences.Save();
			P_Spacer.Refresh();
		}

		private void B_Terminate_Click(object sender, EventArgs e) => Data.MainForm.Close();

		private void P_Spacer_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(FormDesign.Design.BackColor);
			e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.ActiveColor), 0, 0, 1, P_Spacer.Height - 150);

			e.Graphics.FillRectangle(new LinearGradientBrush(
				new RectangleF(0, P_Spacer.Height - 175, 1, 150),
				FormDesign.Design.ActiveColor,
				FormDesign.Design.BackColor,
				90),
				new RectangleF(0, P_Spacer.Height - 175, 1, 150));
		}

		private void SettingsForm_Resize(object sender, EventArgs e)
		{
			P_Spacer.Refresh();
		}

		private void P_OptionContainer_Resize(object sender, EventArgs e)
		{
			P_Options.MaximumSize = new Size(P_OptionContainer.Width, 9999);
			P_Options.MinimumSize = new Size(P_OptionContainer.Width, 0);
		}

		private void Tile_Click(object sender, EventArgs e)
		{
			P_OptionContainer.SuspendDrawing();
			foreach (var item in P_Options.Controls.ThatAre<Panel>())
				item.Visible = (sender as Control).Tag == item.Tag;
			foreach (var item in P_Tiles.Controls.ThatAre<SlickTile>())
				item.Selected = item == sender;
			P_OptionContainer.ResumeDrawing();
		}
	}
}
