using Extensions;

using ShowsCalendar.Classes;

using ShowsRenamer.Module.Handlers;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

using The_Pirate_Bay;

namespace ShowsCalendar
{
	public partial class PC_Download : PanelContent
	{
		private bool reversed;
		private int runningLoads;
		private PictureBox selectedLabel;
		private PictureBox selectedSort;

		public Episode Episode { get; }
		public Movie Movie { get; }
		public Season Season { get; }

		public PictureBox SelectedSort
		{
			get => selectedSort;
			set
			{
				selectedSort?.Color(FormDesign.Design.IconColor);
				selectedSort = value;
				value?.Color(FormDesign.Design.ActiveColor);
			}
		}

		private PC_Download()
		{
			InitializeComponent();

			SlickTip.SetTo(L_Low, "Only show Low quality torrents");
			SlickTip.SetTo(L_720p, "Only show 720p quality torrents");
			SlickTip.SetTo(L_1080p, "Only show 1080p quality torrents");
			SlickTip.SetTo(L_4K, "Only show 4K UHD quality torrents");
			SlickTip.SetTo(L_3D, "Only show 3D quality torrents");
			SlickTip.SetTo(L_AllDownloads, "Show all torrents");

			SlickTip.SetTo(PB_Label, "Sort by Name");
			SlickTip.SetTo(PB_Res, "Sort by Quality");
			SlickTip.SetTo(PB_Subs, "Sort by Subtitles");
			SlickTip.SetTo(PB_Sound, "Sort by Sound Quality");
			SlickTip.SetTo(PB_Health, "Sort by Torrent Health");
			SlickTip.SetTo(PB_Size, "Sort by Download Size");

			TorrentsTile.QualityFilter = OtherTorrentsTile.QualityFilter = Data.Options.ShowAllDownloads ? QualityFilter.All : (QualityFilter)Data.Options.PrefferedQuality;
			SelectedSort = PB_Health;
			applySort(TorrentSortOption.Health);

			switch (TorrentsTile.QualityFilter)
			{
				case QualityFilter.Low:
					Select(selectedLabel = L_Low);
					break;

				case QualityFilter.p720:
					Select(selectedLabel = L_720p);
					break;

				case QualityFilter.p1080:
					Select(selectedLabel = L_1080p);
					break;

				case QualityFilter.K4:
					Select(selectedLabel = L_4K);
					break;

				case QualityFilter.D3:
					Select(selectedLabel = L_3D);
					break;

				case QualityFilter.All:
					Select(selectedLabel = L_AllDownloads);
					break;
			}
		}

		public PC_Download(Episode episode) : this()
		{
			Episode = episode;
			Text = Episode.Show.Name;
			PB_Image.GetImage(episode.StillPath.IfEmpty(episode.Show.BackdropPath), 160);
		}

		public PC_Download(Season season) : this()
		{
			Season = season;
			Text = Season.Show.Name;
			PB_Image.GetImage(season.PosterPath.IfEmpty(season.Show.PosterPath), 60);
		}

		public PC_Download(Movie mov) : this()
		{
			Movie = mov;
			Text = Movie.Name;
			PB_Image.GetImage(mov.BackdropPath, 160);
		}

		#region Torrent Methods

		private static string GetHttp(string s)
			=> HttpUtility.UrlEncode(s.RemoveDoubleSpaces());

