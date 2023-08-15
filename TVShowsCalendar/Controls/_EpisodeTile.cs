using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Extensions;
using System.Text.RegularExpressions;
using TVShowsCalendar.Classes;
using System.Drawing.Imaging;
using ProjectImages = TVShowsCalendar.Properties.Resources;
using TVShowsCalendar.Forms;
using TVShowsCalendar.HandlerClasses;
using System.Runtime.InteropServices;
using SlickControls.Enums;
using SlickControls.Forms;
using SlickControls.Classes;

namespace TVShowsCalendar
{
	public partial class EpisodeTile : UserControl
	{
		public Episode Episode;
		private bool PictureLoaded;
		private bool shouldLoad = true;
		private bool DownloadView;

		public event EventHandler DownloadClicked;

		public EpisodeTile(Episode episode, Size? size = null, bool downloadView = false)
		{
			InitializeComponent();
			Size = size ?? Size;
			DownloadView = downloadView;
            ResizeRedraw = true;
			
			SetData(episode);
			LocalShowHandler.FolderChanged += LocalShowHandler_FolderChanged;
			Disposed += (s,e) =>
				LocalShowHandler.FolderChanged -= LocalShowHandler_FolderChanged;
		}

		public void SetData(Episode episode)
		{
			if (InvokeRequired)
				Invoke(new Action(() => SetData(episode)));
			else
			{
				this.SuspendDrawing();
				Episode = episode;
				L_Name_Text = $"{Episode.Show.Name} {Episode.SN}x{Episode.EN} - {Episode.Name}";

				if (episode.AirState == AirStateEnum.Aired)
					L_AirTime_Text = $"Aired {Episode.TMDbData.AirDate?.RelativeString()}";
				else if (episode.AirState == AirStateEnum.ToBeAired)
					L_AirTime_Text = $"Airing {Episode.TMDbData.AirDate?.RelativeString()}";
				else
					L_AirTime_Visible = false;

				L_Overview_Text = episode.TMDbData.Overview.IfEmpty("No Overview Yet");

				//ML_Download.ActiveColor = () => FormDesign.Design.GreenColor;
				//ML_Download.Click += (s, e) => L_GetTorrent_Click();
				//L_Name.Click += (s, e) => L_Name_Click();

				if (episode.TMDbData.VoteCount != 0)
				{
					//toolTip.SetToolTip(PB_Star, episode.TMDbData.VoteAverage.ToString());
					PB_Star_Visible = true;
				}

				LocalShowHandler_FolderChanged(null);
				
				if (Episode.Season.Poster != null)
				{
					PB_Thumb.Image = PB_Thumb.InitialImage = PB_Thumb.ErrorImage = Episode.Season.Poster;
					PictureLoaded = true;
					shouldLoad = false;
				}
				else if (Episode.Season.Show.Poster != null)
				{
					PB_Thumb.Image = PB_Thumb.InitialImage = PB_Thumb.ErrorImage = Episode.Season.Show.Poster;
					shouldLoad = false;
				}
				
				FormDesign.DesignChanged += DesignChanged;
				DesignChanged(FormDesign.Design);

				if (!PictureLoaded)
					ConnectionHandler.Connected += (c) =>
					{
						if (shouldLoad)
							PB_Thumb.Image = null;
						shouldLoad = true;
						if (!string.IsNullOrWhiteSpace(Episode.Season.TMDbData.PosterPath))
							PB_Thumb.LoadAsync($"https://image.tmdb.org/t/p/w200{Episode.Season.TMDbData.PosterPath}");
						else if (!string.IsNullOrWhiteSpace(Episode.Season.Show.TMDbData.PosterPath))
							PB_Thumb.LoadAsync($"https://image.tmdb.org/t/p/w200{Episode.Season.Show.TMDbData.PosterPath}");
					};

				this.ResumeDrawing();
			}
		}

