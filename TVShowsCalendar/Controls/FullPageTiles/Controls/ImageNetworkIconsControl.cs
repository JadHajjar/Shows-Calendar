using Extensions;

using SlickControls;

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public class ImageNetworkIconsControl : SlickImageBackgroundPanel
	{
		public ImageNetworkIconsControl()
		{
			Dock = DockStyle.Top;
			Margin = new Padding(0, 10, 0, 0);

			Controls.Add(new SlickImageBackgroundControl
			{
				Dock = DockStyle.Top,
				Height = 30
			});
		}

		public void Add(int id, string name, string logoPath)
		{
			var current = Controls.FirstOrDefault(x => x.Data == id);

			if (current == null)
			{
				current = new SlickImageBackgroundControl
				{
					Dock = DockStyle.Top,
					Text = name,
					Height = 40,
					Padding = new Padding(5),
					Data = id
				};

				current.Paint += network_Paint;
				current.CalculateAutoSize += network_CalculateSize;
				Controls.Add(current);
				Height += 40;
			}
			else
				current.Text = name;

			if (!string.IsNullOrWhiteSpace(logoPath) && current.Image == null && !current.Loading)
				current.LoadImage(() =>
				{
					var img = ImageHandler.GetImage(logoPath, (int)Math.Floor(30 / 1.15 / UI.FontScale), false, true);

					if (img != null)
					{
						if (img.GetAverageColor().If(x => x.GetBrightness() <= 0.1 && x.GetSaturation() < 0.15, true, false))
							img.SafeColor(FormDesign.Design.ActiveColor.MergeColor(FormDesign.Design.BackColor, 90));
					}

					return img;
				});
		}

		private void network_Paint(object sender, PaintEventArgs e)
		{
			var c = sender as SlickImageBackgroundControl;

			if (c.Image != null)
			{
				e.Graphics.DrawImage(c.Image.Width <= c.DrawBounds.Width
					? c.Image : new Bitmap(c.Image, c.DrawBounds.Width, c.Image.Height * c.DrawBounds.Width / c.Image.Width)
					, c.DrawBounds, ImageSizeMode.Center);
			}
			else if (c.Loading)
				e.Graphics.DrawLoader(c.LoaderPercentage, c.DrawBounds.CenterR(24, 24));
			else
				e.Graphics.DrawFancyText(c.Text, UI.Font(10.5F, FontStyle.Bold), c.DrawBounds, new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter });
		}

		private void network_CalculateSize(object sender, PaintEventArgs e)
		{
			var c = sender as SlickImageBackgroundControl;
			if (c.Image != null || c.Loading)
				c.Height = 40;
			else
				c.Height = (int)e.Graphics.Measure(c.Text, UI.Font(10.5F, FontStyle.Bold), c.Width).Height;
		}

		public override void OnPaint(PaintEventArgs e)
		{
			var bnds = DrawBounds;

			e.Graphics.DrawString(Text, UI.Font(9.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), bnds);

			base.OnPaint(e);
		}

		public override void CalculateSize(PaintEventArgs e)
		{
			if (Visible = Controls.Count > 1)
			{
				Controls[0].Height = (int)e.Graphics.Measure(Text, UI.Font(9.75F, FontStyle.Bold), Width).Height;
				Height = Controls.Sum(x => x.Height);
			}
		}
	}
}