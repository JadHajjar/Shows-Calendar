using SlickControls;

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public class AutoSizeLabel : Control
	{
		private string text;

		[Browsable(true)]
		[Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Bindable(true)]
		public override string Text { get => text; set { text = value; Invalidate(); } }

		public AutoSizeLabel()
		{
			ResizeRedraw = true;
			DoubleBuffered = true;
			TabStop = false;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var h = (int)FontMeasuring.Measure(Text, Font, Width).Height;

			if (h != Height)
				Height = h;
			else
			{
				e.Graphics.Clear(BackColor);
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height));
			}
		}
	}
}