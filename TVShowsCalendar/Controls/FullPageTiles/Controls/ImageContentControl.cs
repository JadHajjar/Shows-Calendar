using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public class ImageContentControl<T> : SlickAdvancedImageImageBackgroundControl where T : IContent, IInteractableContent
	{
		protected Rectangle ActionRect;
		protected string imagePath;
		protected bool blurred;

		public T Content { get; private set; }
		public IPlayableContent PlayableContent { get; private set; }
		public IDownloadableContent DownloadableContent { get; private set; }
		public IParentContent ParentContent { get; private set; }
		public bool Horizontal { get; private set; }
		public bool DisplayView { get; private set; }
		public List<string> SearchTags { get; } = new List<string>();

		public ImageContentControl(T content, bool horizontal = false)
		{
			Margin = new Padding(7);
			Horizontal = horizontal;
			EnableDots = true;

			Install(content);
		}

		public virtual void Install(T content)
		{
			UnInstall();
			if (content == null) return;

			Content = content;
			DefaultImage = Horizontal ? Content.BigIcon : Content.HugeIcon;
			ParentContent = Content as IParentContent;
			DownloadableContent = Content as IDownloadableContent;
			PlayableContent = Content as IPlayableContent;

			Content.InfoChanged += InfoChanged;
			Content.ContentRemoved += ContentRemoved;

			if (PlayableContent != null)
			{
				PlayableContent.WatchTimeChanged += WatchTimeChanged;
				PlayableContent.FilesChanged += FilesChanged;
				IO.Handler.FilesChanged += IO_FilesChanged;

				FilesChanged(null, null);
			}

			InfoChanged(null, null);
		}

		public virtual void UnInstall()
		{
			if (Content == null) return;

			Content.InfoChanged -= InfoChanged;
			Content.ContentRemoved -= ContentRemoved;

			if (PlayableContent != null)
			{
				PlayableContent.WatchTimeChanged -= WatchTimeChanged;
				PlayableContent.FilesChanged -= FilesChanged;
				IO.Handler.FilesChanged -= IO_FilesChanged;
			}

			Content = default;
			PlayableContent = null;
			ParentContent = null;
			DownloadableContent = null;
		}

		protected virtual void InfoChanged(object sender, EventArgs e)
		{
			if (Container?.IsVisible() ?? true)
			{
				if (Horizontal)
				{
					if (imagePath != Content.PosterPath)
						LoadImage(() => ImageHandler.GetImage(imagePath = Content.PosterPath, 160, false, true));
				}
				else
				{
					var toBlur = !Horizontal
						&& ShowsCalendar.Data.Options.SpoilerThumbnail
						&& Content is Episode ep
						&& ep.Show.DateAdded != DateTime.MinValue
						&& !ep.Watched
						&& !(ep.Previous?.Watched ?? true);

					if (imagePath != Content.BackdropPath || blurred != toBlur)
						LoadImage(() => ImageHandler.GetImage(imagePath = Content.BackdropPath, 275, false,
								blur: (blurred = toBlur) ? (int?)null : 0));
				}

				Invalidate();
			}
		}

		private void WatchTimeChanged(object sender, double e)
		{
			if (Container.IsVisible() && Enabled)
				Invalidate();
		}

		private void FilesChanged(object sender, EventArgs e) => Invalidate();

		private void IO_FilesChanged(object sender, IO.Folder e) => Invalidate();

		private void ContentRemoved(object sender, EventArgs e) => Dispose();

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				UnInstall();

			base.Dispose(disposing);
		}

		protected override void OnLoadCompleted(AsyncCompletedEventArgs e)
		{
			if ((e.Error != null || Image == null) && !Horizontal && PlayableContent != null && PlayableContent.Playable)
			{
				Loading = true;
				new BackgroundAction(() =>
				{
					Image = PlayableContent
						.GetThumbnail()
						.Blur(ShowsCalendar.Data.Options.SpoilerThumbnail
							&& Content is Episode ep
							&& ep.Show.DateAdded != DateTime.MinValue
							&& !ep.Watched
							&& !(ep.Previous?.Watched ?? true) ? (int?)null : 0);
					Loading = false;
				}).Run();
			}
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
			{
				if (new Rectangle(1 + (Width - 2) / 2, 0, (Width - 2) / 2, ImageBounds.Height).Contains(e.Location))
				{
					Cursor.Current = Cursors.WaitCursor;

					if (PlayableContent?.Playable ?? false)
						PlayableContent.Play();
					else if (ParentContent?.Playable ?? false)
						ParentContent.Play();
					else if (ConnectionHandler.IsConnected && (DownloadableContent?.CanBeDownloaded ?? false))
						DownloadableContent.Download();
					else
						Content.ShowInfoPage();

					Cursor.Current = Cursors.Default;
				}
				else
					Content.ShowInfoPage();
			}
			else if (e.Button == MouseButtons.Right)
				Content.ShowStrip(PointToScreen(e.Location));
		}

		public override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && Horizontal && ActionRect.Contains(e.Location))
			{
				Cursor.Current = Cursors.WaitCursor;

				if (PlayableContent?.Playable ?? false)
					PlayableContent.Play();
				else if (ParentContent?.Playable ?? false)
					ParentContent.Play();
				else if (ConnectionHandler.IsConnected && (DownloadableContent?.CanBeDownloaded ?? false))
					DownloadableContent.Download();

				Cursor.Current = Cursors.Default;
			}
			else
				base.OnMouseClick(e);
		}

		protected override void OnDotsMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				Content.ShowStrip(PointToScreen(e.Location));
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			Cursor = Enabled && !DisplayView && (
				ImageBounds.Contains(e.Location) ||
				DotsBounds.Contains(e.Location) ||
				(Horizontal && ActionRect.Contains(e.Location) && ((PlayableContent?.Playable ?? false) || (ParentContent?.Playable ?? false) || (ConnectionHandler.IsConnected && (DownloadableContent?.CanBeDownloaded ?? false)))))
				? Cursors.Hand
				: Cursors.Default;
		}

		protected override IEnumerable<Bitmap> HoverIcons
		{
			get
			{
				yield return ProjectImages.Icon_Info;

				if (!Horizontal && ((PlayableContent?.Playable ?? false) || (ParentContent?.Playable ?? false)))
					yield return ProjectImages.Icon_PlaySlick;
				else if (!Horizontal && (ConnectionHandler.IsConnected && (DownloadableContent?.CanBeDownloaded ?? false)))
					yield return ProjectImages.Icon_Download;
			}
		}

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				yield return Content.NewBanner;

				if (Content.AirDate < DateTime.Today)
				{
					if (!(PlayableContent?.Watched ?? true))
						yield return new Banner(Horizontal ? string.Empty : "NOT WATCHED", BannerStyle.Yellow, ProjectImages.Tiny_Unwatched);
					else if ((ParentContent?.UnwatchedContent ?? 0) > 0)
						yield return new Banner(Horizontal ? string.Empty : $"{ParentContent.UnwatchedContent} {ParentContent.ContentType}".Plural(ParentContent.UnwatchedContent), BannerStyle.Yellow, ProjectImages.Tiny_Unwatched);
				}

				if (Content.Rating.Loved)
					yield return new Banner("Loved", BannerStyle.Red, ProjectImages.Tiny_Love);

				if (Content.Rating.Rated)
					yield return new Banner(Content.Rating.Rating.ToString("0.#"), Content.Rating.Rating.RatingColor(), ProjectImages.Tiny_Rating);

				if ((ParentContent?.ContentRating ?? 0) > 0)
					yield return new Banner(ParentContent.ContentRating.ToString("0.#"), ParentContent.ContentRating.RatingColor(), ProjectImages.Tiny_Stars);

				var rating = Content.VoteAverage;
				var votes = Content.VoteCount;

				if (votes > 0)
					yield return new Banner(rating.ToString("0.#"), rating.RatingColor(), ProjectImages.Tiny_Star);

				if (PlayableContent?.WatchDate > DateTime.MinValue)
					yield return new Banner((!Horizontal ? $"Watched " : "") + PlayableContent.WatchDate.ToReadableString(PlayableContent.WatchDate.Year != DateTime.Now.Year, ExtensionClass.DateFormat.MDY, false), BannerStyle.Text, ProjectImages.Tiny_Watched);

				foreach (var item in SearchTags.WhereNotEmpty())
					yield return new Banner(item, BannerStyle.Text, ProjectImages.Tiny_Search);
			}
		}

		public override void CalculateSize(PaintEventArgs e)
		{
			if (Dock == DockStyle.Top && Horizontal)
				Height = (int)(160 * UI.FontScale);
			else
				Size = UI.Scale(Horizontal ? new Size(425, 160) : new Size(275, 220), UI.FontScale);

			var width = Bounds.Width;

			ImageBounds = (Horizontal
				   ? new Rectangle(0, 0, Height * 2 / 3, Height)
				   : new Rectangle(0, 0, width, width * 9 / 16)).Pad(1);

			ActionRect = new Rectangle(width - 40, width - ImageBounds.Width < 20 ? ImageBounds.Top + ImageBounds.Height + 4 : 4, 16, 16);
		}

		protected override void OnDraw(PaintEventArgs e)
		{
			//Size = UI.Scale(Horizontal ? new Size(350, 135) : new Size(275, 220), UI.FontScale);

			var progress = PlayableContent?.Progress ?? 0;
			var barRect = Enabled && progress > 0
				? new Rectangle(3 + ImageBounds.Left, ImageBounds.Top + ImageBounds.Height - (int)(5 * UI.FontScale) - 2, ImageBounds.Width - 5, (int)(5 * UI.FontScale))
				: Rectangle.Empty;

			if (Enabled && progress > 0)
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(175, FormDesign.Design.AccentColor)), barRect);

				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, FormDesign.Design.ActiveColor))
					, new RectangleF(barRect.X, barRect.Y, (float)(barRect.Width * progress / 100), barRect.Height));
			}

			if (Content == null)
				return;

			DrawTextOnImage(e, Content.Type.ToString().FormatWords().ToUpper(), false, barRect.Height);

			if (!Horizontal)
				DrawTextOnImage(e, Content.ToolTipText?.ToUpper(), true, barRect.Height);

			if (Horizontal)
			{
				if ((PlayableContent?.Playable ?? false) || (ParentContent?.Playable ?? false))
					e.Graphics.DrawImage(ProjectImages.Tiny_Play.Color(ActionRect.Contains(PointToClient(Cursor.Position)) ? FormDesign.Design.ActiveColor : FormDesign.Design.IconColor), ActionRect);
				else if (ConnectionHandler.IsConnected && (DownloadableContent?.CanBeDownloaded ?? false))
					e.Graphics.DrawImage(ProjectImages.Tiny_Download.Color(ActionRect.Contains(PointToClient(Cursor.Position)) ? FormDesign.Design.GreenColor : FormDesign.Design.IconColor), ActionRect);
			}

			DrawText(e, Content.Name, UI.Font(9.75F, FontStyle.Bold), FormDesign.Design.ForeColor, rigthPad: 40);
			DrawText(e, Content.SubInfo, UI.Font(8.25F), FormDesign.Design.LabelColor);

			var txt = $"No {Content.Type.If(ContentType.Movie, "release", "air")} date yet";
			if (Content.AirDate != null)
			{
				if (Content.AirDate < DateTime.Today)
					txt = $"{Content.Type.If(ContentType.Movie, "Released", "Aired")} {Content.AirDate?.RelativeString()}";
				else
					txt = $"{Content.Type.If(ContentType.Movie, "Premieres", "Airs")} {Content.AirDate?.RelativeString()}";
			}

			DrawText(e, txt, UI.Font(6.75F), FormDesign.Design.InfoColor.MergeColor(Horizontal ? FormDesign.Design.ForeColor : FormDesign.Design.LabelColor));

			if (Horizontal)
				DrawText(e, Content.Overview.IfEmpty("No overview"), UI.Font(6.75F), FormDesign.Design.InfoColor.MergeColor(FormDesign.Design.LabelColor), fill: true);
		}
	}
}