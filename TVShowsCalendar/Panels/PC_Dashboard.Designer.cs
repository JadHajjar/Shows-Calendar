namespace ShowsCalendar
{
	partial class PC_Dashboard
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
				LocalShowHandler.WatchInfoChanged -= ManageShowChange;
				LocalMovieHandler.WatchInfoChanged -= ManageMovieChange;
				ShowManager.ShowAdded -= ManageShow;
				MovieManager.MovieAdded -= ManageMovie;

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
			this.panel1 = new System.Windows.Forms.Panel();
			this.PB_FirstLoad = new SlickControls.SlickPictureBox();
			this.P_Tabs = new System.Windows.Forms.Panel();
			this.SP_SimilarContent = new SlickControls.SlickSectionPanel();
			this.SP_Curation = new SlickControls.SlickSectionPanel();
			this.SP_UpcomingMovies = new SlickControls.SlickSectionPanel();
			this.SP_UpcomingEps = new SlickControls.SlickSectionPanel();
			this.SP_RecentMovies = new SlickControls.SlickSectionPanel();
			this.SP_RecentEps = new SlickControls.SlickSectionPanel();
			this.SP_ContinueWatching = new SlickControls.SlickSectionPanel();
			this.SP_OnDeck = new SlickControls.SlickSectionPanel();
			this.verticalScroll = new SlickControls.SlickScroll();
			this.spacer = new SlickControls.SlickSpacer();
			this.panel1.SuspendLayout();

			this.P_Tabs.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(85, 26);
			this.base_Text.Text = "Dashboard";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.PB_FirstLoad);
			this.panel1.Controls.Add(this.P_Tabs);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(5, 31);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(775, 438);
			this.panel1.TabIndex = 15;
			// 
			// PB_FirstLoad
			// 
			this.PB_FirstLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.PB_FirstLoad.Image = null;
			this.PB_FirstLoad.Loading = true;
			this.PB_FirstLoad.Location = new System.Drawing.Point(363, 195);
			this.PB_FirstLoad.Name = "PB_FirstLoad";
			this.PB_FirstLoad.Size = new System.Drawing.Size(48, 48);
			this.PB_FirstLoad.TabIndex = 33;
			this.PB_FirstLoad.TabStop = false;
			// 
			// P_Tabs
			// 
			this.P_Tabs.AutoSize = true;
			this.P_Tabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tabs.Controls.Add(this.SP_SimilarContent);
			this.P_Tabs.Controls.Add(this.SP_Curation);
			this.P_Tabs.Controls.Add(this.SP_UpcomingMovies);
			this.P_Tabs.Controls.Add(this.SP_UpcomingEps);
			this.P_Tabs.Controls.Add(this.SP_RecentMovies);
			this.P_Tabs.Controls.Add(this.SP_RecentEps);
			this.P_Tabs.Controls.Add(this.SP_ContinueWatching);
			this.P_Tabs.Controls.Add(this.SP_OnDeck);
			this.P_Tabs.Location = new System.Drawing.Point(0, 0);
			this.P_Tabs.MinimumSize = new System.Drawing.Size(500, 0);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(500, 432);
			this.P_Tabs.TabIndex = 12;
			// 
			// SP_SimilarContent
			// 
			this.SP_SimilarContent.Active = false;
			this.SP_SimilarContent.AutoHide = true;
			this.SP_SimilarContent.AutoSize = true;
			this.SP_SimilarContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_SimilarContent.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_SimilarContent.Flavor = null;
			this.SP_SimilarContent.Icon = global::ShowsCalendar.Properties.Resources.Big_Similar;
			this.SP_SimilarContent.Info = "Find content usually suggested with shows & movies from your library";
			this.SP_SimilarContent.Location = new System.Drawing.Point(0, 378);
			this.SP_SimilarContent.MaximumSize = new System.Drawing.Size(500, 2147483647);
			this.SP_SimilarContent.Name = "SP_SimilarContent";
			this.SP_SimilarContent.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_SimilarContent.Size = new System.Drawing.Size(500, 54);
			this.SP_SimilarContent.TabIndex = 7;
			this.SP_SimilarContent.Text = "Expand your Library";
			// 
			// SP_Curation
			// 
			this.SP_Curation.Active = false;
			this.SP_Curation.AutoHide = true;
			this.SP_Curation.AutoSize = true;
			this.SP_Curation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Curation.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Curation.Flavor = null;
			this.SP_Curation.Icon = global::ShowsCalendar.Properties.Resources.Big_Rating;
			this.SP_Curation.Info = "Movies & Shows we think you should watch";
			this.SP_Curation.Location = new System.Drawing.Point(0, 324);
			this.SP_Curation.MaximumSize = new System.Drawing.Size(500, 2147483647);
			this.SP_Curation.Name = "SP_Curation";
			this.SP_Curation.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Curation.Size = new System.Drawing.Size(500, 54);
			this.SP_Curation.TabIndex = 6;
			this.SP_Curation.Text = "Suggested Content";
			// 
			// SP_UpcomingMovies
			// 
			this.SP_UpcomingMovies.Active = false;
			this.SP_UpcomingMovies.AutoHide = true;
			this.SP_UpcomingMovies.AutoSize = true;
			this.SP_UpcomingMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_UpcomingMovies.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_UpcomingMovies.Flavor = null;
			this.SP_UpcomingMovies.Icon = global::ShowsCalendar.Properties.Resources.Big_Movie;
			this.SP_UpcomingMovies.Info = "Movies with a definitive release date";
			this.SP_UpcomingMovies.Location = new System.Drawing.Point(0, 270);
			this.SP_UpcomingMovies.MaximumSize = new System.Drawing.Size(500, 2147483647);
			this.SP_UpcomingMovies.Name = "SP_UpcomingMovies";
			this.SP_UpcomingMovies.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_UpcomingMovies.Size = new System.Drawing.Size(500, 54);
			this.SP_UpcomingMovies.TabIndex = 5;
			this.SP_UpcomingMovies.Text = "Upcoming Movies";
			// 
			// SP_UpcomingEps
			// 
			this.SP_UpcomingEps.Active = false;
			this.SP_UpcomingEps.AutoHide = true;
			this.SP_UpcomingEps.AutoSize = true;
			this.SP_UpcomingEps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_UpcomingEps.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_UpcomingEps.Flavor = null;
			this.SP_UpcomingEps.Icon = global::ShowsCalendar.Properties.Resources.Big_TV;
			this.SP_UpcomingEps.Info = "Episodes to look forward to within the next month";
			this.SP_UpcomingEps.Location = new System.Drawing.Point(0, 216);
			this.SP_UpcomingEps.MaximumSize = new System.Drawing.Size(500, 2147483647);
			this.SP_UpcomingEps.Name = "SP_UpcomingEps";
			this.SP_UpcomingEps.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_UpcomingEps.Size = new System.Drawing.Size(500, 54);
			this.SP_UpcomingEps.TabIndex = 4;
			this.SP_UpcomingEps.Text = "Upcoming Episodes";
			// 
			// SP_RecentMovies
			// 
			this.SP_RecentMovies.Active = false;
			this.SP_RecentMovies.AutoHide = true;
			this.SP_RecentMovies.AutoSize = true;
			this.SP_RecentMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_RecentMovies.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_RecentMovies.Flavor = null;
			this.SP_RecentMovies.Icon = global::ShowsCalendar.Properties.Resources.Big_Popcorn;
			this.SP_RecentMovies.Info = "Movies that were released not so long ago";
			this.SP_RecentMovies.Location = new System.Drawing.Point(0, 162);
			this.SP_RecentMovies.MaximumSize = new System.Drawing.Size(500, 2147483647);
			this.SP_RecentMovies.Name = "SP_RecentMovies";
			this.SP_RecentMovies.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_RecentMovies.Size = new System.Drawing.Size(500, 54);
			this.SP_RecentMovies.TabIndex = 3;
			this.SP_RecentMovies.Text = "Recently Premiered";
			// 
			// SP_RecentEps
			// 
			this.SP_RecentEps.Active = false;
			this.SP_RecentEps.AutoHide = true;
			this.SP_RecentEps.AutoSize = true;
			this.SP_RecentEps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_RecentEps.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_RecentEps.Flavor = new string[] {
        "Episodes that came out in the past week",
        "Episodes straight out of last week",
        "Freshest episodes around"};
			this.SP_RecentEps.Icon = global::ShowsCalendar.Properties.Resources.Big_Airing;
			this.SP_RecentEps.Info = "Freshest episodes around";
			this.SP_RecentEps.Location = new System.Drawing.Point(0, 108);
			this.SP_RecentEps.MaximumSize = new System.Drawing.Size(500, 2147483647);
			this.SP_RecentEps.Name = "SP_RecentEps";
			this.SP_RecentEps.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_RecentEps.Size = new System.Drawing.Size(500, 54);
			this.SP_RecentEps.TabIndex = 2;
			this.SP_RecentEps.Text = "Recently Aired";
			// 
			// SP_ContinueWatching
			// 
			this.SP_ContinueWatching.Active = false;
			this.SP_ContinueWatching.AutoHide = true;
			this.SP_ContinueWatching.AutoSize = true;
			this.SP_ContinueWatching.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_ContinueWatching.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_ContinueWatching.Flavor = new string[] {
        "Episodes you should download",
        "Download the episodes and continue the binge!",
        "Ready to put on the Deck"};
			this.SP_ContinueWatching.Icon = global::ShowsCalendar.Properties.Resources.Big_ViewPlay;
			this.SP_ContinueWatching.Info = "Download the episodes and continue the binge!";
			this.SP_ContinueWatching.Location = new System.Drawing.Point(0, 54);
			this.SP_ContinueWatching.MaximumSize = new System.Drawing.Size(500, 2147483647);
			this.SP_ContinueWatching.Name = "SP_ContinueWatching";
			this.SP_ContinueWatching.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_ContinueWatching.Size = new System.Drawing.Size(500, 54);
			this.SP_ContinueWatching.TabIndex = 1;
			this.SP_ContinueWatching.Text = "Continue Watching";
			// 
			// SP_OnDeck
			// 
			this.SP_OnDeck.Active = true;
			this.SP_OnDeck.AutoHide = true;
			this.SP_OnDeck.AutoSize = true;
			this.SP_OnDeck.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_OnDeck.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_OnDeck.Flavor = new string[] {
        "Where were we?",
        "Must. Binge. More.",
        "Yes, those are the things you were watching.",
        "Gotcha covered, buddy!",
        "The temptation is small, but your will is weak, press on one."};
			this.SP_OnDeck.Icon = global::ShowsCalendar.Properties.Resources.Big_Play;
			this.SP_OnDeck.Info = "Where were we?";
			this.SP_OnDeck.Location = new System.Drawing.Point(0, 0);
			this.SP_OnDeck.MaximumSize = new System.Drawing.Size(500, 2147483647);
			this.SP_OnDeck.Name = "SP_OnDeck";
			this.SP_OnDeck.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_OnDeck.Size = new System.Drawing.Size(500, 54);
			this.SP_OnDeck.TabIndex = 0;
			this.SP_OnDeck.Text = "On-Deck";
			// 
			// verticalScroll
			// 
			this.verticalScroll.AutoSizeSource = true;
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = this.P_Tabs;
			this.verticalScroll.Location = new System.Drawing.Point(774, 31);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.MouseDownLocation = new System.Drawing.Point(0, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(6, 438);
			this.verticalScroll.Style = SlickControls.StyleType.Vertical;
			this.verticalScroll.TabIndex = 16;
			this.verticalScroll.TabStop = false;
			this.verticalScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.verticalScroll_Scroll);
			// 
			// spacer
			// 
			this.spacer.Dock = System.Windows.Forms.DockStyle.Top;
			this.spacer.Location = new System.Drawing.Point(5, 30);
			this.spacer.Name = "spacer";
			this.spacer.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
			this.spacer.Size = new System.Drawing.Size(775, 1);
			this.spacer.TabIndex = 17;
			this.spacer.TabStop = false;
			// 
			// PC_Dashboard
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.spacer);
			this.Name = "PC_Dashboard";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Size = new System.Drawing.Size(780, 469);
			this.Text = "Dashboard";
			this.Load += new System.EventHandler(this.PC_Dashboard_Load);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.spacer, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.verticalScroll, 0);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();

			this.P_Tabs.ResumeLayout(false);
			this.P_Tabs.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel P_Tabs;
		private SlickControls.SlickScroll verticalScroll;
		private SlickControls.SlickSectionPanel SP_UpcomingMovies;
		private SlickControls.SlickSectionPanel SP_UpcomingEps;
		private SlickControls.SlickSectionPanel SP_RecentMovies;
		private SlickControls.SlickSectionPanel SP_RecentEps;
		private SlickControls.SlickSectionPanel SP_OnDeck;
		private SlickControls.SlickSectionPanel SP_ContinueWatching;
		private SlickControls.SlickSpacer spacer;
		private SlickControls.SlickSectionPanel SP_Curation;
		private SlickControls.SlickSectionPanel SP_SimilarContent;
		private SlickControls.SlickPictureBox PB_FirstLoad;
	}
}