		private IEnumerable<SearchQuery> getQuerySearches()
		{
			if (Movie != null)
			{
				var nameYear = Movie.Name + (Movie.ReleaseDate == null ? string.Empty : $" {Movie.ReleaseDate?.Year}");
				yield return new SearchQuery(GetHttp(nameYear), false);

				var filteredName = string.Concat(Regex.Matches(nameYear, @"[\s\w]+").Cast<Match>().Select(x => x.Value));
				if (nameYear != filteredName)
					yield return new SearchQuery(GetHttp(filteredName), false);

				if (!string.IsNullOrEmpty(Movie.OriginalTitle) && Movie.Name != Movie.OriginalTitle)
				{
					nameYear = Movie.OriginalTitle + (Movie.ReleaseDate == null ? string.Empty : $" {Movie.ReleaseDate?.Year}");
					yield return new SearchQuery(GetHttp(nameYear), false);

					filteredName = string.Concat(Regex.Matches(nameYear, @"[\s\w]+").Cast<Match>().Select(x => x.Value));
					if (nameYear != filteredName)
						yield return new SearchQuery(GetHttp(filteredName), false);
				}
			}
			else if (Episode != null || Season != null)
			{
				var show = Episode?.Show ?? Season?.Show;
				var names = !string.IsNullOrEmpty(show.OriginalName) && show.Name != show.OriginalName ? new[] { show.Name, show.OriginalName } : new[] { show.Name };

				foreach (var showName in names)
				{
					if (Episode != null)
					{
						yield return new SearchQuery(GetHttp($"{showName} S{Episode.SN:00}E{Episode.EN:00}"), false);
						yield return new SearchQuery(GetHttp($"{showName} S{Episode.SN:00} E{Episode.EN:00}"), true);

						var filteredName = string.Concat(Regex.Matches(showName, @"[\s\w]+").Cast<Match>().Select(x => x.Value));
						if (showName != filteredName)
						{
							yield return new SearchQuery(GetHttp($"{filteredName} S{Episode.SN:00}E{Episode.EN:00}"), false);
							yield return new SearchQuery(GetHttp($"{filteredName} S{Episode.SN:00} E{Episode.EN:00}"), true);
						}
					}

					if (Season != null)
					{
						yield return new SearchQuery(GetHttp($"{showName} S{Season.SeasonNumber:00}"), false);
						yield return new SearchQuery(GetHttp($"{showName} Season {Season.SeasonNumber}"), true);
						yield return new SearchQuery(GetHttp($"{showName} Season {Season.SeasonNumber:00}"), true);

						var filteredName = string.Concat(Regex.Matches(showName, @"[\s\w]+").Cast<Match>().Select(x => x.Value));
						if (showName != filteredName)
						{
							yield return new SearchQuery(GetHttp($"{filteredName} S{Season.SeasonNumber:00}"), false);
							yield return new SearchQuery(GetHttp($"{filteredName} Season {Season.SeasonNumber}"), true);
							yield return new SearchQuery(GetHttp($"{filteredName} Season {Season.SeasonNumber:00}"), true);
						}
					}
				}
			}
		}

		private void LoadZooqleTorrents(SearchQuery query)
		{
			runningLoads++;
			var uri = $"https://zooqle.com/search?q={query}+category%3A{(Movie == null ? "TV" : "Movies")}";
			var html = BrowserHandler.GetHTML(uri, wait: false).Result;
			runningLoads--;

			addTorrents(html, Torrent.Origin.Zooqle
				, "<tr( \"=\"\")?.+?22.+?\\d+?\\..+?</tr>");
		}

		private void LoadPirateTorrents(SearchQuery query)
		{
			runningLoads++;
			var uri = new Query(query.Query, 0, TorrentCategory.AllVideo, QueryOrder.BySeeds).TranslateToUrl().Replace("+", "%20");
			var html = BrowserHandler.GetHTML(uri, wait: false).Result.Replace("\r\n", "");
			runningLoads--;

			addTorrents(html, Torrent.Origin.PirateBay
				, "<td.+?<div class=detname><a.+?href=\"(.+?)\".*?>(.+?)</a>.+?<a.+?href=\"(.+?)\".+?src=\".+?magnet\\.gif\".*?>.+?</td><td.+?>(\\d+)</td><td.+?>(\\d+)</td>");
		}