		private void LocalShowHandler_FolderChanged(Show show)
		{
			if(show == null || show == Episode.Show)
			{
				if (InvokeRequired)
				{ Invoke(new Action(() => LocalShowHandler_FolderChanged(show))); return; }

				if (!DownloadView && Episode.AirState == AirStateEnum.Aired)
				{
					if (Episode.EpFile != null)
					{
						//ML_Download.Text = "Play";
						//ML_Download.Image = ProjectImages.Icon_Play;
						//ML_Download.ActiveColor = null;
						//ML_Download.HideText = true;
						//ML_Download.Show();
						PerformLayout();
					}
					else if (Episode.DownloadAvailable != null)
					{
						LoadDownload();
					}
					else
					{
						if (!Episode.DownloadRequested)
							new Action(Episode.LoadDownload).RunInBackground();

						Episode.DownloadLoaded += (s, e) => LoadDownload();
					}
					PB_Thumb.Refresh();
				}
			}
		}

		private void LoadDownload()
		{
			if (InvokeRequired)
				Invoke(new Action(LoadDownload));
			else
			{
				if (Episode.EpFile == null)
				{
					if (Episode.DownloadAvailable ?? false)
					{
						try
						{
							//ML_Download.Text = "Download";
							//ML_Download.Image = ProjectImages.Icon_Download_Rounded;
							//ML_Download.ActiveColor = () => FormDesign.Design.GreenColor;
							//ML_Download.HideText = true;
							//ML_Download.Show();
							PerformLayout();
						}
						catch { }
					}
					else if (string.IsNullOrWhiteSpace(Episode.Show.ZooqleURL))
					{
						try
						{
							//ML_Download.Text = "Link";
							//ML_Download.Image = ProjectImages.Icon_Link;
							//ML_Download.ActiveColor = null;
							//ML_Download.HideText = false;
							//ML_Download.Show();
							PerformLayout();
						}
						catch { }
					}
				}
			}
		}

		private void DesignChanged(FormDesign design)
		{
			//RP_Back.BackColor = TLP_Main.BackColor = PB_Star.BackColor = MI_More.BackColor = design.MenuColor;
			PB_Star_Image = GetStar( Episode.TMDbData.VoteAverage * 10, design);

			ForeColor = (Episode.AirState == AirStateEnum.ToBeAired && Height <= 70) ? design.LabelColor : design.ForeColor;

			if (shouldLoad)
			{
				PB_Thumb.InitialImage = design.ID == 2 ? ProjectImages.GIF_Ring_B : ProjectImages.GIF_Ring_W;
				PB_Thumb.ErrorImage = new Bitmap(ProjectImages.Icon_VTV, Height <= 70 ? new Size(28, 28) : new Size(50, 50)).Color(design.IconColor);
			}

			if (!PictureLoaded && shouldLoad)
				PB_Thumb.Image = new Bitmap(ProjectImages.Icon_VTV, Height <= 70 ? new Size(28, 28) : new Size(50, 50)).Color(design.IconColor);
		}

