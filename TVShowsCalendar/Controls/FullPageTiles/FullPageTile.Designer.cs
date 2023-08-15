using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowsCalendar
{
	partial class FullPageTile
	{
		protected SlickImageBackgroundPanel _SidePanel;
		protected SlickImageBackgroundPanel _MainPanel;
		protected SlickImageBackgroundPanel SidePanel;
		protected SlickImageBackgroundPanel MainPanel;
		protected SlickImageBackgroundPanel ActionButtonsPanel;
		protected SlickImageBackgroundPanel PageButtonsPanel;
		protected SlickImageBackgroundControl PosterImage;
		protected SlickImageBackgroundControl TopLabel;
		protected SlickImageBackgroundControl SubInfo;
		private Image blurredImage;

		private void InitializeComponent()
		{
			Dock = DockStyle.Fill;
			Tag = "NoMouseDown";

			_SidePanel = new SlickImageBackgroundPanel
			{
				Dock = DockStyle.Left,
				Margin = new Padding(15)
			};

			_MainPanel = new SlickImageBackgroundPanel
			{
				Dock = DockStyle.Fill
			};

			ActionButtonsPanel = new SlickImageBackgroundPanel
			{
				Dock = DockStyle.Right,
				Width = 80,
				Padding = new Padding(0, (int)(16 * UI.UIScale) - 5, 0, 10)
			};

			PageButtonsPanel = new SlickImageBackgroundPanel
			{
				Dock = DockStyle.Right,
				Width = 90,
				Padding = new Padding(0, (int)(16 * UI.UIScale) - 5, 10, 10)
			};

			PosterImage = new SlickImageBackgroundControl
			{
				Dock = DockStyle.Top,
				Cursor = Cursors.Hand,
				DefaultImage = Properties.Resources.Huge_TV
			};

			TopLabel = new SlickImageBackgroundControl
			{
				Dock = DockStyle.Top,
				Cursor = Cursors.Hand,
				Margin = new Padding(0, 15, 0, 3)
			};

			SubInfo = new SlickImageBackgroundControl
			{
				Dock = DockStyle.Top,
				Margin = new Padding(5, 0, 0, 0)
			};

			Content.Add(_SidePanel);
			Content.Add(_MainPanel);
			Content.Add(PageButtonsPanel);
			Content.Add(ActionButtonsPanel);

			_SidePanel.Controls.Add(PosterImage);
			_SidePanel.Controls.Add(SidePanel = new SlickImageBackgroundPanel
			{
				Dock = DockStyle.Fill,
				AutoScroll = true
			});

			_MainPanel.Controls.Add(TopLabel);
			_MainPanel.Controls.Add(SubInfo);
			_MainPanel.Controls.Add(MainPanel = new SlickImageBackgroundPanel
			{
				Dock = DockStyle.Fill,
				AutoScroll = true
			});

			PosterImage.ImageChanged += posterImage_ImageChanged;
			PosterImage.Paint += posterImage_Paint;
			PosterImage.MouseClick += (s, e) => OnImageMouseClick(e);
			TopLabel.MouseClick += TitleClicked;
			TopLabel.Paint += topLabel_Paint;
			SubInfo.Paint += subInfo_Paint;
			TopLabel.CalculateAutoSize += topLabel_CalculateAutoSize;
			SubInfo.CalculateAutoSize += subInfo_CalculateAutoSize;

			var w = (int)(16 * UI.UIScale) + 6;
			var right = w * 3;
			foreach (TopIcon.IconStyle item in Enum.GetValues(typeof(TopIcon.IconStyle)))
			{
				var b = new SlickImageBackgroundControl
				{
					Data = item,
					Bounds = new Rectangle(right, 0, w, w),
					Padding = new Padding(0, 6, 6, 0),
					Anchor = AnchorStyles.Top | AnchorStyles.Right,
					Cursor = Cursors.Hand
				};

				b.MouseClick += topIcon_MouseClick;
				b.Paint += topIcon_Paint;
				Content.Add(b);
				right -= w;
			}
		}

		private void posterImage_ImageChanged(object sender, EventArgs e)
		{
			if (PosterImage.Image == null)
			{
				blurredImage?.Dispose();
				blurredImage = null;
			}
			else Data.Mainform.OnNextIdle(() => new BackgroundAction(() =>
				{
					blurredImage = new Bitmap(PosterImage.Image).Blur(40);

					PosterImage.Invalidate();
				}).Run());
		}

		private void subInfo_CalculateAutoSize(object sender, PaintEventArgs e)
		{
			SubInfo.Height = 15 + (int)e.Graphics.Measure(DrawInfo ? SubTitle : $"{PageName} • {SubTitle}", UI.Font(9F, FontStyle.Bold), SubInfo.Width).Height;
			SubInfo.Margin = new Padding(DrawInfo ? 5 : 15, 0, 0, 0);
		}

		private void topLabel_CalculateAutoSize(object sender, PaintEventArgs e)
		{
			e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			var bnds = e.Graphics.Measure(Title, UI.Font(20F, FontStyle.Bold), TopLabel.Width);
			TopLabel.Size = new Size(35 + (int)bnds.Width, (int)bnds.Height + 3);
			TopLabel.Margin = new Padding(DrawInfo ? 0 : 10, 15, 0, 3);
		}

		private void topLabel_Paint(object sender, PaintEventArgs e)
		{       
			e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

			var titleRect = TopLabel.DrawBounds.Pad(1);
			var titleHovered = TopLabel.HoverState.HasFlag(HoverState.Hovered);

			if (titleHovered)
			{
				e.Graphics.FillRoundedRectangle(Gradient(TopLabel.HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveColor : Color.FromArgb(200, FormDesign.Design.BackColor), titleRect.Pad(2)), titleRect, 10);
				if (!TopLabel.HoverState.HasFlag(HoverState.Pressed))
					e.Graphics.DrawRoundedRectangle(new Pen(Color.FromArgb(125, FormDesign.Design.ActiveColor), 2F) { DashStyle = DashStyle.Dash }, titleRect, 10);
			}

			e.Graphics.DrawImage(Properties.Resources.Big_ChevronLeft.Color(titleHovered ? (TopLabel.HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveForeColor : FormDesign.Design.ActiveColor) : FormDesign.Design.ForeColor), new Rectangle(titleRect.X + 6, titleRect.Y + (titleRect.Height - 24) / 2, 24, 24));
			e.Graphics.DrawString(
				Title,
				UI.Font(20F, FontStyle.Bold),
				new SolidBrush(titleHovered && TopLabel.HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveForeColor : FormDesign.Design.ForeColor),
				titleRect.Pad(32, 0, 0, 0));
		}

		private void subInfo_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawString(DrawInfo ? SubTitle : $"{PageName} • {SubTitle}", UI.Font(9F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor.MergeColor(FormDesign.Design.BackColor, 80)), SubInfo.DrawBounds);
		}

		private void posterImage_Paint(object sender, PaintEventArgs e)
		{
			var imgBounds = PosterImage.DrawBounds.Pad(0, 0, 1, 1);

			if (PosterImage.Image != null || PosterImage.Loading)
				e.Graphics.DrawBorderedImage(PosterImage.HoverState.HasFlag(HoverState.Hovered) ? blurredImage ?? PosterImage.Image : PosterImage.Image, imgBounds, ImageSizeMode.CenterScaled, FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.ForeColor, 65));
			else
				e.Graphics.DrawBorderedImage(PosterImage.DefaultImage.Color(PosterImage.HoverState.HasFlag(HoverState.Hovered) ? FormDesign.Design.ActiveColor : FormDesign.Design.IconColor), imgBounds, ImageSizeMode.Center, FormDesign.Design.AccentColor.MergeColor(FormDesign.Design.ForeColor, 65));

			if (PosterImage.Loading)
				e.Graphics.DrawLoader(PosterImage.LoaderPercentage, imgBounds.CenterR(64, 64));

			if (PosterImage.HoverState.HasFlag(HoverState.Hovered) && HoverIcons != null)
				e.Graphics.DrawIconsOverImage(imgBounds, PosterImage.PointToClient(Cursor.Position), HoverIcons.ToArray());

			if (Data.Options.AlwaysShowBanners || PosterImage.HoverState.HasFlag(HoverState.Hovered))
				e.Graphics.DrawBannersOverImage(PosterImage.PointToClient(Cursor.Position), imgBounds, Banners, 8.25F);
		}

		private void topIcon_Paint(object sender, PaintEventArgs e)
		{
			var c = (sender as SlickImageBackgroundControl);
			var bnds = c.DrawBounds;
			var color = FormDesign.Design.IconColor;

			switch ((TopIcon.IconStyle)c.Data)
			{
				case TopIcon.IconStyle.Maximize:
					color = FormDesign.Design.GreenColor; break;

				case TopIcon.IconStyle.Minimize:
					color = FormDesign.Design.YellowColor; break;

				case TopIcon.IconStyle.Close:
					color = FormDesign.Design.RedColor; break;
			}

			e.Graphics.FillEllipse(
				Gradient(c.HoverState.HasFlag(HoverState.Hovered) ? color : Color.FromArgb(75, FormDesign.Design.BackColor.MergeColor(color, 50)), bnds, c.HoverState.HasFlag(HoverState.Hovered) ? 3 : 1.5F),
				new RectangleF(bnds.X, bnds.Y, bnds.Width - 1F, bnds.Height - 1F));

			if (!c.HoverState.HasFlag(HoverState.Hovered))
				e.Graphics.DrawEllipse(
					new Pen(color, 1.5F),
					new RectangleF(bnds.X, bnds.Y, bnds.Width - 1.5F, bnds.Height - 1.5F));
		}

		private void topIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				var c = (sender as SlickImageBackgroundControl);

				switch ((TopIcon.IconStyle)c.Data)
				{
					case TopIcon.IconStyle.Minimize:
						Data.Mainform.Hide();
						break;
					case TopIcon.IconStyle.Maximize:
						Data.Mainform.WindowState = Data.Mainform.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
						break;
					case TopIcon.IconStyle.Close:
						Data.Mainform.WindowState = FormWindowState.Minimized;
						break;
				}
			}
		}
	}
}
