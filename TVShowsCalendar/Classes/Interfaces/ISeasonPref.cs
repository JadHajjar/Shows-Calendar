using System.Collections.Generic;

namespace ShowsCalendar
{
	public class ISeasonPref
	{
		public int SeasonNumber { get; set; }
		public RatingInfo Rating { get; set; }

		public List<IEpisodePref> EpisodePrefs { get; set; }
	}
}