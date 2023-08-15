using System;
using System.Collections.Generic;

namespace ShowsCalendar
{
	public class ShowPref
	{
		public RatingInfo Rating { get; set; }
		public DateTime DateAdded { get; set; }

		public List<ISeasonPref> SeasonPrefs { get; set; }
	}

	public interface IShowPref
	{
		RatingInfo Rating { get; set; }
		DateTime DateAdded { get; set; }

		List<ISeasonPref> SeasonPrefs { get; set; }
	}
}