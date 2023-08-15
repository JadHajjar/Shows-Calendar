using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PC_ManageCategory : PanelContent
	{
		public RatingInfo RatingInfo { get; set; }

		private readonly List<string> categories;

		public PC_ManageCategory(RatingInfo info, Action<RatingInfo> success)
		{
			InitializeComponent();
			RatingInfo = info;

			ISave.Load(out categories, "Categories.tf");

			//check null
			foreach (var item in categories.Distinct())
				SP_Categories.Add(new CategoryControl(item, 3));
		}

		private void B_Add_Click(object sender, EventArgs e)
		{
			var res = ShowInputPrompt("Enter the name for your category", "", PromptButtons.OKCancel);

			if (res.DialogResult == DialogResult.OK)
			{
				SP_Categories.Add(new CategoryControl(res.Input, 3));
				categories.Add(res.Input);
				ISave.Save(categories.Distinct(), "Categories.tf");
			}
		}
	}
}