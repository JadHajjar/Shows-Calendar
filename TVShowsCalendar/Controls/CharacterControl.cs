using Extensions;

using SlickControls;

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public partial class CharacterControl : SlickAdvancedImageControl
	{
		private readonly string name;
		private readonly string character;
		private readonly dynamic Data;

		public Person Person { get; }
		public List<string> ShownTags { get; } = new List<string>();
		protected override IEnumerable<Bitmap> HoverIcons { get { yield return ProjectImages.Icon_Info; } }

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				foreach (var item in ShownTags.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct())
					yield return new Banner(item.FormatWords(), BannerStyle.Text, ProjectImages.Tiny_Search);
			}
		}

		private CharacterControl(dynamic data, string name, string character, string image, Bitmap defaultImage)
		{
			InitializeComponent();

			this.name = name;
			this.character = character;
			DefaultImage = defaultImage;
			Data = data;

			SlickTip.SetTo(this, name, character);

			this.GetImage(image, 110, false);
		}

		public CharacterControl(Cast cast)
			: this(cast, cast.Name, cast.Character, cast.ProfilePath, ProjectImages.Huge_Cast) { }

		public CharacterControl(TMDbLib.Objects.Movies.Cast cast)
			: this(cast, cast.Name, cast.Character, cast.ProfilePath, ProjectImages.Huge_Cast) { }

		public CharacterControl(Crew crew)
			: this(crew, crew.Name, crew.Job, crew.ProfilePath, ProjectImages.Huge_Crew) { }

		public CharacterControl(CreatedBy item)
			: this(item, item.Name, string.Empty, item.ProfilePath, ProjectImages.Huge_Quill) { }

		public CharacterControl(Person person)
			: this(person, person.Name, string.Empty, person.ProfilePath, ProjectImages.Huge_Team)
		{
			Person = person;
		}

		protected override void UIChanged()
		{
			Size = UI.Scale(new Size(110, 200), UI.FontScale);
			ImageBounds = new Rectangle(new Point(1, 1), UI.Scale(new Size(108, 162), UI.FontScale));
		}

		protected override void OnDraw(PaintEventArgs e)
		{
			DrawText(e, name, UI.Font(8.25F), FormDesign.Design.ForeColor, fill: string.IsNullOrWhiteSpace(character));

			DrawText(e, character, UI.Font(6.75F), FormDesign.Design.LabelColor, fill: true);

			if (Height < yIndex + 3)
				Height = yIndex + 3;
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				ShowsCalendar.Data.Mainform.PushPanel(null, new PC_CharacterView(Data));
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
				Data.Mainform.PushPanel(null, new PC_CharacterView(Data));
			else
				base.OnMouseClick(e);
		}
	}
}