namespace TVShowsCalendar.Forms
{
	partial class AddMediaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddMediaForm));
            this.TLP_4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FLP_Results = new System.Windows.Forms.FlowLayoutPanel();
            this.verticalScroll1 = new SlickControls.Controls.VerticalScroll();
            this.TLP_SeriesName = new System.Windows.Forms.TableLayoutPanel();
            this.TB_SeriesName = new SlickControls.Controls.SlickTextBox();
            this.ML_Info = new SlickControls.Controls.SlickIcon();
            this.base_P_Content.SuspendLayout();
            this.base_P_Controls.SuspendLayout();
            this.TLP_4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.TLP_SeriesName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ML_Info)).BeginInit();
            this.SuspendLayout();
            // 
            // P_Content
            // 
            this.base_P_Content.Controls.Add(this.TLP_4);
            this.base_P_Content.Location = new System.Drawing.Point(1, 71);
            this.base_P_Content.Size = new System.Drawing.Size(826, 351);
            // 
            // P_Controls
            // 
            this.base_P_Controls.Controls.Add(this.TLP_SeriesName);
            this.base_P_Controls.Size = new System.Drawing.Size(826, 45);
            // 
            // TLP_4
            // 
            this.TLP_4.ColumnCount = 2;
            this.TLP_4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.TLP_4.Controls.Add(this.panel1, 0, 0);
            this.TLP_4.Controls.Add(this.verticalScroll1, 1, 0);
            this.TLP_4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_4.Location = new System.Drawing.Point(0, 0);
            this.TLP_4.Name = "TLP_4";
            this.TLP_4.RowCount = 1;
            this.TLP_4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_4.Size = new System.Drawing.Size(826, 351);
            this.TLP_4.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(15, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(15, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(796, 345);
            this.panel1.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.FLP_Results, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(841, 6);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // FLP_Results
            // 
            this.FLP_Results.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.FLP_Results.AutoSize = true;
            this.FLP_Results.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FLP_Results.Location = new System.Drawing.Point(420, 3);
            this.FLP_Results.Name = "FLP_Results";
            this.FLP_Results.Size = new System.Drawing.Size(0, 0);
            this.FLP_Results.TabIndex = 13;
            // 
            // verticalScroll1
            // 
            this.verticalScroll1.BarColor = null;
            this.verticalScroll1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalScroll1.LinkedControl = this.tableLayoutPanel1;
            this.verticalScroll1.Location = new System.Drawing.Point(858, 0);
            this.verticalScroll1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.verticalScroll1.Name = "verticalScroll1";
            this.verticalScroll1.Size = new System.Drawing.Size(4, 384);
            this.verticalScroll1.TabIndex = 9;
            // 
            // TLP_SeriesName
            // 
            this.TLP_SeriesName.ColumnCount = 2;
            this.TLP_SeriesName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_SeriesName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.TLP_SeriesName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_SeriesName.Controls.Add(this.ML_Info, 1, 0);
            this.TLP_SeriesName.Controls.Add(this.TB_SeriesName, 0, 0);
            this.TLP_SeriesName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_SeriesName.Location = new System.Drawing.Point(0, 0);
            this.TLP_SeriesName.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
            this.TLP_SeriesName.Name = "TLP_SeriesName";
            this.TLP_SeriesName.RowCount = 1;
            this.TLP_SeriesName.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_SeriesName.Size = new System.Drawing.Size(826, 45);
            this.TLP_SeriesName.TabIndex = 10;
            // 
            // TB_SeriesName
            // 
            this.TB_SeriesName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TB_SeriesName.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_SeriesName.LabelText = "Series Name";
            this.TB_SeriesName.Location = new System.Drawing.Point(30, 5);
            this.TB_SeriesName.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.TB_SeriesName.MaximumSize = new System.Drawing.Size(9999, 34);
            this.TB_SeriesName.MinimumSize = new System.Drawing.Size(0, 34);
            this.TB_SeriesName.Name = "TB_SeriesName";
            this.TB_SeriesName.Placeholder = "Type the name of a Series or Movie to search";
            this.TB_SeriesName.SelectedText = "";
            this.TB_SeriesName.SelectionLength = 0;
            this.TB_SeriesName.SelectionStart = 0;
            this.TB_SeriesName.Size = new System.Drawing.Size(600, 34);
            this.TB_SeriesName.TabIndex = 0;
            this.TB_SeriesName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TB_SeriesName.Validation = SlickControls.Enums.ValidationType.None;
            this.TB_SeriesName.ValidationRegex = "";
            // 
            // ML_Info
            // 
            this.ML_Info.ActiveColor = null;
            this.ML_Info.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ML_Info.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ML_Info.HoverState = SlickControls.Enums.HoverState.Normal;
            this.ML_Info.Image = global::TVShowsCalendar.Properties.Resources.Icon_Info;
            this.ML_Info.Location = new System.Drawing.Point(790, 11);
            this.ML_Info.MinimumIconSize = 20;
            this.ML_Info.Name = "ML_Info";
            this.ML_Info.Size = new System.Drawing.Size(22, 22);
            this.ML_Info.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ML_Info.TabIndex = 13;
            this.ML_Info.TabStop = false;
            this.ML_Info.Click += new System.EventHandler(this.Info_Click);
            // 
            // AddMediaForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.FormIcon = global::TVShowsCalendar.Properties.Resources.TVCalendar;
            this.ClientSize = new System.Drawing.Size(845, 440);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimumSize = new System.Drawing.Size(675, 350);
            this.Name = "AddMediaForm";
            this.ShowControls = true;
            this.ShowTopSpacer = false;
            this.Text = "Auto-Icons";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.TMDbForm_Layout);
            this.Resize += new System.EventHandler(this.TMDbForm_Resize);
            this.base_P_Content.ResumeLayout(false);
            this.base_P_Controls.ResumeLayout(false);
            this.TLP_4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.TLP_SeriesName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ML_Info)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TableLayoutPanel TLP_4;
		private System.Windows.Forms.TableLayoutPanel TLP_SeriesName;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel FLP_Results;
		private System.Windows.Forms.Panel panel1;
		private SlickControls.Controls.SlickTextBox TB_SeriesName;
		private SlickControls.Controls.VerticalScroll verticalScroll1;
        private SlickControls.Controls.SlickIcon ML_Info;
    }
}