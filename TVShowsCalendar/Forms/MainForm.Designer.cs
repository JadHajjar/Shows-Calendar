namespace TVShowsCalendar
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.P_FormContent = new System.Windows.Forms.Panel();
			this.TabScroll = new SlickControls.Controls.VerticalScroll();
			this.P_Tabs = new System.Windows.Forms.Panel();
			this.P_Space_3 = new System.Windows.Forms.Panel();
			this.T_Upcoming = new TVShowsCalendar.Tab();
			this.P_Space_2 = new System.Windows.Forms.Panel();
			this.T_Missed = new TVShowsCalendar.Tab();
			this.P_Space_1 = new System.Windows.Forms.Panel();
			this.T_Today = new TVShowsCalendar.Tab();
			this.P_Space_0 = new System.Windows.Forms.Panel();
			this.T_Play = new TVShowsCalendar.Tab();
			this.L_Icons8 = new System.Windows.Forms.Label();
			this.PB_TMDb = new System.Windows.Forms.PictureBox();
			this.L_Version = new System.Windows.Forms.Label();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.TLP_Buttons = new System.Windows.Forms.TableLayoutPanel();
			this.B_Options = new SlickControls.Controls.SlickLabel();
			this.B_Shows = new SlickControls.Controls.SlickLabel();
			this.B_Movies = new SlickControls.Controls.SlickLabel();
			this.B_Watch = new SlickControls.Controls.SlickLabel();
			this.base_P_Content.SuspendLayout();
			this.base_P_Controls.SuspendLayout();
			this.P_FormContent.SuspendLayout();
			this.P_Tabs.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_TMDb)).BeginInit();
			this.TLP_Buttons.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(58)))));
			this.base_P_Content.Controls.Add(this.P_FormContent);
			this.base_P_Content.Location = new System.Drawing.Point(1, 73);
			this.base_P_Content.Size = new System.Drawing.Size(291, 409);
			// 
			// base_P_Controls
			// 
			this.base_P_Controls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(54)))));
			this.base_P_Controls.Controls.Add(this.TLP_Buttons);
			this.base_P_Controls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.base_P_Controls.Size = new System.Drawing.Size(291, 45);
			// 
			// base_P_Top_Spacer
			// 
			this.base_P_Top_Spacer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(89)))));
			this.base_P_Top_Spacer.Location = new System.Drawing.Point(1, 71);
			this.base_P_Top_Spacer.Size = new System.Drawing.Size(291, 2);
			// 
			// P_FormContent
			// 
			this.P_FormContent.Controls.Add(this.TabScroll);
			this.P_FormContent.Controls.Add(this.P_Tabs);
			this.P_FormContent.Controls.Add(this.L_Icons8);
			this.P_FormContent.Controls.Add(this.PB_TMDb);
			this.P_FormContent.Controls.Add(this.L_Version);
			this.P_FormContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_FormContent.Location = new System.Drawing.Point(0, 0);
			this.P_FormContent.Margin = new System.Windows.Forms.Padding(0);
			this.P_FormContent.Name = "P_FormContent";
			this.P_FormContent.Size = new System.Drawing.Size(291, 409);
			this.P_FormContent.TabIndex = 16;
			this.P_FormContent.Resize += new System.EventHandler(this.P_Tabs_Resize);
			// 
			// TabScroll
			// 
			this.TabScroll.BarColor = null;
			this.TabScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.TabScroll.LinkedControl = null;
			this.TabScroll.Location = new System.Drawing.Point(287, 0);
			this.TabScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.TabScroll.Name = "TabScroll";
			this.TabScroll.Size = new System.Drawing.Size(4, 409);
			this.TabScroll.TabIndex = 31;
			// 
			// P_Tabs
			// 
			this.P_Tabs.AutoSize = true;
			this.P_Tabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tabs.Controls.Add(this.P_Space_3);
			this.P_Tabs.Controls.Add(this.T_Upcoming);
			this.P_Tabs.Controls.Add(this.P_Space_2);
			this.P_Tabs.Controls.Add(this.T_Missed);
			this.P_Tabs.Controls.Add(this.P_Space_1);
			this.P_Tabs.Controls.Add(this.T_Today);
			this.P_Tabs.Controls.Add(this.P_Space_0);
			this.P_Tabs.Controls.Add(this.T_Play);
			this.P_Tabs.Location = new System.Drawing.Point(0, 0);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(0, 100);
			this.P_Tabs.TabIndex = 30;
			this.P_Tabs.Resize += new System.EventHandler(this.P_Tabs_Resize);
			// 
			// P_Space_3
			// 
			this.P_Space_3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
			this.P_Space_3.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Space_3.Location = new System.Drawing.Point(0, 97);
			this.P_Space_3.Name = "P_Space_3";
			this.P_Space_3.Size = new System.Drawing.Size(0, 3);
			this.P_Space_3.TabIndex = 23;
			// 
			// T_Upcoming
			// 
			this.T_Upcoming.Dock = System.Windows.Forms.DockStyle.Top;
			this.T_Upcoming.Location = new System.Drawing.Point(0, 75);
			this.T_Upcoming.Margin = new System.Windows.Forms.Padding(11, 8, 11, 8);
			this.T_Upcoming.Name = "T_Upcoming";
			this.T_Upcoming.ResizeControls = true;
			this.T_Upcoming.Size = new System.Drawing.Size(0, 22);
			this.T_Upcoming.TabIndex = 29;
			this.T_Upcoming.Text = "Upcoming";
			// 
			// P_Space_2
			// 
			this.P_Space_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
			this.P_Space_2.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Space_2.Location = new System.Drawing.Point(0, 72);
			this.P_Space_2.Name = "P_Space_2";
			this.P_Space_2.Size = new System.Drawing.Size(0, 3);
			this.P_Space_2.TabIndex = 18;
			// 
			// T_Missed
			// 
			this.T_Missed.Dock = System.Windows.Forms.DockStyle.Top;
			this.T_Missed.Location = new System.Drawing.Point(0, 50);
			this.T_Missed.Margin = new System.Windows.Forms.Padding(11, 8, 11, 8);
			this.T_Missed.Name = "T_Missed";
			this.T_Missed.ResizeControls = true;
			this.T_Missed.Size = new System.Drawing.Size(0, 22);
			this.T_Missed.TabIndex = 28;
			this.T_Missed.Text = "Recent";
			// 
			// P_Space_1
			// 
			this.P_Space_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
			this.P_Space_1.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Space_1.Location = new System.Drawing.Point(0, 47);
			this.P_Space_1.Name = "P_Space_1";
			this.P_Space_1.Size = new System.Drawing.Size(0, 3);
			this.P_Space_1.TabIndex = 16;
			// 
			// T_Today
			// 
			this.T_Today.Dock = System.Windows.Forms.DockStyle.Top;
			this.T_Today.Location = new System.Drawing.Point(0, 25);
			this.T_Today.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
			this.T_Today.Name = "T_Today";
			this.T_Today.ResizeControls = true;
			this.T_Today.Size = new System.Drawing.Size(0, 22);
			this.T_Today.TabIndex = 27;
			this.T_Today.Text = "New";
			// 
			// P_Space_0
			// 
			this.P_Space_0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
			this.P_Space_0.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Space_0.Location = new System.Drawing.Point(0, 22);
			this.P_Space_0.Name = "P_Space_0";
			this.P_Space_0.Size = new System.Drawing.Size(0, 3);
			this.P_Space_0.TabIndex = 31;
			// 
			// T_Play
			// 
			this.T_Play.Dock = System.Windows.Forms.DockStyle.Top;
			this.T_Play.Location = new System.Drawing.Point(0, 0);
			this.T_Play.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
			this.T_Play.Name = "T_Play";
			this.T_Play.ResizeControls = false;
			this.T_Play.Size = new System.Drawing.Size(0, 22);
			this.T_Play.TabIndex = 30;
			this.T_Play.Text = "Play";
			// 
			// L_Icons8
			// 
			this.L_Icons8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.L_Icons8.AutoSize = true;
			this.L_Icons8.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Icons8.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_Icons8.Location = new System.Drawing.Point(113, 390);
			this.L_Icons8.Name = "L_Icons8";
			this.L_Icons8.Padding = new System.Windows.Forms.Padding(2);
			this.L_Icons8.Size = new System.Drawing.Size(169, 19);
			this.L_Icons8.TabIndex = 26;
			this.L_Icons8.Text = "Icons courtesy of Icons8.com";
			this.L_Icons8.Click += new System.EventHandler(this.Icons8_Click);
			this.L_Icons8.MouseEnter += new System.EventHandler(this.Icons8_MouseEnter);
			this.L_Icons8.MouseLeave += new System.EventHandler(this.Icons8_MouseLeave);
			// 
			// PB_TMDb
			// 
			this.PB_TMDb.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.PB_TMDb.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_TMDb.Image = ((System.Drawing.Image)(resources.GetObject("PB_TMDb.Image")));
			this.PB_TMDb.Location = new System.Drawing.Point(55, 323);
			this.PB_TMDb.Name = "PB_TMDb";
			this.PB_TMDb.Size = new System.Drawing.Size(180, 50);
			this.PB_TMDb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_TMDb.TabIndex = 25;
			this.PB_TMDb.TabStop = false;
			this.PB_TMDb.Click += new System.EventHandler(this.PB_Zooqle_Click);
			// 
			// L_Version
			// 
			this.L_Version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.L_Version.AutoSize = true;
			this.L_Version.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Version.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_Version.Location = new System.Drawing.Point(0, 390);
			this.L_Version.Name = "L_Version";
			this.L_Version.Padding = new System.Windows.Forms.Padding(2);
			this.L_Version.Size = new System.Drawing.Size(52, 19);
			this.L_Version.TabIndex = 24;
			this.L_Version.Text = "Version";
			this.L_Version.Click += new System.EventHandler(this.L_Version_Click);
			// 
			// notifyIcon
			// 
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Shows Calendar";
			this.notifyIcon.Visible = true;
			this.notifyIcon.Click += new System.EventHandler(this.NotifyIcon_Click);
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 20000;
			this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.toolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(65)))), ((int)(((byte)(77)))));
			this.toolTip.InitialDelay = 600;
			this.toolTip.ReshowDelay = 100;
			// 
			// TLP_Buttons
			// 
			this.TLP_Buttons.ColumnCount = 4;
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_Buttons.Controls.Add(this.B_Options, 0, 0);
			this.TLP_Buttons.Controls.Add(this.B_Shows, 2, 0);
			this.TLP_Buttons.Controls.Add(this.B_Movies, 1, 0);
			this.TLP_Buttons.Controls.Add(this.B_Watch, 3, 0);
			this.TLP_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Buttons.Location = new System.Drawing.Point(0, 0);
			this.TLP_Buttons.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Buttons.Name = "TLP_Buttons";
			this.TLP_Buttons.RowCount = 1;
			this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Buttons.Size = new System.Drawing.Size(291, 45);
			this.TLP_Buttons.TabIndex = 17;
			// 
			// B_Options
			// 
			this.B_Options.ActiveColor = null;
			this.B_Options.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Options.AutoSize = true;
			this.B_Options.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Options.Center = true;
			this.B_Options.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Options.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Options.HideText = false;
			this.B_Options.HoverState = SlickControls.Enums.HoverState.Normal;
			this.B_Options.IconSize = 18;
			this.B_Options.Image = global::TVShowsCalendar.Properties.Resources.Icon_Settings;
			this.B_Options.Location = new System.Drawing.Point(0, 10);
			this.B_Options.Margin = new System.Windows.Forms.Padding(0);
			this.B_Options.MinimumSize = new System.Drawing.Size(0, 26);
			this.B_Options.Name = "B_Options";
			this.B_Options.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.B_Options.Size = new System.Drawing.Size(72, 26);
			this.B_Options.TabIndex = 4;
			this.B_Options.Text = "Options";
			// 
			// B_Shows
			// 
			this.B_Shows.ActiveColor = null;
			this.B_Shows.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Shows.AutoSize = true;
			this.B_Shows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Shows.Center = true;
			this.B_Shows.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Shows.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Shows.HideText = false;
			this.B_Shows.HoverState = SlickControls.Enums.HoverState.Normal;
			this.B_Shows.IconSize = 18;
			this.B_Shows.Image = global::TVShowsCalendar.Properties.Resources.Icon_VTV;
			this.B_Shows.Location = new System.Drawing.Point(144, 10);
			this.B_Shows.Margin = new System.Windows.Forms.Padding(0);
			this.B_Shows.MinimumSize = new System.Drawing.Size(0, 26);
			this.B_Shows.Name = "B_Shows";
			this.B_Shows.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.B_Shows.Size = new System.Drawing.Size(72, 26);
			this.B_Shows.TabIndex = 2;
			this.B_Shows.Text = "Shows";
			// 
			// B_Movies
			// 
			this.B_Movies.ActiveColor = null;
			this.B_Movies.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Movies.AutoSize = true;
			this.B_Movies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Movies.Center = true;
			this.B_Movies.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Movies.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Movies.HideText = false;
			this.B_Movies.HoverState = SlickControls.Enums.HoverState.Normal;
			this.B_Movies.IconSize = 18;
			this.B_Movies.Image = global::TVShowsCalendar.Properties.Resources.Icon_Movie;
			this.B_Movies.Location = new System.Drawing.Point(72, 10);
			this.B_Movies.Margin = new System.Windows.Forms.Padding(0);
			this.B_Movies.MinimumSize = new System.Drawing.Size(0, 26);
			this.B_Movies.Name = "B_Movies";
			this.B_Movies.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.B_Movies.Size = new System.Drawing.Size(72, 26);
			this.B_Movies.TabIndex = 2;
			this.B_Movies.Text = "Movies";
			// 
			// B_Watch
			// 
			this.B_Watch.ActiveColor = null;
			this.B_Watch.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Watch.AutoSize = true;
			this.B_Watch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Watch.Center = true;
			this.B_Watch.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Watch.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Watch.HideText = false;
			this.B_Watch.HoverState = SlickControls.Enums.HoverState.Normal;
			this.B_Watch.IconSize = 18;
			this.B_Watch.Image = global::TVShowsCalendar.Properties.Resources.Icon_Play;
			this.B_Watch.Location = new System.Drawing.Point(216, 10);
			this.B_Watch.Margin = new System.Windows.Forms.Padding(0);
			this.B_Watch.MinimumSize = new System.Drawing.Size(0, 26);
			this.B_Watch.Name = "B_Watch";
			this.B_Watch.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.B_Watch.Size = new System.Drawing.Size(75, 26);
			this.B_Watch.TabIndex = 2;
			this.B_Watch.Text = "Watch";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(310, 500);
			this.CloseForm = false;
			this.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(59)))));
			this.FormIcon = global::TVShowsCalendar.Properties.Resources.TVCalendar;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(25, 25);
			this.MinimumSize = new System.Drawing.Size(300, 375);
			this.Name = "MainForm";
			this.ShowControls = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Shows Calendar";
			this.WindowStateChanged += new System.EventHandler(this.MainForm_WindowStateChanged);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.base_P_Content.ResumeLayout(false);
			this.base_P_Controls.ResumeLayout(false);
			this.P_FormContent.ResumeLayout(false);
			this.P_FormContent.PerformLayout();
			this.P_Tabs.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PB_TMDb)).EndInit();
			this.TLP_Buttons.ResumeLayout(false);
			this.TLP_Buttons.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel P_FormContent;
		private SlickControls.Controls.SlickLabel B_Options;
		private System.Windows.Forms.Panel P_Space_2;
		private System.Windows.Forms.Panel P_Space_1;
		private System.Windows.Forms.Panel P_Space_3;
		public System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.Label L_Version;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Label L_Icons8;
		private System.Windows.Forms.PictureBox PB_TMDb;
		public Tab T_Today;
		public Tab T_Upcoming;
		public Tab T_Missed;
		public SlickControls.Controls.SlickLabel B_Shows;
		private System.Windows.Forms.TableLayoutPanel TLP_Buttons;
		public SlickControls.Controls.SlickLabel B_Movies;
		private SlickControls.Controls.VerticalScroll TabScroll;
		private System.Windows.Forms.Panel P_Tabs;
		public SlickControls.Controls.SlickLabel B_Watch;
        private System.Windows.Forms.Panel P_Space_0;
        public Tab T_Play;
    }
}

