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
	public partial class ShowTile : SlickAdvancedImageControl
	{
		public List<string> ShownTags { get; } = new List<string>();

		public ShowTile(TvShow show)
		{
			InitializeComponent();

			TvShow = show;
			DefaultImage = ProjectImages.Huge_TV;
			EnableDots = true;

			LinkedShow_TMDbDataChanged(this, new EventArgs());

			ShowManager.ShowRemoved += ShowManager_ShowRemoved;
		}

		protected override void UIChanged()
		{
			Size = UI.Scale(new Size(275, 220), UI.FontScale);
			ImageBounds = new Rectangle(1, 0, Width - 2, (Width - 2) * 9 / 16);
		}

		private void ShowManager_ShowRemoved(TvShow show)
		{
			if (show == TvShow)
				Dispose();
		}

		public TvShow TvShow { get; private set; }

		private void LinkedShow_TMDbDataChanged(object sender, EventArgs e) => this.TryInvoke(() =>
		{
			if (string.IsNullOrWhiteSpace(TvShow.BackdropPath))
				Invalidate();
			else
			{
				this.GetImage(TvShow.BackdropPath, 275, false);
			}
		});

		private void ShowPage() => Data.Mainform.PushPanel(null, new PC_ShowPage(TvShow));

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				TvShow.ShowStrip(PointToScreen(e.Location));
			}
			else if (e.Button == MouseButtons.Left)
			{
				if (new Rectangle(1 + (Width - 2) / 2, 0, (Width - 2) / 2, (Width - 2) * 9 / 16).Contains(e.Location)
					&& TvShow.GetCurrentWatchEpisode() != null)
					TvShow.GetCurrentWatchEpisode().Play();
				else
					ShowPage();
			}
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
				ShowPage();
			else
				base.OnMouseClick(e);
		}

		protected override void OnDotsMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				TvShow.ShowStrip();
		}

		protected override IEnumerable<Bitmap> HoverIcons
		{
			get
			{
				yield return ProjectImages.Icon_Info;

				if (TvShow.Episodes.Any(x => x.Playable))
					yield return ProjectImages.Icon_PlaySlick;
			}
		}

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				if (TvShow.Episodes.Any(x => !x.Watched && x.AirState == AirStateEnum.Aired))
				{
					if (TvShow.LastEpisode != null && (TvShow.LastEpisode.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-8))
						yield return new Banner("NEW EPISODE", BannerStyle.Active, ProjectImages.Tiny_New);

					yield return new Banner($"{TvShow.Episodes.Count(x => !x.Watched)} NOT WATCHED", BannerStyle.Yellow, ProjectImages.Tiny_Unwatched);
				}

				if (TvShow.Rating.Loved)
					yield return new Banner("Loved", BannerStyle.Red, ProjectImages.Tiny_Love);

				if (TvShow.Rating.Rated)
					yield return new Banner(TvShow.Rating.Rating.ToString("0.##"), TvShow.Rating.Rating.RatingColor(), ProjectImages.Tiny_Rating);

				var rating = TvShow.VoteAverage;
				var votes = TvShow.VoteCount;

				if (votes > 0)
					yield return new Banner(rating.ToString("0.##"), rating.RatingColor(), ProjectImages.Tiny_Star);

				foreach (var item in ShownTags.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct())
					yield return new Banner(item.FormatWords(), BannerStyle.Text, ProjectImages.Tiny_Search);
			}
		}

		protected override void OnDraw(PaintEventArgs e)
		{
			DrawTextOnImage(e, "TV SHOW", false);

			DrawText(e, TvShow.Name, UI.Font(9.75F, FontStyle.Bold), FormDesign.Design.ForeColor);

			DrawText(e, string.Join(" • ", TvShow.Genres?.Take(3).Select(x => x.Name)), UI.Font(8.25F), FormDesign.Design.LabelColor);

			DrawText(e, TvShow.Years, UI.Font(6.75F), FormDesign.Design.InfoColor);
		}
	}
}