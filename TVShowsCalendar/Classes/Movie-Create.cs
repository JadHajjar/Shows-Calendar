using Extensions;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShowsCalendar
{
	public partial class Movie
	{
		public Movie()
		{
		}

		public static async Task<Movie> Create(int id, bool temporary = false)
		{
			var dat = await Data.TMDbHandler.GetMovie(id);

			var movie = new Movie
			{
				TMDbData = dat
			};

			if (movie.Temporary = temporary)
				MovieManager.TemporaryMovies.Add(movie);

			return movie;
		}

		public Movie(LightContent lightMovie, bool temporary = false)
		{
			Id = lightMovie.Id;
			Title = lightMovie?.Name;
			Overview = lightMovie?.Overview;
			ReleaseDate = lightMovie?.ReleaseDate;
			PosterPath = lightMovie?.PosterPath;
			BackdropPath = lightMovie?.BackdropPath;
			VoteCount = lightMovie?.VoteCount ?? 0;
			VoteAverage = lightMovie?.VoteAverage ?? 0;
			Genres = lightMovie?.GenreIds?.Select(x => Data.TMDbHandler.GetMovieGenre(x)).ToList();

			if (Temporary = temporary) MovieManager.TemporaryMovies.Add(this);

			new BackgroundAction(startFirstLoad).Run();
		}

		private async void startFirstLoad()
		{
			var dat = await Data.TMDbHandler.GetMovie(Id);

			TMDbData = dat;

			SimilarMovies = SimilarMovies.Concat((await Data.TMDbHandler.GetMovieSimilar(Id, 1))?.Select(LightContent.Convert) ?? Array.Empty<LightContent>()).Distinct(x => x.Id).ToArray();

			InfoChanged?.Invoke(this, EventArgs.Empty);
			MovieManager.OnMovieDataChanged(this);
			Save(ChangeType.All);
		}
	}
}