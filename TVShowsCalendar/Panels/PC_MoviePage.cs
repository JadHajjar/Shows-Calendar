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
	public partial class PC_MoviePage : PanelContent
	{
		public readonly Movie LinkedMovie;
		internal readonly MoviePageTile PageTile;

		public PC_MoviePage(Movie linkedMovie) : base(true)
		{
			InitializeComponent();
			LinkedMovie = linkedMovie;

			PageTile = new MoviePageTile();
			Controls.Clear();
			Controls.Add(PageTile);

			LinkedMovie.InfoChanged += linkedMovie_MovieLoaded;
			LinkedMovie.ContentRemoved += linkedMovie_ContentRemoved;

			setData();
		}

		private void linkedMovie_ContentRemoved(object sender, EventArgs e)
		{
			if (Form.CurrentPanel == this)
				Form.PushBack();
			else if (Form.PanelHistory.Contains(this))
				(Form.PanelHistory as List<PanelContent>).Remove(this);
		}

		private void linkedMovie_MovieLoaded(object sender, EventArgs e) => this.TryInvoke(setData);

		private void setData()
		{
			PageTile.SetData(LinkedMovie);

			Text = $"{LinkedMovie.Name}" + (LinkedMovie.ReleaseDate == null ? null : $" • {LinkedMovie.ReleaseDate?.Year}");

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
					//if (item != LinkedShow)
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
					if (item != LinkedMovie)
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
			TLP_SimilarContent.RowStyles.Insert(0, new RowStyle());
			TLP_SimilarContent.Controls.Add(SP_Similar, 0, 0);
			TLP_SimilarContent.SetColumnSpan(SP_Similar, TLP_SimilarContent.ColumnCount);
			SP_Cast.BringToFront();
		}

		internal void ViewPage(MoviePageTile.Page currentPage)
		{
			SetData(currentPage);
			switch (currentPage)
			{
				case MoviePageTile.Page.Info:
					PageTile.SetContent(null);
					break;

				case MoviePageTile.Page.Cast:
					PageTile.PageName = "Cast";
					PageTile.SetContent(FLP_Cast);
					PC_MoviePage_Resize(null, null);
					break;

				case MoviePageTile.Page.Crew:
					PageTile.PageName = "Crew";
					PageTile.SetContent(FLP_Crew);
					break;

				case MoviePageTile.Page.Images:
					PageTile.PageName = "Images";
					PageTile.SetContent(FLP_Images);
					break;

				case MoviePageTile.Page.Videos:
					PageTile.PageName = "Trailers, Teasers & Other Videos";
					PageTile.SetContent(FLP_Videos);
					break;

				case MoviePageTile.Page.Similar:
					PageTile.PageName = "Similar Tv Shows";
					PageTile.SetContent(FLP_Similar);
					break;

				case MoviePageTile.Page.VidFiles:
					PageTile.PageName = "Local Video Files";
					PageTile.SetContent(FLP_Files);
					break;
			}
		}

		private void SetData(MoviePageTile.Page page)
		{
			switch (page)
			{
				case MoviePageTile.Page.Cast:
					if (SP_Cast.Controls.Count == 0)
						if (LinkedMovie.Cast != null)
						{
							SP_Cast.Controls.Clear();
							foreach (var item in LinkedMovie.Cast)
								SP_Cast.Controls.Add(new CharacterControl(item));

							setSimilarCharacters(LinkedMovie.Cast);
						}
					break;

				case MoviePageTile.Page.Crew:
					if (FLP_Crew.Controls.Count == 0)
						if (LinkedMovie.Crew != null)
						{
							FLP_Crew.Controls.Clear();
							var ind = 100;
							foreach (var grp in LinkedMovie.Crew.GroupBy(x => x.Department).OrderByDescending(x => x.Key))
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
					break;

				case MoviePageTile.Page.Images:
					if (SP_Backdrops.Controls.Count == 0)
						if (LinkedMovie.Images?.Backdrops != null)
						{
							foreach (var item in LinkedMovie.Images.Backdrops.Where(x => x.Iso_639_1 == null || x.Iso_639_1 == "en"))
							{
								var width = (int)(265 * UI.UIScale);
								var height = width * item.Height / item.Width;

								var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
								pb.GetImage(item.FilePath, 265);

								SP_Backdrops.Add(pb);
							}
						}

					if (SP_Posters.Controls.Count == 0)
						if (LinkedMovie.Images?.Posters != null)
						{
							foreach (var item in LinkedMovie.Images.Posters.Where(x => x.Iso_639_1 == null || x.Iso_639_1 == "en"))
							{
								var width = (int)(195 * UI.UIScale);
								var height = width * item.Height / item.Width;

								var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
								pb.GetImage(item.FilePath, 195);

								SP_Posters.Add(pb);
							}
						}
					break;

				case MoviePageTile.Page.Videos:
					if (FLP_Videos.Controls.Count == 0)
					{
						var ind = 100;
						foreach (var grp in LinkedMovie.Videos.GroupBy(x => x.Type).OrderByDescending(x => getOrder(x.Key)))
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

							ssp.Add(grp.Select(x => new YoutubeControl(LinkedMovie, x)));

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

				case MoviePageTile.Page.Similar:
					if (FLP_Similar.Controls.Count == 0)
						if (LinkedMovie.SimilarMovies != null)
						{
							FLP_Similar.Controls.Clear();
							foreach (var item in LinkedMovie.SimilarMovies)
								FLP_Similar.Controls.Add(new MediaViewer(item) { Margin = new Padding(15, 10, 15, 10) });
						}
					break;

				case MoviePageTile.Page.VidFiles:
					FLP_Files.Controls.Clear();
					foreach (var item in LinkedMovie.VidFiles)
						FLP_Files.Controls.Add(new VideoFileControl<Movie>(item.Info, LinkedMovie));
					break;
			}
		}

		public override bool OnWndProc(Message m)
		{
			if (m.Msg == 0x210 && m.WParam == (IntPtr)0x1020b && PageTile.CurrentPage != MoviePageTile.Page.Info)
			{
				PageTile.CurrentPage = MoviePageTile.Page.Info;
				return true;
			}

			return base.OnWndProc(m);
		}

		public override bool KeyPressed(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape && PageTile.CurrentPage != MoviePageTile.Page.Info)
			{
				PageTile.CurrentPage = MoviePageTile.Page.Info;
				return true;
			}

			if (keyData == (Keys.Control | Keys.Tab))
			{
				PageTile.CurrentPage = Enum.GetValues(typeof(MoviePageTile.Page)).Cast<MoviePageTile.Page>().Next(PageTile.CurrentPage);
				return true;
			}

			if (keyData == (Keys.Control | Keys.Shift | Keys.Tab))
			{
				PageTile.CurrentPage = PageTile.CurrentPage == MoviePageTile.Page.Info ? MoviePageTile.Page.Similar : Enum.GetValues(typeof(MoviePageTile.Page)).Cast<MoviePageTile.Page>().Previous(PageTile.CurrentPage);
				return true;
			}

			return base.KeyPressed(ref msg, keyData);
		}

		private void PC_MoviePage_Resize(object sender, EventArgs e)
		{
			SP_Similar.MaximumSize = new Size(PageTile.ContentPanel.Width, int.MaxValue);
			SP_Similar.MinimumSize = new Size(PageTile.ContentPanel.Width, 0);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			if (PageTile.CurrentPage != MoviePageTile.Page.Info)
				//Form.OnNextIdle(() =>
				{
					PageTile.CurrentPage = PageTile.CurrentPage;
					ViewPage(PageTile.CurrentPage);
				}//);
		}
	}
}