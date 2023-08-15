namespace ShowsCalendar
{
	partial class PC_ManageCategory
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_Add = new SlickControls.SlickButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SP_Categories = new SlickControls.SlickSectionPanel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Location = new System.Drawing.Point(-2, 3);
			this.base_Text.Size = new System.Drawing.Size(83, 26);
			this.base_Text.Text = "Categories";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.B_Add, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 408);
			this.tableLayoutPanel1.TabIndex = 13;
			// 
			// B_Add
			// 
			this.B_Add.ColorShade = null;
			this.B_Add.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Add.Dock = System.Windows.Forms.DockStyle.Right;
			this.B_Add.Image = global::ShowsCalendar.Properties.Resources.Tiny_Add;
			this.B_Add.Location = new System.Drawing.Point(640, 3);
			this.B_Add.Name = "B_Add";
			this.B_Add.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_Add.Size = new System.Drawing.Size(140, 30);
			this.B_Add.SpaceTriggersClick = true;
			this.B_Add.TabIndex = 0;
			this.B_Add.Text = "ADD CATEGORY";
			this.B_Add.Click += new System.EventHandler(this.B_Add_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.SP_Categories);
			this.panel1.Controls.Add(this.slickScroll1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 36);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(783, 372);
			this.panel1.TabIndex = 1;
			// 
			// SP_Categories
			// 
			this.SP_Categories.Active = false;
			this.SP_Categories.AutoHide = false;
			this.SP_Categories.AutoSize = true;
			this.SP_Categories.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Categories.Flavor = null;
			this.SP_Categories.Icon = null;
			this.SP_Categories.Info = "Select one of your available categories, or add a new one";
			this.SP_Categories.Location = new System.Drawing.Point(0, 0);
			this.SP_Categories.MaximumSize = new System.Drawing.Size(783, 2147483647);
			this.SP_Categories.MinimumSize = new System.Drawing.Size(676, 55);
			this.SP_Categories.Name = "SP_Categories";
			this.SP_Categories.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Categories.Size = new System.Drawing.Size(676, 55);
			this.SP_Categories.TabIndex = 1;
			this.SP_Categories.Text = "Your Categories";
			// 
			// slickScroll1
			// 
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.SP_Categories;
			this.slickScroll1.Location = new System.Drawing.Point(777, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(6, 372);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 0;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.Text = "slickScroll1";
			// 
			// PC_ManageCategory
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.tableLayoutPanel1);
			this.LabelBounds = new System.Drawing.Point(-2, 3);
			this.Name = "PC_ManageCategory";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Text = "Categories";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private SlickControls.SlickButton B_Add;
		private System.Windows.Forms.Panel panel1;
		private SlickControls.SlickSectionPanel SP_Categories;
		private SlickControls.SlickScroll slickScroll1;
	}
}
