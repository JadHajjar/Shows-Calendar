using Extensions;





using SlickControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public partial class EpisodeTile : SlickAdvancedImageControl
	{
		public Episode Episode;

		public bool Horizontal { get; private set; }
		public bool DisplayView { get; private set; }

		public EpisodeTile(Episode episode, bool horizontal = true, bool displayView = false)
		{
			InitializeComponent();

			Horizontal = horizontal;
			DisplayView = displayView;
			Episode = episode;
			EnableDots = true;
			DefaultImage = horizontal ? ProjectImages.Big_TVEmpty : ProjectImages.Huge_TVPlay;

			if (!horizontal)
				Margin = new Padding(7);

			I_Action.Visible = !displayView && horizontal && Episode.AirState == AirStateEnum.Aired;

			var vid = Episode.VidFiles.Any(y => y.Exists);
			I_Action.Icon = vid ? ProjectImages.Tiny_Play : ProjectImages.Tiny_Download;
			I_Action.HoverStyle = vid ? ColorStyle.Active : ColorStyle.Green;

			ReloadData();

			I_Action.Parent = this;

			LoadCompleted += EpisodeTile_LoadCompleted;
			Episode.WatchTimeChanged += Episode_EpisodeWatchTimeChanged;
			ShowManager.EpisodeRemoved += ShowManager_EpisodeRemoved;
			LocalShowHandler.FolderChanged += LocalShowHandler_FolderChanged;
			Disposed += (s, e) =>
			{
				ShowManager.EpisodeRemoved -= ShowManager_EpisodeRemoved;
				Episode.WatchTimeChanged -= Episode_EpisodeWatchTimeChanged;
				LocalShowHandler.FolderChanged -= LocalShowHandler_FolderChanged;
			};
		}

		protected override void UIChanged()
		{
			Size = UI.Scale(Horizontal ? new Size(350, 135) : new Size(275, 220), UI.FontScale);
			ImageBounds = Horizontal
				   ? new Rectangle(new Point(1, 1), UI.Scale(new Size(88, 133), UI.FontScale))
				   : new Rectangle(1, 0, Width - 2, (Width - 2) * 9 / 16);
		}

		private void ShowManager_EpisodeRemoved(Episode episode)
		{
			if (episode == Episode)
				this.TryInvoke(Dispose);
		}

		private void Episode_EpisodeWatchTimeChanged(object sender, double obj) => ReloadData();

		public void ReloadData()
		{
			if (this.IsVisible())
			{
				if (!DisplayView)
				{
					if (Horizontal)
						SlickTip.SetTo(this, Episode.Show.Name);
					else
						SlickTip.SetTo(this, Episode.Name, Episode.Overview.IfEmpty("No Overview"));
				}

				this.TryInvoke(() =>
				{
					if (Horizontal)
						this.GetImage(Episode.Season.PosterPath.IfEmpty(Episode.Show.PosterPath), 82, false);
					else
						this.GetImage(Episode.StillPath.IfEmpty(Episode.Show.BackdropPath), 275, false, blur: !Horizontal && Data.Options.SpoilerThumbnail && !Episode.Watched && !(Episode.Previous?.Watched ?? true) ? 8 : 0);
				});
			}
		}

		private void EpisodeTile_LoadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			if (e.Error != null && !Horizontal && !string.IsNullOrWhiteSpace(Episode.StillPath) && Guid.TryParse(Episode.StillPath.Substring(1, Episode.StillPath.Length - 5), out _))
			{
				Episode.StillPath = null;
				this.GetImage(Episode.Show.BackdropPath, 275, false);

				new Action(() => ShowManager.Save(Episode)).RunInBackground();
			}
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
				ShowPage();
			else
				base.OnMouseClick(e);
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (new Rectangle(1 + (Width - 2) / 2, 0, (Width - 2) / 2, (Width - 2) * 9 / 16).Contains(e.Location)
					&& Episode.Playable)
					Episode.Play();
				else
					ShowPage();
			}
			else if (e.Button == MouseButtons.Right)
				Episode.ShowStrip(PointToScreen(e.Location));
		}

		protected override void OnDotsMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				Episode.ShowStrip();
		}

		private void ShowPage()
		{
			if (Data.Mainform.CurrentPanel is PC_SeasonView seasonView && seasonView.Season == Episode.Season)
				Data.Mainform.PushPanel(null, new PC_EpisodeView(Episode));
			else if (!Data.Options.OpenAllPagesForEp)
				Data.Mainform.PushPanel(null, new PC_EpisodeView(Episode));
			else if (!(Data.Mainform.CurrentPanel is PC_EpisodeView epView && epView.Episode == Episode))
				Data.Mainform.PushPanel(null, new PC_ShowPage(Episode));
		}

		private void EpisodeTile_MouseMove(object sender, MouseEventArgs e)
		{
			if (DisplayView)
				return;

			if (Horizontal && I_Action.MouseHovered) 
				Cursor = Cursors.Hand;
		}

		protected override IEnumerable<Bitmap> HoverIcons
		{
			get
			{
				yield return ProjectImages.Icon_Info;

				if (!Horizontal && Episode.Playable)
					yield return ProjectImages.Icon_PlaySlick;
			}
		}

		protected override IEnumerable<Banner> Banners
		{ 
			get
			{
				if (((Episode.AirDate?.IfNull(false, Episode.AirDate < DateTime.Today)) ?? false) && Episode.AirState == AirStateEnum.Aired)
				{
					if ((Episode.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-8))
						yield return new Banner("NEW", BannerStyle.Active, ProjectImages.Tiny_New);
					if (!Episode.Watched)
						yield return new Banner(Horizontal ? string.Empty : "NOT WATCHED", BannerStyle.Yellow, ProjectImages.Tiny_Unwatched);
				}

				if (Episode.Rating.Loved)
					yield return new Banner("Loved", BannerStyle.Red, ProjectImages.Tiny_Love);

				if (Episode.Rating.Rated)
					yield return new Banner(Episode.Rating.Rating.ToString("0.##"), Episode.Rating.Rating.RatingColor(), ProjectImages.Tiny_Rating);

				var rating = Episode.VoteAverage;
				var votes = Episode.VoteCount;

				if (votes > 0)
					yield return new Banner(rating.ToString("0.##"), rating.RatingColor(), ProjectImages.Tiny_Star);
			}
		}

		protected override void OnDraw(PaintEventArgs e)
		{
			var progress = Episode.Progress;
			var barRect = Enabled && progress > 0
				? new Rectangle(3 + ImageBounds.Left, ImageBounds.Top + ImageBounds.Height - (int)(5 * UI.FontScale) - 2, ImageBounds.Width - 5, (int)(5 * UI.FontScale))
				: Rectangle.Empty;

			if (Enabled && progress > 0)
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(175, FormDesign.Design.AccentColor)), barRect);

				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, FormDesign.Design.ActiveColor))
					, new RectangleF(barRect.X, barRect.Y, (float)(barRect.Width * progress / 100), barRect.Height));
			}

			DrawTextOnImage(e, "EPISODE", false, barRect.Height);

			if (!Horizontal)
				DrawTextOnImage(e, Episode.Show.Name.ToUpper(), true, barRect.Height);

			I_Action.Draw(e);

			DrawText(e, Episode.Name, UI.Font(9.75F, FontStyle.Bold), FormDesign.Design.ForeColor, rigthPad: 40);
			DrawText(e, $"Season {Episode.SN} • Episode {Episode.EN}", UI.Font(8.25F), FormDesign.Design.LabelColor);

			var txt = "No air date yet";
			if (Episode.AirState == AirStateEnum.Aired)
				txt = $"Aired {Episode.AirDate?.RelativeString()}";
			else if (Episode.AirState == AirStateEnum.ToBeAired)
				txt = $"Airing {Episode.AirDate?.RelativeString()}";

			DrawText(e, txt, UI.Font(6.75F), FormDesign.Design.InfoColor);

			if (Horizontal)
				DrawText(e, Episode.Overview.IfEmpty("No overview"), UI.Font(6.75F), FormDesign.Design.InfoColor, fill: true);
		}

		private void LocalShowHandler_FolderChanged(TvShow show)
		{
			if (show == null || show == Episode.Show)
			{
				var vid = Episode.VidFiles.Any(y => y.Exists);
				I_Action.Icon = vid ? ProjectImages.Tiny_Play : ProjectImages.Tiny_Download;
				I_Action.HoverStyle = vid ? ColorStyle.Active : ColorStyle.Green;

				this.TryInvoke(Invalidate);
			}
		}

		private void I_Action_Click(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				if (Episode.VidFiles.Any(x => x.Exists))
				{
					Episode.Play();
				}
				else
				{
					Data.Mainform.PushPanel(null, new PC_Download(Episode));
				}
		}
	}
}