using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using TMDbLib.Objects.General;
using TMDbLib.Objects.People;
using TMDbLib.Objects.TvShows;

using ProjectImages = ShowsCalendar.Properties.Resources;
using TmdbPerson = TMDbLib.Objects.People.Person;

namespace ShowsCalendar
{
	public partial class PC_CharacterView : PanelContent
	{
		public TmdbPerson Person { get; private set; }
		private CharacterPageTile PageTile;
		private readonly Bitmap defaultImage;

		private PC_CharacterView(int id, string name, string profile, Bitmap img) : base(true)
		{
			InitializeComponent();

			Text = name;
			defaultImage = img;
			Person = Person ?? new TmdbPerson() { Id = id, ProfilePath = profile, Name = name };

			PageTile = new CharacterPageTile(this);
			PageTile.SetData(Person, img);
			Controls.Add(PageTile);

			ConnectionHandler.WhenConnected(() => { if (!IsDisposed) StartDataLoad(); });
		}

		public PC_CharacterView(Cast cast)
			: this(cast.Id, cast.Name, cast.ProfilePath, ProjectImages.Huge_Cast) { }

		public PC_CharacterView(TMDbLib.Objects.Movies.Cast cast)
			: this(cast.Id, cast.Name, cast.ProfilePath, ProjectImages.Huge_Cast) { }

		public PC_CharacterView(Crew crew)
			: this(crew.Id, crew.Name, crew.ProfilePath, ProjectImages.Huge_Crew) { }

		public PC_CharacterView(CreatedBy item)
			: this(item.Id, item.Name, item.ProfilePath, ProjectImages.Huge_Quill) { }

		public PC_CharacterView(ShowsCalendar.Person item)
			: this(item.Id, item.Name, item.ProfilePath, null)
		{
			Person = item.TmdbPerson ?? Person;
			PageTile.SetData(Person, ProjectImages.Huge_Team);
		}

		protected override bool LoadData()
		{
			if (!ConnectionHandler.IsConnected) return false;

			Person = Data.TMDbHandler.GetPerson(Person.Id).Result;
			PageTile.SetData(Person, null);

			return true;
		}

		protected override void OnDataLoad()
		{
			if (PageTile.CurrentPage.AnyOf(CharacterPageTile.Page.Credits, CharacterPageTile.Page.Images))
				SetData(PageTile.CurrentPage);
		}

		private void SetData(CharacterPageTile.Page page)
		{
			switch (page)
			{
				case CharacterPageTile.Page.Credits:
					if (FLP_Career.Controls.Count == 0)
						populateCareer();
					break;

				case CharacterPageTile.Page.Local:
					if (FLP_Library.Controls.Count == 0)
						populateLibrary();
					break;

				case CharacterPageTile.Page.Images:
					if (FLP_Images.Controls.Count == 0)
						populateImages();
					break;
			}
		}

		internal void ViewPage(CharacterPageTile.Page page)
		{
			SetData(page);

			switch (page)
			{
				case CharacterPageTile.Page.Info:
					PageTile.SetContent(null);
					break;

				case CharacterPageTile.Page.Credits:
					PageTile.PageName = "Shows & Movie Credits";
					PageTile.SetContent(FLP_Career);
					break;

				case CharacterPageTile.Page.Local:
					PageTile.PageName = "Shows Up In These Shows & Movies";
					PageTile.SetContent(FLP_Library);
					break;

				case CharacterPageTile.Page.Images:
					PageTile.PageName = "Images";
					PageTile.SetContent(FLP_Images);
					break;
			}
		}

		private void populateCareer()
		{
			if (Person.CombinedCredits == null) return;

			var imgs = new List<(int, string, CombinedCredit)>();

			imgs.AddRange(Person.CombinedCredits.Cast.Select(x => (x.FirstAirDate?.Year ?? x.ReleaseDate?.Year ?? 0, $"{x.IsMovie.If("M", "T")}{x.Id}", (CombinedCredit)x)));

			imgs.AddRange(Person.CombinedCredits.Crew.Select(x => (x.FirstAirDate?.Year ?? x.ReleaseDate?.Year ?? 0, $"{x.IsMovie.If("M", "T")}{x.Id}", (CombinedCredit)x)));

			foreach (var grp in imgs.GroupBy(x => x.Item1).OrderByDescending(x => x.Key))
			{
				var ssp = new SlickSectionPanel()
				{
					Text = grp.Key.If(0, "Unknown", grp.Key.ToString()),
					AutoHide = true,
					Dock = DockStyle.Top,
					AutoSize = true,
					Icon = ProjectImages.Big_Calendar,
					MinimumSize = new Size(FLP_Career.Width, 0)
				};

				ssp.Add(grp.GroupBy(x => x.Item2).Select(x => new SmallMediaViewer(x.First().Item3, x.Select(y => y.Item3))));

				FLP_Career.Controls.Add(ssp);
			}
		}

