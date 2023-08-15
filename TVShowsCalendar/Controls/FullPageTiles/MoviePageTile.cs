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
	public partial class MoviePageTile : FullPageContent<Movie>
	{
		internal Page currentPage;

		public enum Page
		{
			Info,
			Cast,
			Crew,
			Images,
			Videos,
			Similar,
			VidFiles
		}

		public Page CurrentPage
		{
			get => currentPage;
			set
			{
				currentPage = (value == Page.VidFiles && (ContentInfo.Temporary || !ContentInfo.Playable)) ? Page.Info : value;
				(PanelContent.GetParentPanel(this) as PC_MoviePage).ViewPage(CurrentPage);
				SideButtons.Foreach(x => { x.Selected = x.PageId == (int)currentPage; x.Invalidate(); });
				MainPanel.Visible = DrawInfo;
			}
		}

		public override bool DrawInfo => CurrentPage == Page.Info;

		public MoviePageTile()
		{ InitializeComponent(); }

		public void SetData(Movie movie)
		{
			ContentInfo = movie;

			Title = $"{ContentInfo.Name}" + (ContentInfo.ReleaseDate == null ? null : $" • {ContentInfo.ReleaseDate?.Year}");
			SubTitle = (string.Join(" • ", ContentInfo.Genres.Select(x => x.Name).WhereNotEmpty()) + TimeSpan.FromMinutes(ContentInfo.Runtime ?? 0).If(x => x.Ticks == 0, string.Empty, x => $" • {x.ToReadableString()}")).Trim(' ', '•');
			PosterImage.DefaultImage = ContentInfo.HugeIcon;

			OverviewLabel.Text = ContentInfo.Overview;
			StatusLabel.Text = ContentInfo.Status;
			DateAddedLabel.Text = ContentInfo.DateAdded != DateTime.MinValue ? ContentInfo.DateAdded.ToReadableString() : null;
			OriginalTitleLabel.Text = ContentInfo.OriginalTitle == ContentInfo.Name ? null : ContentInfo.OriginalTitle;

			switch (ContentInfo.AirState)
			{
				case AirStateEnum.Unknown:
					ReleaseDateLabel.Text = null;
					break;

				case AirStateEnum.Aired:
					ReleaseDateLabel.Text = $"Premiered {ContentInfo.ReleaseDate?.ToReadableString()}";
					break;

				case AirStateEnum.ToBeAired:
					ReleaseDateLabel.Text = $"Premieres {ContentInfo.ReleaseDate?.ToReadableString()}";
					break;
			}

			if (ContentInfo.Languages?.Any() ?? false)
			{
				LanguageLabel.Title = "Spoken Language".Plural(ContentInfo.Languages);
				LanguageLabel.Text = ContentInfo.Languages.Count == 1 ? ContentInfo.Languages[0]
					: ContentInfo.Languages.Select(x => $"• {x}").ListStrings("\n");
			}

			if (ContentInfo.ProductionCountries?.Any() ?? false)
			{
				CountryLabel.Title = "Filming Countr".Plural(ContentInfo.ProductionCountries, "ies", "y");
				CountryLabel.Text = ContentInfo.ProductionCountries.Count == 1 ? ContentInfo.ProductionCountries[0].Name
					: ContentInfo.ProductionCountries.Select(x => $"• {x.Name}").ListStrings("\n");
			}

			if ((ContentInfo.Cast?.Any() ?? false) && FeaturesPanel.Controls.Count == 0)
				foreach (var item in ContentInfo.Cast.Take(5))
					FeaturesPanel.Controls.Add(new ImagePersonControl(item));

			if ((ContentInfo.Crew?.Any(x => x.Job == "Director") ?? false) && FeaturesPanel.Controls.Count == 0)
			{
				foreach (var item in ContentInfo.Crew.Where(x => x.Job == "Director").Take(5))
					DirectorPanel.Controls.Add(new ImagePersonControl(item));
				DirectorLabel.Text = $"{(char)0x200B}";
			}
			else
				DirectorLabel.Text = string.Empty;

			SocialLinksControl.Homepage = ContentInfo.Homepage;
			SocialLinksControl.Imdb = ContentInfo.ExternalIds?.ImdbId;
			SocialLinksControl.Twitter = ContentInfo.ExternalIds?.TwitterId;
			SocialLinksControl.Facebook = ContentInfo.ExternalIds?.FacebookId;
			SocialLinksControl.Instagram = ContentInfo.ExternalIds?.InstagramId;

			TagsPanel.SetTags(ContentInfo.Rating.Tags);
			KeywordsPanel.SetTags(ContentInfo.Keywords?.Skip(2));

			if (PosterImage.Image == null && !PosterImage.Loading)
				PosterImage.LoadImage(() => ImageHandler.GetImage(ContentInfo.PosterPath, 300, false, true));

			if (ContentInfo.ProductionCompanies != null)
				foreach (var item in ContentInfo.ProductionCompanies)
					NetworksControl.Add(item.Id, item.Name, item.LogoPath);

			NetworksControl.Text = "Production Compan".Plural(ContentInfo.ProductionCompanies, "ies", "y");
			TrailerButton.Visible = ContentInfo.Videos?.Any(x => x.Type == "Trailer" && x.Site == "YouTube" && x.Iso_639_1 == "en") ?? false;

			LoadBackground(ContentInfo.BackdropPath);

			this.TryInvoke(Invalidate);
		}

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				yield return new Banner("MOVIE", BannerStyle.Text, ProjectImages.Tiny_Movie);

				if (ContentInfo.ReleaseDate.IfNull(false, ContentInfo.ReleaseDate < DateTime.Today))
				{
					if ((ContentInfo.ReleaseDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-8))
						yield return new Banner("NEW", BannerStyle.Active, ProjectImages.Tiny_New);
					if (!ContentInfo.Watched)
						yield return new Banner("NOT WATCHED", BannerStyle.Yellow, ProjectImages.Tiny_Unwatched);
				}

				if (ContentInfo.Rating.Loved)
					yield return new Banner("LOVED", BannerStyle.Red, ProjectImages.Tiny_Love);

				if (ContentInfo.Rating.Rated)
					yield return new Banner(ContentInfo.Rating.Rating.ToString("0.##"), ContentInfo.Rating.Rating.RatingColor(), ProjectImages.Tiny_Rating);

				var rating = ContentInfo.VoteAverage;
				var votes = ContentInfo.VoteCount;

				if (votes > 0)
					yield return new Banner(rating.ToString("0.##"), rating.RatingColor(), ProjectImages.Tiny_Star);

				if (ContentInfo.WatchDate > DateTime.MinValue)
					yield return new Banner($"Watched {ContentInfo.WatchDate.ToReadableString(ContentInfo.WatchDate.Year != DateTime.Now.Year, ExtensionClass.DateFormat.MDY, false)}", BannerStyle.Text, ProjectImages.Tiny_Watched);
			}
		}

		protected override IEnumerable<Bitmap> HoverIcons
		{
			get
			{
				if (MovieManager.Movie(ContentInfo.Id) == null)
					yield return ProjectImages.Huge_Plus;
				else if (ContentInfo.Playable)
					yield return ProjectImages.Huge_Play;
				else if (ContentInfo.AirState == AirStateEnum.Aired)
					yield return ProjectImages.Huge_Download;
			}
		}

		protected override Bitmap DefaultImage => ProjectImages.Huge_Movie;

		protected override void TitleClicked(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (currentPage == Page.Info)
					Data.Mainform.PushBack();
				else
					CurrentPage = Page.Info;
			}
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
			{
				if (MovieManager.Movie(ContentInfo.Id) == null)
					MovieManager.Add(ContentInfo);
				else if (ContentInfo.Playable)
					ContentInfo.Play();
				else if (ContentInfo.AirState == AirStateEnum.Aired)
				{
					Cursor.Current = Cursors.WaitCursor;
					Data.Mainform.PushPanel(null, new PC_Download(ContentInfo));
					Cursor.Current = Cursors.Default;
				}
			}
			else
				base.OnImageMouseClick(e);
		}

		public override void RemoveTag(string tag)
		{
			ContentInfo.Rating = ContentInfo.Rating.RemoveTag(tag);
			new BackgroundAction(() => ContentInfo.Save(ChangeType.Preferences)).Run();
		}
	}
}