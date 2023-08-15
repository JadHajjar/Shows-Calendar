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
	public partial class SeasonPageTile : FullPageContent<Season>
	{
		internal Page currentPage;
		private PC_SeasonView ParentPanel;

		public enum Page
		{
			Info,
			Episodes,
			Cast,
			Crew,
			Images,
			Videos
		}

		public Page CurrentPage
		{
			get => currentPage;
			set
			{
				lock (this)
				{
					currentPage = value;
					ParentPanel.ViewPage(CurrentPage);
					SideButtons.Foreach(x => { x.Selected = x.PageId == (int)currentPage; x.Invalidate(); });
					ActionButton.Visible = DotsButton.Visible = value == Page.Info;
					EpisodesParentPanel.Visible = value == Page.Episodes;
					MainPanel.Visible = value == Page.Info;
				}
			}
		}

		public override bool DrawInfo => CurrentPage <= Page.Episodes;

		public SeasonPageTile(PC_SeasonView panel)
		{ ParentPanel = panel; InitializeComponent(); }

		public void SetData(Season season)
		{
			ContentInfo = season;

			Title = $"{ContentInfo.Name}{(ContentInfo.AirDate == null ? null : $" • {ContentInfo.AirDate?.Year}")}";
			SubTitle = (ContentInfo.Episodes.Count == 0 ? null : $"{ContentInfo.Episodes.Count} Episodes • ") + $"{ContentInfo.Show} Season {ContentInfo.SeasonNumber}";
			PosterImage.DefaultImage = ContentInfo.HugeIcon;

			OverviewLabel.Text = ContentInfo.Overview.IfEmpty(ContentInfo.Show.Overview);
			StatusLabel.Text = ContentInfo.Status + ContentInfo.Years.IfEmpty(string.Empty, $" ({ContentInfo.Years})");
			TypeLabel.Text = ContentInfo.Show.ShowType;

			EpisodeControl.Install(getDrawnEpisode(out var epInf));
			EpisodeLabel.Title = epInf;
			EpisodeControl.Visible = EpisodeControl.Content != null;

			PreviousButton.Visible = ContentInfo.Previous != null;
			NextButton.Visible = ContentInfo.Next != null;

			var eps = ContentInfo.Episodes.Count();

			if (eps > 0)
			{
				var epsWatched = ContentInfo.Episodes.Count(x => x.Watched);
				var runtime = (ContentInfo.Show.EpisodeRunTime?.Any() ?? false) ? ContentInfo.Show.EpisodeRunTime.Average() : 0;
				EpisodeRuntimeLabel.Text = $"{eps} Total Episodes"
					+ (epsWatched > 0 ? $" • {epsWatched} Watched" : null)
					+ TimeSpan.FromMinutes(epsWatched.If(0, eps) * runtime).If(x => x.Ticks == 0, string.Empty, x => $" • {x.ToReadableString()} {epsWatched.If(0, "to watch everything", "spent watching")}")
					+ TimeSpan.FromMinutes(epsWatched.If(0, 0, eps - epsWatched) * runtime).If(x => x.Ticks == 0, string.Empty, x => $" • {x.ToReadableString()} left to finish");
			}

			foreach (var item in ContentInfo.Episodes)
			{
				if (!EpisodesPanel.Controls.OfType<ImageContentControl<Episode>>().Any(x => x.Content == item))
				{
					EpisodesPanel.Controls.Add(new ImageContentControl<Episode>(item));
				}
			}

			if ((ContentInfo.Credits?.Cast?.Any() ?? false) && FeaturesPanel.Controls.Count == 0)
				foreach (var item in ContentInfo.Credits.Cast.Take(5))
					FeaturesPanel.Controls.Add(new ImagePersonControl(item));
			FeaturesLabel.Text = FeaturesPanel.Controls.Count == 0 ? null : $"{(char)0x200B}";

			SocialLinksControl.Homepage = ContentInfo.Show.Homepage;
			SocialLinksControl.Imdb = ContentInfo.Show.ExternalIds?.ImdbId;
			SocialLinksControl.Twitter = ContentInfo.Show.ExternalIds?.TwitterId;
			SocialLinksControl.Facebook = ContentInfo.Show.ExternalIds?.FacebookId;
			SocialLinksControl.Instagram = ContentInfo.Show.ExternalIds?.InstagramId;

			TagsPanel.SetTags(ContentInfo.Rating.Tags);

			if (PosterImage.Image == null && !PosterImage.Loading)
				PosterImage.LoadImage(() => ImageHandler.GetImage(ContentInfo.PosterPath, 300, false, true));

			if (ContentInfo.Show.Networks != null)
				foreach (var item in ContentInfo.Show.Networks)
					NetworksControl.Add(item.Id, item.Name, item.LogoPath);

			NetworksControl.Text = "Network".Plural(ContentInfo.Show.Networks);

			LoadBackground(ContentInfo.BackdropPath);

			this.TryInvoke(Invalidate);
		}

		protected override Bitmap DefaultImage => ProjectImages.Huge_TVPlay;

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				yield return new Banner("SEASON", BannerStyle.Text, ProjectImages.Tiny_Season);

				if (ContentInfo.Episodes.Any(x => !x.Watched && x.AirState == AirStateEnum.Aired))
				{
					if (ContentInfo.Episodes.LastThat(x => x.AirState == AirStateEnum.Aired) != null && (ContentInfo.Episodes.LastThat(x => x.AirState == AirStateEnum.Aired).AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-8))
						yield return new Banner("NEW EPISODE", BannerStyle.Active, ProjectImages.Tiny_New);

					yield return new Banner($"{ContentInfo.Episodes.Count(x => !x.Watched)} Episode".Plural(ContentInfo.Episodes.Count(x => !x.Watched)), BannerStyle.Yellow, ProjectImages.Tiny_Unwatched);
				}

				if (ContentInfo.Rating.Loved)
					yield return new Banner("LOVED", BannerStyle.Red, ProjectImages.Tiny_Love);

				if (ContentInfo.Rating.Rated)
					yield return new Banner(ContentInfo.Rating.Rating.ToString("0.##"), ContentInfo.Rating.Rating.RatingColor(), ProjectImages.Tiny_Rating);

				if (ContentInfo.ContentRating > 0)
					yield return new Banner(ContentInfo.ContentRating.ToString("0.#"), ContentInfo.ContentRating.RatingColor(), ProjectImages.Tiny_Stars);
			}
		}

		protected override IEnumerable<Bitmap> HoverIcons
		{
			get
			{
				if (ShowManager.Show(ContentInfo.Show.Id) == null)
					yield return ProjectImages.Huge_Plus;
				else if (!(ContentInfo.Show.GetCurrentWatchEpisode(ContentInfo.SeasonNumber)?.Watched ?? true))
					yield return ProjectImages.Huge_Play;
				else
				{
					var ep = ContentInfo.Episodes.FirstOrDefault(x => !x.Watched && x.AirState == AirStateEnum.Aired && !x.Playable);

					if (ep != null)
						yield return ProjectImages.Huge_Download;
					else
						yield return ProjectImages.Huge_PlayBack;
				}
			}
		}

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
				else
				{
					var ep = ContentInfo.Show.GetCurrentWatchEpisode(ContentInfo.SeasonNumber);

					if (ep != null && !ep.Watched)
					{ 
						ep.Play(); 
					}
					else
					{
						ep = ContentInfo.Episodes.FirstOrDefault(x => !x.Watched && x.AirState == AirStateEnum.Aired && !x.Playable);

						if (ep != null)
						{
							if (ContentInfo.Episodes.All(x => !x.Watched && x.AirState == AirStateEnum.Aired && !x.Playable))
							{
								Cursor.Current = Cursors.WaitCursor;
								Data.Mainform.PushPanel(null, new PC_Download(ContentInfo));
								Cursor.Current = Cursors.Default;
							}
							else
							{
								Cursor.Current = Cursors.WaitCursor;
								Data.Mainform.PushPanel(null, new PC_Download(ep));
								Cursor.Current = Cursors.Default;
							}
						}
						else
							ContentInfo.RePlay();
					}
				}
			}
			else
				base.OnImageMouseClick(e);
		}

		private Episode getDrawnEpisode(out string reason)
		{
			var ep = ContentInfo.Show.GetCurrentWatchEpisode(ContentInfo.SeasonNumber);

			if (!(ep?.Watched ?? true))
			{
				reason = (ep.Previous?.Watched ?? false) ? "On-Deck Episode" : "First Episode";
				return ep;
			}

			ep = ContentInfo.Episodes.FirstOrDefault(x => !x.Watched && x.AirState == AirStateEnum.Aired);
			if (ep != null)
			{
				reason = ep == ContentInfo.Episodes.First() ? "First Episode" : "Up-Next Episode";
				return ep;
			}

			if (ContentInfo.Episodes.Any())
			{
				reason = ContentInfo.Status == "Aired" ? "First Episode" : "Premiere Episode";
				return ep = ContentInfo.Episodes.FirstOrDefault();
			}

			reason = null;
			return null;
		}

		public override void RemoveTag(string tag)
		{
			ContentInfo.Rating = ContentInfo.Rating.RemoveTag(tag);
			new BackgroundAction(() => ContentInfo.Save(ChangeType.Preferences)).Run();
		}
	}
}