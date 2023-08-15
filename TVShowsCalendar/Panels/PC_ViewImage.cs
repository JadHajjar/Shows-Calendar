using Extensions;

using SlickControls;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PC_ViewImage : PanelContent
	{
		private bool zoomed;
		private bool imgLoaded;

		private Bitmap plus;
		private Bitmap minus;

		public PC_ViewImage(string url, bool tmdb = false)
		{
			InitializeComponent();

			if (tmdb)
				pictureBox.GetImage(url, 0);
			else
				pictureBox.LoadAsync(url);

			var md = new MouseDetector();
			md.MouseMove += Md_MouseMove;

			pictureBox.LoadCompleted += PictureBox_LoadCompleted;

			Disposed += (s, e) => md.Dispose();
		}

		private void Md_MouseMove(object sender, Point p)
		{
			if (zoomed)
			{
				var w = Width / 3;
				var h = Height / 3;
				var cur = PointToClient(p);
				var x = -(2D * (cur.X - w)).Between(0, 2 * w) / (2 * w);
				var y = -(2D * (cur.Y - h)).Between(0, 2 * h) / (2 * h);

				new AnimationHandler(pictureBox,
					new Point(
					(int)((pictureBox.Width - Width + 200) * x) + 100,
					(int)((pictureBox.Height - Height + 200) * y) + 100))
				{ Speed = 2 }.StartAnimation();
			}
		}

		private void PictureBox_LoadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			if (e.Error != null || e.Cancelled)
			{
				pictureBox.Cursor = Cursors.Default;
			}
			else
			{
				imgLoaded = true;
				pictureBox.Cursor = new Cursor(plus.GetHicon());
			}
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			pictureBox.ErrorImage = Properties.Resources.Icon_ErrorImage.Color(design.RedColor);

			var magnifier = Properties.Resources.Zoom_Magnifier.Color(design.ForeColor);
			using (var g = Graphics.FromImage(magnifier))
			{
				g.DrawImage(Properties.Resources.Zoom_Plus.Color(design.BackColor), new Rectangle(Point.Empty, Properties.Resources.Zoom_Magnifier.Size));
				plus = magnifier;
			}

			magnifier = Properties.Resources.Zoom_Magnifier.Color(design.ForeColor);
			using (var g = Graphics.FromImage(magnifier))
			{
				g.DrawImage(Properties.Resources.Zoom_Minus.Color(design.BackColor), new Rectangle(Point.Empty, Properties.Resources.Zoom_Magnifier.Size));
				minus = magnifier;
			}
		}

		private void pictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			if (imgLoaded && e.Button == MouseButtons.Left)
			{
				zoomed = !zoomed;

				if (zoomed)
				{
					pictureBox.Cursor = new Cursor(minus.GetHicon());
					pictureBox.Dock = DockStyle.None;
					pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
					Md_MouseMove(null, Cursor.Position);
				}
				else
				{
					pictureBox.Cursor = new Cursor(plus.GetHicon());
					pictureBox.Dock = DockStyle.Fill;
					pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
					AnimationHandler.CancelAnimations(pictureBox);
				}
			}
			else
			{
				SlickToolStrip.Show(Form,
				new SlickStripItem("Save Image"
					, () =>
					{
						var path = SaveImage();

						if (path != null)
							new Bitmap(pictureBox.Image).Save(path);
					}
					, Properties.Resources.Tiny_Download),

					new SlickStripItem("Copy Image"
					, () => Clipboard.SetImage(pictureBox.Image)
					, Properties.Resources.Tiny_Clipboard)
				);
			}
		}

		private string SaveImage()
		{
			var sd = new SaveFileDialog() { Filter = "Images|*.jpeg;*.jpg", InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) };

			if (sd.ShowDialog(this) == DialogResult.OK)
				return sd.FileName;

			return null;
		}
	}
}