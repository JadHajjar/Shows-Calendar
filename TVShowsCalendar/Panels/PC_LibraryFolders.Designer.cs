namespace ShowsCalendar
{
	partial class PC_LibraryFolders
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PC_LibraryFolders));
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.B_Refresh = new SlickControls.SlickButton();
			this.B_Done = new SlickControls.SlickButton();
			this.FLP_Folders = new System.Windows.Forms.FlowLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.B_SearchMedia = new SlickControls.SlickButton();
			this.TB_Path = new SlickControls.SlickPathTextBox();
			this.B_Add = new SlickControls.SlickButton();
			this.ioSelectionDialog1 = new SlickControls.IOSelectionDialog();
			this.TLP_Main.SuspendLayout();
			this.SuspendLayout();
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 3;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.Controls.Add(this.B_Refresh, 1, 3);
			this.TLP_Main.Controls.Add(this.B_Done, 0, 3);
			this.TLP_Main.Controls.Add(this.FLP_Folders, 0, 2);
			this.TLP_Main.Controls.Add(this.label2, 0, 1);
			this.TLP_Main.Controls.Add(this.B_SearchMedia, 2, 3);
			this.TLP_Main.Controls.Add(this.TB_Path, 0, 0);
			this.TLP_Main.Controls.Add(this.B_Add, 2, 0);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(5, 30);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 4;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.Size = new System.Drawing.Size(773, 403);
			this.TLP_Main.TabIndex = 12;
			// 
			// B_Refresh
			// 
			this.B_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Refresh.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_Refresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Refresh.ColorShade = null;
			this.B_Refresh.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Refresh.Image = global::ShowsCalendar.Properties.Resources.Tiny_Refresh;
			this.B_Refresh.Location = new System.Drawing.Point(443, 360);
			this.B_Refresh.Margin = new System.Windows.Forms.Padding(0, 0, 15, 15);
			this.B_Refresh.Name = "B_Refresh";
			this.B_Refresh.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.B_Refresh.Size = new System.Drawing.Size(150, 28);
			this.B_Refresh.TabIndex = 109;
			this.B_Refresh.Text = "REFRESH FILES";
			this.B_Refresh.Click += new System.EventHandler(this.B_Refresh_Click);
			// 
			// B_Done
			// 
			this.B_Done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Done.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_Done.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Done.ColorShade = null;
			this.B_Done.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Done.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.B_Done.Image = global::ShowsCalendar.Properties.Resources.Tiny_Ok;
			this.B_Done.Location = new System.Drawing.Point(15, 360);
			this.B_Done.Margin = new System.Windows.Forms.Padding(15, 0, 20, 15);
			this.B_Done.Name = "B_Done";
			this.B_Done.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.B_Done.Size = new System.Drawing.Size(100, 28);
			this.B_Done.TabIndex = 13;
			this.B_Done.Text = "DONE";
			this.B_Done.Visible = false;
			this.B_Done.Click += new System.EventHandler(this.B_Done_Click);
			// 
			// FLP_Folders
			// 
			this.TLP_Main.SetColumnSpan(this.FLP_Folders, 3);
			this.FLP_Folders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FLP_Folders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.FLP_Folders.Location = new System.Drawing.Point(10, 95);
			this.FLP_Folders.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.FLP_Folders.Name = "FLP_Folders";
			this.FLP_Folders.Size = new System.Drawing.Size(753, 260);
			this.FLP_Folders.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.TLP_Main.SetColumnSpan(this.label2, 3);
			this.label2.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(15, 68);
			this.label2.Margin = new System.Windows.Forms.Padding(15, 20, 0, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "Selected Folders:";
			// 
			// B_SearchMedia
			// 
			this.B_SearchMedia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_SearchMedia.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_SearchMedia.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_SearchMedia.ColorShade = null;
			this.B_SearchMedia.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_SearchMedia.Image = global::ShowsCalendar.Properties.Resources.Tiny_Discover;
			this.B_SearchMedia.Location = new System.Drawing.Point(608, 360);
			this.B_SearchMedia.Margin = new System.Windows.Forms.Padding(0, 0, 15, 15);
			this.B_SearchMedia.Name = "B_SearchMedia";
			this.B_SearchMedia.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.B_SearchMedia.Size = new System.Drawing.Size(150, 28);
			this.B_SearchMedia.TabIndex = 108;
			this.B_SearchMedia.Text = "SEARCH FOR MEDIA";
			this.B_SearchMedia.Click += new System.EventHandler(this.B_SearchMedia_Click);
			// 
			// TB_Path
			// 
			this.TLP_Main.SetColumnSpan(this.TB_Path, 2);
			this.TB_Path.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_Path.FileExtensions = new string[0];
			this.TB_Path.Folder = true;
			this.TB_Path.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TB_Path.Image = ((System.Drawing.Image)(resources.GetObject("TB_Path.Image")));
			this.TB_Path.LabelText = "Folder Path";
			this.TB_Path.Location = new System.Drawing.Point(15, 11);
			this.TB_Path.Margin = new System.Windows.Forms.Padding(15, 11, 15, 3);
			this.TB_Path.MaximumSize = new System.Drawing.Size(900, 34);
			this.TB_Path.MaxLength = 32767;
			this.TB_Path.MinimumSize = new System.Drawing.Size(50, 34);
			this.TB_Path.Name = "TB_Path";
			this.TB_Path.Password = false;
			this.TB_Path.Placeholder = "Folder containing your series";
			this.TB_Path.ReadOnly = false;
			this.TB_Path.Required = false;
			this.TB_Path.SelectAllOnFocus = false;
			this.TB_Path.SelectedText = "";
			this.TB_Path.SelectionLength = 0;
			this.TB_Path.SelectionStart = 0;
			this.TB_Path.Size = new System.Drawing.Size(578, 34);
			this.TB_Path.TabIndex = 1;
			this.TB_Path.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.TB_Path.Validation = SlickControls.ValidationType.None;
			this.TB_Path.ValidationRegex = "";
			// 
			// B_Add
			// 
			this.B_Add.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Add.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_Add.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Add.ColorShade = null;
			this.B_Add.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Add.Image = global::ShowsCalendar.Properties.Resources.Tiny_AddFolder;
			this.B_Add.Location = new System.Drawing.Point(627, 13);
			this.B_Add.Margin = new System.Windows.Forms.Padding(15, 11, 10, 3);
			this.B_Add.Name = "B_Add";
			this.B_Add.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_Add.Size = new System.Drawing.Size(131, 30);
			this.B_Add.TabIndex = 12;
			this.B_Add.Text = "ADD FOLDER";
			this.B_Add.Click += new System.EventHandler(this.B_Add_Click);
			// 
			// ioSelectionDialog1
			// 
			this.ioSelectionDialog1.PreserveLastPath = false;
			// 
			// PC_LibraryFolders
			// 
			this.AcceptButton = this.B_Add;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.TLP_Main);
			this.Name = "PC_LibraryFolders";
			this.Text = "Library Management";
			this.Controls.SetChildIndex(this.TLP_Main, 0);
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.FlowLayoutPanel FLP_Folders;
		private System.Windows.Forms.Label label2;
		private SlickControls.SlickPathTextBox TB_Path;
		private SlickControls.SlickButton B_Add;
		private SlickControls.SlickButton B_Done;
		private SlickControls.SlickButton B_SearchMedia;
		private SlickControls.SlickButton B_Refresh;
		private SlickControls.IOSelectionDialog ioSelectionDialog1;
	}
}
