namespace ShowsCalendar
{
	partial class PC_Player
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
			if (cancelDispose)
			{
				Visible = cancelDispose = false;
				return;
			}

			Data.ActivePlayer = null;

			if (PnP && P_BackContent.Parent is ShowsCalendar.PictureInPictureControl pnp)
				pnp.Dispose();

			P_BackContent.Visible = Visible = false;

			mouseHook?.Uninstall();
			mouseDetector?.Dispose();
			watchTimeTimer?.Dispose();
			controlsHideAnimation?.Dispose();
			topNotchAnimation?.Dispose();
			miniPlayerAnimation?.Dispose();
			subDelayAnimation?.Dispose();
			volumeAnimation?.Dispose();
			fullScreenPlayer?.Dispose();
			upNextAnimation?.Dispose();
			countdownTimer?.Dispose();
			bufferIdentifier?.Dispose();
			controlsHideIdentifier?.Dispose();
			lazyPlayerWaitIdentifier?.Dispose();
			pauseWaitIdentifier?.Dispose();
			subDelayWaitIdentifier?.Dispose();
			subDelaySetIdentifier?.Dispose();
			topNotchIdentifier?.Dispose();
			volumeIdentifier?.Dispose();
			upnextHideIdentifier?.Dispose();

			vlcControl.Parent = null;

			System.Threading.Tasks.Task.Run(() =>
			{
				if (vlcControl.Audio != null)
					vlcControl.Audio.Volume = 0;
				vlcControl.Stop();
				Extensions.ExtensionClass.TryInvoke(vlcControl, vlcControl.Dispose);
			});

