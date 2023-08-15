namespace TVShowsCalendar.Forms
{
	partial class WatchForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private Vlc.DotNet.Forms.VlcControl vlcControl;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WatchForm));
			this.TLP_Buttons = new System.Windows.Forms.TableLayoutPanel();
			this.SL_Play = new SlickControls.Controls.SlickLabel();
			this.SL_Forward = new SlickControls.Controls.SlickLabel();
			this.SL_Next = new SlickControls.Controls.SlickLabel();
			this.SL_Backwards = new SlickControls.Controls.SlickLabel();
			this.SL_Previous = new SlickControls.Controls.SlickLabel();
			this.SI_More = new SlickControls.Controls.SlickIcon();
			this.SL_Subs = new SlickControls.Controls.SlickLabel();
			this.SS_Volume = new SlickControls.Controls.SlickSlider();
			this.SL_Audio = new SlickControls.Controls.SlickLabel();
			this.SL_FullScreen = new SlickControls.Controls.SlickLabel();
			this.SL_MiniPlayer = new SlickControls.Controls.SlickLabel();
			this.SS_TimeSlider = new SlickControls.Controls.SlickSlider();
			this.TLP_Controls = new System.Windows.Forms.TableLayoutPanel();
			this.L_CurrentTime = new System.Windows.Forms.Label();
			this.L_Time = new System.Windows.Forms.Label();
			this.P_BotSpacer = new System.Windows.Forms.Panel();
			this.base_P_Content.SuspendLayout();
			this.TLP_Buttons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SI_More)).BeginInit();
			this.TLP_Controls.SuspendLayout();
			this.P_BotSpacer.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(58)))));
			this.base_P_Content.Controls.Add(this.P_BotSpacer);
			this.base_P_Content.Location = new System.Drawing.Point(1, 63);
			this.base_P_Content.Size = new System.Drawing.Size(894, 469);
			// 
			// base_P_Controls
			// 
			this.base_P_Controls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(54)))));
			this.base_P_Controls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.base_P_Controls.Size = new System.Drawing.Size(894, 35);
			// 
			// base_P_Top_Spacer
			// 
			this.base_P_Top_Spacer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(89)))));
			this.base_P_Top_Spacer.Location = new System.Drawing.Point(1, 61);
			this.base_P_Top_Spacer.Size = new System.Drawing.Size(894, 2);
			// 
			// TLP_Buttons
			// 
			this.TLP_Buttons.ColumnCount = 13;
			this.TLP_Controls.SetColumnSpan(this.TLP_Buttons, 3);
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Buttons.Controls.Add(this.SL_Play, 6, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Forward, 7, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Next, 8, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Backwards, 5, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Previous, 4, 0);
			this.TLP_Buttons.Controls.Add(this.SI_More, 12, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Subs, 0, 0);
			this.TLP_Buttons.Controls.Add(this.SS_Volume, 2, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Audio, 1, 0);
			this.TLP_Buttons.Controls.Add(this.SL_FullScreen, 11, 0);
			this.TLP_Buttons.Controls.Add(this.SL_MiniPlayer, 10, 0);
			this.TLP_Buttons.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Buttons.Location = new System.Drawing.Point(0, 28);
			this.TLP_Buttons.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Buttons.Name = "TLP_Buttons";
			this.TLP_Buttons.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.TLP_Buttons.RowCount = 1;
			this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Buttons.Size = new System.Drawing.Size(894, 34);
			this.TLP_Buttons.TabIndex = 0;
			// 
			// SL_Play
			// 
			this.SL_Play.ActiveColor = null;
			this.SL_Play.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Play.AutoSize = true;
			this.SL_Play.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SL_Play.Center = true;
			this.SL_Play.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Play.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Play.HideText = true;
			this.SL_Play.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Play.IconSize = 18;
			this.SL_Play.Image = global::TVShowsCalendar.Properties.Resources.Icon_Play;
			this.SL_Play.Location = new System.Drawing.Point(424, 3);
			this.SL_Play.Margin = new System.Windows.Forms.Padding(0);
			this.SL_Play.MinimumSize = new System.Drawing.Size(0, 26);
			this.SL_Play.Name = "SL_Play";
			this.SL_Play.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.SL_Play.Size = new System.Drawing.Size(45, 26);
			this.SL_Play.TabIndex = 3;
			this.SL_Play.Click += new System.EventHandler(this.SL_Play_Click);
			// 
			// SL_Forward
			// 
			this.SL_Forward.ActiveColor = null;
			this.SL_Forward.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Forward.AutoSize = true;
			this.SL_Forward.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SL_Forward.Center = true;
			this.SL_Forward.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Forward.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Forward.HideText = true;
			this.SL_Forward.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Forward.IconSize = 18;
			this.SL_Forward.Image = global::TVShowsCalendar.Properties.Resources.Icon_Forward;
			this.SL_Forward.Location = new System.Drawing.Point(474, 3);
			this.SL_Forward.Margin = new System.Windows.Forms.Padding(0);
			this.SL_Forward.MinimumSize = new System.Drawing.Size(0, 26);
			this.SL_Forward.Name = "SL_Forward";
			this.SL_Forward.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.SL_Forward.Size = new System.Drawing.Size(45, 26);
			this.SL_Forward.TabIndex = 3;
			this.SL_Forward.Click += new System.EventHandler(this.SL_Forward_Click);
			// 
			// SL_Next
			// 
			this.SL_Next.ActiveColor = null;
			this.SL_Next.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Next.AutoSize = true;
			this.SL_Next.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SL_Next.Center = true;
			this.SL_Next.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Next.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Next.HideText = true;
			this.SL_Next.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Next.IconSize = 18;
			this.SL_Next.Image = global::TVShowsCalendar.Properties.Resources.Icon_P_Next;
			this.SL_Next.Location = new System.Drawing.Point(524, 3);
			this.SL_Next.Margin = new System.Windows.Forms.Padding(0);
			this.SL_Next.MinimumSize = new System.Drawing.Size(0, 26);
			this.SL_Next.Name = "SL_Next";
			this.SL_Next.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.SL_Next.Size = new System.Drawing.Size(45, 26);
			this.SL_Next.TabIndex = 3;
			this.SL_Next.Visible = false;
			this.SL_Next.Click += new System.EventHandler(this.SL_Next_Click);
			// 
			// SL_Backwards
			// 
			this.SL_Backwards.ActiveColor = null;
			this.SL_Backwards.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Backwards.AutoSize = true;
			this.SL_Backwards.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SL_Backwards.Center = true;
			this.SL_Backwards.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Backwards.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Backwards.HideText = true;
			this.SL_Backwards.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Backwards.IconSize = 18;
			this.SL_Backwards.Image = global::TVShowsCalendar.Properties.Resources.Icon_Retry;
			this.SL_Backwards.Location = new System.Drawing.Point(374, 3);
			this.SL_Backwards.Margin = new System.Windows.Forms.Padding(0);
			this.SL_Backwards.MinimumSize = new System.Drawing.Size(0, 26);
			this.SL_Backwards.Name = "SL_Backwards";
			this.SL_Backwards.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.SL_Backwards.Size = new System.Drawing.Size(45, 26);
			this.SL_Backwards.TabIndex = 3;
			this.SL_Backwards.Click += new System.EventHandler(this.SL_Backwards_Click);
			// 
			// SL_Previous
			// 
			this.SL_Previous.ActiveColor = null;
			this.SL_Previous.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Previous.AutoSize = true;
			this.SL_Previous.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SL_Previous.Center = true;
			this.SL_Previous.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Previous.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Previous.HideText = true;
			this.SL_Previous.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Previous.IconSize = 18;
			this.SL_Previous.Image = global::TVShowsCalendar.Properties.Resources.Icon_P_Back;
			this.SL_Previous.Location = new System.Drawing.Point(324, 3);
			this.SL_Previous.Margin = new System.Windows.Forms.Padding(0);
			this.SL_Previous.MinimumSize = new System.Drawing.Size(0, 26);
			this.SL_Previous.Name = "SL_Previous";
			this.SL_Previous.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.SL_Previous.Size = new System.Drawing.Size(45, 26);
			this.SL_Previous.TabIndex = 3;
			this.SL_Previous.Visible = false;
			this.SL_Previous.Click += new System.EventHandler(this.SL_Previous_Click);
			// 
			// SI_More
			// 
			this.SI_More.ActiveColor = null;
			this.SI_More.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SI_More.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SI_More.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SI_More.Image = global::TVShowsCalendar.Properties.Resources.Icons_VDots;
			this.SI_More.Location = new System.Drawing.Point(844, 4);
			this.SI_More.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
			this.SI_More.MinimumIconSize = 18;
			this.SI_More.Name = "SI_More";
			this.SI_More.Size = new System.Drawing.Size(40, 23);
			this.SI_More.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.SI_More.TabIndex = 4;
			this.SI_More.TabStop = false;
			this.SI_More.Click += new System.EventHandler(this.SI_More_Click);
			// 
			// SL_Subs
			// 
			this.SL_Subs.ActiveColor = null;
			this.SL_Subs.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Subs.AutoSize = true;
			this.SL_Subs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SL_Subs.Center = true;
			this.SL_Subs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Subs.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Subs.HideText = true;
			this.SL_Subs.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Subs.IconSize = 18;
			this.SL_Subs.Image = global::TVShowsCalendar.Properties.Resources.Icon_Subs;
			this.SL_Subs.Location = new System.Drawing.Point(2, 3);
			this.SL_Subs.Margin = new System.Windows.Forms.Padding(0);
			this.SL_Subs.MinimumSize = new System.Drawing.Size(0, 26);
			this.SL_Subs.Name = "SL_Subs";
			this.SL_Subs.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.SL_Subs.Size = new System.Drawing.Size(45, 26);
			this.SL_Subs.TabIndex = 3;
			this.SL_Subs.Click += new System.EventHandler(this.SL_Subs_Click);
			// 
			// SS_Volume
			// 
			this.TLP_Buttons.SetColumnSpan(this.SS_Volume, 2);
			this.SS_Volume.Dock = System.Windows.Forms.DockStyle.Left;
			this.SS_Volume.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
			this.SS_Volume.FromValue = 0D;
			this.SS_Volume.Location = new System.Drawing.Point(100, 3);
			this.SS_Volume.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.SS_Volume.MaxValue = 200D;
			this.SS_Volume.MinValue = 0D;
			this.SS_Volume.Name = "SS_Volume";
			this.SS_Volume.Padding = new System.Windows.Forms.Padding(14, 7, 14, 8);
			this.SS_Volume.Percentage = 0.5D;
			this.SS_Volume.PercFrom = 0D;
			this.SS_Volume.PercTo = 0.5D;
			this.SS_Volume.ShowValues = false;
			this.SS_Volume.Size = new System.Drawing.Size(113, 28);
			this.SS_Volume.SliderStyle = SlickControls.Controls.SliderStyle.SingleHorizontal;
			this.SS_Volume.TabIndex = 5;
			this.SS_Volume.ToValue = 100D;
			this.SS_Volume.Value = 100D;
			this.SS_Volume.ValueOutput = null;
			this.SS_Volume.ValuesChanged += new System.EventHandler(this.SS_Volume_ValuesChanged);
			// 
			// SL_Audio
			// 
			this.SL_Audio.ActiveColor = null;
			this.SL_Audio.AutoSize = true;
			this.SL_Audio.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SL_Audio.Center = true;
			this.SL_Audio.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Audio.Enabled = false;
			this.SL_Audio.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Audio.HideText = true;
			this.SL_Audio.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Audio.IconSize = 18;
			this.SL_Audio.Image = global::TVShowsCalendar.Properties.Resources.Icon_Volume;
			this.SL_Audio.Location = new System.Drawing.Point(50, 3);
			this.SL_Audio.Margin = new System.Windows.Forms.Padding(0);
			this.SL_Audio.MinimumSize = new System.Drawing.Size(0, 26);
			this.SL_Audio.Name = "SL_Audio";
			this.SL_Audio.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.SL_Audio.Size = new System.Drawing.Size(45, 26);
			this.SL_Audio.TabIndex = 3;
			this.SL_Audio.Click += new System.EventHandler(this.SL_Audio_Click);
			// 
			// SL_FullScreen
			// 
			this.SL_FullScreen.ActiveColor = null;
			this.SL_FullScreen.AutoSize = true;
			this.SL_FullScreen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SL_FullScreen.Center = true;
			this.SL_FullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_FullScreen.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_FullScreen.HideText = true;
			this.SL_FullScreen.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_FullScreen.IconSize = 16;
			this.SL_FullScreen.Image = global::TVShowsCalendar.Properties.Resources.Icon_NormScreen;
			this.SL_FullScreen.Location = new System.Drawing.Point(794, 3);
			this.SL_FullScreen.Margin = new System.Windows.Forms.Padding(0);
			this.SL_FullScreen.MinimumSize = new System.Drawing.Size(0, 26);
			this.SL_FullScreen.Name = "SL_FullScreen";
			this.SL_FullScreen.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.SL_FullScreen.Size = new System.Drawing.Size(43, 26);
			this.SL_FullScreen.TabIndex = 6;
			this.SL_FullScreen.Click += new System.EventHandler(this.SL_FullScreen_Click);
			// 
			// SL_MiniPlayer
			// 
			this.SL_MiniPlayer.ActiveColor = null;
			this.SL_MiniPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SL_MiniPlayer.AutoSize = true;
			this.SL_MiniPlayer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SL_MiniPlayer.Center = true;
			this.SL_MiniPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_MiniPlayer.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_MiniPlayer.HideText = true;
			this.SL_MiniPlayer.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_MiniPlayer.IconSize = 18;
			this.SL_MiniPlayer.Image = global::TVShowsCalendar.Properties.Resources.Icon_MiniPlayerOn;
			this.SL_MiniPlayer.Location = new System.Drawing.Point(749, 3);
			this.SL_MiniPlayer.Margin = new System.Windows.Forms.Padding(0);
			this.SL_MiniPlayer.MinimumSize = new System.Drawing.Size(0, 26);
			this.SL_MiniPlayer.Name = "SL_MiniPlayer";
			this.SL_MiniPlayer.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
			this.SL_MiniPlayer.Size = new System.Drawing.Size(45, 26);
			this.SL_MiniPlayer.TabIndex = 3;
			this.SL_MiniPlayer.Click += new System.EventHandler(this.SL_MiniPlayer_Click);
			// 
			// SS_TimeSlider
			// 
			this.SS_TimeSlider.Dock = System.Windows.Forms.DockStyle.Top;
			this.SS_TimeSlider.DrawValues = false;
			this.SS_TimeSlider.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
			this.SS_TimeSlider.FromValue = 0D;
			this.SS_TimeSlider.Location = new System.Drawing.Point(9, 0);
			this.SS_TimeSlider.Margin = new System.Windows.Forms.Padding(0);
			this.SS_TimeSlider.MaxValue = 100D;
			this.SS_TimeSlider.MinValue = 0D;
			this.SS_TimeSlider.Name = "SS_TimeSlider";
			this.SS_TimeSlider.Padding = new System.Windows.Forms.Padding(14, 15, 14, 0);
			this.SS_TimeSlider.Percentage = 0D;
			this.SS_TimeSlider.PercFrom = 0D;
			this.SS_TimeSlider.PercTo = 0D;
			this.SS_TimeSlider.Size = new System.Drawing.Size(876, 28);
			this.SS_TimeSlider.SliderStyle = SlickControls.Controls.SliderStyle.SingleHorizontal;
			this.SS_TimeSlider.TabIndex = 1;
			this.SS_TimeSlider.ToValue = 0D;
			this.SS_TimeSlider.Value = 0D;
			this.SS_TimeSlider.ValueOutput = null;
			this.SS_TimeSlider.ValuesChanged += new System.EventHandler(this.slickSlider1_ValuesChanged);
			this.SS_TimeSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.slickSlider1_MouseDown);
			this.SS_TimeSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.slickSlider1_MouseUp);
			// 
			// TLP_Controls
			// 
			this.TLP_Controls.AutoSize = true;
			this.TLP_Controls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Controls.ColumnCount = 3;
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Controls.Controls.Add(this.L_CurrentTime, 0, 0);
			this.TLP_Controls.Controls.Add(this.TLP_Buttons, 0, 1);
			this.TLP_Controls.Controls.Add(this.SS_TimeSlider, 1, 0);
			this.TLP_Controls.Controls.Add(this.L_Time, 2, 0);
			this.TLP_Controls.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Controls.Location = new System.Drawing.Point(0, 2);
			this.TLP_Controls.Name = "TLP_Controls";
			this.TLP_Controls.RowCount = 2;
			this.TLP_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Controls.Size = new System.Drawing.Size(894, 62);
			this.TLP_Controls.TabIndex = 2;
			// 
			// L_CurrentTime
			// 
			this.L_CurrentTime.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_CurrentTime.AutoSize = true;
			this.L_CurrentTime.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_CurrentTime.Location = new System.Drawing.Point(9, 6);
			this.L_CurrentTime.Margin = new System.Windows.Forms.Padding(9, 0, 0, 0);
			this.L_CurrentTime.Name = "L_CurrentTime";
			this.L_CurrentTime.Size = new System.Drawing.Size(0, 15);
			this.L_CurrentTime.TabIndex = 4;
			// 
			// L_Time
			// 
			this.L_Time.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Time.AutoSize = true;
			this.L_Time.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_Time.Location = new System.Drawing.Point(885, 6);
			this.L_Time.Margin = new System.Windows.Forms.Padding(0, 0, 9, 0);
			this.L_Time.Name = "L_Time";
			this.L_Time.Size = new System.Drawing.Size(0, 15);
			this.L_Time.TabIndex = 2;
			// 
			// P_BotSpacer
			// 
			this.P_BotSpacer.Controls.Add(this.TLP_Controls);
			this.P_BotSpacer.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.P_BotSpacer.Location = new System.Drawing.Point(0, 404);
			this.P_BotSpacer.Margin = new System.Windows.Forms.Padding(0);
			this.P_BotSpacer.Name = "P_BotSpacer";
			this.P_BotSpacer.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.P_BotSpacer.Size = new System.Drawing.Size(894, 65);
			this.P_BotSpacer.TabIndex = 3;
			// 
			// WatchForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(913, 550);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(59)))));
			this.FormIcon = global::TVShowsCalendar.Properties.Resources.TVCalendar;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "WatchForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "WatchForm";
			this.WindowStateChanged += new System.EventHandler(this.WatchForm_WindowStateChanged);
			this.Load += new System.EventHandler(this.WatchForm_Load);
			this.MouseLeave += new System.EventHandler(this.WatchForm_MouseLeave);
			this.base_P_Content.ResumeLayout(false);
			this.TLP_Buttons.ResumeLayout(false);
			this.TLP_Buttons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.SI_More)).EndInit();
			this.TLP_Controls.ResumeLayout(false);
			this.TLP_Controls.PerformLayout();
			this.P_BotSpacer.ResumeLayout(false);
			this.P_BotSpacer.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_Buttons;
		public SlickControls.Controls.SlickLabel SL_Play;
		public SlickControls.Controls.SlickLabel SL_Forward;
		public SlickControls.Controls.SlickLabel SL_Next;
		public SlickControls.Controls.SlickLabel SL_Backwards;
		public SlickControls.Controls.SlickLabel SL_Previous;
		private SlickControls.Controls.SlickSlider SS_TimeSlider;
		private System.Windows.Forms.TableLayoutPanel TLP_Controls;
		private System.Windows.Forms.Panel P_BotSpacer;
		private SlickControls.Controls.SlickIcon SI_More;
		public SlickControls.Controls.SlickLabel SL_Subs;
		public SlickControls.Controls.SlickLabel SL_Audio;
		private SlickControls.Controls.SlickSlider SS_Volume;
		public SlickControls.Controls.SlickLabel SL_MiniPlayer;
		public SlickControls.Controls.SlickLabel SL_FullScreen;
		private System.Windows.Forms.Label L_CurrentTime;
		private System.Windows.Forms.Label L_Time;
	}
}