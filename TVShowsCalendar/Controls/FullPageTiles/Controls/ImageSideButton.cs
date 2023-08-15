using Extensions;

using SlickControls;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public class ImageSideButton : SlickImageBackgroundControl
	{
		public bool Selected { get; set; }
		public int PageId { get; set; } = -1;

		public ImageSideButton()
		{
			Size = new Size(80, 80);
			Padding = new Padding(8);
		}

		public override void OnPaint(PaintEventArgs e)
		{
			var rect = DrawBounds;
			var backColor = HoverState.HasFlag(HoverState.Pressed) && !Selected ? FormDesign.Design.ActiveColor : (Container as FullPageTile).DrawInfo ? Color.FromArgb(HoverState.HasFlag(HoverState.Hovered) || Selected ? 150 : 85, FormDesign.Design.BackColor) : FormDesign.Design.AccentBackColor;

			e.Graphics.FillRoundedRectangle(
				SlickControl.Gradient(rect, backColor),
				rect,
				10);

			var color = FormDesign.Design.IconColor.MergeColor(backColor, 85);

			if (Selected)
				color = FormDesign.Design.ActiveColor;
			else if (HoverState.HasFlag(HoverState.Pressed))
				color = FormDesign.Design.ActiveForeColor;
			else if (HoverState.HasFlag(HoverState.Hovered))
				color = FormDesign.Design.ActiveColor;

			e.Graphics.DrawImage(Image.Color(color), rect, ImageSizeMode.Center);

			if (HoverState.HasFlag(HoverState.Hovered) && !Selected && !HoverState.HasFlag(HoverState.Pressed))
				e.Graphics.DrawRoundedRectangle(new Pen(Color.FromArgb(125, FormDesign.Design.ActiveColor), 2F) { DashStyle = DashStyle.Dash }, rect, 10);
		}

		public override void CalculateSize(PaintEventArgs e)
		{
			if (PageId >= 0)
				Visible = Bounds.Y - 80 > (Parent.Controls.Where(x => x.Dock == DockStyle.Top).LastOrDefault()?.Bounds.Y ?? 0);
		}
	}
}