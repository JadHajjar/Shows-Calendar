using System;
using System.Collections.Generic;

using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace ShowsCalendar
{
	public interface IMovieData
	{
		int Id { get; set; }
		string Title { get; set; }
		string OriginalTitle { get; set; }
		DateTime? ReleaseDate { get; set; }
		string Overview { get; set; }
		string Tagline { get; set; }
		List<Genre> Genres { get; set; }
		string BackdropPath { get; set; }
		string PosterPath { get; set; }
		List<ProductionCompany> ProductionCompanies { get; set; }
		string Homepage { get; set; }
		List<ProductionCountry> ProductionCountries { get; set; }
		List<string> Languages { get; set; }
		string Status { get; set; }
		ExternalIdsMovie ExternalIds { get; set; }
		double VoteAverage { get; set; }
		int VoteCount { get; set; }
		long Revenue { get; set; }
		long Budget { get; set; }
		int? Runtime { get; set; }
		List<Cast> Cast { get; set; }
		List<Crew> Crew { get; set; }
		LightContent[] SimilarMovies { get; set; }
		Video[] Videos { get; set; }
		Images Images { get; set; }
		List<string> Keywords { get; set; }
		DateTime LastRefresh { get; set; }
	}
}