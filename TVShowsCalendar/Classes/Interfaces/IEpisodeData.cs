using System;
using System.Collections.Generic;

using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;

namespace ShowsCalendar
{
	public interface IEpisodeData
	{
		int Id { get; set; }
		string Name { get; set; }
		DateTime? AirDate { get; set; }
		string Overview { get; set; }
		string StillPath { get; set; }
		int EN { get; set; }
		double VoteAverage { get; set; }
		bool Custom { get; set; }
		int VoteCount { get; set; }
		List<Cast> GuestStars { get; set; }
		List<Crew> Crew { get; set; }
	}
}