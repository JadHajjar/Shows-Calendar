namespace TVShowsCalendar
{
	partial class SettingsForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.B_Done = new SlickControls.Controls.SlickButton();
			this.P_OptionContainer = new System.Windows.Forms.Panel();
			this.verticalScroll1 = new SlickControls.Controls.VerticalScroll();
			this.P_Options = new System.Windows.Forms.Panel();
			this.P_Watch = new System.Windows.Forms.Panel();
			this.OC_BackwardTime = new TVShowsCalendar.OptionControl();
			this.OC_ForwardTime = new TVShowsCalendar.OptionControl();
			this.OC_FullScreenPlayer = new TVShowsCalendar.OptionControl();
			this.P_Download = new System.Windows.Forms.Panel();
			this.OC_Quality = new TVShowsCalendar.OptionControl();
			this.OC_DownloadOption = new TVShowsCalendar.OptionControl();
			this.OC_DownloadBehavior = new TVShowsCalendar.OptionControl();
			this.OC_EpBehavior = new TVShowsCalendar.OptionControl();
			this.P_App = new System.Windows.Forms.Panel();
			this.OC_MoviesRefreshDays = new TVShowsCalendar.OptionControl();
			this.OC_ShowsRefreshDays = new TVShowsCalendar.OptionControl();
			this.OC_StartMode = new TVShowsCalendar.OptionControl();
			this.OC_LaunchWithWindows = new TVShowsCalendar.OptionControl();
			this.P_General = new System.Windows.Forms.Panel();
			this.OC_NotificationSound = new TVShowsCalendar.OptionControl();
			this.OC_UnwatchedBanner = new TVShowsCalendar.OptionControl();
			this.OC_FinaleWarning = new TVShowsCalendar.OptionControl();
			this.OC_EpNotification = new TVShowsCalendar.OptionControl();
			this.P_Tiles = new System.Windows.Forms.Panel();
			this.T_Watch = new SlickControls.Controls.SlickTile();
			this.T_Downloads = new SlickControls.Controls.SlickTile();
			this.T_App = new SlickControls.Controls.SlickTile();
			this.T_General = new SlickControls.Controls.SlickTile();
			this.P_Spacer = new SlickControls.Controls.DBPanel();
			this.base_P_Content.SuspendLayout();
			this.TLP_Main.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_Done)).BeginInit();
			this.P_OptionContainer.SuspendLayout();
			this.P_Options.SuspendLayout();
			this.P_Watch.SuspendLayout();
			this.P_Download.SuspendLayout();
			this.P_App.SuspendLayout();
			this.P_General.SuspendLayout();
			this.P_Tiles.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(58)))));
			this.base_P_Content.Controls.Add(this.TLP_Main);
			this.base_P_Content.Size = new System.Drawing.Size(781, 1054);
			// 
			// base_P_Controls
			// 
			this.base_P_Controls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(54)))));
			this.base_P_Controls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.base_P_Controls.Size = new System.Drawing.Size(781, 45);
			// 
			// base_P_Top_Spacer
			// 
			this.base_P_Top_Spacer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(89)))));
			this.base_P_Top_Spacer.Size = new System.Drawing.Size(781, 2);
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 20000;
			this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.toolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(65)))), ((int)(((byte)(77)))));
			this.toolTip.InitialDelay = 600;
			this.toolTip.ReshowDelay = 100;
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 3;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Controls.Add(this.B_Done, 0, 1);
			this.TLP_Main.Controls.Add(this.P_OptionContainer, 2, 0);
			this.TLP_Main.Controls.Add(this.P_Tiles, 0, 0);
			this.TLP_Main.Controls.Add(this.P_Spacer, 1, 0);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 2;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.Size = new System.Drawing.Size(781, 1054);
			this.TLP_Main.TabIndex = 2;
			// 
			// B_Done
			// 
			this.B_Done.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Done.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_Done.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Main.SetColumnSpan(this.B_Done, 3);
			this.B_Done.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Done.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Done.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.B_Done.HueShade = null;
			this.B_Done.IconSize = 17;
			this.B_Done.Image = global::TVShowsCalendar.Properties.Resources.Icon_OK;
			this.B_Done.Location = new System.Drawing.Point(338, 1020);
			this.B_Done.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.B_Done.Name = "B_Done";
			this.B_Done.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.B_Done.Size = new System.Drawing.Size(105, 28);
			this.B_Done.TabIndex = 10;
			this.B_Done.Text = "Apply";
			this.B_Done.Click += new System.EventHandler(this.B_Apply_Click);
			// 
			// P_OptionContainer
			// 
			this.P_OptionContainer.Controls.Add(this.verticalScroll1);
			this.P_OptionContainer.Controls.Add(this.P_Options);
			this.P_OptionContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_OptionContainer.Location = new System.Drawing.Point(176, 0);
			this.P_OptionContainer.Margin = new System.Windows.Forms.Padding(0);
			this.P_OptionContainer.Name = "P_OptionContainer";
			this.P_OptionContainer.Size = new System.Drawing.Size(605, 1014);
			this.P_OptionContainer.TabIndex = 34;
			this.P_OptionContainer.Resize += new System.EventHandler(this.P_OptionContainer_Resize);
			// 
			// verticalScroll1
			// 
			this.verticalScroll1.BarColor = null;
			this.verticalScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll1.LinkedControl = this.P_Options;
			this.verticalScroll1.Location = new System.Drawing.Point(416, 0);
			this.verticalScroll1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll1.Name = "verticalScroll1";
			this.verticalScroll1.Size = new System.Drawing.Size(4, 225);
			this.verticalScroll1.TabIndex = 35;
			// 
			// P_Options
			// 
			this.P_Options.AutoSize = true;
			this.P_Options.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Options.Controls.Add(this.P_Watch);
			this.P_Options.Controls.Add(this.P_Download);
			this.P_Options.Controls.Add(this.P_App);
			this.P_Options.Controls.Add(this.P_General);
			this.P_Options.Location = new System.Drawing.Point(0, 0);
			this.P_Options.MinimumSize = new System.Drawing.Size(600, 0);
			this.P_Options.Name = "P_Options";
			this.P_Options.Size = new System.Drawing.Size(600, 930);
			this.P_Options.TabIndex = 34;
			// 
			// P_Watch
			// 
			this.P_Watch.AutoSize = true;
			this.P_Watch.Controls.Add(this.OC_BackwardTime);
			this.P_Watch.Controls.Add(this.OC_ForwardTime);
			this.P_Watch.Controls.Add(this.OC_FullScreenPlayer);
			this.P_Watch.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Watch.Location = new System.Drawing.Point(0, 744);
			this.P_Watch.Name = "P_Watch";
			this.P_Watch.Size = new System.Drawing.Size(600, 186);
			this.P_Watch.TabIndex = 37;
			this.P_Watch.Visible = false;
			// 
			// OC_BackwardTime
			// 
			this.OC_BackwardTime.Checked = true;
			this.OC_BackwardTime.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_BackwardTime.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_BackwardTime.Location = new System.Drawing.Point(0, 124);
			this.OC_BackwardTime.Margin = new System.Windows.Forms.Padding(0);
			this.OC_BackwardTime.Name = "OC_BackwardTime";
			this.OC_BackwardTime.OptionList = new string[] {
        "60 seconds",
        "45 seconds",
        "30 seconds",
        "15 seconds",
        "10 seconds",
        "5 seconds"};
			this.OC_BackwardTime.OptionType = TVShowsCalendar.OptionType.OptionList;
			this.OC_BackwardTime.SelectedOption = "5 seconds";
			this.OC_BackwardTime.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_BackwardTime.Size = new System.Drawing.Size(600, 62);
			this.OC_BackwardTime.TabIndex = 3;
			this.OC_BackwardTime.Text_CheckBox_Checked = "Show All Downloads";
			this.OC_BackwardTime.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
			this.OC_BackwardTime.Text_Info = "Choose how much time to jump backward at once.";
			this.OC_BackwardTime.Text_Title = "Backward Time";
			// 
			// OC_ForwardTime
			// 
			this.OC_ForwardTime.Checked = true;
			this.OC_ForwardTime.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_ForwardTime.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_ForwardTime.Location = new System.Drawing.Point(0, 62);
			this.OC_ForwardTime.Margin = new System.Windows.Forms.Padding(0);
			this.OC_ForwardTime.Name = "OC_ForwardTime";
			this.OC_ForwardTime.OptionList = new string[] {
        "60 seconds",
        "45 seconds",
        "30 seconds",
        "15 seconds",
        "10 seconds",
        "5 seconds"};
			this.OC_ForwardTime.OptionType = TVShowsCalendar.OptionType.OptionList;
			this.OC_ForwardTime.SelectedOption = "15 seconds";
			this.OC_ForwardTime.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_ForwardTime.Size = new System.Drawing.Size(600, 62);
			this.OC_ForwardTime.TabIndex = 2;
			this.OC_ForwardTime.Text_CheckBox_Checked = "Show All Downloads";
			this.OC_ForwardTime.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
			this.OC_ForwardTime.Text_Info = "Choose how much time to jump forward at once.";
			this.OC_ForwardTime.Text_Title = "Forward Time";
			// 
			// OC_FullScreenPlayer
			// 
			this.OC_FullScreenPlayer.Checked = true;
			this.OC_FullScreenPlayer.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_FullScreenPlayer.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_FullScreenPlayer.Location = new System.Drawing.Point(0, 0);
			this.OC_FullScreenPlayer.Margin = new System.Windows.Forms.Padding(0);
			this.OC_FullScreenPlayer.Name = "OC_FullScreenPlayer";
			this.OC_FullScreenPlayer.OptionList = new string[0];
			this.OC_FullScreenPlayer.OptionType = TVShowsCalendar.OptionType.Checkbox;
			this.OC_FullScreenPlayer.SelectedOption = "";
			this.OC_FullScreenPlayer.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_FullScreenPlayer.Size = new System.Drawing.Size(600, 62);
			this.OC_FullScreenPlayer.TabIndex = 4;
			this.OC_FullScreenPlayer.Text_CheckBox_Checked = "Fullscreen Window";
			this.OC_FullScreenPlayer.Text_CheckBox_Unchecked = "Normal Window";
			this.OC_FullScreenPlayer.Text_Info = "Choose to start the video player in full-screen or in a normal window.";
			this.OC_FullScreenPlayer.Text_Title = "Download Options";
			// 
			// P_Download
			// 
			this.P_Download.AutoSize = true;
			this.P_Download.Controls.Add(this.OC_Quality);
			this.P_Download.Controls.Add(this.OC_DownloadOption);
			this.P_Download.Controls.Add(this.OC_DownloadBehavior);
			this.P_Download.Controls.Add(this.OC_EpBehavior);
			this.P_Download.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Download.Location = new System.Drawing.Point(0, 496);
			this.P_Download.Name = "P_Download";
			this.P_Download.Size = new System.Drawing.Size(600, 248);
			this.P_Download.TabIndex = 23;
			this.P_Download.Visible = false;
			// 
			// OC_Quality
			// 
			this.OC_Quality.Checked = true;
			this.OC_Quality.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_Quality.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_Quality.Location = new System.Drawing.Point(0, 186);
			this.OC_Quality.Margin = new System.Windows.Forms.Padding(0);
			this.OC_Quality.Name = "OC_Quality";
			this.OC_Quality.OptionList = new string[] {
        "3D",
        "4K Ultra",
        "1080p",
        "720p",
        "Low"};
			this.OC_Quality.OptionType = TVShowsCalendar.OptionType.OptionList;
			this.OC_Quality.SelectedOption = "1080p";
			this.OC_Quality.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_Quality.Size = new System.Drawing.Size(600, 62);
			this.OC_Quality.TabIndex = 1;
			this.OC_Quality.Text_CheckBox_Checked = "Show All Downloads";
			this.OC_Quality.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
			this.OC_Quality.Text_Info = "Specifies which Video Quality you\'d like highlighted while downloading Episodes.";
			this.OC_Quality.Text_Title = "Preferred Quality";
			// 
			// OC_DownloadOption
			// 
			this.OC_DownloadOption.Checked = true;
			this.OC_DownloadOption.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_DownloadOption.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_DownloadOption.Location = new System.Drawing.Point(0, 124);
			this.OC_DownloadOption.Margin = new System.Windows.Forms.Padding(0);
			this.OC_DownloadOption.Name = "OC_DownloadOption";
			this.OC_DownloadOption.OptionList = new string[0];
			this.OC_DownloadOption.OptionType = TVShowsCalendar.OptionType.Checkbox;
			this.OC_DownloadOption.SelectedOption = "";
			this.OC_DownloadOption.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_DownloadOption.Size = new System.Drawing.Size(600, 62);
			this.OC_DownloadOption.TabIndex = 2;
			this.OC_DownloadOption.Text_CheckBox_Checked = "Show All Downloads";
			this.OC_DownloadOption.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
			this.OC_DownloadOption.Text_Info = "Changes the default download view between \'All\' qualities or \'Preferred\' quality." +
    "";
			this.OC_DownloadOption.Text_Title = "Download Options";
			// 
			// OC_DownloadBehavior
			// 
			this.OC_DownloadBehavior.Checked = true;
			this.OC_DownloadBehavior.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_DownloadBehavior.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_DownloadBehavior.Location = new System.Drawing.Point(0, 62);
			this.OC_DownloadBehavior.Margin = new System.Windows.Forms.Padding(0);
			this.OC_DownloadBehavior.Name = "OC_DownloadBehavior";
			this.OC_DownloadBehavior.OptionList = new string[0];
			this.OC_DownloadBehavior.OptionType = TVShowsCalendar.OptionType.Checkbox;
			this.OC_DownloadBehavior.SelectedOption = "";
			this.OC_DownloadBehavior.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_DownloadBehavior.Size = new System.Drawing.Size(600, 62);
			this.OC_DownloadBehavior.TabIndex = 3;
			this.OC_DownloadBehavior.Text_CheckBox_Checked = "Close After Download";
			this.OC_DownloadBehavior.Text_CheckBox_Unchecked = "Keep Opened";
			this.OC_DownloadBehavior.Text_Info = "When enabled, the Download window will automatically close after you click on a t" +
    "orrent\'s download button.";
			this.OC_DownloadBehavior.Text_Title = "Download Behavior";
			// 
			// OC_EpBehavior
			// 
			this.OC_EpBehavior.Checked = true;
			this.OC_EpBehavior.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_EpBehavior.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_EpBehavior.Location = new System.Drawing.Point(0, 0);
			this.OC_EpBehavior.Margin = new System.Windows.Forms.Padding(0);
			this.OC_EpBehavior.Name = "OC_EpBehavior";
			this.OC_EpBehavior.OptionList = new string[0];
			this.OC_EpBehavior.OptionType = TVShowsCalendar.OptionType.Checkbox;
			this.OC_EpBehavior.SelectedOption = "";
			this.OC_EpBehavior.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_EpBehavior.Size = new System.Drawing.Size(600, 62);
			this.OC_EpBehavior.TabIndex = 5;
			this.OC_EpBehavior.Text_CheckBox_Checked = "Download Window";
			this.OC_EpBehavior.Text_CheckBox_Unchecked = "Show Page Window";
			this.OC_EpBehavior.Text_Info = "Choose between opening the Download or Show window when clicking on the Last Aire" +
    "d Episode.";
			this.OC_EpBehavior.Text_Title = "Episode Behavior";
			// 
			// P_App
			// 
			this.P_App.AutoSize = true;
			this.P_App.Controls.Add(this.OC_MoviesRefreshDays);
			this.P_App.Controls.Add(this.OC_ShowsRefreshDays);
			this.P_App.Controls.Add(this.OC_StartMode);
			this.P_App.Controls.Add(this.OC_LaunchWithWindows);
			this.P_App.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_App.Location = new System.Drawing.Point(0, 248);
			this.P_App.Name = "P_App";
			this.P_App.Size = new System.Drawing.Size(600, 248);
			this.P_App.TabIndex = 22;
			this.P_App.Visible = false;
			// 
			// OC_MoviesRefreshDays
			// 
			this.OC_MoviesRefreshDays.Checked = true;
			this.OC_MoviesRefreshDays.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_MoviesRefreshDays.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_MoviesRefreshDays.Location = new System.Drawing.Point(0, 186);
			this.OC_MoviesRefreshDays.Margin = new System.Windows.Forms.Padding(0);
			this.OC_MoviesRefreshDays.Name = "OC_MoviesRefreshDays";
			this.OC_MoviesRefreshDays.OptionList = new string[] {
        "30 days",
        "15 days",
        "10 days",
        "7 days",
        "5 days",
        "3 days",
        "2 days",
        "1 day"};
			this.OC_MoviesRefreshDays.OptionType = TVShowsCalendar.OptionType.OptionList;
			this.OC_MoviesRefreshDays.SelectedOption = "10 days";
			this.OC_MoviesRefreshDays.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_MoviesRefreshDays.Size = new System.Drawing.Size(600, 62);
			this.OC_MoviesRefreshDays.TabIndex = 11;
			this.OC_MoviesRefreshDays.Text_CheckBox_Checked = "Show All Downloads";
			this.OC_MoviesRefreshDays.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
			this.OC_MoviesRefreshDays.Text_Info = "Set how often a movie is refreshed for newer information.";
			this.OC_MoviesRefreshDays.Text_Title = "Movies Refresh Frequency";
			// 
			// OC_ShowsRefreshDays
			// 
			this.OC_ShowsRefreshDays.Checked = true;
			this.OC_ShowsRefreshDays.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_ShowsRefreshDays.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_ShowsRefreshDays.Location = new System.Drawing.Point(0, 124);
			this.OC_ShowsRefreshDays.Margin = new System.Windows.Forms.Padding(0);
			this.OC_ShowsRefreshDays.Name = "OC_ShowsRefreshDays";
			this.OC_ShowsRefreshDays.OptionList = new string[] {
        "30 days",
        "15 days",
        "10 days",
        "7 days",
        "5 days",
        "3 days",
        "2 days",
        "1 day"};
			this.OC_ShowsRefreshDays.OptionType = TVShowsCalendar.OptionType.OptionList;
			this.OC_ShowsRefreshDays.SelectedOption = "2 days";
			this.OC_ShowsRefreshDays.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_ShowsRefreshDays.Size = new System.Drawing.Size(600, 62);
			this.OC_ShowsRefreshDays.TabIndex = 10;
			this.OC_ShowsRefreshDays.Text_CheckBox_Checked = "Show All Downloads";
			this.OC_ShowsRefreshDays.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
			this.OC_ShowsRefreshDays.Text_Info = "Set how often a show is refreshed for newer episodes and information.";
			this.OC_ShowsRefreshDays.Text_Title = "Shows Refresh Frequency";
			// 
			// OC_StartMode
			// 
			this.OC_StartMode.Checked = true;
			this.OC_StartMode.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_StartMode.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_StartMode.Location = new System.Drawing.Point(0, 62);
			this.OC_StartMode.Margin = new System.Windows.Forms.Padding(0);
			this.OC_StartMode.Name = "OC_StartMode";
			this.OC_StartMode.OptionList = new string[0];
			this.OC_StartMode.OptionType = TVShowsCalendar.OptionType.Checkbox;
			this.OC_StartMode.SelectedOption = "";
			this.OC_StartMode.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_StartMode.Size = new System.Drawing.Size(600, 62);
			this.OC_StartMode.TabIndex = 8;
			this.OC_StartMode.Text_CheckBox_Checked = "Start Normally";
			this.OC_StartMode.Text_CheckBox_Unchecked = "Start Hidden in Taskbar";
			this.OC_StartMode.Text_Info = "Choose to start the App normally or hidden in the TaskBar.";
			this.OC_StartMode.Text_Title = "Startup Mode";
			// 
			// OC_LaunchWithWindows
			// 
			this.OC_LaunchWithWindows.Checked = true;
			this.OC_LaunchWithWindows.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_LaunchWithWindows.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_LaunchWithWindows.Location = new System.Drawing.Point(0, 0);
			this.OC_LaunchWithWindows.Margin = new System.Windows.Forms.Padding(0);
			this.OC_LaunchWithWindows.Name = "OC_LaunchWithWindows";
			this.OC_LaunchWithWindows.OptionList = new string[0];
			this.OC_LaunchWithWindows.OptionType = TVShowsCalendar.OptionType.Checkbox;
			this.OC_LaunchWithWindows.SelectedOption = "";
			this.OC_LaunchWithWindows.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_LaunchWithWindows.Size = new System.Drawing.Size(600, 62);
			this.OC_LaunchWithWindows.TabIndex = 9;
			this.OC_LaunchWithWindows.Text_CheckBox_Checked = "Launch Automatically";
			this.OC_LaunchWithWindows.Text_CheckBox_Unchecked = "Launch Manually";
			this.OC_LaunchWithWindows.Text_Info = "Toggles whether to start the App when turning on the computer or not. (App must b" +
    "e run in Administrattor Mode)";
			this.OC_LaunchWithWindows.Text_Title = "Boot Launch";
			// 
			// P_General
			// 
			this.P_General.AutoSize = true;
			this.P_General.Controls.Add(this.OC_NotificationSound);
			this.P_General.Controls.Add(this.OC_UnwatchedBanner);
			this.P_General.Controls.Add(this.OC_FinaleWarning);
			this.P_General.Controls.Add(this.OC_EpNotification);
			this.P_General.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_General.Location = new System.Drawing.Point(0, 0);
			this.P_General.Name = "P_General";
			this.P_General.Size = new System.Drawing.Size(600, 248);
			this.P_General.TabIndex = 21;
			// 
			// OC_NotificationSound
			// 
			this.OC_NotificationSound.Checked = true;
			this.OC_NotificationSound.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_NotificationSound.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_NotificationSound.Location = new System.Drawing.Point(0, 186);
			this.OC_NotificationSound.Margin = new System.Windows.Forms.Padding(0);
			this.OC_NotificationSound.Name = "OC_NotificationSound";
			this.OC_NotificationSound.OptionList = new string[0];
			this.OC_NotificationSound.OptionType = TVShowsCalendar.OptionType.Checkbox;
			this.OC_NotificationSound.SelectedOption = "";
			this.OC_NotificationSound.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_NotificationSound.Size = new System.Drawing.Size(600, 62);
			this.OC_NotificationSound.TabIndex = 18;
			this.OC_NotificationSound.Text_CheckBox_Checked = "Play Sound";
			this.OC_NotificationSound.Text_CheckBox_Unchecked = "Do not play Sound";
			this.OC_NotificationSound.Text_Info = "Choose to turn the notification sound off if you want.";
			this.OC_NotificationSound.Text_Title = "Notification Sound";
			// 
			// OC_UnwatchedBanner
			// 
			this.OC_UnwatchedBanner.Checked = true;
			this.OC_UnwatchedBanner.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_UnwatchedBanner.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_UnwatchedBanner.Location = new System.Drawing.Point(0, 124);
			this.OC_UnwatchedBanner.Margin = new System.Windows.Forms.Padding(0);
			this.OC_UnwatchedBanner.Name = "OC_UnwatchedBanner";
			this.OC_UnwatchedBanner.OptionList = new string[0];
			this.OC_UnwatchedBanner.OptionType = TVShowsCalendar.OptionType.Checkbox;
			this.OC_UnwatchedBanner.SelectedOption = "";
			this.OC_UnwatchedBanner.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_UnwatchedBanner.Size = new System.Drawing.Size(600, 62);
			this.OC_UnwatchedBanner.TabIndex = 19;
			this.OC_UnwatchedBanner.Text_CheckBox_Checked = "Show Banner";
			this.OC_UnwatchedBanner.Text_CheckBox_Unchecked = "Hide Banner";
			this.OC_UnwatchedBanner.Text_Info = "Choose to show a banner on episodes when they haven\'t been watched yet.";
			this.OC_UnwatchedBanner.Text_Title = "Un-Watched Banner";
			// 
			// OC_FinaleWarning
			// 
			this.OC_FinaleWarning.Checked = true;
			this.OC_FinaleWarning.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_FinaleWarning.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_FinaleWarning.Location = new System.Drawing.Point(0, 62);
			this.OC_FinaleWarning.Margin = new System.Windows.Forms.Padding(0);
			this.OC_FinaleWarning.Name = "OC_FinaleWarning";
			this.OC_FinaleWarning.OptionList = new string[0];
			this.OC_FinaleWarning.OptionType = TVShowsCalendar.OptionType.Checkbox;
			this.OC_FinaleWarning.SelectedOption = "";
			this.OC_FinaleWarning.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_FinaleWarning.Size = new System.Drawing.Size(600, 62);
			this.OC_FinaleWarning.TabIndex = 20;
			this.OC_FinaleWarning.Text_CheckBox_Checked = "Warn";
			this.OC_FinaleWarning.Text_CheckBox_Unchecked = "Do not Warn";
			this.OC_FinaleWarning.Text_Info = "Get warned when you start up the finale episode of a season.";
			this.OC_FinaleWarning.Text_Title = "Finale Warning";
			// 
			// OC_EpNotification
			// 
			this.OC_EpNotification.Checked = true;
			this.OC_EpNotification.Dock = System.Windows.Forms.DockStyle.Top;
			this.OC_EpNotification.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OC_EpNotification.Location = new System.Drawing.Point(0, 0);
			this.OC_EpNotification.Margin = new System.Windows.Forms.Padding(0);
			this.OC_EpNotification.Name = "OC_EpNotification";
			this.OC_EpNotification.OptionList = new string[0];
			this.OC_EpNotification.OptionType = TVShowsCalendar.OptionType.Checkbox;
			this.OC_EpNotification.SelectedOption = "";
			this.OC_EpNotification.SettingType = TVShowsCalendar.Classes.Setting.App;
			this.OC_EpNotification.Size = new System.Drawing.Size(600, 62);
			this.OC_EpNotification.TabIndex = 17;
			this.OC_EpNotification.Text_CheckBox_Checked = "On Episode Air";
			this.OC_EpNotification.Text_CheckBox_Unchecked = "On Download";
			this.OC_EpNotification.Text_Info = "Choose to be notified when an Episode Airs or when Downloads are available.";
			this.OC_EpNotification.Text_Title = "Episode Notification";
			// 
			// P_Tiles
			// 
			this.P_Tiles.Controls.Add(this.T_Watch);
			this.P_Tiles.Controls.Add(this.T_Downloads);
			this.P_Tiles.Controls.Add(this.T_App);
			this.P_Tiles.Controls.Add(this.T_General);
			this.P_Tiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Tiles.Location = new System.Drawing.Point(0, 0);
			this.P_Tiles.Margin = new System.Windows.Forms.Padding(0);
			this.P_Tiles.Name = "P_Tiles";
			this.P_Tiles.Size = new System.Drawing.Size(175, 1014);
			this.P_Tiles.TabIndex = 35;
			// 
			// T_Watch
			// 
			this.T_Watch.Dock = System.Windows.Forms.DockStyle.Top;
			this.T_Watch.DrawLeft = false;
			this.T_Watch.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.T_Watch.IconSize = 14;
			this.T_Watch.Image = global::TVShowsCalendar.Properties.Resources.ArrowRight;
			this.T_Watch.Location = new System.Drawing.Point(0, 135);
			this.T_Watch.Name = "T_Watch";
			this.T_Watch.Padding = new System.Windows.Forms.Padding(10);
			this.T_Watch.Size = new System.Drawing.Size(175, 45);
			this.T_Watch.TabIndex = 40;
			this.T_Watch.TabStop = false;
			this.T_Watch.Text = "Video Player";
			this.T_Watch.Click += new System.EventHandler(this.Tile_Click);
			// 
			// T_Downloads
			// 
			this.T_Downloads.Dock = System.Windows.Forms.DockStyle.Top;
			this.T_Downloads.DrawLeft = false;
			this.T_Downloads.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.T_Downloads.IconSize = 14;
			this.T_Downloads.Image = global::TVShowsCalendar.Properties.Resources.ArrowRight;
			this.T_Downloads.Location = new System.Drawing.Point(0, 90);
			this.T_Downloads.Name = "T_Downloads";
			this.T_Downloads.Padding = new System.Windows.Forms.Padding(10);
			this.T_Downloads.Size = new System.Drawing.Size(175, 45);
			this.T_Downloads.TabIndex = 39;
			this.T_Downloads.TabStop = false;
			this.T_Downloads.Text = "Downloads";
			this.T_Downloads.Click += new System.EventHandler(this.Tile_Click);
			// 
			// T_App
			// 
			this.T_App.Dock = System.Windows.Forms.DockStyle.Top;
			this.T_App.DrawLeft = false;
			this.T_App.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.T_App.IconSize = 14;
			this.T_App.Image = global::TVShowsCalendar.Properties.Resources.ArrowRight;
			this.T_App.Location = new System.Drawing.Point(0, 45);
			this.T_App.Name = "T_App";
			this.T_App.Padding = new System.Windows.Forms.Padding(10);
			this.T_App.Size = new System.Drawing.Size(175, 45);
			this.T_App.TabIndex = 38;
			this.T_App.TabStop = false;
			this.T_App.Text = "Application";
			this.T_App.Click += new System.EventHandler(this.Tile_Click);
			// 
			// T_General
			// 
			this.T_General.Dock = System.Windows.Forms.DockStyle.Top;
			this.T_General.DrawLeft = false;
			this.T_General.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.T_General.IconSize = 14;
			this.T_General.Image = global::TVShowsCalendar.Properties.Resources.ArrowRight;
			this.T_General.Location = new System.Drawing.Point(0, 0);
			this.T_General.Name = "T_General";
			this.T_General.Padding = new System.Windows.Forms.Padding(10);
			this.T_General.Selected = true;
			this.T_General.Size = new System.Drawing.Size(175, 45);
			this.T_General.TabIndex = 37;
			this.T_General.TabStop = false;
			this.T_General.Text = "General";
			this.T_General.Click += new System.EventHandler(this.Tile_Click);
			// 
			// P_Spacer
			// 
			this.P_Spacer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Spacer.Location = new System.Drawing.Point(175, 0);
			this.P_Spacer.Margin = new System.Windows.Forms.Padding(0);
			this.P_Spacer.Name = "P_Spacer";
			this.P_Spacer.Size = new System.Drawing.Size(1, 1014);
			this.P_Spacer.TabIndex = 36;
			this.P_Spacer.Paint += new System.Windows.Forms.PaintEventHandler(this.P_Spacer_Paint);
			// 
			// SettingsForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(800, 1100);
			this.CloseForm = false;
			this.FormIcon = global::TVShowsCalendar.Properties.Resources.TVCalendar;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(6);
			this.MinimumSize = new System.Drawing.Size(700, 375);
			this.Name = "SettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.ResizeEnd += new System.EventHandler(this.SettingsForm_ResizeEnd);
			this.Resize += new System.EventHandler(this.SettingsForm_Resize);
			this.base_P_Content.ResumeLayout(false);
			this.TLP_Main.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.B_Done)).EndInit();
			this.P_OptionContainer.ResumeLayout(false);
			this.P_OptionContainer.PerformLayout();
			this.P_Options.ResumeLayout(false);
			this.P_Options.PerformLayout();
			this.P_Watch.ResumeLayout(false);
			this.P_Download.ResumeLayout(false);
			this.P_App.ResumeLayout(false);
			this.P_General.ResumeLayout(false);
			this.P_Tiles.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private SlickControls.Controls.SlickButton B_Done;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.Panel P_Options;
		private OptionControl OC_LaunchWithWindows;
		private OptionControl OC_StartMode;
		private OptionControl OC_EpBehavior;
		private OptionControl OC_DownloadBehavior;
		private OptionControl OC_DownloadOption;
		private OptionControl OC_Quality;
		private System.Windows.Forms.Panel P_OptionContainer;
		private SlickControls.Controls.VerticalScroll verticalScroll1;
		private OptionControl OC_EpNotification;
		private OptionControl OC_NotificationSound;
		private OptionControl OC_UnwatchedBanner;
		private OptionControl OC_FinaleWarning;
		private System.Windows.Forms.Panel P_Tiles;
		private SlickControls.Controls.SlickTile T_General;
		private SlickControls.Controls.SlickTile T_Downloads;
		private SlickControls.Controls.SlickTile T_App;
		private SlickControls.Controls.DBPanel P_Spacer;
		private System.Windows.Forms.Panel P_General;
		private System.Windows.Forms.Panel P_Download;
		private System.Windows.Forms.Panel P_App;
		private System.Windows.Forms.Panel P_Watch;
		private OptionControl OC_BackwardTime;
		private OptionControl OC_ForwardTime;
		private SlickControls.Controls.SlickTile T_Watch;
		private OptionControl OC_FullScreenPlayer;
		private OptionControl OC_MoviesRefreshDays;
		private OptionControl OC_ShowsRefreshDays;
	}
}

