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
	public partial class PC_SeasonView : PanelContent
	{
		public Episode EpisodeToFocus { get; }
		public Season Season { get; }
		internal readonly SeasonPageTile PageTile;

		public PC_SeasonView(Season season, Episode episode = null)
		{
			InitializeComponent();

			Season = season;
			EpisodeToFocus = episode;
			Text = season.Show.Name;

			Controls.Clear();
			Controls.Add(PageTile = new SeasonPageTile(this));
			PageTile.SetData(season);

			if (EpisodeToFocus != null)
				PageTile.currentPage = SeasonPageTile.Page.Episodes;

			Season.ContentRemoved += episode_ContentRemoved;
			Season.InfoChanged += episode_InfoChanged;
		}

		private void episode_InfoChanged(object sender, EventArgs e)
		{
			this.TryInvoke(PageTile.Invalidate);
		}

		private void episode_ContentRemoved(object sender, EventArgs e)
		{
			if (Form.CurrentPanel == this)
				Form.PushBack();
			else if (Form.PanelHistory.Contains(this))
				(Form.PanelHistory as List<PanelContent>).Remove(this);
		}

		internal void ViewPage(SeasonPageTile.Page page)
		{
			SetData(page);

			switch (page)
			{
				case SeasonPageTile.Page.Info:
					PageTile.SetContent(null);
					break;

				case SeasonPageTile.Page.Episodes:
					PageTile.PageName = $"Episodes • {Season.Episodes.Count}";
					PageTile.SetContent(null);
					break;

				case SeasonPageTile.Page.Cast:
					PageTile.PageName = "Season Cast";
					PageTile.SetContent(P_Cast);
					break;

				case SeasonPageTile.Page.Crew:
					PageTile.PageName = "Crew";
					PageTile.SetContent(P_Crew);
					break;

				case SeasonPageTile.Page.Images:
					PageTile.PageName = "Images";
					PageTile.SetContent(FLP_Images);
					break;

				case SeasonPageTile.Page.Videos:
					PageTile.PageName = "Trailers, Teasers & Other Videos";
					PageTile.SetContent(FLP_Videos);
					break;
			}
		}

		private void SetData(SeasonPageTile.Page page)
		{
			switch (page)
			{
				case SeasonPageTile.Page.Info:
					break;

				case SeasonPageTile.Page.Episodes:
					//if (FLP_Episodes.Controls.Count == 0)
					//	foreach (var item in Season.Episodes.OrderBy(x => x.EN))
					//		FLP_Episodes.Controls.Add(new ContentControl<Episode>(item, false));
					break;

				case SeasonPageTile.Page.Cast:
					if (SP_Cast.Controls.Count == 0)
						if (Season.Credits?.Cast != null)
						{
							foreach (var item in Season.Credits.Cast)
								SP_Cast.Add(new CharacterControl(item));

							setSimilarCharacters(Season.Credits.Cast);
						}
					break;

				case SeasonPageTile.Page.Crew:
					if (P_Crew.Controls.Count == 0)
					{
						var ind = 100;
						if (Season.Credits?.Crew != null)
							foreach (var grp in Season.Credits.Crew.GroupBy(x => x.Department).OrderByDescending(x => x.Key))
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
								P_Crew.Controls.Add(ssp);
							}
					}
					break;

				case SeasonPageTile.Page.Images:
					if (FLP_Videos.Controls.Count == 0)
						if (Season.Images?.Posters != null)
						{
							foreach (var item in Season.Images.Posters)
							{
								var width = (int)(200 * UI.UIScale);
								var height = width * item.Height / item.Width;

								var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
								pb.GetImage(item.FilePath, 200);

								FLP_Images.Controls.Add(pb);
							}
						}
					break;

				case SeasonPageTile.Page.Videos:
					if (FLP_Episodes.Controls.Count == 0)
						if (FLP_Videos.Controls.Count == 0)
							if (Season.Videos != null)
							{
								foreach (var item in Season.Videos.Where(x => x.Site == "YouTube" && x.Iso_639_1 == "en").OrderBy(x => x.Id))
									FLP_Videos.Controls.Add(new YoutubeControl(Season, item));
							}
					break;
			}
		}

		public override bool OnWndProc(Message m)
		{
			if (m.Msg == 0x210 && m.WParam == (IntPtr)0x1020b && PageTile.CurrentPage != SeasonPageTile.Page.Info)
			{
				PageTile.CurrentPage = SeasonPageTile.Page.Info;
				return true;
			}

			return base.OnWndProc(m);
		}

		public override bool KeyPressed(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Left && Season.Previous != null)
			{
				JumpPrevious();
				return true;
			}

			if (keyData == Keys.Right && Season.Next != null)
			{
				JumpNext();
				return true;
			}

			if (keyData == Keys.Escape && PageTile.CurrentPage != SeasonPageTile.Page.Info)
			{
				PageTile.CurrentPage = SeasonPageTile.Page.Info;
				return true;
			}

			if (keyData == (Keys.Control | Keys.Tab))
			{
				PageTile.CurrentPage = Enum.GetValues(typeof(SeasonPageTile.Page)).Cast<SeasonPageTile.Page>().Next(PageTile.CurrentPage);
				return true;
			}

			if (keyData == (Keys.Control | Keys.Shift | Keys.Tab))
			{
				PageTile.CurrentPage = PageTile.CurrentPage == SeasonPageTile.Page.Info ? SeasonPageTile.Page.Videos : Enum.GetValues(typeof(SeasonPageTile.Page)).Cast<SeasonPageTile.Page>().Previous(PageTile.CurrentPage);
				return true;
			}

			return base.KeyPressed(ref msg, keyData);
		}

		private void ShowManager_SeasonRemoved(Season season)
		{
			if (season == Season)
			{
				this.TryInvoke(() =>
				{
					if (Form.CurrentPanel == this)
						Form.PushBack();
					else
						Dispose();
				});
			}
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
					if (item != Season.Show)
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

		public void JumpNext()
		{
			var sePan = new PC_SeasonView(Season.Next);
			sePan.PageTile.currentPage = PageTile.CurrentPage;
			Form.SetPanel(null, sePan, clearHistory: false);
		}

		public void JumpPrevious()
		{
			var sePan = new PC_SeasonView(Season.Previous);
			sePan.PageTile.currentPage = PageTile.CurrentPage;
			Form.SetPanel(null, sePan, clearHistory: false);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			if (PageTile.CurrentPage == SeasonPageTile.Page.Episodes)
				PageTile.CurrentPage = PageTile.CurrentPage;
			else if (PageTile.CurrentPage != SeasonPageTile.Page.Info)
			{
				//Form.OnNextIdle(() =>
				{
					PageTile.CurrentPage = PageTile.CurrentPage;
					ViewPage(PageTile.CurrentPage);

					//if (EpisodeToFocus != null)
					//{
					//	var tile = FLP_Episodes.Controls.OfType<ContentControl<Episode>>().FirstOrDefault(x => x.Content == EpisodeToFocus);
					//	if (tile != null)
					//	{
					//		SlickScroll.GlobalScrollTo(tile);
					//		BeginInvoke(new BackgroundAction(() => tile.Focus()));
					//	}
					//}
				}//);
			}
		}
	}
}