		private void Load1337XTorrents(SearchQuery query)
		{
			if (query.Query.StartsWith("%")) return;

			runningLoads++;
			var uri = $"https://www.1377x.to/category-search/{query.Query.Replace("+", "%20")}/{(Movie == null ? "TV" : "Movies")}/1";
			var html = BrowserHandler.GetHTML(uri, wait: false).Result;
			runningLoads--;

			addTorrents(html, Torrent.Origin.X1337
				, "<tr>.+?<td.+?<a href=\"(/torrent.+?)\">(.+?)</a.+?<td.+?>(.+?)</td>.+?<td.+?>(.+?)</td>.+?<td.+?>.+?</td>.+?<td.+?>(.+?)</td>.+?</tr>");
		}

		private void LoadYTSTorrents(SearchQuery query)
		{
			if (query.SpacedOut)
				return;

			runningLoads++;
			var uri = $"https://eztv.re/search/{query.Query.Replace("+", "-")}";
			var html = BrowserHandler.GetHTML(uri, wait: false).Result;
			runningLoads--;

			addTorrents(html, Torrent.Origin.YTS
				, "<tr .+?<td.+?>.+?</td>.+?<td.+?<a.+?href=\"(/.+?)\".+?>(.+?)</a.+?<td.+?<a.+?href=\"(magnet.+?)\".+?</td>.+?<td.+?>(.+?)</td>.+?<td.+?>.+?</td>.+?<td.+?>(.+?)<.+?</tr>");
		}

		private void LoadRarbgXTorrents(SearchQuery query)
		{
			runningLoads++;
			var uri = $"https://www.rarbggo.to/search/?search={query.Query.Replace("+", "%20")}";
			var html = BrowserHandler.GetHTML(uri, wait: false).Result;
			runningLoads--;

			addTorrents(html, Torrent.Origin.Rarbg
				, "<tr .+?<td.+?>.+?</td>.+?<td.+?<a.+?href=\"(/torrent.+?)\">(.+?)</a.+?<td.+?>.+?</td>.+?<td.+?>.+?</td>.+?<td.+?>(.+?)</td>.+?<td.+?>(.+?)</td>.+?<td.+?>(.+?)</td>.+?</tr>");
		}

		private void LoadNyaaTorrents(SearchQuery query)
		{
			runningLoads++;
			var uri = $"https://nyaa.si/?f=0&c=1_0&q={query.Query}";
			var html = BrowserHandler.GetHTML(uri, wait: false).Result;
			runningLoads--;

			addTorrents(html, Torrent.Origin.NyaaSi
				, "<tr\\s.+?</tr>");
		}

		private void addTorrents(string html, Torrent.Origin origin, string pattern)
		{
			foreach (Match row in Regex.Matches(html.RegexReplace("(\r|\n)", " "), "<tr.+?</tr>", RegexOptions.IgnoreCase))
			{
				var match = Regex.Match(row.Value, pattern, RegexOptions.IgnoreCase);

				if (!match.Success)
					continue;

				var tf = new Torrent(match, origin);
			
				if ((Episode != null && LocalShowHandler.Check(Episode.Show.Name, NameExtractor.GetSeriesName(tf.SortingData.Name), true) <= 1 && LocalShowHandler.Check(Episode.Show.OriginalName, NameExtractor.GetSeriesName(tf.SortingData.Name), true) <= 1)
					|| (Season != null && LocalShowHandler.Check(Season.Show.Name, tf.SortingData.Name, true) < 1 && LocalShowHandler.Check(Season.Show.OriginalName, tf.SortingData.Name, true) < 1)
					|| (Movie != null && !LocalMovieHandler.Match(Movie.Name, tf.SortingData.Name, true) && !LocalMovieHandler.Match(Movie.OriginalTitle, tf.SortingData.Name, true)))
				{
					OtherTorrentsTile.Add(tf);
				}
				else
					TorrentsTile.Add(tf);
			}

			this.TryInvoke(() =>
			{
				if (OtherTorrentsTile.Torrents + TorrentsTile.Torrents != 0)
				{
					PB_Loader.Hide();
					TLP_Icons.Enabled = true;
					foreach (Control item in TLP_QualitySelection.Controls)
						item.Enabled = true;

					TLP_MoreTorrents.Visible = OtherTorrentsTile.Torrents > 0;

					if (runningLoads > 0)
						PB_Loader2.Show();
					else
					{
						PB_Loader2.Hide();

						if (TorrentsTile.Torrents == 0)
						{
							TLP_MoreTorrents.Visible = false;
							OtherTorrentsTile.Visible = true;
						}
					}
				}
				else if (runningLoads == 0)
				{
					PB_Loader2.Hide();
					PB_Loader.Hide();
					L_NoResults.Show();
				}

				OtherTorrentsTile.Invalidate();
				TorrentsTile.Invalidate();
			});
		}

