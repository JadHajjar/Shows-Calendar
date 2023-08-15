using Extensions;

using SlickControls;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class BorderedImage : SlickImageControl
	{
		public string ImageUrl { get; set; }

		public BorderedImage()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			var imgRect = new Rectangle(1, 1, Width - 2, Height - 2);

			e.Graphics.DrawBorderedImage(
				Loading ? null : (Image ?? Properties.Resources.Icon_ErrorImage.Color(FormDesign.Modern.IconColor)),
				imgRect
			);

			DrawFocus(e.Graphics, imgRect, 0);

			if (Loading)
				DrawLoader(e.Graphics, new Rectangle(new Rectangle(1, 1, Width - 2, Height - 2).Center(new Size(32, 32)), new Size(32, 32)));
			else if (Enabled && (HoverState.HasFlag(HoverState.Focused) || HoverState.HasFlag(HoverState.Hovered)))
				e.Graphics.DrawIconsOverImage(imgRect, PointToClient(MousePosition), Properties.Resources.Icon_Images);
		}

		private void BorderedImage_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
			{
				Data.Mainform.PushPanel(null, new PC_ViewImage(ImageUrl, true));
			}
			else if (e.Button == MouseButtons.Right)
			{
				SlickToolStrip.Show(Data.Mainform, PointToScreen(e.Location),
					new SlickStripItem("Save Image"
					, () =>
					{
						var path = SaveImage();

						if (path != null)
							Image.Save(path);
					}
					, Properties.Resources.Tiny_Download),

					new SlickStripItem("Copy Image"
					, () => Clipboard.SetImage(Image)
					, Properties.Resources.Tiny_Clipboard),

					new SlickStripItem("", show: !string.IsNullOrWhiteSpace(ImageUrl)),

					new SlickStripItem("Download Original"
					, () =>
					{
						var path = SaveImage();

						if (path != null)
						{
							var frm = Notification.Create("Downloading Image", "Getting the full resolution image you requested.\nThis should take a couple seconds..", SlickControls.PromptIcons.Info, null)
											.Show(Data.Mainform);
							var pb = frm.PictureBox;

							pb.GetImage(ImageUrl, 0);
							pb.LoadCompleted += (s, re) =>
							{
								if (re.Error == null && !re.Cancelled)
									pb.Image.Save(path);
								frm.TryInvoke(frm.Dispose);
							};
						}
					}
					, Properties.Resources.Tiny_CloudDownload
					, show: !string.IsNullOrWhiteSpace(ImageUrl))
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