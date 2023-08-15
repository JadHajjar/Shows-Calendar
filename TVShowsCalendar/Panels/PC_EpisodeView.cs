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
	public partial class PC_EpisodeView : PanelContent
	{
		public Episode Episode { get; }
		public EpisodePageTile PageTile { get; }

		public PC_EpisodeView(Episode episode)
		{
			InitializeComponent();

			Episode = episode;

			Text = $"{episode.Show.Name} • {episode.Season.Name}";

			StartDataLoad();

			Controls.Clear();
			Controls.Add(PageTile = new EpisodePageTile(this));
			PageTile.SetData(episode);

			Episode.ContentRemoved += episode_ContentRemoved;
			Episode.InfoChanged += episode_InfoChanged;
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

		private void ShowManager_EpisodeRemoved(Episode episode)
		{
			if (episode == Episode)
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

		protected override bool LoadData()
		{
			if (Episode.Images == null && ConnectionHandler.IsConnected)
				Episode.Images = Data.TMDbHandler.GetEpisodeImages(Episode.Show.Id, Episode.SN, Episode.EN).Result;

			if (Episode.Videos == null && ConnectionHandler.IsConnected)
				Episode.Videos = Data.TMDbHandler.GetEpisodeVideos(Episode.Show.Id, Episode.SN, Episode.EN).Result;

			return ConnectionHandler.IsConnected;
		}

		protected override void OnDataLoad()
		{
			if (PageTile != null)
				if (PageTile.CurrentPage == EpisodePageTile.Page.Images || PageTile.CurrentPage == EpisodePageTile.Page.Videos)
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
					if (item != Episode.Show)
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
			var epPan = new PC_EpisodeView(Episode.Next);
			epPan.PageTile.currentPage = PageTile.CurrentPage;
			Form.SetPanel(null, epPan, clearHistory: false);
		}

		public void JumpPrevious()
		{
			var epPan = new PC_EpisodeView(Episode.Previous);
			epPan.PageTile.currentPage = PageTile.CurrentPage;
			Form.SetPanel(null, epPan, clearHistory: false);
		}

		internal void ViewPage(EpisodePageTile.Page page)
		{
			SetData(page);

			switch (page)
			{
				case EpisodePageTile.Page.Info:
					PageTile.SetContent(null);
					break;

				case EpisodePageTile.Page.Cast:
					PageTile.PageName = "Guest Stars";
					PageTile.SetContent(P_Cast);
					break;

				case EpisodePageTile.Page.Crew:
					PageTile.PageName = "Episode's Crew";
					PageTile.SetContent(P_Crew);
					break;

				case EpisodePageTile.Page.Images:
					PageTile.PageName = "Images";
					PageTile.SetContent(FLP_Images);
					break;

				case EpisodePageTile.Page.Videos:
					PageTile.PageName = "Trailers, Teasers & Other Videos";
					PageTile.SetContent(FLP_Videos);
					break;

				case EpisodePageTile.Page.VidFiles:
					PageTile.PageName = "Local Video Files";
					PageTile.SetContent(FLP_VidFiles);
					break;
			}
		}

		private void SetData(EpisodePageTile.Page page)
		{
			switch (page)
			{
				case EpisodePageTile.Page.Cast:
					if (SP_Cast.Controls.Count == 0)
						if (Episode.GuestStars != null)
						{
							foreach (var item in Episode.GuestStars)
							{
								var pb = new CharacterControl(item)
								{
									Margin = new Padding(5),
									Tag = item
								};

								SP_Cast.Controls.Add(pb);
							}

							setSimilarCharacters(Episode.GuestStars);
						}
					break;

				case EpisodePageTile.Page.Crew:
					if (P_Crew.Controls.Count == 0)
					{
						var ind = 100;
						if (Episode.Crew != null)
							foreach (var grp in Episode.Crew.GroupBy(x => x.Department).OrderByDescending(x => x.Key))
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

				case EpisodePageTile.Page.Images:
					if (FLP_Images.Controls.Count == 0)
						if (Episode.Images?.Stills != null)
						{
							foreach (var item in Episode.Images.Stills)
							{
								var width = (int)(265 * UI.UIScale);
								var height = width * item.Height / item.Width;

								var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
								pb.GetImage(item.FilePath, 265);

								FLP_Images.Controls.Add(pb);
							}
						}
					break;

				case EpisodePageTile.Page.Videos:
					if (FLP_Videos.Controls.Count == 0)
						if (Episode.Videos != null)
						{
							foreach (var item in Episode.Videos)
							{
								FLP_Videos.Controls.Add(new YoutubeControl(Episode, item));
							}
						}
					break;

				case EpisodePageTile.Page.VidFiles:
					if (FLP_VidFiles.Controls.Count == 0) foreach (var item in Episode.VidFiles)
						{
							var ctrl = new VideoFileControl<Episode>(item.Info, Episode);

							FLP_VidFiles.Controls.Add(ctrl);
						}
					break;
			}
		}

		public override bool OnWndProc(Message m)
		{
			if (m.Msg == 0x210 && m.WParam == (IntPtr)0x1020b && PageTile.CurrentPage != EpisodePageTile.Page.Info)
			{
				PageTile.CurrentPage = EpisodePageTile.Page.Info;
				return true;
			}

			return base.OnWndProc(m);
		}

		public override bool KeyPressed(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Left && Episode.Previous != null)
			{
				JumpPrevious();
				return true;
			}

			if (keyData == Keys.Right && Episode.Next != null)
			{
				JumpNext();
				return true;
			}

			if (keyData == Keys.Escape && PageTile.CurrentPage != EpisodePageTile.Page.Info)
			{
				PageTile.CurrentPage = EpisodePageTile.Page.Info;
				return true;
			}

			if (keyData == (Keys.Control | Keys.Tab))
			{
				PageTile.CurrentPage = Enum.GetValues(typeof(EpisodePageTile.Page)).Cast<EpisodePageTile.Page>().Next(PageTile.CurrentPage);
				return true;
			}

			if (keyData == (Keys.Control | Keys.Shift | Keys.Tab))
			{
				PageTile.CurrentPage = PageTile.CurrentPage == EpisodePageTile.Page.Info ? EpisodePageTile.Page.VidFiles : Enum.GetValues(typeof(EpisodePageTile.Page)).Cast<EpisodePageTile.Page>().Previous(PageTile.CurrentPage);
				return true;
			}

			return base.KeyPressed(ref msg, keyData);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			if (PageTile.CurrentPage != EpisodePageTile.Page.Info)
				//Form.OnNextIdle(() =>
				{
					PageTile.CurrentPage = PageTile.CurrentPage;
					ViewPage(PageTile.CurrentPage);
				BeginInvoke(new Action(PageTile.Invalidate));
				}//);
		}

		internal void GoToShow()
		{
			var cur = Form.PanelHistory.LastOrDefault(x => x is PC_ShowPage sp && sp.LinkedShow == Episode.Show);

			if (cur != null)
				while (Form.CurrentPanel != cur)
					Form.PushBack();
			else
			{
				cur = new PC_ShowPage(Episode.Show);
				Form.SetPanel(null, cur, clearHistory: false);
			}
		}

		internal void GoToSeason()
		{
			var cur = Form.PanelHistory.LastOrDefault(x => x is PC_SeasonView sp && sp.Season == Episode.Season);

			if (cur != null)
				while (Form.CurrentPanel != cur)
					Form.PushBack();
			else
			{
				cur = new PC_SeasonView(Episode.Season);
				Form.SetPanel(null, cur, clearHistory: false);
			}

			(cur as PC_SeasonView).PageTile.CurrentPage = SeasonPageTile.Page.Episodes;
		}
	}
}