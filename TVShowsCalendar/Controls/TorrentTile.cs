using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class TorrentTile : SlickListControl<Torrent>
	{
		private ExtensionClass.action hoveredAction;
		private Bitmap[] signalLevels;
		private Bitmap[] sources;

		public QualityFilter QualityFilter { get; set; }
		public bool Reversed { get; set; }
		public TorrentSortOption SortOption { get; set; }
		public int Torrents => Items.Count(x => QualityFilter == QualityFilter.All || x.SortingData.Quality == QualityFilter);

		public TorrentTile()
		{
			InitializeComponent();

			ItemHeight = 28;

			CanDrawItem += torrentTile_CanDrawItem;
		}

		public override void Add(Torrent torrent)
		{
			if (!Items.Any(x => x.URL == torrent.URL))
			{
				torrent.Tile = this;
				base.Add(torrent);
			}
		}

		protected override void DesignChanged(FormDesign design)
		{
			signalLevels = new[]
			{
				Properties.Resources.Tiny_Signal_0.Color(design.IconColor),
				Properties.Resources.Tiny_Signal_1.Color(design.RedColor),
				Properties.Resources.Tiny_Signal_2.Color(design.YellowColor),
				Properties.Resources.Tiny_Signal_3.Color(design.GreenColor),
			};

			sources = new[]
			{
				Properties.Resources.Tiny_Z.Color(design.IconColor),
				Properties.Resources.Tiny_Pirate.Color(design.IconColor),
				Properties.Resources.Tiny_X.Color(design.IconColor),
				Properties.Resources.Tiny_Nyaa.Color(design.IconColor),
				Properties.Resources.Tiny_Rarbg.Color(design.IconColor),
				Properties.Resources.Tiny_YTS.Color(design.IconColor),
			};
		}

		protected override void OnPaintItem(ItemPaintEventArgs<Torrent> e)
		{
			var torrent = e.Item;
			var cur = PointToClient(Cursor.Position);
			var tab = (int)(55 * UI.FontScale);
			var stringFormat = new StringFormat { Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };

			hoveredAction = null;

			e.Graphics.Clear(e.HoverState.HasFlag(HoverState.Hovered) ? BackColor.Tint(Lum: FormDesign.Design.Type.If(FormDesignType.Dark, 3, -3)) : BackColor);

			using (var font = UI.Font(6.75F))
			using (var highbrush = new SolidBrush(FormDesign.Design.ActiveColor))
			using (var brush = new SolidBrush(FormDesign.Design.LabelColor))
			{
				var rect = e.ClipRectangle;
				var x = Width - 40;
				var btnSize = (int)(22 * UI.FontScale);
				var btnRect = new Rectangle(x + (40 - btnSize) / 2, rect.Y + (rect.Height - btnSize) / 2, btnSize, btnSize);

				SlickButton.GetColors(out var fore, out var back, btnRect.Contains(cur) ? e.HoverState : HoverState.Normal, ColorStyle.Green);
				e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				e.Graphics.FillRoundedRectangle(new SolidBrush(back), btnRect, 5);
				e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
				if (torrent.Loading)
				{
					DrawLoader(e.Graphics, new Rectangle(btnRect.X + 1 + (btnRect.Width - 16) / 2, btnRect.Y + 1 + (btnRect.Height - 16) / 2, 16, 16), e.HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveForeColor.Tint(FormDesign.Design.GreenColor) : FormDesign.Design.GreenColor);
				}
				else
				{
					e.Graphics.DrawImage(Properties.Resources.Tiny_Download.Color(fore), new Rectangle(btnRect.X + 1 + (btnRect.Width - 16) / 2, btnRect.Y + 1 + (btnRect.Height - 16) / 2, 16, 16));
					if (btnRect.Contains(cur))
					{
						hoveredAction = torrent.Download;
					}
				}

				e.Graphics.DrawLine(new Pen(FormDesign.Design.AccentBackColor, 1F), 0, rect.Y, Width, rect.Y);

				x -= 40;

				if (e.HoverState.HasFlag(HoverState.Hovered))
				{
					e.Graphics.DrawString(torrent.Seeders.ToString(), font, new SolidBrush(FormDesign.Design.GreenColor), new Rectangle(x, rect.Y + 3, 40, rect.Height), new StringFormat { Alignment = StringAlignment.Center });
					e.Graphics.DrawString(torrent.Leechers.ToString(), font, new SolidBrush(FormDesign.Design.RedColor), new Rectangle(x, rect.Y, 40, rect.Height - 1), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far });
				}
				else
				{
					e.Graphics.DrawImage(signalLevels[((int)Math.Floor(torrent.Health / 25)).Between(0, 3)], new Rectangle(x + (40 - 16) / 2, rect.Y + (rect.Height - 16) / 2, 16, 16));
				}

				x -= tab;
				e.Graphics.DrawString(torrent.Size, font, brush, new Rectangle(x, rect.Y + (rect.Height - font.Height) / 2, tab, font.Height), stringFormat);

				x -= tab;
				e.Graphics.DrawString(torrent.Quality, font, Data.Options.PrefferedQuality == (int)torrent.SortingData.Quality ? highbrush : brush, new Rectangle(x, rect.Y + (rect.Height - font.Height) / 2, tab, font.Height), stringFormat);

				x -= tab;
				e.Graphics.DrawString(torrent.Sound, font, brush, new Rectangle(x, rect.Y + (rect.Height - font.Height) / 2, tab, font.Height), stringFormat);

				x -= tab * 3;
				e.Graphics.DrawString(torrent.Subs, font, brush, new Rectangle(x, rect.Y + (rect.Height - font.Height) / 2, tab * 3, font.Height), new StringFormat { Alignment = StringAlignment.Far, Trimming = StringTrimming.EllipsisCharacter });

				e.Graphics.DrawImage(sources[(int)torrent.Source], new Rectangle(4, rect.Y + (rect.Height - 16) / 2, 16, 16));

				var nameHovered = e.HoverState.HasFlag(HoverState.Hovered) && new Rectangle(25, rect.Y + (rect.Height - Font.Height) / 2, x - 5, Font.Height).Contains(cur);
				if (nameHovered)
				{
					if (nameHovered = new Rectangle(new Point(25, rect.Y + (rect.Height - Font.Height) / 2), e.Graphics.Measure(torrent.Name, Font).ToSize()).Contains(cur))
					{
						hoveredAction = torrent.Open;
					}
				}

				using (var namebrush = new SolidBrush(FormDesign.Design.ForeColor))
				{
					e.Graphics.DrawString(torrent.Name, nameHovered ? new Font(Font, FontStyle.Underline) : Font, namebrush, new Rectangle(25, rect.Y + (rect.Height - Font.Height) / 2, x - 5, Font.Height), new StringFormat { Trimming = StringTrimming.EllipsisCharacter });
				}
			}
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			hoveredAction?.Invoke();

			if (Data.Options.DownloadBehavior && e.Button == MouseButtons.Left && Loading)
			{
				Data.Mainform.PushBack();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Cursor = hoveredAction == null ? Cursors.Default : Cursors.Hand;
		}

		protected override IEnumerable<DrawableItem<Torrent>> OrderItems(IEnumerable<DrawableItem<Torrent>> items)
		{
			switch (SortOption)
			{
				case TorrentSortOption.Name:
					return order(x => $"{(int)x.Source}{x.SortingData.Name}");

				case TorrentSortOption.Subs:
					return order(x => x.SortingData.Subs);

				case TorrentSortOption.Sound:
					return order(x => x.SortingData.Sound);

				case TorrentSortOption.Res:
					return order(x => x.SortingData.Quality);

				case TorrentSortOption.Size:
					return order(x => x.SortingData.Size);

				case TorrentSortOption.Health:
					return order(x => x.SortingData.Health);
			}

			return order(x => x.SortingData.Health);

			IEnumerable<DrawableItem<Torrent>> order<T>(Func<Torrent, T> conv)
			{
				var sorted = (SortOption == TorrentSortOption.Name ? !Reversed : Reversed)
					? items.OrderBy(x => conv(x.Item))
					: items.OrderByDescending(x => conv(x.Item));

				if (SortOption != TorrentSortOption.Health)
				{
					sorted = sorted.ThenByDescending(x => x.Item.Health);
				}

				return sorted;
			}
		}

		private void torrentTile_CanDrawItem(object sender, CanDrawItemEventArgs<Torrent> e)
		{
			e.DoNotDraw = QualityFilter != QualityFilter.All && e.Item.SortingData.Quality != QualityFilter;
		}
	}
}