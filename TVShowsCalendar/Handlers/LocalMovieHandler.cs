using Extensions;

using ShowsCalendar.IO;

using ShowsRenamer.Module.Handlers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShowsCalendar
{
	public static class LocalMovieHandler
	{
		public static event EventHandler<Movie> WatchInfoChanged;

		private static readonly object lockObj = new object();

		public static void GetCuration(out List<Movie> curation, Movie refMovie = null)
		{
			curation = new List<Movie>();

			foreach (var movie in MovieManager.Movies.Where(x => refMovie == null || refMovie == x))
			{
				if (movie.Playable)
				{
					if (movie.Started && !movie.Watched)
					{
						if ((DateTime.Now - movie.WatchDate).TotalDays <= 45)
							curation.Add(movie);
					}

					if (!movie.Watched && !movie.Started && movie.VidFiles.Any(x => (DateTime.Now - x.Info.CreationTime).TotalDays <= 30))
						curation.Add(movie);
				}
			}
		}

		public static void Load()
		{
			MovieManager.MovieAdded += MovieManager_MovieLoaded;
			MovieManager.MovieDataChanged += MovieManager_MovieLoaded;

			LoadFiles();

			OnWatchInfoChanged(null);

			Handler.FilesChanged += File_Changed;
		}

		public static void LoadFiles(Movie _mov = null)
		{
			if (Handler.Paused) return;

			lock (_mov ?? lockObj)
			{
				var movies = _mov == null ? MovieManager.Movies : new List<Movie> { _mov };

				workFolder(Handler.Folders);

				if (Handler.FirstLoadFinished)
					OnWatchInfoChanged(null);

				void workFolder(Folder dir)
				{
					if (dir.Exists || string.IsNullOrWhiteSpace(dir.Path))
						foreach (var folder in dir.SubFolders)
						{
							foreach (var file in folder.Files.Where(x => EpisodeFileHandler.GetEpNumbers(x.Info, true) == null))
							{
								var bestMovie = MatchFile(MovieManager.Movies, file.Info);

								if (bestMovie != null && movies.Contains(bestMovie))
								{
									bestMovie.AddFile(file);

									if (string.IsNullOrWhiteSpace(bestMovie.BackdropPath))
										bestMovie.LoadThumbnail(file.Info);
								}
							}

							workFolder(folder);
						}
				}
			}
		}

		public static void LoadLibrary(out List<Movie> onDeck, out List<Movie> continueWatching, out List<Movie> startWatching, Movie refMovie = null)
		{
			onDeck = new List<Movie>();
			continueWatching = new List<Movie>();
			startWatching = new List<Movie>();

			foreach (var movie in MovieManager.Movies.Where(x => refMovie == null || refMovie == x))
			{
				if (movie.VidFiles.Any(x => x.Exists))
				{
					if (movie.Started)
					{
						if (movie.WatchDate > DateTime.Now.AddDays(-10) && !movie.Watched)
							onDeck.Add(movie);
						else
							continueWatching.Add(movie);
					}
					else if (!movie.Watched)
					{
						startWatching.Add(movie);
					}
				}
			}
		}

		public static void LoadLibrary(out List<Movie> onDeck, Movie refMovie = null)
		{
			onDeck = new List<Movie>();

			foreach (var movie in MovieManager.Movies.Where(x => refMovie == null || refMovie == x))
			{
				if (movie.VidFiles.Any(x => x.Exists))
				{
					if (movie.Started && !movie.Watched)
					{
						if (movie.WatchDate > DateTime.Now.AddDays(-10))
							onDeck.Add(movie);
					}
				}
			}
		}

		public static bool Match(string movieName, string folder, bool containCheck = false)
			=> NameExtractor.Match(movieName, folder, containCheck) > 0;

		public static Movie MatchFile(IEnumerable<Movie> movies, FileInfo file)
		{
			var bestScore = 0;
			var bestMovie = (Movie)null;
			var filename = file.FileName();

			foreach (var movie in movies)
			{
				var score = Math.Max(Check(movie.Name, filename), string.IsNullOrWhiteSpace(movie.OriginalTitle) ? 0 : Check(movie.OriginalTitle, filename));

				if (score > bestScore)
				{
					bestScore = score;
					bestMovie = movie;
				}
			}

			return bestMovie;
		}

		public static void OnWatchInfoChanged(Movie movie, object sender = null) => WatchInfoChanged?.Invoke(sender, movie);

		public static int Check(string movieName, string folder, bool containCheck = false)
			=> NameExtractor.Match(movieName, folder, containCheck);

		private static void File_Changed(object sender, Folder e)
		{
			if (Handler.FirstLoadFinished)
				new BackgroundAction(() => LoadFiles()).Run();
		}

		private static void MovieManager_MovieLoaded(Movie show)
							=> new BackgroundAction(() => LoadFiles(show)).Run();
	}
}