		private void populateImages()
		{
			if (Person.Images != null)
			{
				foreach (var item in Person.Images.Profiles)
				{
					var width = (int)(180 * UI.UIScale);
					var height = width * item.Height / item.Width;

					var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
					pb.GetImage(item.FilePath, 180);

					FLP_Images.Controls.Add(pb);
				}
			}
		}

		private void populateLibrary()
		{
			var showsDic = new List<TvShow>();
			var epsDic = new List<Episode>();
			var moviesDic = new List<Movie>();

			foreach (var item in ShowManager.Shows)
			{
				if ((item.Cast?.Any(z => z.Id == Person.Id) ?? false)
					|| (item.Crew?.Any(z => z.Id == Person.Id) ?? false)
					|| item.Seasons.Any(y => y.Credits != null && (y.Credits.Cast.Any(z => z.Id == Person.Id) || y.Credits.Crew.Any(z => z.Id == Person.Id))))
				{
					showsDic.Add(item);
				}

				epsDic.AddRange(item.Episodes.Where(y => y.GuestStars.Any(z => z.Id == Person.Id) || y.Crew.Any(z => z.Id == Person.Id)));
			}

			foreach (var item in MovieManager.Movies)
			{
				if ((item.Cast?.Any(z => z.Id == Person.Id) ?? false)
					|| (item.Crew?.Any(z => z.Id == Person.Id) ?? false))
				{
					moviesDic.Add(item);
				}
			}

			if (showsDic.Any())
			{
				var ssp = new SlickSectionPanel()
				{
					Text = "TV Shows",
					Info = "One of the main characters in these shows",
					Dock = DockStyle.Top,
					AutoSize = true,
					Icon = ProjectImages.Big_TV
				};

				foreach (var item in showsDic)
					ssp.Add(new ContentControl<TvShow>(item));

				FLP_Library.Controls.Add(ssp);
			}

			if (epsDic.Any())
			{
				var ssp = new SlickSectionPanel()
				{
					Text = "Guest Star",
					Info = "Popping in these little episodes",
					Dock = DockStyle.Top,
					AutoSize = true,
					Icon = ProjectImages.Big_Cast
				};

				foreach (var item in epsDic)
					ssp.Add(new ContentControl<Episode>(item, false));

				FLP_Library.Controls.Add(ssp);
			}

			if (moviesDic.Any())
			{
				var ssp = new SlickSectionPanel()
				{
					Text = "Movies",
					Info = "Showing up in those movies",
					Dock = DockStyle.Top,
					AutoSize = true,
					Icon = ProjectImages.Big_Movie
				};

				foreach (var item in moviesDic)
					ssp.Add(new ContentControl<Movie>(item));

				FLP_Library.Controls.Add(ssp);
			}
		}

		public override bool OnWndProc(Message m)
		{
			if (m.Msg == 0x210 && m.WParam == (IntPtr)0x1020b && PageTile.CurrentPage != CharacterPageTile.Page.Info)
			{
				PageTile.CurrentPage = CharacterPageTile.Page.Info;
				return true;
			}

			return base.OnWndProc(m);
		}

		public override bool KeyPressed(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape && PageTile.CurrentPage != CharacterPageTile.Page.Info)
			{
				PageTile.CurrentPage = CharacterPageTile.Page.Info;
				return true;
			}

			if (keyData == (Keys.Control | Keys.Tab))
			{
				PageTile.CurrentPage = Enum.GetValues(typeof(CharacterPageTile.Page)).Cast<CharacterPageTile.Page>().Next(PageTile.CurrentPage);
				return true;
			}

			if (keyData == (Keys.Control | Keys.Shift | Keys.Tab))
			{
				PageTile.CurrentPage = PageTile.CurrentPage == CharacterPageTile.Page.Info ? CharacterPageTile.Page.Images : Enum.GetValues(typeof(CharacterPageTile.Page)).Cast<CharacterPageTile.Page>().Previous(PageTile.CurrentPage);
				return true;
			}

			return base.KeyPressed(ref msg, keyData);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			if (PageTile.CurrentPage != CharacterPageTile.Page.Info)
				Form.OnNextIdle(() =>
				{
					ViewPage(PageTile.CurrentPage);
				});
		}
	}
}