using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions;
using SlickControls.Controls;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.UserControls;
using ProjectImages = TVShowsCalendar.Properties.Resources;

namespace TVShowsCalendar.Forms
{
	public partial class SortingPopup : BaseForm
	{
		private bool IsMovie;
		private bool ascending;

		public SortingPopup(bool isMovie = false)
		{
			InitializeComponent();

			IsMovie = isMovie;
			ascending = Data.Preferences.ShowSortAscending;
			L_Order.Image = ascending ? ProjectImages.Check_Checked : ProjectImages.Check_Unchecked;
			L_Order.Text = ascending ? "Ascending" : "Descending";
			L_Order.Click += (s, e) => Sorting_Click();

			if(isMovie)
			{
				L_Name.Text = "Movie Title";
				L_LastDate.Text = "Release Date";
				L_FirstDate.Visible = false;
			}

			L_Name.Data = SortingType.Name;
			L_LastDate.Data = SortingType.LastAirDate;
			L_FirstDate.Data = SortingType.FirstAirDate;
			L_Rating.Data = SortingType.Rating;

			L_Name.RadioGroup.FirstThat(x => (SortingType)x.Data == Data.Preferences.ShowSortType).Checked = true;
		}

		protected override void DesignChanged(FormDesign design)
		{
            base.DesignChanged(design);

			TLP_Main.BackColor = design.BackColor;
			ForeColor = design.ForeColor;
			label1.ForeColor = label2.ForeColor = design.LabelColor;
		}

		private void Sorting_Click()
		{
			ascending = !ascending;
			L_Order.Image = ascending ? ProjectImages.Check_Checked : ProjectImages.Check_Unchecked;
			L_Order.Text = ascending ? "Ascending" : "Descending";
		}
        
		private void B_OK_Click(object sender, EventArgs e)
		{
			if (IsMovie)
			{
				Data.Preferences.MovieSortAscending = ascending;
				Data.Preferences.MovieSortType = (SortingType)(L_Name.RadioGroup.GetSelectedData() ?? Data.Preferences.ShowSortType);
			}
			else
			{
				Data.Preferences.ShowSortAscending = ascending;
				Data.Preferences.ShowSortType = (SortingType)(L_Name.RadioGroup.GetSelectedData() ?? Data.Preferences.ShowSortType);
			}
			Data.Preferences.Save();
			Close();
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
