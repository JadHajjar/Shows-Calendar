namespace TVShowsCalendar
{
	partial class EpisodeTile
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
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PB_Thumb = new System.Windows.Forms.PictureBox();
            this.I_Dots = new SlickControls.Controls.SlickIcon();
            this.L_Actions = new SlickControls.Controls.SlickLabel();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Thumb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.I_Dots)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 20000;
            this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.toolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(65)))), ((int)(((byte)(77)))));
            this.toolTip.InitialDelay = 600;
            this.toolTip.ReshowDelay = 100;
            // 
            // PB_Thumb
            // 
            this.PB_Thumb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PB_Thumb.Dock = System.Windows.Forms.DockStyle.Left;
            this.PB_Thumb.ImageLocation = "";
            this.PB_Thumb.Location = new System.Drawing.Point(0, 0);
            this.PB_Thumb.Name = "PB_Thumb";
            this.PB_Thumb.Size = new System.Drawing.Size(64, 98);
            this.PB_Thumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_Thumb.TabIndex = 0;
            this.PB_Thumb.TabStop = false;
            this.PB_Thumb.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.PB_Thumb_LoadCompleted);
            this.PB_Thumb.Click += new System.EventHandler(this.PB_Thumb_Click);
            this.PB_Thumb.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_Thumb_Paint);
            // 
            // I_Dots
            // 
            this.I_Dots.ActiveColor = null;
            this.I_Dots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.I_Dots.Cursor = System.Windows.Forms.Cursors.Hand;
            this.I_Dots.HoverState = SlickControls.Enums.HoverState.Normal;
            this.I_Dots.Image = global::TVShowsCalendar.Properties.Resources.Icons_VDots;
            this.I_Dots.Location = new System.Drawing.Point(482, 5);
            this.I_Dots.MinimumIconSize = 20;
            this.I_Dots.Name = "I_Dots";
            this.I_Dots.Size = new System.Drawing.Size(24, 24);
            this.I_Dots.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.I_Dots.TabIndex = 1;
            this.I_Dots.TabStop = false;
            // 
            // L_Actions
            // 
            this.L_Actions.ActiveColor = null;
            this.L_Actions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.L_Actions.AutoSize = true;
            this.L_Actions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.L_Actions.Center = false;
            this.L_Actions.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.L_Actions.HideText = false;
            this.L_Actions.HoverState = SlickControls.Enums.HoverState.Normal;
            this.L_Actions.IconSize = 16;
            this.L_Actions.Image = global::TVShowsCalendar.Properties.Resources.Icon_Download_Rounded;
            this.L_Actions.Location = new System.Drawing.Point(405, 70);
            this.L_Actions.Name = "L_Actions";
            this.L_Actions.Padding = new System.Windows.Forms.Padding(5, 3, 2, 3);
            this.L_Actions.Size = new System.Drawing.Size(101, 23);
            this.L_Actions.TabIndex = 2;
            this.L_Actions.Text = "Download";
            this.L_Actions.LocationChanged += new System.EventHandler(this.L_Actions_LocationChanged);
            // 
            // EpisodeTile
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.PB_Thumb);
            this.Controls.Add(this.I_Dots);
            this.Controls.Add(this.L_Actions);
            this.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.Name = "EpisodeTile";
            this.Size = new System.Drawing.Size(511, 98);
            this.Load += new System.EventHandler(this.EpisodeTile_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.EpisodeTile_Paint);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.EpisodeTile_Layout);
            this.Resize += new System.EventHandler(this.EpisodeTile_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.PB_Thumb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.I_Dots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.PictureBox PB_Thumb;
        private SlickControls.Controls.SlickIcon I_Dots;
        private SlickControls.Controls.SlickLabel L_Actions;
	}
}
