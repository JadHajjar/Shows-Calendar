using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.Forms;
using TVShowsCalendar.Panels;
using TVShowsCalendar.Properties;

namespace TVShowsCalendar
{
	public partial class DownloadForm : BaseForm
	{
		public Episode Episode;
		private EpisodeTile EpTile = null;
		private DisableIdentifier QualityChangeIdentifier = new DisableIdentifier();

		private bool Reversed;

		private Label selectedLabel;

		private PictureBox SelectedSort;

		public DownloadForm(Episode episode)
		{
			InitializeComponent();
			Episode = episode;
			Text = episode.Show.Name;
			//EpTile = new EpisodeTile(episode, null, true) { Margin = new Padding(8), Dock = DockStyle.Fill };
			TLP_Episode.Controls.Add(EpTile);
			InitializeQuality();

			verticalScroll.LinkedControl = TLP_Torrents;

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
			SlickTip.SetTo(PB_Size, "Sort by Download Size");
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			TLP_Episode.BackColor = TLP_Icons.BackColor = TLP_QualitySelection.BackColor = design.MenuColor;
			L_SelectedQuality.ForeColor = design.LabelColor;

			if (selectedLabel != null)
				Select(selectedLabel);

			L_NoResults.ForeColor = design.LabelColor;

			PB_Loader.Image = FormDesign.Loader;

			if (!L_Low.Font.Bold)
				L_Low.ForeColor = design.LightColor;
			else
				L_Low.ForeColor = Color.Empty;

			if (!L_720p.Font.Bold)
				L_720p.ForeColor = design.LightColor;
			else
				L_720p.ForeColor = Color.Empty;

			if (!L_1080p.Font.Bold)
				L_1080p.ForeColor = design.LightColor;
			else
				L_1080p.ForeColor = Color.Empty;

			if (!L_4K.Font.Bold)
				L_4K.ForeColor = design.LightColor;
			else
				L_4K.ForeColor = Color.Empty;

			if (!L_3D.Font.Bold)
				L_3D.ForeColor = design.LightColor;
			else
				L_3D.ForeColor = Color.Empty;

			PB_Label.Color(design.IconColor);
			PB_Subs.Color(design.IconColor);
			PB_Sound.Color(design.IconColor);
			PB_Size.Color(design.IconColor);
			PB_Res.Color(design.IconColor);
			PB_Download.Color(design.IconColor);
			if (SelectedSort != null)
				SelectedSort.Color(design.ActiveColor);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			FormDesign.DesignChanged -= DesignChanged;
			base.OnClosing(e);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
		}

		private void B_Max_Click(object sender, EventArgs e)
		{
			WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
		}

		private void B_Min_Click(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
		}

		private void Deselect(Label label)
		{
			label.ForeColor = Color.Empty;
			label.BackColor = Color.Empty;
		}

		private void InitializeQuality()
		{
			new Action(async () =>
			{
				try
				{
					var html = await ShowAnalyzer.GetHTML(Episode.ZooqleURL + "?tg=");
					html = Regex.Match(html, "Quality:.+?</ul>").Value;
					Invoke(new Action(() =>
					{
						foreach (var item in Regex.Matches(html, "<a.+?>(.+?)(?:&nbsp;)?(<span.+?>(\\d+)</span>)?</a>"))
						{
							if ((item as Match).Groups[2].Value != "")
							{
								switch ((item as Match).Groups[1].Value)
								{
									case "Low": L_Low.Font = new Font(L_Low.Font, FontStyle.Bold); break;
									case "Std": L_Low.Font = new Font(L_Low.Font, FontStyle.Bold); break;
									case "Med": L_Low.Font = new Font(L_Low.Font, FontStyle.Bold); break;
									case "720p": L_720p.Font = new Font(L_720p.Font, FontStyle.Bold); break;
									case "1080p": L_1080p.Font = new Font(L_1080p.Font, FontStyle.Bold); break;
									case "Ultra": L_4K.Font = new Font(L_4K.Font, FontStyle.Bold); break;
									case "3D": L_3D.Font = new Font(L_3D.Font, FontStyle.Bold); break;
									default:
										break;
								}
							}
						}

						if (L_Low.Font.Bold)
							L_Low.Click += Quality_Click;
						else
							L_Low.Cursor = Cursors.Default;

						if (L_720p.Font.Bold)
							L_720p.Click += Quality_Click;
						else
							L_720p.Cursor = Cursors.Default;

						if (L_1080p.Font.Bold)
							L_1080p.Click += Quality_Click;
						else
							L_1080p.Cursor = Cursors.Default;

						if (L_4K.Font.Bold)
							L_4K.Click += Quality_Click;
						else
							L_4K.Cursor = Cursors.Default;

						if (L_3D.Font.Bold)
							L_3D.Click += Quality_Click;
						else
							L_3D.Cursor = Cursors.Default;

						if (!Data.Options.ShowAllDownloads && (TLP_QualitySelection.Controls.Where(x => GeneralMethods.QualityDicTxt2Id.ContainsKey(x.Text)
							 && GeneralMethods.QualityDicTxt2Id[x.Text] == Data.Options.PrefferedQuality).FirstOrDefault() as Label).Font.Bold)
						{
							LoadTorrents(new List<string> { Episode.ZooqleURL + "?tg=" + Data.Options.PrefferedQuality });
							Select(selectedLabel = (TLP_QualitySelection.Controls.Where(x => GeneralMethods.QualityDicTxt2Id.ContainsKey(x.Text)
								&& GeneralMethods.QualityDicTxt2Id[x.Text] == Data.Options.PrefferedQuality).FirstOrDefault() as Label));
						}
						else
						{
							LoadTorrents(new List<string> { Episode.ZooqleURL });
							Select(L_AllDownloads);
							selectedLabel = L_AllDownloads;
						}
					}));
				}
				catch { }
			}).RunInBackground();
		}

