using Extensions;

using SlickControls;

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public partial class MediaViewer : SlickAdvancedImageControl
	{
		public LightContent SearchData { get; private set; }
		private bool IsMovie => SearchData?.Movie ?? false;
		private readonly bool disposeOnLoad;

		public MediaViewer(LightContent content) : this(content.PosterPath)
		{
			SearchData = content;

			SlickTip.SetTo(this, content.Name, content.Overview.IfEmpty("No overview"));

			if (IsMovie)
			{
				if (disposeOnLoad = MovieManager.IsDisliked(content.Id))
					Hide();
			}
			else
			{
				if (disposeOnLoad = ShowManager.IsDisliked(content.Id))
					Hide();
			}

			DefaultImage = IsMovie ? ProjectImages.Huge_Movie : ProjectImages.Huge_TV;
		}

		protected override void UIChanged()
		{
			Size = UI.Scale(new Size(350, 135), UI.FontScale);
			ImageBounds = new Rectangle(new Point(1, 1), UI.Scale(new Size(88, 133), UI.FontScale));
		}

		public MediaViewer(string data)
		{
			InitializeComponent();
			this.GetImage(data, 82, false);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
				showPanel();
			else if (e.Button == MouseButtons.Left && new Rectangle(Width - 20, 4, 16, 16).Contains(e.Location))
				add();
			else
				base.OnMouseClick(e);
		}

		private void add()
		{
			if (!Added)
			{
				if (ConnectionHandler.IsConnected)
				{
					if (IsMovie)
						MovieManager.Add(MovieManager.TemporaryMovies.FirstOrDefault(x => x.Id == SearchData.Id) ?? new Movie((LightContent)SearchData), true);
					else
						ShowManager.Add(ShowManager.TemporaryShows.FirstOrDefault(x => x.Id == SearchData.Id) ?? new TvShow((LightContent)SearchData), true);

					Invalidate();
				}
				else
					Notification.Create(
						"No Connection",
						"You are not connected to the internet to interact with online content",
						PromptIcons.Hand, null,
						NotificationSound.None,
						new Size(250, 70))
						.Show(Data.Mainform, 5);
			}
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				showPanel();
			else if (e.Button == MouseButtons.Right && HasLoaded)
			{
				if (Added)
				{
					if (IsMovie)
						MovieManager.Movie(SearchData.Id)?.ShowStrip();
					else
						ShowManager.Show(SearchData.Id)?.ShowStrip();

					return;
				}

				SlickToolStrip.Show(Data.Mainform, PointToScreen(e.Location),
					new SlickStripItem("Add To Library", add, image: ProjectImages.Tiny_Add),

					new SlickStripItem("Download"
						, () => Data.Mainform.PushPanel(null, new PC_Download(new Movie { Title = SearchData.Name, Overview = SearchData.Overview }))
						, image: ProjectImages.Tiny_Download
						, show: IsMovie
						, fade: !ConnectionHandler.IsConnected),

					new SlickStripItem("More Info", showPanel, image: ProjectImages.Tiny_Info),

					SlickStripItem.Empty,

					new SlickStripItem("Suggest Less", () =>
					{
						if (IsMovie)
						{
							Data.Preferences.MoviesDisliked.Add(SearchData.Id);
						}
						else
						{
							Data.Preferences.ShowsDisliked.Add(SearchData.Id);
						}

						Data.Preferences.Save();

						Dispose();
					}, image: ProjectImages.Tiny_Dislike));
			}
		}

		private void showPanel()
		{
			if (!Added)
			{
				if (ConnectionHandler.IsConnected)
				{
					if (IsMovie)
						Data.Mainform.PushPanel(null, new PC_MoviePage(MovieManager.TemporaryMovies.FirstOrDefault(x => x.Id == SearchData.Id) ?? new Movie((LightContent)SearchData, true)));
					else
						Data.Mainform.PushPanel(null, new PC_ShowPage(ShowManager.TemporaryShows.FirstOrDefault(x => x.Id == SearchData.Id) ?? new TvShow((LightContent)SearchData, true)));

					Invalidate();
				}
				else
					Notification.Create(
						"No Connection",
						"You are not connected to the internet to interact with online content",
						PromptIcons.Hand, null,
						NotificationSound.None,
						new Size(250, 70))
						.Show(Data.Mainform, 5);
			}
			else
			{
				if (IsMovie)
					Data.Mainform.PushPanel(null, new PC_MoviePage(MovieManager.Movie(SearchData.Id)));
				else
					Data.Mainform.PushPanel(null, new PC_ShowPage(ShowManager.Show(SearchData.Id)));
			}
		}

		private void MediaViewer_MouseMove(object sender, MouseEventArgs e)
		{
			if (!Added && new Rectangle(Width - 20, 6, 16, 16).Contains(e.Location))
				Cursor = Cursors.Hand;
		}

		private bool HasLoaded
		{
			get
			{
				if (Added)
				{
					if (IsMovie)
						return MovieManager.Movie(SearchData.Id) != null;
					else
						return ShowManager.Show(SearchData.Id) != null;
				}

				return true;
			}
		}

		private bool Added
		{
			get
			{
				if (IsMovie)
					return MovieManager.Movie(SearchData.Id) != null;
				else
					return ShowManager.Show(SearchData.Id) != null;
			}
		}

		protected override void OnCreateControl()
		{
			if (disposeOnLoad)
				Dispose();
			else
				base.OnCreateControl();
		}

		protected override IEnumerable<Bitmap> HoverIcons { get; } = new[] { ProjectImages.Icon_Info };

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				double rating = SearchData.VoteAverage;
				int votes = SearchData.VoteCount;

				if (votes > 0)
					yield return new Banner(rating.ToString("0.##"), rating.RatingColor(), ProjectImages.Tiny_Star);

				yield return new Banner(string.Empty, BannerStyle.Text, IsMovie ? ProjectImages.Tiny_Movie : ProjectImages.Tiny_TV);
			}
		}

		protected override void OnDraw(PaintEventArgs e)
		{
			DrawTextOnImage(e, IsMovie ? "MOVIE" : "TV SHOW", false);

			if (Added)
				e.Graphics.DrawImage(ProjectImages.Tiny_Ok.Color(FormDesign.Design.GreenColor), Width - 20, 4, 16, 16);
			else
				e.Graphics.DrawImage(ProjectImages.Tiny_Add.Color(new Rectangle(Width - 20, 6, 16, 16).Contains(CursorLocation) ? FormDesign.Design.ActiveColor : FormDesign.Design.IconColor), Width - 20, 4, 16, 16);

			DrawText(e, SearchData.Name, UI.Font(9.75F, FontStyle.Bold), FormDesign.Design.ForeColor, rigthPad: 40);

			DrawText(e, SearchData.GenreIds.Select(x => IsMovie ? Data.TMDbHandler.GetMovieGenre(x).Name : Data.TMDbHandler.GetTvGenre(x).Name).Where(x => !string.IsNullOrWhiteSpace(x)).Take(2).ListStrings(" • ")
				, UI.Font(8.25F), FormDesign.Design.LabelColor);

			DrawText(e, SearchData.ReleaseDate != null ? SearchData.ReleaseDate?.ToReadableString() : IsMovie.If("No release date", "No air date"), UI.Font(6.75F), FormDesign.Design.InfoColor);

			DrawText(e, SearchData.Overview.IfEmpty("No overview"), UI.Font(6.75F), FormDesign.Design.InfoColor, fill: true);
		}
	}
}