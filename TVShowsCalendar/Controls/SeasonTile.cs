using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public class SeasonTile : ContentControl<Season>
	{
		public SeasonTile(Season season) : base(season)
		{
		}

		protected override void UIChanged()
		{
			Size = new Size((int)(110 * UI.FontScale), (int)(110 * 3 / 2 * UI.FontScale) + UI.Font(8.25F).Height * 2 + 10);
			ImageBounds = new Rectangle(new Point(1, 1), new Size(Width - 2, (Width - 2) * 3 / 2)); ;
		}

		protected override void InfoChanged(object sender, EventArgs e)
		{
			if (imagePath != Content.PosterPath.IfEmpty(Content.Show.PosterPath))
				this.GetImage(imagePath = Content.PosterPath.IfEmpty(Content.Show.PosterPath), 110, false);
			SlickTip.SetTo(this, Content.Name);
		}

		protected override void OnDraw(PaintEventArgs e)
		{
			if (Loading && (Content.Episodes?.Any() ?? false)) Loading = false;

			if (Image == null && !Loading)
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

			DrawText(e, Content.Name, UI.Font(8.25F), FormDesign.Design.ForeColor, 3, 25);

			DrawText(e, Content.SubInfo, UI.Font(6.75F), FormDesign.Design.LabelColor);
		}
	}

	public partial class _SeasonTile : SlickAdvancedImageControl
	{
		public TvShow TvShow { get; }
		public Season Season => TvShow.Seasons.FirstOrDefault(x => x.SeasonNumber == SeasonNumber);
		public int SeasonNumber { get; }

		private readonly WaitIdentifier animationIdentifier = new WaitIdentifier();

		public _SeasonTile(TvShow show, int season)
		{
			InitializeComponent();
			TvShow = show;
			SeasonNumber = season;
			LoadCompleted += SeasonTile_LoadCompleted;

			this.GetImage(Season.PosterPath.IfEmpty(TvShow.PosterPath), 110, false);
			SlickTip.SetTo(this, Season.Name);
		}

		private void SeasonTile_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			Loading = Season.Temporary;
		}

		protected override void UIChanged()
		{
			Size = new Size((int)(110 * UI.FontScale), (int)(110 * 3 / 2 * UI.FontScale) + UI.Font(8.25F).Height * 2 + 10);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
				Data.Mainform.PushPanel(null, new PC_SeasonView(Season));
			else
				base.OnMouseClick(e);
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (!Season.Temporary)
			{
				if (e.Button == MouseButtons.Left)
					Data.Mainform.PushPanel(null, new PC_SeasonView(Season));
				else if (e.Button == MouseButtons.Right)
					Season.ShowStrip(PointToScreen(e.Location));
			}
		}

		public override Rectangle ImageBounds
		{
			get => new Rectangle(new Point(1, 1 + Padding.Top), new Size(Width - 2, (Width - 2) * 3 / 2));
		}

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				if (Season.Episodes.Any(x => !x.Watched && x.AirState == AirStateEnum.Aired))
				{
					if (Season.Episodes.LastThat(x => x.AirState == AirStateEnum.Aired) != null && !Season.Episodes.LastThat(x => x.AirState == AirStateEnum.Aired).Watched && (Season.Episodes.LastThat(x => x.AirState == AirStateEnum.Aired).AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-8))
						yield return new Banner("NEW EPISODE", BannerStyle.Active, ProjectImages.Tiny_New);

					yield return new Banner($"{Season.Episodes.Count(x => !x.Watched)} Episode".Plural(Season.Episodes.Count(x => !x.Watched)), BannerStyle.Yellow, ProjectImages.Tiny_Unwatched);

					if (Season.Rating.Loved)
						yield return new Banner("Loved", BannerStyle.Red, ProjectImages.Tiny_Love);

					if (Season.Rating.Rated)
						yield return new Banner(Season.Rating.Rating.ToString("0.##"), Season.Rating.Rating.RatingColor(), ProjectImages.Tiny_Rating);
				}
			}
		}

		protected override IEnumerable<Bitmap> HoverIcons { get; } = new[] { ProjectImages.Icon_Info };

		protected override void OnDraw(PaintEventArgs e)
		{
			if (Loading && (Season.Episodes?.Any() ?? false)) Loading = false;

			if (Image == null && !Loading)
			{
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

				var sBnd = e.Graphics.Measure("Season", UI.Font(9.75F));
				var nBnd = e.Graphics.Measure(Season.SeasonNumber.ToString(), UI.Font(32));
				var numberPoint = new Point((int)((Width - nBnd.Width) * 2 / 3), Padding.Top + (int)((Height - nBnd.Height) / 2));

				using (var brush = new SolidBrush(FormDesign.Design.IconColor))
				{
					e.Graphics.DrawString(Season.SeasonNumber.ToString(), UI.Font(32), brush, numberPoint);
					e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
					e.Graphics.DrawString("Season", UI.Font(9.75F), brush, (int)((Width - sBnd.Width) / 3), numberPoint.Y - sBnd.Height / 2);
				}
			}

			DrawText(e, Season.Name, UI.Font(8.25F), FormDesign.Design.ForeColor);

			DrawText(e, $"{Season.Episodes.Count} Episode".Plural(Season.Episodes.Count) + (Season.Episodes?.FirstOrDefault()?.AirDate == null ? null : $" • {Season.Episodes?.FirstOrDefault()?.AirDate?.Year.ToString()}"), UI.Font(6.75F), FormDesign.Design.LabelColor);
		}

		private void SeasonTile_HoverStateChanged(object sender, HoverState e)
		{
			if (e.HasFlag(HoverState.Hovered) || e.HasFlag(HoverState.Focused))
			{
				if (AnimationHandler.NoAnimations)
				{
					Padding = new Padding(0);
					Invalidate();
				}
				else
					animationIdentifier.Wait(() => AnimationHandler.Animate(this, new Padding(0), 0D), 150);
			}
			else
			{
				animationIdentifier.Cancel();
				AnimationHandler.Animate(this, new Padding(0, 10, 0, 0), 0D);
			}
		}
	}
}