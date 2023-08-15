namespace TVShowsCalendar.Forms
{
	partial class LibraryForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LibraryForm));
			this.TLP_Controls = new System.Windows.Forms.TableLayoutPanel();
			this.B_Folders = new SlickControls.Controls.SlickButton();
			this.L_Status = new System.Windows.Forms.Label();
			this.B_Refresh = new SlickControls.Controls.SlickButton();
			this.TLP_OnDeck = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_OnDeck = new System.Windows.Forms.FlowLayoutPanel();
			this.L_OnDeck = new System.Windows.Forms.Label();
			this.FL_OnDeck = new System.Windows.Forms.Label();
			this.P_Line1 = new System.Windows.Forms.Panel();
			this.PB_OnDeck = new System.Windows.Forms.PictureBox();
			this.verticalScroll = new SlickControls.Controls.VerticalScroll();
			this.P_Tabs = new System.Windows.Forms.Panel();
			this.TLP_Rewatch = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_Rewatch = new System.Windows.Forms.FlowLayoutPanel();
			this.L_Rewatch = new System.Windows.Forms.Label();
			this.FL_Rewatch = new System.Windows.Forms.Label();
			this.P_Line4 = new System.Windows.Forms.Panel();
			this.PB_Rewatch = new System.Windows.Forms.PictureBox();
			this.TLP_StartMovies = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_StartMovies = new System.Windows.Forms.FlowLayoutPanel();
			this.L_StartMovies = new System.Windows.Forms.Label();
			this.FL_StartMovies = new System.Windows.Forms.Label();
			this.P_StartMovies = new System.Windows.Forms.Panel();
			this.PB_StartMovies = new System.Windows.Forms.PictureBox();
			this.TLP_Start = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_Start = new System.Windows.Forms.FlowLayoutPanel();
			this.L_Start = new System.Windows.Forms.Label();
			this.FL_StartShows = new System.Windows.Forms.Label();
			this.P_Line3 = new System.Windows.Forms.Panel();
			this.PB_StartShows = new System.Windows.Forms.PictureBox();
			this.TLP_ContinueMovies = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_ContinueMovies = new System.Windows.Forms.FlowLayoutPanel();
			this.L_ContinueMovies = new System.Windows.Forms.Label();
			this.FL_ContinueMovies = new System.Windows.Forms.Label();
			this.P_ContinueMovies = new System.Windows.Forms.Panel();
			this.PB_ContinueMovies = new System.Windows.Forms.PictureBox();
			this.TLP_Continue = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_Continue = new System.Windows.Forms.FlowLayoutPanel();
			this.L_Continue = new System.Windows.Forms.Label();
			this.FL_ContinueEpisodes = new System.Windows.Forms.Label();
			this.P_Line2 = new System.Windows.Forms.Panel();
			this.PB_Continue = new System.Windows.Forms.PictureBox();
			this.base_P_Content.SuspendLayout();
			this.base_P_Controls.SuspendLayout();
			this.TLP_Controls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_Folders)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Refresh)).BeginInit();
			this.TLP_OnDeck.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_OnDeck)).BeginInit();
			this.P_Tabs.SuspendLayout();
			this.TLP_Rewatch.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Rewatch)).BeginInit();
			this.TLP_StartMovies.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_StartMovies)).BeginInit();
			this.TLP_Start.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_StartShows)).BeginInit();
			this.TLP_ContinueMovies.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_ContinueMovies)).BeginInit();
			this.TLP_Continue.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Continue)).BeginInit();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(58)))));
			this.base_P_Content.Controls.Add(this.P_Tabs);
			this.base_P_Content.Controls.Add(this.verticalScroll);
			this.base_P_Content.Location = new System.Drawing.Point(1, 73);
			this.base_P_Content.Size = new System.Drawing.Size(781, 359);
			this.base_P_Content.Resize += new System.EventHandler(this.base_P_Content_Resize);
			// 
			// base_P_Controls
			// 
			this.base_P_Controls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(54)))));
			this.base_P_Controls.Controls.Add(this.TLP_Controls);
			this.base_P_Controls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.base_P_Controls.Size = new System.Drawing.Size(781, 45);
			// 
			// base_P_Top_Spacer
			// 
			this.base_P_Top_Spacer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(89)))));
			this.base_P_Top_Spacer.Location = new System.Drawing.Point(1, 71);
			this.base_P_Top_Spacer.Size = new System.Drawing.Size(781, 2);
			// 
			// TLP_Controls
			// 
			this.TLP_Controls.ColumnCount = 3;
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Controls.Controls.Add(this.B_Folders, 2, 0);
			this.TLP_Controls.Controls.Add(this.L_Status, 0, 0);
			this.TLP_Controls.Controls.Add(this.B_Refresh, 1, 0);
			this.TLP_Controls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Controls.Location = new System.Drawing.Point(0, 0);
			this.TLP_Controls.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Controls.Name = "TLP_Controls";
			this.TLP_Controls.RowCount = 1;
			this.TLP_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Controls.Size = new System.Drawing.Size(781, 45);
			this.TLP_Controls.TabIndex = 0;
			// 
			// B_Folders
			// 
			this.B_Folders.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Folders.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_Folders.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Folders.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Folders.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Folders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.B_Folders.HueShade = null;
			this.B_Folders.IconSize = 16;
			this.B_Folders.Image = global::TVShowsCalendar.Properties.Resources.Icon_Folder;
			this.B_Folders.Location = new System.Drawing.Point(739, 7);
			this.B_Folders.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.B_Folders.Name = "B_Folders";
			this.B_Folders.Padding = new System.Windows.Forms.Padding(10, 5, 6, 5);
			this.B_Folders.Size = new System.Drawing.Size(35, 30);
			this.B_Folders.TabIndex = 10;
			this.B_Folders.Text = "Folders";
			this.B_Folders.Click += new System.EventHandler(this.B_Folders_Click);
			// 
			// L_Status
			// 
			this.L_Status.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Status.AutoSize = true;
			this.L_Status.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_Status.Location = new System.Drawing.Point(358, 14);
			this.L_Status.Margin = new System.Windows.Forms.Padding(100, 0, 3, 0);
			this.L_Status.Name = "L_Status";
			this.L_Status.Size = new System.Drawing.Size(64, 16);
			this.L_Status.TabIndex = 1;
			this.L_Status.Text = "On Deck";
			// 
			// B_Refresh
			// 
			this.B_Refresh.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Refresh.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_Refresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Refresh.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Refresh.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Refresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.B_Refresh.HueShade = null;
			this.B_Refresh.IconSize = 16;
			this.B_Refresh.Image = global::TVShowsCalendar.Properties.Resources.Icon_Retry;
			this.B_Refresh.Location = new System.Drawing.Point(690, 7);
			this.B_Refresh.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.B_Refresh.Name = "B_Refresh";
			this.B_Refresh.Padding = new System.Windows.Forms.Padding(10, 5, 6, 5);
			this.B_Refresh.Size = new System.Drawing.Size(35, 30);
			this.B_Refresh.TabIndex = 10;
			this.B_Refresh.Text = "Folders";
			this.B_Refresh.Click += new System.EventHandler(this.B_Refresh_Click);
			// 
			// TLP_OnDeck
			// 
			this.TLP_OnDeck.AutoSize = true;
			this.TLP_OnDeck.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_OnDeck.ColumnCount = 3;
			this.TLP_OnDeck.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_OnDeck.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_OnDeck.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_OnDeck.Controls.Add(this.FLP_OnDeck, 1, 2);
			this.TLP_OnDeck.Controls.Add(this.L_OnDeck, 1, 0);
			this.TLP_OnDeck.Controls.Add(this.FL_OnDeck, 2, 0);
			this.TLP_OnDeck.Controls.Add(this.P_Line1, 1, 1);
			this.TLP_OnDeck.Controls.Add(this.PB_OnDeck, 0, 0);
			this.TLP_OnDeck.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_OnDeck.Location = new System.Drawing.Point(0, 0);
			this.TLP_OnDeck.Name = "TLP_OnDeck";
			this.TLP_OnDeck.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
			this.TLP_OnDeck.RowCount = 3;
			this.TLP_OnDeck.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.TLP_OnDeck.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_OnDeck.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_OnDeck.Size = new System.Drawing.Size(500, 57);
			this.TLP_OnDeck.TabIndex = 0;
			// 
			// FLP_OnDeck
			// 
			this.FLP_OnDeck.AutoSize = true;
			this.FLP_OnDeck.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_OnDeck.SetColumnSpan(this.FLP_OnDeck, 2);
			this.FLP_OnDeck.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_OnDeck.Location = new System.Drawing.Point(53, 54);
			this.FLP_OnDeck.Name = "FLP_OnDeck";
			this.FLP_OnDeck.Size = new System.Drawing.Size(444, 0);
			this.FLP_OnDeck.TabIndex = 0;
			// 
			// L_OnDeck
			// 
			this.L_OnDeck.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_OnDeck.AutoSize = true;
			this.L_OnDeck.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_OnDeck.Location = new System.Drawing.Point(60, 23);
			this.L_OnDeck.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			this.L_OnDeck.Name = "L_OnDeck";
			this.L_OnDeck.Size = new System.Drawing.Size(79, 19);
			this.L_OnDeck.TabIndex = 1;
			this.L_OnDeck.Text = "On-Deck";
			// 
			// FL_OnDeck
			// 
			this.FL_OnDeck.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.FL_OnDeck.AutoSize = true;
			this.FL_OnDeck.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FL_OnDeck.Location = new System.Drawing.Point(152, 26);
			this.FL_OnDeck.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
			this.FL_OnDeck.Name = "FL_OnDeck";
			this.FL_OnDeck.Size = new System.Drawing.Size(112, 16);
			this.FL_OnDeck.TabIndex = 1;
			this.FL_OnDeck.Text = "Where Were We?";
			// 
			// P_Line1
			// 
			this.TLP_OnDeck.SetColumnSpan(this.P_Line1, 2);
			this.P_Line1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Line1.Location = new System.Drawing.Point(80, 50);
			this.P_Line1.Margin = new System.Windows.Forms.Padding(30, 0, 100, 0);
			this.P_Line1.Name = "P_Line1";
			this.P_Line1.Size = new System.Drawing.Size(320, 1);
			this.P_Line1.TabIndex = 2;
			// 
			// PB_OnDeck
			// 
			this.PB_OnDeck.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.PB_OnDeck.Image = global::TVShowsCalendar.Properties.Resources.Icon_Play;
			this.PB_OnDeck.Location = new System.Drawing.Point(25, 21);
			this.PB_OnDeck.Name = "PB_OnDeck";
			this.PB_OnDeck.Size = new System.Drawing.Size(22, 22);
			this.PB_OnDeck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_OnDeck.TabIndex = 3;
			this.PB_OnDeck.TabStop = false;
			// 
			// verticalScroll
			// 
			this.verticalScroll.BarColor = null;
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = null;
			this.verticalScroll.Location = new System.Drawing.Point(777, 0);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(4, 359);
			this.verticalScroll.TabIndex = 1;
			// 
			// P_Tabs
			// 
			this.P_Tabs.AutoSize = true;
			this.P_Tabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tabs.Controls.Add(this.TLP_Rewatch);
			this.P_Tabs.Controls.Add(this.TLP_StartMovies);
			this.P_Tabs.Controls.Add(this.TLP_Start);
			this.P_Tabs.Controls.Add(this.TLP_ContinueMovies);
			this.P_Tabs.Controls.Add(this.TLP_Continue);
			this.P_Tabs.Controls.Add(this.TLP_OnDeck);
			this.P_Tabs.Location = new System.Drawing.Point(0, 0);
			this.P_Tabs.MinimumSize = new System.Drawing.Size(500, 0);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(500, 342);
			this.P_Tabs.TabIndex = 2;
			// 
			// TLP_Rewatch
			// 
			this.TLP_Rewatch.AutoSize = true;
			this.TLP_Rewatch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Rewatch.ColumnCount = 3;
			this.TLP_Rewatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Rewatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Rewatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Rewatch.Controls.Add(this.FLP_Rewatch, 1, 2);
			this.TLP_Rewatch.Controls.Add(this.L_Rewatch, 1, 0);
			this.TLP_Rewatch.Controls.Add(this.FL_Rewatch, 2, 0);
			this.TLP_Rewatch.Controls.Add(this.P_Line4, 1, 1);
			this.TLP_Rewatch.Controls.Add(this.PB_Rewatch, 0, 0);
			this.TLP_Rewatch.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Rewatch.Location = new System.Drawing.Point(0, 285);
			this.TLP_Rewatch.Name = "TLP_Rewatch";
			this.TLP_Rewatch.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
			this.TLP_Rewatch.RowCount = 3;
			this.TLP_Rewatch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.TLP_Rewatch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_Rewatch.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Rewatch.Size = new System.Drawing.Size(500, 57);
			this.TLP_Rewatch.TabIndex = 3;
			// 
			// FLP_Rewatch
			// 
			this.FLP_Rewatch.AutoSize = true;
			this.FLP_Rewatch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Rewatch.SetColumnSpan(this.FLP_Rewatch, 2);
			this.FLP_Rewatch.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Rewatch.Location = new System.Drawing.Point(53, 54);
			this.FLP_Rewatch.Name = "FLP_Rewatch";
			this.FLP_Rewatch.Size = new System.Drawing.Size(444, 0);
			this.FLP_Rewatch.TabIndex = 0;
			// 
			// L_Rewatch
			// 
			this.L_Rewatch.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Rewatch.AutoSize = true;
			this.L_Rewatch.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_Rewatch.Location = new System.Drawing.Point(60, 23);
			this.L_Rewatch.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			this.L_Rewatch.Name = "L_Rewatch";
			this.L_Rewatch.Size = new System.Drawing.Size(154, 19);
			this.L_Rewatch.TabIndex = 1;
			this.L_Rewatch.Text = "Re-Watch Episodes";
			// 
			// FL_Rewatch
			// 
			this.FL_Rewatch.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.FL_Rewatch.AutoSize = true;
			this.FL_Rewatch.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FL_Rewatch.Location = new System.Drawing.Point(227, 26);
			this.FL_Rewatch.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
			this.FL_Rewatch.Name = "FL_Rewatch";
			this.FL_Rewatch.Size = new System.Drawing.Size(222, 16);
			this.FL_Rewatch.TabIndex = 1;
			this.FL_Rewatch.Text = "Be Sure You Haven\'t Missed Anything";
			// 
			// P_Line4
			// 
			this.TLP_Rewatch.SetColumnSpan(this.P_Line4, 2);
			this.P_Line4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Line4.Location = new System.Drawing.Point(80, 50);
			this.P_Line4.Margin = new System.Windows.Forms.Padding(30, 0, 100, 0);
			this.P_Line4.Name = "P_Line4";
			this.P_Line4.Size = new System.Drawing.Size(320, 1);
			this.P_Line4.TabIndex = 2;
			// 
			// PB_Rewatch
			// 
			this.PB_Rewatch.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.PB_Rewatch.Image = global::TVShowsCalendar.Properties.Resources.Icon_VTV;
			this.PB_Rewatch.Location = new System.Drawing.Point(25, 21);
			this.PB_Rewatch.Name = "PB_Rewatch";
			this.PB_Rewatch.Size = new System.Drawing.Size(22, 22);
			this.PB_Rewatch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Rewatch.TabIndex = 3;
			this.PB_Rewatch.TabStop = false;
			// 
			// TLP_StartMovies
			// 
			this.TLP_StartMovies.AutoSize = true;
			this.TLP_StartMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_StartMovies.ColumnCount = 3;
			this.TLP_StartMovies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_StartMovies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_StartMovies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_StartMovies.Controls.Add(this.FLP_StartMovies, 1, 2);
			this.TLP_StartMovies.Controls.Add(this.L_StartMovies, 1, 0);
			this.TLP_StartMovies.Controls.Add(this.FL_StartMovies, 2, 0);
			this.TLP_StartMovies.Controls.Add(this.P_StartMovies, 1, 1);
			this.TLP_StartMovies.Controls.Add(this.PB_StartMovies, 0, 0);
			this.TLP_StartMovies.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_StartMovies.Location = new System.Drawing.Point(0, 228);
			this.TLP_StartMovies.Name = "TLP_StartMovies";
			this.TLP_StartMovies.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
			this.TLP_StartMovies.RowCount = 3;
			this.TLP_StartMovies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.TLP_StartMovies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_StartMovies.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_StartMovies.Size = new System.Drawing.Size(500, 57);
			this.TLP_StartMovies.TabIndex = 5;
			// 
			// FLP_StartMovies
			// 
			this.FLP_StartMovies.AutoSize = true;
			this.FLP_StartMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_StartMovies.SetColumnSpan(this.FLP_StartMovies, 2);
			this.FLP_StartMovies.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_StartMovies.Location = new System.Drawing.Point(53, 54);
			this.FLP_StartMovies.Name = "FLP_StartMovies";
			this.FLP_StartMovies.Size = new System.Drawing.Size(444, 0);
			this.FLP_StartMovies.TabIndex = 0;
			// 
			// L_StartMovies
			// 
			this.L_StartMovies.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_StartMovies.AutoSize = true;
			this.L_StartMovies.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_StartMovies.Location = new System.Drawing.Point(60, 23);
			this.L_StartMovies.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			this.L_StartMovies.Name = "L_StartMovies";
			this.L_StartMovies.Size = new System.Drawing.Size(98, 19);
			this.L_StartMovies.TabIndex = 1;
			this.L_StartMovies.Text = "Start Movies";
			// 
			// FL_StartMovies
			// 
			this.FL_StartMovies.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.FL_StartMovies.AutoSize = true;
			this.FL_StartMovies.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FL_StartMovies.Location = new System.Drawing.Point(171, 26);
			this.FL_StartMovies.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
			this.FL_StartMovies.Name = "FL_StartMovies";
			this.FL_StartMovies.Size = new System.Drawing.Size(200, 16);
			this.FL_StartMovies.TabIndex = 1;
			this.FL_StartMovies.Text = "Pick Up That Episode You Started";
			// 
			// P_StartMovies
			// 
			this.TLP_StartMovies.SetColumnSpan(this.P_StartMovies, 2);
			this.P_StartMovies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_StartMovies.Location = new System.Drawing.Point(80, 50);
			this.P_StartMovies.Margin = new System.Windows.Forms.Padding(30, 0, 100, 0);
			this.P_StartMovies.Name = "P_StartMovies";
			this.P_StartMovies.Size = new System.Drawing.Size(320, 1);
			this.P_StartMovies.TabIndex = 2;
			// 
			// PB_StartMovies
			// 
			this.PB_StartMovies.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.PB_StartMovies.Image = global::TVShowsCalendar.Properties.Resources.Icon_Movie;
			this.PB_StartMovies.Location = new System.Drawing.Point(25, 21);
			this.PB_StartMovies.Name = "PB_StartMovies";
			this.PB_StartMovies.Size = new System.Drawing.Size(22, 22);
			this.PB_StartMovies.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_StartMovies.TabIndex = 3;
			this.PB_StartMovies.TabStop = false;
			// 
			// TLP_Start
			// 
			this.TLP_Start.AutoSize = true;
			this.TLP_Start.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Start.ColumnCount = 3;
			this.TLP_Start.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Start.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Start.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Start.Controls.Add(this.FLP_Start, 1, 2);
			this.TLP_Start.Controls.Add(this.L_Start, 1, 0);
			this.TLP_Start.Controls.Add(this.FL_StartShows, 2, 0);
			this.TLP_Start.Controls.Add(this.P_Line3, 1, 1);
			this.TLP_Start.Controls.Add(this.PB_StartShows, 0, 0);
			this.TLP_Start.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Start.Location = new System.Drawing.Point(0, 171);
			this.TLP_Start.Name = "TLP_Start";
			this.TLP_Start.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
			this.TLP_Start.RowCount = 3;
			this.TLP_Start.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.TLP_Start.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_Start.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Start.Size = new System.Drawing.Size(500, 57);
			this.TLP_Start.TabIndex = 2;
			// 
			// FLP_Start
			// 
			this.FLP_Start.AutoSize = true;
			this.FLP_Start.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Start.SetColumnSpan(this.FLP_Start, 2);
			this.FLP_Start.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Start.Location = new System.Drawing.Point(53, 54);
			this.FLP_Start.Name = "FLP_Start";
			this.FLP_Start.Size = new System.Drawing.Size(444, 0);
			this.FLP_Start.TabIndex = 0;
			// 
			// L_Start
			// 
			this.L_Start.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Start.AutoSize = true;
			this.L_Start.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_Start.Location = new System.Drawing.Point(60, 23);
			this.L_Start.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			this.L_Start.Name = "L_Start";
			this.L_Start.Size = new System.Drawing.Size(92, 19);
			this.L_Start.TabIndex = 1;
			this.L_Start.Text = "Start Shows";
			// 
			// FL_StartShows
			// 
			this.FL_StartShows.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.FL_StartShows.AutoSize = true;
			this.FL_StartShows.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FL_StartShows.Location = new System.Drawing.Point(165, 26);
			this.FL_StartShows.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
			this.FL_StartShows.Name = "FL_StartShows";
			this.FL_StartShows.Size = new System.Drawing.Size(134, 16);
			this.FL_StartShows.TabIndex = 1;
			this.FL_StartShows.Text = "Start Something New";
			// 
			// P_Line3
			// 
			this.TLP_Start.SetColumnSpan(this.P_Line3, 2);
			this.P_Line3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Line3.Location = new System.Drawing.Point(80, 50);
			this.P_Line3.Margin = new System.Windows.Forms.Padding(30, 0, 100, 0);
			this.P_Line3.Name = "P_Line3";
			this.P_Line3.Size = new System.Drawing.Size(320, 1);
			this.P_Line3.TabIndex = 2;
			// 
			// PB_StartShows
			// 
			this.PB_StartShows.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.PB_StartShows.Image = global::TVShowsCalendar.Properties.Resources.Icon_VTV;
			this.PB_StartShows.Location = new System.Drawing.Point(25, 21);
			this.PB_StartShows.Name = "PB_StartShows";
			this.PB_StartShows.Size = new System.Drawing.Size(22, 22);
			this.PB_StartShows.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_StartShows.TabIndex = 3;
			this.PB_StartShows.TabStop = false;
			// 
			// TLP_ContinueMovies
			// 
			this.TLP_ContinueMovies.AutoSize = true;
			this.TLP_ContinueMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_ContinueMovies.ColumnCount = 3;
			this.TLP_ContinueMovies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_ContinueMovies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_ContinueMovies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_ContinueMovies.Controls.Add(this.FLP_ContinueMovies, 1, 2);
			this.TLP_ContinueMovies.Controls.Add(this.L_ContinueMovies, 1, 0);
			this.TLP_ContinueMovies.Controls.Add(this.FL_ContinueMovies, 2, 0);
			this.TLP_ContinueMovies.Controls.Add(this.P_ContinueMovies, 1, 1);
			this.TLP_ContinueMovies.Controls.Add(this.PB_ContinueMovies, 0, 0);
			this.TLP_ContinueMovies.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_ContinueMovies.Location = new System.Drawing.Point(0, 114);
			this.TLP_ContinueMovies.Name = "TLP_ContinueMovies";
			this.TLP_ContinueMovies.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
			this.TLP_ContinueMovies.RowCount = 3;
			this.TLP_ContinueMovies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.TLP_ContinueMovies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_ContinueMovies.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ContinueMovies.Size = new System.Drawing.Size(500, 57);
			this.TLP_ContinueMovies.TabIndex = 4;
			// 
			// FLP_ContinueMovies
			// 
			this.FLP_ContinueMovies.AutoSize = true;
			this.FLP_ContinueMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_ContinueMovies.SetColumnSpan(this.FLP_ContinueMovies, 2);
			this.FLP_ContinueMovies.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_ContinueMovies.Location = new System.Drawing.Point(53, 54);
			this.FLP_ContinueMovies.Name = "FLP_ContinueMovies";
			this.FLP_ContinueMovies.Size = new System.Drawing.Size(444, 0);
			this.FLP_ContinueMovies.TabIndex = 0;
			// 
			// L_ContinueMovies
			// 
			this.L_ContinueMovies.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_ContinueMovies.AutoSize = true;
			this.L_ContinueMovies.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_ContinueMovies.Location = new System.Drawing.Point(60, 23);
			this.L_ContinueMovies.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			this.L_ContinueMovies.Name = "L_ContinueMovies";
			this.L_ContinueMovies.Size = new System.Drawing.Size(136, 19);
			this.L_ContinueMovies.TabIndex = 1;
			this.L_ContinueMovies.Text = "Continue Movies";
			// 
			// FL_ContinueMovies
			// 
			this.FL_ContinueMovies.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.FL_ContinueMovies.AutoSize = true;
			this.FL_ContinueMovies.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FL_ContinueMovies.Location = new System.Drawing.Point(209, 26);
			this.FL_ContinueMovies.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
			this.FL_ContinueMovies.Name = "FL_ContinueMovies";
			this.FL_ContinueMovies.Size = new System.Drawing.Size(200, 16);
			this.FL_ContinueMovies.TabIndex = 1;
			this.FL_ContinueMovies.Text = "Pick Up That Episode You Started";
			// 
			// P_ContinueMovies
			// 
			this.TLP_ContinueMovies.SetColumnSpan(this.P_ContinueMovies, 2);
			this.P_ContinueMovies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_ContinueMovies.Location = new System.Drawing.Point(80, 50);
			this.P_ContinueMovies.Margin = new System.Windows.Forms.Padding(30, 0, 100, 0);
			this.P_ContinueMovies.Name = "P_ContinueMovies";
			this.P_ContinueMovies.Size = new System.Drawing.Size(320, 1);
			this.P_ContinueMovies.TabIndex = 2;
			// 
			// PB_ContinueMovies
			// 
			this.PB_ContinueMovies.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.PB_ContinueMovies.Image = global::TVShowsCalendar.Properties.Resources.Icon_Movie;
			this.PB_ContinueMovies.Location = new System.Drawing.Point(25, 21);
			this.PB_ContinueMovies.Name = "PB_ContinueMovies";
			this.PB_ContinueMovies.Size = new System.Drawing.Size(22, 22);
			this.PB_ContinueMovies.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_ContinueMovies.TabIndex = 3;
			this.PB_ContinueMovies.TabStop = false;
			// 
			// TLP_Continue
			// 
			this.TLP_Continue.AutoSize = true;
			this.TLP_Continue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Continue.ColumnCount = 3;
			this.TLP_Continue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Continue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Continue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Continue.Controls.Add(this.FLP_Continue, 1, 2);
			this.TLP_Continue.Controls.Add(this.L_Continue, 1, 0);
			this.TLP_Continue.Controls.Add(this.FL_ContinueEpisodes, 2, 0);
			this.TLP_Continue.Controls.Add(this.P_Line2, 1, 1);
			this.TLP_Continue.Controls.Add(this.PB_Continue, 0, 0);
			this.TLP_Continue.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Continue.Location = new System.Drawing.Point(0, 57);
			this.TLP_Continue.Name = "TLP_Continue";
			this.TLP_Continue.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
			this.TLP_Continue.RowCount = 3;
			this.TLP_Continue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.TLP_Continue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_Continue.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Continue.Size = new System.Drawing.Size(500, 57);
			this.TLP_Continue.TabIndex = 1;
			// 
			// FLP_Continue
			// 
			this.FLP_Continue.AutoSize = true;
			this.FLP_Continue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Continue.SetColumnSpan(this.FLP_Continue, 2);
			this.FLP_Continue.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Continue.Location = new System.Drawing.Point(53, 54);
			this.FLP_Continue.Name = "FLP_Continue";
			this.FLP_Continue.Size = new System.Drawing.Size(444, 0);
			this.FLP_Continue.TabIndex = 0;
			// 
			// L_Continue
			// 
			this.L_Continue.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Continue.AutoSize = true;
			this.L_Continue.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_Continue.Location = new System.Drawing.Point(60, 23);
			this.L_Continue.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			this.L_Continue.Name = "L_Continue";
			this.L_Continue.Size = new System.Drawing.Size(149, 19);
			this.L_Continue.TabIndex = 1;
			this.L_Continue.Text = "Continue Episodes";
			// 
			// FL_ContinueEpisodes
			// 
			this.FL_ContinueEpisodes.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.FL_ContinueEpisodes.AutoSize = true;
			this.FL_ContinueEpisodes.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FL_ContinueEpisodes.Location = new System.Drawing.Point(222, 26);
			this.FL_ContinueEpisodes.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
			this.FL_ContinueEpisodes.Name = "FL_ContinueEpisodes";
			this.FL_ContinueEpisodes.Size = new System.Drawing.Size(200, 16);
			this.FL_ContinueEpisodes.TabIndex = 1;
			this.FL_ContinueEpisodes.Text = "Pick Up That Episode You Started";
			// 
			// P_Line2
			// 
			this.TLP_Continue.SetColumnSpan(this.P_Line2, 2);
			this.P_Line2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Line2.Location = new System.Drawing.Point(80, 50);
			this.P_Line2.Margin = new System.Windows.Forms.Padding(30, 0, 100, 0);
			this.P_Line2.Name = "P_Line2";
			this.P_Line2.Size = new System.Drawing.Size(320, 1);
			this.P_Line2.TabIndex = 2;
			// 
			// PB_Continue
			// 
			this.PB_Continue.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.PB_Continue.Image = global::TVShowsCalendar.Properties.Resources.Icon_VTV;
			this.PB_Continue.Location = new System.Drawing.Point(25, 21);
			this.PB_Continue.Name = "PB_Continue";
			this.PB_Continue.Size = new System.Drawing.Size(22, 22);
			this.PB_Continue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Continue.TabIndex = 3;
			this.PB_Continue.TabStop = false;
			// 
			// LibraryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.CloseForm = false;
			this.FormIcon = global::TVShowsCalendar.Properties.Resources.TVCalendar;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LibraryForm";
			this.ShowControls = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Library";
			this.ResizeEnd += new System.EventHandler(this.LibraryForm_ResizeEnd);
			this.base_P_Content.ResumeLayout(false);
			this.base_P_Content.PerformLayout();
			this.base_P_Controls.ResumeLayout(false);
			this.TLP_Controls.ResumeLayout(false);
			this.TLP_Controls.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_Folders)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Refresh)).EndInit();
			this.TLP_OnDeck.ResumeLayout(false);
			this.TLP_OnDeck.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_OnDeck)).EndInit();
			this.P_Tabs.ResumeLayout(false);
			this.P_Tabs.PerformLayout();
			this.TLP_Rewatch.ResumeLayout(false);
			this.TLP_Rewatch.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Rewatch)).EndInit();
			this.TLP_StartMovies.ResumeLayout(false);
			this.TLP_StartMovies.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_StartMovies)).EndInit();
			this.TLP_Start.ResumeLayout(false);
			this.TLP_Start.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_StartShows)).EndInit();
			this.TLP_ContinueMovies.ResumeLayout(false);
			this.TLP_ContinueMovies.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_ContinueMovies)).EndInit();
			this.TLP_Continue.ResumeLayout(false);
			this.TLP_Continue.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Continue)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_Controls;
		private SlickControls.Controls.SlickButton B_Folders;
		private System.Windows.Forms.Panel P_Tabs;
		private System.Windows.Forms.TableLayoutPanel TLP_Start;
		private System.Windows.Forms.FlowLayoutPanel FLP_Start;
		private System.Windows.Forms.Label L_Start;
		private System.Windows.Forms.TableLayoutPanel TLP_Continue;
		private System.Windows.Forms.FlowLayoutPanel FLP_Continue;
		private System.Windows.Forms.Label L_Continue;
		private System.Windows.Forms.TableLayoutPanel TLP_OnDeck;
		private System.Windows.Forms.FlowLayoutPanel FLP_OnDeck;
		private System.Windows.Forms.Label L_OnDeck;
		private SlickControls.Controls.VerticalScroll verticalScroll;
		private System.Windows.Forms.Label L_Status;
		private SlickControls.Controls.SlickButton B_Refresh;
		private System.Windows.Forms.TableLayoutPanel TLP_Rewatch;
		private System.Windows.Forms.FlowLayoutPanel FLP_Rewatch;
		private System.Windows.Forms.Label L_Rewatch;
		private System.Windows.Forms.Label FL_Rewatch;
		private System.Windows.Forms.Label FL_StartShows;
		private System.Windows.Forms.Label FL_ContinueEpisodes;
		private System.Windows.Forms.Label FL_OnDeck;
		private System.Windows.Forms.Panel P_Line4;
		private System.Windows.Forms.Panel P_Line3;
		private System.Windows.Forms.Panel P_Line2;
		private System.Windows.Forms.Panel P_Line1;
		private System.Windows.Forms.TableLayoutPanel TLP_StartMovies;
		private System.Windows.Forms.FlowLayoutPanel FLP_StartMovies;
		private System.Windows.Forms.Label L_StartMovies;
		private System.Windows.Forms.Label FL_StartMovies;
		private System.Windows.Forms.Panel P_StartMovies;
		private System.Windows.Forms.PictureBox PB_StartMovies;
		private System.Windows.Forms.TableLayoutPanel TLP_ContinueMovies;
		private System.Windows.Forms.FlowLayoutPanel FLP_ContinueMovies;
		private System.Windows.Forms.Label L_ContinueMovies;
		private System.Windows.Forms.Label FL_ContinueMovies;
		private System.Windows.Forms.Panel P_ContinueMovies;
		private System.Windows.Forms.PictureBox PB_ContinueMovies;
		private System.Windows.Forms.PictureBox PB_Rewatch;
		private System.Windows.Forms.PictureBox PB_StartShows;
		private System.Windows.Forms.PictureBox PB_Continue;
		private System.Windows.Forms.PictureBox PB_OnDeck;
	}
}