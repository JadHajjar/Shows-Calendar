using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PC_People : PanelContent
	{
		private readonly Dictionary<Person, CharacterControl> Tiles = new Dictionary<Person, CharacterControl>();
		private string lastSearch;
		private Factory dataLoadFactory = new Factory(50);
		private TicketBooth TicketBooth = new TicketBooth();

		public PC_People()
		{
			InitializeComponent();

			SlickTip.SetTo(PB_Search, "Click to Search");
		}

		private CharacterControl AddPerson(Person person, CharacterControl tile = null)
		{
			if (person.Id == 0)
				return null;

			if (tile == null)
				tile = new CharacterControl(person);

			P_Tabs.Controls.Add(tile);

			return tile;
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			L_NoMoviesInfo.ForeColor = design.InfoColor;
			L_NoMovies.ForeColor = design.LabelColor;
			P_TopSpacer.BackColor = design.AccentColor;

			PB_Search.Image = Properties.Resources.Big_Search.Color(searchOpened ? design.ActiveColor : design.IconColor);
		}

		protected override void UIChanged()
		{
			base.UIChanged();
			L_NoMovies.Font = UI.Font(9.75F, FontStyle.Bold);
			L_NoMoviesInfo.Font = UI.Font(8.25F, FontStyle.Italic);
			TLP_NoMovies.Location = TLP_NoMovies.Parent.Size.Center(TLP_NoMovies.Size);
		}

		private readonly WaitIdentifier searchWaitIdentifier = new WaitIdentifier();

		private void TB_Search_TextChanged(object sender, EventArgs e)
		{
			if (searchOpened && (lastSearch == TB_Search.Text || Tiles.Count == 0))
				return;

			if (!searchOpened || TB_Search.Width == 0)
				toggleSearch();

			if (!string.IsNullOrWhiteSpace(TB_Search.Text))
				PB_Search.Loading = true;

			P_Tabs.Controls.Clear(true, x => !Tiles.ContainsValue(x as CharacterControl));
			P_Tabs.Controls.Clear();

			TLP_NoMovies.Visible = false;
			Invalidate();

			if (!ConnectionHandler.IsConnected) return;

			var ticket = TicketBooth.GetTicket();

			searchWaitIdentifier.Wait(async () =>
			{
				if (string.IsNullOrWhiteSpace(TB_Search.Text)) this.TryInvoke(() =>
				{
					P_Tabs.SuspendDrawing();
					foreach (var tile in Tiles)
						AddPerson(tile.Key, tile.Value);
					P_Tabs.ResumeDrawing();
				});
				else
				{
					var results = await getPeople(TB_Search.Text);

					if (TicketBooth.IsLast(ticket))
						this.TryInvoke(() =>
						{
							P_Tabs.SuspendDrawing();
							foreach (var item in results)
								AddPerson(item);
							P_Tabs.ResumeDrawing();

							PB_Search.Image = Properties.Resources.Big_Search.Color(searchOpened ? FormDesign.Design.ActiveColor : FormDesign.Design.IconColor);
							lastSearch = TB_Search.Text;
							TLP_NoMovies.Visible = P_Tabs.Controls.Count == 0;
						});
				}
			}, string.IsNullOrWhiteSpace(TB_Search.Text) ? 10 : 300);
		}

		private async Task<IEnumerable<Person>> getPeople(string text)
		{
			var people = await Data.TMDbHandler.SearchPerson(text, 0);

			return people.Select(x => new Person(x));
		}

		private bool CheckSearch(Person person, CharacterControl tile)
		{
			tile?.ShownTags.Clear();

			if (string.IsNullOrWhiteSpace(TB_Search.Text))
				return true;

			var tags = person.Jobs.Concat(new[] { person.Name }).Where(x => x.SearchCheck(TB_Search.Text) || x.GetAbbreviation().SearchCheck(TB_Search.Text)).ToList();

			if (person.TmdbPerson != null)
			{
				var q = TB_Search.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				if (person.TmdbPerson.Birthday != null && q.Any(x => x == person.TmdbPerson.Birthday?.Year.ToString()))
					tags.Add($"Born on {person.TmdbPerson.Birthday?.ToReadableString(format: ExtensionClass.DateFormat.TDMY)}");

				if (person.TmdbPerson.Deathday != null && q.Any(x => x == person.TmdbPerson.Deathday?.Year.ToString()))
					tags.Add($"Passed away on {person.TmdbPerson.Deathday?.ToReadableString(format: ExtensionClass.DateFormat.TDMY)}");

				if (person.TmdbPerson.Birthday != null && q.Any(x => x == person.Age.ToString()))
					tags.Add($"{person.Age} years old");

				if (person.TmdbPerson.PlaceOfBirth?.SearchCheck(TB_Search.Text) ?? false)
					tags.Add(person.TmdbPerson.PlaceOfBirth);
			}

			tile?.ShownTags.AddRange(tags);

			return tags.Any(x => !string.IsNullOrWhiteSpace(x));
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
				(searchAnimation = new AnimationHandler(TB_Search, new Size((int)(350 * UI.FontScale), TB_Search.Height), AnimationOption.IgnoreHeight)).StartAnimation();
				SlickTip.SetTo(PB_Search, "Close Search");

				if (!TB_Search.Focused)
					TB_Search.Focus();
			}
			else
			{
				(searchAnimation = new AnimationHandler(TB_Search, new Size(0, TB_Search.Height), AnimationOption.IgnoreHeight)).StartAnimation();
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

		private void TB_Search_Leave(object sender, EventArgs e) => TB_Search.Focus();

		private void PC_Movies_Shown(object sender, EventArgs e)
		{
			BeginInvoke(new Action(() => TB_Search.Focus()));
		}

		private void PC_Movies_Load(object sender, EventArgs e)
		{
			new BackgroundAction(firstLoad).Run();
		}

		private void firstLoad()
		{
			var people = new List<Person>();

			foreach (var show in ShowManager.Shows)
			{
				people.AddRange(show.Cast.Select(x => new Person(x, show.Name)));
				people.AddRange(show.CreatedBy.Select(x => new Person(x, show.Name)));

				foreach (var season in show.Seasons)
				{
					people.AddRange(season.Credits.Cast.Select(x => new Person(x, show.Name)));

					foreach (var episode in season.Episodes)
					{
						people.AddRange(episode.GuestStars.Select(x => new Person(x, show.Name)));
					}
				}
			}

			foreach (var movie in MovieManager.Movies)
			{
				people.AddRange(movie.Cast.Select(x => new Person(x, movie.Name)));
			}

			people = people.GroupBy(x => x.Id).Select(p => Person.Merge(p)).OrderByDescending(x => x.Hits).ToList();

			Form.OnNextIdle(() =>
			{
				PB_FirstLoad.Visible = false;

				P_Tabs.SuspendDrawing();
				foreach (var person in people.Take(100))
					Tiles.Add(person, AddPerson(person));
				P_Tabs.ResumeDrawing();

				TLP_NoMovies.Visible = people.Count == 0;
			});
		}
	}
}