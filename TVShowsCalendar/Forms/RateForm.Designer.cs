namespace ShowsCalendar
{
	partial class RateForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RateForm));
			this.slickControl1 = new SlickControls.SlickPictureBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_Done = new SlickControls.SlickButton();
			this.B_Cancel = new SlickControls.SlickButton();
			this.base_P_Container.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.base_PB_Icon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Close)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Max)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Min)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.slickControl1)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_P_Container
			// 
			this.base_P_Container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(217)))), ((int)(((byte)(179)))));
			this.base_P_Container.Controls.Add(this.slickControl1);
			this.base_P_Container.Controls.Add(this.tableLayoutPanel1);
			this.base_P_Container.Size = new System.Drawing.Size(439, 169);
			// 
			// slickControl1
			// 
			this.slickControl1.Cursor = System.Windows.Forms.Cursors.VSplit;
			this.slickControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickControl1.Image = null;
			this.slickControl1.Location = new System.Drawing.Point(1, 1);
			this.slickControl1.Name = "slickControl1";
			this.slickControl1.Size = new System.Drawing.Size(437, 121);
			this.slickControl1.TabIndex = 1;
			this.slickControl1.TabStop = false;
			this.slickControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.slickControl1_Paint);
			this.slickControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.slickControl1_MouseMove);
			this.slickControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.slickControl1_MouseMove);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.B_Done, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_Cancel, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 122);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(437, 46);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// B_Done
			// 
			this.B_Done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Done.ColorShade = null;
			this.B_Done.ColorStyle = Extensions.ColorStyle.Green;
			this.B_Done.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Done.EnterTriggersClick = false;
			this.B_Done.Image = global::ShowsCalendar.Properties.Resources.Tiny_Rating;
			this.B_Done.Location = new System.Drawing.Point(329, 8);
			this.B_Done.Margin = new System.Windows.Forms.Padding(8);
			this.B_Done.Name = "B_Done";
			this.B_Done.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_Done.Size = new System.Drawing.Size(100, 30);
			this.B_Done.SpaceTriggersClick = true;
			this.B_Done.TabIndex = 0;
			this.B_Done.Text = "Rate";
			this.B_Done.Click += new System.EventHandler(this.B_Done_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Cancel.ColorShade = null;
			this.B_Cancel.ColorStyle = Extensions.ColorStyle.Red;
			this.B_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Cancel.EnterTriggersClick = false;
			this.B_Cancel.Image = global::ShowsCalendar.Properties.Resources.Tiny_Cancel;
			this.B_Cancel.Location = new System.Drawing.Point(213, 8);
			this.B_Cancel.Margin = new System.Windows.Forms.Padding(8);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_Cancel.Size = new System.Drawing.Size(100, 30);
			this.B_Cancel.SpaceTriggersClick = true;
			this.B_Cancel.TabIndex = 1;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// RateForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(450, 180);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizedBounds = new System.Drawing.Rectangle(0, 0, 1920, 1080);
			this.Name = "RateForm";
			this.Opacity = 0D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Rate";
			this.base_P_Container.ResumeLayout(false);
			this.base_P_Container.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.base_PB_Icon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Close)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Max)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Min)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.slickControl1)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private SlickControls.SlickPictureBox slickControl1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private SlickControls.SlickButton B_Done;
		private SlickControls.SlickButton B_Cancel;
	}
}