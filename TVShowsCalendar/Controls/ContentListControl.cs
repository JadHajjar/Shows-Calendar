using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public class ContentListControl<T> : SlickListControl<ImageContent<T>> where T : IContent, IInteractableContent
	{
		private Action<T, MouseEventArgs> HoveredAction;

		public Func<IEnumerable<DrawableItem<ImageContent<T>>>, IEnumerable<DrawableItem<ImageContent<T>>>> OrderFunction;

		public ContentListControl()
		{
			ItemHeight = 42;

			CanDrawItem += (s, e) => e.DoNotDraw = !e.Item.Visible;
		}

		public void Add(T item)
		{
			if (Items.Any(x => x.Item.Equals(item)))
				return;

			var c = new ImageContent<T> { Item = item, Visible = true };

			item.InfoChanged += (s, e) => this.TryInvoke(Invalidate);
			item.ContentRemoved += (s, e) => this.TryInvoke(() => Remove(c));

			new BackgroundAction(() => { c.Image = ImageHandler.GetImage(item.BackdropPath, 40, false, true); Invalidate(c); }).Run();

			Add(c);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (var item in Items)
					item.Image?.Dispose();
			}

			base.Dispose(disposing);
		}

		protected override IEnumerable<DrawableItem<ImageContent<T>>> OrderItems(IEnumerable<DrawableItem<ImageContent<T>>> items)
			=> OrderFunction?.Invoke(items) ?? base.OrderItems(items);

		protected override void OnItemMouseClick(DrawableItem<ImageContent<T>> item, MouseEventArgs e)
		{
			HoveredAction?.Invoke(item.Item.Item, e);
		}

		protected override void OnPaintItem(ItemPaintEventArgs<ImageContent<T>> e)
		{
			var Content = e.Item.Item;
			var ParentContent = Content as IParentContent;
			var DownloadableContent = Content as IDownloadableContent;
			var PlayableContent = Content as IPlayableContent;
			var imgRect = new Rectangle(0, e.ClipRectangle.Y, 7 * e.ClipRectangle.Height / 4, e.ClipRectangle.Height).Pad(3);

			if (e.HoverState.HasFlag(HoverState.Hovered))
				e.Graphics.FillRectangle(Gradient(FormDesign.Design.AccentBackColor, e.ClipRectangle, .5F), e.ClipRectangle);
			else
				e.Graphics.Clear(FormDesign.Design.BackColor);

			e.Graphics.DrawLine(new Pen(FormDesign.Design.AccentBackColor, 1F), 0, e.ClipRectangle.Y, Width, e.ClipRectangle.Y);

			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			e.Graphics.DrawBorderedImage(e.Item.Image ?? Content.BigIcon.Color(FormDesign.Design.IconColor), imgRect, ImageSizeMode.Fill);

			if (e.HoverState.HasFlag(HoverState.Hovered) && imgRect.Contains(CursorLocation))
			{
				HoveredAction = ImageClick;

				e.Graphics.DrawIconsOverImage(imgRect, CursorLocation, ProjectImages.Tiny_Info);
			}

			var textRect = new Rectangle(imgRect.Width + 9, e.ClipRectangle.Y, 500, UI.Font(9.75F, FontStyle.Bold).Height + UI.Font(7.5F).Height + 2);
			textRect.Y += (e.ClipRectangle.Height - textRect.Height) / 2;

			e.Graphics.DrawString(
				Content.Name,
				UI.Font(9.75F, FontStyle.Bold),
				new SolidBrush(FormDesign.Design.ForeColor),
				textRect.Pad(0, 0, 0, UI.Font(7.5F).Height),
				new StringFormat { Trimming = StringTrimming.EllipsisCharacter });

			e.Graphics.DrawString(
				Content.SubInfo,
				UI.Font(7.5F),
				new SolidBrush(FormDesign.Design.LabelColor),
				textRect.Pad(2, UI.Font(9.75F, FontStyle.Bold).Height, 0, 0),
				new StringFormat { LineAlignment = StringAlignment.Far, Trimming = StringTrimming.EllipsisCharacter });

			var rect = new Rectangle(Width - 64*2, e.ClipRectangle.Y, 64, e.ClipRectangle.Height);
			bool hovered() => rect.Contains(CursorLocation);

			rect.X -= rect.Width;

			if (hovered())
			{
				drawInfo(null, Content.Rating.Loved ? ProjectImages.Tiny_Dislike : ProjectImages.Tiny_Love, FormDesign.Design.RedColor, rect);
				HoveredAction = SwitchLove;
			}
			else if (Content.Rating.Loved)
				drawInfo(null, ProjectImages.Tiny_Love, FormDesign.Design.RedColor, rect);

			rect.X -= rect.Width;

			if (hovered())
			{
				drawInfo(null, ProjectImages.Tiny_Rating, Content.Rating.Rated ? FormDesign.Design.ActiveColor : FormDesign.Design.GreenColor, rect);
				HoveredAction = Rate;
			}
			else if (Content.Rating.Rated)
				drawInfo(Content.Rating.Rating.ToString("0.#"), ProjectImages.Tiny_Rating, Content.Rating.Rating.RatingColor(), rect);
			else if (Content is IParentContent parent && parent.ContentRating > 0)
				drawInfo(parent.ContentRating.ToString("0.#"), ProjectImages.Tiny_Stars, parent.ContentRating.RatingColor(), rect);

			rect.X -= rect.Width;

			if (Content.VoteCount > 0)
				drawInfo(Content.VoteAverage.ToString("0.#"), ProjectImages.Tiny_Star, Content.VoteAverage.RatingColor(), rect);

			rect.X -= rect.Width;

			if (Content.AirDate < DateTime.Today)
			{
				var isWatched = false;
				var count = 0;

				if (!(PlayableContent?.Watched ?? true))
					isWatched = false;
				else if (PlayableContent != null)
					isWatched = true;
				else if ((ParentContent?.UnwatchedContent ?? 0) > 0)
				{
					isWatched = false;
					count = ParentContent.UnwatchedContent;
				}
				else if (ParentContent != null)
					isWatched = true;

				drawInfo(count == 0 ? null : count.ToString(), (hovered() ? !isWatched : isWatched) ? ProjectImages.Tiny_Watched : ProjectImages.Tiny_Unwatched, (hovered() ? !isWatched : isWatched) ? FormDesign.Design.GreenColor : FormDesign.Design.YellowColor, rect);
				if (hovered())
					HoveredAction = SwitchWatch;
			}

			rect.X -= rect.Width;

			if (Content.NewBanner != null)
				drawInfo(null, ProjectImages.Tiny_New, FormDesign.Design.ActiveColor, rect);

			if (DrawIconButton(e, new Rectangle(Width - 50, e.ClipRectangle.Y + (e.ClipRectangle.Height - 36) / 2, 36, 36), ProjectImages.Big_Dots_V))
				HoveredAction = DotsClicked;


			if ((PlayableContent?.Playable ?? false) || (ParentContent?.Playable ?? false))
			{
				if (DrawIconButton(e, new Rectangle(Width - 100, e.ClipRectangle.Y + (e.ClipRectangle.Height - 36) / 2, 36, 36), ProjectImages.Big_Play))
					HoveredAction = ActionClicked;
			}
			else if (ConnectionHandler.IsConnected && (DownloadableContent?.CanBeDownloaded ?? false))
			{
				if (DrawIconButton(e, new Rectangle(Width - 100, e.ClipRectangle.Y + (e.ClipRectangle.Height - 36) / 2, 36, 36), ProjectImages.Big_Download))
					HoveredAction = ActionClicked;
			}


			void drawInfo(string text, Bitmap img, Color color, Rectangle rectangle)
			{
				e.Graphics.DrawImage(img.Color(color), rectangle.Pad(0, 0, 0, string.IsNullOrWhiteSpace(text) ? 0 : UI.Font(7.5F).Height), ImageSizeMode.Center);

				if (!string.IsNullOrWhiteSpace(text))
					using (var brush = new SolidBrush(color))
						e.Graphics.DrawString(
							text,
							UI.Font(7.5F),
							brush,
							rectangle.Pad(0, UI.Font(7.5F).Height, 0, 0),
							new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter });
			}
		}

		protected bool DrawIconButton(PaintEventArgs e, Rectangle rect, Bitmap img, bool selected = false)
		{
			var hovered = rect.Contains(CursorLocation);

			e.Graphics.FillRoundedRectangle(Gradient(HoverState.HasFlag(HoverState.Pressed) && hovered && !selected ? FormDesign.Design.ActiveColor : hovered ? FormDesign.Design.BackColor : FormDesign.Design.AccentBackColor, rect), rect, 10);

			DrawIcon(e, img, rect, selected);

			if (hovered && !selected)
			{
				if (!HoverState.HasFlag(HoverState.Pressed))
					e.Graphics.DrawRoundedRectangle(new Pen(Color.FromArgb(125, FormDesign.Design.ActiveColor), 2F) { DashStyle = DashStyle.Dash }, rect, 10);

				return true;
			}

			return false;
		}

		protected bool DrawIcon(PaintEventArgs e, Bitmap icon, Rectangle rectangle, bool hovered = false, ImageSizeMode sizeMode = ImageSizeMode.Center)
		{
			if (icon != null)
			{
				var color = FormDesign.Design.IconColor;

				if (hovered)
					color = FormDesign.Design.ActiveColor;
				else if (hovered = rectangle.Contains(PointToClient(Cursor.Position)))
					color = HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveForeColor : FormDesign.Design.ActiveColor;

				e.Graphics.DrawImage(icon.Color(color), rectangle, sizeMode);

				return hovered;
			}

			return false;
		}

		private void ImageClick(T item, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
				item.ShowInfoPage();
			else if (e.Button == MouseButtons.Right)
				item.ShowStrip(PointToScreen(e.Location));
		}

		private void DotsClicked(T item, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
				item.ShowStrip(PointToScreen(e.Location));
		}

		private void ActionClicked(T item, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
			{
				Cursor.Current = Cursors.WaitCursor;
				var PlayableContent = item as IPlayableContent;
				var DownloadableContent = item as IDownloadableContent;
				var ParentContent = item as IParentContent;

				if (PlayableContent?.Playable ?? false)
					PlayableContent.Play();
				else if (ParentContent?.Playable ?? false)
					ParentContent.Play();
				else if (ConnectionHandler.IsConnected && (DownloadableContent?.CanBeDownloaded ?? false))
					DownloadableContent.Download();
				else
					item.ShowInfoPage();

				Cursor.Current = Cursors.Default;
			}
		}

		private void SwitchWatch(T item, MouseEventArgs e)
		{
			var ParentContent = item as IParentContent;
			var PlayableContent = item as IPlayableContent;
			var isWatched = false;

			if (!(PlayableContent?.Watched ?? true))
				isWatched = false;
			else if (PlayableContent != null)
				isWatched = true;
			else if ((ParentContent?.UnwatchedContent ?? 0) > 0)
				isWatched = false;
			else if (ParentContent != null)
				isWatched = true;

			if (e.Button == MouseButtons.Left)
				item.MarkAs(!isWatched);
		}

		private void Rate(T item, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				var res = RateForm.Show(item.Rating.Rated ? item.Rating.Rating : 5);
				if (res.Item1)
				{
					item.Rating = item.Rating.Rate(res.Item2);
					Invalidate();
					new BackgroundAction(() => item.Save(ChangeType.Preferences)).Run();
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				item.Rating = item.Rating.UnRate();
				Invalidate();
				new BackgroundAction(() => item.Save(ChangeType.Preferences)).Run();
			}
		}

		private void SwitchLove(T item, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
			{
				item.Rating = item.Rating.SwitchLove();
				Invalidate();
				new BackgroundAction(() => item.Save(ChangeType.Preferences)).Run();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			HoveredAction = null;

			base.OnPaint(e);

			Cursor = HoveredAction == null ? Cursors.Default : Cursors.Hand;
		}
	}
}