using Extensions;

using SlickControls;

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class FullPageTile : SlickImageBackgroundContainer
	{
		private bool backgroundLoaded;
		public string PageName { get; set; }
		public string Title { get; set; }
		public string SubTitle { get; set; }
		public Panel ContentPanel { get; } = new Panel { Dock = DockStyle.Fill };
		protected SlickSpacer Spacer { get; } = new SlickSpacer { Visible = false };

		protected SlickScroll ScrollBar { get; } = new SlickScroll
		{
			Dock = DockStyle.None,
			SmallHandle = true,
			Visible = false
		};

		public virtual bool DrawInfo { get; }

		protected override bool ImageHovered => HoverState.HasFlag(HoverState.Hovered) && ImageBounds.Contains(PointToClient(Cursor.Position));

		public FullPageTile()
		{
			InitializeComponent();

			Padding = new Padding((int)(200 * UI.FontScale) + 30, SubInfo.Bounds.Y + SubInfo.Bounds.Height, 90, 0);

			Spacer.Parent = this;
			ScrollBar.Parent = this;
			ScrollBar.Location = new Point(Width - ScrollBar.Width, 100);
			ContentPanel.Paint += contentPanel_Paint;
			ScrollBar.Scroll += (s, e) => Spacer.Visible = e.NewValue > 0;
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			ScrollBar.Bounds = new Rectangle(Width - ScrollBar.BAR_SIZE_MAX, (int)(16 * UI.UIScale) + 12, ScrollBar.Width, Height);
		}

		protected override void UIChanged()
		{
			ScrollBar.Padding = new Padding(0, 0, 0, (int)(16 * UI.UIScale) + 12);
			_SidePanel.Width = (int)(200 * UI.FontScale);
			PosterImage.Height = (int)(300 * UI.FontScale);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (HoveredControl == null && e.Y < 50)
				Data.Mainform.ForceWindowMove(e);
			else
				base.OnMouseDown(e);
		}

		public void SetContent(Control ctrl)
		{
			if (ctrl == null)
			{
				ContentPanel.Controls.Clear();
				setContent(true);
				return;
			}

			ContentPanel.Controls.Clear();
			ContentPanel.Invalidate();
			Invalidate();

			Application.DoEvents();

			if (ctrl != null)
			{
				ctrl.Location = Point.Empty;
				ctrl.MaximumSize = new Size(Width - Padding.Horizontal, int.MaxValue);
				ctrl.MinimumSize = new Size(Width - Padding.Horizontal, 0);
				ctrl.Parent = ContentPanel;
				ctrl.ControlAdded += ctrl_ControlAdded;

				ScrollBar.Bounds = new Rectangle(Width - ScrollBar.BAR_SIZE_MAX, (int)(16 * UI.UIScale) + 12, ScrollBar.Width, Height);
				ScrollBar.LinkedControl = ctrl;
				ScrollBar.Reset();

				ctrl.ResetFocus();
			}

			setContent(false);
			ContentPanel.Invalidate();
		}

		private void setContent(bool value)
		{
			ContentPanel.Bounds = new Rectangle(Padding.Left, Padding.Top, Width - Padding.Horizontal, Height - Padding.Vertical);
			ContentPanel.Parent = value ? null : this;

			if (!value)
			{
				if (IsHandleCreated)
					BeginInvoke(new Action(ContentPanel.ResetFocus));
				else
					ContentPanel.ResetFocus();
			}
			else
			{
				Spacer.Visible = ScrollBar.Visible = false;
				Focus();
			}

			Invalidate();
		}

		private void ctrl_ControlAdded(object sender, ControlEventArgs e) => ContentPanel.Invalidate();

		private void contentPanel_Paint(object sender, PaintEventArgs e)
		{
			if (ContentPanel.Controls.Count == 0)
				e.Graphics.DrawString("Loading..", UI.Font(9F), new SolidBrush(FormDesign.Design.InfoColor), new Rectangle(Point.Empty, ContentPanel.Size), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
			else if (ScrollBar?.LinkedControl?.Controls?.Cast<Control>()?.All(x => !x.Visible) ?? true)
				e.Graphics.DrawString("Nothing to see here..", UI.Font(9F), new SolidBrush(FormDesign.Design.InfoColor), new Rectangle(Point.Empty, ContentPanel.Size), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (!DrawInfo)
				ScrollBar?.TriggerMouseWheel(e);
			else
				base.OnMouseWheel(e);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				blurredImage?.Dispose();
				ContentPanel.Dispose();
				ScrollBar.Dispose();
			}

			base.Dispose(disposing);
		}

		protected void LoadBackground(string link)
		{
			if (!string.IsNullOrWhiteSpace(link) && !backgroundLoaded)
			{
				backgroundLoaded = true;

				if (ImageHandler.File(link, 0, blur: 4).Exists)
					Image = ImageHandler.GetImage(link, 0, false, blur: 4, desiredSize: Screen.PrimaryScreen.Bounds.Size);
				else
				new BackgroundAction(() => Image = ImageHandler.GetImage(link, 0, false, blur: 4, desiredSize: Screen.PrimaryScreen.Bounds.Size)).Run();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var mainBounds = _MainPanel.Bounds;

			if (!DrawInfo)
			{
				e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.BackColor), new Rectangle(mainBounds.X, 0, Width, Height));
				e.Graphics.DrawLine(new Pen(Color.FromArgb(100, FormDesign.Design.ActiveColor), 1.5F), mainBounds.X - 1, 0, mainBounds.X - 1, Height);
			}

			base.OnPaint(e);

			e.Graphics.ResetTransform();

			Padding = new Padding(mainBounds.X, SubInfo.Bounds.Y + SubInfo.Bounds.Height, 90, 0);

			if (HoveredControl != null && HoveredControl != PosterImage && HoveredControl.Cursor == Cursors.Hand)
			{
				e.Graphics.SetClip(new Rectangle(HoveredControl.ContainerLocation(), HoveredControl.Bounds.Size));

				var w = Math.Min(120, Math.Min(HoveredControl.Width, HoveredControl.Height) * 2);
				var path = new GraphicsPath();
				path.AddEllipse(CursorLocation.X - w / 2, CursorLocation.Y - w / 2, w, w);

				var pthGrBrush = new PathGradientBrush(path)
				{
					CenterColor = Color.FromArgb(25, FormDesign.Design.ActiveColor.MergeColor(Color.White)),
					SurroundColors = new[] { Color.Empty }
				};

				e.Graphics.FillEllipse(pthGrBrush, CursorLocation.X - w / 2, CursorLocation.Y - w / 2, w, w);
			}
		}

		protected virtual void TitleClicked(object sender, MouseEventArgs e)
		{
			throw new NotImplementedException();
		}

		public virtual void RemoveTag(string tag)
		{
			throw new NotImplementedException();
		}
	}
}