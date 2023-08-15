using Extensions;

using SlickControls;

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class RateForm : SlickForm
	{
		public double Rating { get; private set; }

		private RateForm(double rating)
		{
			Rating = rating;

			InitializeComponent();
		}

		public static (bool, double) Show(double rating)
		{
			var @out = DialogResult.Cancel;
			var prompt = (RateForm)null;

			Data.Mainform.TryInvoke(() =>
			{
				prompt = new RateForm(rating);

				Data.Mainform.CurrentFormState = FormState.ForcedFocused;
				Data.Mainform.ShowUp();

				try
				{
					@out = prompt.ShowDialog(Data.Mainform);
				}
				finally
				{
					new BackgroundAction(() =>
					Data.Mainform.TryInvoke(() =>
					{
						Data.Mainform.ShowUp();
						Data.Mainform.CurrentFormState = FormState.NormalFocused;
					})).RunIn(50);
				}
			});

			return (@out == DialogResult.OK, prompt.Rating);
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			tableLayoutPanel1.BackColor = design.BackColor;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Escape:
					B_Cancel_Click(null, null);
					return true;

				case Keys.Enter:
					B_Done_Click(null, null);
					return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void slickControl1_Paint(object sender, PaintEventArgs e)
		{
			var size = (int)(65 * UI.FontScale) + 6;
			var rect = new Rectangle((slickControl1.Width - size * 5) / 2, 30 + (slickControl1.Height - size) / 2, size * 5, size);

			e.Graphics.Clear(FormDesign.Design.BackColor);
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

			e.Graphics.DrawString(Math.Floor(Math.Round(Rating, 1)).ToString("0"), UI.Font(24F, FontStyle.Bold), new SolidBrush(Rating.RatingColor()), new Rectangle(0, 0, Width / 2, 20 + (slickControl1.Height - size) / 2), new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far });
			e.Graphics.DrawString($".{Math.Round(Rating, 1) * 10 % 10:0}", UI.Font(9.75F, FontStyle.Bold), new SolidBrush(Color.FromArgb(175, Rating.RatingColor())), new Rectangle(Width / 2 - 10, 0, Width / 2, 9 + (slickControl1.Height - size) / 2), new StringFormat { LineAlignment = StringAlignment.Far });

			drawStars(e, false);

			e.Graphics.SetClip(new RectangleF(rect.X - 2, 0, (float)(rect.Width * Rating / 10), slickControl1.Height));

			drawStars(e, true);
		}

		private void drawStars(PaintEventArgs e, bool filled)
		{
			var size = (int)(65 * UI.FontScale);
			var rect = new Rectangle((slickControl1.Width - (size + 6) * 5) / 2, 30 + (slickControl1.Height - size) / 2, size, size);
			DrawStar(e, rect, filled);
			rect.X += rect.Width + 6;
			DrawStar(e, rect, filled);
			rect.X += rect.Width + 6;
			DrawStar(e, rect, filled);
			rect.X += rect.Width + 6;
			DrawStar(e, rect, filled);
			rect.X += rect.Width + 6;
			DrawStar(e, rect, filled);
		}

		private void DrawStar(PaintEventArgs e, Rectangle bounds, bool filled)
		{
			var path = StarPoints(bounds);

			var color = Rating.RatingColor();

			e.Graphics.DrawPath(new Pen(filled ? color : FormDesign.Design.AccentColor, 3F), path);
			e.Graphics.FillPath(new SolidBrush(filled ? color : FormDesign.Design.BackColor), path);
		}

		private GraphicsPath StarPoints(Rectangle bounds)
		{
			var num_points = 5;
			var pts = new PointF[num_points + 1];

			var rx = bounds.Width / 2;
			var ry = bounds.Height / 2;
			var cx = bounds.X + rx;
			var cy = bounds.Y + ry;

			// Start at the top.
			var theta = -Math.PI / 2;
			var dtheta = 4 * Math.PI / num_points;
			for (var i = 0; i < num_points; i++)
			{
				pts[i] = new PointF(
					(float)(cx + rx * Math.Cos(theta)),
					(float)(cy + ry * Math.Sin(theta)));
				theta += dtheta;
			}
			pts[num_points] = pts[0];

			var path = new GraphicsPath(FillMode.Winding);
			path.AddPolygon(pts);
			return path;
		}

		private void slickControl1_MouseMove(object sender, MouseEventArgs e)
		{
			if (slickControl1.HoverState.HasFlag(HoverState.Pressed))
			{
				var size = (int)(65 * UI.FontScale) + 6;
				var rect = new Rectangle((slickControl1.Width - size * 5) / 2, 30 + (slickControl1.Height - size) / 2, size * 5, size);
				Rating = ((e.X + 3 - rect.X) / (double)rect.Width * 10).Between(0, 10);
				slickControl1.Invalidate();
			}
		}

		private void B_Done_Click(object sender, EventArgs e)
		{
			Rating = Math.Round(Rating, 1);
			DialogResult = DialogResult.OK;
			Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}