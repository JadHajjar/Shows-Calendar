using Extensions;

using SlickControls;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public class ImageSeasonControl : ImageContentControl<Season>
	{
		public ImageSeasonControl(Season season) : base(season)
		{
		}

		protected override void InfoChanged(object sender, EventArgs e)
		{
			if (imagePath != Content.PosterPath)
				LoadImage(() => ImageHandler.GetImage(imagePath = Content.PosterPath, 130, false));
		}

		public override void Install(Season content)
		{
			base.Install(content);

			DefaultImage = null;
		}

		public override void CalculateSize(PaintEventArgs e)
		{
			Size = new Size((int)(130 * UI.FontScale), 500);
			ImageBounds = new Rectangle(new Point(1, 1), new Size(Width - 2, (Width - 2) * 3 / 2));

			yIndex = Width - ImageBounds.Width < 20 ? ImageBounds.Top + ImageBounds.Height + 4 : 2;

			if(Content!= null)
			Height = yIndex
				+ MeasureText(e, Content.Name, UI.Font(8.25F), fill: true).Height
				+ MeasureText(e, Content.SubInfo, UI.Font(6.75F), fill: true).Height + 10;

			ActionRect = new Rectangle(Width - 40, Width - ImageBounds.Width < 20 ? ImageBounds.Top + ImageBounds.Height + 4 : 4, 16, 16);
		}

		public override void OnPaint(PaintEventArgs e)
		{
			if (Image == null && !Loading&& Content != null)
			{
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

				var sBnd = e.Graphics.Measure("Season", UI.Font(9.75F));
				var nBnd = e.Graphics.Measure(Content.SeasonNumber.ToString(), UI.Font(32));
				var numberPoint = new Point((int)((Width - nBnd.Width) * 2 / 3), Padding.Top + (int)((Height - nBnd.Height) / 2));

				using (var brush = new SolidBrush(FormDesign.Design.IconColor))
				{
					e.Graphics.DrawString(Content.SeasonNumber.ToString(), UI.Font(32), brush, numberPoint);
					e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
					e.Graphics.DrawString("Season", UI.Font(9.75F), brush, (int)((Width - sBnd.Width) / 3), numberPoint.Y - sBnd.Height / 2);
				}
			}

			base.OnPaint(e);
		}
	}
}