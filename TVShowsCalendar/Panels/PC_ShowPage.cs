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
	public partial class PC_ShowPage : PanelContent
	{
		public readonly TvShow LinkedShow;
		private readonly ShowPageTile PageTile;

		public PC_ShowPage(TvShow linkedShow)
		{
			InitializeComponent();
			LinkedShow = linkedShow;

			PageTile = new ShowPageTile();
			Controls.Clear();
			Controls.Add(PageTile);

			setData();

			LinkedShow.InfoChanged += linkedShow_ShowLoaded;
			LinkedShow.ContentRemoved += linkedMovie_ContentRemoved;
			LocalShowHandler.WatchInfoChanged += LocalShowHandler_EpisodeWatchChanged;
		}

		private void linkedMovie_ContentRemoved(object sender, EventArgs e)
		{
			if (Form.CurrentPanel == this)
				Form.PushBack();
			else if (Form.PanelHistory.Contains(this))
				(Form.PanelHistory as List<PanelContent>).Remove(this);
		}

		private void linkedShow_ShowLoaded(object sender, EventArgs e) => this.TryInvoke(setData);

		private void setData()
		{
			PageTile.SetData(LinkedShow);

			Text = LinkedShow.Name;

			SetData(PageTile.CurrentPage);
		}

		private void setSimilarCharacters(IEnumerable<dynamic> cast)
		{
			TLP_SimilarContent.Controls.Clear();
			TLP_SimilarContent.RowStyles.Clear();

			var showsDic = new Dictionary<TvShow, List<dynamic>>();
			var moviesDic = new Dictionary<Movie, List<dynamic>>();

			var ind = 0;

			foreach (var c in cast)
			{
				foreach (var item in ShowManager.Shows)
				{
					if (item != LinkedShow)
					{
						var ct = item.Seasons.FirstOrDefault(y => y.Credits != null && y.Credits.Cast.Any(z => z.Id == c.Id))?.Credits.Cast.FirstOrDefault(z => z.Id == c.Id)
							?? item.Episodes.FirstOrDefault(y => y.GuestStars.Any(z => z.Id == c.Id))?.GuestStars.FirstOrDefault(z => z.Id == c.Id);

						if (ct != null)
						{
							if (showsDic.ContainsKey(item))
								showsDic[item].Add(ct);
							else
								showsDic.Add(item, new List<dynamic>() { ct });
						}
					}
				}

				foreach (var item in MovieManager.Movies)
				{
					//if (item != LinkedMovie)
					{
						var ct = item.Cast.FirstOrDefault(z => z.Id == c.Id);

						if (ct != null)
						{
							if (moviesDic.ContainsKey(item))
								moviesDic[item].Add(ct);
							else
								moviesDic.Add(item, new List<dynamic>() { ct });
						}
					}
				}
			}

			TLP_SimilarContent.Controls.Clear();

			if (showsDic.Count + moviesDic.Count > 0)
			{
				foreach (var item in showsDic)
				{
					TLP_SimilarContent.RowStyles.Add(new RowStyle());

					var flp = new FlowLayoutPanel()
					{
						Dock = DockStyle.Top,
						AutoSize = true,
						AutoSizeMode = AutoSizeMode.GrowOnly
					};

					foreach (var c in item.Value)
						flp.Controls.Add(new CharacterControl(c));

					TLP_SimilarContent.Controls.Add(new ContentControl<TvShow>(item.Key), 0, ind);
					TLP_SimilarContent.Controls.Add(flp, 1, ind);

					ind++;
				}

				foreach (var item in moviesDic)
				{
					TLP_SimilarContent.RowStyles.Add(new RowStyle());

					var flp = new FlowLayoutPanel()
					{
						Dock = DockStyle.Top,
						AutoSize = true,
						AutoSizeMode = AutoSizeMode.GrowOnly
					};

					foreach (var c in item.Value)
						flp.Controls.Add(new CharacterControl(c));

					TLP_SimilarContent.Controls.Add(new ContentControl<Movie>(item.Key), 0, ind);
					TLP_SimilarContent.Controls.Add(flp, 1, ind);

					ind++;
				}
			}

			TLP_SimilarContent.Visible = SP_Similar.Visible = TLP_SimilarContent.Controls.Count > 0;
		}

		private void LocalShowHandler_EpisodeWatchChanged(object sender, TvShow show)
		{
			if ((show == null || show == LinkedShow) && this.IsVisible())
			{
				foreach (Control item in FLP_Seasons.Controls)
					item.TryInvoke(item.Invalidate);
			}
		}

		protected override void UIChanged()
		{
			base.UIChanged();

			Padding = new Padding(0, 0, 0, 0);
		}

		public PC_ShowPage(Episode episode) : this(episode.Show)
			=> Load += (s, e) =>
			{
				SetSeason(episode.Season);
				Form.PushPanel(null, new PC_EpisodeView(episode));
			};

		public PC_ShowPage(Season season, Episode episode = null) : this(season.Show)
			=> Load += (s, e) =>
			{
				SetSeason(season, episode);
			};

		private void SetSeason(Season season, Episode episode = null) => Form.PushPanel(null, new PC_SeasonView(season, episode));

		internal void ViewPage(ShowPageTile.Page page)
		{
			SetData(page);

			switch (page)
			{
				case ShowPageTile.Page.Info:
					PageTile.SetContent(null);
					break;

				case ShowPageTile.Page.Seasons:
					PageTile.PageName = "Seasons";
					PageTile.SetContent(null);
					break;

				case ShowPageTile.Page.Cast:
					PageTile.PageName = "Cast";
					PageTile.SetContent(FLP_Cast);
					break;

				case ShowPageTile.Page.Crew:
					PageTile.PageName = "Crew";
					PageTile.SetContent(FLP_Crew);
					break;

				case ShowPageTile.Page.Images:
					PageTile.PageName = "Images";
					PageTile.SetContent(FLP_Images);
					break;

				case ShowPageTile.Page.Videos:
					PageTile.PageName = "Trailers, Teasers & Other Videos";
					PageTile.SetContent(FLP_Videos);
					break;

				case ShowPageTile.Page.Similar:
					PageTile.PageName = "Similar Tv Shows";
					PageTile.SetContent(FLP_Similar);
					break;
			}
		}

		private void SetData(ShowPageTile.Page page)
		{
			switch (page)
			{
				case ShowPageTile.Page.Seasons:
					//if (FLP_Seasons.Controls.Count == 0)
					//	foreach (var item in LinkedShow.Seasons.OrderBy(x => x.SeasonNumber))
					//		FLP_Seasons.Controls.Add(new SeasonTile(item));
					break;

				case ShowPageTile.Page.Cast:
					if (SP_Cast.Controls.Count == 0)
						if (LinkedShow.Cast != null)
						{
							foreach (var item in LinkedShow.Cast)
								SP_Cast.Controls.Add(new CharacterControl(item));

							setSimilarCharacters(LinkedShow.Cast);
						}
					break;

				case ShowPageTile.Page.Crew:
					if (FLP_Crew.Controls.Count == 0)
					{
						if (LinkedShow.CreatedBy != null)
						{
							var ssp = new SlickSectionPanel()
							{
								Text = "Creator".Plural(LinkedShow.CreatedBy),
								AutoHide = true,
								Dock = DockStyle.Top,
								AutoSize = true,
								TabIndex = 101,
								Icon = ProjectImages.Big_Crew
							};

							ssp.Add(LinkedShow.CreatedBy.Select(x => new CharacterControl(x)));
							FLP_Crew.Controls.Add(ssp);
						}

						if (LinkedShow.Crew != null)
						{
							var ind = 100;
							foreach (var grp in LinkedShow.Crew.GroupBy(x => x.Department).OrderByDescending(x => x.Key))
							{
								var ssp = new SlickSectionPanel()
								{
									Text = string.IsNullOrWhiteSpace(grp.Key) ? "Unknown" : grp.Key,
									AutoHide = true,
									Dock = DockStyle.Top,
									AutoSize = true,
									TabIndex = ind--,
									Icon = ProjectImages.Big_Crew
								};

								ssp.Add(grp.Select(x => new CharacterControl(x)));
								FLP_Crew.Controls.Add(ssp);
							}
						}
					}
					break;

				case ShowPageTile.Page.Images:
					if (SP_Backdrops.Controls.Count == 0)
						if (LinkedShow.Backdrops != null)
						{
							foreach (var item in LinkedShow.Backdrops)
							{
								var width = (int)(265 * UI.UIScale);
								var height = width * item.Height / item.Width;

								var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
								pb.GetImage(item.FilePath, 265);

								SP_Backdrops.Add(pb);
							}
						}

					if (SP_Posters.Controls.Count == 0)
						if (LinkedShow.Posters != null)
						{
							foreach (var item in LinkedShow.Posters)
							{
								var width = (int)(195 * UI.UIScale);
								var height = width * item.Height / item.Width;

								var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
								pb.GetImage(item.FilePath, 195);

								SP_Posters.Add(pb);
							}
						}
					break;

				case ShowPageTile.Page.Videos:
					if (FLP_Videos.Controls.Count == 0)
					{
						var ind = 100;
						foreach (var grp in LinkedShow.Videos.GroupBy(x => x.Type).OrderByDescending(x => getOrder(x.Key)))
						{
							var ssp = new SlickSectionPanel()
							{
								Text = grp.Key,
								AutoHide = true,
								Dock = DockStyle.Top,
								AutoSize = true,
								TabIndex = ind--,
								Icon = ProjectImages.Big_Play
							};

							switch (grp.Key)
							{
								case "Trailer": ssp.Icon = ProjectImages.Big_Trailer; ssp.Active = true; break;
								case "Behind the Scenes": ssp.Icon = ProjectImages.Big_BehindScenes; break;
								case "Clip": ssp.Icon = ProjectImages.Big_Clip; break;
								case "Featurette": ssp.Icon = ProjectImages.Big_Featurette; break;
								case "Bloopers": ssp.Icon = ProjectImages.Big_Blooper; break;
							}

							ssp.Add(grp.Select(x => new YoutubeControl(LinkedShow, x)));

							FLP_Videos.Controls.Add(ssp);
						}

						int getOrder(string key)
						{
							switch (key)
							{
								case "Trailer": return 0;
								case "Teaser": return 1;
								case "Clip": return 2;
								case "Behind the Scenes": return 3;
								case "Featurette": return 4;
								case "Bloopers": return 5;
							}
							return 6;
						}
					}
					break;

				case ShowPageTile.Page.Similar:
					if (FLP_Similar.Controls.Count == 0)
						if (LinkedShow.SimilarShows != null)
						{
							foreach (var item in LinkedShow.SimilarShows)
								FLP_Similar.Controls.Add(new MediaViewer(item) { Margin = new Padding(15, 10, 15, 10) });
						}
					break;
			}
		}

		public override bool OnWndProc(Message m)
		{
			if (m.Msg == 0x210 && m.WParam == (IntPtr)0x1020b && PageTile.CurrentPage != ShowPageTile.Page.Info)
			{
				PageTile.CurrentPage = ShowPageTile.Page.Info;
				return true;
			}

			return base.OnWndProc(m);
		}

		public override bool KeyPressed(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape && PageTile.CurrentPage != ShowPageTile.Page.Info)
			{
				PageTile.CurrentPage = ShowPageTile.Page.Info;
				return true;
			}

			if (keyData == (Keys.Control | Keys.Tab))
			{
				PageTile.CurrentPage = Enum.GetValues(typeof(ShowPageTile.Page)).Cast<ShowPageTile.Page>().Next(PageTile.CurrentPage);
				return true;
			}

			if (keyData == (Keys.Control | Keys.Shift | Keys.Tab))
			{
				PageTile.CurrentPage = PageTile.CurrentPage == ShowPageTile.Page.Info ? ShowPageTile.Page.Similar : Enum.GetValues(typeof(ShowPageTile.Page)).Cast<ShowPageTile.Page>().Previous(PageTile.CurrentPage);
				return true;
			}

			return base.KeyPressed(ref msg, keyData);
		}
	}
}