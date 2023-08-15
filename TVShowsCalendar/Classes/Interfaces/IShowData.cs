using System;
using System.Collections.Generic;

using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;

namespace ShowsCalendar
{
	public interface IShowData
	{
		int Id { get; set; }
		string Name { get; set; }
		string OriginalName { get; set; }
		string Status { get; set; }
		string BackdropPath { get; set; }
		string Overview { get; set; }
		string PosterPath { get; set; }
		List<NetworkWithLogo> Networks { get; set; }
		string Homepage { get; set; }
		List<CreatedBy> CreatedBy { get; set; }
		DateTime? FirstAirDate { get; set; }
		DateTime? LastAirDate { get; set; }
		List<int> EpisodeRunTime { get; set; }
		ExternalIdsTvShow ExternalIds { get; set; }
		string ShowType { get; set; }
		bool InProduction { get; set; }
		List<string> OriginCountry { get; set; }
		List<string> Languages { get; set; }
		List<Genre> Genres { get; set; }
		double VoteAverage { get; set; }
		int VoteCount { get; set; }
		ImageData[] Backdrops { get; set; }
		ImageData[] Posters { get; set; }
		List<Cast> Cast { get; set; }
		List<Crew> Crew { get; set; }
		LightContent[] SimilarShows { get; set; }
		Video[] Videos { get; set; }
		List<string> Keywords { get; set; }
		DateTime LastRefresh { get; set; }

		List<Season> Seasons { get; set; }
	}
}