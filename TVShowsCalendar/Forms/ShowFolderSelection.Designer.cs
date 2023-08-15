namespace TVShowsCalendar.Forms
{
	partial class ShowFolderSelection
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowFolderSelection));
			this.B_OK = new SlickControls.Controls.SlickButton();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_Add = new SlickControls.Controls.SlickButton();
			this.TB_Path = new SlickControls.Controls.SlickPathTextBox();
			this.FLP_Folders = new System.Windows.Forms.FlowLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.base_P_Content.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_OK)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_Add)).BeginInit();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(58)))));
			this.base_P_Content.Controls.Add(this.tableLayoutPanel1);
			this.base_P_Content.Size = new System.Drawing.Size(528, 237);
			// 
			// base_P_Controls
			// 
			this.base_P_Controls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(54)))));
			this.base_P_Controls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.base_P_Controls.Size = new System.Drawing.Size(528, 45);
			// 
			// base_P_Top_Spacer
			// 
			this.base_P_Top_Spacer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(89)))));
			this.base_P_Top_Spacer.Size = new System.Drawing.Size(528, 2);
			// 
			// B_OK
			// 
			this.B_OK.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_OK.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_OK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.SetColumnSpan(this.B_OK, 2);
			this.B_OK.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_OK.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.B_OK.HueShade = null;
			this.B_OK.IconSize = 17;
			this.B_OK.Image = global::TVShowsCalendar.Properties.Resources.Icon_OK;
			this.B_OK.Location = new System.Drawing.Point(211, 204);
			this.B_OK.Margin = new System.Windows.Forms.Padding(5);
			this.B_OK.Name = "B_OK";
			this.B_OK.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_OK.Size = new System.Drawing.Size(105, 28);
			this.B_OK.TabIndex = 6;
			this.B_OK.Text = "Done";
			this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.B_OK, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.B_Add, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.TB_Path, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.FLP_Folders, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(528, 237);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// B_Add
			// 
			this.B_Add.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Add.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_Add.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Add.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Add.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
			this.B_Add.HueShade = null;
			this.B_Add.IconSize = 17;
			this.B_Add.Image = global::TVShowsCalendar.Properties.Resources.Icon_Add;
			this.B_Add.Location = new System.Drawing.Point(439, 11);
			this.B_Add.Margin = new System.Windows.Forms.Padding(7, 5, 7, 0);
			this.B_Add.Name = "B_Add";
			this.B_Add.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_Add.Size = new System.Drawing.Size(82, 31);
			this.B_Add.TabIndex = 4;
			this.B_Add.Text = "Add";
			this.B_Add.Click += new System.EventHandler(this.B_Add_Click);
			// 
			// TB_Path
			// 
			this.TB_Path.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_Path.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TB_Path.Image = ((System.Drawing.Image)(resources.GetObject("TB_Path.Image")));
			this.TB_Path.LabelText = "Folder Path";
			this.TB_Path.Location = new System.Drawing.Point(15, 11);
			this.TB_Path.Margin = new System.Windows.Forms.Padding(15, 11, 15, 3);
			this.TB_Path.MaximumSize = new System.Drawing.Size(9999, 34);
			this.TB_Path.MaxLength = 32767;
			this.TB_Path.MinimumSize = new System.Drawing.Size(50, 35);
			this.TB_Path.Name = "TB_Path";
			this.TB_Path.Placeholder = "Folder containing your series";
			this.TB_Path.SelectedText = "";
			this.TB_Path.SelectionLength = 0;
			this.TB_Path.SelectionStart = 0;
			this.TB_Path.Size = new System.Drawing.Size(402, 35);
			this.TB_Path.TabIndex = 1;
			this.TB_Path.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.TB_Path.Validation = SlickControls.Enums.ValidationType.None;
			this.TB_Path.ValidationRegex = "";
			// 
			// FLP_Folders
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.FLP_Folders, 2);
			this.FLP_Folders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FLP_Folders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.FLP_Folders.Location = new System.Drawing.Point(10, 85);
			this.FLP_Folders.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.FLP_Folders.Name = "FLP_Folders";
			this.FLP_Folders.Size = new System.Drawing.Size(508, 109);
			this.FLP_Folders.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Century Gothic", 9.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.label2.Location = new System.Drawing.Point(15, 59);
			this.label2.Margin = new System.Windows.Forms.Padding(15, 10, 0, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Selected:";
			// 
			// ShowFolderSelection
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(547, 283);
			this.FormIcon = global::TVShowsCalendar.Properties.Resources.TVCalendar;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(325, 180);
			this.Name = "ShowFolderSelection";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Show Folder Selection";
			this.base_P_Content.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.B_OK)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_Add)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label2;
		private SlickControls.Controls.SlickPathTextBox TB_Path;
		private SlickControls.Controls.SlickButton B_Add;
		private SlickControls.Controls.SlickButton B_OK;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.FlowLayoutPanel FLP_Folders;
	}
}