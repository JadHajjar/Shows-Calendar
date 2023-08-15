namespace TVShowsCalendar.Forms
{
	partial class SortingPopup
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SortingPopup));
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.B_OK = new SlickControls.Controls.SlickButton();
			this.B_Cancel = new SlickControls.Controls.SlickButton();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.L_FirstDate = new SlickControls.Controls.SlickRadioButton();
			this.L_Rating = new SlickControls.Controls.SlickRadioButton();
			this.L_Name = new SlickControls.Controls.SlickRadioButton();
			this.L_LastDate = new SlickControls.Controls.SlickRadioButton();
			this.L_Order = new SlickControls.Controls.SlickLabel();
			this.base_P_Content.SuspendLayout();
			this.TLP_Main.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_OK)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Cancel)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.Controls.Add(this.TLP_Main);
			this.base_P_Content.Size = new System.Drawing.Size(306, 220);
			// 
			// base_P_Controls
			// 
			this.base_P_Controls.Size = new System.Drawing.Size(306, 45);
			// 
			// base_P_Top_Spacer
			// 
			this.base_P_Top_Spacer.Size = new System.Drawing.Size(306, 2);
			// 
			// TLP_Main
			// 
			this.TLP_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.TLP_Main.ColumnCount = 2;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.Controls.Add(this.B_OK, 1, 1);
			this.TLP_Main.Controls.Add(this.B_Cancel, 0, 1);
			this.TLP_Main.Controls.Add(this.tableLayoutPanel1, 0, 0);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 2;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.Size = new System.Drawing.Size(306, 220);
			this.TLP_Main.TabIndex = 0;
			// 
			// B_OK
			// 
			this.B_OK.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_OK.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_OK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_OK.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_OK.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_OK.HueShade = null;
			this.B_OK.IconSize = 14;
			this.B_OK.Image = global::TVShowsCalendar.Properties.Resources.Icon_OK;
			this.B_OK.Location = new System.Drawing.Point(183, 189);
			this.B_OK.Margin = new System.Windows.Forms.Padding(0);
			this.B_OK.Name = "B_OK";
			this.B_OK.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.B_OK.Size = new System.Drawing.Size(93, 26);
			this.B_OK.TabIndex = 4;
			this.B_OK.Text = "Apply";
			this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Cancel.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_Cancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Cancel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_Cancel.HueShade = null;
			this.B_Cancel.IconSize = 14;
			this.B_Cancel.Image = global::TVShowsCalendar.Properties.Resources.Icon_Cancel;
			this.B_Cancel.Location = new System.Drawing.Point(30, 189);
			this.B_Cancel.Margin = new System.Windows.Forms.Padding(0);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.B_Cancel.Size = new System.Drawing.Size(93, 26);
			this.B_Cancel.TabIndex = 0;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.TLP_Main.SetColumnSpan(this.tableLayoutPanel1, 2);
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.L_Order, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 179);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(15, 15);
			this.label1.Margin = new System.Windows.Forms.Padding(15, 15, 3, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Order:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(15, 61);
			this.label2.Margin = new System.Windows.Forms.Padding(15, 15, 3, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Sorting:";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.L_FirstDate, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.L_Rating, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.L_Name, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.L_LastDate, 0, 2);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(94, 46);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 4;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(206, 133);
			this.tableLayoutPanel2.TabIndex = 2;
			// 
			// L_FirstDate
			// 
			this.L_FirstDate.ActiveColor = null;
			this.L_FirstDate.AutoSize = true;
			this.L_FirstDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.L_FirstDate.Center = false;
			this.L_FirstDate.Checked = false;
			this.L_FirstDate.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_FirstDate.Data = null;
			this.L_FirstDate.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_FirstDate.HideText = false;
			this.L_FirstDate.HoverState = SlickControls.Enums.HoverState.Normal;
			this.L_FirstDate.IconSize = 14;
			this.L_FirstDate.Image = ((System.Drawing.Image)(resources.GetObject("L_FirstDate.Image")));
			this.L_FirstDate.Location = new System.Drawing.Point(3, 36);
			this.L_FirstDate.Name = "L_FirstDate";
			this.L_FirstDate.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.L_FirstDate.Size = new System.Drawing.Size(123, 27);
			this.L_FirstDate.TabIndex = 3;
			this.L_FirstDate.Text = "First Air-Date";
			// 
			// L_Rating
			// 
			this.L_Rating.ActiveColor = null;
			this.L_Rating.AutoSize = true;
			this.L_Rating.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.L_Rating.Center = false;
			this.L_Rating.Checked = false;
			this.L_Rating.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Rating.Data = null;
			this.L_Rating.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_Rating.HideText = false;
			this.L_Rating.HoverState = SlickControls.Enums.HoverState.Normal;
			this.L_Rating.IconSize = 14;
			this.L_Rating.Image = ((System.Drawing.Image)(resources.GetObject("L_Rating.Image")));
			this.L_Rating.Location = new System.Drawing.Point(3, 102);
			this.L_Rating.Name = "L_Rating";
			this.L_Rating.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.L_Rating.Size = new System.Drawing.Size(83, 27);
			this.L_Rating.TabIndex = 2;
			this.L_Rating.Text = "Rating";
			// 
			// L_Name
			// 
			this.L_Name.ActiveColor = null;
			this.L_Name.AutoSize = true;
			this.L_Name.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.L_Name.Center = false;
			this.L_Name.Checked = false;
			this.L_Name.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Name.Data = null;
			this.L_Name.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_Name.HideText = false;
			this.L_Name.HoverState = SlickControls.Enums.HoverState.Normal;
			this.L_Name.IconSize = 14;
			this.L_Name.Image = ((System.Drawing.Image)(resources.GetObject("L_Name.Image")));
			this.L_Name.Location = new System.Drawing.Point(3, 3);
			this.L_Name.Name = "L_Name";
			this.L_Name.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.L_Name.Size = new System.Drawing.Size(120, 27);
			this.L_Name.TabIndex = 0;
			this.L_Name.Text = "Show Name";
			// 
			// L_LastDate
			// 
			this.L_LastDate.ActiveColor = null;
			this.L_LastDate.AutoSize = true;
			this.L_LastDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.L_LastDate.Center = false;
			this.L_LastDate.Checked = false;
			this.L_LastDate.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_LastDate.Data = null;
			this.L_LastDate.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_LastDate.HideText = false;
			this.L_LastDate.HoverState = SlickControls.Enums.HoverState.Normal;
			this.L_LastDate.IconSize = 14;
			this.L_LastDate.Image = ((System.Drawing.Image)(resources.GetObject("L_LastDate.Image")));
			this.L_LastDate.Location = new System.Drawing.Point(3, 69);
			this.L_LastDate.Name = "L_LastDate";
			this.L_LastDate.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.L_LastDate.Size = new System.Drawing.Size(124, 27);
			this.L_LastDate.TabIndex = 1;
			this.L_LastDate.Text = "Last Air-Date";
			// 
			// L_Order
			// 
			this.L_Order.ActiveColor = null;
			this.L_Order.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Order.AutoSize = true;
			this.L_Order.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.L_Order.Center = false;
			this.L_Order.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Order.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_Order.HideText = false;
			this.L_Order.HoverState = SlickControls.Enums.HoverState.Normal;
			this.L_Order.IconSize = 16;
			this.L_Order.Image = global::TVShowsCalendar.Properties.Resources.Check_Checked;
			this.L_Order.Location = new System.Drawing.Point(97, 9);
			this.L_Order.Margin = new System.Windows.Forms.Padding(23, 3, 3, 3);
			this.L_Order.Name = "L_Order";
			this.L_Order.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.L_Order.Size = new System.Drawing.Size(81, 27);
			this.L_Order.TabIndex = 3;
			this.L_Order.Text = "Order";
			// 
			// SortingPopup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(325, 266);
			this.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
			this.FormIcon = global::TVShowsCalendar.Properties.Resources.TVCalendar;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(325, 180);
			this.Name = "SortingPopup";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Sorting";
			this.TopMost = true;
			this.base_P_Content.ResumeLayout(false);
			this.TLP_Main.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.B_OK)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Cancel)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private SlickControls.Controls.SlickButton B_Cancel;
		private SlickControls.Controls.SlickButton B_OK;
		internal System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private SlickControls.Controls.SlickRadioButton L_Name;
		private SlickControls.Controls.SlickRadioButton L_LastDate;
		private SlickControls.Controls.SlickRadioButton L_FirstDate;
		private SlickControls.Controls.SlickRadioButton L_Rating;
		private SlickControls.Controls.SlickLabel L_Order;
	}
}