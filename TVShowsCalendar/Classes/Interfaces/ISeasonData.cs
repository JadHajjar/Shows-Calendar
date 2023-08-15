using Newtonsoft.Json;

using System;
using System.Collections.Generic;

using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;

namespace ShowsCalendar
{
	public interface ISeasonData
	{
		int SeasonNumber { get; set; }
		string Name { get; set; }
		Credits Credits { get; set; }
		List<Video> Videos { get; set; }
		DateTime? AirDate { get; set; }
		string Overview { get; set; }

		[JsonProperty("PosterPath")]
		string SeasonPosterPath { get; set; }

		PosterImages Images { get; set; }
		bool Custom { get; set; }

		List<Episode> Episodes { get; set; }
	}
}