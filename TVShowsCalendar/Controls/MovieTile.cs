using Extensions;





using SlickControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public partial class MovieTile : SlickAdvancedImageControl
	{
		public Movie Movie { get; private set; }
		public bool Horizontal { get; private set; }
		public bool DisplayView { get; private set; }

		public List<string> ShownTags { get; } = new List<string>();

		public MovieTile(Movie movie, bool horizontal = false, bool displayView = false)
		{
			InitializeComponent();

			Movie = movie;
			Horizontal = horizontal;
			DisplayView = displayView;
			EnableDots = true;
			DefaultImage = ProjectImages.Huge_Movie;

			if (horizontal)
				Margin = new Padding(7);

			if (I_Action.Visible = !displayView && horizontal && Movie.AirState == AirStateEnum.Aired)
			{
				var vid = Movie.Playable;
				I_Action.Icon = vid ? ProjectImages.Tiny_Play : ProjectImages.Tiny_Download;
				I_Action.HoverStyle = vid ? ColorStyle.Active : ColorStyle.Green;
				I_Action.Parent = this;
			}

			LinkedMovie_TMDbDataChanged(this, new EventArgs());

			MovieManager.MovieRemoved += MovieManager_MovieRemoved;
		}

		protected override void UIChanged()
		{
			Size = UI.Scale(Horizontal ? new Size(350, 135) : new Size(275, 220), UI.FontScale);
			ImageBounds = Horizontal
				   ? new Rectangle(new Point(1, 1), UI.Scale(new Size(88, 133), UI.FontScale))
				   : new Rectangle(1, 0, Width - 2, (Width - 2) * 9 / 16);
		}

		private void MovieManager_MovieRemoved(Movie show)
		{
			if (show == Movie)
				Dispose();
		}

		private void LinkedMovie_TMDbDataChanged(object sender, EventArgs e) => this.TryInvoke(() =>
		{
			if (string.IsNullOrWhiteSpace(Movie.BackdropPath))
			{
				Invalidate();
			}
			else
			{
				if (Horizontal)
					this.GetImage(Movie.PosterPath, 82, false);
				else
					this.GetImage(Movie.BackdropPath, 275, false);
			}
		});

		private void MoviePage() => Data.Mainform.PushPanel(null, new PC_MoviePage(Movie));

		protected override IEnumerable<Bitmap> HoverIcons
		{
			get
			{
				yield return ProjectImages.Icon_Info;

				if (!Horizontal && Movie.Playable)
					yield return ProjectImages.Icon_PlaySlick;
			}
		}

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				if (Movie.ReleaseDate.IfNull(false, Movie.ReleaseDate < DateTime.Today))
				{
					if ((Movie.ReleaseDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-8))
						yield return new Banner("NEW", BannerStyle.Active, ProjectImages.Tiny_New);
					if (!Movie.Watched)
						yield return new Banner(Horizontal ? string.Empty : "NOT WATCHED", BannerStyle.Yellow, ProjectImages.Tiny_Unwatched);
				}

				if (Movie.Rating.Loved)
					yield return new Banner("Loved", BannerStyle.Red, ProjectImages.Tiny_Love);

				if (Movie.Rating.Rated)
					yield return new Banner(Movie.Rating.Rating.ToString("0.##"), Movie.Rating.Rating.RatingColor(), ProjectImages.Tiny_Rating);

				var rating = Movie.VoteAverage;
				var votes = Movie.VoteCount;

				if (votes > 0)
					yield return new Banner(rating.ToString("0.##"), rating.RatingColor(), ProjectImages.Tiny_Star);

				foreach (var item in ShownTags)
					yield return new Banner(item, BannerStyle.Text, ProjectImages.Tiny_Search);
			}
		}

		protected override void OnDraw(PaintEventArgs e)
		{
			var progress = Movie.Progress;
			var barRect = Enabled && progress > 0
				? new Rectangle(3 + ImageBounds.Left, ImageBounds.Top + ImageBounds.Height - (int)(5 * UI.FontScale) - 2, ImageBounds.Width - 5, (int)(5 * UI.FontScale))
				: Rectangle.Empty;

			if (Enabled && progress > 0)
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(175, FormDesign.Design.AccentColor)), barRect);

				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, FormDesign.Design.ActiveColor))
					, new RectangleF(barRect.X, barRect.Y, (float)(barRect.Width * progress / 100), barRect.Height));
			}

			DrawTextOnImage(e, "MOVIE", false, barRect.Height);

			I_Action.Draw(e);

			DrawText(e, Movie.Title, UI.Font(9.75F, FontStyle.Bold), FormDesign.Design.ForeColor, rigthPad: 40);

			DrawText(e, Horizontal
					? Movie.Tagline
					: string.Join(" • ", Movie.Genres?.Take(3).Select(x => x.Name))
				, UI.Font(8.25F)
				, FormDesign.Design.LabelColor
				, fill: Horizontal);

			var txt = "No release date yet";
			if (Movie.ReleaseDate != null)
				txt = (Movie.ReleaseDate < DateTime.Today ? "Released " : "Premieres ") + Movie.ReleaseDate?.RelativeString();

			DrawText(e, txt, UI.Font(6.75F), FormDesign.Design.InfoColor);

			if (Horizontal)
				DrawText(e, Movie.Overview.IfEmpty("No overview"), UI.Font(6.75F), FormDesign.Design.InfoColor, fill: true);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
				MoviePage();
			else
				base.OnMouseClick(e);
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (new Rectangle(1 + (Width - 2) / 2, 0, (Width - 2) / 2, (Width - 2) * 9 / 16).Contains(e.Location) 
					&& Movie.Playable)
					Movie.Play();
				else
					MoviePage();
			}
			else if (e.Button == MouseButtons.Right)
				Movie.ShowStrip(PointToScreen(e.Location));
		}

		protected override void OnDotsMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				Movie.ShowStrip();
		}

		private void MovieTile_MouseMove(object sender, MouseEventArgs e)
		{
			if (DisplayView)
				return;

			if (Horizontal && I_Action.MouseHovered)
				Cursor = Cursors.Hand;
		}

		private void I_Action_Click(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				if (Movie.Playable)
					Movie.Play();
				else
					Data.Mainform.PushPanel(null, new PC_Download(Movie));
		}
	}
}