		#endregion Torrent Methods

		#region Form Methods

		protected override void UIChanged()
		{
			base.UIChanged();

			L_NoResults.Font = UI.Font(10.5F);
			TLP_Main.RowStyles[0].Height = (int)(100 * UI.FontScale);
			TLP_Main.RowStyles[2].Height = (int)(30 * UI.FontScale);
			TLP_Main.RowStyles[4].Height = (int)(30 * UI.FontScale);

			TLP_Icons.ColumnStyles[1].Width = TLP_QualitySelection.ColumnStyles[1].Width =
				TLP_Icons.ColumnStyles[2].Width = TLP_Icons.ColumnStyles[3].Width = TLP_Icons.ColumnStyles[4].Width =
				TLP_QualitySelection.ColumnStyles[2].Width = TLP_QualitySelection.ColumnStyles[3].Width = TLP_QualitySelection.ColumnStyles[4].Width
					= (int)(55 * UI.FontScale);

			verticalScroll.Padding = new Padding(0, panel1.Top, 0, 0);
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			P_Spacer_1.BackColor = P_Spacer_2.BackColor = P_Spacer_3.BackColor = P_Spacer_4.BackColor = design.AccentColor;
			OtherTorrentsTile.BackColor = design.AccentBackColor;
			I_MoreInfo.Color(FormDesign.Design.ForeColor);

			if (selectedLabel != null)
				Select(selectedLabel);

			L_NoResults.ForeColor = design.LabelColor;

			L_Low.Color(design.IconColor);
			L_720p.Color(design.IconColor);
			L_1080p.Color(design.IconColor);
			L_4K.Color(design.IconColor);
			L_3D.Color(design.IconColor);
			PB_Label.Color(design.IconColor);
			PB_Subs.Color(design.IconColor);
			PB_Sound.Color(design.IconColor);
			PB_Size.Color(design.IconColor);
			PB_Health.Color(design.IconColor);
			PB_Res.Color(design.IconColor);
			PB_Download.Color(design.IconColor);
			SelectedSort?.Color(design.ActiveColor);
			selectedLabel?.Color(design.ActiveColor);
		}

		private void Deselect(PictureBox label)
		{
			label.Color(FormDesign.Design.IconColor);
			label.Invalidate();
		}

		private void PB_Health_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right || SelectedSort == sender;
			if (SelectedSort == PB_Health && r == reversed)
			{
				SelectedSort = null;
				reversed = false;
				applySort(TorrentSortOption.None);
			}
			else
			{
				reversed = r;
				SelectedSort = PB_Health;
				applySort(TorrentSortOption.Health);
			}
		}

		private void PB_Image_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(PB_Image.BackColor);

			var imgRect = new Rectangle(new Point(10, 5), UI.Scale(new Size(Season == null ? 160 : 60, 90), UI.FontScale));

			if ((PB_Image.Image == null && PB_Image.Loading) || (PB_Image.Image?.Width ?? 0) >= 100)
				e.Graphics.DrawBorderedImage(PB_Image.Image, imgRect);
			else
				e.Graphics.DrawBorderedImage((Movie != null ? Properties.Resources.Huge_Movie : Properties.Resources.Huge_TV).Color(FormDesign.Design.IconColor), imgRect, ImageSizeMode.Center);

