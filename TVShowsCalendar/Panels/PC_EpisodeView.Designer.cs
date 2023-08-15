namespace ShowsCalendar
{
	partial class PC_EpisodeView
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
				Episode.ContentRemoved -= episode_ContentRemoved;
				Episode.InfoChanged -= episode_InfoChanged;
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
			this.P_Cast = new System.Windows.Forms.Panel();
			this.TLP_SimilarContent = new System.Windows.Forms.TableLayoutPanel();
			this.SP_Similar = new SlickControls.SlickSectionPanel();
			this.SP_Cast = new SlickControls.SlickSectionPanel();
			this.FLP_Videos = new System.Windows.Forms.FlowLayoutPanel();
			this.FLP_VidFiles = new System.Windows.Forms.FlowLayoutPanel();
			this.FLP_Images = new System.Windows.Forms.FlowLayoutPanel();
			this.P_Crew = new System.Windows.Forms.Panel();
			this.P_Cast.SuspendLayout();
			this.SuspendLayout();
			// 
			// P_Cast
			// 
			this.P_Cast.AutoSize = true;
			this.P_Cast.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Cast.Controls.Add(this.TLP_SimilarContent);
			this.P_Cast.Controls.Add(this.SP_Similar);
			this.P_Cast.Controls.Add(this.SP_Cast);
			this.P_Cast.Location = new System.Drawing.Point(0, 0);
			this.P_Cast.MaximumSize = new System.Drawing.Size(775, 2147483647);
			this.P_Cast.MinimumSize = new System.Drawing.Size(775, 0);
			this.P_Cast.Name = "P_Cast";
			this.P_Cast.Size = new System.Drawing.Size(775, 158);
			this.P_Cast.TabIndex = 2;
			// 
			// TLP_SimilarContent
			// 
			this.TLP_SimilarContent.AutoSize = true;
			this.TLP_SimilarContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_SimilarContent.ColumnCount = 2;
			this.TLP_SimilarContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_SimilarContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_SimilarContent.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_SimilarContent.Location = new System.Drawing.Point(0, 108);
			this.TLP_SimilarContent.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_SimilarContent.MinimumSize = new System.Drawing.Size(800, 50);
			this.TLP_SimilarContent.Name = "TLP_SimilarContent";
			this.TLP_SimilarContent.Padding = new System.Windows.Forms.Padding(43, 0, 0, 0);
			this.TLP_SimilarContent.RowCount = 1;
			this.TLP_SimilarContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_SimilarContent.Size = new System.Drawing.Size(800, 50);
			this.TLP_SimilarContent.TabIndex = 25;
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
			this.SP_Similar.Location = new System.Drawing.Point(0, 54);
			this.SP_Similar.MaximumSize = new System.Drawing.Size(775, 2147483647);
			this.SP_Similar.Name = "SP_Similar";
			this.SP_Similar.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Similar.Size = new System.Drawing.Size(775, 54);
			this.SP_Similar.TabIndex = 24;
			this.SP_Similar.Text = "Where have I seen them?";
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
			this.SP_Cast.MaximumSize = new System.Drawing.Size(775, 2147483647);
			this.SP_Cast.Name = "SP_Cast";
			this.SP_Cast.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Cast.Size = new System.Drawing.Size(775, 54);
			this.SP_Cast.TabIndex = 23;
			this.SP_Cast.Text = "Who\'s in this?";
			// 
			// FLP_Videos
			// 
			this.FLP_Videos.AutoSize = true;
			this.FLP_Videos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Videos.Location = new System.Drawing.Point(0, 0);
			this.FLP_Videos.MaximumSize = new System.Drawing.Size(775, 2147483647);
			this.FLP_Videos.MinimumSize = new System.Drawing.Size(775, 0);
			this.FLP_Videos.Name = "FLP_Videos";
			this.FLP_Videos.Size = new System.Drawing.Size(775, 0);
			this.FLP_Videos.TabIndex = 20;
			// 
			// FLP_VidFiles
			// 
			this.FLP_VidFiles.AutoSize = true;
			this.FLP_VidFiles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_VidFiles.Location = new System.Drawing.Point(0, 0);
			this.FLP_VidFiles.MaximumSize = new System.Drawing.Size(783, 0);
			this.FLP_VidFiles.MinimumSize = new System.Drawing.Size(783, 0);
			this.FLP_VidFiles.Name = "FLP_VidFiles";
			this.FLP_VidFiles.Size = new System.Drawing.Size(783, 0);
			this.FLP_VidFiles.TabIndex = 22;
			// 
			// FLP_Images
			// 
			this.FLP_Images.AutoSize = true;
			this.FLP_Images.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Images.Location = new System.Drawing.Point(0, 0);
			this.FLP_Images.MaximumSize = new System.Drawing.Size(775, 2147483647);
			this.FLP_Images.MinimumSize = new System.Drawing.Size(775, 0);
			this.FLP_Images.Name = "FLP_Images";
			this.FLP_Images.Size = new System.Drawing.Size(775, 0);
			this.FLP_Images.TabIndex = 19;
			// 
			// P_Crew
			// 
			this.P_Crew.AutoSize = true;
			this.P_Crew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Crew.Location = new System.Drawing.Point(0, 0);
			this.P_Crew.MaximumSize = new System.Drawing.Size(783, 0);
			this.P_Crew.MinimumSize = new System.Drawing.Size(783, 0);
			this.P_Crew.Name = "P_Crew";
			this.P_Crew.Size = new System.Drawing.Size(783, 0);
			this.P_Crew.TabIndex = 21;
			// 
			// PC_EpisodeView
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Name = "PC_EpisodeView";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.HideWindowIcons = true;
			this.P_Cast.ResumeLayout(false);
			this.P_Cast.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel P_Cast;
		private System.Windows.Forms.TableLayoutPanel TLP_SimilarContent;
		private SlickControls.SlickSectionPanel SP_Similar;
		private SlickControls.SlickSectionPanel SP_Cast;
		private System.Windows.Forms.FlowLayoutPanel FLP_Images;
		private System.Windows.Forms.FlowLayoutPanel FLP_Videos;
		private System.Windows.Forms.Panel P_Crew;
		private System.Windows.Forms.FlowLayoutPanel FLP_VidFiles;
	}
}
