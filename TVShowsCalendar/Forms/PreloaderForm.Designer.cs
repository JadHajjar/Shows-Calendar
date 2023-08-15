namespace ShowsCalendar
{
	partial class PreloaderForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreloaderForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.base_PB_Icon = new System.Windows.Forms.PictureBox();
			this.base_L_Text = new System.Windows.Forms.Label();
			this.L_Version = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();

			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.L_Version, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.base_PB_Icon, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.base_L_Text, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.62712F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.37288F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(343, 236);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// base_PB_Icon
			// 
			this.base_PB_Icon.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.base_PB_Icon.Location = new System.Drawing.Point(139, 47);
			this.base_PB_Icon.Margin = new System.Windows.Forms.Padding(15);
			this.base_PB_Icon.Name = "base_PB_Icon";
			this.base_PB_Icon.Size = new System.Drawing.Size(64, 64);
			this.base_PB_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.base_PB_Icon.TabIndex = 0;
			this.base_PB_Icon.TabStop = false;
			this.base_PB_Icon.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			// 
			// base_L_Text
			// 
			this.base_L_Text.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.base_L_Text.AutoSize = true;
			this.base_L_Text.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.base_L_Text.Location = new System.Drawing.Point(110, 126);
			this.base_L_Text.Name = "base_L_Text";
			this.base_L_Text.Size = new System.Drawing.Size(122, 21);
			this.base_L_Text.TabIndex = 1;
			this.base_L_Text.Text = "Shows Calendar";
			// 
			// L_Version
			// 
			this.L_Version.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Version.AutoSize = true;
			this.L_Version.Font = new System.Drawing.Font("Nirmala UI", 6.75F);
			this.L_Version.Location = new System.Drawing.Point(0, 219);
			this.L_Version.Margin = new System.Windows.Forms.Padding(0);
			this.L_Version.Name = "L_Version";
			this.L_Version.Padding = new System.Windows.Forms.Padding(2);
			this.L_Version.Size = new System.Drawing.Size(38, 16);
			this.L_Version.TabIndex = 28;
			this.L_Version.Text = "Version";
			// 
			// PreloaderForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(343, 236);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PreloaderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Shows Calendar";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();

			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.PictureBox base_PB_Icon;
		private System.Windows.Forms.Label base_L_Text;
		private System.Windows.Forms.Label L_Version;
	}
}