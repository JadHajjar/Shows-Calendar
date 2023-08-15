using Extensions;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using TMDbLib.Objects.People;

namespace ShowsCalendar
{
	public partial class CharacterPageTile : FullPageTile
	{
		public TMDbLib.Objects.People.Person Person { get; private set; }
		internal Page currentPage;
		private readonly PC_CharacterView ParentPanel;

		public enum Page
		{
			Info,
			Credits,
			Local,
			Images
		}

		public Page CurrentPage
		{
			get => currentPage;
			set
			{
				lock (this)
				{
					currentPage = value;
					ParentPanel.ViewPage(CurrentPage);
					SideButtons.Foreach(x => { x.Selected = x.PageId == (int)currentPage; x.Invalidate(); });
					MainPanel.Visible = DrawInfo;
				}
			}
		}

		public override bool DrawInfo => CurrentPage == Page.Info;

		public CharacterPageTile(PC_CharacterView panel)
		{ ParentPanel = panel; InitializeComponent(); }

		public void SetData(TMDbLib.Objects.People.Person person, Bitmap defaultImage)
		{
			if (person != null)
			{
				Person = person;

				Title = Person.Name;
				SubTitle = KnownForString();
				PosterImage.DefaultImage = Properties.Resources.Huge_Team;

				if (Person.CombinedCredits != null)
				{
					LoadingControl?.Dispose();
					CreditsLabel.Text = $"{(char)0x200B}";

					if (CreditsPanel.Controls.Count == 0)
						this.TryInvoke(() =>
						{
							foreach (var item in KnownFor().Distinct((x, y) => x.Id == y.Id && x.IsMovie == y.IsMovie).Take(16))
								CreditsPanel.Controls.Add(new ImageSmallMediaViewerControl(item, new[] { item }));
						});
				}

				OverviewLabel.Text = Person.Biography;
				GenderLabel.Text = Person.CombinedCredits == null ? null : Person.Gender.Switch("Unknown/Non-Binary (They/Them)", (PersonGender.Female, "Woman (She/Her)"), (PersonGender.Male, "Man (He/Him)"));
				AKALabel.Text = Person.AlsoKnownAs != null && Person.AlsoKnownAs.Count == 1 ? Person.AlsoKnownAs[0] : Person.AlsoKnownAs?.Select(x => $"• {x}").ListStrings("\n");

				if (Person.Birthday != null)
				{
					var today = Person.Deathday ?? DateTime.Today;
					var age = today.Year - (Person.Birthday?.Year ?? today.Year);

					if (Person.Birthday?.Date > today.AddYears(-age)) age--;

					BirthdayLabel.Text = Person.Birthday?.ToReadableString(format: ExtensionClass.DateFormat.MDY) + (Person.Deathday == null ? $" • {age} years old" : null);

					if (Person.Deathday != null)
						DeathdayLabel.Text = Person.Deathday?.ToReadableString(format: ExtensionClass.DateFormat.MDY) + $" • {age} years old";
				}

				SocialLinksControl.Homepage = Person.Homepage;
				SocialLinksControl.Imdb = Person.ExternalIds?.ImdbId;
				SocialLinksControl.Twitter = Person.ExternalIds?.TwitterId;
				SocialLinksControl.Facebook = Person.ExternalIds?.FacebookId;
				SocialLinksControl.Instagram = Person.ExternalIds?.InstagramId;

				if (PosterImage.Image == null && !PosterImage.Loading)
					PosterImage.LoadImage(() => ImageHandler.GetImage(Person.ProfilePath, 300, false, true));

				LoadBackground(KnownFor().FirstOrDefault(x => !string.IsNullOrEmpty(x.BackdropPath))?.BackdropPath);

				if (defaultImage != null)
					DefaultImage = defaultImage;

				this.TryInvoke(Invalidate);
			}
		}

		private CombinedCredit[] KnownFor()
		{
			if (Person.CombinedCredits == null) return Array.Empty<CombinedCredit>();

			return Person.CombinedCredits.Cast.Cast<CombinedCredit>().Concat(Person.CombinedCredits.Crew.Cast<CombinedCredit>())
				.OrderByDescending(x =>
				{
					var factor = x.IsMovie ? 6 : x.EpisodeCount / 3;
					return factor * x.Popularity * x.VoteAverage;
				}).ToArray();
		}

		protected override void TitleClicked(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (currentPage == Page.Info)
					Data.Mainform.PushBack();
				else
					CurrentPage = Page.Info;
			}
		}

		private string KnownForString()
		{
			if (Person.CombinedCredits != null)
			{
				var dic = new Dictionary<string, double>() { { "Known for Acting", Person.CombinedCredits.Cast.Sum(x => x.IsMovie ? 4 : Math.Max(1, x.EpisodeCount / 4)) } };

				foreach (var item in Person.CombinedCredits.Crew.Where(x => !string.IsNullOrWhiteSpace(x.Department)))
				{
					var dep = "Known for " + item.Department;
					if (dic.ContainsKey(dep))
						dic[dep] += 4;
					else
						dic.Add(dep, 4);
				}

				var res = dic.OrderBy(x => x.Value).Last();

				return $"{res.Key} • {Person.CombinedCredits.Cast.Length + Person.CombinedCredits.Crew.Length} Credits";
			}

			return ConnectionHandler.IsConnected ? "Loading info.." : string.Empty;
		}
	}
}