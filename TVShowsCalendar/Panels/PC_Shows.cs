using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public partial class PC_Shows : PanelContent
	{
		private readonly Dictionary<TvShow, ContentControl<TvShow>> Tiles = new Dictionary<TvShow, ContentControl<TvShow>>();
		private string lastSearch;
		private readonly ContentListControl<TvShow> ListControl;

		public PC_Shows()
		{
			InitializeComponent();

			FirstFocusedControl = TB_Search;

			ListControl = new ContentListControl<TvShow>()
			{
				Name = "ListControl",
				Dock = DockStyle.Top,
				Parent = P_Tabs,
				OrderFunction = OrderItems
			};

			SlickTip.SetTo(PB_Search, "Click to Search");

			ShowManager.ShowAdded += ShowManager_ShowAdded;
		}

		private void SetLoading()
		{
			PB_FirstLoad.Visible = false;

			if (Data.Options.ShowSorting == MediaSortOptions.Default && Data.Preferences.ShowsView == ViewType.Grid)
			{
				P_Tabs.SuspendDrawing();
				SP_Airing.BringToFront();
				SP_Upcoming.BringToFront();
				SP_Returning.BringToFront();
				SP_Ended.BringToFront();
				P_Tabs.ResumeDrawing();
			}
		}

		private void ShowManager_ShowAdded(TvShow show) => this.TryInvoke(() =>
		{
			SetLoading();
			TLP_NoShows.Visible = false;
		});

		private void AddShow(TvShow show, ContentControl<TvShow> tile = null)
		{
			if (show.Id == 0)
				return;

			if (Data.Preferences.ShowsView == ViewType.List)
			{
				if (!ListControl.Items.Any(s => s.Equals(show)))
					ListControl.Add(show);
				return;
			}

			var panel = SP_Returning;

			switch (Data.Options.ShowSorting)
			{
				case MediaSortOptions.Default:
					if (show.Status == "Ended" || show.Status == "Canceled")
					{
						panel = SP_Ended;
					}
					else if (((!show.LastEpisode?.Empty ?? false) && show.LastEpisode.AirDate >= DateTime.Today.AddDays(-7))
						|| ((!show.NextEpisode?.Empty ?? false) && show.NextEpisode.AirDate <= DateTime.Today.AddDays(7)))
					{
						panel = SP_Airing;
					}
					else if ((!show.NextEpisode?.Empty ?? false) && show.NextEpisode.AirState == AirStateEnum.ToBeAired)
					{
						panel = SP_Upcoming;
					}
					break;

				case MediaSortOptions.Year:
					var year = show.LastAirDate?.Year.ToString() ?? "Unknown";
					panel = P_Tabs.Controls.OfType<SlickSectionPanel>().FirstOrDefault(x => x.Text == year) ?? createSection(year, Properties.Resources.Big_Calendar);
					break;

				case MediaSortOptions.Name:
					var name = show.Name.Substring(0, 1).ToUpper();
					panel = P_Tabs.Controls.OfType<SlickSectionPanel>().FirstOrDefault(x => x.Text == name) ?? createSection(name, Properties.Resources.Big_Label);
					break;

				case MediaSortOptions.Genre:
					var genre = show.Genres.FirstOrDefault()?.Name ?? "Unknown";
					panel = P_Tabs.Controls.OfType<SlickSectionPanel>().FirstOrDefault(x => x.Text == genre) ?? createSection(genre, Properties.Resources.Big_Genre);
					break;

				default:
					break;
			}

			if (tile == null)
			{
				foreach (var item in P_Tabs.Controls.ThatAre<SlickSectionPanel>())
					item.Controls.Clear(true, x => (x is ContentControl<TvShow> st) && st.Content == show);

				tile = new ContentControl<TvShow>(show);

				Tiles.TryAdd(show, tile);

				if (CheckSearch(tile))
					panel.Controls.Add(tile);
			}
			else
			{
				panel.Controls.Add(tile);
			}
		}

		private SlickSectionPanel createSection(string text, Image icon)
		{
			var ssp = new SlickSectionPanel()
			{
				Text = text,
				AutoHide = true,
				Dock = DockStyle.Top,
				AutoSize = true,
				Icon = icon
			};

			P_Tabs.Controls.Add(ssp);

			switch (Data.Options.ShowSorting)
			{
				case MediaSortOptions.Year:
					P_Tabs.OrderBy(x => x.Text);
					break;

				case MediaSortOptions.Name:
					P_Tabs.OrderByDescending(x => x.Text);
					break;

				case MediaSortOptions.Genre:
					P_Tabs.OrderBy(x => (x as SlickSectionPanel).Controls.Count);
					break;

				default:
					break;
			}

			if (Data.Options.ShowSorting == MediaSortOptions.Year)
			{
				var first = P_Tabs.Controls.OfType<SlickSectionPanel>().FirstOrDefault(x => x.Text == DateTime.Now.Year.ToString());
				if (first != null)
					first.Active = true;
			}

			return ssp;
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			L_NoShowsInfo.ForeColor = design.InfoColor;
			L_NoShows.ForeColor = design.LabelColor;
			P_TopSpacer.BackColor = design.AccentColor;

			PB_Search.Image = Properties.Resources.Big_Search.Color(searchOpened ? design.ActiveColor : design.IconColor);
		}

		protected override void UIChanged()
		{
			base.UIChanged();
			B_Add.Font = B_ListGridToggle.Font = UI.Font(9.75F);
			L_NoShows.Font = UI.Font(9.75F, FontStyle.Bold);
			L_NoShowsInfo.Font = UI.Font(8.25F, FontStyle.Italic);
			TLP_NoShows.Location = TLP_NoShows.Parent.Size.Center(TLP_NoShows.Size);
		}

		private void B_Add_Click(object sender, EventArgs e)
		{
			if (ConnectionHandler.State == Extensions.ConnectionState.Connected)
			{
				Form.PushPanel(null, new PC_AddMedia(false, TB_Search.Text));
			}
			else
			{
				ShowPrompt("You can not add Shows without Internet connection.\n\nCheck your connectivity then try again.",
					"No Connection",
					 PromptButtons.OK,
					 PromptIcons.Hand);
			}
		}

		private readonly WaitIdentifier searchWaitIdentifier = new WaitIdentifier();

		private void TB_Search_TextChanged(object sender, EventArgs e)
		{
			if (searchOpened && (lastSearch == TB_Search.Text || (Tiles.Count == 0 && Data.Preferences.ShowsView == ViewType.Grid)))
				return;

			if (!searchOpened || TB_Search.Width == 0)
				toggleSearch();

			if (Data.Preferences.ShowsView == ViewType.List)
			{
				foreach (var item in ListControl.Items)
					item.Visible = string.IsNullOrWhiteSpace(TB_Search.Text) || CheckSearch(item.Item).Any(x => !string.IsNullOrWhiteSpace(x));

				ListControl.Invalidate();
				TLP_NoShows.Visible = ListControl.Items.All(x => !x.Visible);
				return;
			}

			if (!string.IsNullOrWhiteSpace(TB_Search.Text))
				PB_Search.Loading = true;

			searchWaitIdentifier.Wait(() =>
			{
				this.TryInvoke(() =>
				{
					var results = false;
					P_Tabs.SuspendDrawing();
					foreach (var item in Tiles.Values)
					{
						if (CheckSearch(item))
						{
							AddShow(item.Content, item);
							results = true;
						}
						else
						{
							item.Parent = null;
						}
					}
					P_Tabs.ResumeDrawing();

					TLP_NoShows.Visible = !results;
					PB_Search.Image = Properties.Resources.Big_Search.Color(searchOpened ? FormDesign.Design.ActiveColor : FormDesign.Design.IconColor);

					lastSearch = TB_Search.Text;
				});
			}, string.IsNullOrWhiteSpace(TB_Search.Text) ? 10 : 300);
		}

		private bool CheckSearch(ContentControl<TvShow> tile)
		{
			var show = tile.Content;
			tile.SearchTags.Clear();

			if (string.IsNullOrWhiteSpace(TB_Search.Text))
				return true;

			tile.SearchTags.AddRange(CheckSearch(show));

			return tile.SearchTags.Any(x => !string.IsNullOrWhiteSpace(x));
		}

		private IEnumerable<string> CheckSearch(TvShow show)
		{
			if (!string.IsNullOrWhiteSpace(TB_Search.Text))
			{
				var q = TB_Search.Text.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				foreach (var item in show.Keywords.Concat(show.Rating.Tags).Where(x => TB_Search.Text.SearchCheck(x)))
					yield return item;

				foreach (var item in show.Genres.Where(x => q.Any(y => y.Contains(x.Name.ToLower()))).Select(x => x.Name))
					yield return item;

				yield return show.Episodes.FirstOrDefault(x => q.Any(y => y == x.AirDate?.Year.ToString()))?.AirDate?.Year.ToString();

				if ((q.Any("not") && q.Any(x => Regex.IsMatch(x, "^wa[$t]?[$c]?[$h]?[$e]?[$d]?$")) || q.Any(x => Regex.IsMatch(x, "^unwa[$t]?[$c]?[$h]?[$e]?[$d]?$")))
					&& show.Episodes.Any(x => x.AirState == AirStateEnum.Aired && !x.Watched))
					yield return "Not Watched";

				if (!q.Any("not") && q.Any(x => Regex.IsMatch(x, "^wa[$t]?[$c]?[$h]?[$e]?[$d]?$"))
					&& !show.Episodes.Any(x => x.AirState == AirStateEnum.Aired && !x.Watched))
					yield return "Watched";

				if (q.Any("loved") && show.Rating.Loved)
					yield return "Loved";

				if (q.Any("download") && show.Episodes.Any(x => !x.Watched && x.CanBeDownloaded && !x.Playable))
					yield return "To Download";
			}
		}

		private bool searchOpened = false;
		private AnimationHandler searchAnimation;

		private void PB_Search_Click(object sender, EventArgs e)
		{
			TB_Search.Text = string.Empty;
			toggleSearch();
		}

		private void toggleSearch()
		{
			searchAnimation?.Dispose();

			searchOpened = !searchOpened;
			if (searchOpened)
			{
				TB_Search.Show();
				(searchAnimation = new AnimationHandler(TB_Search, new Size((int)(350 * UI.FontScale), TB_Search.Height), AnimationOption.IgnoreHeight)).StartAnimation();
				SlickTip.SetTo(PB_Search, "Close Search");

				if (!TB_Search.Focused)
					TB_Search.Focus();
			}
			else
			{
				(searchAnimation = new AnimationHandler(TB_Search, new Size(TB_Search.MinimumSize.Width, TB_Search.Height), AnimationOption.IgnoreHeight)).StartAnimation(TB_Search.Hide);
				SlickTip.SetTo(PB_Search, "Click to Search");
			}

			PB_Search.Image = Properties.Resources.Big_Search.Color(searchOpened ? FormDesign.Design.ActiveColor : FormDesign.Design.IconColor);
		}

		public override bool KeyPressed(ref Message msg, Keys keyData)
		{
			if (searchOpened && keyData == Keys.Escape)
			{
				TB_Search.Text = string.Empty;
				toggleSearch();
				return true;
			}

			if (keyData == (Keys.Control | Keys.A) && string.IsNullOrWhiteSpace(TB_Search.Text))
				B_Add_Click(null, null);

			if (!searchOpened && !TB_Search.Focused && keyData.IsDigitOrLetter())
			{
				toggleSearch();
				return true;
			}
			else if (!searchOpened && TB_Search.Width < 30 && keyData.IsDigitOrLetter())
				TB_Search.Width = 30;

			return base.KeyPressed(ref msg, keyData);
		}

		private void verticalScroll_Scroll(object sender, ScrollEventArgs e) => P_TopSpacer.Visible = e.NewValue > 0;

		private void PC_Shows_Load(object sender, EventArgs e)
		{
			if (Data.Options.ShowSorting != MediaSortOptions.Default || Data.Preferences.ShowsView == ViewType.List)
				ApplyView();
			else
				Form.OnNextIdle(ApplyView);
		}

		private void B_ListGridToggle_Click(object sender, EventArgs e)
		{
			Data.Preferences.ShowsView = Data.Preferences.ShowsView == ViewType.List ? ViewType.Grid : ViewType.List;
			Data.Preferences.Save();

			ApplyView();
		}

		private void ApplyView()
		{
			P_Tabs.Controls.OfType<SlickSectionPanel>().Foreach(x => x.Controls.Clear(true));
			P_Tabs.Controls.Clear(true, x => string.IsNullOrWhiteSpace(x.Name));
			ListControl.Clear();
			ListControl.Visible = C_TopHeader.Visible = Data.Preferences.ShowsView == ViewType.List;
			verticalScroll.SmallHandle = Data.Preferences.ShowsView == ViewType.List;
			B_ListGridToggle.Image = Data.Preferences.ShowsView != ViewType.List ? Properties.Resources.Tiny_List : Properties.Resources.Tiny_Grid;

			foreach (var show in ShowManager.Shows)
				AddShow(show);

			TLP_NoShows.Visible = !ShowManager.Shows.Any();
			SetLoading();
		}

		private enum Column
		{
			Name,
			Loved,
			UserRating,
			Rating,
			Watch,
			New,
		}

		private Column hoveredItem;
		private Column sorting;
		private bool reversed = true;

		private IEnumerable<DrawableItem<ImageContent<TvShow>>> OrderItems(IEnumerable<DrawableItem<ImageContent<TvShow>>> items)
		{
			switch (sorting)
			{
				case Column.Loved: return order(x => x.Rating.Loved);
				case Column.UserRating: return order(x => x.Rating.Rated ? x.Rating.Rating : x.ContentRating);
				case Column.Rating: return order(x => x.VoteAverage);
				case Column.Watch: return order(x => x.UnwatchedContent);
				case Column.New: return order(x => x.NewBanner != null);
				default: return order(x => x.Name);
			}

			IEnumerable<DrawableItem<ImageContent<TvShow>>> order<T>(Func<TvShow, T> conv)
			{
				var sorted = (sorting == Column.Name ? !reversed : reversed)
					? items.OrderByDescending(x => conv(x.Item.Item))
					: items.OrderBy(x => conv(x.Item.Item));

				return sorted;
			}
		}

		private void C_TopHeader_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(FormDesign.Design.BackColor);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			hoveredItem = (Column)(-1);

			var cur = C_TopHeader.PointToClient(Cursor.Position);
			var rect = new Rectangle(C_TopHeader.Width - 64, 0, 64, C_TopHeader.Height);

			e.Graphics.DrawImage((!reversed ? ProjectImages.ArrowUp : ProjectImages.ArrowDown).Color(FormDesign.Design.IconColor), rect, ImageSizeMode.Center);

			rect.X -= rect.Width*2;

			drawInfo(ProjectImages.Tiny_Love, Column.Loved, FormDesign.Design.RedColor, rect);

			rect.X -= rect.Width;

			drawInfo(ProjectImages.Tiny_Rating, Column.UserRating, FormDesign.Design.GreenColor, rect);

			rect.X -= rect.Width;

			drawInfo(ProjectImages.Tiny_Star, Column.Rating, FormDesign.Design.ActiveColor, rect);

			rect.X -= rect.Width;

			drawInfo(ProjectImages.Tiny_Watched, Column.Watch, FormDesign.Design.YellowColor, rect);

			rect.X -= rect.Width;

			drawInfo(ProjectImages.Tiny_New, Column.New, FormDesign.Design.ActiveColor, rect);

			rect = new Rectangle(0, 0, rect.X - rect.Width, rect.Height);

			drawInfo(ProjectImages.Tiny_Label, Column.Name, FormDesign.Design.ActiveColor, rect);

			C_TopHeader.Cursor = (int)hoveredItem == -1 ? Cursors.Default : Cursors.Hand;

			void drawInfo(Bitmap img, Column column, Color color, Rectangle rectangle)
			{
				if (C_TopHeader.HoverState.HasFlag(HoverState.Hovered) && rectangle.Contains(cur))
					hoveredItem = column;

				if (column == sorting)
					e.Graphics.FillRoundedRectangle(new SolidBrush(color), rectangle.CenterR(24, 24).Pad(0, 0, 1, 1), 7);

				e.Graphics.DrawImage(img.Color(column == sorting ? FormDesign.Design.BackColor : hoveredItem == column ? color : FormDesign.Design.IconColor), rectangle, ImageSizeMode.Center);
			}
		}

		private void C_TopHeader_MouseClick(object sender, MouseEventArgs e)
		{
			if ((int)hoveredItem != -1 && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right))
			{
				if (e.Button == MouseButtons.Left && sorting == hoveredItem)
					reversed = !reversed;
				else
					reversed = e.Button == MouseButtons.Left;

				sorting = hoveredItem;
				ListControl.Invalidate();
			}
		}
	}
}