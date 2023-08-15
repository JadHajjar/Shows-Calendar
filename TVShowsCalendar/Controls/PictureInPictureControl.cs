using Extensions;

using SlickControls;

using System.Drawing;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PictureInPictureControl : SlickControl
	{
		private Point startPost;
		private Point mouseStart;

		private AnimationHandler animation;

		public bool IsMoving { get; private set; }

		public PC_Player Player { get; }

		public PictureInPictureControl(PC_Player player)
		{
			InitializeComponent();
			Player = player;

			Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

			player.Form.Controls.Add(this);

			Resize += (s, e) => ReleaseMove();
			player.Form.OnWndProc += Form_OnWndProc;
			player.Form.HandleKeyPress += Form_HandleKeyPress;
		}

		public void StartMouseMove()
		{
			startPost = Location;
			mouseStart = Cursor.Position;
			IsMoving = true;
			animation?.Dispose();
		}

		public void MouseMoved()
		{
			var delta = new Point(Cursor.Position.X - mouseStart.X, Cursor.Position.Y - mouseStart.Y);
			var pad = new Padding(10);

			if (Player.Form.CurrentPanel.Controls.Any(x => x is FullPageTile))
				pad.Right += 170;

			Location = new Point((startPost.X + Cursor.Position.X - mouseStart.X).Between(pad.Left + Player.Form.Padding.Left, Player.Form.Width - Player.Form.Padding.Right - Width - pad.Right),
				(startPost.Y + Cursor.Position.Y - mouseStart.Y).Between(pad.Top + Player.Form.Padding.Top, Player.Form.Height - Player.Form.Padding.Bottom - Height - pad.Bottom));

			Anchor = AnchorStyles.None;

			if (Left + (Width / 2) < Player.Form.Width / 3)
				Anchor |= AnchorStyles.Left;
			else if (Left + (Width / 2) > Player.Form.Width * 2 / 3)
				Anchor |= AnchorStyles.Right;
			else
				Anchor |= delta.X > 0 ? AnchorStyles.Right : AnchorStyles.Left;

			if (Top + (Height / 2) < Player.Form.Height / 3)
				Anchor |= AnchorStyles.Top;
			else if (Top + (Height / 2) > Player.Form.Height * 2 / 3)
				Anchor |= AnchorStyles.Bottom;
			else
				Anchor |= delta.Y > 0 ? AnchorStyles.Bottom : AnchorStyles.Top;

			Anchor &= ~AnchorStyles.None;
		}

		public void ReleaseMove()
		{
			IsMoving = false;

			var pad = new Padding(10);

			if (Anchor.HasFlag(AnchorStyles.Top))
				pad.Left += Player.Form.Controls["base_P_Container"].Controls["base_P_Content"].Controls["base_P_Side"].Width;

			if (Player.Form.CurrentPanel.Controls.Any(x => x is FullPageTile))
			{
				pad.Right += 170;
				pad.Top += UI.Font(20F, FontStyle.Bold).Height + 40;
				if (Anchor.HasFlag(AnchorStyles.Top))
					pad.Left += (int)(200 * UI.FontScale) + 20;
			}
			else
				pad.Top += Player.Form.Controls["base_P_Container"].Controls["base_P_Content"].Controls["base_TLP_TopButtons"].Height;

			var point = new Point(
				Anchor.HasFlag(AnchorStyles.Left) ? pad.Left + Player.Form.Padding.Left : Player.Form.Width - Player.Form.Padding.Right - Width - pad.Right,
				Anchor.HasFlag(AnchorStyles.Top) ? pad.Top + Player.Form.Padding.Top : Player.Form.Height - Player.Form.Padding.Bottom - Height - pad.Bottom);

			animation = new AnimationHandler(this, point)
			{
				Speed = 2
			};
			animation.StartAnimation();
		}

		private void OnDisposing()
		{
			//Controls.Clear(true);
			Player.Form.OnWndProc -= Form_OnWndProc;
			Player.Form.HandleKeyPress -= Form_HandleKeyPress;
		}

		protected override void DesignChanged(FormDesign design) => BackColor = design.AccentColor;

		private bool Form_HandleKeyPress(Message arg1, Keys arg2) => Player.KeyPressed(ref arg1, arg2);

		private bool Form_OnWndProc(Message arg) => Player.OnWndProc(arg);
	}
}