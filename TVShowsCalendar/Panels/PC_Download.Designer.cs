namespace ShowsCalendar
{
	partial class PC_Download
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
			this.verticalScroll = new SlickControls.SlickScroll();
			this.P_Torrents = new System.Windows.Forms.TableLayoutPanel();
			this.OtherTorrentsTile = new ShowsCalendar.TorrentTile();
			this.TorrentsTile = new ShowsCalendar.TorrentTile();
			this.TLP_MoreTorrents = new System.Windows.Forms.TableLayoutPanel();
			this.P_Spacer_4 = new System.Windows.Forms.Panel();
			this.L_MoreInfo = new System.Windows.Forms.Label();
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.P_Spacer_1 = new System.Windows.Forms.Panel();
			this.P_Spacer_2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.L_NoResults = new System.Windows.Forms.Label();
			this.TLP_Icons = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_QualitySelection = new System.Windows.Forms.TableLayoutPanel();
			this.P_Spacer_3 = new System.Windows.Forms.Panel();
			this.PB_Loader2 = new SlickControls.SlickPictureBox();
			this.I_MoreInfo = new System.Windows.Forms.PictureBox();
			this.PB_Image = new SlickControls.SlickPictureBox();
			this.PB_Loader = new SlickControls.SlickPictureBox();
			this.PB_Health = new System.Windows.Forms.PictureBox();
			this.PB_Download = new System.Windows.Forms.PictureBox();
			this.PB_Size = new System.Windows.Forms.PictureBox();
			this.PB_Res = new System.Windows.Forms.PictureBox();
			this.PB_Sound = new System.Windows.Forms.PictureBox();
			this.PB_Subs = new System.Windows.Forms.PictureBox();
			this.PB_Label = new System.Windows.Forms.PictureBox();
			this.L_1080p = new System.Windows.Forms.PictureBox();
			this.L_720p = new System.Windows.Forms.PictureBox();
			this.L_Low = new System.Windows.Forms.PictureBox();
			this.L_3D = new System.Windows.Forms.PictureBox();
			this.L_4K = new System.Windows.Forms.PictureBox();
			this.L_AllDownloads = new System.Windows.Forms.PictureBox();
			this.P_Torrents.SuspendLayout();
			this.TLP_MoreTorrents.SuspendLayout();
			this.TLP_Main.SuspendLayout();
			this.panel1.SuspendLayout();
			this.TLP_Icons.SuspendLayout();
			this.TLP_QualitySelection.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loader2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.I_MoreInfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Image)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Health)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Download)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Size)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Res)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Sound)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Subs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Label)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.L_1080p)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.L_720p)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.L_Low)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.L_3D)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.L_4K)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.L_AllDownloads)).BeginInit();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(138, 26);
			this.base_Text.Text = "Download Episode";
			// 
			// verticalScroll
			// 
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = this.P_Torrents;
			this.verticalScroll.Location = new System.Drawing.Point(777, 30);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(6, 408);
			this.verticalScroll.SmallHandle = true;
			this.verticalScroll.Style = SlickControls.StyleType.Vertical;
			this.verticalScroll.TabIndex = 13;
			this.verticalScroll.TabStop = false;
			// 
			// P_Torrents
			// 
			this.P_Torrents.AutoSize = true;
			this.P_Torrents.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Torrents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.P_Torrents.Controls.Add(this.OtherTorrentsTile, 0, 3);
			this.P_Torrents.Controls.Add(this.TorrentsTile, 0, 1);
			this.P_Torrents.Controls.Add(this.TLP_MoreTorrents, 0, 2);
			this.P_Torrents.Location = new System.Drawing.Point(0, 0);
			this.P_Torrents.MaximumSize = new System.Drawing.Size(753, 2147483647);
			this.P_Torrents.MinimumSize = new System.Drawing.Size(753, 0);
			this.P_Torrents.Name = "P_Torrents";
			this.P_Torrents.RowCount = 4;
			this.P_Torrents.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Torrents.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Torrents.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Torrents.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Torrents.Size = new System.Drawing.Size(753, 26);
			this.P_Torrents.TabIndex = 6;
			// 
			// OtherTorrentsTile
			// 
			this.OtherTorrentsTile.AutoInvalidate = false;
			this.OtherTorrentsTile.Cursor = System.Windows.Forms.Cursors.Default;
			this.OtherTorrentsTile.Dock = System.Windows.Forms.DockStyle.Top;
			this.OtherTorrentsTile.ItemHeight = 28;
			this.OtherTorrentsTile.Location = new System.Drawing.Point(0, 25);
			this.OtherTorrentsTile.Margin = new System.Windows.Forms.Padding(0);
			this.OtherTorrentsTile.Name = "OtherTorrentsTile";
			this.OtherTorrentsTile.QualityFilter = ShowsCalendar.QualityFilter.All;
			this.OtherTorrentsTile.Reversed = false;
			this.OtherTorrentsTile.Size = new System.Drawing.Size(753, 1);
			this.OtherTorrentsTile.SortOption = ShowsCalendar.TorrentSortOption.None;
			this.OtherTorrentsTile.TabIndex = 4;
			this.OtherTorrentsTile.Visible = false;
			// 
			// TorrentsTile
			// 
			this.TorrentsTile.AutoInvalidate = false;
			this.TorrentsTile.Cursor = System.Windows.Forms.Cursors.Default;
			this.TorrentsTile.Dock = System.Windows.Forms.DockStyle.Top;
			this.TorrentsTile.ItemHeight = 28;
			this.TorrentsTile.Location = new System.Drawing.Point(0, 0);
			this.TorrentsTile.Margin = new System.Windows.Forms.Padding(0);
			this.TorrentsTile.Name = "TorrentsTile";
			this.TorrentsTile.QualityFilter = ShowsCalendar.QualityFilter.All;
			this.TorrentsTile.Reversed = false;
			this.TorrentsTile.Size = new System.Drawing.Size(753, 1);
			this.TorrentsTile.SortOption = ShowsCalendar.TorrentSortOption.None;
			this.TorrentsTile.TabIndex = 3;
			// 
			// TLP_MoreTorrents
			// 
			this.TLP_MoreTorrents.AutoSize = true;
			this.TLP_MoreTorrents.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_MoreTorrents.ColumnCount = 4;
			this.TLP_MoreTorrents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_MoreTorrents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MoreTorrents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MoreTorrents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_MoreTorrents.Controls.Add(this.P_Spacer_4, 0, 1);
			this.TLP_MoreTorrents.Controls.Add(this.L_MoreInfo, 2, 0);
			this.TLP_MoreTorrents.Controls.Add(this.I_MoreInfo, 1, 0);
			this.TLP_MoreTorrents.Cursor = System.Windows.Forms.Cursors.Hand;
			this.TLP_MoreTorrents.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_MoreTorrents.Location = new System.Drawing.Point(0, 1);
			this.TLP_MoreTorrents.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_MoreTorrents.Name = "TLP_MoreTorrents";
			this.TLP_MoreTorrents.RowCount = 2;
			this.TLP_MoreTorrents.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_MoreTorrents.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_MoreTorrents.Size = new System.Drawing.Size(753, 24);
			this.TLP_MoreTorrents.TabIndex = 2;
			this.TLP_MoreTorrents.Tag = "NoMouseDown";
			this.TLP_MoreTorrents.Visible = false;
			this.TLP_MoreTorrents.Click += new System.EventHandler(this.L_MoreInfo_Click);
			this.TLP_MoreTorrents.MouseEnter += new System.EventHandler(this.L_MoreInfo_MouseEnter);
			this.TLP_MoreTorrents.MouseLeave += new System.EventHandler(this.L_MoreInfo_MouseLeave);
			// 
			// P_Spacer_4
			// 
			this.TLP_MoreTorrents.SetColumnSpan(this.P_Spacer_4, 4);
			this.P_Spacer_4.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Spacer_4.Location = new System.Drawing.Point(10, 23);
			this.P_Spacer_4.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.P_Spacer_4.Name = "P_Spacer_4";
			this.P_Spacer_4.Size = new System.Drawing.Size(733, 1);
			this.P_Spacer_4.TabIndex = 17;
			// 
			// L_MoreInfo
			// 
			this.L_MoreInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_MoreInfo.AutoSize = true;
			this.L_MoreInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_MoreInfo.Location = new System.Drawing.Point(347, 5);
			this.L_MoreInfo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.L_MoreInfo.Name = "L_MoreInfo";
			this.L_MoreInfo.Size = new System.Drawing.Size(74, 13);
			this.L_MoreInfo.TabIndex = 2;
			this.L_MoreInfo.Tag = "NoMouseDown";
			this.L_MoreInfo.Text = "More Results";
			this.L_MoreInfo.Click += new System.EventHandler(this.L_MoreInfo_Click);
			this.L_MoreInfo.MouseEnter += new System.EventHandler(this.L_MoreInfo_MouseEnter);
			this.L_MoreInfo.MouseLeave += new System.EventHandler(this.L_MoreInfo_MouseLeave);
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 1;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Controls.Add(this.PB_Image, 0, 0);
			this.TLP_Main.Controls.Add(this.P_Spacer_1, 0, 1);
			this.TLP_Main.Controls.Add(this.P_Spacer_2, 0, 3);
			this.TLP_Main.Controls.Add(this.panel1, 0, 6);
			this.TLP_Main.Controls.Add(this.TLP_Icons, 0, 4);
			this.TLP_Main.Controls.Add(this.TLP_QualitySelection, 0, 2);
			this.TLP_Main.Controls.Add(this.P_Spacer_3, 0, 5);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(5, 30);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.Padding = new System.Windows.Forms.Padding(10, 0, 15, 0);
			this.TLP_Main.RowCount = 7;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Size = new System.Drawing.Size(778, 408);
			this.TLP_Main.TabIndex = 14;
			// 
			// P_Spacer_1
			// 
			this.P_Spacer_1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Spacer_1.Location = new System.Drawing.Point(20, 100);
			this.P_Spacer_1.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.P_Spacer_1.Name = "P_Spacer_1";
			this.P_Spacer_1.Size = new System.Drawing.Size(733, 1);
			this.P_Spacer_1.TabIndex = 21;
			// 
			// P_Spacer_2
			// 
			this.P_Spacer_2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Spacer_2.Location = new System.Drawing.Point(40, 131);
			this.P_Spacer_2.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
			this.P_Spacer_2.Name = "P_Spacer_2";
			this.P_Spacer_2.Size = new System.Drawing.Size(693, 1);
			this.P_Spacer_2.TabIndex = 20;
			this.P_Spacer_2.Visible = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.PB_Loader);
			this.panel1.Controls.Add(this.P_Torrents);
			this.panel1.Controls.Add(this.L_NoResults);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(10, 163);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(753, 245);
			this.panel1.TabIndex = 18;
			// 
			// L_NoResults
			// 
			this.L_NoResults.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.L_NoResults.AutoSize = true;
			this.L_NoResults.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_NoResults.Location = new System.Drawing.Point(332, 109);
			this.L_NoResults.Margin = new System.Windows.Forms.Padding(45, 0, 3, 0);
			this.L_NoResults.Name = "L_NoResults";
			this.L_NoResults.Size = new System.Drawing.Size(85, 21);
			this.L_NoResults.TabIndex = 5;
			this.L_NoResults.Text = "No Results";
			this.L_NoResults.Visible = false;
			// 
			// TLP_Icons
			// 
			this.TLP_Icons.ColumnCount = 7;
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.TLP_Icons.Controls.Add(this.PB_Health, 5, 0);
			this.TLP_Icons.Controls.Add(this.PB_Download, 6, 0);
			this.TLP_Icons.Controls.Add(this.PB_Size, 4, 0);
			this.TLP_Icons.Controls.Add(this.PB_Res, 3, 0);
			this.TLP_Icons.Controls.Add(this.PB_Sound, 2, 0);
			this.TLP_Icons.Controls.Add(this.PB_Subs, 1, 0);
			this.TLP_Icons.Controls.Add(this.PB_Label, 0, 0);
			this.TLP_Icons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Icons.Enabled = false;
			this.TLP_Icons.Location = new System.Drawing.Point(10, 132);
			this.TLP_Icons.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Icons.Name = "TLP_Icons";
			this.TLP_Icons.RowCount = 1;
			this.TLP_Icons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Icons.Size = new System.Drawing.Size(753, 30);
			this.TLP_Icons.TabIndex = 17;
			// 
			// TLP_QualitySelection
			// 
			this.TLP_QualitySelection.ColumnCount = 7;
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.TLP_QualitySelection.Controls.Add(this.L_1080p, 3, 0);
			this.TLP_QualitySelection.Controls.Add(this.L_720p, 2, 0);
			this.TLP_QualitySelection.Controls.Add(this.L_Low, 1, 0);
			this.TLP_QualitySelection.Controls.Add(this.L_3D, 5, 0);
			this.TLP_QualitySelection.Controls.Add(this.L_4K, 4, 0);
			this.TLP_QualitySelection.Controls.Add(this.L_AllDownloads, 6, 0);
			this.TLP_QualitySelection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_QualitySelection.Location = new System.Drawing.Point(10, 101);
			this.TLP_QualitySelection.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_QualitySelection.Name = "TLP_QualitySelection";
			this.TLP_QualitySelection.RowCount = 1;
			this.TLP_QualitySelection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_QualitySelection.Size = new System.Drawing.Size(753, 30);
			this.TLP_QualitySelection.TabIndex = 4;
			// 
			// P_Spacer_3
			// 
			this.P_Spacer_3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Spacer_3.Location = new System.Drawing.Point(20, 162);
			this.P_Spacer_3.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.P_Spacer_3.Name = "P_Spacer_3";
			this.P_Spacer_3.Size = new System.Drawing.Size(733, 1);
			this.P_Spacer_3.TabIndex = 19;
			// 
			// PB_Loader2
			// 
			this.PB_Loader2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PB_Loader2.Image = null;
			this.PB_Loader2.Loading = true;
			this.PB_Loader2.Location = new System.Drawing.Point(736, 30);
			this.PB_Loader2.Name = "PB_Loader2";
			this.PB_Loader2.Size = new System.Drawing.Size(32, 32);
			this.PB_Loader2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Loader2.TabIndex = 15;
			this.PB_Loader2.TabStop = false;
			this.PB_Loader2.Visible = false;
			// 
			// I_MoreInfo
			// 
			this.I_MoreInfo.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.I_MoreInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_MoreInfo.Image = global::ShowsCalendar.Properties.Resources.ArrowDown;
			this.I_MoreInfo.Location = new System.Drawing.Point(328, 3);
			this.I_MoreInfo.Margin = new System.Windows.Forms.Padding(0);
			this.I_MoreInfo.Name = "I_MoreInfo";
			this.I_MoreInfo.Size = new System.Drawing.Size(16, 16);
			this.I_MoreInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.I_MoreInfo.TabIndex = 3;
			this.I_MoreInfo.TabStop = false;
			this.I_MoreInfo.Click += new System.EventHandler(this.L_MoreInfo_Click);
			this.I_MoreInfo.MouseEnter += new System.EventHandler(this.L_MoreInfo_MouseEnter);
			this.I_MoreInfo.MouseLeave += new System.EventHandler(this.L_MoreInfo_MouseLeave);
			// 
			// PB_Image
			// 
			this.PB_Image.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_Image.Image = null;
			this.PB_Image.Location = new System.Drawing.Point(10, 0);
			this.PB_Image.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Image.Name = "PB_Image";
			this.PB_Image.Size = new System.Drawing.Size(753, 100);
			this.PB_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Image.TabIndex = 24;
			this.PB_Image.TabStop = false;
			this.PB_Image.UserDraw = true;
			this.PB_Image.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_Image_Paint);
			// 
			// PB_Loader
			// 
			this.PB_Loader.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.PB_Loader.Image = null;
			this.PB_Loader.Loading = true;
			this.PB_Loader.Location = new System.Drawing.Point(352, 98);
			this.PB_Loader.Name = "PB_Loader";
			this.PB_Loader.Size = new System.Drawing.Size(48, 48);
			this.PB_Loader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Loader.TabIndex = 7;
			this.PB_Loader.TabStop = false;
			// 
			// PB_Health
			// 
			this.PB_Health.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Health.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_Health.Image = global::ShowsCalendar.Properties.Resources.Tiny_Health;
			this.PB_Health.Location = new System.Drawing.Point(673, 0);
			this.PB_Health.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Health.Name = "PB_Health";
			this.PB_Health.Size = new System.Drawing.Size(40, 30);
			this.PB_Health.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.PB_Health.TabIndex = 11;
			this.PB_Health.TabStop = false;
			this.PB_Health.Click += new System.EventHandler(this.PB_Health_Click);
			this.PB_Health.MouseEnter += new System.EventHandler(this.PB_Label_MouseEnter);
			this.PB_Health.MouseLeave += new System.EventHandler(this.PB_Label_MouseLeave);
			// 
			// PB_Download
			// 
			this.PB_Download.Cursor = System.Windows.Forms.Cursors.Default;
			this.PB_Download.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_Download.Image = global::ShowsCalendar.Properties.Resources.Tiny_CloudDownload;
			this.PB_Download.Location = new System.Drawing.Point(713, 0);
			this.PB_Download.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Download.Name = "PB_Download";
			this.PB_Download.Size = new System.Drawing.Size(40, 30);
			this.PB_Download.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.PB_Download.TabIndex = 10;
			this.PB_Download.TabStop = false;
			// 
			// PB_Size
			// 
			this.PB_Size.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Size.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_Size.Image = global::ShowsCalendar.Properties.Resources.Tiny_Storage;
			this.PB_Size.Location = new System.Drawing.Point(608, 0);
			this.PB_Size.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Size.Name = "PB_Size";
			this.PB_Size.Size = new System.Drawing.Size(65, 30);
			this.PB_Size.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.PB_Size.TabIndex = 9;
			this.PB_Size.TabStop = false;
			this.PB_Size.Click += new System.EventHandler(this.PB_Size_Click);
			this.PB_Size.MouseEnter += new System.EventHandler(this.PB_Label_MouseEnter);
			this.PB_Size.MouseLeave += new System.EventHandler(this.PB_Label_MouseLeave);
			// 
			// PB_Res
			// 
			this.PB_Res.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Res.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_Res.Image = global::ShowsCalendar.Properties.Resources.Tiny_Resolution;
			this.PB_Res.Location = new System.Drawing.Point(543, 0);
			this.PB_Res.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Res.Name = "PB_Res";
			this.PB_Res.Size = new System.Drawing.Size(65, 30);
			this.PB_Res.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.PB_Res.TabIndex = 10;
			this.PB_Res.TabStop = false;
			this.PB_Res.Click += new System.EventHandler(this.PB_Res_Click);
			this.PB_Res.MouseEnter += new System.EventHandler(this.PB_Label_MouseEnter);
			this.PB_Res.MouseLeave += new System.EventHandler(this.PB_Label_MouseLeave);
			// 
			// PB_Sound
			// 
			this.PB_Sound.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Sound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_Sound.Image = global::ShowsCalendar.Properties.Resources.Tiny_Sound;
			this.PB_Sound.Location = new System.Drawing.Point(478, 0);
			this.PB_Sound.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Sound.Name = "PB_Sound";
			this.PB_Sound.Size = new System.Drawing.Size(65, 30);
			this.PB_Sound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.PB_Sound.TabIndex = 10;
			this.PB_Sound.TabStop = false;
			this.PB_Sound.Click += new System.EventHandler(this.PB_Sound_Click);
			this.PB_Sound.MouseEnter += new System.EventHandler(this.PB_Label_MouseEnter);
			this.PB_Sound.MouseLeave += new System.EventHandler(this.PB_Label_MouseLeave);
			// 
			// PB_Subs
			// 
			this.PB_Subs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Subs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_Subs.Image = global::ShowsCalendar.Properties.Resources.Tiny_CC;
			this.PB_Subs.Location = new System.Drawing.Point(413, 0);
			this.PB_Subs.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Subs.Name = "PB_Subs";
			this.PB_Subs.Size = new System.Drawing.Size(65, 30);
			this.PB_Subs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.PB_Subs.TabIndex = 10;
			this.PB_Subs.TabStop = false;
			this.PB_Subs.Click += new System.EventHandler(this.PB_Subs_Click);
			this.PB_Subs.MouseEnter += new System.EventHandler(this.PB_Label_MouseEnter);
			this.PB_Subs.MouseLeave += new System.EventHandler(this.PB_Label_MouseLeave);
			// 
			// PB_Label
			// 
			this.PB_Label.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Label.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_Label.Image = global::ShowsCalendar.Properties.Resources.Tiny_Label;
			this.PB_Label.Location = new System.Drawing.Point(0, 0);
			this.PB_Label.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Label.Name = "PB_Label";
			this.PB_Label.Size = new System.Drawing.Size(413, 30);
			this.PB_Label.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.PB_Label.TabIndex = 10;
			this.PB_Label.TabStop = false;
			this.PB_Label.Click += new System.EventHandler(this.PB_Label_Click);
			this.PB_Label.MouseEnter += new System.EventHandler(this.PB_Label_MouseEnter);
			this.PB_Label.MouseLeave += new System.EventHandler(this.PB_Label_MouseLeave);
			// 
			// L_1080p
			// 
			this.L_1080p.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_1080p.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_1080p.Enabled = false;
			this.L_1080p.Image = global::ShowsCalendar.Properties.Resources.Tiny_1080;
			this.L_1080p.Location = new System.Drawing.Point(543, 0);
			this.L_1080p.Margin = new System.Windows.Forms.Padding(0);
			this.L_1080p.Name = "L_1080p";
			this.L_1080p.Size = new System.Drawing.Size(65, 30);
			this.L_1080p.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.L_1080p.TabIndex = 16;
			this.L_1080p.TabStop = false;
			this.L_1080p.Click += new System.EventHandler(this.Quality_Click);
			this.L_1080p.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
			this.L_1080p.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
			// 
			// L_720p
			// 
			this.L_720p.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_720p.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_720p.Enabled = false;
			this.L_720p.Image = global::ShowsCalendar.Properties.Resources.Tiny_720;
			this.L_720p.Location = new System.Drawing.Point(478, 0);
			this.L_720p.Margin = new System.Windows.Forms.Padding(0);
			this.L_720p.Name = "L_720p";
			this.L_720p.Size = new System.Drawing.Size(65, 30);
			this.L_720p.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.L_720p.TabIndex = 15;
			this.L_720p.TabStop = false;
			this.L_720p.Click += new System.EventHandler(this.Quality_Click);
			this.L_720p.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
			this.L_720p.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
			// 
			// L_Low
			// 
			this.L_Low.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Low.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_Low.Enabled = false;
			this.L_Low.Image = global::ShowsCalendar.Properties.Resources.Tiny_SD;
			this.L_Low.Location = new System.Drawing.Point(413, 0);
			this.L_Low.Margin = new System.Windows.Forms.Padding(0);
			this.L_Low.Name = "L_Low";
			this.L_Low.Size = new System.Drawing.Size(65, 30);
			this.L_Low.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.L_Low.TabIndex = 14;
			this.L_Low.TabStop = false;
			this.L_Low.Click += new System.EventHandler(this.Quality_Click);
			this.L_Low.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
			this.L_Low.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
			// 
			// L_3D
			// 
			this.L_3D.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_3D.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_3D.Enabled = false;
			this.L_3D.Image = global::ShowsCalendar.Properties.Resources.Tiny_3D;
			this.L_3D.Location = new System.Drawing.Point(673, 0);
			this.L_3D.Margin = new System.Windows.Forms.Padding(0);
			this.L_3D.Name = "L_3D";
			this.L_3D.Size = new System.Drawing.Size(40, 30);
			this.L_3D.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.L_3D.TabIndex = 13;
			this.L_3D.TabStop = false;
			this.L_3D.Click += new System.EventHandler(this.Quality_Click);
			this.L_3D.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
			this.L_3D.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
			// 
			// L_4K
			// 
			this.L_4K.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_4K.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_4K.Enabled = false;
			this.L_4K.Image = global::ShowsCalendar.Properties.Resources.Tiny_4K;
			this.L_4K.Location = new System.Drawing.Point(608, 0);
			this.L_4K.Margin = new System.Windows.Forms.Padding(0);
			this.L_4K.Name = "L_4K";
			this.L_4K.Size = new System.Drawing.Size(65, 30);
			this.L_4K.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.L_4K.TabIndex = 12;
			this.L_4K.TabStop = false;
			this.L_4K.Click += new System.EventHandler(this.Quality_Click);
			this.L_4K.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
			this.L_4K.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
			// 
			// L_AllDownloads
			// 
			this.L_AllDownloads.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_AllDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_AllDownloads.Enabled = false;
			this.L_AllDownloads.Image = global::ShowsCalendar.Properties.Resources.Tiny_All;
			this.L_AllDownloads.Location = new System.Drawing.Point(713, 0);
			this.L_AllDownloads.Margin = new System.Windows.Forms.Padding(0);
			this.L_AllDownloads.Name = "L_AllDownloads";
			this.L_AllDownloads.Size = new System.Drawing.Size(40, 30);
			this.L_AllDownloads.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.L_AllDownloads.TabIndex = 11;
			this.L_AllDownloads.TabStop = false;
			this.L_AllDownloads.Click += new System.EventHandler(this.Quality_Click);
			this.L_AllDownloads.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
			this.L_AllDownloads.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
			// 
			// PC_Download
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.PB_Loader2);
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.TLP_Main);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_Download";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Text = "Download Episode";
			this.Load += new System.EventHandler(this.PC_Download_Load);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.TLP_Main, 0);
			this.Controls.SetChildIndex(this.verticalScroll, 0);
			this.Controls.SetChildIndex(this.PB_Loader2, 0);
			this.P_Torrents.ResumeLayout(false);
			this.P_Torrents.PerformLayout();
			this.TLP_MoreTorrents.ResumeLayout(false);
			this.TLP_MoreTorrents.PerformLayout();
			this.TLP_Main.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.TLP_Icons.ResumeLayout(false);
			this.TLP_QualitySelection.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PB_Loader2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.I_MoreInfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Image)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Health)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Download)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Size)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Res)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Sound)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Subs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Label)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.L_1080p)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.L_720p)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.L_Low)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.L_3D)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.L_4K)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.L_AllDownloads)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private SlickControls.SlickScroll verticalScroll;
		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel TLP_Icons;
		private System.Windows.Forms.PictureBox PB_Download;
		private System.Windows.Forms.PictureBox PB_Size;
		private System.Windows.Forms.PictureBox PB_Res;
		private System.Windows.Forms.PictureBox PB_Sound;
		private System.Windows.Forms.PictureBox PB_Subs;
		private System.Windows.Forms.PictureBox PB_Label;
		private System.Windows.Forms.TableLayoutPanel TLP_QualitySelection;
		private System.Windows.Forms.Panel P_Spacer_3;
		private System.Windows.Forms.Panel P_Spacer_1;
		private System.Windows.Forms.Panel P_Spacer_2;
		private System.Windows.Forms.PictureBox L_1080p;
		private System.Windows.Forms.PictureBox L_720p;
		private System.Windows.Forms.PictureBox L_Low;
		private System.Windows.Forms.PictureBox L_3D;
		private System.Windows.Forms.PictureBox L_4K;
		private System.Windows.Forms.PictureBox L_AllDownloads;
		private System.Windows.Forms.Label L_NoResults;
		private SlickControls.SlickPictureBox PB_Image;
		private System.Windows.Forms.PictureBox PB_Health;
		private SlickControls.SlickPictureBox PB_Loader2;
		private System.Windows.Forms.TableLayoutPanel P_Torrents;
		private TorrentTile TorrentsTile;
		private System.Windows.Forms.TableLayoutPanel TLP_MoreTorrents;
		private System.Windows.Forms.Label L_MoreInfo;
		private System.Windows.Forms.PictureBox I_MoreInfo;
		private System.Windows.Forms.Panel P_Spacer_4;
		private SlickControls.SlickPictureBox PB_Loader;
		private TorrentTile OtherTorrentsTile;
	}
}
