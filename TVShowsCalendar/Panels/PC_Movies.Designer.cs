namespace ShowsCalendar
{
	partial class PC_Movies
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
				MovieManager.MovieAdded -= MovieManager_MovieAdded;

				ListControl?.Dispose();
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
			this.panel2 = new System.Windows.Forms.Panel();
			this.P_Tabs = new System.Windows.Forms.Panel();
			this.SP_Oldies = new SlickControls.SlickSectionPanel();
			this.SP_Earlier = new SlickControls.SlickSectionPanel();
			this.SP_Upcoming = new SlickControls.SlickSectionPanel();
			this.SP_Recent = new SlickControls.SlickSectionPanel();
			this.verticalScroll = new SlickControls.SlickScroll();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_ListGridToggle = new SlickControls.SlickButton();
			this.P_TopSpacer = new System.Windows.Forms.Panel();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.B_Add = new SlickControls.SlickButton();
			this.PB_Search = new SlickControls.SlickPictureBox();
			this.TLP_NoMovies = new System.Windows.Forms.TableLayoutPanel();
			this.L_NoMovies = new System.Windows.Forms.Label();
			this.L_NoMoviesInfo = new System.Windows.Forms.Label();
			this.PB_FirstLoad = new SlickControls.SlickPictureBox();
			this.C_TopHeader = new SlickControls.SlickControl();
			this.panel2.SuspendLayout();
			this.P_Tabs.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Search)).BeginInit();
			this.TLP_NoMovies.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_FirstLoad)).BeginInit();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(103, 26);
			this.base_Text.Text = "Movie Library";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.P_Tabs);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(5, 103);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(880, 390);
			this.panel2.TabIndex = 23;
			// 
			// P_Tabs
			// 
			this.P_Tabs.AutoSize = true;
			this.P_Tabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tabs.Controls.Add(this.SP_Oldies);
			this.P_Tabs.Controls.Add(this.SP_Earlier);
			this.P_Tabs.Controls.Add(this.SP_Upcoming);
			this.P_Tabs.Controls.Add(this.SP_Recent);
			this.P_Tabs.Location = new System.Drawing.Point(0, 0);
			this.P_Tabs.MaximumSize = new System.Drawing.Size(880, 2147483647);
			this.P_Tabs.MinimumSize = new System.Drawing.Size(880, 0);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(880, 220);
			this.P_Tabs.TabIndex = 12;
			// 
			// SP_Oldies
			// 
			this.SP_Oldies.Active = false;
			this.SP_Oldies.AutoHide = true;
			this.SP_Oldies.AutoSize = true;
			this.SP_Oldies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Oldies.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Oldies.Flavor = null;
			this.SP_Oldies.Icon = global::ShowsCalendar.Properties.Resources.Big_OldMovie;
			this.SP_Oldies.Info = "Movies released a while back";
			this.SP_Oldies.Location = new System.Drawing.Point(0, 165);
			this.SP_Oldies.MaximumSize = new System.Drawing.Size(880, 2147483647);
			this.SP_Oldies.MinimumSize = new System.Drawing.Size(380, 55);
			this.SP_Oldies.Name = "SP_Oldies";
			this.SP_Oldies.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Oldies.Size = new System.Drawing.Size(880, 55);
			this.SP_Oldies.TabIndex = 8;
			this.SP_Oldies.Text = "Oldies\'";
			// 
			// SP_Earlier
			// 
			this.SP_Earlier.Active = false;
			this.SP_Earlier.AutoHide = true;
			this.SP_Earlier.AutoSize = true;
			this.SP_Earlier.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Earlier.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Earlier.Flavor = null;
			this.SP_Earlier.Icon = global::ShowsCalendar.Properties.Resources.Big_Movie;
			this.SP_Earlier.Info = "Movies released a while back";
			this.SP_Earlier.Location = new System.Drawing.Point(0, 110);
			this.SP_Earlier.MaximumSize = new System.Drawing.Size(880, 2147483647);
			this.SP_Earlier.MinimumSize = new System.Drawing.Size(376, 55);
			this.SP_Earlier.Name = "SP_Earlier";
			this.SP_Earlier.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Earlier.Size = new System.Drawing.Size(880, 55);
			this.SP_Earlier.TabIndex = 7;
			this.SP_Earlier.Text = "Earlier";
			// 
			// SP_Upcoming
			// 
			this.SP_Upcoming.Active = false;
			this.SP_Upcoming.AutoHide = true;
			this.SP_Upcoming.AutoSize = true;
			this.SP_Upcoming.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Upcoming.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Upcoming.Flavor = null;
			this.SP_Upcoming.Icon = global::ShowsCalendar.Properties.Resources.Big_Schedule;
			this.SP_Upcoming.Info = "Recently aired Movies";
			this.SP_Upcoming.Location = new System.Drawing.Point(0, 55);
			this.SP_Upcoming.MaximumSize = new System.Drawing.Size(880, 2147483647);
			this.SP_Upcoming.MinimumSize = new System.Drawing.Size(354, 55);
			this.SP_Upcoming.Name = "SP_Upcoming";
			this.SP_Upcoming.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Upcoming.Size = new System.Drawing.Size(880, 55);
			this.SP_Upcoming.TabIndex = 6;
			this.SP_Upcoming.Text = "Upcoming";
			// 
			// SP_Recent
			// 
			this.SP_Recent.Active = true;
			this.SP_Recent.AutoHide = true;
			this.SP_Recent.AutoSize = true;
			this.SP_Recent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Recent.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Recent.Flavor = null;
			this.SP_Recent.Icon = global::ShowsCalendar.Properties.Resources.Big_Popcorn;
			this.SP_Recent.Info = "Recently aired Movies";
			this.SP_Recent.Location = new System.Drawing.Point(0, 0);
			this.SP_Recent.MaximumSize = new System.Drawing.Size(880, 2147483647);
			this.SP_Recent.MinimumSize = new System.Drawing.Size(433, 55);
			this.SP_Recent.Name = "SP_Recent";
			this.SP_Recent.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Recent.Size = new System.Drawing.Size(880, 55);
			this.SP_Recent.TabIndex = 5;
			this.SP_Recent.Text = "Recently Premiered";
			// 
			// verticalScroll
			// 
			this.verticalScroll.AutoSizeSource = true;
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = this.P_Tabs;
			this.verticalScroll.Location = new System.Drawing.Point(879, 103);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(6, 390);
			this.verticalScroll.Style = SlickControls.StyleType.Vertical;
			this.verticalScroll.TabIndex = 24;
			this.verticalScroll.TabStop = false;
			this.verticalScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.verticalScroll_Scroll);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.C_TopHeader, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_ListGridToggle, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.P_TopSpacer, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.TB_Search, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_Add, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.PB_Search, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(880, 73);
			this.tableLayoutPanel1.TabIndex = 25;
			// 
			// B_ListGridToggle
			// 
			this.B_ListGridToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_ListGridToggle.ColorShade = null;
			this.B_ListGridToggle.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_ListGridToggle.Font = new System.Drawing.Font("Nirmala UI", 9.75F);
			this.B_ListGridToggle.Image = global::ShowsCalendar.Properties.Resources.Tiny_List;
			this.B_ListGridToggle.Location = new System.Drawing.Point(690, 0);
			this.B_ListGridToggle.Margin = new System.Windows.Forms.Padding(15, 0, 10, 5);
			this.B_ListGridToggle.Name = "B_ListGridToggle";
			this.B_ListGridToggle.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_ListGridToggle.Size = new System.Drawing.Size(30, 30);
			this.B_ListGridToggle.SpaceTriggersClick = true;
			this.B_ListGridToggle.TabIndex = 34;
			this.B_ListGridToggle.Click += new System.EventHandler(this.B_ListGridToggle_Click);
			// 
			// P_TopSpacer
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.P_TopSpacer, 4);
			this.P_TopSpacer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_TopSpacer.Location = new System.Drawing.Point(0, 72);
			this.P_TopSpacer.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.P_TopSpacer.Name = "P_TopSpacer";
			this.P_TopSpacer.Size = new System.Drawing.Size(875, 1);
			this.P_TopSpacer.TabIndex = 28;
			this.P_TopSpacer.Visible = false;
			// 
			// TB_Search
			// 
			this.TB_Search.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.TB_Search.EnterTriggersClick = false;
			this.TB_Search.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TB_Search.Image = null;
			this.TB_Search.LabelText = "Search";
			this.TB_Search.Location = new System.Drawing.Point(51, 9);
			this.TB_Search.Margin = new System.Windows.Forms.Padding(10, 4, 3, 0);
			this.TB_Search.MaximumSize = new System.Drawing.Size(9999, 20);
			this.TB_Search.MaxLength = 32767;
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Visible = false;
			this.TB_Search.Password = false;
			this.TB_Search.Placeholder = "Search Movies..";
			this.TB_Search.ReadOnly = false;
			this.TB_Search.Required = false;
			this.TB_Search.SelectAllOnFocus = false;
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.ShowLabel = false;
			this.TB_Search.Size = new System.Drawing.Size(0, 20);
			this.TB_Search.TabIndex = 27;
			this.TB_Search.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.TB_Search.Validation = SlickControls.ValidationType.None;
			this.TB_Search.ValidationRegex = "";
			this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
			// 
			// B_Add
			// 
			this.B_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Add.ColorShade = null;
			this.B_Add.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Add.Font = new System.Drawing.Font("Nirmala UI", 9.75F);
			this.B_Add.Image = global::ShowsCalendar.Properties.Resources.Tiny_Add;
			this.B_Add.Location = new System.Drawing.Point(745, 0);
			this.B_Add.Margin = new System.Windows.Forms.Padding(15, 0, 10, 5);
			this.B_Add.Name = "B_Add";
			this.B_Add.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_Add.Size = new System.Drawing.Size(125, 30);
			this.B_Add.SpaceTriggersClick = true;
			this.B_Add.TabIndex = 14;
			this.B_Add.Text = "ADD MOVIE";
			this.B_Add.Click += new System.EventHandler(this.B_Add_Click);
			// 
			// PB_Search
			// 
			this.PB_Search.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.PB_Search.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Search.Image = global::ShowsCalendar.Properties.Resources.Big_Search;
			this.PB_Search.Location = new System.Drawing.Point(15, 8);
			this.PB_Search.Margin = new System.Windows.Forms.Padding(15, 4, 4, 5);
			this.PB_Search.Name = "PB_Search";
			this.PB_Search.Size = new System.Drawing.Size(22, 22);
			this.PB_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Search.TabIndex = 15;
			this.PB_Search.TabStop = false;
			this.PB_Search.Click += new System.EventHandler(this.PB_Search_Click);
			// 
			// TLP_NoMovies
			// 
			this.TLP_NoMovies.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.TLP_NoMovies.AutoSize = true;
			this.TLP_NoMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_NoMovies.ColumnCount = 1;
			this.TLP_NoMovies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoMovies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoMovies.Controls.Add(this.L_NoMovies, 0, 0);
			this.TLP_NoMovies.Controls.Add(this.L_NoMoviesInfo, 0, 1);
			this.TLP_NoMovies.Location = new System.Drawing.Point(300, 216);
			this.TLP_NoMovies.Name = "TLP_NoMovies";
			this.TLP_NoMovies.RowCount = 2;
			this.TLP_NoMovies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoMovies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoMovies.Size = new System.Drawing.Size(284, 60);
			this.TLP_NoMovies.TabIndex = 30;
			this.TLP_NoMovies.Visible = false;
			// 
			// L_NoMovies
			// 
			this.L_NoMovies.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoMovies.AutoSize = true;
			this.L_NoMovies.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_NoMovies.Location = new System.Drawing.Point(105, 3);
			this.L_NoMovies.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
			this.L_NoMovies.Name = "L_NoMovies";
			this.L_NoMovies.Size = new System.Drawing.Size(74, 17);
			this.L_NoMovies.TabIndex = 1;
			this.L_NoMovies.Text = "No Movies";
			// 
			// L_NoMoviesInfo
			// 
			this.L_NoMoviesInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoMoviesInfo.AutoSize = true;
			this.L_NoMoviesInfo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic);
			this.L_NoMoviesInfo.Location = new System.Drawing.Point(0, 30);
			this.L_NoMoviesInfo.Margin = new System.Windows.Forms.Padding(0);
			this.L_NoMoviesInfo.Name = "L_NoMoviesInfo";
			this.L_NoMoviesInfo.Size = new System.Drawing.Size(284, 30);
			this.L_NoMoviesInfo.TabIndex = 1;
			this.L_NoMoviesInfo.Text = "You don\'t seem to have any Movies in your library.\r\nAdd Movies using the Add butt" +
    "on to start.";
			this.L_NoMoviesInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PB_FirstLoad
			// 
			this.PB_FirstLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.PB_FirstLoad.Image = null;
			this.PB_FirstLoad.Loading = true;
			this.PB_FirstLoad.Location = new System.Drawing.Point(418, 222);
			this.PB_FirstLoad.Name = "PB_FirstLoad";
			this.PB_FirstLoad.Size = new System.Drawing.Size(48, 48);
			this.PB_FirstLoad.TabIndex = 33;
			this.PB_FirstLoad.TabStop = false;
			// 
			// C_TopHeader
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.C_TopHeader, 4);
			this.C_TopHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_TopHeader.Location = new System.Drawing.Point(0, 35);
			this.C_TopHeader.Margin = new System.Windows.Forms.Padding(0);
			this.C_TopHeader.Name = "C_TopHeader";
			this.C_TopHeader.Size = new System.Drawing.Size(880, 37);
			this.C_TopHeader.TabIndex = 35;
			this.C_TopHeader.Visible = false;
			this.C_TopHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.C_TopHeader_Paint);
			this.C_TopHeader.MouseClick += new System.Windows.Forms.MouseEventHandler(this.C_TopHeader_MouseClick);
			// 
			// PC_Movies
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.PB_FirstLoad);
			this.Controls.Add(this.TLP_NoMovies);
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_Movies";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Size = new System.Drawing.Size(885, 493);
			this.Text = "Movie Library";
			this.Load += new System.EventHandler(this.PC_Movies_Load);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.panel2, 0);
			this.Controls.SetChildIndex(this.verticalScroll, 0);
			this.Controls.SetChildIndex(this.TLP_NoMovies, 0);
			this.Controls.SetChildIndex(this.PB_FirstLoad, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.P_Tabs.ResumeLayout(false);
			this.P_Tabs.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PB_Search)).EndInit();
			this.TLP_NoMovies.ResumeLayout(false);
			this.TLP_NoMovies.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_FirstLoad)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel P_Tabs;
		private SlickControls.SlickScroll verticalScroll;
		private SlickControls.SlickButton B_Add;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private SlickControls.SlickPictureBox PB_Search;
		private System.Windows.Forms.TableLayoutPanel TLP_NoMovies;
		private System.Windows.Forms.Label L_NoMovies;
		private System.Windows.Forms.Label L_NoMoviesInfo;
		private SlickControls.SlickSectionPanel SP_Recent;
		private SlickControls.SlickSectionPanel SP_Earlier;
		private SlickControls.SlickSectionPanel SP_Upcoming;
		private SlickControls.SlickTextBox TB_Search;
		private SlickControls.SlickSectionPanel SP_Oldies;
		private System.Windows.Forms.Panel P_TopSpacer;
		private SlickControls.SlickPictureBox PB_FirstLoad;
		private SlickControls.SlickButton B_ListGridToggle;
		private SlickControls.SlickControl C_TopHeader;
	}
}
