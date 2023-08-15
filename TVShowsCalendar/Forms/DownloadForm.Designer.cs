namespace TVShowsCalendar
{
	partial class DownloadForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadForm));
			this.TLP_Icons = new System.Windows.Forms.TableLayoutPanel();
			this.PB_Download = new System.Windows.Forms.PictureBox();
			this.PB_Size = new System.Windows.Forms.PictureBox();
			this.PB_Res = new System.Windows.Forms.PictureBox();
			this.PB_Sound = new System.Windows.Forms.PictureBox();
			this.PB_Subs = new System.Windows.Forms.PictureBox();
			this.PB_Label = new System.Windows.Forms.PictureBox();
			this.TLP_QualitySelection = new System.Windows.Forms.TableLayoutPanel();
			this.L_AllDownloads = new System.Windows.Forms.Label();
			this.L_3D = new System.Windows.Forms.Label();
			this.L_4K = new System.Windows.Forms.Label();
			this.L_1080p = new System.Windows.Forms.Label();
			this.L_720p = new System.Windows.Forms.Label();
			this.L_Low = new System.Windows.Forms.Label();
			this.L_SelectedQuality = new System.Windows.Forms.Label();
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_Episode = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.verticalScroll = new SlickControls.Controls.VerticalScroll();
			this.panel1 = new System.Windows.Forms.Panel();
			this.TLP_Torrents = new System.Windows.Forms.TableLayoutPanel();
			this.PB_Loader = new System.Windows.Forms.PictureBox();
			this.L_NoResults = new System.Windows.Forms.Label();
			this.base_P_Content.SuspendLayout();
			this.base_P_Controls.SuspendLayout();
			this.TLP_Icons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Download)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Size)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Res)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Sound)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Subs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Label)).BeginInit();
			this.TLP_QualitySelection.SuspendLayout();
			this.TLP_Main.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loader)).BeginInit();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(58)))));
			this.base_P_Content.Controls.Add(this.tableLayoutPanel1);
			this.base_P_Content.Location = new System.Drawing.Point(1, 183);
			this.base_P_Content.Size = new System.Drawing.Size(706, 324);
			// 
			// base_P_Controls
			// 
			this.base_P_Controls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(55)))), ((int)(((byte)(68)))));
			this.base_P_Controls.Controls.Add(this.TLP_Main);
			this.base_P_Controls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.base_P_Controls.Size = new System.Drawing.Size(706, 155);
			// 
			// base_P_Top_Spacer
			// 
			this.base_P_Top_Spacer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(89)))));
			this.base_P_Top_Spacer.Location = new System.Drawing.Point(1, 181);
			this.base_P_Top_Spacer.Size = new System.Drawing.Size(706, 2);
			// 
			// TLP_Icons
			// 
			this.TLP_Icons.ColumnCount = 6;
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_Icons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.TLP_Icons.Controls.Add(this.PB_Download, 5, 0);
			this.TLP_Icons.Controls.Add(this.PB_Size, 4, 0);
			this.TLP_Icons.Controls.Add(this.PB_Res, 3, 0);
			this.TLP_Icons.Controls.Add(this.PB_Sound, 2, 0);
			this.TLP_Icons.Controls.Add(this.PB_Subs, 1, 0);
			this.TLP_Icons.Controls.Add(this.PB_Label, 0, 0);
			this.TLP_Icons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Icons.Location = new System.Drawing.Point(0, 125);
			this.TLP_Icons.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Icons.Name = "TLP_Icons";
			this.TLP_Icons.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
			this.TLP_Icons.RowCount = 1;
			this.TLP_Icons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Icons.Size = new System.Drawing.Size(706, 30);
			this.TLP_Icons.TabIndex = 17;
			// 
			// PB_Download
			// 
			this.PB_Download.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.PB_Download.Cursor = System.Windows.Forms.Cursors.Default;
			this.PB_Download.Image = global::TVShowsCalendar.Properties.Resources.Icon_Download;
			this.PB_Download.Location = new System.Drawing.Point(621, 5);
			this.PB_Download.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Download.Name = "PB_Download";
			this.PB_Download.Size = new System.Drawing.Size(20, 20);
			this.PB_Download.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Download.TabIndex = 10;
			this.PB_Download.TabStop = false;
			// 
			// PB_Size
			// 
			this.PB_Size.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.PB_Size.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Size.Image = global::TVShowsCalendar.Properties.Resources.Icon_HDD;
			this.PB_Size.Location = new System.Drawing.Point(528, 5);
			this.PB_Size.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Size.Name = "PB_Size";
			this.PB_Size.Size = new System.Drawing.Size(20, 20);
			this.PB_Size.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Size.TabIndex = 9;
			this.PB_Size.TabStop = false;
			this.PB_Size.Click += new System.EventHandler(this.PB_Size_Click);
			this.PB_Size.MouseEnter += new System.EventHandler(this.PB_Label_MouseEnter);
			this.PB_Size.MouseLeave += new System.EventHandler(this.PB_Label_MouseLeave);
			// 
			// PB_Res
			// 
			this.PB_Res.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.PB_Res.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Res.Image = global::TVShowsCalendar.Properties.Resources.Icon_Resolution;
			this.PB_Res.Location = new System.Drawing.Point(466, 5);
			this.PB_Res.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Res.Name = "PB_Res";
			this.PB_Res.Size = new System.Drawing.Size(20, 20);
			this.PB_Res.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Res.TabIndex = 10;
			this.PB_Res.TabStop = false;
			this.PB_Res.Click += new System.EventHandler(this.PB_Res_Click);
			this.PB_Res.MouseEnter += new System.EventHandler(this.PB_Label_MouseEnter);
			this.PB_Res.MouseLeave += new System.EventHandler(this.PB_Label_MouseLeave);
			// 
			// PB_Sound
			// 
			this.PB_Sound.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.PB_Sound.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Sound.Image = global::TVShowsCalendar.Properties.Resources.Icon_Volume;
			this.PB_Sound.Location = new System.Drawing.Point(416, 5);
			this.PB_Sound.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Sound.Name = "PB_Sound";
			this.PB_Sound.Size = new System.Drawing.Size(20, 20);
			this.PB_Sound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Sound.TabIndex = 10;
			this.PB_Sound.TabStop = false;
			this.PB_Sound.Click += new System.EventHandler(this.PB_Sound_Click);
			this.PB_Sound.MouseEnter += new System.EventHandler(this.PB_Label_MouseEnter);
			this.PB_Sound.MouseLeave += new System.EventHandler(this.PB_Label_MouseLeave);
			// 
			// PB_Subs
			// 
			this.PB_Subs.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.PB_Subs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Subs.Image = global::TVShowsCalendar.Properties.Resources.Icon_Subs;
			this.PB_Subs.Location = new System.Drawing.Point(368, 5);
			this.PB_Subs.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Subs.Name = "PB_Subs";
			this.PB_Subs.Size = new System.Drawing.Size(20, 20);
			this.PB_Subs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Subs.TabIndex = 10;
			this.PB_Subs.TabStop = false;
			this.PB_Subs.Click += new System.EventHandler(this.PB_Subs_Click);
			this.PB_Subs.MouseEnter += new System.EventHandler(this.PB_Label_MouseEnter);
			this.PB_Subs.MouseLeave += new System.EventHandler(this.PB_Label_MouseLeave);
			// 
			// PB_Label
			// 
			this.PB_Label.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.PB_Label.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Label.Image = global::TVShowsCalendar.Properties.Resources.Icon_Label;
			this.PB_Label.Location = new System.Drawing.Point(165, 5);
			this.PB_Label.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Label.Name = "PB_Label";
			this.PB_Label.Size = new System.Drawing.Size(20, 20);
			this.PB_Label.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Label.TabIndex = 10;
			this.PB_Label.TabStop = false;
			this.PB_Label.Click += new System.EventHandler(this.PB_Label_Click);
			this.PB_Label.MouseEnter += new System.EventHandler(this.PB_Label_MouseEnter);
			this.PB_Label.MouseLeave += new System.EventHandler(this.PB_Label_MouseLeave);
			// 
			// TLP_QualitySelection
			// 
			this.TLP_QualitySelection.ColumnCount = 7;
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_QualitySelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TLP_QualitySelection.Controls.Add(this.L_AllDownloads, 6, 0);
			this.TLP_QualitySelection.Controls.Add(this.L_3D, 5, 0);
			this.TLP_QualitySelection.Controls.Add(this.L_4K, 4, 0);
			this.TLP_QualitySelection.Controls.Add(this.L_1080p, 3, 0);
			this.TLP_QualitySelection.Controls.Add(this.L_720p, 2, 0);
			this.TLP_QualitySelection.Controls.Add(this.L_Low, 1, 0);
			this.TLP_QualitySelection.Controls.Add(this.L_SelectedQuality, 0, 0);
			this.TLP_QualitySelection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_QualitySelection.Location = new System.Drawing.Point(0, 95);
			this.TLP_QualitySelection.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_QualitySelection.Name = "TLP_QualitySelection";
			this.TLP_QualitySelection.RowCount = 1;
			this.TLP_QualitySelection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_QualitySelection.Size = new System.Drawing.Size(706, 30);
			this.TLP_QualitySelection.TabIndex = 4;
			// 
			// L_AllDownloads
			// 
			this.L_AllDownloads.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_AllDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_AllDownloads.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_AllDownloads.Location = new System.Drawing.Point(641, 0);
			this.L_AllDownloads.Margin = new System.Windows.Forms.Padding(0);
			this.L_AllDownloads.Name = "L_AllDownloads";
			this.L_AllDownloads.Size = new System.Drawing.Size(65, 30);
			this.L_AllDownloads.TabIndex = 16;
			this.L_AllDownloads.Text = "All";
			this.L_AllDownloads.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.L_AllDownloads.Click += new System.EventHandler(this.Quality_Click);
			// 
			// L_3D
			// 
			this.L_3D.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_3D.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_3D.Font = new System.Drawing.Font("Century Gothic", 9.25F);
			this.L_3D.Location = new System.Drawing.Point(576, 0);
			this.L_3D.Margin = new System.Windows.Forms.Padding(0);
			this.L_3D.Name = "L_3D";
			this.L_3D.Size = new System.Drawing.Size(65, 30);
			this.L_3D.TabIndex = 11;
			this.L_3D.Text = "3D";
			this.L_3D.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_4K
			// 
			this.L_4K.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_4K.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_4K.Font = new System.Drawing.Font("Century Gothic", 9.25F);
			this.L_4K.Location = new System.Drawing.Point(511, 0);
			this.L_4K.Margin = new System.Windows.Forms.Padding(0);
			this.L_4K.Name = "L_4K";
			this.L_4K.Size = new System.Drawing.Size(65, 30);
			this.L_4K.TabIndex = 12;
			this.L_4K.Text = "4K Ultra";
			this.L_4K.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_1080p
			// 
			this.L_1080p.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_1080p.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_1080p.Font = new System.Drawing.Font("Century Gothic", 9.25F);
			this.L_1080p.Location = new System.Drawing.Point(446, 0);
			this.L_1080p.Margin = new System.Windows.Forms.Padding(0);
			this.L_1080p.Name = "L_1080p";
			this.L_1080p.Size = new System.Drawing.Size(65, 30);
			this.L_1080p.TabIndex = 13;
			this.L_1080p.Text = "1080p";
			this.L_1080p.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_720p
			// 
			this.L_720p.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_720p.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_720p.Font = new System.Drawing.Font("Century Gothic", 9.25F);
			this.L_720p.Location = new System.Drawing.Point(381, 0);
			this.L_720p.Margin = new System.Windows.Forms.Padding(0);
			this.L_720p.Name = "L_720p";
			this.L_720p.Size = new System.Drawing.Size(65, 30);
			this.L_720p.TabIndex = 14;
			this.L_720p.Text = "720p";
			this.L_720p.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_Low
			// 
			this.L_Low.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Low.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_Low.Font = new System.Drawing.Font("Century Gothic", 9.25F);
			this.L_Low.Location = new System.Drawing.Point(316, 0);
			this.L_Low.Margin = new System.Windows.Forms.Padding(0);
			this.L_Low.Name = "L_Low";
			this.L_Low.Size = new System.Drawing.Size(65, 30);
			this.L_Low.TabIndex = 15;
			this.L_Low.Text = "Low";
			this.L_Low.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_SelectedQuality
			// 
			this.L_SelectedQuality.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_SelectedQuality.AutoSize = true;
			this.L_SelectedQuality.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_SelectedQuality.Location = new System.Drawing.Point(45, 7);
			this.L_SelectedQuality.Margin = new System.Windows.Forms.Padding(45, 0, 3, 0);
			this.L_SelectedQuality.Name = "L_SelectedQuality";
			this.L_SelectedQuality.Size = new System.Drawing.Size(117, 16);
			this.L_SelectedQuality.TabIndex = 0;
			this.L_SelectedQuality.Text = "Selected Quality:";
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 1;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Controls.Add(this.TLP_Icons, 0, 2);
			this.TLP_Main.Controls.Add(this.TLP_QualitySelection, 0, 1);
			this.TLP_Main.Controls.Add(this.TLP_Episode, 0, 0);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 3;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.Size = new System.Drawing.Size(706, 155);
			this.TLP_Main.TabIndex = 7;
			// 
			// TLP_Episode
			// 
			this.TLP_Episode.ColumnCount = 1;
			this.TLP_Episode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Episode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Episode.Location = new System.Drawing.Point(0, 0);
			this.TLP_Episode.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Episode.Name = "TLP_Episode";
			this.TLP_Episode.RowCount = 1;
			this.TLP_Episode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Episode.Size = new System.Drawing.Size(706, 95);
			this.TLP_Episode.TabIndex = 9;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
			this.tableLayoutPanel1.Controls.Add(this.verticalScroll, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(706, 324);
			this.tableLayoutPanel1.TabIndex = 8;
			// 
			// verticalScroll
			// 
			this.verticalScroll.BarColor = null;
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = null;
			this.verticalScroll.Location = new System.Drawing.Point(701, 0);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(4, 324);
			this.verticalScroll.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.TLP_Torrents);
			this.panel1.Controls.Add(this.PB_Loader);
			this.panel1.Controls.Add(this.L_NoResults);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(15, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(676, 324);
			this.panel1.TabIndex = 1;
			this.panel1.Layout += new System.Windows.Forms.LayoutEventHandler(this.TLP_Torrents_Layout);
			// 
			// TLP_Torrents
			// 
			this.TLP_Torrents.AutoSize = true;
			this.TLP_Torrents.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Torrents.ColumnCount = 1;
			this.TLP_Torrents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Torrents.Location = new System.Drawing.Point(0, 0);
			this.TLP_Torrents.Name = "TLP_Torrents";
			this.TLP_Torrents.RowCount = 1;
			this.TLP_Torrents.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.TLP_Torrents.Size = new System.Drawing.Size(0, 35);
			this.TLP_Torrents.TabIndex = 1;
			// 
			// PB_Loader
			// 
			this.PB_Loader.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.PB_Loader.Image = ((System.Drawing.Image)(resources.GetObject("PB_Loader.Image")));
			this.PB_Loader.Location = new System.Drawing.Point(324, 52);
			this.PB_Loader.Name = "PB_Loader";
			this.PB_Loader.Size = new System.Drawing.Size(32, 32);
			this.PB_Loader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Loader.TabIndex = 0;
			this.PB_Loader.TabStop = false;
			// 
			// L_NoResults
			// 
			this.L_NoResults.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.L_NoResults.AutoSize = true;
			this.L_NoResults.Font = new System.Drawing.Font("Century Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_NoResults.Location = new System.Drawing.Point(294, 55);
			this.L_NoResults.Margin = new System.Windows.Forms.Padding(45, 0, 3, 0);
			this.L_NoResults.Name = "L_NoResults";
			this.L_NoResults.Size = new System.Drawing.Size(89, 21);
			this.L_NoResults.TabIndex = 0;
			this.L_NoResults.Text = "No Results";
			this.L_NoResults.Visible = false;
			// 
			// DownloadForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(725, 525);
			this.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormIcon = global::TVShowsCalendar.Properties.Resources.TVCalendar;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(625, 400);
			this.Name = "DownloadForm";
			this.ShowControls = true;
			this.Text = "Download Form";
			this.base_P_Content.ResumeLayout(false);
			this.base_P_Controls.ResumeLayout(false);
			this.TLP_Icons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PB_Download)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Size)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Res)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Sound)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Subs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Label)).EndInit();
			this.TLP_QualitySelection.ResumeLayout(false);
			this.TLP_QualitySelection.PerformLayout();
			this.TLP_Main.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loader)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.PictureBox PB_Label;
		private System.Windows.Forms.PictureBox PB_Subs;
		private System.Windows.Forms.PictureBox PB_Sound;
		private System.Windows.Forms.PictureBox PB_Res;
		private System.Windows.Forms.PictureBox PB_Size;
		private System.Windows.Forms.PictureBox PB_Download;
		private System.Windows.Forms.Label L_SelectedQuality;
		private System.Windows.Forms.Label L_Low;
		private System.Windows.Forms.Label L_720p;
		private System.Windows.Forms.Label L_1080p;
		private System.Windows.Forms.Label L_4K;
		private System.Windows.Forms.Label L_3D;
		private System.Windows.Forms.Label L_AllDownloads;
		private System.Windows.Forms.TableLayoutPanel TLP_Icons;
		private System.Windows.Forms.TableLayoutPanel TLP_QualitySelection;
		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private SlickControls.Controls.VerticalScroll verticalScroll;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel TLP_Episode;
		private System.Windows.Forms.PictureBox PB_Loader;
		private System.Windows.Forms.Label L_NoResults;
		private System.Windows.Forms.TableLayoutPanel TLP_Torrents;
	}
}