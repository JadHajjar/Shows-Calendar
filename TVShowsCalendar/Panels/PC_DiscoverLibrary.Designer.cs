namespace ShowsCalendar
{
	partial class PC_DiscoverLibrary
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
			this.TLP_Tabs = new System.Windows.Forms.TableLayoutPanel();
			this.T_Movies = new SlickControls.SlickTab();
			this.T_Shows = new SlickControls.SlickTab();
			this.P_OptionContainer = new System.Windows.Forms.Panel();
			this.verticalScroll1 = new SlickControls.SlickScroll();
			this.P_Stuff = new System.Windows.Forms.Panel();
			this.TLP_Movies = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_MovieResults = new System.Windows.Forms.FlowLayoutPanel();
			this.TLP_Shows = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_ShowResults = new System.Windows.Forms.FlowLayoutPanel();
			this.PB_Loader = new SlickControls.SlickPictureBox();
			this.TLP_Tabs.SuspendLayout();
			this.P_OptionContainer.SuspendLayout();
			this.P_Stuff.SuspendLayout();
			this.TLP_Movies.SuspendLayout();
			this.TLP_Shows.SuspendLayout();

			this.SuspendLayout();
			// 
			// TLP_Tabs
			// 
			this.TLP_Tabs.ColumnCount = 4;
			this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Tabs.Controls.Add(this.T_Movies, 2, 0);
			this.TLP_Tabs.Controls.Add(this.T_Shows, 1, 0);
			this.TLP_Tabs.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Tabs.Location = new System.Drawing.Point(0, 2);
			this.TLP_Tabs.Name = "TLP_Tabs";
			this.TLP_Tabs.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.TLP_Tabs.RowCount = 1;
			this.TLP_Tabs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Tabs.Size = new System.Drawing.Size(783, 30);
			this.TLP_Tabs.TabIndex = 20;
			// 
			// T_Movies
			// 
			this.T_Movies.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Movies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.T_Movies.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.T_Movies.Hovered = false;
			this.T_Movies.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Movie;
			this.T_Movies.Location = new System.Drawing.Point(391, 6);
			this.T_Movies.Margin = new System.Windows.Forms.Padding(0);
			this.T_Movies.Name = "T_Movies";
			this.T_Movies.Selected = false;
			this.T_Movies.Size = new System.Drawing.Size(150, 24);
			this.T_Movies.TabIndex = 3;
			this.T_Movies.Text = "Movies";
			this.T_Movies.TabSelected += new System.EventHandler(this.T_Movies_TabSelected);
			// 
			// T_Shows
			// 
			this.T_Shows.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Shows.Dock = System.Windows.Forms.DockStyle.Fill;
			this.T_Shows.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.T_Shows.Hovered = false;
			this.T_Shows.Icon = global::ShowsCalendar.Properties.Resources.Tiny_TV;
			this.T_Shows.Location = new System.Drawing.Point(241, 6);
			this.T_Shows.Margin = new System.Windows.Forms.Padding(0);
			this.T_Shows.Name = "T_Shows";
			this.T_Shows.Selected = true;
			this.T_Shows.Size = new System.Drawing.Size(150, 24);
			this.T_Shows.TabIndex = 0;
			this.T_Shows.Text = "TV Shows";
			this.T_Shows.TabSelected += new System.EventHandler(this.T_Shows_TabSelected);
			// 
			// P_OptionContainer
			// 
			this.P_OptionContainer.Controls.Add(this.verticalScroll1);
			this.P_OptionContainer.Controls.Add(this.P_Stuff);
			this.P_OptionContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_OptionContainer.Location = new System.Drawing.Point(0, 32);
			this.P_OptionContainer.Margin = new System.Windows.Forms.Padding(0);
			this.P_OptionContainer.Name = "P_OptionContainer";
			this.P_OptionContainer.Size = new System.Drawing.Size(783, 406);
			this.P_OptionContainer.TabIndex = 35;
			// 
			// verticalScroll1
			// 
			this.verticalScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll1.LinkedControl = this.P_Stuff;
			this.verticalScroll1.Location = new System.Drawing.Point(778, 0);
			this.verticalScroll1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll1.Name = "verticalScroll1";
			this.verticalScroll1.Size = new System.Drawing.Size(5, 406);
			this.verticalScroll1.AutoSizeSource = true;
			this.verticalScroll1.Style = SlickControls.StyleType.Vertical;
			this.verticalScroll1.TabIndex = 35;
			// 
			// P_Stuff
			// 
			this.P_Stuff.AutoSize = true;
			this.P_Stuff.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Stuff.Controls.Add(this.TLP_Movies);
			this.P_Stuff.Controls.Add(this.TLP_Shows);
			this.P_Stuff.Location = new System.Drawing.Point(0, 0);
			this.P_Stuff.MinimumSize = new System.Drawing.Size(600, 200);
			this.P_Stuff.Name = "P_Stuff";
			this.P_Stuff.Size = new System.Drawing.Size(600, 200);
			this.P_Stuff.TabIndex = 34;
			// 
			// TLP_Movies
			// 
			this.TLP_Movies.AutoSize = true;
			this.TLP_Movies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Movies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Movies.Controls.Add(this.FLP_MovieResults, 0, 0);
			this.TLP_Movies.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Movies.Location = new System.Drawing.Point(0, 6);
			this.TLP_Movies.Name = "TLP_Movies";
			this.TLP_Movies.RowCount = 1;
			this.TLP_Movies.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Movies.Size = new System.Drawing.Size(600, 6);
			this.TLP_Movies.TabIndex = 1;
			this.TLP_Movies.Visible = false;
			// 
			// FLP_MovieResults
			// 
			this.FLP_MovieResults.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.FLP_MovieResults.AutoSize = true;
			this.FLP_MovieResults.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_MovieResults.Location = new System.Drawing.Point(300, 3);
			this.FLP_MovieResults.Name = "FLP_MovieResults";
			this.FLP_MovieResults.Size = new System.Drawing.Size(0, 0);
			this.FLP_MovieResults.TabIndex = 14;
			// 
			// TLP_Shows
			// 
			this.TLP_Shows.AutoSize = true;
			this.TLP_Shows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Shows.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Shows.Controls.Add(this.FLP_ShowResults, 0, 0);
			this.TLP_Shows.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Shows.Location = new System.Drawing.Point(0, 0);
			this.TLP_Shows.Name = "TLP_Shows";
			this.TLP_Shows.RowCount = 1;
			this.TLP_Shows.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Shows.Size = new System.Drawing.Size(600, 6);
			this.TLP_Shows.TabIndex = 0;
			// 
			// FLP_ShowResults
			// 
			this.FLP_ShowResults.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.FLP_ShowResults.AutoSize = true;
			this.FLP_ShowResults.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_ShowResults.Location = new System.Drawing.Point(300, 3);
			this.FLP_ShowResults.Name = "FLP_ShowResults";
			this.FLP_ShowResults.Size = new System.Drawing.Size(0, 0);
			this.FLP_ShowResults.TabIndex = 14;
			// 
			// PB_Loader
			// 
			this.PB_Loader.Location = new System.Drawing.Point(132, 4);
			this.PB_Loader.Name = "PB_Loader";
			this.PB_Loader.Size = new System.Drawing.Size(25, 25);
			this.PB_Loader.TabIndex = 36;
			this.PB_Loader.Loading = true;
			// 
			// PC_DiscoverLibrary
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.PB_Loader);
			this.Controls.Add(this.P_OptionContainer);
			this.Controls.Add(this.TLP_Tabs);
			this.Name = "PC_DiscoverLibrary";
			this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.Text = "Discover Library";
			this.Resize += new System.EventHandler(this.PC_DiscoverLibrary_Resize);
			this.Controls.SetChildIndex(this.TLP_Tabs, 0);
			this.Controls.SetChildIndex(this.P_OptionContainer, 0);
			this.Controls.SetChildIndex(this.PB_Loader, 0);
			this.TLP_Tabs.ResumeLayout(false);
			this.P_OptionContainer.ResumeLayout(false);
			this.P_OptionContainer.PerformLayout();
			this.P_Stuff.ResumeLayout(false);
			this.P_Stuff.PerformLayout();
			this.TLP_Movies.ResumeLayout(false);
			this.TLP_Movies.PerformLayout();
			this.TLP_Shows.ResumeLayout(false);
			this.TLP_Shows.PerformLayout();

			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TableLayoutPanel TLP_Tabs;
		private SlickControls.SlickTab T_Movies;
		private SlickControls.SlickTab T_Shows;
		private System.Windows.Forms.Panel P_OptionContainer;
		private SlickControls.SlickScroll verticalScroll1;
		private System.Windows.Forms.Panel P_Stuff;
		private System.Windows.Forms.TableLayoutPanel TLP_Movies;
		private System.Windows.Forms.TableLayoutPanel TLP_Shows;
		private System.Windows.Forms.FlowLayoutPanel FLP_MovieResults;
		private System.Windows.Forms.FlowLayoutPanel FLP_ShowResults;
		private SlickControls.SlickPictureBox PB_Loader;
	}
}
