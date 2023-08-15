using Extensions;

using SlickControls;

using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PC_Settings : PanelContent
	{
		private bool valuesChanged = false;

		public PC_Settings()
		{
			InitializeComponent();

			FirstFocusedControl = B_Done;

			PC_ShowsOrder.OptionList = PC_MoviesOrder.OptionList = typeof(MediaSortOptions).GetEnumDescs();

			if (!Data.FirstTimeSetup)
			{
				OC_Quality.SelectedOption = GeneralMethods.QualityDicId2Txt[Data.Options.PrefferedQuality.If(0, 7)];
				OC_DownloadOption.Checked = Data.Options.ShowAllDownloads;
				OC_StartMode.Checked = Data.Options.StartupMode;
				OC_DownloadBehavior.Checked = Data.Options.DownloadBehavior;
				OC_EpNotification.Checked = Data.Options.EpisodeNotification;
				OC_NotificationSound.Checked = Data.Options.NotificationSound;
				OC_FinaleWarning.Checked = Data.Options.FinaleWarning;
				OC_FullScreenPlayer.Checked = Data.Options.FullScreenPlayer;
				OC_EpBehavior.Checked = Data.Options.OpenAllPagesForEp;
				OC_AutoEpSwitch.Checked = Data.Options.AutomaticEpisodeSwitching;
				OC_AutoPauseScroll.Checked = Data.Options.AutoPauseOnInfo;
				OC_IgnoreSpecialsSeason.Checked = Data.Options.IgnoreSpecialsSeason;
				OC_SpoilerThumbnail.Checked = Data.Options.SpoilerThumbnail;
				OC_PnP.Checked = Data.Options.KeepPlayerOpen;
				OC_StickyMiniPlayer.Checked = Data.Options.StickyMiniPlayer;
				OC_NoAnimations.Checked = Data.Options.NoAnimations;
				OC_TopMostPlayer.Checked = Data.Options.TopMostPlayer;
				OC_AlwaysShowBanners.Checked = Data.Options.AlwaysShowBanners;
				OC_PauseWhenOutOfFocusFullScreen.Checked = Data.Options.PauseWhenOutOfFocusFullScreen;
				OC_ForwardTime.SelectedOption = $"{Data.Options.ForwardTime} seconds";
				OC_BackwardTime.SelectedOption = $"{Data.Options.BackwardTime} seconds";
				PC_ShowsOrder.SelectedOption = Data.Options.ShowSorting.GetDescription();
				PC_MoviesOrder.SelectedOption = Data.Options.MovieSorting.GetDescription();

				OC_LaunchWithWindows.Checked = File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk");
			}

			TB_SavePath.Text = ISave.CustomSaveDirectory;

			valuesChanged = Data.FirstTimeSetup;

			SlickTip.SetTo(B_Done, "Applies all the chosen settings and closes this window");

			if (Data.FirstTimeSetup)
				B_Done.Anchor = AnchorStyles.Left | AnchorStyles.Top;

			slickTabControl1.ScrollBar.VisibleChanged += verticalScroll1_VisibleChanged;
		}

		private void B_Apply_Click(object sender, EventArgs e)
		{
			Data.Options.PrefferedQuality = GeneralMethods.QualityDicId2Txt.Where(x => x.Value == OC_Quality.SelectedOption).FirstOrDefault().Key.If(0, 7);
			Data.Options.ShowAllDownloads = OC_DownloadOption.Checked;
			Data.Options.StartupMode = OC_StartMode.Checked;
			Data.Options.LaunchWithWindows = OC_LaunchWithWindows.Checked;
			Data.Options.DownloadBehavior = OC_DownloadBehavior.Checked;
			Data.Options.EpisodeNotification = OC_EpNotification.Checked;
			Data.Options.NotificationSound = Notification.PlaySounds = OC_NotificationSound.Checked;
			Data.Options.FinaleWarning = OC_FinaleWarning.Checked;
			Data.Options.OpenAllPagesForEp = OC_EpBehavior.Checked;
			Data.Options.FullScreenPlayer = OC_FullScreenPlayer.Checked;
			Data.Options.AutomaticEpisodeSwitching = OC_AutoEpSwitch.Checked;
			Data.Options.AutoPauseOnInfo = OC_AutoPauseScroll.Checked;
			Data.Options.SpoilerThumbnail = OC_SpoilerThumbnail.Checked;
			Data.Options.IgnoreSpecialsSeason = OC_IgnoreSpecialsSeason.Checked;
			Data.Options.KeepPlayerOpen = OC_PnP.Checked;
			Data.Options.StickyMiniPlayer = OC_StickyMiniPlayer.Checked;
			Data.Options.NoAnimations = AnimationHandler.NoAnimations = OC_NoAnimations.Checked;
			Data.Options.TopMostPlayer = OC_TopMostPlayer.Checked;
			Data.Options.PauseWhenOutOfFocusFullScreen = OC_PauseWhenOutOfFocusFullScreen.Checked;
			SlickAdvancedImageControl.AlwaysShowBanners = Data.Options.AlwaysShowBanners = OC_AlwaysShowBanners.Checked;
			Data.Options.ForwardTime = OC_ForwardTime.SelectedOption.SmartParse();
			Data.Options.BackwardTime = OC_BackwardTime.SelectedOption.SmartParse();
			Data.Options.ShowSorting = (MediaSortOptions)typeof(MediaSortOptions).GetEnumValueFromDescs(PC_ShowsOrder.SelectedOption);
			Data.Options.MovieSorting = (MediaSortOptions)typeof(MediaSortOptions).GetEnumValueFromDescs(PC_MoviesOrder.SelectedOption);

			if ((string.IsNullOrWhiteSpace(TB_SavePath.Text) || TB_SavePath.ValidInput) && (ISave.CustomSaveDirectory ?? string.Empty) != TB_SavePath.Text)
			{
				if (ShowPrompt("Would you like to transfer your current settings to the new save location?", PromptButtons.YesNo, PromptIcons.Question) == DialogResult.Yes)
				{
					var prompt = ProccessPrompt.Create("Transferring your settings..");

					new BackgroundAction(() =>
					{
						var old = ISave.DocsFolder;
						ISave.CustomSaveDirectory = TB_SavePath.Text;

						new DirectoryInfo(old).CopyAll(new DirectoryInfo(ISave.DocsFolder));

						prompt.Close();
					}).Run();

					prompt.ShowDialog(Form);
				}

				ISave.CustomSaveDirectory = null;
				ISave.Save(TB_SavePath.Text, "CustomSaveDirectory.tf");
				ISave.CustomSaveDirectory = TB_SavePath.Text;
			}

			Data.Options.Save();

			if (GeneralMethods.IsAdministrator)
			{
				if (OC_LaunchWithWindows.Checked)
					GeneralMethods.CreateShortcut(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk", Application.ExecutablePath, "/startup");
				else if (File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk"))
					File.Delete(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk");
			}
			else if (OC_LaunchWithWindows.Checked != File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk"))
			{
				try
				{
					Process.Start(new ProcessStartInfo(AppDomain.CurrentDomain.FriendlyName)
					{
						UseShellExecute = true,
						Verb = "runas",
						Arguments = $"/shortcut {OC_LaunchWithWindows.Checked}"
					});
				}
				catch { }
			}

			valuesChanged = false;

			if (Data.FirstTimeSetup)
			{
				Data.Mainform.Setup(1);
			}
		}

		protected override void UIChanged()
		{
			base.UIChanged();

			slickTabControl1.Padding = new Padding(base_Text.Width + 32, base_Text.Height - UI.Font(8.25F).Height + 5, 0, 0);
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			B_Done.BackColor = design.BackColor;
			panel1.BackColor = design.AccentColor;
			tableLayoutPanel1.BackColor = design.AccentBackColor;
		}

		public override bool CanExit(bool toBeDisposed)
		{
			if (valuesChanged && toBeDisposed)
			{
				if (DialogResult.Yes == ShowPrompt("Do you want to save your changes before leaving?", "Save Changes?", PromptButtons.YesNo, PromptIcons.Question))
					B_Apply_Click(null, null);
			}

			return true;
		}

		private void OC_ValueChanged(object sender, EventArgs e) => valuesChanged = true;

		private void verticalScroll1_VisibleChanged(object sender, EventArgs e)
		{
			panel1.Visible = slickTabControl1.ScrollBar.Visible;
		}

		private void PC_Settings_Resize(object sender, EventArgs e)
		{
			TB_SavePath.MaximumSize = new System.Drawing.Size(slickTabControl1.Width - 30, 1000);
			TB_SavePath.MinimumSize = new System.Drawing.Size(slickTabControl1.Width - 30, 0);
		}
	}
}