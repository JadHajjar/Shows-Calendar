namespace TVShowsCalendar
{
	partial class NotifyForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyForm));
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.B_OpenAll = new SlickControls.Controls.SlickButton();
			this.B_Done = new SlickControls.Controls.SlickButton();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.verticalScroll1 = new SlickControls.Controls.VerticalScroll();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_Episodes = new System.Windows.Forms.FlowLayoutPanel();
			this.base_P_Content.SuspendLayout();
			this.TLP_Main.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_OpenAll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Done)).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.Controls.Add(this.TLP_Main);
			this.base_P_Content.Size = new System.Drawing.Size(481, 207);
			// 
			// base_P_Controls
			// 
			this.base_P_Controls.Size = new System.Drawing.Size(481, 45);
			// 
			// base_P_Top_Spacer
			// 
			this.base_P_Top_Spacer.Location = new System.Drawing.Point(1, 26);
			this.base_P_Top_Spacer.Size = new System.Drawing.Size(481, 2);
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 20000;
			this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.toolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(65)))), ((int)(((byte)(77)))));
			this.toolTip.InitialDelay = 600;
			this.toolTip.ReshowDelay = 100;
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 2;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.Controls.Add(this.B_OpenAll, 0, 1);
			this.TLP_Main.Controls.Add(this.B_Done, 1, 1);
			this.TLP_Main.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 2;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.Size = new System.Drawing.Size(481, 207);
			this.TLP_Main.TabIndex = 0;
			// 
			// B_OpenAll
			// 
			this.B_OpenAll.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_OpenAll.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_OpenAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_OpenAll.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_OpenAll.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_OpenAll.HueShade = null;
			this.B_OpenAll.IconSize = 14;
			this.B_OpenAll.Image = global::TVShowsCalendar.Properties.Resources.Icon_Download_Rounded;
			this.B_OpenAll.Location = new System.Drawing.Point(69, 176);
			this.B_OpenAll.Margin = new System.Windows.Forms.Padding(5);
			this.B_OpenAll.Name = "B_OpenAll";
			this.B_OpenAll.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.B_OpenAll.Size = new System.Drawing.Size(102, 26);
			this.B_OpenAll.TabIndex = 3;
			this.B_OpenAll.Text = "Open All";
			this.B_OpenAll.Click += new System.EventHandler(this.B_OpenAll_Click);
			// 
			// B_Done
			// 
			this.B_Done.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Done.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_Done.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Done.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Done.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Done.HueShade = null;
			this.B_Done.IconSize = 14;
			this.B_Done.Image = global::TVShowsCalendar.Properties.Resources.Icon_OK;
			this.B_Done.Location = new System.Drawing.Point(309, 176);
			this.B_Done.Margin = new System.Windows.Forms.Padding(5);
			this.B_Done.Name = "B_Done";
			this.B_Done.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.B_Done.Size = new System.Drawing.Size(102, 26);
			this.B_Done.TabIndex = 3;
			this.B_Done.Text = "Dismiss";
			this.B_Done.Click += new System.EventHandler(this.B_Done_Click);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.TLP_Main.SetColumnSpan(this.tableLayoutPanel2, 2);
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
			this.tableLayoutPanel2.Controls.Add(this.verticalScroll1, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(481, 171);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// verticalScroll1
			// 
			this.verticalScroll1.BarColor = null;
			this.verticalScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll1.LinkedControl = null;
			this.verticalScroll1.Location = new System.Drawing.Point(476, 0);
			this.verticalScroll1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll1.Name = "verticalScroll1";
			this.verticalScroll1.Size = new System.Drawing.Size(4, 171);
			this.verticalScroll1.TabIndex = 6;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tableLayoutPanel1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(466, 171);
			this.panel1.TabIndex = 7;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.FLP_Episodes, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(6, 6);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// FLP_Episodes
			// 
			this.FLP_Episodes.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.FLP_Episodes.AutoSize = true;
			this.FLP_Episodes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Episodes.Location = new System.Drawing.Point(3, 3);
			this.FLP_Episodes.Name = "FLP_Episodes";
			this.FLP_Episodes.Size = new System.Drawing.Size(0, 0);
			this.FLP_Episodes.TabIndex = 5;
			// 
			// NotifyForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(500, 253);
			this.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormIcon = global::TVShowsCalendar.Properties.Resources.TVCalendar;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(475, 125);
			this.Name = "NotifyForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "New Episodes";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.NotifyForm_Load);
			this.Resize += new System.EventHandler(this.NotifyForm_Resize);
			this.base_P_Content.ResumeLayout(false);
			this.TLP_Main.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.B_OpenAll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Done)).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private SlickControls.Controls.SlickButton B_OpenAll;
		private SlickControls.Controls.SlickButton B_Done;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.FlowLayoutPanel FLP_Episodes;
		private SlickControls.Controls.VerticalScroll verticalScroll1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}