			if (disposing)
				components?.Dispose();
			
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PC_Player));
			this.TLP_Controls = new System.Windows.Forms.TableLayoutPanel();
			this.L_CurrentTime = new System.Windows.Forms.Label();
			this.TLP_Buttons = new System.Windows.Forms.TableLayoutPanel();
			this.SL_Play = new SlickControls.SlickLabel();
			this.SL_Forward = new SlickControls.SlickLabel();
			this.SL_Next = new SlickControls.SlickLabel();
			this.SL_Backwards = new SlickControls.SlickLabel();
			this.SL_Previous = new SlickControls.SlickLabel();
			this.SL_Subs = new SlickControls.SlickLabel();
			this.SS_Volume = new SlickControls.SlickSlider();
			this.SL_Audio = new SlickControls.SlickLabel();
			this.SL_More = new SlickControls.SlickLabel();
			this.SL_FullScreen = new SlickControls.SlickLabel();
			this.SL_MiniPlayer = new SlickControls.SlickLabel();
			this.SL_PnP = new SlickControls.SlickLabel();
			this.SS_TimeSlider = new SlickControls.SlickSlider();
			this.L_Time = new System.Windows.Forms.Label();
			this.P_BotSpacer = new System.Windows.Forms.Panel();
			this.TLP_MoreInfo = new System.Windows.Forms.TableLayoutPanel();
			this.P_Spacer = new System.Windows.Forms.Panel();
			this.L_MoreInfo = new System.Windows.Forms.Label();
			this.I_MoreInfo = new System.Windows.Forms.PictureBox();
			this.P_VLC = new System.Windows.Forms.Panel();
			this.C_Rate = new SlickControls.SlickControl();
			this.P_SubDelay = new System.Windows.Forms.TableLayoutPanel();
			this.L_SubDelay = new System.Windows.Forms.Label();
			this.SL_SubDelayMinus = new SlickControls.SlickIcon();
			this.SL_SubDelayPlus = new SlickControls.SlickIcon();
			this.PB_UpNext = new SlickControls.SlickPictureBox();
			this.PB_Volume = new SlickControls.DBPictureBox();
			this.PB_Thumb = new SlickControls.SlickPictureBox();
			this.PB_Loader = new SlickControls.SlickPictureBox();
			this.PB_Close = new System.Windows.Forms.PictureBox();
			this.L_TopNotch = new SlickControls.SlickPictureBox();
			this.PB_Thumbnail = new SlickControls.SlickPictureBox();
			this.UpNextCountdown = new SlickControls.SlickPictureBox();
			this.P_Progress = new SlickControls.DBPanel();
			this.P_Info = new System.Windows.Forms.Panel();
			this.TLP_SimilarContent = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_Suggestions = new System.Windows.Forms.TableLayoutPanel();
			this.SP_Crew = new SlickControls.SlickSectionPanel();
			this.SP_Similar = new SlickControls.SlickSectionPanel();
			this.SP_Cast = new SlickControls.SlickSectionPanel();
			this.TLP_Info = new System.Windows.Forms.TableLayoutPanel();
			this.L_PlotLabel = new System.Windows.Forms.Label();
			this.L_ShowPlotLabel = new System.Windows.Forms.Label();
			this.L_ShowPlot = new System.Windows.Forms.Label();
			this.L_Plot = new System.Windows.Forms.Label();
			this.slickSectionPanel1 = new SlickControls.SlickSectionPanel();
			this.P_AllContent = new System.Windows.Forms.Panel();
			this.P_BackContent = new System.Windows.Forms.Panel();
			this.slickScroll = new SlickControls.SlickScroll();
			this.TLP_MoreInfo2 = new System.Windows.Forms.TableLayoutPanel();
			this.L_MoreInfo2 = new System.Windows.Forms.Label();
			this.I_MoreInfo2 = new System.Windows.Forms.PictureBox();
			this.P_Spacer2 = new System.Windows.Forms.Panel();
			this.TLP_Controls.SuspendLayout();
			this.TLP_Buttons.SuspendLayout();
			this.P_BotSpacer.SuspendLayout();
			this.TLP_MoreInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.I_MoreInfo)).BeginInit();
			this.P_VLC.SuspendLayout();
			this.P_SubDelay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_UpNext)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Volume)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Thumb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Close)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.L_TopNotch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Thumbnail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UpNextCountdown)).BeginInit();
			this.P_Info.SuspendLayout();
			this.TLP_Suggestions.SuspendLayout();
			this.TLP_Info.SuspendLayout();
			this.P_AllContent.SuspendLayout();
			this.P_BackContent.SuspendLayout();
			this.TLP_MoreInfo2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.I_MoreInfo2)).BeginInit();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Location = new System.Drawing.Point(3, 1);
			this.base_Text.Size = new System.Drawing.Size(3, 21);
			this.base_Text.SpaceTriggersClick = false;
			this.base_Text.Text = "";
			// 
			// TLP_Controls
			// 
			this.TLP_Controls.AutoSize = true;
			this.TLP_Controls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Controls.ColumnCount = 3;
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
			this.TLP_Controls.Controls.Add(this.L_CurrentTime, 0, 1);
			this.TLP_Controls.Controls.Add(this.TLP_Buttons, 0, 2);
			this.TLP_Controls.Controls.Add(this.SS_TimeSlider, 1, 1);
			this.TLP_Controls.Controls.Add(this.L_Time, 2, 1);
			this.TLP_Controls.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Controls.Location = new System.Drawing.Point(0, 2);
			this.TLP_Controls.Name = "TLP_Controls";
			this.TLP_Controls.RowCount = 3;
			this.TLP_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Controls.Size = new System.Drawing.Size(696, 60);
			this.TLP_Controls.TabIndex = 2;
			// 
			// L_CurrentTime
			// 
			this.L_CurrentTime.AutoSize = true;
			this.L_CurrentTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_CurrentTime.Location = new System.Drawing.Point(9, 0);
			this.L_CurrentTime.Margin = new System.Windows.Forms.Padding(9, 0, 0, 0);
			this.L_CurrentTime.Name = "L_CurrentTime";
			this.L_CurrentTime.Size = new System.Drawing.Size(1, 30);
			this.L_CurrentTime.TabIndex = 4;
			this.L_CurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TLP_Buttons
			// 
			this.TLP_Buttons.ColumnCount = 13;
			this.TLP_Controls.SetColumnSpan(this.TLP_Buttons, 3);
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.Controls.Add(this.SL_Play, 6, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Forward, 7, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Next, 8, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Backwards, 5, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Previous, 4, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Subs, 0, 0);
			this.TLP_Buttons.Controls.Add(this.SS_Volume, 2, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Audio, 1, 0);
			this.TLP_Buttons.Controls.Add(this.SL_More, 12, 0);
			this.TLP_Buttons.Controls.Add(this.SL_FullScreen, 11, 0);
			this.TLP_Buttons.Controls.Add(this.SL_MiniPlayer, 10, 0);
			this.TLP_Buttons.Controls.Add(this.SL_PnP, 9, 0);
			this.TLP_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Buttons.Location = new System.Drawing.Point(0, 30);
			this.TLP_Buttons.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Buttons.Name = "TLP_Buttons";
			this.TLP_Buttons.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.TLP_Buttons.RowCount = 1;
			this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Buttons.Size = new System.Drawing.Size(696, 30);
			this.TLP_Buttons.TabIndex = 0;
			this.TLP_Buttons.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TLP_Buttons_MouseDown);
			// 
			// SL_Play
			// 
			this.SL_Play.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Play.AutoHideText = false;
			this.SL_Play.AutoSize = true;
			this.SL_Play.ColorShade = null;
			this.SL_Play.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Play.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Play.Image = ((System.Drawing.Image)(resources.GetObject("SL_Play.Image")));
			this.SL_Play.Location = new System.Drawing.Point(331, 3);
			this.SL_Play.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Play.Name = "SL_Play";
			this.SL_Play.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Play.ScrollToOnFocus = false;
			this.SL_Play.Selected = false;
			this.SL_Play.Size = new System.Drawing.Size(39, 24);
			this.SL_Play.SpaceTriggersClick = true;
			this.SL_Play.TabIndex = 4;
			this.SL_Play.Click += new System.EventHandler(this.SL_Play_Click);
			this.SL_Play.Enter += new System.EventHandler(this.PlayerControls_FocusEnter);
			// 
			// SL_Forward
			// 
			this.SL_Forward.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Forward.AutoHideText = false;
			this.SL_Forward.AutoSize = true;
			this.SL_Forward.ColorShade = null;
			this.SL_Forward.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Forward.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Forward.Image = ((System.Drawing.Image)(resources.GetObject("SL_Forward.Image")));
			this.SL_Forward.Location = new System.Drawing.Point(376, 3);
			this.SL_Forward.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Forward.Name = "SL_Forward";
			this.SL_Forward.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Forward.ScrollToOnFocus = false;
			this.SL_Forward.Selected = false;
			this.SL_Forward.Size = new System.Drawing.Size(39, 24);
			this.SL_Forward.SpaceTriggersClick = true;
			this.SL_Forward.TabIndex = 5;
			this.SL_Forward.Click += new System.EventHandler(this.SL_Forward_Click);
			this.SL_Forward.Enter += new System.EventHandler(this.PlayerControls_FocusEnter);
			// 
			// SL_Next
			// 
			this.SL_Next.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Next.AutoHideText = false;
			this.SL_Next.AutoSize = true;
			this.SL_Next.ColorShade = null;
			this.SL_Next.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Next.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Next.Image = ((System.Drawing.Image)(resources.GetObject("SL_Next.Image")));
			this.SL_Next.Location = new System.Drawing.Point(421, 3);
			this.SL_Next.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Next.Name = "SL_Next";
			this.SL_Next.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Next.ScrollToOnFocus = false;
			this.SL_Next.Selected = false;
			this.SL_Next.Size = new System.Drawing.Size(39, 24);
			this.SL_Next.SpaceTriggersClick = true;
			this.SL_Next.TabIndex = 6;
			this.SL_Next.Visible = false;
			this.SL_Next.Click += new System.EventHandler(this.SL_Next_Click);
			this.SL_Next.Enter += new System.EventHandler(this.PlayerControls_FocusEnter);
			// 
			// SL_Backwards
			// 
			this.SL_Backwards.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Backwards.AutoHideText = false;
			this.SL_Backwards.AutoSize = true;
			this.SL_Backwards.ColorShade = null;
			this.SL_Backwards.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Backwards.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Backwards.Image = ((System.Drawing.Image)(resources.GetObject("SL_Backwards.Image")));
			this.SL_Backwards.Location = new System.Drawing.Point(286, 3);
			this.SL_Backwards.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Backwards.Name = "SL_Backwards";
			this.SL_Backwards.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Backwards.ScrollToOnFocus = false;
			this.SL_Backwards.Selected = false;
			this.SL_Backwards.Size = new System.Drawing.Size(39, 24);
			this.SL_Backwards.SpaceTriggersClick = true;
			this.SL_Backwards.TabIndex = 3;
			this.SL_Backwards.Click += new System.EventHandler(this.SL_Backwards_Click);
			this.SL_Backwards.Enter += new System.EventHandler(this.PlayerControls_FocusEnter);
			// 
			// SL_Previous
			// 
			this.SL_Previous.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Previous.AutoHideText = false;
			this.SL_Previous.AutoSize = true;
			this.SL_Previous.ColorShade = null;
			this.SL_Previous.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Previous.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Previous.Image = ((System.Drawing.Image)(resources.GetObject("SL_Previous.Image")));
			this.SL_Previous.Location = new System.Drawing.Point(241, 3);
			this.SL_Previous.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Previous.Name = "SL_Previous";
			this.SL_Previous.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Previous.ScrollToOnFocus = false;
			this.SL_Previous.Selected = false;
			this.SL_Previous.Size = new System.Drawing.Size(39, 24);
			this.SL_Previous.SpaceTriggersClick = true;
			this.SL_Previous.TabIndex = 2;
			this.SL_Previous.Visible = false;
			this.SL_Previous.Click += new System.EventHandler(this.SL_Previous_Click);
			this.SL_Previous.Enter += new System.EventHandler(this.PlayerControls_FocusEnter);
			// 
			// SL_Subs
			// 
			this.SL_Subs.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Subs.AutoHideText = false;
			this.SL_Subs.AutoSize = true;
			this.SL_Subs.ColorShade = null;
			this.SL_Subs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Subs.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Subs.Image = ((System.Drawing.Image)(resources.GetObject("SL_Subs.Image")));
			this.SL_Subs.Location = new System.Drawing.Point(3, 3);
			this.SL_Subs.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Subs.Name = "SL_Subs";
			this.SL_Subs.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Subs.ScrollToOnFocus = false;
			this.SL_Subs.Selected = false;
			this.SL_Subs.Size = new System.Drawing.Size(39, 24);
			this.SL_Subs.SpaceTriggersClick = true;
			this.SL_Subs.TabIndex = 0;
			this.SL_Subs.Click += new System.EventHandler(this.SL_Subs_Click);
			this.SL_Subs.Enter += new System.EventHandler(this.PlayerControls_FocusEnter);
			// 
			// SS_Volume
			// 
			this.SS_Volume.AnimatedValue = 0;
			this.TLP_Buttons.SetColumnSpan(this.SS_Volume, 2);
			this.SS_Volume.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SS_Volume.Dock = System.Windows.Forms.DockStyle.Left;
			this.SS_Volume.FromValue = 0D;
			this.SS_Volume.Location = new System.Drawing.Point(90, 3);
			this.SS_Volume.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.SS_Volume.MaxValue = 200D;
			this.SS_Volume.MinValue = 0D;
			this.SS_Volume.Name = "SS_Volume";
			this.SS_Volume.Padding = new System.Windows.Forms.Padding(8, 8, 8, 4);
			this.SS_Volume.Percentage = 0.5D;
			this.SS_Volume.PercFrom = 0D;
			this.SS_Volume.PercTo = 0.5D;
			this.SS_Volume.ShowValues = false;
			this.SS_Volume.Size = new System.Drawing.Size(100, 24);
			this.SS_Volume.SliderStyle = SlickControls.SliderStyle.SingleHorizontal;
			this.SS_Volume.TabIndex = 5;
			this.SS_Volume.TabStop = false;
			this.SS_Volume.TargetAnimationValue = 0;
			this.SS_Volume.ToValue = 100D;
			this.SS_Volume.Value = 100D;
			this.SS_Volume.ValueOutput = null;
			this.SS_Volume.ValuesChanged += new System.EventHandler(this.SS_Volume_ValuesChanged);
			this.SS_Volume.Click += new System.EventHandler(this.SS_Volume_ValuesChanged);
			// 
			// SL_Audio
			// 
			this.SL_Audio.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Audio.AutoHideText = false;
			this.SL_Audio.AutoSize = true;
			this.SL_Audio.ColorShade = null;
			this.SL_Audio.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Audio.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Audio.Image = ((System.Drawing.Image)(resources.GetObject("SL_Audio.Image")));
			this.SL_Audio.Location = new System.Drawing.Point(48, 3);
			this.SL_Audio.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Audio.Name = "SL_Audio";
			this.SL_Audio.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Audio.ScrollToOnFocus = false;
			this.SL_Audio.Selected = false;
			this.SL_Audio.Size = new System.Drawing.Size(39, 24);
			this.SL_Audio.SpaceTriggersClick = true;
			this.SL_Audio.TabIndex = 1;
			this.SL_Audio.Enter += new System.EventHandler(this.PlayerControls_FocusEnter);
			this.SL_Audio.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SL_Audio_Click);
			// 
			// SL_More
			// 
			this.SL_More.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_More.AutoHideText = false;
			this.SL_More.AutoSize = true;
			this.SL_More.ColorShade = null;
			this.SL_More.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_More.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_More.Image = ((System.Drawing.Image)(resources.GetObject("SL_More.Image")));
			this.SL_More.Location = new System.Drawing.Point(654, 3);
			this.SL_More.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_More.Name = "SL_More";
			this.SL_More.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_More.ScrollToOnFocus = false;
			this.SL_More.Selected = false;
			this.SL_More.Size = new System.Drawing.Size(39, 24);
			this.SL_More.SpaceTriggersClick = true;
			this.SL_More.TabIndex = 10;
			this.SL_More.Enter += new System.EventHandler(this.PlayerControls_FocusEnter);
			// 
			// SL_FullScreen
			// 
			this.SL_FullScreen.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_FullScreen.AutoHideText = false;
			this.SL_FullScreen.AutoSize = true;
			this.SL_FullScreen.ColorShade = null;
			this.SL_FullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_FullScreen.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_FullScreen.Image = ((System.Drawing.Image)(resources.GetObject("SL_FullScreen.Image")));
			this.SL_FullScreen.Location = new System.Drawing.Point(609, 3);
			this.SL_FullScreen.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_FullScreen.Name = "SL_FullScreen";
			this.SL_FullScreen.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_FullScreen.ScrollToOnFocus = false;
			this.SL_FullScreen.Selected = false;
			this.SL_FullScreen.Size = new System.Drawing.Size(39, 24);
			this.SL_FullScreen.SpaceTriggersClick = true;
			this.SL_FullScreen.TabIndex = 9;
			this.SL_FullScreen.Click += new System.EventHandler(this.SL_FullScreen_Click);
			this.SL_FullScreen.Enter += new System.EventHandler(this.PlayerControls_FocusEnter);
			// 
			// SL_MiniPlayer
			// 
			this.SL_MiniPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SL_MiniPlayer.AutoHideText = false;
			this.SL_MiniPlayer.AutoSize = true;
			this.SL_MiniPlayer.ColorShade = null;
			this.SL_MiniPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_MiniPlayer.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_MiniPlayer.Image = ((System.Drawing.Image)(resources.GetObject("SL_MiniPlayer.Image")));
			this.SL_MiniPlayer.Location = new System.Drawing.Point(564, 3);
			this.SL_MiniPlayer.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_MiniPlayer.Name = "SL_MiniPlayer";
			this.SL_MiniPlayer.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_MiniPlayer.ScrollToOnFocus = false;
			this.SL_MiniPlayer.Selected = false;
			this.SL_MiniPlayer.Size = new System.Drawing.Size(39, 24);
			this.SL_MiniPlayer.SpaceTriggersClick = true;
			this.SL_MiniPlayer.TabIndex = 8;
			this.SL_MiniPlayer.Click += new System.EventHandler(this.SL_MiniPlayer_Click);
			this.SL_MiniPlayer.Enter += new System.EventHandler(this.PlayerControls_FocusEnter);
			// 
			// SL_PnP
			// 
			this.SL_PnP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SL_PnP.AutoHideText = false;
			this.SL_PnP.AutoSize = true;
			this.SL_PnP.ColorShade = null;
			this.SL_PnP.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_PnP.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_PnP.Image = ((System.Drawing.Image)(resources.GetObject("SL_PnP.Image")));
			this.SL_PnP.Location = new System.Drawing.Point(519, 3);
			this.SL_PnP.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_PnP.Name = "SL_PnP";
			this.SL_PnP.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_PnP.ScrollToOnFocus = false;
			this.SL_PnP.Selected = false;
			this.SL_PnP.Size = new System.Drawing.Size(39, 24);
			this.SL_PnP.SpaceTriggersClick = true;
			this.SL_PnP.TabIndex = 7;
			this.SL_PnP.Click += new System.EventHandler(this.SL_PnP_Click);
			this.SL_PnP.Enter += new System.EventHandler(this.PlayerControls_FocusEnter);
			// 
			// SS_TimeSlider
			// 
			this.SS_TimeSlider.AnimatedValue = 0;
			this.SS_TimeSlider.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SS_TimeSlider.Dock = System.Windows.Forms.DockStyle.Top;
			this.SS_TimeSlider.FromValue = 0D;
			this.SS_TimeSlider.Location = new System.Drawing.Point(0, 0);
			this.SS_TimeSlider.Margin = new System.Windows.Forms.Padding(0);
			this.SS_TimeSlider.MaximumSize = new System.Drawing.Size(9999, 28);
			this.SS_TimeSlider.MaxValue = 100D;
			this.SS_TimeSlider.MinValue = 0D;
			this.SS_TimeSlider.Name = "SS_TimeSlider";
			this.SS_TimeSlider.Padding = new System.Windows.Forms.Padding(14, 15, 14, 0);
			this.SS_TimeSlider.Percentage = 0D;
			this.SS_TimeSlider.PercFrom = 0D;
			this.SS_TimeSlider.PercTo = 0D;
			this.SS_TimeSlider.ShowValues = false;
			this.SS_TimeSlider.Size = new System.Drawing.Size(696, 28);
			this.SS_TimeSlider.SliderStyle = SlickControls.SliderStyle.SingleHorizontal;
			this.SS_TimeSlider.TabIndex = 1;
			this.SS_TimeSlider.TabStop = false;
			this.SS_TimeSlider.TargetAnimationValue = 0;
			this.SS_TimeSlider.ToValue = 0D;
			this.SS_TimeSlider.Value = 0D;
			this.SS_TimeSlider.ValueOutput = null;
			this.SS_TimeSlider.ValuesChanged += new System.EventHandler(this.SS_TimeSlider_ValuesChanged);
			this.SS_TimeSlider.MouseEnter += new System.EventHandler(this.SS_TimeSlider_MouseEnter);
			this.SS_TimeSlider.MouseLeave += new System.EventHandler(this.SS_TimeSlider_MouseLeave);
			this.SS_TimeSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SS_TimeSlider_MouseMove);
			// 
			// L_Time
			// 
			this.L_Time.AutoSize = true;
			this.L_Time.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_Time.Location = new System.Drawing.Point(696, 0);
			this.L_Time.Margin = new System.Windows.Forms.Padding(0, 0, 9, 0);
			this.L_Time.Name = "L_Time";
			this.L_Time.Size = new System.Drawing.Size(1, 30);
			this.L_Time.TabIndex = 2;
			this.L_Time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.L_Time.MouseEnter += new System.EventHandler(this.L_Time_MouseEnter);
			this.L_Time.MouseLeave += new System.EventHandler(this.L_Time_MouseLeave);
			// 
			// P_BotSpacer
			// 
			this.P_BotSpacer.Controls.Add(this.TLP_MoreInfo);
			this.P_BotSpacer.Controls.Add(this.TLP_Controls);
			this.P_BotSpacer.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_BotSpacer.Location = new System.Drawing.Point(0, 275);
			this.P_BotSpacer.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.P_BotSpacer.Name = "P_BotSpacer";
			this.P_BotSpacer.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.P_BotSpacer.Size = new System.Drawing.Size(696, 82);
			this.P_BotSpacer.TabIndex = 0;
			// 
			// TLP_MoreInfo
			// 
			this.TLP_MoreInfo.ColumnCount = 4;
			this.TLP_MoreInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_MoreInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MoreInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MoreInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_MoreInfo.Controls.Add(this.P_Spacer, 0, 1);
			this.TLP_MoreInfo.Controls.Add(this.L_MoreInfo, 2, 0);
			this.TLP_MoreInfo.Controls.Add(this.I_MoreInfo, 1, 0);
			this.TLP_MoreInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.TLP_MoreInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_MoreInfo.Location = new System.Drawing.Point(0, 62);
			this.TLP_MoreInfo.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_MoreInfo.Name = "TLP_MoreInfo";
			this.TLP_MoreInfo.RowCount = 2;
			this.TLP_MoreInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_MoreInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_MoreInfo.Size = new System.Drawing.Size(696, 20);
			this.TLP_MoreInfo.TabIndex = 17;
			this.TLP_MoreInfo.Tag = "NoMouseDown";
			this.TLP_MoreInfo.Click += new System.EventHandler(this.TLP_MoreInfo_Click);
			this.TLP_MoreInfo.MouseEnter += new System.EventHandler(this.TLP_MoreInfo_MouseEnter);
			this.TLP_MoreInfo.MouseLeave += new System.EventHandler(this.TLP_MoreInfo_MouseLeave);
			// 
			// P_Spacer
			// 
			this.TLP_MoreInfo.SetColumnSpan(this.P_Spacer, 4);
			this.P_Spacer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Spacer.Location = new System.Drawing.Point(10, 19);
			this.P_Spacer.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.P_Spacer.Name = "P_Spacer";
			this.P_Spacer.Size = new System.Drawing.Size(676, 1);
			this.P_Spacer.TabIndex = 3;
			// 
			// L_MoreInfo
			// 
			this.L_MoreInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_MoreInfo.AutoSize = true;
			this.L_MoreInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_MoreInfo.Location = new System.Drawing.Point(303, 0);
			this.L_MoreInfo.Name = "L_MoreInfo";
			this.L_MoreInfo.Size = new System.Drawing.Size(105, 19);
			this.L_MoreInfo.TabIndex = 0;
			this.L_MoreInfo.Tag = "NoMouseDown";
			this.L_MoreInfo.Text = "More Info";
			this.L_MoreInfo.Click += new System.EventHandler(this.TLP_MoreInfo_Click);
			this.L_MoreInfo.MouseEnter += new System.EventHandler(this.TLP_MoreInfo_MouseEnter);
			this.L_MoreInfo.MouseLeave += new System.EventHandler(this.TLP_MoreInfo_MouseLeave);
			// 
			// I_MoreInfo
			// 
			this.I_MoreInfo.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.I_MoreInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_MoreInfo.Image = global::ShowsCalendar.Properties.Resources.ArrowDown;
			this.I_MoreInfo.Location = new System.Drawing.Point(284, 1);
			this.I_MoreInfo.Margin = new System.Windows.Forms.Padding(0);
			this.I_MoreInfo.Name = "I_MoreInfo";
			this.I_MoreInfo.Size = new System.Drawing.Size(16, 16);
			this.I_MoreInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.I_MoreInfo.TabIndex = 1;
			this.I_MoreInfo.TabStop = false;
			this.I_MoreInfo.Click += new System.EventHandler(this.TLP_MoreInfo_Click);
			this.I_MoreInfo.MouseEnter += new System.EventHandler(this.TLP_MoreInfo_MouseEnter);
			this.I_MoreInfo.MouseLeave += new System.EventHandler(this.TLP_MoreInfo_MouseLeave);
			// 
			// P_VLC
			// 
			this.P_VLC.Controls.Add(this.C_Rate);
			this.P_VLC.Controls.Add(this.P_SubDelay);
			this.P_VLC.Controls.Add(this.PB_UpNext);
			this.P_VLC.Controls.Add(this.PB_Volume);
			this.P_VLC.Controls.Add(this.PB_Thumb);
			this.P_VLC.Controls.Add(this.PB_Loader);
			this.P_VLC.Controls.Add(this.PB_Close);
			this.P_VLC.Controls.Add(this.L_TopNotch);
			this.P_VLC.Controls.Add(this.PB_Thumbnail);
			this.P_VLC.Controls.Add(this.UpNextCountdown);
			this.P_VLC.Controls.Add(this.P_Progress);
			this.P_VLC.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_VLC.Location = new System.Drawing.Point(0, 0);
			this.P_VLC.Name = "P_VLC";
			this.P_VLC.Size = new System.Drawing.Size(696, 275);
			this.P_VLC.TabIndex = 14;
			// 
			// C_Rate
			// 
			this.C_Rate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.C_Rate.Cursor = System.Windows.Forms.Cursors.Hand;
			this.C_Rate.Location = new System.Drawing.Point(28, 104);
			this.C_Rate.Name = "C_Rate";
			this.C_Rate.Size = new System.Drawing.Size(122, 46);
			this.C_Rate.TabIndex = 17;
			this.C_Rate.TabStop = false;
			this.C_Rate.Visible = false;
			this.C_Rate.Paint += new System.Windows.Forms.PaintEventHandler(this.C_Rate_Paint);
			this.C_Rate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.C_Rate_MouseClick);
			// 
			// P_SubDelay
			// 
			this.P_SubDelay.ColumnCount = 3;
			this.P_SubDelay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.P_SubDelay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_SubDelay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.P_SubDelay.Controls.Add(this.L_SubDelay, 1, 0);
			this.P_SubDelay.Controls.Add(this.SL_SubDelayMinus, 0, 0);
			this.P_SubDelay.Controls.Add(this.SL_SubDelayPlus, 2, 0);
			this.P_SubDelay.Location = new System.Drawing.Point(265, 92);
			this.P_SubDelay.Name = "P_SubDelay";
			this.P_SubDelay.Padding = new System.Windows.Forms.Padding(1);
			this.P_SubDelay.RowCount = 1;
			this.P_SubDelay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_SubDelay.Size = new System.Drawing.Size(150, 40);
			this.P_SubDelay.TabIndex = 9;
			this.P_SubDelay.Visible = false;
			// 
			// L_SubDelay
			// 
			this.L_SubDelay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_SubDelay.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_SubDelay.Location = new System.Drawing.Point(31, 1);
			this.L_SubDelay.Margin = new System.Windows.Forms.Padding(0);
			this.L_SubDelay.Name = "L_SubDelay";
			this.L_SubDelay.Size = new System.Drawing.Size(88, 38);
			this.L_SubDelay.TabIndex = 0;
			this.L_SubDelay.Text = "0 s";
			this.L_SubDelay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SL_SubDelayMinus
			// 
			this.SL_SubDelayMinus.ActiveColor = null;
			this.SL_SubDelayMinus.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_SubDelayMinus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SL_SubDelayMinus.Image = ((System.Drawing.Image)(resources.GetObject("SL_SubDelayMinus.Image")));
			this.SL_SubDelayMinus.Location = new System.Drawing.Point(1, 1);
			this.SL_SubDelayMinus.Margin = new System.Windows.Forms.Padding(0);
			this.SL_SubDelayMinus.Name = "SL_SubDelayMinus";
			this.SL_SubDelayMinus.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_SubDelayMinus.Size = new System.Drawing.Size(30, 38);
			this.SL_SubDelayMinus.TabIndex = 3;
			this.SL_SubDelayMinus.TabStop = false;
			this.SL_SubDelayMinus.Click += new System.EventHandler(this.SL_SubDelayMinus_Click);
			// 
			// SL_SubDelayPlus
			// 
			this.SL_SubDelayPlus.ActiveColor = null;
			this.SL_SubDelayPlus.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_SubDelayPlus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SL_SubDelayPlus.Image = ((System.Drawing.Image)(resources.GetObject("SL_SubDelayPlus.Image")));
			this.SL_SubDelayPlus.Location = new System.Drawing.Point(119, 1);
			this.SL_SubDelayPlus.Margin = new System.Windows.Forms.Padding(0);
			this.SL_SubDelayPlus.Name = "SL_SubDelayPlus";
			this.SL_SubDelayPlus.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_SubDelayPlus.Size = new System.Drawing.Size(30, 38);
			this.SL_SubDelayPlus.TabIndex = 3;
			this.SL_SubDelayPlus.TabStop = false;
			this.SL_SubDelayPlus.Click += new System.EventHandler(this.SL_SubDelayPlus_Click);
			// 
			// PB_UpNext
			// 
			this.PB_UpNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.PB_UpNext.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_UpNext.LoaderSpeed = 1D;
			this.PB_UpNext.Location = new System.Drawing.Point(348, 137);
			this.PB_UpNext.Name = "PB_UpNext";
			this.PB_UpNext.Size = new System.Drawing.Size(32, 32);
			this.PB_UpNext.TabIndex = 6;
			this.PB_UpNext.TabStop = false;
			this.PB_UpNext.UserDraw = true;
			this.PB_UpNext.Visible = false;
			this.PB_UpNext.Click += new System.EventHandler(this.PB_UpNext_Click);
			this.PB_UpNext.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_UpNext_Paint);
			// 
			// PB_Volume
			// 
			this.PB_Volume.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.PB_Volume.Location = new System.Drawing.Point(340, 129);
			this.PB_Volume.Name = "PB_Volume";
			this.PB_Volume.Size = new System.Drawing.Size(32, 32);
			this.PB_Volume.TabIndex = 5;
			this.PB_Volume.TabStop = false;
			this.PB_Volume.Visible = false;
			this.PB_Volume.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_Volume_Paint);
			// 
			// PB_Thumb
			// 
			this.PB_Thumb.LoaderSpeed = 1D;
			this.PB_Thumb.Location = new System.Drawing.Point(135, 157);
			this.PB_Thumb.Name = "PB_Thumb";
			this.PB_Thumb.Size = new System.Drawing.Size(160, 90);
			this.PB_Thumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Thumb.TabIndex = 4;
			this.PB_Thumb.TabStop = false;
			this.PB_Thumb.UserDraw = true;
			this.PB_Thumb.Visible = false;
			this.PB_Thumb.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_Thumb_Paint);
			// 
			// PB_Loader
			// 
			this.PB_Loader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_Loader.LoaderSpeed = 1D;
			this.PB_Loader.Loading = true;
			this.PB_Loader.Location = new System.Drawing.Point(0, 0);
			this.PB_Loader.Name = "PB_Loader";
			this.PB_Loader.Size = new System.Drawing.Size(696, 273);
			this.PB_Loader.TabIndex = 3;
			this.PB_Loader.TabStop = false;
			this.PB_Loader.Visible = false;
			// 
			// PB_Close
			// 
			this.PB_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PB_Close.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Close.Location = new System.Drawing.Point(676, 0);
			this.PB_Close.Name = "PB_Close";
			this.PB_Close.Size = new System.Drawing.Size(20, 20);
			this.PB_Close.TabIndex = 1;
			this.PB_Close.TabStop = false;
			this.PB_Close.Visible = false;
			this.PB_Close.Click += new System.EventHandler(this.PB_Close_Click);
			this.PB_Close.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_Close_Paint);
			// 
			// L_TopNotch
			// 
			this.L_TopNotch.Font = new System.Drawing.Font("Nirmala UI", 12.75F, System.Drawing.FontStyle.Bold);
			this.L_TopNotch.LoaderSpeed = 1D;
			this.L_TopNotch.Location = new System.Drawing.Point(0, 30);
			this.L_TopNotch.MinimumSize = new System.Drawing.Size(0, 32);
			this.L_TopNotch.Name = "L_TopNotch";
			this.L_TopNotch.Size = new System.Drawing.Size(0, 32);
			this.L_TopNotch.TabIndex = 0;
			this.L_TopNotch.TabStop = false;
			this.L_TopNotch.Paint += new System.Windows.Forms.PaintEventHandler(this.L_TopNotch_Paint);
			// 
			// PB_Thumbnail
			// 
			this.PB_Thumbnail.BackColor = System.Drawing.Color.Black;
			this.PB_Thumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_Thumbnail.LoaderSpeed = 1D;
			this.PB_Thumbnail.Location = new System.Drawing.Point(0, 0);
			this.PB_Thumbnail.Name = "PB_Thumbnail";
			this.PB_Thumbnail.Size = new System.Drawing.Size(696, 273);
			this.PB_Thumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Thumbnail.TabIndex = 7;
			this.PB_Thumbnail.TabStop = false;
			this.PB_Thumbnail.Visible = false;
			// 
			// UpNextCountdown
			// 
			this.UpNextCountdown.BackColor = System.Drawing.Color.Black;
			this.UpNextCountdown.Cursor = System.Windows.Forms.Cursors.Hand;
			this.UpNextCountdown.Dock = System.Windows.Forms.DockStyle.Fill;
			this.UpNextCountdown.LoaderSpeed = 1D;
			this.UpNextCountdown.Location = new System.Drawing.Point(0, 0);
			this.UpNextCountdown.Name = "UpNextCountdown";
			this.UpNextCountdown.Size = new System.Drawing.Size(696, 273);
			this.UpNextCountdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.UpNextCountdown.TabIndex = 8;
			this.UpNextCountdown.TabStop = false;
			this.UpNextCountdown.UserDraw = true;
			this.UpNextCountdown.Visible = false;
			this.UpNextCountdown.Paint += new System.Windows.Forms.PaintEventHandler(this.UpNextCountdown_Paint);
			this.UpNextCountdown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UpNextCountdown_MouseClick);
			// 
			// P_Progress
			// 
			this.P_Progress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.P_Progress.Location = new System.Drawing.Point(0, 273);
			this.P_Progress.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.P_Progress.Name = "P_Progress";
			this.P_Progress.Size = new System.Drawing.Size(696, 2);
			this.P_Progress.TabIndex = 16;
			this.P_Progress.Paint += new System.Windows.Forms.PaintEventHandler(this.P_Progress_Paint);
			// 
			// P_Info
			// 
			this.P_Info.AutoSize = true;
			this.P_Info.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Info.Controls.Add(this.TLP_SimilarContent);
			this.P_Info.Controls.Add(this.TLP_Suggestions);
			this.P_Info.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Info.Location = new System.Drawing.Point(0, 357);
			this.P_Info.Name = "P_Info";
			this.P_Info.Size = new System.Drawing.Size(696, 460);
			this.P_Info.TabIndex = 1;
			// 
			// TLP_SimilarContent
			// 
			this.TLP_SimilarContent.AutoSize = true;
			this.TLP_SimilarContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_SimilarContent.ColumnCount = 2;
			this.TLP_SimilarContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_SimilarContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_SimilarContent.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_SimilarContent.Location = new System.Drawing.Point(0, 410);
			this.TLP_SimilarContent.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_SimilarContent.MinimumSize = new System.Drawing.Size(800, 50);
			this.TLP_SimilarContent.Name = "TLP_SimilarContent";
			this.TLP_SimilarContent.Padding = new System.Windows.Forms.Padding(43, 0, 0, 0);
			this.TLP_SimilarContent.RowCount = 1;
			this.TLP_SimilarContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_SimilarContent.Size = new System.Drawing.Size(800, 50);
			this.TLP_SimilarContent.TabIndex = 22;
			// 
			// TLP_Suggestions
			// 
			this.TLP_Suggestions.AutoSize = true;
			this.TLP_Suggestions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Suggestions.ColumnCount = 4;
			this.TLP_Suggestions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
			this.TLP_Suggestions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Suggestions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Suggestions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Suggestions.Controls.Add(this.SP_Crew, 0, 4);
			this.TLP_Suggestions.Controls.Add(this.SP_Similar, 0, 5);
			this.TLP_Suggestions.Controls.Add(this.SP_Cast, 0, 3);
			this.TLP_Suggestions.Controls.Add(this.TLP_Info, 2, 1);
			this.TLP_Suggestions.Controls.Add(this.slickSectionPanel1, 0, 0);
			this.TLP_Suggestions.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Suggestions.Location = new System.Drawing.Point(0, 0);
			this.TLP_Suggestions.Name = "TLP_Suggestions";
			this.TLP_Suggestions.RowCount = 6;
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Suggestions.Size = new System.Drawing.Size(696, 410);
			this.TLP_Suggestions.TabIndex = 21;
			// 
			// SP_Crew
			// 
			this.SP_Crew.Active = false;
			this.SP_Crew.AutoHide = true;
			this.SP_Crew.AutoSize = true;
			this.SP_Crew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Suggestions.SetColumnSpan(this.SP_Crew, 4);
			this.SP_Crew.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Crew.Flavor = null;
			this.SP_Crew.Icon = global::ShowsCalendar.Properties.Resources.Big_Crew;
			this.SP_Crew.Info = "Know the people behind the works of this episode";
			this.SP_Crew.Location = new System.Drawing.Point(3, 351);
			this.SP_Crew.MaximumSize = new System.Drawing.Size(696, 2147483647);
			this.SP_Crew.MinimumSize = new System.Drawing.Size(696, 55);
			this.SP_Crew.Name = "SP_Crew";
			this.SP_Crew.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Crew.Size = new System.Drawing.Size(696, 55);
			this.SP_Crew.TabIndex = 6;
			this.SP_Crew.Text = "Who\'s behind this?";
			// 
			// SP_Similar
			// 
			this.SP_Similar.Active = false;
			this.SP_Similar.AutoHide = false;
			this.SP_Similar.AutoSize = true;
			this.SP_Similar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Suggestions.SetColumnSpan(this.SP_Similar, 4);
			this.SP_Similar.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Similar.Flavor = null;
			this.SP_Similar.Icon = global::ShowsCalendar.Properties.Resources.Big_Rating;
			this.SP_Similar.Info = "Recognize someone? You probably saw them here";
			this.SP_Similar.Location = new System.Drawing.Point(3, 412);
			this.SP_Similar.MaximumSize = new System.Drawing.Size(696, 2147483647);
			this.SP_Similar.MinimumSize = new System.Drawing.Size(696, 55);
			this.SP_Similar.Name = "SP_Similar";
			this.SP_Similar.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Similar.Size = new System.Drawing.Size(696, 55);
			this.SP_Similar.TabIndex = 5;
			this.SP_Similar.Text = "Where have I seen them?";
			// 
			// SP_Cast
			// 
			this.SP_Cast.Active = false;
			this.SP_Cast.AutoHide = true;
			this.SP_Cast.AutoSize = true;
			this.SP_Cast.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Suggestions.SetColumnSpan(this.SP_Cast, 4);
			this.SP_Cast.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Cast.Flavor = null;
			this.SP_Cast.Icon = global::ShowsCalendar.Properties.Resources.Big_Cast;
			this.SP_Cast.Info = "From your beloved cast to this episode\'s notable guest stars";
			this.SP_Cast.Location = new System.Drawing.Point(3, 290);
			this.SP_Cast.MaximumSize = new System.Drawing.Size(696, 2147483647);
			this.SP_Cast.MinimumSize = new System.Drawing.Size(696, 55);
			this.SP_Cast.Name = "SP_Cast";
			this.SP_Cast.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.SP_Cast.Size = new System.Drawing.Size(696, 55);
			this.SP_Cast.TabIndex = 4;
			this.SP_Cast.Text = "Who\'s in this?";
			// 
			// TLP_Info
			// 
			this.TLP_Info.AutoSize = true;
			this.TLP_Info.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Info.ColumnCount = 1;
			this.TLP_Suggestions.SetColumnSpan(this.TLP_Info, 2);
			this.TLP_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Info.Controls.Add(this.L_PlotLabel, 0, 0);
			this.TLP_Info.Controls.Add(this.L_ShowPlotLabel, 0, 2);
			this.TLP_Info.Controls.Add(this.L_ShowPlot, 0, 3);
			this.TLP_Info.Controls.Add(this.L_Plot, 0, 1);
			this.TLP_Info.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Info.Location = new System.Drawing.Point(603, 64);
			this.TLP_Info.Name = "TLP_Info";
			this.TLP_Info.Padding = new System.Windows.Forms.Padding(10);
			this.TLP_Info.RowCount = 4;
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Info.Size = new System.Drawing.Size(90, 220);
			this.TLP_Info.TabIndex = 2;
			// 
			// L_PlotLabel
			// 
			this.L_PlotLabel.AutoSize = true;
			this.L_PlotLabel.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_PlotLabel.Location = new System.Drawing.Point(13, 13);
			this.L_PlotLabel.Margin = new System.Windows.Forms.Padding(3);
			this.L_PlotLabel.Name = "L_PlotLabel";
			this.L_PlotLabel.Size = new System.Drawing.Size(41, 20);
			this.L_PlotLabel.TabIndex = 0;
			this.L_PlotLabel.Text = "Plot:";
			// 
			// L_ShowPlotLabel
			// 
			this.L_ShowPlotLabel.AutoSize = true;
			this.L_ShowPlotLabel.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_ShowPlotLabel.Location = new System.Drawing.Point(13, 119);
			this.L_ShowPlotLabel.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.L_ShowPlotLabel.Name = "L_ShowPlotLabel";
			this.L_ShowPlotLabel.Size = new System.Drawing.Size(51, 20);
			this.L_ShowPlotLabel.TabIndex = 2;
			this.L_ShowPlotLabel.Text = "Show:";
			// 
			// L_ShowPlot
			// 
			this.L_ShowPlot.AutoSize = true;
			this.L_ShowPlot.Location = new System.Drawing.Point(14, 146);
			this.L_ShowPlot.Margin = new System.Windows.Forms.Padding(4);
			this.L_ShowPlot.Name = "L_ShowPlot";
			this.L_ShowPlot.Size = new System.Drawing.Size(57, 60);
			this.L_ShowPlot.TabIndex = 1;
			this.L_ShowPlot.Text = "label2";
			// 
			// L_Plot
			// 
			this.L_Plot.AutoSize = true;
			this.L_Plot.Location = new System.Drawing.Point(14, 40);
			this.L_Plot.Margin = new System.Windows.Forms.Padding(4);
			this.L_Plot.Name = "L_Plot";
			this.L_Plot.Size = new System.Drawing.Size(57, 60);
			this.L_Plot.TabIndex = 1;
			this.L_Plot.Text = "label2";
			// 
			// slickSectionPanel1
			// 
			this.slickSectionPanel1.Active = false;
			this.slickSectionPanel1.AutoHide = false;
			this.slickSectionPanel1.AutoSize = true;
			this.slickSectionPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Suggestions.SetColumnSpan(this.slickSectionPanel1, 4);
			this.slickSectionPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSectionPanel1.Flavor = null;
			this.slickSectionPanel1.Icon = global::ShowsCalendar.Properties.Resources.Big_Play;
			this.slickSectionPanel1.Info = "More about what\'s up there";
			this.slickSectionPanel1.Location = new System.Drawing.Point(3, 3);
			this.slickSectionPanel1.MaximumSize = new System.Drawing.Size(696, 2147483647);
			this.slickSectionPanel1.MinimumSize = new System.Drawing.Size(630, 55);
			this.slickSectionPanel1.Name = "slickSectionPanel1";
			this.slickSectionPanel1.Padding = new System.Windows.Forms.Padding(43, 54, 0, 0);
			this.slickSectionPanel1.Size = new System.Drawing.Size(690, 55);
			this.slickSectionPanel1.TabIndex = 3;
			this.slickSectionPanel1.Text = "What you\'re watching";
			// 
			// P_AllContent
			// 
			this.P_AllContent.AutoSize = true;
			this.P_AllContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_AllContent.Controls.Add(this.P_Info);
			this.P_AllContent.Controls.Add(this.P_BotSpacer);
			this.P_AllContent.Controls.Add(this.P_VLC);
			this.P_AllContent.Location = new System.Drawing.Point(0, 0);
			this.P_AllContent.MaximumSize = new System.Drawing.Size(696, 2147483647);
			this.P_AllContent.MinimumSize = new System.Drawing.Size(696, 0);
			this.P_AllContent.Name = "P_AllContent";
			this.P_AllContent.Size = new System.Drawing.Size(696, 817);
			this.P_AllContent.TabIndex = 0;
			// 
			// P_BackContent
			// 
			this.P_BackContent.Controls.Add(this.slickScroll);
			this.P_BackContent.Controls.Add(this.TLP_MoreInfo2);
			this.P_BackContent.Controls.Add(this.P_AllContent);
			this.P_BackContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_BackContent.Location = new System.Drawing.Point(0, 30);
			this.P_BackContent.Name = "P_BackContent";
			this.P_BackContent.Size = new System.Drawing.Size(696, 965);
			this.P_BackContent.TabIndex = 0;
			// 
			// slickScroll
			// 
			this.slickScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll.LinkedControl = this.P_AllContent;
			this.slickScroll.Location = new System.Drawing.Point(686, 23);
			this.slickScroll.Name = "slickScroll";
			this.slickScroll.ShowHandle = false;
			this.slickScroll.Size = new System.Drawing.Size(10, 942);
			this.slickScroll.SmallHandle = true;
			this.slickScroll.Style = SlickControls.StyleType.Vertical;
			this.slickScroll.TabIndex = 20;
			this.slickScroll.TabStop = false;
			this.slickScroll.TakeUpSpaceFromPanel = false;
			this.slickScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SlickScroll_Scroll);
			// 
			// TLP_MoreInfo2
			// 
			this.TLP_MoreInfo2.ColumnCount = 4;
			this.TLP_MoreInfo2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_MoreInfo2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MoreInfo2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MoreInfo2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_MoreInfo2.Controls.Add(this.L_MoreInfo2, 2, 0);
			this.TLP_MoreInfo2.Controls.Add(this.I_MoreInfo2, 1, 0);
			this.TLP_MoreInfo2.Controls.Add(this.P_Spacer2, 0, 1);
			this.TLP_MoreInfo2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.TLP_MoreInfo2.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_MoreInfo2.Location = new System.Drawing.Point(0, 0);
			this.TLP_MoreInfo2.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_MoreInfo2.Name = "TLP_MoreInfo2";
			this.TLP_MoreInfo2.RowCount = 2;
			this.TLP_MoreInfo2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_MoreInfo2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP_MoreInfo2.Size = new System.Drawing.Size(696, 23);
			this.TLP_MoreInfo2.TabIndex = 21;
			this.TLP_MoreInfo2.Tag = "NoMouseDown";
			this.TLP_MoreInfo2.Click += new System.EventHandler(this.TLP_MoreInfo_Click);
			this.TLP_MoreInfo2.MouseEnter += new System.EventHandler(this.TLP_MoreInfo2_MouseEnter);
			this.TLP_MoreInfo2.MouseLeave += new System.EventHandler(this.TLP_MoreInfo2_MouseLeave);
			// 
			// L_MoreInfo2
			// 
			this.L_MoreInfo2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_MoreInfo2.AutoSize = true;
			this.L_MoreInfo2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_MoreInfo2.Location = new System.Drawing.Point(303, 0);
			this.L_MoreInfo2.Name = "L_MoreInfo2";
			this.L_MoreInfo2.Size = new System.Drawing.Size(105, 22);
			this.L_MoreInfo2.TabIndex = 0;
			this.L_MoreInfo2.Tag = "NoMouseDown";
			this.L_MoreInfo2.Text = "More Info";
			this.L_MoreInfo2.Click += new System.EventHandler(this.TLP_MoreInfo_Click);
			this.L_MoreInfo2.MouseEnter += new System.EventHandler(this.TLP_MoreInfo2_MouseEnter);
			this.L_MoreInfo2.MouseLeave += new System.EventHandler(this.TLP_MoreInfo2_MouseLeave);
			// 
			// I_MoreInfo2
			// 
			this.I_MoreInfo2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.I_MoreInfo2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_MoreInfo2.Image = global::ShowsCalendar.Properties.Resources.ArrowDown;
			this.I_MoreInfo2.Location = new System.Drawing.Point(284, 3);
			this.I_MoreInfo2.Margin = new System.Windows.Forms.Padding(0);
			this.I_MoreInfo2.Name = "I_MoreInfo2";
			this.I_MoreInfo2.Size = new System.Drawing.Size(16, 16);
			this.I_MoreInfo2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.I_MoreInfo2.TabIndex = 1;
			this.I_MoreInfo2.TabStop = false;
			this.I_MoreInfo2.Click += new System.EventHandler(this.TLP_MoreInfo_Click);
			this.I_MoreInfo2.MouseEnter += new System.EventHandler(this.TLP_MoreInfo2_MouseEnter);
			this.I_MoreInfo2.MouseLeave += new System.EventHandler(this.TLP_MoreInfo2_MouseLeave);
			// 
			// P_Spacer2
			// 
			this.TLP_MoreInfo2.SetColumnSpan(this.P_Spacer2, 4);
			this.P_Spacer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Spacer2.Location = new System.Drawing.Point(10, 22);
			this.P_Spacer2.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.P_Spacer2.Name = "P_Spacer2";
			this.P_Spacer2.Size = new System.Drawing.Size(676, 1);
			this.P_Spacer2.TabIndex = 2;
			// 
			// PC_Player
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.P_BackContent);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.LabelBounds = new System.Drawing.Point(3, 1);
			this.Name = "PC_Player";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(696, 995);
			this.Shown += new System.EventHandler(this.PC_Player_Shown);
			this.Load += new System.EventHandler(this.PC_Player_Load);
			this.Resize += new System.EventHandler(this.PC_Player_Resize);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.P_BackContent, 0);
			this.TLP_Controls.ResumeLayout(false);
			this.TLP_Controls.PerformLayout();
			this.TLP_Buttons.ResumeLayout(false);
			this.TLP_Buttons.PerformLayout();
			this.P_BotSpacer.ResumeLayout(false);
			this.P_BotSpacer.PerformLayout();
			this.TLP_MoreInfo.ResumeLayout(false);
			this.TLP_MoreInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.I_MoreInfo)).EndInit();
			this.P_VLC.ResumeLayout(false);
			this.P_SubDelay.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PB_UpNext)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Volume)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Thumb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Close)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.L_TopNotch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Thumbnail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UpNextCountdown)).EndInit();
			this.P_Info.ResumeLayout(false);
			this.P_Info.PerformLayout();
			this.TLP_Suggestions.ResumeLayout(false);
			this.TLP_Suggestions.PerformLayout();
			this.TLP_Info.ResumeLayout(false);
			this.TLP_Info.PerformLayout();
			this.P_AllContent.ResumeLayout(false);
			this.P_AllContent.PerformLayout();
			this.P_BackContent.ResumeLayout(false);
			this.P_BackContent.PerformLayout();
			this.TLP_MoreInfo2.ResumeLayout(false);
			this.TLP_MoreInfo2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.I_MoreInfo2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_Controls;
		private System.Windows.Forms.Label L_CurrentTime;
		private System.Windows.Forms.TableLayoutPanel TLP_Buttons;
		public SlickControls.SlickLabel SL_Play;
		public SlickControls.SlickLabel SL_Forward;
		public SlickControls.SlickLabel SL_Next;
		public SlickControls.SlickLabel SL_Backwards;
		public SlickControls.SlickLabel SL_Previous;
		public SlickControls.SlickLabel SL_Subs;
		private SlickControls.SlickSlider SS_Volume;
		public SlickControls.SlickLabel SL_Audio;
		public SlickControls.SlickLabel SL_FullScreen;
		public SlickControls.SlickLabel SL_MiniPlayer;
		public SlickControls.SlickLabel SL_More;
		private SlickControls.SlickSlider SS_TimeSlider;
		private System.Windows.Forms.Label L_Time;
		private System.Windows.Forms.Panel P_BotSpacer;
		private System.Windows.Forms.Panel P_VLC;
		private System.Windows.Forms.Panel P_Info;
		private System.Windows.Forms.Panel P_AllContent;
		internal System.Windows.Forms.Panel P_BackContent;
		private SlickControls.SlickScroll slickScroll;
		internal System.Windows.Forms.TableLayoutPanel TLP_Suggestions;
		private SlickControls.SlickSectionPanel SP_Cast;
		private SlickControls.SlickSectionPanel slickSectionPanel1;
		internal System.Windows.Forms.TableLayoutPanel TLP_MoreInfo;
		private System.Windows.Forms.Label L_MoreInfo;
		private System.Windows.Forms.PictureBox I_MoreInfo;
		private SlickControls.SlickSectionPanel SP_Similar;
		private System.Windows.Forms.TableLayoutPanel TLP_SimilarContent;
		private SlickControls.SlickSectionPanel SP_Crew;
		private System.Windows.Forms.TableLayoutPanel TLP_MoreInfo2;
		private System.Windows.Forms.Label L_MoreInfo2;
		private System.Windows.Forms.PictureBox I_MoreInfo2;
		private System.Windows.Forms.Panel P_Spacer2;
		private SlickControls.SlickPictureBox L_TopNotch;
		private System.Windows.Forms.Label L_PlotLabel;
		private System.Windows.Forms.Label L_ShowPlotLabel;
		private System.Windows.Forms.Label L_ShowPlot;
		private System.Windows.Forms.Label L_Plot;
		private System.Windows.Forms.Panel P_Spacer;
		private System.Windows.Forms.PictureBox PB_Close;
		private SlickControls.SlickPictureBox PB_Loader;
		private SlickControls.SlickPictureBox PB_Thumb;
		private SlickControls.DBPictureBox PB_Volume;
		private SlickControls.SlickPictureBox PB_UpNext;
		private SlickControls.SlickPictureBox PB_Thumbnail;
		private SlickControls.SlickPictureBox UpNextCountdown;
		private System.Windows.Forms.TableLayoutPanel TLP_Info;
		private System.Windows.Forms.TableLayoutPanel P_SubDelay;
		public SlickControls.SlickIcon SL_SubDelayMinus;
		public SlickControls.SlickIcon SL_SubDelayPlus;
		private System.Windows.Forms.Label L_SubDelay;
		private SlickControls.DBPanel P_Progress;
		private SlickControls.SlickControl C_Rate;
		public SlickControls.SlickLabel SL_PnP;
	}
}
