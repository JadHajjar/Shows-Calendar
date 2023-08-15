using Extensions;

using System;
using System.Collections.Generic;

using TMDbLib.Objects.Search;

namespace ShowsCalendar
{
	public class LightContent
	{
		public bool Movie { get; set; }
		public string Name { get; set; }
		public List<int> GenreIds { get; set; }
		public DateTime? ReleaseDate { get; set; }
		public string Overview { get; set; }
		public int Id { get; set; }
		public string PosterPath { get; set; }
		public string BackdropPath { get; set; }
		public double VoteAverage { get; set; }
		public int VoteCount { get; set; }

		[Obsolete("Use 'Name'", true)]
		public string Title { set { Name = value.IfEmpty(Name); Movie = true; } }

		[Obsolete("Use 'ReleaseDate'", true)]
		public DateTime? FirstAirDate { set => ReleaseDate = value ?? ReleaseDate; }

		public static LightContent Convert(SearchMovie searchMovie) => new LightContent
		{
			Movie = true,
			Name = searchMovie.Title,
			GenreIds = searchMovie.GenreIds,
			ReleaseDate = searchMovie.ReleaseDate,
			Overview = searchMovie.Overview,
			Id = searchMovie.Id,
			PosterPath = searchMovie.PosterPath,
			BackdropPath = searchMovie.BackdropPath,
			VoteCount = searchMovie.VoteCount,
			VoteAverage = searchMovie.VoteAverage
		};

		public static LightContent Convert(SearchTv searchTv) => new LightContent
		{
			Name = searchTv.Name,
			GenreIds = searchTv.GenreIds,
			ReleaseDate = searchTv.FirstAirDate,
			Overview = searchTv.Overview,
			Id = searchTv.Id,
			PosterPath = searchTv.PosterPath,
			BackdropPath = searchTv.BackdropPath,
			VoteAverage = searchTv.VoteAverage,
			VoteCount = searchTv.VoteCount
		};

		public static LightContent Convert(TMDbLib.Objects.TvShows.TvShow searchTv) => new LightContent
		{
			Name = searchTv.Name,
			GenreIds = searchTv.GenreIds,
			ReleaseDate = searchTv.FirstAirDate,
			Overview = searchTv.Overview,
			Id = searchTv.Id,
			PosterPath = searchTv.PosterPath,
			BackdropPath = searchTv.BackdropPath,
			VoteAverage = searchTv.VoteAverage,
			VoteCount = searchTv.VoteCount
		};
	}
}