		private void More_Click(object sender, EventArgs ea)
		{
			new FlatToolStrip(new List<FlatStripItem>()
			{
				new FlatStripItem(ML_Download_Text, L_GetTorrent_Click, image: (Bitmap)ML_Download_Image, show: ML_Download_Visible),
				new FlatStripItem("More Info", () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					if (Episode.Show.Page == null)
						Episode.Show.Page = new ShowPage(Episode);
					else
						Episode.Show.Page.SetSeason(Episode.Season, Episode);
					Cursor.Current = Cursors.Default;
				}, image: ProjectImages.Icon_Info),
				new FlatStripItem("Mark", image: ProjectImages.Icon_OK, fade: true),
				new FlatStripItem("  Ep. as " + (Episode.Watched ? "Unwatched" : "Watched"), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					Episode.Watched = !Episode.Watched;
					new Action(()=>
					{
						Data.ShowLibrary?.LoadLibrary();
						ShowManager.Save(Episode);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}),
				new FlatStripItem("  Season as " + (Episode.Season.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched) ? "Unwatched" : "Watched"), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					var set = !Episode.Season.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched);
					Episode.Season.Episodes.ForEach(e => e.Watched = e.AirState == AirStateEnum.Aired ? set : e.Watched);
					new Action(()=>
					{
						Data.ShowLibrary?.LoadLibrary();
						ShowManager.Save(Episode.Show);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}),
				new FlatStripItem("  Show as " + (Episode.Show.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched)) ? "Unwatched" : "Watched"), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					var set = !Episode.Show.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched));
					Episode.Show.Seasons.ForEach(x => x.Episodes.ForEach(e => e.Watched = e.AirState == AirStateEnum.Aired ? set : e.Watched));
					new Action(()=>
					{
						Data.ShowLibrary?.LoadLibrary();
						ShowManager.Save(Episode.Show);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				})
			}, 185).ShowUp();
		}
		
		public void L_GetTorrent_Click()
		{
			if (Episode.EpFile != null && Episode.EpFile.Exists)
			{
				Cursor.Current = Cursors.WaitCursor;
				try
				{
					Episode.Play(Episode.EpFile);
				}
				catch (Exception ex) { Cursor.Current = Cursors.Default; MessagePrompt.Show($"Could not play the file.\nCheck that you have a default app selected for {Episode.EpFile.Extension} files.\n\nError Message: {ex.Message}", "No Supported Player", PromptButtons.OK, PromptIcons.Error); }
				Cursor.Current = Cursors.Default;
			}
			else if (Episode.EpFile != null)
			{
				if (DialogResult.Retry == MessagePrompt.Show("Could not find this episode's file.\n\nWould you like to " + (string.IsNullOrWhiteSpace(Episode.Show.ZooqleURL) ? "link this Show to Zooqle for download options?" : "open the download window for this episode?"), "File Missing", PromptButtons.YesNo, PromptIcons.Warning))
					goto P;
			}
			else
				goto P;

			return;

			P: if (!string.IsNullOrWhiteSpace(Episode.Show.ZooqleURL))
			{
				Cursor.Current = Cursors.WaitCursor;
				DownloadClicked?.Invoke(this, new EventArgs());
				(new DownloadForm(Episode)).ShowUp();
				Cursor.Current = Cursors.Default;
			}
			else
			{
				var result = MessagePrompt.ShowInput(
				$"Enter a Zooqle TV Show link for {Episode.Show.Name}",
				"Zooqle Link",
				Episode.Show.ZooqleURL.IfEmpty(""),
				PromptButtons.OKCancel,
				PromptIcons.Input,
				s => Regex.IsMatch(s, @"(https?://)?zooqle.com/tv/[^/]+/", RegexOptions.IgnoreCase));

				if (result.DialogResult == DialogResult.OK)
				{
					Episode.Show.ZooqleURL = Regex.Match(result.Input, @"(https?://)?zooqle.com/tv/[^/]+/", RegexOptions.IgnoreCase).Value;
					ShowManager.Save(Episode.Show);
					ML_Download_Visible = false;					
					
					new Action(LoadDownload).RunInBackground();
				}
			}
		}

		private void PB_Thumb_Click(object sender, EventArgs e) => L_Name_Click();

		private void L_Name_Click()
		{
			Cursor.Current = Cursors.WaitCursor;
			if (Episode.Show.Page == null)
				Episode.Show.Page = new ShowPage(Episode);
			else
				Episode.Show.Page.SetSeason(Episode.Season, Episode);
			Cursor.Current = Cursors.Default;
		}

		private void EpisodeTile_Layout(object sender, LayoutEventArgs e)
		{
			//if (Episode == null)
			//	return;

			//var border = 5;

			//PB_Thumb.Width = Height * 2 / 3;

			//MI_More.Location = new Point(Width - 9 - MI_More.Width, 9);
			//PB_Star.Location = new Point(MI_More.Location.X - 3 - PB_Star.Width, 9);

			//L_Name.MaximumSize = new Size(PB_Star.Visible.If(true, PB_Star.Location.X, MI_More.Location.X) - L_Name.Location.X - PB_Thumb.Width, 999);

			//L_Overview_Visible = Height > 100 || DownloadView;

			//L_Overview.MaximumSize = new Size(TLP_Main.Width - 10 - (Height * 2 / 3), Height - (DownloadView ? 62 : 80));

			//if (Width > 550)
			//	L_Name.Text = $"{Episode.Show.Name} {Episode.SN}x{Episode.EN} - {Episode.Name}";
			//else
			//	L_Name.Text = $"{(Episode.Show.Name.Length > 15 ? Episode.Show.Name.GetAbbreviation() : Episode.Show.Name)} {Episode.SN}x{Episode.EN} - {Episode.Name}";

			//if (Height <= 70 || DownloadView)
			//{
			//	TLP_Main.Bounds = new Rectangle(PB_Thumb.Width, border, Width - PB_Thumb.Width - border, Height - border - border);
			//	L_Name.Margin = new Padding(0);
			//	L_AirTime_Margin = 15;
			//	PB_Star.Size = new Size(22, 22);
			//	MI_More.Height = 22;
			//	L_AirTime_Font = new Font("Century Gothic", 9.5F, FontStyle.Italic);
			//	L_Name.IconSize = 15;
			//}
			//else
			//{
			//	TLP_Main.Bounds = new Rectangle(PB_Thumb.Width + border, border, Width - PB_Thumb.Width - border - border, Height - border - border);
			//}

			//if (ML_Download.Visible)
			//	ML_Download.HideText = TLP_Main.Width - L_AirTime.Location.X - L_AirTime.Width <= 130;
		}

		private string L_Name_Text = "";
		private Font L_Name_Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
		private Bitmap PB_Star_Image;
		private string L_Overview_Text;

		public Font L_AirTime_Font = new Font("Century Gothic", 11.25F, FontStyle.Italic);
		private string L_AirTime_Text;
		private bool L_Overview_Visible = false;
		private int L_AirTime_Margin = 30;
		private bool L_AirTime_Visible = true;
		private bool PB_Star_Visible=false;
		private string ML_Download_Text;
		private Bitmap ML_Download_Image;
		private bool ML_Download_Visible;

		private void PB_Thumb_Paint(object sender, PaintEventArgs e)
		{
			if (PB_Thumb.Image == null || PB_Thumb.Image.RawFormat.Guid == ImageFormat.Gif.Guid)
				return;

			p: try
			{
				if (PB_Thumb.Image.Width > 100)
				{
					var brush = new TextureBrush(new Bitmap(PB_Thumb.Image, new Size(PB_Thumb.Height * PB_Thumb.Image.Width / PB_Thumb.Image.Height, PB_Thumb.Height)));
					var border = 10 * 2;

					e.Graphics.Clear(BackColor);
					e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

					e.Graphics.FillPie(brush, new Rectangle(0, 0, border, border), 180, 90);
					e.Graphics.FillPie(brush, new Rectangle(0, PB_Thumb.Height - border, border, border), 90, 90);

					e.Graphics.FillRectangles(brush, new Rectangle[]
					{
						new Rectangle(border / 2 - 1, 0, PB_Thumb.Width, PB_Thumb.Height),
						new Rectangle(0, border / 2 - 1, PB_Thumb.Width, PB_Thumb.Height - border + 2)
					});

					if (!Episode.Watched && Episode.AirState == AirStateEnum.Aired && Data.Options.ShowUnwatchedOnThumb)
					{
						if ((Episode.TMDbData.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-7))
						{
							var fontSize = (Height <= 70 || DownloadView) ? 6F : 12F;
							var size = e.Graphics.MeasureString("NEW", new Font("Century Gothic", fontSize, FontStyle.Bold));
							var rect = new RectangleF(0, PB_Thumb.Height - (size.Height + 2) - (border / 2), PB_Thumb.Width, size.Height + 4);

							e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.ActiveColor), rect);
							e.Graphics.DrawString("NEW", new Font("Century Gothic", fontSize, FontStyle.Bold), new SolidBrush(Color.FromArgb(30, 30, 30)),
								new PointF((PB_Thumb.Width - size.Width + ((Height <= 70 || DownloadView) ? 1 : 4F)) / 2, rect.Y + (7 / 2) + ((Height <= 70 || DownloadView) ? -1 : 0F)));
						}
						else
						{
							var fontSize = (Height <= 70 || DownloadView) ? 5F : 10.5F;
							var size = e.Graphics.MeasureString("UNWATCHED", new Font("Century Gothic", fontSize, FontStyle.Bold));
							var rect = new RectangleF(0, PB_Thumb.Height - (size.Height + 2) - (border / 2), PB_Thumb.Width, size.Height + 4);

							e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.YellowColor), rect);
							e.Graphics.DrawString("UNWATCHED", new Font("Century Gothic", fontSize, FontStyle.Bold), new SolidBrush(Color.FromArgb(60, 60, 60)),
								new PointF((PB_Thumb.Width - size.Width + ((Height <= 70 || DownloadView) ? 1 : 4F)) / 2, rect.Y + (7 / 2) + ((Height <= 70 || DownloadView) ? 0 : 1F)));
						}
					}
				}
			}
			catch (InvalidOperationException)
			{ Thread.Sleep(150); goto p; }
		}

		private void PB_Thumb_LoadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			PictureLoaded = !e.Cancelled && e.Error == null;
			PB_Thumb.SizeMode = PictureLoaded ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
			if (PictureLoaded)
				Episode.Season.Poster = (Bitmap)PB_Thumb.Image;
		}

		private Bitmap GetStar(double perc, FormDesign design)
		{
			if (perc == 100)
				return ProjectImages.Icon_Star.Color(design.ActiveColor);
			else if (perc == 0)
				return ProjectImages.Icon_Star.Color(design.InfoColor);

			var bitmap = new Bitmap(ProjectImages.Icon_Star, new Size(28, 28));

			for (int x = 0; x < 28; x++)
			{
				for (int y = 0; y < 28; y++)
					bitmap.SetPixel(x, y, Color.FromArgb(bitmap.GetPixel(x, y).A, 100D * x / 28D <= perc ? design.ActiveColor : design.InfoColor));
			}

			return bitmap;
		}

		private void EpisodeTile_Load(object sender, EventArgs e)
		{
			this.ResumeDrawing();
		}

		private void EpisodeTile_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			e.Graphics.FillRoundedRectangle(new SolidBrush(FormDesign.Design.MenuColor), new Rectangle(new Point(Padding.Left, Padding.Top), new Size(Width - Padding.Horizontal, Height - Padding.Vertical)), 20);

			e.Graphics.DrawString(L_Name_Text, L_Name_Font, new SolidBrush(ForeColor), new PointF(PB_Thumb.Width + 5 + Padding.Left, Padding.Top + 5));

			e.Graphics.DrawImage(new Bitmap(ProjectImages.Icons_VDots, new Size(26, 26)).Color(FormDesign.Design.IconColor), new Point(Width - Padding.Right - 20, Padding.Top + 5));
			if(PB_Star_Visible)
			e.Graphics.DrawImage(new Bitmap(PB_Star_Image, new Size(26, 26)), new Point(Width - Padding.Right - 50, Padding.Top + 5));

			//var overBounds = L_Overview_Visible ? e.Graphics.MeasureString(L_Overview_Text, new Font("Century Gothic", 9.75F)) : new SizeF();
			//var airBounds = L_AirTime_Visible ? e.Graphics.MeasureString(L_AirTime_Text, L_AirTime_Font) : new SizeF();

			//var y = Padding.Top + 25 + (L_AirTime_Visible ? Math.Max(0, (Height - 25 - overBounds.Height - 10 - Padding.Vertical) / 2) : 0);

			//if (L_AirTime_Visible)
			//	e.Graphics.DrawString(L_AirTime_Text, L_AirTime_Font, new SolidBrush(base.ForeColor), new PointF(PB_Thumb.Width + L_AirTime_Margin + Padding.Left, y));

			//if (L_Overview_Visible)
			//	e.Graphics.DrawString(L_Overview_Text, 
			//		new Font("Century Gothic", 9.75F), 
			//		new SolidBrush(ForeColor), 
			//		new RectangleF(PB_Thumb.Width + 7 + Padding.Left, 
			//			Math.Max(y + airBounds.Height + 5, Height - Padding.Bottom - overBounds.Height),
			//			Width - Padding.Horizontal - PB_Thumb.Width,
			//			Height - Math.Max(y + airBounds.Height + 5, Height - Padding.Bottom - overBounds.Height)));
		}

        private void L_Actions_LocationChanged(object sender, EventArgs e)
        {
            L_Actions.Location = new Point(Width - L_Actions.Width - 5, Height - L_Actions.Height - 5);
        }

        private void EpisodeTile_Resize(object sender, EventArgs e)
        {
            PB_Thumb.Width = Height * 2 / 3;
        }
    }
}
