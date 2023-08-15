using Extensions;

using SlickControls;

using System.Drawing;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public class ImageLabelText : SlickImageBackgroundControl
	{
		public string Title { get; set; }

		public ImageLabelText(string title = null)
		{
			Dock = DockStyle.Top;
			Title = title;
			Margin = new Padding(0, 10, 0, 0);
		}

		public override void OnPaint(PaintEventArgs e)
		{
			var bnds = DrawBounds;

			e.Graphics.DrawString(Title, UI.Font(9.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), bnds);

			bnds = bnds.Pad(7, (int)e.Graphics.Measure(Title, UI.Font(9.75F, FontStyle.Bold), Width).Height + 2, 0, 0);

			e.Graphics.DrawString(Text, UI.Font(8.25F), new SolidBrush(FormDesign.Design.ForeColor.MergeColor(FormDesign.Design.BackColor, 80)), bnds);
		}

		public override void CalculateSize(PaintEventArgs e)
		{
			if (Visible = !string.IsNullOrWhiteSpace(Text))
			{
				Height = 10 + (int)e.Graphics.Measure(Title, UI.Font(9.75F, FontStyle.Bold), Width).Height
					+ (Text != $"{(char)0x200B}" ? (int)e.Graphics.Measure(Text, UI.Font(8.25F), Width - 7).Height : 0);
			}
		}
	}
}