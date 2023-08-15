namespace TVShowsCalendar
{
	partial class ShowForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowForm));
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.TLP_Mains = new System.Windows.Forms.TableLayoutPanel();
			this.TB_Search = new SlickControls.Controls.SlickTextBox();
			this.B_AddShow = new SlickControls.Controls.SlickButton();
			this.B_Discover = new SlickControls.Controls.SlickLabel();
			this.B_Sort = new SlickControls.Controls.SlickLabel();
			this.B_Folders = new SlickControls.Controls.SlickLabel();
			this.ML_Clear = new SlickControls.Controls.SlickLabel();
			this.P_ContentShows = new System.Windows.Forms.Panel();
			this.verticalScroll = new SlickControls.Controls.VerticalScroll();
			this.TLP_TileContainer = new System.Windows.Forms.TableLayoutPanel();
			this.L_NoShows = new System.Windows.Forms.Label();
			this.FLP_Tiles = new System.Windows.Forms.FlowLayoutPanel();
			this.base_P_Content.SuspendLayout();
			this.base_P_Controls.SuspendLayout();
			this.TLP_Mains.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_AddShow)).BeginInit();
			this.P_ContentShows.SuspendLayout();
			this.TLP_TileContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(58)))));
			this.base_P_Content.Controls.Add(this.P_ContentShows);
			this.base_P_Content.Location = new System.Drawing.Point(1, 73);
			this.base_P_Content.Size = new System.Drawing.Size(881, 409);
			// 
			// base_P_Controls
			// 
			this.base_P_Controls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(54)))));
			this.base_P_Controls.Controls.Add(this.TLP_Mains);
			this.base_P_Controls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.base_P_Controls.Size = new System.Drawing.Size(881, 45);
			// 
			// base_P_Top_Spacer
			// 
			this.base_P_Top_Spacer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(89)))));
			this.base_P_Top_Spacer.Location = new System.Drawing.Point(1, 71);
			this.base_P_Top_Spacer.Size = new System.Drawing.Size(881, 2);
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 20000;
			this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.toolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(65)))), ((int)(((byte)(77)))));
			this.toolTip.InitialDelay = 600;
			this.toolTip.ReshowDelay = 100;
			// 
			// TLP_Mains
			// 
			this.TLP_Mains.ColumnCount = 7;
			this.TLP_Mains.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Mains.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Mains.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Mains.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Mains.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Mains.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Mains.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Mains.Controls.Add(this.TB_Search, 0, 0);
			this.TLP_Mains.Controls.Add(this.B_AddShow, 6, 0);
			this.TLP_Mains.Controls.Add(this.B_Discover, 5, 0);
			this.TLP_Mains.Controls.Add(this.B_Sort, 3, 0);
			this.TLP_Mains.Controls.Add(this.B_Folders, 4, 0);
			this.TLP_Mains.Controls.Add(this.ML_Clear, 1, 0);
			this.TLP_Mains.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Mains.Location = new System.Drawing.Point(0, 0);
			this.TLP_Mains.Name = "TLP_Mains";
			this.TLP_Mains.RowCount = 1;
			this.TLP_Mains.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Mains.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Mains.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Mains.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Mains.Size = new System.Drawing.Size(881, 45);
			this.TLP_Mains.TabIndex = 1;
			// 
			// TB_Search
			// 
			this.TB_Search.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.TB_Search.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TB_Search.Image = null;
			this.TB_Search.LabelText = "Search";
			this.TB_Search.Location = new System.Drawing.Point(30, 5);
			this.TB_Search.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
			this.TB_Search.MaximumSize = new System.Drawing.Size(9999, 34);
			this.TB_Search.MaxLength = 32767;
			this.TB_Search.MinimumSize = new System.Drawing.Size(50, 35);
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Placeholder = "Filter your library";
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.Size = new System.Drawing.Size(486, 35);
			this.TB_Search.TabIndex = 0;
			this.TB_Search.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.TB_Search.Validation = SlickControls.Enums.ValidationType.None;
			this.TB_Search.ValidationRegex = "";
			this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
			// 
			// B_AddShow
			// 
			this.B_AddShow.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_AddShow.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_AddShow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_AddShow.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_AddShow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.B_AddShow.HueShade = null;
			this.B_AddShow.IconSize = 14;
			this.B_AddShow.Image = ((System.Drawing.Image)(resources.GetObject("B_AddShow.Image")));
			this.B_AddShow.Location = new System.Drawing.Point(803, 8);
			this.B_AddShow.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.B_AddShow.Name = "B_AddShow";
			this.B_AddShow.Padding = new System.Windows.Forms.Padding(10, 3, 5, 3);
			this.B_AddShow.Size = new System.Drawing.Size(71, 28);
			this.B_AddShow.TabIndex = 9;
			this.B_AddShow.Text = "Add";
			this.B_AddShow.Click += new System.EventHandler(this.B_AddShow_Click);
			// 
			// B_Discover
			// 
			this.B_Discover.ActiveColor = null;
			this.B_Discover.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Discover.AutoSize = true;
			this.B_Discover.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Discover.Center = false;
			this.B_Discover.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Discover.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Discover.HideText = false;
			this.B_Discover.HoverState = SlickControls.Enums.HoverState.Normal;
			this.B_Discover.IconSize = 14;
			this.B_Discover.Image = ((System.Drawing.Image)(resources.GetObject("B_Discover.Image")));
			this.B_Discover.Location = new System.Drawing.Point(766, 8);
			this.B_Discover.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.B_Discover.Name = "B_Discover";
			this.B_Discover.Padding = new System.Windows.Forms.Padding(5, 7, 5, 7);
			this.B_Discover.Size = new System.Drawing.Size(27, 28);
			this.B_Discover.TabIndex = 9;
			this.B_Discover.Click += new System.EventHandler(this.B_Discover_Click);
			// 
			// B_Sort
			// 
			this.B_Sort.ActiveColor = null;
			this.B_Sort.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_Sort.AutoSize = true;
			this.B_Sort.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Sort.Center = false;
			this.B_Sort.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Sort.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Sort.HideText = false;
			this.B_Sort.HoverState = SlickControls.Enums.HoverState.Normal;
			this.B_Sort.IconSize = 14;
			this.B_Sort.Image = ((System.Drawing.Image)(resources.GetObject("B_Sort.Image")));
			this.B_Sort.Location = new System.Drawing.Point(700, 8);
			this.B_Sort.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.B_Sort.Name = "B_Sort";
			this.B_Sort.Padding = new System.Windows.Forms.Padding(5, 7, 5, 7);
			this.B_Sort.Size = new System.Drawing.Size(27, 28);
			this.B_Sort.TabIndex = 9;
			this.B_Sort.Click += new System.EventHandler(this.B_Sort_Click);
			// 
			// B_Folders
			// 
			this.B_Folders.ActiveColor = null;
			this.B_Folders.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Folders.AutoSize = true;
			this.B_Folders.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Folders.Center = false;
			this.B_Folders.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Folders.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Folders.HideText = false;
			this.B_Folders.HoverState = SlickControls.Enums.HoverState.Normal;
			this.B_Folders.IconSize = 14;
			this.B_Folders.Image = ((System.Drawing.Image)(resources.GetObject("B_Folders.Image")));
			this.B_Folders.Location = new System.Drawing.Point(733, 8);
			this.B_Folders.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.B_Folders.Name = "B_Folders";
			this.B_Folders.Padding = new System.Windows.Forms.Padding(5, 7, 5, 7);
			this.B_Folders.Size = new System.Drawing.Size(27, 28);
			this.B_Folders.TabIndex = 9;
			this.B_Folders.Click += new System.EventHandler(this.B_Folders_Click);
			// 
			// ML_Clear
			// 
			this.ML_Clear.ActiveColor = null;
			this.ML_Clear.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.ML_Clear.AutoSize = true;
			this.ML_Clear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ML_Clear.Center = false;
			this.ML_Clear.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ML_Clear.HideText = false;
			this.ML_Clear.HoverState = SlickControls.Enums.HoverState.Normal;
			this.ML_Clear.IconSize = 16;
			this.ML_Clear.Image = ((System.Drawing.Image)(resources.GetObject("ML_Clear.Image")));
			this.ML_Clear.Location = new System.Drawing.Point(526, 10);
			this.ML_Clear.Margin = new System.Windows.Forms.Padding(7);
			this.ML_Clear.MinimumSize = new System.Drawing.Size(0, 26);
			this.ML_Clear.Name = "ML_Clear";
			this.ML_Clear.Padding = new System.Windows.Forms.Padding(4);
			this.ML_Clear.Size = new System.Drawing.Size(27, 26);
			this.ML_Clear.TabIndex = 19;
			this.ML_Clear.Visible = false;
			// 
			// P_ContentShows
			// 
			this.P_ContentShows.Controls.Add(this.verticalScroll);
			this.P_ContentShows.Controls.Add(this.TLP_TileContainer);
			this.P_ContentShows.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_ContentShows.Location = new System.Drawing.Point(0, 0);
			this.P_ContentShows.Margin = new System.Windows.Forms.Padding(0);
			this.P_ContentShows.Name = "P_ContentShows";
			this.P_ContentShows.Size = new System.Drawing.Size(881, 409);
			this.P_ContentShows.TabIndex = 20;
			this.P_ContentShows.Layout += new System.Windows.Forms.LayoutEventHandler(this.TLP_TileContainer_Layout);
			// 
			// verticalScroll
			// 
			this.verticalScroll.BarColor = null;
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = null;
			this.verticalScroll.Location = new System.Drawing.Point(869, 0);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(12, 409);
			this.verticalScroll.TabIndex = 19;
			// 
			// TLP_TileContainer
			// 
			this.TLP_TileContainer.AutoSize = true;
			this.TLP_TileContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_TileContainer.ColumnCount = 1;
			this.TLP_TileContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_TileContainer.Controls.Add(this.L_NoShows, 0, 1);
			this.TLP_TileContainer.Controls.Add(this.FLP_Tiles, 0, 0);
			this.TLP_TileContainer.Location = new System.Drawing.Point(0, 0);
			this.TLP_TileContainer.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_TileContainer.Name = "TLP_TileContainer";
			this.TLP_TileContainer.RowCount = 2;
			this.TLP_TileContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_TileContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_TileContainer.Size = new System.Drawing.Size(290, 40);
			this.TLP_TileContainer.TabIndex = 18;
			this.TLP_TileContainer.Layout += new System.Windows.Forms.LayoutEventHandler(this.TLP_TileContainer_Layout);
			// 
			// L_NoShows
			// 
			this.L_NoShows.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoShows.AutoSize = true;
			this.L_NoShows.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_NoShows.Location = new System.Drawing.Point(9, 15);
			this.L_NoShows.Margin = new System.Windows.Forms.Padding(9);
			this.L_NoShows.Name = "L_NoShows";
			this.L_NoShows.Size = new System.Drawing.Size(272, 16);
			this.L_NoShows.TabIndex = 11;
			this.L_NoShows.Text = "No Shows yet, use the Add button to start";
			// 
			// FLP_Tiles
			// 
			this.FLP_Tiles.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.FLP_Tiles.AutoSize = true;
			this.FLP_Tiles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Tiles.Location = new System.Drawing.Point(145, 3);
			this.FLP_Tiles.Name = "FLP_Tiles";
			this.FLP_Tiles.Size = new System.Drawing.Size(0, 0);
			this.FLP_Tiles.TabIndex = 14;
			this.FLP_Tiles.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.FLP_Tiles_ControlAdded);
			// 
			// ShowForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.ClientSize = new System.Drawing.Size(900, 500);
			this.CloseForm = false;
			this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormIcon = ((System.Drawing.Image)(resources.GetObject("$this.FormIcon")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(775, 400);
			this.Name = "ShowForm";
			this.ShowControls = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Shows";
			this.Load += new System.EventHandler(this.ShowForm_Load);
			this.ResizeEnd += new System.EventHandler(this.ShowForm_ResizeEnd);
			this.base_P_Content.ResumeLayout(false);
			this.base_P_Controls.ResumeLayout(false);
			this.TLP_Mains.ResumeLayout(false);
			this.TLP_Mains.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_AddShow)).EndInit();
			this.P_ContentShows.ResumeLayout(false);
			this.P_ContentShows.PerformLayout();
			this.TLP_TileContainer.ResumeLayout(false);
			this.TLP_TileContainer.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private SlickControls.Controls.SlickButton B_AddShow;
		private System.Windows.Forms.ToolTip toolTip;
		public SlickControls.Controls.SlickTextBox TB_Search;
		private System.Windows.Forms.TableLayoutPanel TLP_Mains;
		private SlickControls.Controls.SlickLabel ML_Clear;
		private SlickControls.Controls.SlickLabel B_Discover;
		private SlickControls.Controls.SlickLabel B_Sort;
		private SlickControls.Controls.SlickLabel B_Folders;
		private System.Windows.Forms.Panel P_ContentShows;
		private SlickControls.Controls.VerticalScroll verticalScroll;
		private System.Windows.Forms.TableLayoutPanel TLP_TileContainer;
		private System.Windows.Forms.Label L_NoShows;
		internal System.Windows.Forms.FlowLayoutPanel FLP_Tiles;
	}
}