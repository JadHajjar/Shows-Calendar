namespace ShowsCalendar
{
	partial class PC_People
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

			dataLoadFactory?.Clear();
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
			this.P_Tabs = new System.Windows.Forms.FlowLayoutPanel();
			this.verticalScroll = new SlickControls.SlickScroll();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.P_TopSpacer = new System.Windows.Forms.Panel();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.PB_Search = new SlickControls.SlickPictureBox();
			this.TLP_NoMovies = new System.Windows.Forms.TableLayoutPanel();
			this.L_NoMovies = new System.Windows.Forms.Label();
			this.L_NoMoviesInfo = new System.Windows.Forms.Label();
			this.PB_FirstLoad = new SlickControls.SlickPictureBox();
			this.panel2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Search)).BeginInit();
			this.TLP_NoMovies.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_FirstLoad)).BeginInit();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(57, 26);
			this.base_Text.Text = "People";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.P_Tabs);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(5, 62);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(880, 431);
			this.panel2.TabIndex = 23;
			// 
			// P_Tabs
			// 
			this.P_Tabs.AutoSize = true;
			this.P_Tabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tabs.Location = new System.Drawing.Point(0, 0);
			this.P_Tabs.MaximumSize = new System.Drawing.Size(880, 2147483647);
			this.P_Tabs.MinimumSize = new System.Drawing.Size(880, 0);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(880, 0);
			this.P_Tabs.TabIndex = 12;
			// 
			// verticalScroll
			// 
			this.verticalScroll.AutoSizeSource = true;
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = this.P_Tabs;
			this.verticalScroll.Location = new System.Drawing.Point(879, 62);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(6, 431);
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
			this.tableLayoutPanel1.Controls.Add(this.P_TopSpacer, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.TB_Search, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.PB_Search, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(880, 32);
			this.tableLayoutPanel1.TabIndex = 25;
			// 
			// P_TopSpacer
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.P_TopSpacer, 4);
			this.P_TopSpacer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_TopSpacer.Location = new System.Drawing.Point(0, 31);
			this.P_TopSpacer.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.P_TopSpacer.Name = "P_TopSpacer";
			this.P_TopSpacer.Size = new System.Drawing.Size(875, 1);
			this.P_TopSpacer.TabIndex = 28;
			this.P_TopSpacer.Visible = false;
			// 
			// TB_Search
			// 
			this.TB_Search.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.TB_Search.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TB_Search.Image = null;
			this.TB_Search.LabelText = "Search";
			this.TB_Search.Location = new System.Drawing.Point(51, 7);
			this.TB_Search.Margin = new System.Windows.Forms.Padding(10, 4, 3, 0);
			this.TB_Search.MaximumSize = new System.Drawing.Size(9999, 20);
			this.TB_Search.MaxLength = 32767;
			this.TB_Search.MinimumSize = new System.Drawing.Size(0, 20);
			this.TB_Search.Name = "TB_Search";
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
			this.TB_Search.Leave += new System.EventHandler(this.TB_Search_Leave);
			// 
			// PB_Search
			// 
			this.PB_Search.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.PB_Search.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Search.Image = global::ShowsCalendar.Properties.Resources.Big_Search;
			this.PB_Search.Location = new System.Drawing.Point(15, 4);
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
			this.TLP_NoMovies.Size = new System.Drawing.Size(291, 60);
			this.TLP_NoMovies.TabIndex = 30;
			this.TLP_NoMovies.Visible = false;
			// 
			// L_NoMovies
			// 
			this.L_NoMovies.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoMovies.AutoSize = true;
			this.L_NoMovies.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_NoMovies.Location = new System.Drawing.Point(87, 3);
			this.L_NoMovies.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
			this.L_NoMovies.Name = "L_NoMovies";
			this.L_NoMovies.Size = new System.Drawing.Size(117, 17);
			this.L_NoMovies.TabIndex = 1;
			this.L_NoMovies.Text = "No-one lives here";
			// 
			// L_NoMoviesInfo
			// 
			this.L_NoMoviesInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoMoviesInfo.AutoSize = true;
			this.L_NoMoviesInfo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic);
			this.L_NoMoviesInfo.Location = new System.Drawing.Point(0, 30);
			this.L_NoMoviesInfo.Margin = new System.Windows.Forms.Padding(0);
			this.L_NoMoviesInfo.Name = "L_NoMoviesInfo";
			this.L_NoMoviesInfo.Size = new System.Drawing.Size(291, 30);
			this.L_NoMoviesInfo.TabIndex = 1;
			this.L_NoMoviesInfo.Text = "You don\'t seem to have any content in your library.\r\nAdd Shows & Movies to find p" +
    "eople here.";
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
			// PC_People
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.TLP_NoMovies);
			this.Controls.Add(this.PB_FirstLoad);
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_People";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Size = new System.Drawing.Size(885, 493);
			this.Text = "People";
			this.Shown += new System.EventHandler(this.PC_Movies_Shown);
			this.Load += new System.EventHandler(this.PC_Movies_Load);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.panel2, 0);
			this.Controls.SetChildIndex(this.verticalScroll, 0);
			this.Controls.SetChildIndex(this.PB_FirstLoad, 0);
			this.Controls.SetChildIndex(this.TLP_NoMovies, 0);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
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
		private System.Windows.Forms.FlowLayoutPanel P_Tabs;
		private SlickControls.SlickScroll verticalScroll;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private SlickControls.SlickPictureBox PB_Search;
		private System.Windows.Forms.TableLayoutPanel TLP_NoMovies;
		private System.Windows.Forms.Label L_NoMovies;
		private System.Windows.Forms.Label L_NoMoviesInfo;
		private SlickControls.SlickTextBox TB_Search;
		private System.Windows.Forms.Panel P_TopSpacer;
		private SlickControls.SlickPictureBox PB_FirstLoad;
	}
}
