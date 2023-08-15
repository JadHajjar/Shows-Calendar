using Extensions;

using SlickControls;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public class ImageSocialLinksControl : SlickImageBackgroundPanel
	{
		public string Homepage { get; set; }
		public string Imdb { get; set; }
		public string Twitter { get; set; }
		public string Instagram { get; set; }
		public string Facebook { get; set; }

		private readonly SlickImageBackgroundControl homeControl = new SlickImageBackgroundControl { Image = Properties.Resources.Icon_Website, Size = new Size(36, 24), Padding = new Padding(12, 0, 0, 0), Cursor = Cursors.Hand, Dock = DockStyle.Right };
		private readonly SlickImageBackgroundControl imdbControl = new SlickImageBackgroundControl { Image = Properties.Resources.Icon_Imdb, Size = new Size(36, 24), Padding = new Padding(12, 0, 0, 0), Cursor = Cursors.Hand, Dock = DockStyle.Right };
		private readonly SlickImageBackgroundControl instagramControl = new SlickImageBackgroundControl { Image = Properties.Resources.Icon_Instagram, Size = new Size(36, 24), Padding = new Padding(0, 0, 12, 0), Cursor = Cursors.Hand, Dock = DockStyle.Left };
		private readonly SlickImageBackgroundControl facebookControl = new SlickImageBackgroundControl { Image = Properties.Resources.Icon_Facebook, Size = new Size(36, 24), Padding = new Padding(0, 0, 12, 0), Cursor = Cursors.Hand, Dock = DockStyle.Left };
		private readonly SlickImageBackgroundControl twitterControl = new SlickImageBackgroundControl { Image = Properties.Resources.Icon_Twitter, Size = new Size(36, 24), Padding = new Padding(0, 0, 12, 0), Cursor = Cursors.Hand, Dock = DockStyle.Left };

		public ImageSocialLinksControl()
		{
			Dock = DockStyle.Top;
			Height = 48;
			Padding = new Padding(4, 5, 3, 18);

			Controls.Add(homeControl);
			Controls.Add(imdbControl);
			Controls.Add(instagramControl);
			Controls.Add(facebookControl);
			Controls.Add(twitterControl);

			homeControl.MouseClick += homeControl_MouseClick;
			imdbControl.MouseClick += imdbControl_MouseClick;
			instagramControl.MouseClick += instagramControl_MouseClick;
			facebookControl.MouseClick += facebookControl_MouseClick;
			twitterControl.MouseClick += twitterControl_MouseClick;

			homeControl.Paint += homeControl_Paint;
			imdbControl.Paint += homeControl_Paint;
			instagramControl.Paint += homeControl_Paint;
			facebookControl.Paint += homeControl_Paint;
			twitterControl.Paint += homeControl_Paint;
		}

		private void homeControl_Paint(object sender, PaintEventArgs e)
		{
			var c = sender as SlickImageBackgroundControl;

			if (c.HoverState.HasFlag(HoverState.Pressed))
				e.Graphics.DrawImage(c.Image.Color(FormDesign.Design.ActiveForeColor)
					, c.DrawBounds.Margin(1, 1, 0, 0), ImageSizeMode.Center);

			e.Graphics.DrawImage(c.Image.Color(c.HoverState.HasFlag(HoverState.Hovered) ? FormDesign.Design.ActiveColor : FormDesign.Design.IconColor)
				, c.DrawBounds.Pad(0, 0, 1, 1), ImageSizeMode.Center);
		}

		private void twitterControl_MouseClick(object sender, MouseEventArgs e)
		{
			ShowsCalendar.Data.Open($"https://twitter.com/{Twitter}");
		}

		private void facebookControl_MouseClick(object sender, MouseEventArgs e)
		{
			ShowsCalendar.Data.Open($"https://facebook.com/{Facebook}");
		}

		private void instagramControl_MouseClick(object sender, MouseEventArgs e)
		{
			ShowsCalendar.Data.Open($"https://instagram.com/{Instagram}");
		}

		private void imdbControl_MouseClick(object sender, MouseEventArgs e)
		{
			ShowsCalendar.Data.Open($"https://www.imdb.com/title/{Imdb}");
		}

		private void homeControl_MouseClick(object sender, MouseEventArgs e)
		{
			ShowsCalendar.Data.Open(Homepage);
		}

		public override void OnPaint(PaintEventArgs e)
		{
			var bnds = DrawBounds;
			e.Graphics.DrawLine(new Pen(Color.FromArgb(125, FormDesign.Design.ForeColor), 3F) { DashStyle = DashStyle.Dot, DashCap = DashCap.Round }, bnds.X, bnds.Y + bnds.Height + 14, Width + bnds.X, bnds.Y + bnds.Height + 14);

			base.OnPaint(e);
		}

		public override void CalculateSize(PaintEventArgs e)
		{
			Visible = !string.IsNullOrWhiteSpace(Homepage)
				|| !string.IsNullOrWhiteSpace(Imdb)
				|| !string.IsNullOrWhiteSpace(Twitter)
				|| !string.IsNullOrWhiteSpace(Instagram)
				|| !string.IsNullOrWhiteSpace(Facebook);

			homeControl.Visible = !string.IsNullOrWhiteSpace(Homepage);
			imdbControl.Visible = !string.IsNullOrWhiteSpace(Imdb);
			twitterControl.Visible = !string.IsNullOrWhiteSpace(Twitter);
			instagramControl.Visible = !string.IsNullOrWhiteSpace(Instagram);
			facebookControl.Visible = !string.IsNullOrWhiteSpace(Facebook);
		}
	}
}