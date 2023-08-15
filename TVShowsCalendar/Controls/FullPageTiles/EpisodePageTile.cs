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
	public partial class EpisodePageTile : FullPageContent<Episode>
	{
		internal Page currentPage;
		private PC_EpisodeView ParentPanel;

		public enum Page
		{
			Info,
			Cast,
			Crew,
			Images,
			Videos,
			VidFiles
		}

		public Page CurrentPage
		{
			get => currentPage;
			set
			{
				lock (this)
				{
					currentPage = (value == Page.VidFiles && (ContentInfo.Show.Temporary || !ContentInfo.Playable)) ? Page.Info : value;
					ParentPanel.ViewPage(CurrentPage);
					SideButtons.Foreach(x => { x.Selected = x.PageId == (int)currentPage; x.Invalidate(); });
					ActionButton.Visible = value == Page.Info && ActionButton.Image != null;
					DotsButton.Visible = value == Page.Info;
					MainPanel.Visible = DrawInfo;
				}
			}
		}

		public override bool DrawInfo => CurrentPage == Page.Info;

		public EpisodePageTile(PC_EpisodeView panel)
		{ ParentPanel = panel; InitializeComponent(); }

		public void SetData(Episode ep)
		{
			ContentInfo = ep;

			Title = ContentInfo.Name;
			SubTitle = $"Season {ContentInfo.SN} • Episode {ContentInfo.EN} • {ContentInfo.Show}";
			PosterImage.DefaultImage = ContentInfo.HugeIcon;

			OverviewLabel.Text = ContentInfo.Overview.IfEmpty(ContentInfo.Show.Overview);
			TypeLabel.Text = ContentInfo.Show.ShowType;
			ReleaseDateLabel.Text = ContentInfo.Show.ShowType;

			switch (ContentInfo.AirState)
			{
				case AirStateEnum.Unknown:
					StatusLabel.Text = "No air date yet";
					ReleaseDateLabel.Text = null;
					break;

				case AirStateEnum.Aired:
					StatusLabel.Text = $"Aired";
					ReleaseDateLabel.Text = $"Premiered {ContentInfo.AirDate?.ToReadableString()}";
					break;

				case AirStateEnum.ToBeAired:
					StatusLabel.Text = $"Airs {ContentInfo.AirDate?.RelativeString()}";
					ReleaseDateLabel.Text = $"Premieres {ContentInfo.AirDate?.ToReadableString()}";
					break;
			}

			SeasonControl.UnInstall();
			SeasonControl.Install(ContentInfo.Season);

			PreviousButton.Visible = ContentInfo.Previous != null;
			NextButton.Visible = ContentInfo.Next != null;

			SocialLinksControl.Homepage = ContentInfo.Show.Homepage;
			SocialLinksControl.Imdb = ContentInfo.Show.ExternalIds?.ImdbId;
			SocialLinksControl.Twitter = ContentInfo.Show.ExternalIds?.TwitterId;
			SocialLinksControl.Facebook = ContentInfo.Show.ExternalIds?.FacebookId;
			SocialLinksControl.Instagram = ContentInfo.Show.ExternalIds?.InstagramId;

			TagsPanel.SetTags(ContentInfo.Rating.Tags);

			if ((ContentInfo.GuestStars?.Any() ?? false) && GuestsPanel.Controls.Count == 0)
			{
				FeaturesLabel.Title = "Featuring";
				GuestsLabel.Text = $"{(char)0x200B}";
				foreach (var item in ContentInfo.GuestStars)
					GuestsPanel.Controls.Add(new ImagePersonControl(item));
			}

			if ((ContentInfo.Season.Credits?.Cast?.Any() ?? false) && FeaturesPanel.Controls.Count == 0)
			{
				FeaturesLabel.Text = $"{(char)0x200B}";
				foreach (var item in ContentInfo.Season.Credits.Cast.Take(5))
					FeaturesPanel.Controls.Add(new ImagePersonControl(item));
			}

			if (PosterImage.Image == null && !PosterImage.Loading)
				PosterImage.LoadImage(() => ImageHandler.GetImage(ContentInfo.PosterPath, 300, false, true));

			if (ContentInfo.Show.Networks != null)
				foreach (var item in ContentInfo.Show.Networks)
					NetworksControl.Add(item.Id, item.Name, item.LogoPath);

			NetworksControl.Text = "Network".Plural(ContentInfo.Show.Networks);

			LoadBackground(ContentInfo.BackdropPath);

			this.TryInvoke(Invalidate);
		}

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				yield return new Banner("EPISODE", BannerStyle.Text, ProjectImages.Tiny_TVEmpty);

				if (((ContentInfo.AirDate?.IfNull(false, ContentInfo.AirDate < DateTime.Today)) ?? false) && ContentInfo.AirState == AirStateEnum.Aired)
				{
					if ((ContentInfo.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-8))
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
				if (ShowManager.Show(ContentInfo.Show.Id) == null)
					yield return ProjectImages.Huge_Plus;
				else if (ContentInfo.Playable)
					yield return ProjectImages.Huge_Play;
				else if (ContentInfo.AirState == AirStateEnum.Aired)
					yield return ProjectImages.Huge_Download;
			}
		}

		protected override Bitmap DefaultImage => ProjectImages.Huge_TVPlay;

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
				if (ShowManager.Show(ContentInfo.Show.Id) == null)
				{
					ShowManager.Add(ContentInfo.Show);
				}
				else if (ContentInfo.Playable)
				{
					ContentInfo.Play();
				}
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