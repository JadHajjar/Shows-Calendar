namespace TVShowsCalendar.Forms
{
	partial class DiscoverMediaForm

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiscoverMediaForm));
			this.TLP_4 = new System.Windows.Forms.TableLayoutPanel();
			this.B_Done = new SlickControls.Controls.SlickButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.FLP_Results = new System.Windows.Forms.FlowLayoutPanel();
			this.verticalScroll1 = new SlickControls.Controls.VerticalScroll();
			this.PB_Load = new System.Windows.Forms.PictureBox();
			this.base_P_Content.SuspendLayout();
			this.TLP_4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_Done)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Load)).BeginInit();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(58)))));
			this.base_P_Content.Controls.Add(this.TLP_4);
			this.base_P_Content.Location = new System.Drawing.Point(1, 73);
			this.base_P_Content.Size = new System.Drawing.Size(776, 359);
			// 
			// base_P_Controls
			// 
			this.base_P_Controls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(55)))), ((int)(((byte)(68)))));
			this.base_P_Controls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.base_P_Controls.Size = new System.Drawing.Size(776, 45);
			// 
			// base_P_Top_Spacer
			// 
			this.base_P_Top_Spacer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(89)))));
			this.base_P_Top_Spacer.Location = new System.Drawing.Point(1, 71);
			this.base_P_Top_Spacer.Size = new System.Drawing.Size(776, 2);
			// 
			// TLP_4
			// 
			this.TLP_4.ColumnCount = 2;
			this.TLP_4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
			this.TLP_4.Controls.Add(this.B_Done, 0, 1);
			this.TLP_4.Controls.Add(this.panel1, 0, 0);
			this.TLP_4.Controls.Add(this.verticalScroll1, 1, 0);
			this.TLP_4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_4.Location = new System.Drawing.Point(0, 0);
			this.TLP_4.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_4.Name = "TLP_4";
			this.TLP_4.RowCount = 2;
			this.TLP_4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_4.Size = new System.Drawing.Size(776, 359);
			this.TLP_4.TabIndex = 1;
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
			this.B_Done.Location = new System.Drawing.Point(338, 328);
			this.B_Done.Margin = new System.Windows.Forms.Padding(20, 5, 5, 5);
			this.B_Done.Name = "B_Done";
			this.B_Done.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.B_Done.Size = new System.Drawing.Size(99, 26);
			this.B_Done.TabIndex = 105;
			this.B_Done.Text = "Dismiss";
			this.B_Done.Click += new System.EventHandler(this.B_Done_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.FLP_Results);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(15, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(746, 323);
			this.panel1.TabIndex = 8;
			this.panel1.Layout += new System.Windows.Forms.LayoutEventHandler(this.DiscoverShowsForm_Layout);
			// 
			// FLP_Results
			// 
			this.FLP_Results.AutoSize = true;
			this.FLP_Results.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Results.Location = new System.Drawing.Point(0, 0);
			this.FLP_Results.Name = "FLP_Results";
			this.FLP_Results.Size = new System.Drawing.Size(0, 0);
			this.FLP_Results.TabIndex = 13;
			// 
			// verticalScroll1
			// 
			this.verticalScroll1.BarColor = null;
			this.verticalScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll1.LinkedControl = null;
			this.verticalScroll1.Location = new System.Drawing.Point(763, 0);
			this.verticalScroll1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll1.Name = "verticalScroll1";
			this.verticalScroll1.Size = new System.Drawing.Size(12, 323);
			this.verticalScroll1.TabIndex = 12;
			// 
			// PB_Load
			// 
			this.PB_Load.Location = new System.Drawing.Point(383, 207);
			this.PB_Load.Name = "PB_Load";
			this.PB_Load.Size = new System.Drawing.Size(26, 26);
			this.PB_Load.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Load.TabIndex = 9;
			this.PB_Load.TabStop = false;
			// 
			// DiscoverMediaForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(795, 450);
			this.Controls.Add(this.PB_Load);
			this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
			this.FormIcon = global::TVShowsCalendar.Properties.Resources.TVCalendar;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(5);
			this.MinimumSize = new System.Drawing.Size(625, 350);
			this.Name = "DiscoverMediaForm";
			this.Text = "Discover";
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.DiscoverShowsForm_Layout);
			this.Resize += new System.EventHandler(this.DiscoverShowsForm_Resize);
			this.Controls.SetChildIndex(this.PB_Load, 0);
			this.base_P_Content.ResumeLayout(false);
			this.TLP_4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.B_Done)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Load)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TableLayoutPanel TLP_4;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FlowLayoutPanel FLP_Results;
		private System.Windows.Forms.PictureBox PB_Load;
		private SlickControls.Controls.VerticalScroll verticalScroll1;
		private SlickControls.Controls.SlickButton B_Done;
	}
}