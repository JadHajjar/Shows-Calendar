namespace ShowsCalendar
{
	partial class PC_ShowPage
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
				LinkedShow.InfoChanged -= linkedShow_ShowLoaded;
				LinkedShow.ContentRemoved -= linkedMovie_ContentRemoved;
				LocalShowHandler.WatchInfoChanged -= LocalShowHandler_EpisodeWatchChanged;

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
			this.FLP_Videos = new System.Windows.Forms.Panel();
			this.FLP_Images = new System.Windows.Forms.Panel();
			this.SP_Posters = new SlickControls.SlickSectionPanel();
			this.SP_Backdrops = new SlickControls.SlickSectionPanel();
			this.FLP_Similar = new System.Windows.Forms.FlowLayoutPanel();
			this.FLP_Seasons = new System.Windows.Forms.FlowLayoutPanel();
			this.FLP_Crew = new System.Windows.Forms.Panel();
			this.SP_Cast = new SlickControls.SlickSectionPanel();
			this.SP_Similar = new SlickControls.SlickSectionPanel();
			this.TLP_SimilarContent = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_Cast = new System.Windows.Forms.Panel();
			this.L_InfoNetwork = new System.Windows.Forms.Label();
			this.L_InfoWebpage = new System.Windows.Forms.Label();
			this.L_Tags = new System.Windows.Forms.Label();
			this.L_InfoStatus = new System.Windows.Forms.Label();
			this.P_Info = new System.Windows.Forms.Panel();
			this.FLP_Images.SuspendLayout();
			this.FLP_Cast.SuspendLayout();
			this.P_Info.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(86, 26);
			this.base_Text.Text = "Show Page";
			this.base_Text.Visible = false;
			// 
			// FLP_Videos
			// 
			this.FLP_Videos.AutoSize = true;
			this.FLP_Videos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Videos.Location = new System.Drawing.Point(0, 0);
			this.FLP_Videos.MaximumSize = new System.Drawing.Size(948, 0);
			this.FLP_Videos.MinimumSize = new System.Drawing.Size(948, 0);
			this.FLP_Videos.Name = "FLP_Videos";
			this.FLP_Videos.Size = new System.Drawing.Size(948, 0);
			this.FLP_Videos.TabIndex = 23;
			// 
			// FLP_Images
			// 
			this.FLP_Images.AutoSize = true;
			this.FLP_Images.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Images.Controls.Add(this.SP_Posters);
			this.FLP_Images.Controls.Add(this.SP_Backdrops);
			this.FLP_Images.Location = new System.Drawing.Point(0, 0);
			this.FLP_Images.MaximumSize = new System.Drawing.Size(940, 2147483647);
			this.FLP_Images.MinimumSize = new System.Drawing.Size(940, 0);
			this.FLP_Images.Name = "FLP_Images";
			this.FLP_Images.Size = new System.Drawing.Size(940, 110);
			this.FLP_Images.TabIndex = 21;
			// 
			// SP_Posters
			// 
			this.SP_Posters.Active = false;
			this.SP_Posters.AutoHide = true;
			this.SP_Posters.AutoSize = true;
			this.SP_Posters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Posters.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Posters.Flavor = null;
			this.SP_Posters.Icon = global::ShowsCalendar.Properties.Resources.Big_Portrait;
			this.SP_Posters.Info = null;
			this.SP_Posters.Location = new System.Drawing.Point(0, 55);
			this.SP_Posters.MaximumSize = new System.Drawing.Size(940, 2147483647);
			this.SP_Posters.MinimumSize = new System.Drawing.Size(153, 55);
			this.SP_Posters.Name = "SP_Posters";
			this.SP_Posters.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Posters.Size = new System.Drawing.Size(940, 55);
			this.SP_Posters.TabIndex = 1;
			this.SP_Posters.Text = "Posters";
			// 
			// SP_Backdrops
			// 
			this.SP_Backdrops.Active = false;
			this.SP_Backdrops.AutoHide = true;
			this.SP_Backdrops.AutoSize = true;
			this.SP_Backdrops.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Backdrops.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Backdrops.Flavor = null;
			this.SP_Backdrops.Icon = global::ShowsCalendar.Properties.Resources.Big_Picture;
			this.SP_Backdrops.Info = null;
			this.SP_Backdrops.Location = new System.Drawing.Point(0, 0);
			this.SP_Backdrops.MaximumSize = new System.Drawing.Size(940, 2147483647);
			this.SP_Backdrops.MinimumSize = new System.Drawing.Size(179, 55);
			this.SP_Backdrops.Name = "SP_Backdrops";
			this.SP_Backdrops.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Backdrops.Size = new System.Drawing.Size(940, 55);
			this.SP_Backdrops.TabIndex = 0;
			this.SP_Backdrops.Text = "Backdrops";
			// 
			// FLP_Similar
			// 
			this.FLP_Similar.AutoSize = true;
			this.FLP_Similar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Similar.Location = new System.Drawing.Point(0, 0);
			this.FLP_Similar.MaximumSize = new System.Drawing.Size(940, 2147483647);
			this.FLP_Similar.MinimumSize = new System.Drawing.Size(940, 0);
			this.FLP_Similar.Name = "FLP_Similar";
			this.FLP_Similar.Size = new System.Drawing.Size(940, 0);
			this.FLP_Similar.TabIndex = 20;
			// 
			// FLP_Seasons
			// 
			this.FLP_Seasons.AutoSize = true;
			this.FLP_Seasons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Seasons.Location = new System.Drawing.Point(0, 0);
			this.FLP_Seasons.MaximumSize = new System.Drawing.Size(940, 2147483647);
			this.FLP_Seasons.MinimumSize = new System.Drawing.Size(940, 0);
			this.FLP_Seasons.Name = "FLP_Seasons";
			this.FLP_Seasons.Size = new System.Drawing.Size(940, 0);
			this.FLP_Seasons.TabIndex = 19;
			// 
			// FLP_Crew
			// 
			this.FLP_Crew.AutoSize = true;
			this.FLP_Crew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Crew.Location = new System.Drawing.Point(0, 0);
			this.FLP_Crew.MaximumSize = new System.Drawing.Size(940, 2147483647);
			this.FLP_Crew.MinimumSize = new System.Drawing.Size(940, 0);
			this.FLP_Crew.Name = "FLP_Crew";
			this.FLP_Crew.Size = new System.Drawing.Size(940, 0);
			this.FLP_Crew.TabIndex = 19;
			// 
			// SP_Cast
			// 
			this.SP_Cast.Active = false;
			this.SP_Cast.AutoHide = true;
			this.SP_Cast.AutoSize = true;
			this.SP_Cast.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Cast.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Cast.Flavor = null;
			this.SP_Cast.Icon = global::ShowsCalendar.Properties.Resources.Big_Cast;
			this.SP_Cast.Info = "From your beloved cast to this episode\'s notable guest stars";
			this.SP_Cast.Location = new System.Drawing.Point(0, 0);
			this.SP_Cast.MaximumSize = new System.Drawing.Size(940, 2147483647);
			this.SP_Cast.MinimumSize = new System.Drawing.Size(626, 55);
			this.SP_Cast.Name = "SP_Cast";
			this.SP_Cast.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Cast.Size = new System.Drawing.Size(940, 55);
			this.SP_Cast.TabIndex = 6;
			this.SP_Cast.Text = "Who\'s in this?";
			// 
			// SP_Similar
			// 
			this.SP_Similar.Active = false;
			this.SP_Similar.AutoHide = false;
			this.SP_Similar.AutoSize = true;
			this.SP_Similar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Similar.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Similar.Flavor = null;
			this.SP_Similar.Icon = global::ShowsCalendar.Properties.Resources.Big_Rating;
			this.SP_Similar.Info = "Recognize someone? You probably saw them here";
			this.SP_Similar.Location = new System.Drawing.Point(0, 55);
			this.SP_Similar.MaximumSize = new System.Drawing.Size(940, 2147483647);
			this.SP_Similar.MinimumSize = new System.Drawing.Size(662, 55);
			this.SP_Similar.Name = "SP_Similar";
			this.SP_Similar.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Similar.Size = new System.Drawing.Size(940, 55);
			this.SP_Similar.TabIndex = 7;
			this.SP_Similar.Text = "Where have I seen them?";
			// 
			// TLP_SimilarContent
			// 
			this.TLP_SimilarContent.AutoSize = true;
			this.TLP_SimilarContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_SimilarContent.ColumnCount = 2;
			this.TLP_SimilarContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_SimilarContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_SimilarContent.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_SimilarContent.Location = new System.Drawing.Point(0, 110);
			this.TLP_SimilarContent.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_SimilarContent.Name = "TLP_SimilarContent";
			this.TLP_SimilarContent.Padding = new System.Windows.Forms.Padding(43, 0, 0, 0);
			this.TLP_SimilarContent.RowCount = 1;
			this.TLP_SimilarContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_SimilarContent.Size = new System.Drawing.Size(940, 0);
			this.TLP_SimilarContent.TabIndex = 23;
			// 
			// FLP_Cast
			// 
			this.FLP_Cast.AutoSize = true;
			this.FLP_Cast.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Cast.Controls.Add(this.TLP_SimilarContent);
			this.FLP_Cast.Controls.Add(this.SP_Similar);
			this.FLP_Cast.Controls.Add(this.SP_Cast);
			this.FLP_Cast.Location = new System.Drawing.Point(0, 0);
			this.FLP_Cast.MaximumSize = new System.Drawing.Size(940, 2147483647);
			this.FLP_Cast.MinimumSize = new System.Drawing.Size(940, 0);
			this.FLP_Cast.Name = "FLP_Cast";
			this.FLP_Cast.Size = new System.Drawing.Size(940, 110);
			this.FLP_Cast.TabIndex = 20;
			// 
			// L_InfoNetwork
			// 
			this.L_InfoNetwork.AutoSize = true;
			this.L_InfoNetwork.Location = new System.Drawing.Point(352, 557);
			this.L_InfoNetwork.Margin = new System.Windows.Forms.Padding(15, 7, 3, 7);
			this.L_InfoNetwork.Name = "L_InfoNetwork";
			this.L_InfoNetwork.Size = new System.Drawing.Size(77, 13);
			this.L_InfoNetwork.TabIndex = 4;
			this.L_InfoNetwork.Text = "L_InfoNetwork";
			this.L_InfoNetwork.Visible = false;
			// 
			// L_InfoWebpage
			// 
			this.L_InfoWebpage.AutoSize = true;
			this.L_InfoWebpage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_InfoWebpage.Location = new System.Drawing.Point(395, 530);
			this.L_InfoWebpage.Margin = new System.Windows.Forms.Padding(15, 7, 3, 7);
			this.L_InfoWebpage.Name = "L_InfoWebpage";
			this.L_InfoWebpage.Size = new System.Drawing.Size(84, 13);
			this.L_InfoWebpage.TabIndex = 4;
			this.L_InfoWebpage.Tag = "NoMouseDown";
			this.L_InfoWebpage.Text = "L_InfoWebpage";
			this.L_InfoWebpage.Visible = false;
			// 
			// L_Tags
			// 
			this.L_Tags.AutoSize = true;
			this.L_Tags.Location = new System.Drawing.Point(512, 505);
			this.L_Tags.Margin = new System.Windows.Forms.Padding(15, 7, 3, 7);
			this.L_Tags.Name = "L_Tags";
			this.L_Tags.Size = new System.Drawing.Size(35, 13);
			this.L_Tags.TabIndex = 4;
			this.L_Tags.Text = "label1";
			this.L_Tags.Visible = false;
			// 
			// L_InfoStatus
			// 
			this.L_InfoStatus.AutoSize = true;
			this.L_InfoStatus.Location = new System.Drawing.Point(568, 517);
			this.L_InfoStatus.Margin = new System.Windows.Forms.Padding(15, 7, 3, 7);
			this.L_InfoStatus.Name = "L_InfoStatus";
			this.L_InfoStatus.Size = new System.Drawing.Size(67, 13);
			this.L_InfoStatus.TabIndex = 4;
			this.L_InfoStatus.Text = "L_InfoStatus";
			this.L_InfoStatus.Visible = false;
			// 
			// P_Info
			// 
			this.P_Info.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Info.Controls.Add(this.L_InfoStatus);
			this.P_Info.Controls.Add(this.L_Tags);
			this.P_Info.Controls.Add(this.L_InfoWebpage);
			this.P_Info.Controls.Add(this.L_InfoNetwork);
			this.P_Info.Location = new System.Drawing.Point(0, 0);
			this.P_Info.MaximumSize = new System.Drawing.Size(940, 2147483647);
			this.P_Info.MinimumSize = new System.Drawing.Size(940, 0);
			this.P_Info.Name = "P_Info";
			this.P_Info.Size = new System.Drawing.Size(940, 702);
			this.P_Info.TabIndex = 18;
			// 
			// PC_ShowPage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.HideWindowIcons = true;
			this.Name = "PC_ShowPage";
			this.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.Size = new System.Drawing.Size(948, 760);
			this.Text = "Show Page";
			this.FLP_Images.ResumeLayout(false);
			this.FLP_Images.PerformLayout();
			this.FLP_Cast.ResumeLayout(false);
			this.FLP_Cast.PerformLayout();
			this.P_Info.ResumeLayout(false);
			this.P_Info.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel FLP_Similar;
		private System.Windows.Forms.Panel FLP_Videos;
		private System.Windows.Forms.Panel FLP_Images;
		private SlickControls.SlickSectionPanel SP_Posters;
		private SlickControls.SlickSectionPanel SP_Backdrops;
		private System.Windows.Forms.FlowLayoutPanel FLP_Seasons;
		private System.Windows.Forms.Panel FLP_Crew;
		private SlickControls.SlickSectionPanel SP_Cast;
		private SlickControls.SlickSectionPanel SP_Similar;
		private System.Windows.Forms.TableLayoutPanel TLP_SimilarContent;
		private System.Windows.Forms.Panel FLP_Cast;
		private System.Windows.Forms.Label L_InfoNetwork;
		private System.Windows.Forms.Label L_InfoWebpage;
		private System.Windows.Forms.Label L_Tags;
		private System.Windows.Forms.Label L_InfoStatus;
		private System.Windows.Forms.Panel P_Info;
	}
}
