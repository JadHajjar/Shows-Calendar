using Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace ShowsCalendar
{
#pragma warning disable CS4014

	public class TMDbHandler
	{
		private readonly TMDbClient Client;
		private const string TMDb_KEY = "0e035c6b8f9b56f8f3783fa1043c81b1";
		private List<Genre> MovieGenres;
		private List<Genre> TvGenres;

		public TMDbHandler()
		{
			Client = new TMDbClient(TMDb_KEY);

			ConnectionHandler.WhenConnected(SetGenres);
		}

		public async Task<List<SearchMovie>> DiscoverMovies(int page = 0)
			=> (await RunTask(x => x.GetMoviePopularListAsync(page: page)))?.Results;

		public async Task<List<SearchTv>> DiscoverTvShow(int page = 0)
			=> (await RunTask(x => x.GetTvShowPopularAsync(page: page)))?.Results;

		public async Task<StillImages> GetEpisodeImages(int id, int seasonNumber, int episodeNumber)
			=> await RunTask(x => x.GetTvEpisodeImagesAsync(id, seasonNumber, episodeNumber, "en"));

		public async Task<List<Video>> GetEpisodeVideos(int id, int seasonNumber, int episodeNumber)
			=> (await RunTask(x => x.GetTvEpisodeVideosAsync(id, seasonNumber, episodeNumber)))?.Results;

		public async Task<TMDbLib.Objects.Movies.Movie> GetMovie(int id)
			=> await RunTask(x => x.GetMovieAsync(id, MovieMethods.Credits | MovieMethods.Similar | MovieMethods.Images | MovieMethods.Videos | MovieMethods.Keywords | MovieMethods.ExternalIds));

		public Genre GetMovieGenre(int id) => MovieGenres?.FirstOrDefault(x => x.Id == id) ?? new Genre() { Name = string.Empty };

		public async Task<List<SearchMovie>> GetMovieSimilar(int id, int page)
			=> (await RunTask(x => x.GetMovieSimilarAsync(id, page)))?.Results;

		public async Task<TMDbLib.Objects.People.Person> GetPerson(int id)
			=> await RunTask(x => x.GetPersonAsync(id, PersonMethods.Images | PersonMethods.CombinedCredits | PersonMethods.ExternalIds));

		public Genre GetTvGenre(int id) => TvGenres?.FirstOrDefault(x => x.Id == id) ?? new Genre() { Name = string.Empty };

		public async Task<TvSeason> GetTvSeason(int id, int seasonNumber)
			=> await RunTask(x => x.GetTvSeasonAsync(id, seasonNumber, TvSeasonMethods.Credits | TvSeasonMethods.Images | TvSeasonMethods.Videos));

		public async Task<TMDbLib.Objects.TvShows.TvShow> GetTvShow(int id)
			=> await RunTask(x => x.GetTvShowAsync(id, TvShowMethods.Credits | TvShowMethods.Similar | TvShowMethods.Images | TvShowMethods.Videos | TvShowMethods.Keywords | TvShowMethods.ExternalIds));

		public async Task<List<SearchTv>> GetTvShowSimilar(int id, int page)
			=> (await RunTask(x => x.GetTvShowSimilarAsync(id, page)))?.Results;

		public async Task<List<SearchMovie>> SearchMovie(string querry, int page = 0, bool adult = false, int year = 0)
			=> (await RunTask(x => x.SearchMovieAsync(querry, page, adult, year)))?.Results;

		public async Task<List<SearchPerson>> SearchPerson(string querry, int page = 0)
			=> (await RunTask(x => x.SearchPersonAsync(querry, page, true)))?.Results;

		public async Task<List<SearchTv>> SearchTvShow(string querry, int page = 0)
			=> (await RunTask(x => x.SearchTvShowAsync(querry, page, true)))?.Results;

		public async Task<List<SearchMovie>> FindMoviesByTag(string tag, int page = 0)
		{
			var id = await GetTagId(tag);
			
			if (id != null)
				return (await RunTask(x => x.GetKeywordMoviesAsync((int)id, page)))?.Results;

			return new List<SearchMovie>();
		}

		public async Task<List<SearchTv>> FindShowsByTag(string tag, int page = 0)
		{
			var id = await GetTagId(tag);

			if (id != null)
				return (await RunTask(x => x.GetKeywordShowsAsync((int)id, page)))?.Results;

			return new List<SearchTv>();
		}

		private async Task<int?> GetTagId(string tag)
			=> (await RunTask(x => x.SearchKeywordAsync(tag))).Results.FirstOrDefault(x => x.Name == tag)?.Id;

		private async Task<T> RunTask<T>(Func<TMDbClient, Task<T>> func)
		{
			T @out;

		start: try
			{
				@out = await func(Client);
			}
			catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
			{
				await Task.Delay(5000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonReaderException)
			{
				await Task.Delay(1000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonSerializationException)
			{
				await Task.Delay(1000);
				goto start;
			}

			return @out;
		}

		private async void SetGenres()
		{
			try
			{
				TvGenres = await RunTask(x => x.GetTvGenresAsync());
				MovieGenres = await RunTask(x => x.GetMovieGenresAsync());
			}
			catch { }
		}
	}

#pragma warning restore CS4014
}