		private void L_Title_Click(object sender, EventArgs e)
		{
			if ((e as MouseEventArgs).Clicks == 2)
				B_Max_Click(sender, e);
		}

		private void LoadTorrents(List<string> urls)
		{
			PB_Loader.Show();
			L_NoResults.Hide();
			var SeedOrder = 0;
			var torrentsAvailable = false;
			foreach (var URL in urls)
			{
				new Action(async () =>
				{
					var html = await ShowAnalyzer.GetHTML(URL);
					try
					{
						Invoke(new Action(() =>
						{
							urls.Remove(URL);
							TLP_Torrents.SuspendDrawing();
							foreach (var item in Regex.Matches(html, @"<tr>.+?22.+?\d+?\..+?</tr>"))
							{
								var tf = new TorrentTile(item as Match, SeedOrder) { Dock = DockStyle.Fill };
								try
								{
									if (!tf.Canceled)
									{
										TLP_Torrents.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
										TLP_Torrents.Controls.Add(tf);
										tf.Show();
										torrentsAvailable = true;
										SeedOrder++;
									}
								}
								catch (ObjectDisposedException) { }
							}
							TLP_Torrents.ResumeDrawing();

							if (urls.Count == 0)
							{
								PB_Loader.Hide();

								if (!torrentsAvailable)
									L_NoResults.Show();

								QualityChangeIdentifier.Enable();
							}
						}));
					}
					catch { }
				}).RunInBackground();
			}
		}

		private void PB_Label_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right;
			if (SelectedSort == PB_Label && r == Reversed)
			{
				SelectedSort = null;
				Reversed = false;
				SortBy(SortOption.None);
			}
			else
			{
				Reversed = r;
				SelectedSort = PB_Label;
				SortBy(SortOption.Name);
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
			var r = (e as MouseEventArgs).Button == MouseButtons.Right;
			if (SelectedSort == PB_Res && r == Reversed)
			{
				SelectedSort = null;
				Reversed = false;
				SortBy(SortOption.None);
			}
			else
			{
				Reversed = r;
				SelectedSort = PB_Res;
				SortBy(SortOption.Res);
			}
		}

		private void PB_Size_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right;
			if (SelectedSort == PB_Size && r == Reversed)
			{
				SelectedSort = null;
				Reversed = false;
				SortBy(SortOption.None);
			}
			else
			{
				Reversed = r;
				SelectedSort = PB_Size;
				SortBy(SortOption.Size);
			}
		}

