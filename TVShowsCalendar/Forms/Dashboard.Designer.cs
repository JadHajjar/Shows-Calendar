namespace ShowsCalendar
{
	partial class Dashboard
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
			this.PI_Settings = new SlickControls.PanelItem();
			this.PI_LibraryFolders = new SlickControls.PanelItem();
			this.PI_Shows = new SlickControls.PanelItem();
			this.PI_Movies = new SlickControls.PanelItem();
			this.PI_Library = new SlickControls.PanelItem();
			this.PI_Watch = new SlickControls.PanelItem();
			this.PI_Dashboard = new SlickControls.PanelItem();
			this.L_Text = new System.Windows.Forms.Label();
			this.L_Version = new System.Windows.Forms.Label();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.PI_About = new SlickControls.PanelItem();
			this.PI_LibraryRename = new SlickControls.PanelItem();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.PI_People = new SlickControls.PanelItem();
			this.base_P_SideControls.SuspendLayout();
			this.base_P_Container.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.Size = new System.Drawing.Size(987, 637);
			// 
			// base_P_SideControls
			// 
			this.base_P_SideControls.Controls.Add(this.tableLayoutPanel1);
			this.base_P_SideControls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(129)))), ((int)(((byte)(150)))));
			this.base_P_SideControls.Location = new System.Drawing.Point(0, 529);
			this.base_P_SideControls.MinimumSize = new System.Drawing.Size(0, 0);
			this.base_P_SideControls.Size = new System.Drawing.Size(0, 16);
			// 
			// base_P_Container
			// 
			this.base_P_Container.Size = new System.Drawing.Size(989, 639);
			// 
			// PI_Settings
			// 
			this.PI_Settings.ForceReopen = false;
			this.PI_Settings.Group = "Manage";
			this.PI_Settings.Highlighted = false;
			this.PI_Settings.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Settings;
			this.PI_Settings.Selected = false;
			this.PI_Settings.Text = "Settings";
			this.PI_Settings.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Settings_OnClick);
			// 
			// PI_LibraryFolders
			// 
			this.PI_LibraryFolders.ForceReopen = false;
			this.PI_LibraryFolders.Group = "Manage";
			this.PI_LibraryFolders.Highlighted = false;
			this.PI_LibraryFolders.Icon = global::ShowsCalendar.Properties.Resources.Tiny_FolderPlay;
			this.PI_LibraryFolders.Selected = false;
			this.PI_LibraryFolders.Text = "Library Folders";
			this.PI_LibraryFolders.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_LibraryFolders_OnClick);
			// 
			// PI_Shows
			// 
			this.PI_Shows.ForceReopen = false;
			this.PI_Shows.Group = "Content";
			this.PI_Shows.Highlighted = false;
			this.PI_Shows.Icon = ((System.Drawing.Bitmap)(resources.GetObject("PI_Shows.Icon")));
			this.PI_Shows.Selected = false;
			this.PI_Shows.Text = "TV Shows";
			this.PI_Shows.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Shows_OnClick);
			// 
			// PI_Movies
			// 
			this.PI_Movies.ForceReopen = false;
			this.PI_Movies.Group = "Content";
			this.PI_Movies.Highlighted = false;
			this.PI_Movies.Icon = ((System.Drawing.Bitmap)(resources.GetObject("PI_Movies.Icon")));
			this.PI_Movies.Selected = false;
			this.PI_Movies.Text = "Movies";
			this.PI_Movies.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Movies_OnClick);
			// 
			// PI_Library
			// 
			this.PI_Library.ForceReopen = false;
			this.PI_Library.Group = "Watch";
			this.PI_Library.Highlighted = false;
			this.PI_Library.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Library;
			this.PI_Library.Selected = false;
			this.PI_Library.Text = "Video Library";
			this.PI_Library.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Library_OnClick);
			// 
			// PI_Watch
			// 
			this.PI_Watch.ForceReopen = false;
			this.PI_Watch.Group = "Watch";
			this.PI_Watch.Highlighted = false;
			this.PI_Watch.Icon = ((System.Drawing.Bitmap)(resources.GetObject("PI_Watch.Icon")));
			this.PI_Watch.Selected = false;
			this.PI_Watch.Text = "Watch";
			this.PI_Watch.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Watch_OnClick);
			// 
			// PI_Dashboard
			// 
			this.PI_Dashboard.ForceReopen = false;
			this.PI_Dashboard.Group = null;
			this.PI_Dashboard.Highlighted = false;
			this.PI_Dashboard.Icon = ((System.Drawing.Bitmap)(resources.GetObject("PI_Dashboard.Icon")));
			this.PI_Dashboard.Selected = false;
			this.PI_Dashboard.Text = "Dashboard";
			this.PI_Dashboard.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Dashboard_OnClick);
			// 
			// L_Text
			// 
			this.L_Text.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Text.AutoSize = true;
			this.L_Text.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Text.Font = new System.Drawing.Font("Nirmala UI", 6.75F);
			this.L_Text.Location = new System.Drawing.Point(0, 0);
			this.L_Text.Margin = new System.Windows.Forms.Padding(0);
			this.L_Text.Name = "L_Text";
			this.L_Text.Padding = new System.Windows.Forms.Padding(2);
			this.L_Text.Size = new System.Drawing.Size(73, 16);
			this.L_Text.TabIndex = 29;
			this.L_Text.Text = "Shows Calendar";
			this.L_Text.Click += new System.EventHandler(this.L_Text_Click);
			// 
			// L_Version
			// 
			this.L_Version.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.L_Version.AutoSize = true;
			this.L_Version.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Version.Font = new System.Drawing.Font("Nirmala UI", 6.75F);
			this.L_Version.Location = new System.Drawing.Point(73, 0);
			this.L_Version.Margin = new System.Windows.Forms.Padding(0);
			this.L_Version.Name = "L_Version";
			this.L_Version.Padding = new System.Windows.Forms.Padding(2);
			this.L_Version.Size = new System.Drawing.Size(1, 16);
			this.L_Version.TabIndex = 27;
			this.L_Version.Text = "Version";
			this.L_Version.Click += new System.EventHandler(this.L_Version_Click);
			// 
			// notifyIcon
			// 
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Shows Calendar";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
			// 
			// PI_About
			// 
			this.PI_About.ForceReopen = false;
			this.PI_About.Group = "Other";
			this.PI_About.Highlighted = false;
			this.PI_About.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Info;
			this.PI_About.Selected = false;
			this.PI_About.Text = "About";
			this.PI_About.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_About_OnClick);
			// 
			// PI_LibraryRename
			// 
			this.PI_LibraryRename.ForceReopen = false;
			this.PI_LibraryRename.Group = "Manage";
			this.PI_LibraryRename.Highlighted = false;
			this.PI_LibraryRename.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Manage;
			this.PI_LibraryRename.Selected = false;
			this.PI_LibraryRename.Text = "Files Manager";
			this.PI_LibraryRename.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_LibraryRename_OnClick);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.L_Version, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.L_Text, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(0, 16);
			this.tableLayoutPanel1.TabIndex = 30;
			// 
			// PI_People
			// 
			this.PI_People.ForceReopen = false;
			this.PI_People.Group = "Content";
			this.PI_People.Highlighted = false;
			this.PI_People.Icon = global::ShowsCalendar.Properties.Resources.Tiny_People;
			this.PI_People.Selected = false;
			this.PI_People.Text = "People";
			this.PI_People.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_People_OnClick);
			// 
			// Dashboard
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1000, 650);
			this.CloseForm = false;
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IconBounds = new System.Drawing.Rectangle(3, 30, 14, 32);
			this.MaximizeBox = true;
			this.MinimizeBox = true;
			this.MinimumSize = new System.Drawing.Size(765, 445);
			this.Name = "Dashboard";
			this.SidebarItems = new SlickControls.PanelItem[] {
        this.PI_Dashboard,
        this.PI_Shows,
        this.PI_Movies,
        this.PI_People,
        this.PI_Watch,
        this.PI_Library,
        this.PI_Settings,
        this.PI_LibraryFolders,
        this.PI_LibraryRename,
        this.PI_About};
			this.Text = "Shows Calendar";
			this.Activated += new System.EventHandler(this.Dashboard_Activated);
			this.Deactivate += new System.EventHandler(this.Dashboard_Deactivate);
			this.Load += new System.EventHandler(this.Dashboard_Load);
			this.ResizeEnd += new System.EventHandler(this.Dashboard_ResizeEnd);
			this.base_P_SideControls.ResumeLayout(false);
			this.base_P_SideControls.PerformLayout();
			this.base_P_Container.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label L_Text;
		private System.Windows.Forms.Label L_Version;
		public System.Windows.Forms.NotifyIcon notifyIcon;
		internal SlickControls.PanelItem PI_Settings;
		internal SlickControls.PanelItem PI_LibraryFolders;
		internal SlickControls.PanelItem PI_Shows;
		internal SlickControls.PanelItem PI_Movies;
		internal SlickControls.PanelItem PI_Library;
		internal SlickControls.PanelItem PI_Watch;
		internal SlickControls.PanelItem PI_Dashboard;
		internal SlickControls.PanelItem PI_About;
		internal SlickControls.PanelItem PI_LibraryRename;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		internal SlickControls.PanelItem PI_People;
	}
}