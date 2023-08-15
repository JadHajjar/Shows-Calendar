using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using TMDbLib.Objects.People;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public partial class SmallMediaViewer : SlickAdvancedImageControl
	{
		public CombinedCredit Credit { get; }
		public bool IsMovie { get; }
		public int Year { get; }
		public int Id { get; }
		public string HeaderText { get; }
		public string InfoText { get; }
		public string HoverText { get; }
		public Bitmap ErrorImage { get; }
		private int CreditCount { get; }

		public SmallMediaViewer(CombinedCredit item, IEnumerable<CombinedCredit> items)
		{
			InitializeComponent();

			Credit = item;
			Id = item.Id;
			IsMovie = item.IsMovie;
			HeaderText = item.IsMovie ? item.Title : item.Name;
			InfoText = string.Join(", ", items.Convert(x => x is CombinedCreditsCast role ? role.Character.IfEmpty("", $"As {role.Character}") : (x as CombinedCreditsCrew).Job).Where(x => !string.IsNullOrWhiteSpace(x)));
			HoverText = item.IsMovie ? "MOVIE" : "TV SERIES";
			DefaultImage = ErrorImage = item.IsMovie ? ProjectImages.Huge_Movie : ProjectImages.Huge_TV;
			Year = item.ReleaseDate?.Year ?? item.FirstAirDate?.Year ?? 0;
			CreditCount = items.Count();

			this.GetImage(item.PosterPath, 130, false);
			SlickTip.SetTo(this, HeaderText, InfoText);
		}

		protected override void UIChanged()
		{
			Size = UI.Scale(new Size(130, 230), UI.UIScale);
			ImageBounds = new Rectangle(1, 1, Width - 2, 3 * (Width - 2) / 2);
		}

		private async void MediaViewer_MouseClick(object sender, MouseEventArgs e)
		{
			if (!ImageBounds.Contains(e.Location) && e.Button != MouseButtons.None) return;

			if (!ConnectionHandler.IsConnected)
			{
				Notification.Create("No Connection", "You are not connected to the internet to interact with online content", PromptIcons.Hand, null, NotificationSound.None, new Size(250, 70))
					.Show(Data.Mainform, 5);

				return;
			}

			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
			{
				if (!added)
				{
					await add(true);
				}
				else
				{
					if (IsMovie)
						Data.Mainform.PushPanel(null, new PC_MoviePage(MovieManager.Movie(Id)));
					else
						Data.Mainform.PushPanel(null, new PC_ShowPage(ShowManager.Show(Id)));
				}

				Invalidate();
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (added)
				{
					if (IsMovie)
						MovieManager.Movie(Id)?.ShowStrip();
					else
						ShowManager.Show(Id)?.ShowStrip();
				}
				else
					SlickToolStrip.Show(Data.Mainform, PointToScreen(e.Location),
						new SlickStripItem("Add To Library", async () => await add(false), image: ProjectImages.Tiny_Add),

						new SlickStripItem("Download", () => Data.Mainform.PushPanel(null, new PC_Download(new Movie { Title = HeaderText }))
						, image: ProjectImages.Tiny_Download
						, show: IsMovie
						, fade: !ConnectionHandler.IsConnected),

						new SlickStripItem("More Info", async () => await add(true), image: ProjectImages.Tiny_Info));
			}
		}

		private async Task add(bool open)
		{
			if (!Loading)
			{
				Loading = true;
				try
				{
					if (IsMovie)
					{
						var movie = MovieManager.TemporaryMovies.FirstOrDefault(x => x.Id == Id) ?? await Movie.Create(Id, true);
						if (open)
							Data.Mainform.PushPanel(null, new PC_MoviePage(movie));
						else
							MovieManager.Add(movie);
					}
					else
					{
						var show = ShowManager.TemporaryShows.FirstOrDefault(x => x.Id == Id) ?? await TvShow.Create(Id, true);
						if (open)
							Data.Mainform.PushPanel(null, new PC_ShowPage(show));
						else
							ShowManager.Add(show);
					}
				}
				catch (Exception)
				{
					MessagePrompt.Show("An error occurred while trying to load some information, please try again later.", PromptButtons.OK, PromptIcons.Error, Data.Mainform);
				}
				Loading = false;
			}
		}

		private bool added
		{
			get
			{
				if (IsMovie)
					return MovieManager.Movie(Id) != null;
				else
					return ShowManager.Show(Id) != null;
			}
		}

		protected override IEnumerable<Bitmap> HoverIcons
		{
			get
			{
				if (!Loading)
					yield return ProjectImages.Icon_Info;
			}
		}

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				if (Credit.EpisodeCount > 0)
					yield return new Banner($"{Credit.EpisodeCount} Episode".Plural(Credit.EpisodeCount), BannerStyle.Active, ProjectImages.Tiny_Rating);

				if (CreditCount > 1)
					yield return new Banner($"{CreditCount} Credits", BannerStyle.Green, ProjectImages.Tiny_Hint);

				yield return new Banner($"{(Credit.IsMovie ? Credit.ReleaseDate?.Year : Credit.FirstAirDate?.Year)}", BannerStyle.Text, Credit.IsMovie ? ProjectImages.Tiny_Movie : ProjectImages.Tiny_TV);
			}
		}

		protected override void OnDraw(PaintEventArgs e)
		{
			DrawTextOnImage(e, HoverText, false);

			DrawText(e, HeaderText, UI.Font(8.25F), FormDesign.Design.ForeColor, fill: true);

			DrawText(e, InfoText, UI.Font(6.75F), FormDesign.Design.InfoColor, fill: true);

			if (Height < yIndex + 3)
				Height = yIndex + 3;
		}
	}
}