		private void PB_Sound_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right;
			if (SelectedSort == PB_Sound && r == Reversed)
			{
				SelectedSort = null;
				Reversed = false;
				SortBy(SortOption.None);
			}
			else
			{
				Reversed = r;
				SelectedSort = PB_Sound;
				SortBy(SortOption.Sound);
			}
		}

		private void PB_Subs_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right;
			if (SelectedSort == PB_Subs && r == Reversed)
			{
				SelectedSort = null;
				Reversed = false;
				SortBy(SortOption.None);
			}
			else
			{
				Reversed = r;
				SelectedSort = PB_Subs;
				SortBy(SortOption.Subs);
			}
		}

		private void Quality_Click(object sender, EventArgs e)
		{
			if (QualityChangeIdentifier.Disabled || selectedLabel == sender) return;
			Cursor = Cursors.WaitCursor;
			QualityChangeIdentifier.Disable();
			if (selectedLabel != null)
				Deselect(selectedLabel);
			selectedLabel = (Label)sender;
			Select((Label)sender);

			var links = new List<string>();
			if (sender == L_AllDownloads)
				links.Add(Episode.ZooqleURL);
			else if (sender == L_3D)
				links.Add(Episode.ZooqleURL + "?tg=11");
			else if (sender == L_4K)
				links.Add(Episode.ZooqleURL + "?tg=9");
			else if (sender == L_1080p)
				links.Add(Episode.ZooqleURL + "?tg=7");
			else if (sender == L_720p)
				links.Add(Episode.ZooqleURL + "?tg=5");
			else if (sender == L_Low)
				links.AddRange(new string[] { Episode.ZooqleURL + "?tg=3", Episode.ZooqleURL + "?tg=2", Episode.ZooqleURL + "?tg=1" });

			TLP_Torrents.SuspendDrawing();
			TLP_Torrents.Controls.Clear(true);
			TLP_Torrents.ResumeDrawing();
			if (SelectedSort != null)
				SelectedSort.Image = (SelectedSort.Image as Bitmap).Color(FormDesign.Design.IconColor);
			SelectedSort = null;
			LoadTorrents(links);
			Cursor = Cursors.Default;
		}

		private void Select(Label label)
		{
			label.ForeColor = FormDesign.Design.ActiveColor;
			label.BackColor = FormDesign.Design.LightColor;
		}

		private void SetTooltips()
		{
			SlickTip.SetTo(L_Low, "Show Low quality downloads only");
			SlickTip.SetTo(L_720p, "Show 720p quality downloads only");
			SlickTip.SetTo(L_1080p, "Show 1080p quality downloads only");
			SlickTip.SetTo(L_4K, "Show 4K Ultra quality downloads only");
			SlickTip.SetTo(L_3D, "Show 3D quality downloads only");
			SlickTip.SetTo(L_AllDownloads, "Show the Top downloads from all video qualities");
			SlickTip.SetTo(PB_Download, "Download");
			SlickTip.SetTo(PB_Size, "Sort by Size");
			SlickTip.SetTo(PB_Res, "Sort by Video Resolution");
			SlickTip.SetTo(PB_Sound, "Sort by Sound Channels");
			SlickTip.SetTo(PB_Subs, "Sort by Subtitles");
			SlickTip.SetTo(PB_Label, "Sort by Torrent Name");
		}

		private void SortBy(SortOption sortOption)
		{
			var torrentData = new List<SortingData>();
			foreach (TorrentTile item in TLP_Torrents.Controls)
				torrentData.Add(item.GetSortingData());

			if (sortOption == SortOption.Name)
				torrentData = torrentData.OrderByDescending(x => x.Name).ToList();
			else if (sortOption == SortOption.Subs)
				torrentData = torrentData.OrderBy(x => x.Subs).ToList();
			else if (sortOption == SortOption.Sound)
				torrentData = torrentData.OrderBy(x => x.Sound).ToList();
			else if (sortOption == SortOption.Res)
				torrentData = torrentData.OrderBy(x => x.Res).ToList();
			else if (sortOption == SortOption.Size)
				torrentData = torrentData.OrderBy(x => x.Size).ToList();
			else
				torrentData = torrentData.OrderByDescending(x => x.SeedOrder).ToList();

			if (Reversed)
				torrentData.Reverse();

			TLP_Torrents.SuspendDrawing();
			foreach (var item in torrentData)
				item.Tile.BringToFront();
			TLP_Torrents.ResumeDrawing();
		}

		private void TLP_Torrents_Layout(object sender, LayoutEventArgs e)
		{
			TLP_Torrents.MaximumSize = new Size(panel1.Width, 9999);
			TLP_Torrents.MinimumSize = new Size(panel1.Width, 0);
		}
	}
}