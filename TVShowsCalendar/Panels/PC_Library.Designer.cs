namespace ShowsCalendar
{
	partial class PC_Library
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
			this.P_Tabs = new System.Windows.Forms.Panel();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.SP_Library = new SlickControls.SlickSectionPanel();
			this.Content = new System.Windows.Forms.FlowLayoutPanel();
			this.videoLibraryViewer = new SlickControls.SlickLibraryViewer();
			this.P_Tabs.SuspendLayout();
			this.SuspendLayout();
			// 
			// P_Tabs
			// 
			this.P_Tabs.AutoSize = true;
			this.P_Tabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tabs.Controls.Add(this.TB_Search);
			this.P_Tabs.Controls.Add(this.SP_Library);
			this.P_Tabs.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Tabs.Location = new System.Drawing.Point(5, 30);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(778, 54);
			this.P_Tabs.TabIndex = 12;
			// 
			// TB_Search
			// 
			this.TB_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.TB_Search.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TB_Search.Image = null;
			this.TB_Search.LabelText = "Search";
			this.TB_Search.Location = new System.Drawing.Point(478, 15);
			this.TB_Search.MaximumSize = new System.Drawing.Size(9999, 33);
			this.TB_Search.MaxLength = 32767;
			this.TB_Search.MinimumSize = new System.Drawing.Size(50, 33);
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Password = false;
			this.TB_Search.Placeholder = "Type to start searching";
			this.TB_Search.ReadOnly = false;
			this.TB_Search.Required = false;
			this.TB_Search.SelectAllOnFocus = false;
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.Size = new System.Drawing.Size(200, 33);
			this.TB_Search.TabIndex = 0;
			this.TB_Search.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.TB_Search.Validation = SlickControls.ValidationType.None;
			this.TB_Search.ValidationRegex = "";
			this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
			// 
			// SP_Library
			// 
			this.SP_Library.Active = false;
			this.SP_Library.AutoHide = false;
			this.SP_Library.AutoSize = true;
			this.SP_Library.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Library.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Library.Flavor = null;
			this.SP_Library.Icon = global::ShowsCalendar.Properties.Resources.Big_ViewPlay;
			this.SP_Library.Info = "A quick view of the videos in your libraries";
			this.SP_Library.Location = new System.Drawing.Point(0, 0);
			this.SP_Library.MaximumSize = new System.Drawing.Size(778, 2147483647);
			this.SP_Library.Name = "SP_Library";
			this.SP_Library.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Library.Size = new System.Drawing.Size(778, 54);
			this.SP_Library.TabIndex = 7;
			this.SP_Library.Text = "Video Library";
			// 
			// Content
			// 
			this.Content.AutoSize = true;
			this.Content.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Content.Dock = System.Windows.Forms.DockStyle.Top;
			this.Content.Location = new System.Drawing.Point(43, 54);
			this.Content.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Content.Name = "Content";
			this.Content.Size = new System.Drawing.Size(457, 0);
			this.Content.TabIndex = 0;
			// 
			// videoLibraryViewer
			// 
			this.videoLibraryViewer.AutoSize = true;
			this.videoLibraryViewer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.videoLibraryViewer.Cursor = System.Windows.Forms.Cursors.Default;
			this.videoLibraryViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.videoLibraryViewer.Location = new System.Drawing.Point(5, 84);
			this.videoLibraryViewer.MinimumSize = new System.Drawing.Size(200, 50);
			this.videoLibraryViewer.Name = "videoLibraryViewer";
			this.videoLibraryViewer.Size = new System.Drawing.Size(778, 354);
			this.videoLibraryViewer.TabIndex = 8;
			// 
			// PC_Library
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.videoLibraryViewer);
			this.Controls.Add(this.P_Tabs);
			this.Name = "PC_Library";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Text = "Library";
			this.Shown += new System.EventHandler(this.PC_Library_Shown);
			this.Controls.SetChildIndex(this.P_Tabs, 0);
			this.Controls.SetChildIndex(this.videoLibraryViewer, 0);
			this.P_Tabs.ResumeLayout(false);
			this.P_Tabs.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel P_Tabs;
		private SlickControls.SlickSectionPanel SP_Library;
		private SlickControls.SlickLibraryViewer videoLibraryViewer;
		private System.Windows.Forms.FlowLayoutPanel Content;
		private SlickControls.SlickTextBox TB_Search;
	}
}
