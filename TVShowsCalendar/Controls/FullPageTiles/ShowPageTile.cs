using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public partial class ShowPageTile : FullPageContent<TvShow>
	{
		private Page currentPage;

		public enum Page
		{
			Info,
			Seasons,
			Cast,
			Crew,
			Images,
			Videos,
			Similar
		}

		public Page CurrentPage
		{
			get => currentPage;
			set
			{
				lock (this)
				{
					currentPage = value;
					(PanelContent.GetParentPanel(this) as PC_ShowPage).ViewPage(CurrentPage);
					SideButtons.Foreach(x => { x.Selected = x.PageId == (int)currentPage; x.Invalidate(); });
					SeasonsParentPanel.Visible = value == Page.Seasons;
					MainPanel.Visible = value == Page.Info;
				}
			}
		}

		public override bool DrawInfo => CurrentPage <= Page.Seasons;

		public ShowPageTile()
		{
			InitializeComponent();
		}

		public void SetData(TvShow show)
		{
			ContentInfo = show;

			Title = $"{ContentInfo.Name}" + (ContentInfo.FirstAirDate == null ? null : $" • {ContentInfo.FirstAirDate?.Year}");
			SubTitle = (string.Join(" • ", ContentInfo.Genres.Select(x => x.Name).WhereNotEmpty()) + TimeSpan.FromMinutes((ContentInfo.EpisodeRunTime?.Any() ?? false) ? ContentInfo.EpisodeRunTime.Average() : 0).If(x => x.Ticks == 0, string.Empty, x => $" • {x.ToReadableString()}")).Trim(' ', '•');
			PosterImage.DefaultImage = ContentInfo.HugeIcon;

			OverviewLabel.Text = ContentInfo.Overview;
			StatusLabel.Text = ContentInfo.Status + ContentInfo.Years.IfEmpty(string.Empty, $" ({ContentInfo.Years})");
			TypeLabel.Text = ContentInfo.ShowType;
			DateAddedLabel.Text = ContentInfo.DateAdded != DateTime.MinValue ? ContentInfo.DateAdded.ToReadableString() : null;
			OriginalTitleLabel.Text = ContentInfo.OriginalName == ContentInfo.Name ? null : ContentInfo.OriginalName;

			var ep = LocalShowHandler.GetCurrentWatchEpisode(ContentInfo);
			if (ep != null)
			{
				EpisodeLabel.Title = "On-Deck Episode";
				EpisodeControl.Install(ep);
			}
			else
			{
				ep = ContentInfo.Episodes.LastOrDefault(x => x.AirState == AirStateEnum.Aired);

				if (ep != null)
				{
					if ((ep.Next?.AirState ?? AirStateEnum.Unknown) == AirStateEnum.ToBeAired)
					{
						EpisodeLabel.Title = "Upcoming Episode";
						EpisodeControl.Install(ep.Next);
					}
					else
					{
						EpisodeLabel.Title = "Last Aired Episode";
						EpisodeControl.Install(ep);
					}
				}
			}

			EpisodeLabel.Text = ep == null ? null : Text = $"{(char)0x200B}";
			EpisodeControl.Visible = ep != null;

			foreach (var item in ContentInfo.Seasons)
			{
				if (!SeasonsPanel.Controls.OfType<ImageSeasonControl>().Any(x => x.Content.Equals(item)))
				{
					SeasonsPanel.Controls.Add(new ImageSeasonControl(item));
				}
			}

			SeasonControl.Install(show.Seasons.LastOrDefault(x => x.SeasonNumber > 0 && (x.Episodes?.Any() ?? false) && x.AirDate != null));
			SeasonLabel.Title = SeasonControl.Content?.AirDate < DateTime.Today ? "Last Season" : "Upcoming Season";

			if ((ContentInfo.Cast?.Any() ?? false) && FeaturesPanel.Controls.Count == 0)
				foreach (var item in ContentInfo.Cast.Take(5))
					FeaturesPanel.Controls.Add(new ImagePersonControl(item));
			FeaturesLabel.Text = FeaturesPanel.Controls.Count == 0 ? null : $"{(char)0x200B}";

			var eps = ContentInfo.Episodes.Count();

			if (eps > 0)
			{
				var epsWatched = ContentInfo.Episodes.Count(x => x.Watched);
				var runtime = (ContentInfo.EpisodeRunTime?.Any() ?? false) ? ContentInfo.EpisodeRunTime.Average() : 0;
				EpisodeRuntimeLabel.Text = $"{eps} Total Episodes"
					+ (epsWatched > 0 ? $" • {epsWatched} Watched" : null)
					+ TimeSpan.FromMinutes(epsWatched.If(0, eps) * runtime).If(x => x.Ticks == 0, string.Empty, x => $" • {x.ToReadableString()} {epsWatched.If(0, "to watch everything", "spent watching")}")
					+ TimeSpan.FromMinutes(epsWatched.If(0, 0, eps - epsWatched) * runtime).If(x => x.Ticks == 0, string.Empty, x => $" • {x.ToReadableString()} left to finish");
			}

			if (ContentInfo.Languages?.Any() ?? false)
			{
				LanguageLabel.Title = "Spoken Language".Plural(ContentInfo.Languages);
				LanguageLabel.Text = ContentInfo.Languages.Count == 1 ? ContentInfo.Languages[0]
					: ContentInfo.Languages.Select(x => $"• {x}").ListStrings("\n");
			}

			if (ContentInfo.OriginCountry?.Any() ?? false)
			{
				CountryLabel.Title = "Countr".Plural(ContentInfo.OriginCountry, "ies", "y") + " of Origin";
				CountryLabel.Text = ContentInfo.OriginCountry.Count == 1 ? new RegionInfo(ContentInfo.OriginCountry[0]).EnglishName
					: ContentInfo.OriginCountry.Select(x => $"• {new RegionInfo(x).EnglishName}").ListStrings("\n");
			}

			SocialLinksControl.Homepage = ContentInfo.Homepage;
			SocialLinksControl.Imdb = ContentInfo.ExternalIds?.ImdbId;
			SocialLinksControl.Twitter = ContentInfo.ExternalIds?.TwitterId;
			SocialLinksControl.Facebook = ContentInfo.ExternalIds?.FacebookId;
			SocialLinksControl.Instagram = ContentInfo.ExternalIds?.InstagramId;

			TagsPanel.SetTags(ContentInfo.Rating.Tags);
			KeywordsPanel.SetTags(ContentInfo.Keywords?.Skip(2));

			if (PosterImage.Image == null && !PosterImage.Loading)
			{
				PosterImage.LoadImage(() => ImageHandler.GetImage(ContentInfo.PosterPath, 300, false, true));
			}

			if (ContentInfo.Networks != null)
			{
				foreach (var item in ContentInfo.Networks)
				{
					NetworksControl.Add(item.Id, item.Name, item.LogoPath);
				}
			}

			NetworksControl.Text = "Network".Plural(ContentInfo.Networks);
			TrailerButton.Visible = ContentInfo.Videos?.Any(x => x.Type == "Trailer" && x.Site == "YouTube" && x.Iso_639_1 == "en") ?? false;

			LoadBackground(ContentInfo.BackdropPath);

			this.TryInvoke(Invalidate);
		}

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				yield return new Banner("TV SHOW", BannerStyle.Text, ProjectImages.Tiny_TV);

				if (ContentInfo.Episodes.Any(x => !x.Watched && x.AirState == AirStateEnum.Aired))
				{
					if (ContentInfo.LastEpisode != null && (ContentInfo.LastEpisode.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-8))
					{
						yield return new Banner("NEW EPISODE", BannerStyle.Active, ProjectImages.Tiny_New);
					}

					yield return new Banner($"{ContentInfo.Episodes.Count(x => !x.Watched)} NOT WATCHED", BannerStyle.Yellow, ProjectImages.Tiny_Unwatched);
				}

				if (ContentInfo.Rating.Loved)
				{
					yield return new Banner("LOVED", BannerStyle.Red, ProjectImages.Tiny_Love);
				}

				if (ContentInfo.Rating.Rated)
				{
					yield return new Banner(ContentInfo.Rating.Rating.ToString("0.##"), ContentInfo.Rating.Rating.RatingColor(), ProjectImages.Tiny_Rating);
				}

				if (ContentInfo.ContentRating > 0)
				{
					yield return new Banner(ContentInfo.ContentRating.ToString("0.#"), ContentInfo.ContentRating.RatingColor(), ProjectImages.Tiny_Stars);
				}

				var rating = ContentInfo.VoteAverage;
				var votes = ContentInfo.VoteCount;

				if (votes > 0)
				{
					yield return new Banner(rating.ToString("0.##"), rating.RatingColor(), ProjectImages.Tiny_Star);
				}

				if (ContentInfo.InProduction)
				{
					yield return new Banner("In Production", BannerStyle.Red, ProjectImages.Tiny_VideoCam);
				}
			}
		}

		protected override IEnumerable<Bitmap> HoverIcons
		{
			get
			{
				if (ShowManager.Show(ContentInfo.Id) == null)
				{
					yield return ProjectImages.Huge_Plus;
				}
				else if (!(ContentInfo.GetCurrentWatchEpisode()?.Watched ?? true))
				{
					yield return ProjectImages.Huge_Play;
				}
				else
				{
					var ep = ContentInfo.Episodes.FirstOrDefault(x => !x.Watched && x.AirState == AirStateEnum.Aired && !x.Playable);

					if (ep != null)
					{
						yield return ProjectImages.Huge_Download;
					}
					else
					{
						yield return ProjectImages.Huge_PlayBack;
					}
				}
			}
		}

		protected override Bitmap DefaultImage => ProjectImages.Huge_TV;

		protected override void TitleClicked(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (currentPage == Page.Info)
				{
					Data.Mainform.PushBack();
				}
				else
				{
					CurrentPage = Page.Info;
				}
			}
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
			{
				if (ShowManager.Show(ContentInfo.Id) == null)
				{
					ShowManager.Add(ContentInfo);
				}
				else if (!(ContentInfo.GetCurrentWatchEpisode()?.Watched ?? true))
				{
					ContentInfo.GetCurrentWatchEpisode().Play();
				}
				else
				{
					var ep = ContentInfo.Episodes.FirstOrDefault(x => !x.Watched && x.AirState == AirStateEnum.Aired && !x.Playable);

					if (ep != null)
					{
						if (ep.Season.Episodes.All(x => !x.Watched && x.AirState == AirStateEnum.Aired && !x.Playable))
						{
							Cursor.Current = Cursors.WaitCursor;
							Data.Mainform.PushPanel(null, new PC_Download(ep.Season));
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
					{
						ContentInfo.RePlay();
					}
				}
			}
			else
			{
				base.OnImageMouseClick(e);
			}
		}

		public override void RemoveTag(string tag)
		{
			ContentInfo.Rating = ContentInfo.Rating.RemoveTag(tag);
			new BackgroundAction(() => ContentInfo.Save(ChangeType.Preferences)).Run();
		}
	}
}