using Extensions;

using NReco.VideoInfo;

using SlickControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public class WatchControl<T> : SlickAdvancedImageControl, IWatchControl where T : IPlayableContent, IContent, IInteractableContent, IDownloadableContent
	{
		private TimeSpan duration;
		private Bitmap vidQuality;
		private string imagePath;
		private int subCount;

		public T Content { get; }
		public string BannerName { get; }
		public bool OnDeck { get; }

		public WatchControl(T content, bool onDeck = true, string bannerName = null)
		{
			Cursor = Cursors.Hand;
			Margin = new Padding(10);
			BannerName = bannerName;
			OnDeck = onDeck;
			DefaultImage = ProjectImages.Huge_Play;
			EnableDots = true;
			Content = content;

			Content.WatchTimeChanged += EpisodeWatchTimeChanged;
			Content.InfoChanged += InfoChanged;
			Content.ContentRemoved += ContentRemoved;
			Content.FilesChanged += FilesChanged;
			IO.Handler.FilesChanged += IO_FilesChanged;

			SlickTip.SetTo(this, Content.Name, Content.Overview.IfEmpty("No Overview"));

			this.GetImage(imagePath = OnDeck ? Content.BackdropPath : Content.PosterPath, OnDeck ? 300 : 135, false);

			new BackgroundAction(() => setVidInfo(content.VidFiles.FirstOrDefault()?.Info)).Run();
		}

		private void ContentRemoved(object sender, EventArgs e) => Dispose();

		private void InfoChanged(object sender, EventArgs e)
		{
			if (this.IsVisible())
			{
				if (imagePath != Content.BackdropPath)
					this.GetImage(imagePath = OnDeck ? Content.BackdropPath : Content.PosterPath, OnDeck ? 300 : 135, false);

				Invalidate();
			}
		}

		private void FilesChanged(object sender, EventArgs e) => Invalidate();

		private void IO_FilesChanged(object sender, IO.Folder e) => Invalidate();

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Content.WatchTimeChanged -= EpisodeWatchTimeChanged;
				Content.InfoChanged -= InfoChanged;
				Content.ContentRemoved -= ContentRemoved;
				Content.FilesChanged -= FilesChanged;
				IO.Handler.FilesChanged -= IO_FilesChanged;
			}

			base.Dispose(disposing);
		}

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				if (Content is Episode ep && ep.IsFinale())
					yield return new Banner("SEASON FINALE", BannerStyle.Active, ProjectImages.Tiny_Rating);

				if (!string.IsNullOrWhiteSpace(BannerName))
					yield return new Banner(BannerName, BannerStyle.Text);

				if (Content.VoteCount > 0)
					yield return new Banner(Content.VoteAverage.ToString("0.#"), Content.VoteAverage.RatingColor(), ProjectImages.Tiny_Star);

				if (duration.Ticks > 0)
				{
					yield return new Banner(
						(duration - TimeSpan.FromMilliseconds(Content.WatchTime)).ToReadableString(true)
							+ (Content.Started ? " left" : string.Empty),
						BannerStyle.Text,
						ProjectImages.Tiny_Clock);
				}

				var langs = (Content as Movie)?.Languages ?? (Content as Episode)?.Show?.Languages;

				var hasEng = langs?.Any(x => x == "English") ?? false;
				var hasNoEng = langs?.FirstOrDefault(x => x != "English");

				if (!string.IsNullOrWhiteSpace(hasNoEng))
					yield return new Banner($"{hasEng.If("English, ")}{hasNoEng}{(langs.Count - (hasEng ? 2 : 1)).If(y => y > 0, y => $" +{y}", y => null)}", hasEng ? BannerStyle.Yellow : BannerStyle.Red, ProjectImages.Tiny_Translation);

				if (subCount > 0)
					yield return new Banner($"{subCount} Subtitle".Plural(subCount), BannerStyle.Green, ProjectImages.Tiny_CC);

				yield return new Banner((Content as Episode)?.Show?.Name, BannerStyle.Text, Content.TinyIcon);

				var ratingInfo = Content.Rating;

				if (ratingInfo.Loved)
					yield return new Banner("Loved", BannerStyle.Red, ProjectImages.Tiny_Love);

				if (ratingInfo.Rated)
					yield return new Banner(ratingInfo.Rating.ToString("0.##"), ratingInfo.Rating.RatingColor(), ProjectImages.Tiny_Rating);
			}
		}

		protected override IEnumerable<Bitmap> HoverIcons
		{
			get
			{
				if (Content.Playable)
					yield return OnDeck ? ProjectImages.Huge_Play : ProjectImages.Icon_PlaySlick;
				else if (ConnectionHandler.IsConnected && Content.CanBeDownloaded)
					yield return ProjectImages.Huge_Download;
			}
		}

		protected override void OnLoadCompleted(AsyncCompletedEventArgs e)
		{
			if (OnDeck && (e.Error != null || Image == null))
			{
				Loading = true;
				new BackgroundAction(() => Image = Content.GetThumbnail()).Run();
			}
		}

		protected override void OnDotsMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				Content.ShowStrip(PointToScreen(e.Location));
		}

		protected override void OnDraw(PaintEventArgs e)
		{
			var text = Content.Name;
			var subText = Content.SubInfo;
			var infoText = Content.AirDate?.ToReadableString();
			var progress = Content.Progress;
			var barRect = Enabled && progress > 0
				? new Rectangle(3 + ImageBounds.Left, ImageBounds.Top + ImageBounds.Height - (int)(5 * UI.FontScale) - 2, ImageBounds.Width - 5, (int)(5 * UI.FontScale))
				: Rectangle.Empty;

			if (Enabled && progress > 0)
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(175, FormDesign.Design.AccentColor)), barRect);

				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, FormDesign.Design.ActiveColor))
					, new RectangleF(barRect.X, barRect.Y, (float)(barRect.Width * progress / 100), barRect.Height));
			}

			if (ImageHovered)
			{
				DrawTextOnImage(e, $"{(Content.Playable ? "PLAY" : ConnectionHandler.IsConnected && Content.CanBeDownloaded ? "DOWNLOAD" : "")} {Content.Type.ToString().ToUpper()}", false, barRect.Height);

				if (OnDeck && Content.Type == ContentType.Episode)
					DrawTextOnImage(e, (Content as Episode).Show.Name.ToUpper(), true, barRect.Height);
			}

			DrawText(e, text, UI.Font(OnDeck ? 9.75F : 8.25F, FontStyle.Bold), FormDesign.Design.ForeColor, rigthPad: 21);

			DrawText(e, subText, UI.Font(OnDeck ? 8.25F : 6.75F), FormDesign.Design.LabelColor, rigthPad: 21);

			DrawText(e, infoText, UI.Font(OnDeck ? 8.25F : 6.75F), FormDesign.Design.InfoColor, rigthPad: 21);

			if (vidQuality != null)
				e.Graphics.DrawImage(vidQuality.Color(FormDesign.Design.IconColor)
					, new Rectangle(Width - 20, Math.Max(ImageBounds.Top + ImageBounds.Height + 24, Enabled ? (yIndex - 20) : 0), 16, 16));
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
				Play();
			else if (e.Button == MouseButtons.Right)
				Content.ShowStrip(PointToScreen(e.Location));
		}

		protected override void UIChanged()
		{
			Size = UI.Scale(OnDeck ? new Size(300, 220) : new Size(135, 250), UI.FontScale);
			ImageBounds = new Rectangle(5, 5, Width - 10, OnDeck ? (Width - 10) * 9 / 16 : (Width - 10) * 3 / 2);
		}

		private void EpisodeWatchTimeChanged(object sender, double percentage)
		{
			if (this.IsVisible() && Enabled) this.TryInvoke(() =>
			{
				Invalidate();
				Parent?.Controls.SetChildIndex(this, 0);
			});
		}

		private void Play()
		{
			Cursor.Current = Cursors.WaitCursor;
			if ((!Content.Playable || !Content.Play()) && ConnectionHandler.IsConnected && Content.CanBeDownloaded)
				Content.Download();
			Cursor.Current = Cursors.Default;
		}

		private void setVidInfo(FileInfo fileInfo)
		{
			if (fileInfo?.Exists ?? false)
			{
				var fileprops = new FFProbe().GetMediaInfo(fileInfo.FullName);
				var vidprops = fileprops.Streams.FirstOrDefault(x => x.CodecType == "video")?.Height ?? 0;

				duration = TimeSpan.FromMinutes(Math.Floor((fileprops.Duration.TotalMinutes + 2) / 5) * 5);

				if (vidprops > 1700)
					vidQuality = ProjectImages.Tiny_4K;
				else if (vidprops > 775)
					vidQuality = ProjectImages.Tiny_1080;
				else if (vidprops > 550)
					vidQuality = ProjectImages.Tiny_720;
				else
					vidQuality = ProjectImages.Tiny_SD;

				subCount = fileprops.Streams.Count(x => x.CodecType == "subtitle") +
					Directory.GetParent(fileInfo.FullName).GetFiles("*.srt", 2).Count(x =>
						Content.Type == ContentType.Movie || x.FileName().SearchCheck(fileInfo.FileName()));

				this.TryInvoke(Invalidate);
			}
		}
	}
}