			if (PB_Image.Loading)
				PB_Image.DrawLoader(e.Graphics, new Rectangle(imgRect.Center(new Size(32, 32)), new Size(32, 32)));

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			var h = 5;
			var w = imgRect.Width + 15;
			var titles = new[]
			{
				(Episode?.Name ?? Movie?.Name ?? Season?.Name ?? string.Empty) + (Season != null ? $" • {Season.Episodes.Count} Episodes" : string.Empty) + (Movie?.ReleaseDate == null ? string.Empty : $" ({Movie.ReleaseDate?.Year})"),
				Episode != null ? $"Season {Episode.SN} • Episode {Episode.EN}" : Movie?.Tagline ?? string.Empty,
				Episode?.Overview ?? Movie?.Overview ?? Season?.Overview ?? "No overview",
			};

			e.Graphics.DrawString(titles[0], UI.Font(9.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), new Rectangle(w, h, PB_Image.Width - w, UI.Font(9.75F, FontStyle.Bold).Height), new StringFormat { LineAlignment = StringAlignment.Center });

			h += UI.Font(9.75F, FontStyle.Bold).Height;

			if (!string.IsNullOrWhiteSpace(titles[1]))
			{
				e.Graphics.DrawString(titles[1], UI.Font(8.25F), new SolidBrush(FormDesign.Design.LabelColor), w, h);
				h += UI.Font(8.25F).Height;
			}

