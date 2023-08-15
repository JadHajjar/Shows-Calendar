namespace ShowsCalendar
{
	partial class PC_Settings
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PC_Settings));
			this.P_Watch = new System.Windows.Forms.FlowLayoutPanel();
			this.OC_BackwardTime = new ShowsCalendar.OptionControl();
			this.OC_ForwardTime = new ShowsCalendar.OptionControl();
			this.OC_AutoEpSwitch = new ShowsCalendar.OptionControl();
			this.OC_FullScreenPlayer = new ShowsCalendar.OptionControl();
			this.OC_PnP = new ShowsCalendar.OptionControl();
			this.OC_StickyMiniPlayer = new ShowsCalendar.OptionControl();
			this.OC_AutoPauseScroll = new ShowsCalendar.OptionControl();
			this.OC_TopMostPlayer = new ShowsCalendar.OptionControl();
			this.P_Download = new System.Windows.Forms.FlowLayoutPanel();
			this.OC_Quality = new ShowsCalendar.OptionControl();
			this.OC_DownloadOption = new ShowsCalendar.OptionControl();
			this.OC_DownloadBehavior = new ShowsCalendar.OptionControl();
			this.P_App = new System.Windows.Forms.FlowLayoutPanel();
			this.TB_SavePath = new SlickControls.SlickPathTextBox();
			this.OC_NoAnimations = new ShowsCalendar.OptionControl();
			this.OC_EpNotification = new ShowsCalendar.OptionControl();
			this.OC_NotificationSound = new ShowsCalendar.OptionControl();
			this.OC_StartMode = new ShowsCalendar.OptionControl();
			this.OC_LaunchWithWindows = new ShowsCalendar.OptionControl();
			this.P_General = new System.Windows.Forms.FlowLayoutPanel();
			this.OC_IgnoreSpecialsSeason = new ShowsCalendar.OptionControl();
			this.OC_SpoilerThumbnail = new ShowsCalendar.OptionControl();
			this.PC_ShowsOrder = new ShowsCalendar.OptionControl();
			this.PC_MoviesOrder = new ShowsCalendar.OptionControl();
			this.OC_AlwaysShowBanners = new ShowsCalendar.OptionControl();
			this.OC_FinaleWarning = new ShowsCalendar.OptionControl();
			this.OC_EpBehavior = new ShowsCalendar.OptionControl();
			this.T_App = new SlickControls.SlickTabControl.Tab();
			this.T_Watch = new SlickControls.SlickTabControl.Tab();
			this.T_Downloads = new SlickControls.SlickTabControl.Tab();
			this.T_General = new SlickControls.SlickTabControl.Tab();
			this.B_Done = new SlickControls.SlickButton();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.slickTabControl1 = new SlickControls.SlickTabControl();
			this.OC_PauseWhenOutOfFocusFullScreen = new ShowsCalendar.OptionControl();
			this.P_Watch.SuspendLayout();
			this.P_Download.SuspendLayout();
			this.P_App.SuspendLayout();
			this.P_General.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(65, 26);
			this.base_Text.Text = "Settings";
			// 
			// P_Watch
			// 
			this.P_Watch.AutoSize = true;
			this.P_Watch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Watch.Controls.Add(this.OC_BackwardTime);
			this.P_Watch.Controls.Add(this.OC_ForwardTime);
			this.P_Watch.Controls.Add(this.OC_AutoEpSwitch);
			this.P_Watch.Controls.Add(this.OC_FullScreenPlayer);
			this.P_Watch.Controls.Add(this.OC_PnP);
			this.P_Watch.Controls.Add(this.OC_StickyMiniPlayer);
			this.P_Watch.Controls.Add(this.OC_AutoPauseScroll);
			this.P_Watch.Controls.Add(this.OC_TopMostPlayer);
			this.P_Watch.Controls.Add(this.OC_PauseWhenOutOfFocusFullScreen);
			this.P_Watch.Location = new System.Drawing.Point(0, 0);
			this.P_Watch.MaximumSize = new System.Drawing.Size(1204, 2147483647);
			this.P_Watch.MinimumSize = new System.Drawing.Size(1204, 0);
			this.P_Watch.Name = "P_Watch";
			this.P_Watch.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.P_Watch.Size = new System.Drawing.Size(1204, 355);
			this.P_Watch.TabIndex = 37;
			// 
			// OC_BackwardTime
			// 
			this.OC_BackwardTime.Checked = true;
			this.OC_BackwardTime.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_BackwardTime.Location = new System.Drawing.Point(15, 15);
			this.OC_BackwardTime.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_BackwardTime.Name = "OC_BackwardTime";
			this.OC_BackwardTime.OptionList = new string[] {
        "60 seconds",
        "45 seconds",
        "30 seconds",
        "15 seconds",
        "10 seconds",
        "5 seconds"};
			this.OC_BackwardTime.OptionType = ShowsCalendar.OptionType.OptionList;
			this.OC_BackwardTime.SelectedOption = "5 seconds";
			this.OC_BackwardTime.Size = new System.Drawing.Size(350, 100);
			this.OC_BackwardTime.SpaceTriggersClick = true;
			this.OC_BackwardTime.TabIndex = 0;
			this.OC_BackwardTime.Text_CheckBox_Checked = "";
			this.OC_BackwardTime.Text_CheckBox_Unchecked = "";
			this.OC_BackwardTime.Text_Info = "Choose how much time to jump backward at once.";
			this.OC_BackwardTime.Text_Title = "Backwards Time";
			this.OC_BackwardTime.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_ForwardTime
			// 
			this.OC_ForwardTime.Checked = true;
			this.OC_ForwardTime.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_ForwardTime.Location = new System.Drawing.Point(380, 15);
			this.OC_ForwardTime.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_ForwardTime.Name = "OC_ForwardTime";
			this.OC_ForwardTime.OptionList = new string[] {
        "60 seconds",
        "45 seconds",
        "30 seconds",
        "15 seconds",
        "10 seconds",
        "5 seconds"};
			this.OC_ForwardTime.OptionType = ShowsCalendar.OptionType.OptionList;
			this.OC_ForwardTime.SelectedOption = "15 seconds";
			this.OC_ForwardTime.Size = new System.Drawing.Size(350, 100);
			this.OC_ForwardTime.SpaceTriggersClick = true;
			this.OC_ForwardTime.TabIndex = 1;
			this.OC_ForwardTime.Text_CheckBox_Checked = "";
			this.OC_ForwardTime.Text_CheckBox_Unchecked = "";
			this.OC_ForwardTime.Text_Info = "Choose how much time to jump forward at once.";
			this.OC_ForwardTime.Text_Title = "Forward Time";
			this.OC_ForwardTime.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_AutoEpSwitch
			// 
			this.OC_AutoEpSwitch.Checked = true;
			this.OC_AutoEpSwitch.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_AutoEpSwitch.Location = new System.Drawing.Point(745, 15);
			this.OC_AutoEpSwitch.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_AutoEpSwitch.Name = "OC_AutoEpSwitch";
			this.OC_AutoEpSwitch.OptionList = new string[0];
			this.OC_AutoEpSwitch.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_AutoEpSwitch.SelectedOption = "";
			this.OC_AutoEpSwitch.Size = new System.Drawing.Size(350, 100);
			this.OC_AutoEpSwitch.SpaceTriggersClick = true;
			this.OC_AutoEpSwitch.TabIndex = 2;
			this.OC_AutoEpSwitch.Text_CheckBox_Checked = "Switch automatically";
			this.OC_AutoEpSwitch.Text_CheckBox_Unchecked = "Do not switch";
			this.OC_AutoEpSwitch.Text_Info = "Switches to the next episode once the one you\'re currently watching ends.";
			this.OC_AutoEpSwitch.Text_Title = "Automatic Episode Switching";
			this.OC_AutoEpSwitch.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_FullScreenPlayer
			// 
			this.OC_FullScreenPlayer.Checked = true;
			this.OC_FullScreenPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_FullScreenPlayer.Location = new System.Drawing.Point(15, 130);
			this.OC_FullScreenPlayer.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_FullScreenPlayer.Name = "OC_FullScreenPlayer";
			this.OC_FullScreenPlayer.OptionList = new string[0];
			this.OC_FullScreenPlayer.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_FullScreenPlayer.SelectedOption = "";
			this.OC_FullScreenPlayer.Size = new System.Drawing.Size(350, 100);
			this.OC_FullScreenPlayer.SpaceTriggersClick = true;
			this.OC_FullScreenPlayer.TabIndex = 3;
			this.OC_FullScreenPlayer.Text_CheckBox_Checked = "Starts in full-screen";
			this.OC_FullScreenPlayer.Text_CheckBox_Unchecked = "Keeps the current window";
			this.OC_FullScreenPlayer.Text_Info = "Choose to start the video player in full-screen or in a normal window.";
			this.OC_FullScreenPlayer.Text_Title = "Full Screen Player";
			this.OC_FullScreenPlayer.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_PnP
			// 
			this.OC_PnP.Checked = true;
			this.OC_PnP.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_PnP.Location = new System.Drawing.Point(380, 130);
			this.OC_PnP.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_PnP.Name = "OC_PnP";
			this.OC_PnP.OptionList = new string[0];
			this.OC_PnP.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_PnP.SelectedOption = "";
			this.OC_PnP.Size = new System.Drawing.Size(350, 100);
			this.OC_PnP.SpaceTriggersClick = true;
			this.OC_PnP.TabIndex = 5;
			this.OC_PnP.Text_CheckBox_Checked = "Enabled";
			this.OC_PnP.Text_CheckBox_Unchecked = "Disabled";
			this.OC_PnP.Text_Info = "Switch to picture-in-picture when exiting a video before it ends.";
			this.OC_PnP.Text_Title = "Picture in Picture";
			this.OC_PnP.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_StickyMiniPlayer
			// 
			this.OC_StickyMiniPlayer.Checked = true;
			this.OC_StickyMiniPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_StickyMiniPlayer.Location = new System.Drawing.Point(745, 130);
			this.OC_StickyMiniPlayer.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_StickyMiniPlayer.Name = "OC_StickyMiniPlayer";
			this.OC_StickyMiniPlayer.OptionList = new string[0];
			this.OC_StickyMiniPlayer.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_StickyMiniPlayer.SelectedOption = "";
			this.OC_StickyMiniPlayer.Size = new System.Drawing.Size(350, 100);
			this.OC_StickyMiniPlayer.SpaceTriggersClick = true;
			this.OC_StickyMiniPlayer.TabIndex = 7;
			this.OC_StickyMiniPlayer.Text_CheckBox_Checked = "Enabled";
			this.OC_StickyMiniPlayer.Text_CheckBox_Unchecked = "Disabled";
			this.OC_StickyMiniPlayer.Text_Info = "Slides the mini-player to automatically move back to a corner of the screen based" +
    " on where you place it.";
			this.OC_StickyMiniPlayer.Text_Title = "Sticky Mini-Player";
			this.OC_StickyMiniPlayer.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_AutoPauseScroll
			// 
			this.OC_AutoPauseScroll.Checked = true;
			this.OC_AutoPauseScroll.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_AutoPauseScroll.Location = new System.Drawing.Point(15, 245);
			this.OC_AutoPauseScroll.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_AutoPauseScroll.Name = "OC_AutoPauseScroll";
			this.OC_AutoPauseScroll.OptionList = new string[0];
			this.OC_AutoPauseScroll.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_AutoPauseScroll.SelectedOption = "";
			this.OC_AutoPauseScroll.Size = new System.Drawing.Size(350, 100);
			this.OC_AutoPauseScroll.SpaceTriggersClick = true;
			this.OC_AutoPauseScroll.TabIndex = 8;
			this.OC_AutoPauseScroll.Text_CheckBox_Checked = "Auto-Pauses";
			this.OC_AutoPauseScroll.Text_CheckBox_Unchecked = "Does nothing";
			this.OC_AutoPauseScroll.Text_Info = "Automatically pause/play when scrolling down to see more info about the episode/m" +
    "ovie.";
			this.OC_AutoPauseScroll.Text_Title = "Auto-Pause on Scroll";
			this.OC_AutoPauseScroll.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_TopMostPlayer
			// 
			this.OC_TopMostPlayer.Checked = true;
			this.OC_TopMostPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_TopMostPlayer.Location = new System.Drawing.Point(380, 245);
			this.OC_TopMostPlayer.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_TopMostPlayer.Name = "OC_TopMostPlayer";
			this.OC_TopMostPlayer.OptionList = new string[0];
			this.OC_TopMostPlayer.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_TopMostPlayer.SelectedOption = "";
			this.OC_TopMostPlayer.Size = new System.Drawing.Size(350, 100);
			this.OC_TopMostPlayer.SpaceTriggersClick = true;
			this.OC_TopMostPlayer.TabIndex = 10;
			this.OC_TopMostPlayer.Text_CheckBox_Checked = "Enabled";
			this.OC_TopMostPlayer.Text_CheckBox_Unchecked = "Disabled";
			this.OC_TopMostPlayer.Text_Info = "Keeps the mini-player on top of all other windows at all times.";
			this.OC_TopMostPlayer.Text_Title = "Keep Mini-Player on Top";
			// 
			// P_Download
			// 
			this.P_Download.AutoSize = true;
			this.P_Download.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Download.Controls.Add(this.OC_Quality);
			this.P_Download.Controls.Add(this.OC_DownloadOption);
			this.P_Download.Controls.Add(this.OC_DownloadBehavior);
			this.P_Download.Location = new System.Drawing.Point(0, 0);
			this.P_Download.MaximumSize = new System.Drawing.Size(1204, 2147483647);
			this.P_Download.MinimumSize = new System.Drawing.Size(1204, 0);
			this.P_Download.Name = "P_Download";
			this.P_Download.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.P_Download.Size = new System.Drawing.Size(1204, 125);
			this.P_Download.TabIndex = 23;
			// 
			// OC_Quality
			// 
			this.OC_Quality.Checked = true;
			this.OC_Quality.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_Quality.Location = new System.Drawing.Point(15, 15);
			this.OC_Quality.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_Quality.Name = "OC_Quality";
			this.OC_Quality.OptionList = new string[] {
        "3D",
        "4K Ultra",
        "1080p",
        "720p",
        "Low"};
			this.OC_Quality.OptionType = ShowsCalendar.OptionType.OptionList;
			this.OC_Quality.SelectedOption = "1080p";
			this.OC_Quality.Size = new System.Drawing.Size(350, 100);
			this.OC_Quality.SpaceTriggersClick = true;
			this.OC_Quality.TabIndex = 0;
			this.OC_Quality.Text_CheckBox_Checked = "";
			this.OC_Quality.Text_CheckBox_Unchecked = "";
			this.OC_Quality.Text_Info = "Select which video quality you\'d like highlighted while downloading torrents.";
			this.OC_Quality.Text_Title = "Preferred Quality";
			this.OC_Quality.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_DownloadOption
			// 
			this.OC_DownloadOption.Checked = true;
			this.OC_DownloadOption.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_DownloadOption.Location = new System.Drawing.Point(380, 15);
			this.OC_DownloadOption.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_DownloadOption.Name = "OC_DownloadOption";
			this.OC_DownloadOption.OptionList = new string[0];
			this.OC_DownloadOption.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_DownloadOption.SelectedOption = "";
			this.OC_DownloadOption.Size = new System.Drawing.Size(350, 100);
			this.OC_DownloadOption.SpaceTriggersClick = true;
			this.OC_DownloadOption.TabIndex = 1;
			this.OC_DownloadOption.Text_CheckBox_Checked = "Show All downloads";
			this.OC_DownloadOption.Text_CheckBox_Unchecked = "Pre-select preferred quality";
			this.OC_DownloadOption.Text_Info = "Changes the default download view between \'All\' qualities or your \'Preferred\' qua" +
    "lity.";
			this.OC_DownloadOption.Text_Title = "Download Options";
			this.OC_DownloadOption.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_DownloadBehavior
			// 
			this.OC_DownloadBehavior.Checked = true;
			this.OC_DownloadBehavior.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_DownloadBehavior.Location = new System.Drawing.Point(745, 15);
			this.OC_DownloadBehavior.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_DownloadBehavior.Name = "OC_DownloadBehavior";
			this.OC_DownloadBehavior.OptionList = new string[0];
			this.OC_DownloadBehavior.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_DownloadBehavior.SelectedOption = "";
			this.OC_DownloadBehavior.Size = new System.Drawing.Size(350, 100);
			this.OC_DownloadBehavior.SpaceTriggersClick = true;
			this.OC_DownloadBehavior.TabIndex = 2;
			this.OC_DownloadBehavior.Text_CheckBox_Checked = "Close automatically";
			this.OC_DownloadBehavior.Text_CheckBox_Unchecked = "Keep opened";
			this.OC_DownloadBehavior.Text_Info = "Closes the download window automatically after you click on a torrent\'s download " +
    "button.";
			this.OC_DownloadBehavior.Text_Title = "Download Behavior";
			this.OC_DownloadBehavior.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// P_App
			// 
			this.P_App.AutoSize = true;
			this.P_App.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_App.Controls.Add(this.TB_SavePath);
			this.P_App.Controls.Add(this.OC_NoAnimations);
			this.P_App.Controls.Add(this.OC_EpNotification);
			this.P_App.Controls.Add(this.OC_NotificationSound);
			this.P_App.Controls.Add(this.OC_StartMode);
			this.P_App.Controls.Add(this.OC_LaunchWithWindows);
			this.P_App.Location = new System.Drawing.Point(0, 0);
			this.P_App.MaximumSize = new System.Drawing.Size(1204, 2147483647);
			this.P_App.MinimumSize = new System.Drawing.Size(1204, 0);
			this.P_App.Name = "P_App";
			this.P_App.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.P_App.Size = new System.Drawing.Size(1204, 290);
			this.P_App.TabIndex = 22;
			// 
			// TB_SavePath
			// 
			this.TB_SavePath.FileExtensions = new string[0];
			this.P_App.SetFlowBreak(this.TB_SavePath, true);
			this.TB_SavePath.Folder = true;
			this.TB_SavePath.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TB_SavePath.Image = ((System.Drawing.Image)(resources.GetObject("TB_SavePath.Image")));
			this.TB_SavePath.LabelText = "Options Save Path";
			this.TB_SavePath.Location = new System.Drawing.Point(15, 15);
			this.TB_SavePath.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.TB_SavePath.MaximumSize = new System.Drawing.Size(9999, 0);
			this.TB_SavePath.MinimumSize = new System.Drawing.Size(50, 35);
			this.TB_SavePath.Name = "TB_SavePath";
			this.TB_SavePath.Placeholder = "Change this to save your options, data && thumbnails in a different place than yo" +
    "ur documents";
			this.TB_SavePath.SelectedText = "";
			this.TB_SavePath.SelectionLength = 0;
			this.TB_SavePath.SelectionStart = 0;
			this.TB_SavePath.Size = new System.Drawing.Size(1060, 35);
			this.TB_SavePath.TabIndex = 0;
			// 
			// OC_NoAnimations
			// 
			this.OC_NoAnimations.Checked = true;
			this.OC_NoAnimations.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_NoAnimations.Location = new System.Drawing.Point(15, 65);
			this.OC_NoAnimations.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_NoAnimations.Name = "OC_NoAnimations";
			this.OC_NoAnimations.OptionList = new string[0];
			this.OC_NoAnimations.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_NoAnimations.SelectedOption = "";
			this.OC_NoAnimations.Size = new System.Drawing.Size(350, 100);
			this.OC_NoAnimations.SpaceTriggersClick = true;
			this.OC_NoAnimations.TabIndex = 2;
			this.OC_NoAnimations.Text_CheckBox_Checked = "Animations are disabled";
			this.OC_NoAnimations.Text_CheckBox_Unchecked = "Animations are enabled";
			this.OC_NoAnimations.Text_Info = "Choose to disable animations & smooth scrolling throughout the application.";
			this.OC_NoAnimations.Text_Title = "Disable Animations";
			this.OC_NoAnimations.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_EpNotification
			// 
			this.OC_EpNotification.Checked = true;
			this.OC_EpNotification.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_EpNotification.Location = new System.Drawing.Point(380, 65);
			this.OC_EpNotification.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_EpNotification.Name = "OC_EpNotification";
			this.OC_EpNotification.OptionList = new string[0];
			this.OC_EpNotification.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_EpNotification.SelectedOption = "";
			this.OC_EpNotification.Size = new System.Drawing.Size(350, 100);
			this.OC_EpNotification.SpaceTriggersClick = true;
			this.OC_EpNotification.TabIndex = 3;
			this.OC_EpNotification.Text_CheckBox_Checked = "Show notification";
			this.OC_EpNotification.Text_CheckBox_Unchecked = "Does nothing";
			this.OC_EpNotification.Text_Info = "Pops up a notification you when episodes air.";
			this.OC_EpNotification.Text_Title = "Episode Notification";
			this.OC_EpNotification.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_NotificationSound
			// 
			this.OC_NotificationSound.Checked = true;
			this.OC_NotificationSound.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_NotificationSound.Location = new System.Drawing.Point(745, 65);
			this.OC_NotificationSound.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_NotificationSound.Name = "OC_NotificationSound";
			this.OC_NotificationSound.OptionList = new string[0];
			this.OC_NotificationSound.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_NotificationSound.SelectedOption = "";
			this.OC_NotificationSound.Size = new System.Drawing.Size(350, 100);
			this.OC_NotificationSound.SpaceTriggersClick = true;
			this.OC_NotificationSound.TabIndex = 4;
			this.OC_NotificationSound.Text_CheckBox_Checked = "Play sounds";
			this.OC_NotificationSound.Text_CheckBox_Unchecked = "Mute sounds";
			this.OC_NotificationSound.Text_Info = "Play a little tone when a notification appears around the app.";
			this.OC_NotificationSound.Text_Title = "Notification Sounds";
			this.OC_NotificationSound.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_StartMode
			// 
			this.OC_StartMode.Checked = true;
			this.OC_StartMode.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_StartMode.Location = new System.Drawing.Point(15, 180);
			this.OC_StartMode.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_StartMode.Name = "OC_StartMode";
			this.OC_StartMode.OptionList = new string[0];
			this.OC_StartMode.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_StartMode.SelectedOption = "";
			this.OC_StartMode.Size = new System.Drawing.Size(350, 100);
			this.OC_StartMode.SpaceTriggersClick = true;
			this.OC_StartMode.TabIndex = 5;
			this.OC_StartMode.Text_CheckBox_Checked = "Starts minimized";
			this.OC_StartMode.Text_CheckBox_Unchecked = "Start normally";
			this.OC_StartMode.Text_Info = "Start the app in the background and remains out of your way.";
			this.OC_StartMode.Text_Title = "Start Minimized";
			this.OC_StartMode.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_LaunchWithWindows
			// 
			this.OC_LaunchWithWindows.Checked = true;
			this.OC_LaunchWithWindows.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_LaunchWithWindows.Location = new System.Drawing.Point(380, 180);
			this.OC_LaunchWithWindows.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_LaunchWithWindows.Name = "OC_LaunchWithWindows";
			this.OC_LaunchWithWindows.OptionList = new string[0];
			this.OC_LaunchWithWindows.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_LaunchWithWindows.SelectedOption = "";
			this.OC_LaunchWithWindows.Size = new System.Drawing.Size(350, 100);
			this.OC_LaunchWithWindows.SpaceTriggersClick = true;
			this.OC_LaunchWithWindows.TabIndex = 6;
			this.OC_LaunchWithWindows.Text_CheckBox_Checked = "Launch automatically";
			this.OC_LaunchWithWindows.Text_CheckBox_Unchecked = "Launch manually";
			this.OC_LaunchWithWindows.Text_Info = "Start the app when turning on the computer to save yourself a few clicks.";
			this.OC_LaunchWithWindows.Text_Title = "Boot Launch";
			this.OC_LaunchWithWindows.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// P_General
			// 
			this.P_General.AutoSize = true;
			this.P_General.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_General.Controls.Add(this.OC_IgnoreSpecialsSeason);
			this.P_General.Controls.Add(this.OC_SpoilerThumbnail);
			this.P_General.Controls.Add(this.PC_ShowsOrder);
			this.P_General.Controls.Add(this.PC_MoviesOrder);
			this.P_General.Controls.Add(this.OC_AlwaysShowBanners);
			this.P_General.Controls.Add(this.OC_FinaleWarning);
			this.P_General.Controls.Add(this.OC_EpBehavior);
			this.P_General.Location = new System.Drawing.Point(0, 0);
			this.P_General.MaximumSize = new System.Drawing.Size(1204, 2147483647);
			this.P_General.MinimumSize = new System.Drawing.Size(1204, 0);
			this.P_General.Name = "P_General";
			this.P_General.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.P_General.Size = new System.Drawing.Size(1204, 355);
			this.P_General.TabIndex = 21;
			// 
			// OC_IgnoreSpecialsSeason
			// 
			this.OC_IgnoreSpecialsSeason.Checked = true;
			this.OC_IgnoreSpecialsSeason.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_IgnoreSpecialsSeason.Location = new System.Drawing.Point(15, 15);
			this.OC_IgnoreSpecialsSeason.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_IgnoreSpecialsSeason.Name = "OC_IgnoreSpecialsSeason";
			this.OC_IgnoreSpecialsSeason.OptionList = new string[0];
			this.OC_IgnoreSpecialsSeason.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_IgnoreSpecialsSeason.SelectedOption = "";
			this.OC_IgnoreSpecialsSeason.Size = new System.Drawing.Size(350, 100);
			this.OC_IgnoreSpecialsSeason.SpaceTriggersClick = true;
			this.OC_IgnoreSpecialsSeason.TabIndex = 0;
			this.OC_IgnoreSpecialsSeason.Text_CheckBox_Checked = "Specials will be ignored";
			this.OC_IgnoreSpecialsSeason.Text_CheckBox_Unchecked = "Specials will be included";
			this.OC_IgnoreSpecialsSeason.Text_Info = "Ignores the specials when finding the order to watch episodes.";
			this.OC_IgnoreSpecialsSeason.Text_Title = "Ignore Specials Season";
			this.OC_IgnoreSpecialsSeason.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_SpoilerThumbnail
			// 
			this.OC_SpoilerThumbnail.Checked = true;
			this.OC_SpoilerThumbnail.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_SpoilerThumbnail.Location = new System.Drawing.Point(380, 15);
			this.OC_SpoilerThumbnail.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_SpoilerThumbnail.Name = "OC_SpoilerThumbnail";
			this.OC_SpoilerThumbnail.OptionList = new string[0];
			this.OC_SpoilerThumbnail.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_SpoilerThumbnail.SelectedOption = "";
			this.OC_SpoilerThumbnail.Size = new System.Drawing.Size(350, 100);
			this.OC_SpoilerThumbnail.SpaceTriggersClick = true;
			this.OC_SpoilerThumbnail.TabIndex = 1;
			this.OC_SpoilerThumbnail.Text_CheckBox_Checked = "Thumbnails will be blurred";
			this.OC_SpoilerThumbnail.Text_CheckBox_Unchecked = "Thumbnails will not be blurred";
			this.OC_SpoilerThumbnail.Text_Info = "Blurs episode thumbnails that you haven\'t watched yet.";
			this.OC_SpoilerThumbnail.Text_Title = "Spoiler Blur";
			this.OC_SpoilerThumbnail.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// PC_ShowsOrder
			// 
			this.PC_ShowsOrder.Checked = true;
			this.PC_ShowsOrder.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PC_ShowsOrder.Location = new System.Drawing.Point(745, 15);
			this.PC_ShowsOrder.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.PC_ShowsOrder.Name = "PC_ShowsOrder";
			this.PC_ShowsOrder.OptionList = new string[] {
        "3D",
        "4K Ultra",
        "1080p",
        "720p",
        "Low"};
			this.PC_ShowsOrder.OptionType = ShowsCalendar.OptionType.OptionList;
			this.PC_ShowsOrder.SelectedOption = "Relative Release Date";
			this.PC_ShowsOrder.Size = new System.Drawing.Size(350, 100);
			this.PC_ShowsOrder.SpaceTriggersClick = true;
			this.PC_ShowsOrder.TabIndex = 2;
			this.PC_ShowsOrder.Text_CheckBox_Checked = "";
			this.PC_ShowsOrder.Text_CheckBox_Unchecked = "";
			this.PC_ShowsOrder.Text_Info = "Choose how your TV Show library stand will be ordered.";
			this.PC_ShowsOrder.Text_Title = "TV Shows Order";
			this.PC_ShowsOrder.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// PC_MoviesOrder
			// 
			this.PC_MoviesOrder.Checked = true;
			this.PC_MoviesOrder.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PC_MoviesOrder.Location = new System.Drawing.Point(15, 130);
			this.PC_MoviesOrder.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.PC_MoviesOrder.Name = "PC_MoviesOrder";
			this.PC_MoviesOrder.OptionList = new string[] {
        "3D",
        "4K Ultra",
        "1080p",
        "720p",
        "Low"};
			this.PC_MoviesOrder.OptionType = ShowsCalendar.OptionType.OptionList;
			this.PC_MoviesOrder.SelectedOption = "Year";
			this.PC_MoviesOrder.Size = new System.Drawing.Size(350, 100);
			this.PC_MoviesOrder.SpaceTriggersClick = true;
			this.PC_MoviesOrder.TabIndex = 3;
			this.PC_MoviesOrder.Text_CheckBox_Checked = "";
			this.PC_MoviesOrder.Text_CheckBox_Unchecked = "";
			this.PC_MoviesOrder.Text_Info = "Choose how your Movie library stand will be ordered.";
			this.PC_MoviesOrder.Text_Title = "Movies Order";
			this.PC_MoviesOrder.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_AlwaysShowBanners
			// 
			this.OC_AlwaysShowBanners.Checked = true;
			this.OC_AlwaysShowBanners.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_AlwaysShowBanners.Location = new System.Drawing.Point(380, 130);
			this.OC_AlwaysShowBanners.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_AlwaysShowBanners.Name = "OC_AlwaysShowBanners";
			this.OC_AlwaysShowBanners.OptionList = new string[0];
			this.OC_AlwaysShowBanners.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_AlwaysShowBanners.SelectedOption = "";
			this.OC_AlwaysShowBanners.Size = new System.Drawing.Size(350, 100);
			this.OC_AlwaysShowBanners.SpaceTriggersClick = true;
			this.OC_AlwaysShowBanners.TabIndex = 5;
			this.OC_AlwaysShowBanners.Text_CheckBox_Checked = "Always Show";
			this.OC_AlwaysShowBanners.Text_CheckBox_Unchecked = "Show on Hover";
			this.OC_AlwaysShowBanners.Text_Info = "Display content banners at all times or only when hovering over the content tile." +
    "";
			this.OC_AlwaysShowBanners.Text_Title = "Content Banners";
			this.OC_AlwaysShowBanners.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_FinaleWarning
			// 
			this.OC_FinaleWarning.Checked = true;
			this.OC_FinaleWarning.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_FinaleWarning.Location = new System.Drawing.Point(745, 130);
			this.OC_FinaleWarning.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_FinaleWarning.Name = "OC_FinaleWarning";
			this.OC_FinaleWarning.OptionList = new string[0];
			this.OC_FinaleWarning.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_FinaleWarning.SelectedOption = "";
			this.OC_FinaleWarning.Size = new System.Drawing.Size(350, 100);
			this.OC_FinaleWarning.SpaceTriggersClick = true;
			this.OC_FinaleWarning.TabIndex = 4;
			this.OC_FinaleWarning.Text_CheckBox_Checked = "Warn me";
			this.OC_FinaleWarning.Text_CheckBox_Unchecked = "Do not warn me";
			this.OC_FinaleWarning.Text_Info = "Get warned when you start up the finale episode of a season.";
			this.OC_FinaleWarning.Text_Title = "Finale Warning";
			this.OC_FinaleWarning.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// OC_EpBehavior
			// 
			this.OC_EpBehavior.Checked = true;
			this.OC_EpBehavior.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_EpBehavior.Location = new System.Drawing.Point(15, 245);
			this.OC_EpBehavior.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_EpBehavior.Name = "OC_EpBehavior";
			this.OC_EpBehavior.OptionList = new string[0];
			this.OC_EpBehavior.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_EpBehavior.SelectedOption = "";
			this.OC_EpBehavior.Size = new System.Drawing.Size(350, 100);
			this.OC_EpBehavior.SpaceTriggersClick = true;
			this.OC_EpBehavior.TabIndex = 5;
			this.OC_EpBehavior.Text_CheckBox_Checked = "Open All";
			this.OC_EpBehavior.Text_CheckBox_Unchecked = "Only open the episode";
			this.OC_EpBehavior.Text_Info = "Choose to open the Show & Season Pages when opening the Episode Page.";
			this.OC_EpBehavior.Text_Title = "Episode Info Behavior";
			this.OC_EpBehavior.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
			// 
			// T_App
			// 
			this.T_App.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_App.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_App.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.T_App.Icon = global::ShowsCalendar.Properties.Resources.Tiny_App;
			this.T_App.LinkedControl = this.P_App;
			this.T_App.Location = new System.Drawing.Point(303, 5);
			this.T_App.Margin = new System.Windows.Forms.Padding(0);
			this.T_App.Name = "T_App";
			this.T_App.Selected = false;
			this.T_App.Size = new System.Drawing.Size(303, 25);
			this.T_App.TabIndex = 4;
			this.T_App.TabStop = false;
			this.T_App.Text = "Application";
			// 
			// T_Watch
			// 
			this.T_Watch.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Watch.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Watch.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.T_Watch.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Play;
			this.T_Watch.LinkedControl = this.P_Watch;
			this.T_Watch.Location = new System.Drawing.Point(606, 5);
			this.T_Watch.Margin = new System.Windows.Forms.Padding(0);
			this.T_Watch.Name = "T_Watch";
			this.T_Watch.Selected = true;
			this.T_Watch.Size = new System.Drawing.Size(303, 25);
			this.T_Watch.TabIndex = 3;
			this.T_Watch.TabStop = false;
			this.T_Watch.Text = "Video Player";
			// 
			// T_Downloads
			// 
			this.T_Downloads.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Downloads.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Downloads.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.T_Downloads.Icon = global::ShowsCalendar.Properties.Resources.Tiny_CloudDownload;
			this.T_Downloads.LinkedControl = this.P_Download;
			this.T_Downloads.Location = new System.Drawing.Point(909, 5);
			this.T_Downloads.Margin = new System.Windows.Forms.Padding(0);
			this.T_Downloads.Name = "T_Downloads";
			this.T_Downloads.Selected = false;
			this.T_Downloads.Size = new System.Drawing.Size(303, 25);
			this.T_Downloads.TabIndex = 1;
			this.T_Downloads.TabStop = false;
			this.T_Downloads.Text = "Downloads";
			// 
			// T_General
			// 
			this.T_General.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_General.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_General.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.T_General.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Settings;
			this.T_General.LinkedControl = this.P_General;
			this.T_General.Location = new System.Drawing.Point(0, 5);
			this.T_General.Margin = new System.Windows.Forms.Padding(0);
			this.T_General.Name = "T_General";
			this.T_General.Selected = false;
			this.T_General.Size = new System.Drawing.Size(303, 25);
			this.T_General.TabIndex = 0;
			this.T_General.TabStop = false;
			this.T_General.Text = "General";
			// 
			// B_Done
			// 
			this.B_Done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Done.ColorShade = null;
			this.B_Done.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Done.Image = ((System.Drawing.Image)(resources.GetObject("B_Done.Image")));
			this.B_Done.Location = new System.Drawing.Point(1063, 11);
			this.B_Done.Margin = new System.Windows.Forms.Padding(15, 0, 15, 15);
			this.B_Done.Name = "B_Done";
			this.B_Done.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.B_Done.Size = new System.Drawing.Size(135, 28);
			this.B_Done.SpaceTriggersClick = true;
			this.B_Done.TabIndex = 107;
			this.B_Done.Text = "APPLY CHANGES";
			this.B_Done.Click += new System.EventHandler(this.B_Apply_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.B_Done, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 652);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1213, 54);
			this.tableLayoutPanel1.TabIndex = 108;
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(15, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(15, 0, 15, 10);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1183, 1);
			this.panel1.TabIndex = 108;
			this.panel1.Visible = false;
			// 
			// slickTabControl1
			// 
			this.slickTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl1.Location = new System.Drawing.Point(0, 0);
			this.slickTabControl1.Name = "slickTabControl1";
			this.slickTabControl1.Size = new System.Drawing.Size(1213, 652);
			this.slickTabControl1.TabIndex = 13;
			this.slickTabControl1.Tabs = new SlickControls.SlickTabControl.Tab[] {
        this.T_General,
        this.T_App,
        this.T_Watch,
        this.T_Downloads};
			// 
			// OC_PauseWhenOutOfFocusFullScreen
			// 
			this.OC_PauseWhenOutOfFocusFullScreen.Checked = true;
			this.OC_PauseWhenOutOfFocusFullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OC_PauseWhenOutOfFocusFullScreen.Location = new System.Drawing.Point(745, 245);
			this.OC_PauseWhenOutOfFocusFullScreen.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.OC_PauseWhenOutOfFocusFullScreen.Name = "OC_PauseWhenOutOfFocusFullScreen";
			this.OC_PauseWhenOutOfFocusFullScreen.OptionList = new string[0];
			this.OC_PauseWhenOutOfFocusFullScreen.OptionType = ShowsCalendar.OptionType.Checkbox;
			this.OC_PauseWhenOutOfFocusFullScreen.SelectedOption = "";
			this.OC_PauseWhenOutOfFocusFullScreen.Size = new System.Drawing.Size(350, 100);
			this.OC_PauseWhenOutOfFocusFullScreen.SpaceTriggersClick = true;
			this.OC_PauseWhenOutOfFocusFullScreen.TabIndex = 11;
			this.OC_PauseWhenOutOfFocusFullScreen.Text_CheckBox_Checked = "Enabled";
			this.OC_PauseWhenOutOfFocusFullScreen.Text_CheckBox_Unchecked = "Disabled";
			this.OC_PauseWhenOutOfFocusFullScreen.Text_Info = "Pause the video when you\'re dooing soomething else while in full screen";
			this.OC_PauseWhenOutOfFocusFullScreen.Text_Title = "Pause when out of focus in Full Screen";
			// 
			// PC_Settings
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.slickTabControl1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PC_Settings";
			this.Padding = new System.Windows.Forms.Padding(0);
			this.Size = new System.Drawing.Size(1213, 706);
			this.Text = "Settings";
			this.Resize += new System.EventHandler(this.PC_Settings_Resize);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.slickTabControl1, 0);
			this.P_Watch.ResumeLayout(false);
			this.P_Download.ResumeLayout(false);
			this.P_App.ResumeLayout(false);
			this.P_General.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel P_Watch;
		private OptionControl OC_BackwardTime;
		private OptionControl OC_ForwardTime;
		private OptionControl OC_FullScreenPlayer;
		private System.Windows.Forms.FlowLayoutPanel P_Download;
		private OptionControl OC_Quality;
		private OptionControl OC_DownloadOption;
		private OptionControl OC_DownloadBehavior;
		private System.Windows.Forms.FlowLayoutPanel P_App;
		private OptionControl OC_StartMode;
		private OptionControl OC_LaunchWithWindows;
		private System.Windows.Forms.FlowLayoutPanel P_General;
		private OptionControl OC_NotificationSound;
		private OptionControl OC_FinaleWarning;
		private OptionControl OC_EpNotification;
		private SlickControls.SlickTabControl.Tab T_App;
		private SlickControls.SlickTabControl.Tab T_Watch;
		private SlickControls.SlickTabControl.Tab T_Downloads;
		private SlickControls.SlickTabControl.Tab T_General;
		private SlickControls.SlickButton B_Done;
		private OptionControl PC_ShowsOrder;
		private OptionControl PC_MoviesOrder;
		private OptionControl OC_EpBehavior;
		private OptionControl OC_AutoEpSwitch;
		private OptionControl OC_AutoPauseScroll;
		private OptionControl OC_SpoilerThumbnail;
		private OptionControl OC_IgnoreSpecialsSeason;
		private OptionControl OC_PnP;
		private OptionControl OC_StickyMiniPlayer;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private OptionControl OC_NoAnimations;
		private System.Windows.Forms.Panel panel1;
		private SlickControls.SlickPathTextBox TB_SavePath;
		private OptionControl OC_TopMostPlayer;
		private SlickControls.SlickTabControl slickTabControl1;
		private OptionControl OC_AlwaysShowBanners;
		private OptionControl OC_PauseWhenOutOfFocusFullScreen;
	}
}
