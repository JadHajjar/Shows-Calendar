namespace ShowsCalendar
{
	partial class PC_Shows
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
				ShowManager.ShowAdded -= ShowManager_ShowAdded;

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
			this.P_Content = new System.Windows.Forms.Panel();
			this.P_Tabs = new System.Windows.Forms.Panel();
			this.SP_Ended = new SlickControls.SlickSectionPanel();
			this.SP_Returning = new SlickControls.SlickSectionPanel();
			this.SP_Upcoming = new SlickControls.SlickSectionPanel();
			this.SP_Airing = new SlickControls.SlickSectionPanel();
			this.verticalScroll = new SlickControls.SlickScroll();
			this.TLP_Top = new System.Windows.Forms.TableLayoutPanel();
			this.PB_Search = new SlickControls.SlickPictureBox();
			this.B_Add = new SlickControls.SlickButton();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.P_TopSpacer = new System.Windows.Forms.Panel();
			this.B_ListGridToggle = new SlickControls.SlickButton();
			this.C_TopHeader = new SlickControls.SlickControl();
			this.TLP_NoShows = new System.Windows.Forms.TableLayoutPanel();
			this.L_NoShows = new System.Windows.Forms.Label();
			this.L_NoShowsInfo = new System.Windows.Forms.Label();
			this.PB_FirstLoad = new SlickControls.SlickPictureBox();
			this.P_Content.SuspendLayout();
			this.P_Tabs.SuspendLayout();
			this.TLP_Top.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Search)).BeginInit();
			this.TLP_NoShows.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_FirstLoad)).BeginInit();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(98, 26);
			this.base_Text.Text = "Show Library";
			// 
			// P_Content
			// 
			this.P_Content.Controls.Add(this.P_Tabs);
			this.P_Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Content.Location = new System.Drawing.Point(5, 103);
			this.P_Content.Name = "P_Content";
			this.P_Content.Size = new System.Drawing.Size(880, 390);
			this.P_Content.TabIndex = 23;
			// 
			// P_Tabs
			// 
			this.P_Tabs.AutoSize = true;
			this.P_Tabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tabs.Controls.Add(this.SP_Ended);
			this.P_Tabs.Controls.Add(this.SP_Returning);
			this.P_Tabs.Controls.Add(this.SP_Upcoming);
			this.P_Tabs.Controls.Add(this.SP_Airing);
			this.P_Tabs.Location = new System.Drawing.Point(0, 0);
			this.P_Tabs.MaximumSize = new System.Drawing.Size(880, 2147483647);
			this.P_Tabs.MinimumSize = new System.Drawing.Size(880, 0);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(880, 220);
			this.P_Tabs.TabIndex = 3;
			// 
			// SP_Ended
			// 
			this.SP_Ended.Active = false;
			this.SP_Ended.AutoHide = true;
			this.SP_Ended.AutoSize = true;
			this.SP_Ended.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Ended.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Ended.Flavor = null;
			this.SP_Ended.Icon = global::ShowsCalendar.Properties.Resources.Big_RetroTv;
			this.SP_Ended.Info = "Ended TV Shows";
			this.SP_Ended.Location = new System.Drawing.Point(0, 165);
			this.SP_Ended.MaximumSize = new System.Drawing.Size(880, 2147483647);
			this.SP_Ended.MinimumSize = new System.Drawing.Size(274, 55);
			this.SP_Ended.Name = "SP_Ended";
			this.SP_Ended.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Ended.Size = new System.Drawing.Size(880, 55);
			this.SP_Ended.TabIndex = 16;
			this.SP_Ended.Text = "Ended";
			// 
			// SP_Returning
			// 
			this.SP_Returning.Active = false;
			this.SP_Returning.AutoHide = true;
			this.SP_Returning.AutoSize = true;
			this.SP_Returning.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Returning.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Returning.Flavor = null;
			this.SP_Returning.Icon = global::ShowsCalendar.Properties.Resources.Big_TVEmpty;
			this.SP_Returning.Info = "Returning TV Shows with no release date yet";
			this.SP_Returning.Location = new System.Drawing.Point(0, 110);
			this.SP_Returning.MaximumSize = new System.Drawing.Size(880, 2147483647);
			this.SP_Returning.MinimumSize = new System.Drawing.Size(515, 55);
			this.SP_Returning.Name = "SP_Returning";
			this.SP_Returning.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Returning.Size = new System.Drawing.Size(880, 55);
			this.SP_Returning.TabIndex = 15;
			this.SP_Returning.Text = "Returning";
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
			this.SP_Upcoming.Info = "TV Shows with a new upcoming episode";
			this.SP_Upcoming.Location = new System.Drawing.Point(0, 55);
			this.SP_Upcoming.MaximumSize = new System.Drawing.Size(880, 2147483647);
			this.SP_Upcoming.MinimumSize = new System.Drawing.Size(489, 55);
			this.SP_Upcoming.Name = "SP_Upcoming";
			this.SP_Upcoming.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Upcoming.Size = new System.Drawing.Size(880, 55);
			this.SP_Upcoming.TabIndex = 14;
			this.SP_Upcoming.Text = "Upcoming";
			// 
			// SP_Airing
			// 
			this.SP_Airing.Active = true;
			this.SP_Airing.AutoHide = true;
			this.SP_Airing.AutoSize = true;
			this.SP_Airing.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Airing.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Airing.Flavor = null;
			this.SP_Airing.Icon = global::ShowsCalendar.Properties.Resources.Big_Airing;
			this.SP_Airing.Info = "Currently airing TV Shows";
			this.SP_Airing.Location = new System.Drawing.Point(0, 0);
			this.SP_Airing.MaximumSize = new System.Drawing.Size(880, 2147483647);
			this.SP_Airing.MinimumSize = new System.Drawing.Size(337, 55);
			this.SP_Airing.Name = "SP_Airing";
			this.SP_Airing.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Airing.Size = new System.Drawing.Size(880, 55);
			this.SP_Airing.TabIndex = 13;
			this.SP_Airing.Text = "Airing";
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
			// TLP_Top
			// 
			this.TLP_Top.AutoSize = true;
			this.TLP_Top.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Top.ColumnCount = 4;
			this.TLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Top.Controls.Add(this.PB_Search, 0, 0);
			this.TLP_Top.Controls.Add(this.B_Add, 3, 0);
			this.TLP_Top.Controls.Add(this.TB_Search, 1, 0);
			this.TLP_Top.Controls.Add(this.P_TopSpacer, 0, 2);
			this.TLP_Top.Controls.Add(this.B_ListGridToggle, 2, 0);
			this.TLP_Top.Controls.Add(this.C_TopHeader, 0, 1);
			this.TLP_Top.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Top.Location = new System.Drawing.Point(5, 30);
			this.TLP_Top.Name = "TLP_Top";
			this.TLP_Top.RowCount = 3;
			this.TLP_Top.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Top.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_Top.Size = new System.Drawing.Size(880, 73);
			this.TLP_Top.TabIndex = 0;
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
			this.PB_Search.TabIndex = 16;
			this.PB_Search.TabStop = false;
			this.PB_Search.Click += new System.EventHandler(this.PB_Search_Click);
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
			this.B_Add.Text = "ADD SHOW";
			this.B_Add.Click += new System.EventHandler(this.B_Add_Click);
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
			this.TB_Search.Password = false;
			this.TB_Search.Visible = false;
			this.TB_Search.Placeholder = "Search Shows..";
			this.TB_Search.ReadOnly = false;
			this.TB_Search.Required = false;
			this.TB_Search.SelectAllOnFocus = false;
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.ShowLabel = false;
			this.TB_Search.Size = new System.Drawing.Size(0, 20);
			this.TB_Search.TabIndex = 0;
			this.TB_Search.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.TB_Search.Validation = SlickControls.ValidationType.None;
			this.TB_Search.ValidationRegex = "";
			this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
			// 
			// P_TopSpacer
			// 
			this.TLP_Top.SetColumnSpan(this.P_TopSpacer, 4);
			this.P_TopSpacer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_TopSpacer.Location = new System.Drawing.Point(0, 72);
			this.P_TopSpacer.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.P_TopSpacer.Name = "P_TopSpacer";
			this.P_TopSpacer.Size = new System.Drawing.Size(875, 1);
			this.P_TopSpacer.TabIndex = 17;
			this.P_TopSpacer.Visible = false;
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
			this.B_ListGridToggle.TabIndex = 14;
			this.B_ListGridToggle.Click += new System.EventHandler(this.B_ListGridToggle_Click);
			// 
			// C_TopHeader
			// 
			this.TLP_Top.SetColumnSpan(this.C_TopHeader, 4);
			this.C_TopHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_TopHeader.Location = new System.Drawing.Point(0, 35);
			this.C_TopHeader.Margin = new System.Windows.Forms.Padding(0);
			this.C_TopHeader.Name = "C_TopHeader";
			this.C_TopHeader.Size = new System.Drawing.Size(880, 37);
			this.C_TopHeader.TabIndex = 18;
			this.C_TopHeader.Visible = false;
			this.C_TopHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.C_TopHeader_Paint);
			this.C_TopHeader.MouseClick += new System.Windows.Forms.MouseEventHandler(this.C_TopHeader_MouseClick);
			// 
			// TLP_NoShows
			// 
			this.TLP_NoShows.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.TLP_NoShows.AutoSize = true;
			this.TLP_NoShows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_NoShows.ColumnCount = 1;
			this.TLP_NoShows.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoShows.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoShows.Controls.Add(this.L_NoShows, 0, 0);
			this.TLP_NoShows.Controls.Add(this.L_NoShowsInfo, 0, 1);
			this.TLP_NoShows.Location = new System.Drawing.Point(302, 216);
			this.TLP_NoShows.Name = "TLP_NoShows";
			this.TLP_NoShows.RowCount = 2;
			this.TLP_NoShows.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoShows.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoShows.Size = new System.Drawing.Size(280, 60);
			this.TLP_NoShows.TabIndex = 28;
			this.TLP_NoShows.Visible = false;
			// 
			// L_NoShows
			// 
			this.L_NoShows.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoShows.AutoSize = true;
			this.L_NoShows.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_NoShows.Location = new System.Drawing.Point(105, 3);
			this.L_NoShows.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
			this.L_NoShows.Name = "L_NoShows";
			this.L_NoShows.Size = new System.Drawing.Size(69, 17);
			this.L_NoShows.TabIndex = 1;
			this.L_NoShows.Text = "No Shows";
			// 
			// L_NoShowsInfo
			// 
			this.L_NoShowsInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoShowsInfo.AutoSize = true;
			this.L_NoShowsInfo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic);
			this.L_NoShowsInfo.Location = new System.Drawing.Point(0, 30);
			this.L_NoShowsInfo.Margin = new System.Windows.Forms.Padding(0);
			this.L_NoShowsInfo.Name = "L_NoShowsInfo";
			this.L_NoShowsInfo.Size = new System.Drawing.Size(280, 30);
			this.L_NoShowsInfo.TabIndex = 1;
			this.L_NoShowsInfo.Text = "You don\'t seem to have any Shows in your library.\r\nAdd Shows using the Add button" +
    " to start.";
			this.L_NoShowsInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			// PC_Shows
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.PB_FirstLoad);
			this.Controls.Add(this.TLP_NoShows);
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.P_Content);
			this.Controls.Add(this.TLP_Top);
			this.Name = "PC_Shows";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Size = new System.Drawing.Size(885, 493);
			this.Text = "Show Library";
			this.Load += new System.EventHandler(this.PC_Shows_Load);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.TLP_Top, 0);
			this.Controls.SetChildIndex(this.P_Content, 0);
			this.Controls.SetChildIndex(this.verticalScroll, 0);
			this.Controls.SetChildIndex(this.TLP_NoShows, 0);
			this.Controls.SetChildIndex(this.PB_FirstLoad, 0);
			this.P_Content.ResumeLayout(false);
			this.P_Content.PerformLayout();
			this.P_Tabs.ResumeLayout(false);
			this.P_Tabs.PerformLayout();
			this.TLP_Top.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PB_Search)).EndInit();
			this.TLP_NoShows.ResumeLayout(false);
			this.TLP_NoShows.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_FirstLoad)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel P_Content;
		private SlickControls.SlickScroll verticalScroll;
		private SlickControls.SlickButton B_Add;
		private System.Windows.Forms.TableLayoutPanel TLP_Top;
		private System.Windows.Forms.TableLayoutPanel TLP_NoShows;
		private System.Windows.Forms.Label L_NoShows;
		private System.Windows.Forms.Label L_NoShowsInfo;
		private SlickControls.SlickSectionPanel SP_Airing;
		private SlickControls.SlickSectionPanel SP_Ended;
		private SlickControls.SlickSectionPanel SP_Upcoming;
		private SlickControls.SlickSectionPanel SP_Returning;
		private System.Windows.Forms.Panel P_Tabs;
		private SlickControls.SlickTextBox TB_Search;
		private SlickControls.SlickPictureBox PB_Search;
		private System.Windows.Forms.Panel P_TopSpacer;
		private SlickControls.SlickPictureBox PB_FirstLoad;
		private SlickControls.SlickButton B_ListGridToggle;
		private SlickControls.SlickControl C_TopHeader;
	}
}
