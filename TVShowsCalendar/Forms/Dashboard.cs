using Extensions;

using ShowsRenamer.Module.Classes;
using ShowsRenamer.Module.Handlers;

using SlickControls;

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class Dashboard : BasePanelForm
	{
		private bool startBoundsSet;
		private Image iconBack;
		private Image iconShadow;
		private Image iconPlay;

		#region Public Constructors

		public Dashboard(string[] args)
		{
			InitializeComponent();

			AllowDrop = true;

			DragEnter += Dashboard_DragEnter;
			DragDrop += Dashboard_DragDrop;

			SetPanel<PC_Dashboard>(PI_Dashboard);
			L_Version.Text = "v " + ProductVersion;

			FormDesign.Initialize(this);

			if (args.TryGet(0) == "/startup")
				if (Visible = Data.Options.StartupMode)
					WindowState = FormWindowState.Minimized;

			if (WindowState != FormWindowState.Minimized)
				WindowState = Data.Preferences.DashMax ? FormWindowState.Maximized : FormWindowState.Normal;

			AutoCleanupTimer.Elapsed += AutoCleanupTimer_Elapsed;
			if (Data.Options.AutoCleaner)
				AutoCleanupTimer.Start();

			if (args.Length > 0 && File.Exists(args[0]))
				Play(args[0]);

			base_PB_Icon.Paint += Base_PB_Icon_Paint;

			if (args.TryGet(0) != "/startup")
				OnNextIdle(() =>
				{
					Data.PreloaderForm?.Close();
					this.ShowUp();
				});

			if (IO.Handler.FirstLoadFinished)
			{
				ShowManager.RunReminder();
				MovieManager.RunReminder();
			}

			ConnectionHandler.ConnectionChanged += (s) => this.TryInvoke(base_PB_Icon.Invalidate);
		}

		private void Base_PB_Icon_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(FormDesign.Design.MenuColor);
			e.Graphics.DrawImage(iconBack, new Rectangle(Point.Empty, base_PB_Icon.Size));
			e.Graphics.DrawImage(iconShadow, new Rectangle(Point.Empty, base_PB_Icon.Size));
			e.Graphics.DrawImage(Properties.Resources.Icon_64_Play.Color(ConnectionHandler.IsConnected ? FormDesign.Design.ActiveColor : FormDesign.Design.RedColor), new Rectangle(Point.Empty, base_PB_Icon.Size));
		}

		protected override void DesignChanged(FormDesign design)
		{
			iconBack = Properties.Resources.Icon_64_Back.Color(FormDesign.Design.MenuForeColor);
			iconShadow = Properties.Resources.Icon_64_Shadow.Color(FormDesign.Design.MenuColor);
			iconPlay = Properties.Resources.Icon_64_Play.Color(FormDesign.Design.ActiveColor);

			base.DesignChanged(design);
		}

		public void Play(string filename)
		{
			Cursor.Current = Cursors.WaitCursor;
			var file = new FileInfo(filename);
			if (file.Extension.ToLower().AnyOf(SlickControls.IO.VideoExtensions))
			{
				if (Data.ActivePlayer == null)
				{
					PushPanel(null, new PC_Player(file, true));
				}
				else
				{
					Data.ActivePlayer.SaveWatchtime();
					Data.ActivePlayer.ClearMedia();
					Data.ActivePlayer.SetFile(file, true);
				}
			}
			Cursor.Current = Cursors.Default;
		}

		private void Dashboard_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("FileName") && SlickControls.IO.VideoExtensions.Any(Path.GetExtension((e.Data.GetData("FileName") as string[]).FirstOrDefault()).ToLower()))
				Play((e.Data.GetData("FileName") as string[]).FirstOrDefault());
		}

		private void Dashboard_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("FileName") && SlickControls.IO.VideoExtensions.Any(Path.GetExtension((e.Data.GetData("FileName") as string[]).FirstOrDefault()).ToLower()))
				e.Effect = DragDropEffects.Move;
			else
				e.Effect = DragDropEffects.None;
		}

		#endregion Public Constructors

		#region Internal Methods

		internal void Setup(int v)
		{
			switch (v)
			{
				case 1:
					SetPanel<PC_LibraryFolders>(PI_LibraryFolders);

					Notification.Clear();
					Notification.Create("First Time Setup • Library Folders", "This is your library folders screen, add in the folders that contain your Series and Movies so the app knows where to look.", PromptIcons.Info, null, NotificationSound.None, new Size(350, 70))
						.Show(this, 20);
					break;

				case 2:
					SetPanel<PC_Shows>(PI_Shows);

					Notification.Clear();
					Notification.Create("First Time Setup • Shows", "Finally, this is your Shows screen, add or discover TV Shows to your library to finish.\n\n" +
						"You can always go to the About section for more help and other tips about the App.", PromptIcons.Info, null, NotificationSound.None, new Size(350, 115))
						.Show(this, 30);
					EnableSideBar();
					Data.FirstTimeSetup = false;
					break;

				default:
					break;
			}
		}

		#endregion Internal Methods

		#region Protected Methods

		protected override void UIChanged()
		{
			base.UIChanged();

			L_Version.Font = L_Text.Font = UI.Font(6.75F);

			if (!startBoundsSet)
			{
				if (Data.FirstTimeSetup)
				{
					Data.Preferences.DashBounds = Bounds;
					Data.Preferences.Save();
				}
				else
					Bounds = Data.Preferences.DashBounds;
			}

			startBoundsSet = true;
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (!Data.FirstTimeSetup && e.CloseReason == CloseReason.UserClosing && e.CloseReason != CloseReason.ApplicationExitCall)
				e.Cancel = MessagePrompt.Show("Are you sure you want to close Shows Calendar?", "Confirm Action", PromptButtons.YesNo, PromptIcons.Question, this) == DialogResult.No;

			if (!e.Cancel)
				notifyIcon.Visible = false;

			base.OnFormClosing(e);
		}

		#endregion Protected Methods

		#region Private Methods

		private void Dashboard_Load(object sender, EventArgs e)
		{
			if (Data.FirstTimeSetup)
			{
				DisableSideBar();

				SetPanel<PC_Settings>(PI_Settings);

				new BackgroundAction(() =>
				{
					Notification.Create("First Time Setup", "Welcome to Shows Calendar!\nLet's set you up nice and breezy.", PromptIcons.Info, null, NotificationSound.None, new Size(350, 70))
						.Show(this, 10);

					Notification.Create("First Time Setup • Settings", "This is your settings screen, go through them to cherry-pick your experience. You can come here anytime.", PromptIcons.Info, null, NotificationSound.None, new Size(350, 70))
						.Show(this, 20);
				}).RunIn(100);
			}

			Application.DoEvents();
		}

		private void L_Version_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			PushPanel(null, new PC_Changelog(Assembly.GetExecutingAssembly(), "ShowsCalendar.ChangeLog.json", new Version(ProductVersion)));
			Cursor.Current = Cursors.Default;
		}

		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				SlickToolStrip.Show(
					new SlickStripItem("Open", this.ShowUp, image: Properties.Resources.Icon_16),
					SlickStripItem.Empty,
					new SlickStripItem("TV Shows", () => { SetPanel<PC_Shows>(PI_Shows); this.ShowUp(); }, image: Properties.Resources.Tiny_TV),
					new SlickStripItem("Movies", () => { SetPanel<PC_Movies>(PI_Movies); this.ShowUp(); }, image: Properties.Resources.Tiny_Movie),
					SlickStripItem.Empty,
					new SlickStripItem("Watch", () => { SetPanel<PC_Watch>(PI_Watch); this.ShowUp(); }, image: Properties.Resources.Tiny_Play),
					new SlickStripItem("Library", () => { SetPanel<PC_Library>(PI_Library); this.ShowUp(); }, image: Properties.Resources.Tiny_Library),
					SlickStripItem.Empty,
					new SlickStripItem("Settings", () => { SetPanel<PC_Settings>(PI_Settings); this.ShowUp(); }, image: Properties.Resources.Tiny_Settings),
					SlickStripItem.Empty,
					new SlickStripItem("Exit App", Close, image: Properties.Resources.Tiny_Cancel)
				);
			}
			else
			{
				this.ShowUp();
			}
		}

		private void PI_About_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_About>((PanelItem)sender);

		private void PI_Dashboard_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Dashboard>((PanelItem)sender);

		private void PI_Library_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Library>((PanelItem)sender);

		private void PI_LibraryFolders_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_LibraryFolders>((PanelItem)sender);

		private void PI_Movies_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Movies>((PanelItem)sender);

		private void PI_People_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_People>((PanelItem)sender);

		private void PI_Settings_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Settings>((PanelItem)sender);

		private void PI_Shows_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Shows>((PanelItem)sender);

		private void PI_Watch_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Watch>((PanelItem)sender);

		private void PI_LibraryRename_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_LibraryRenamer>((PanelItem)sender);

		#endregion Private Methods

		private void L_Text_Click(object sender, EventArgs e) => PI_About_OnClick(PI_About, null);

		private readonly System.Timers.Timer AutoCleanupTimer = new System.Timers.Timer(7200000);

		private void Dashboard_Activated(object sender, EventArgs e)
		{
			if (Data.Options.AutoCleaner)
				AutoCleanupTimer.Stop();
		}

		private void Dashboard_Deactivate(object sender, EventArgs e)
		{
			if (Data.Options.AutoCleaner)
				AutoCleanupTimer.Start();
		}

		private void AutoCleanupTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (Data.Options.AutoCleaner && GetInactiveTime().TotalHours >= 1.5)
			{
				Data.RenameHandler = new RenameHandler(new CleaningSessionInfo()
				{
					Paths = IO.Handler.GeneralFolders.Convert(x => x.FullName)
				});

				new BackgroundAction(() => { try { PC_LibraryRenamer.RunCleaner(); } catch { } }).Run();
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct LASTINPUTINFO
		{
			public uint cbSize;
			public uint dwTime;
		}

		[DllImport("user32.dll")]
		private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool DestroyIcon(IntPtr handle);

		public TimeSpan GetInactiveTime()
		{
			if (Data.Options.AutoCleaner)
			{
				var info = new LASTINPUTINFO();
				info.cbSize = (uint)Marshal.SizeOf(info);
				if (GetLastInputInfo(ref info))
					return TimeSpan.FromMilliseconds(Environment.TickCount - info.dwTime);
			}

			return TimeSpan.Zero;
		}

		private void Dashboard_ResizeEnd(object sender, EventArgs e)
		{
			if (!TopMost)
			{
				if (!(Data.Preferences.DashMax = WindowState == FormWindowState.Maximized))
					Data.Preferences.DashBounds = Bounds;
				Data.Preferences.Save();
			}
		}
	}
}