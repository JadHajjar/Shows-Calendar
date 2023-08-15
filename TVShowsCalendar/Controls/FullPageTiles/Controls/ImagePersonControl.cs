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
	public class ImagePersonControl : SlickAdvancedImageImageBackgroundControl
	{
		private readonly string name;
		private readonly string character;

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

		private ImagePersonControl(dynamic data, string name, string character, string image, Bitmap defaultImage)
		{
			Margin = new Padding(5, 0, 0, 10);

			this.name = name;
			this.character = character;
			DefaultImage = defaultImage;
			Data = data;

			LoadImage(() => ImageHandler.GetImage(image, 110, false));
		}

		public ImagePersonControl(Cast cast)
			: this(cast, cast.Name, cast.Character, cast.ProfilePath, ProjectImages.Huge_Cast) { }

		public ImagePersonControl(TMDbLib.Objects.Movies.Cast cast)
			: this(cast, cast.Name, cast.Character, cast.ProfilePath, ProjectImages.Huge_Cast) { }

		public ImagePersonControl(Crew crew)
			: this(crew, crew.Name, crew.Job, crew.ProfilePath, ProjectImages.Huge_Crew) { }

		public ImagePersonControl(CreatedBy item)
			: this(item, item.Name, string.Empty, item.ProfilePath, ProjectImages.Huge_Quill) { }

		public ImagePersonControl(Person person)
			: this(person, person.Name, string.Empty, person.ProfilePath, ProjectImages.Huge_Team)
		{
			Person = person;
		}

		public override void CalculateSize(PaintEventArgs e)
		{
			Size = UI.Scale(new Size(110, 500), UI.FontScale);
			ImageBounds = new Rectangle(new Point(1, 1), UI.Scale(new Size(108, 162), UI.FontScale));

			yIndex = Width - ImageBounds.Width < 20 ? ImageBounds.Top + ImageBounds.Height + 4 : 2;

			Height = yIndex
				+ MeasureText(e, name, UI.Font(8.25F), fill: true).Height
				+ MeasureText(e, character, UI.Font(6.75F), fill: true).Height + 10;
		}

		protected override void OnDraw(PaintEventArgs e)
		{
			DrawText(e, name, UI.Font(8.25F), FormDesign.Design.ForeColor, fill: string.IsNullOrWhiteSpace(character));

			DrawText(e, character, UI.Font(6.75F), FormDesign.Design.LabelColor, fill: true);
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				ShowsCalendar.Data.Mainform.PushPanel(null, new PC_CharacterView(Data));
		}
	}
}