			e.Graphics.DrawString(titles[2], UI.Font(6.75F), new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(w, h, PB_Image.Width - w - 5, UI.Font(6.75F).GetHeight().ClosestMultipleTo(PB_Image.Height - h - 5)), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });
		}

		private void PB_Label_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right || SelectedSort == sender;
			if (SelectedSort == PB_Label && r == reversed)
			{
				SelectedSort = null;
				reversed = false;
				applySort(TorrentSortOption.None);
			}
			else
			{
				reversed = r;
				SelectedSort = PB_Label;
				applySort(TorrentSortOption.Name);
			}
		}

		private void PB_Label_MouseEnter(object sender, EventArgs e)
		{
			if (sender != SelectedSort)
				(sender as PictureBox).Image = ((sender as PictureBox).Image as Bitmap).Color(FormDesign.Design.LabelColor);
		}

		private void PB_Label_MouseLeave(object sender, EventArgs e)
		{
			if (sender != SelectedSort)
				(sender as PictureBox).Image = ((sender as PictureBox).Image as Bitmap).Color(FormDesign.Design.IconColor);
		}

		private void PB_Res_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right || SelectedSort == sender;
			if (SelectedSort == PB_Res && r == reversed)
			{
				SelectedSort = null;
				reversed = false;
				applySort(TorrentSortOption.None);
			}
			else
			{
				reversed = r;
				SelectedSort = PB_Res;
				applySort(TorrentSortOption.Res);
			}
		}

		private void PB_Size_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right || SelectedSort == sender;
			if (SelectedSort == PB_Size && r == reversed)
			{
				SelectedSort = null;
				reversed = false;
				applySort(TorrentSortOption.None);
			}
			else
			{
				reversed = r;
				SelectedSort = PB_Size;
				applySort(TorrentSortOption.Size);
			}
		}

		private void PB_Sound_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right || SelectedSort == sender;
			if (SelectedSort == PB_Sound && r == reversed)
			{
				SelectedSort = null;
				reversed = false;
				applySort(TorrentSortOption.None);
			}
			else
			{
				reversed = r;
				SelectedSort = PB_Sound;
				applySort(TorrentSortOption.Sound);
			}
		}

		private void PB_Subs_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right || SelectedSort == sender;
			if (SelectedSort == PB_Subs && r == reversed)
			{
				SelectedSort = null;
				reversed = false;
				applySort(TorrentSortOption.None);
			}
			else
			{
				reversed = r;
				SelectedSort = PB_Subs;
				applySort(TorrentSortOption.Subs);
			}
		}

		private void pictureBox1_MouseEnter(object sender, EventArgs e)
		{
			if (sender != selectedLabel && (sender as PictureBox).Tag != null)
				(sender as PictureBox).Image = ((sender as PictureBox).Image as Bitmap).Color(FormDesign.Design.LabelColor);
		}

		private void pictureBox1_MouseLeave(object sender, EventArgs e)
		{
			if (sender != selectedLabel && (sender as PictureBox).Tag != null)
				(sender as PictureBox).Image = ((sender as PictureBox).Image as Bitmap).Color(FormDesign.Design.IconColor);
		}

		private void L_MoreInfo_Click(object sender, EventArgs e)
		{
			I_MoreInfo.Image = !OtherTorrentsTile.Visible ? Properties.Resources.ArrowUp : Properties.Resources.ArrowDown;
			L_MoreInfo.Text = !OtherTorrentsTile.Visible ? "Less Results" : "More Results";
			I_MoreInfo.Color(FormDesign.Design.ActiveColor);
			OtherTorrentsTile.Visible = !OtherTorrentsTile.Visible;
		}

		private void L_MoreInfo_MouseEnter(object sender, EventArgs e)
		{
			L_MoreInfo.ForeColor = FormDesign.Design.ActiveColor;
			I_MoreInfo.Color(FormDesign.Design.ActiveColor);
		}

		private void L_MoreInfo_MouseLeave(object sender, EventArgs e)
		{
			L_MoreInfo.ForeColor = Color.Empty;
			I_MoreInfo.Color(FormDesign.Design.ForeColor);
		}

		private void Select(PictureBox label) => label.Color(FormDesign.Design.ActiveColor);

		private void PC_Download_Load(object sender, EventArgs e)
		{
			var factory = new Factory { ProcessingPower = 4 };

			foreach (var item in getQuerySearches())
			{
				//factory.Run(() => LoadZooqleTorrents(item));
				factory.Run(() => LoadPirateTorrents(item));
				factory.Run(() => Load1337XTorrents(item));
				factory.Run(() => LoadYTSTorrents(item));
				factory.Run(() => LoadRarbgXTorrents(item));
				factory.Run(() => LoadNyaaTorrents(item));
			}
		}

		private void Quality_Click(object sender, EventArgs e)
		{
			if (selectedLabel != null)
				Deselect(selectedLabel);

			if (sender == L_AllDownloads || sender == selectedLabel)
				TorrentsTile.QualityFilter = OtherTorrentsTile.QualityFilter = QualityFilter.All;
			else if (sender == L_Low)
				TorrentsTile.QualityFilter = OtherTorrentsTile.QualityFilter = QualityFilter.Low;
			else if (sender == L_720p)
				TorrentsTile.QualityFilter = OtherTorrentsTile.QualityFilter = QualityFilter.p720;
			else if (sender == L_1080p)
				TorrentsTile.QualityFilter = OtherTorrentsTile.QualityFilter = QualityFilter.p1080;
			else if (sender == L_4K)
				TorrentsTile.QualityFilter = OtherTorrentsTile.QualityFilter = QualityFilter.K4;
			else if (sender == L_3D)
				TorrentsTile.QualityFilter = OtherTorrentsTile.QualityFilter = QualityFilter.D3;

			Select(selectedLabel = TorrentsTile.QualityFilter == QualityFilter.All ? L_AllDownloads : sender as PictureBox);

			OtherTorrentsTile.Invalidate();
			TorrentsTile.Invalidate();
			TLP_MoreTorrents.Visible = OtherTorrentsTile.Torrents > 0 && TorrentsTile.Torrents > 0;
			L_NoResults.Visible = OtherTorrentsTile.Torrents == 0 && TorrentsTile.Torrents == 0;
		}

		private void applySort(TorrentSortOption sortOption)
		{
			TorrentsTile.SortOption = OtherTorrentsTile.SortOption = sortOption;
			TorrentsTile.Reversed = OtherTorrentsTile.Reversed = reversed;

			this.TryInvoke(OtherTorrentsTile.Invalidate);
			this.TryInvoke(TorrentsTile.Invalidate);
		}

		#endregion Form Methods
	}
}