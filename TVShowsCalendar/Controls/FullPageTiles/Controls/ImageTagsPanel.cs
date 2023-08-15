using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public class ImageTagsPanel : SlickImageBackgroundFlowLayoutPanel
	{
		public bool Tags { get; }

		public ImageTagsPanel(string title, bool tags)
		{
			Dock = DockStyle.Top;
			Text = title;
			AutoSize = true;
			Tags = tags;
			Margin = new Padding(0, 10, 0, 0);

			//Controls.Add(new SlickImageBackgroundControl
			//{
			//	Dock = DockStyle.Top,
			//	Height = 30
			//});
		}

		public void SetTags(IEnumerable<string> tags)
		{
			while (Controls.Count > 0)
				Controls[0].Dispose();

			if (tags != null)
				foreach (var item in tags.WhereNotEmpty())
				{
					var ctrl = new SlickImageBackgroundControl
					{
						Text = item.FormatWords(),
						Data = item,
						Cursor = Tags ? Cursors.Hand : null,
						Padding = new Padding(9, 9, 0, 0)
					};

					ctrl.Paint += tag_Paint;
					ctrl.CalculateAutoSize += tag_CalculateSize;

					if (Tags)
						ctrl.MouseClick += ctrl_MouseClick;

					Controls.Add(ctrl);
				}
		}

		private void ctrl_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				(Container as FullPageTile).RemoveTag((sender as SlickImageBackgroundControl).Data);
		}

		private void tag_Paint(object sender, PaintEventArgs e)
		{
			var c = sender as SlickImageBackgroundControl;
			var bannerRect = c.DrawBounds;
			var h = bannerRect.Height;

			using (var font = UI.Font(8.25F))
			using (var foreBrush = new SolidBrush(FormDesign.Design.MenuForeColor))
			using (var backBrush = SlickControl.Gradient(bannerRect, Color.FromArgb(130, FormDesign.Design.MenuColor)))
			{
				var iconSize = Tags ? new Size(16, 16) : Size.Empty;
				var style = Tags && c.HoverState.HasFlag(HoverState.Pressed) ? BannerStyle.Red : BannerStyle.Text;

				e.Graphics.FillRoundedRectangle(backBrush, bannerRect, h / 3);

				e.Graphics.DrawString(c.Text, font, c.HoverState.HasFlag(HoverState.Hovered) ? new SolidBrush(style.ForeColor()) : foreBrush, bannerRect.Pad(Tags ? (iconSize.Width + 8) : 0, 0, 0, 0), new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });

				if (Tags)
					e.Graphics.DrawImage((c.HoverState.HasFlag(HoverState.Hovered) ? Properties.Resources.Tiny_XClose : Properties.Resources.Tiny_Label).Color(style.ForeColor()),
						new Rectangle(bannerRect.X - 2 + (iconSize.Width / 2), bannerRect.Y + 1 + ((h - iconSize.Height) / 2), iconSize.Width, iconSize.Height));
			}
		}

		private void tag_CalculateSize(object sender, PaintEventArgs e)
		{
			var c = sender as SlickImageBackgroundControl;
			var font = UI.Font(8.25F);
			var iconSize = Tags ? new Size(16, 16) : Size.Empty;
			var noText = string.IsNullOrWhiteSpace(c.Text);
			var size = e.Graphics.Measure(c.Text, font);
			var h = Math.Max(iconSize.Height, (int)size.Height) + 4; h += 1 - h % 2;
			var bannerSize = noText
				? new Size(h * 3 / 2 - 4, h)
				: new Size((int)size.Width + font.Height / 2 + iconSize.Width + 5, h);

			bannerSize.Width += c.Padding.Horizontal;
			bannerSize.Height += c.Padding.Vertical;

			c.Size = bannerSize;
		}

		public override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SetClip(e.Graphics.ClipBounds.Pad(0, -(Location.Y + Padding.Top), 0, 0));
			e.Graphics.DrawString(Text, UI.Font(9.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), new Point(-Padding.Left, -Padding.Top));

			base.OnPaint(e);
		}

		public override void CalculateSize(PaintEventArgs e)
		{
			if (Visible = Controls.Count > 0)
			{
				Padding = new Padding(0, (int)e.Graphics.Measure(Text, UI.Font(9.75F, FontStyle.Bold), Width).Height, 0, 0);
				base.CalculateSize(e);
			}
		}

		protected override void SetControlBounds(SlickImageBackgroundControl control)
		{
			base.SetControlBounds(control);
		}

		protected override void PostControlPaint(SlickImageBackgroundControl control, bool painted)
		{
			base.PostControlPaint(control, painted);
		}
	}
}