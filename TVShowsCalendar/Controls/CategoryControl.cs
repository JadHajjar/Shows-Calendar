using Extensions;

using SlickControls;

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public class CategoryControl : SlickAdvancedImageControl, IAnimatable
	{
		private Timer timer;
		private Bitmap[] posters;

		public string Category { get; }
		public int AnimatedValue { get; set; }
		public int TargetAnimationValue { get; } = int.MaxValue;
		public int Lines { get; }

		public CategoryControl(string category, int lines)
		{
			AutoInvalidate = false;
			Category = category.FormatWords(true);

			new BackgroundAction(() =>
			{
				var paths = MovieManager.Movies.Where(x => !x.Rating.Categories.Contains(category)).Select(x => x.PosterPath)
				  .Concat(ShowManager.Shows.Where(x => !x.Rating.Categories.Contains(category)).Select(x => x.PosterPath))
				  .WhereNotEmpty().Shuffle().Take(3 * lines).ToArray();

				posters = new Bitmap[3 * lines];
				for (var i = 0; i < paths.Length; i++)
					posters[i] = ImageHandler.GetImage(paths[i], 75, false);

				if (paths.Length > 0 && paths.Length < posters.Length)
					for (var i = paths.Length; i < posters.Length; i++)
						posters[i] = posters[(i - paths.Length) % paths.Length];
			}).Run();

			timer = new Timer { Interval = 30, Enabled = true };
			timer.Tick += (s, e) =>
			{
				AnimatedValue++;

				var pace = Width / Lines * 3 / 2 * posters.Length / Lines;

				if (AnimatedValue > pace)
					AnimatedValue = 0;

				Invalidate();
			};
			Lines = lines;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				timer?.Dispose();
				posters.Foreach(x => x?.Dispose());
				posters = null;
			}

			base.Dispose(disposing);
		}

		protected override void UIChanged()
		{
			Size = UI.Scale(new Size(Lines * 75, 150), UI.UIScale);
			Margin = UI.Scale(new Padding(7), UI.UIScale);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			if (posters != null)
			{
				var rect = new Rectangle(0, AnimatedValue, Width / Lines, Width / Lines * 3 / 2).Pad(5);

				for (var i = 0; i < Lines; i++)
				{
					for (var j = i * posters.Length / Lines; j < (i + 1) * posters.Length / Lines; j++)
					{
						if (e.Graphics.ClipBounds.IntersectsWith(rect))
							e.Graphics.DrawImage(posters[j], rect, ImageSizeMode.Fill);

						rect.Y += (rect.Height + 10) * (i % 2 == 0 ? -1 : 1);

						if (i % 2 == 0 ? rect.Y + rect.Height < 0 : rect.Y > Height)
						{
							rect.Y = i % 2 == 0 ? (AnimatedValue + 5) : (Height - AnimatedValue);
							rect.Y += (rect.Height + 10) * ((i + 1) * posters.Length / Lines - j - 1) * (i % 2 == 0 ? 1 : -1);
						}
					}

					if (i % 2 == 0)
						e.Graphics.DrawImage(posters[i * posters.Length / Lines], rect, ImageSizeMode.Fill);

					rect.X += rect.Width + 10;
					rect.Y = i % 2 == 0 ? Height - AnimatedValue : AnimatedValue;
				}
			}

			var tr = true;

			var topRect = new Rectangle(0, -1, Width, (int)(UI.Font(10.75F, FontStyle.Bold).Height * 1.2));
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(75, BackColor)), e.ClipRectangle);

			//e.Graphics.FillRectangle(new SolidBrush(BackColor), topRect.Pad(100,0,0,0));

			var gradiantRect = new Rectangle(20, -1, 80, 3);
			//e.Graphics.FillRectangle(new LinearGradientBrush(gradiantRect, Color.Empty, BackColor, LinearGradientMode.Horizontal), gradiantRect.Pad(1, 0, -1, 0));

			gradiantRect = new Rectangle(1, -1, Width, 100);
			e.Graphics.FillRectangle(new LinearGradientBrush(gradiantRect, BackColor, Color.Empty, LinearGradientMode.Vertical), gradiantRect.Pad(0, 0, 0, 1));

			e.Graphics.DrawString(Category, UI.Font(10.75F, FontStyle.Bold)
				, new SolidBrush(HoverState.HasFlag(HoverState.Hovered) ? FormDesign.Design.ActiveColor : ForeColor)
				, topRect.Pad(7, 0, 0, 0), new StringFormat { LineAlignment = StringAlignment.Center });

			e.Graphics.DrawRectangle(new Pen(HoverState.HasFlag(HoverState.Hovered) ? FormDesign.Design.ActiveColor :
				tr ? ForeColor : FormDesign.Design.AccentColor, 1F),
				e.ClipRectangle.Pad(0, 0, 1, 1));

			//e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(HoverState.HasFlag(HoverState.Hovered) ? 170 : 120, HoverState.HasFlag(HoverState.Hovered) ? FormDesign.Design.ActiveColor : BackColor)), e.Graphics.ClipBounds);

			//if (tr)
			//{
			//	e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			//	var p = new GraphicsPath(FillMode.Alternate);
			//	p.AddRectangle(e.ClipRectangle);
			//	p.AddPath(e.ClipRectangle.RoundedRect(20), true);
			//	e.Graphics.FillPath(new SolidBrush(BackColor), p);

			//}
			//else
			//{
			//	var gradiantRect = new Rectangle(0, 0, Width, 100);
			//	e.Graphics.FillRectangle(new LinearGradientBrush(gradiantRect, BackColor, Color.Empty, LinearGradientMode.Vertical), gradiantRect.Pad(0, 0, 0, 1));

			//	gradiantRect.Y = Height - gradiantRect.Height;
			//	e.Graphics.FillRectangle(new LinearGradientBrush(gradiantRect, Color.Empty, BackColor, LinearGradientMode.Vertical), gradiantRect.Pad(0, 1, 0, 0));
			//}
		}
	}
}