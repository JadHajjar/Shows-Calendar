using Extensions;

using ShowsRenamer.Module.Handlers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShowsCalendar
{
	public static class MediaDiscoveryHandler
	{
		public static IEnumerable<LightContent> DiscoverShows()
		{
			foreach (var item in IO.Handler.GeneralFolders
				.Where(x => Directory.Exists(x.FullName))
				.SelectMany(x => x.EnumerateDirectories("*", SearchOption.AllDirectories))
				.Where(isValid))
			{
				var name = NameExtractor.GetSeriesName(item.Name);

				if (!string.IsNullOrWhiteSpace(name))
				{
					var shows = Data.TMDbHandler.SearchTvShow(name)?.Result?.Take(2);

					if (shows != null)
					{
						foreach (var show in shows)
							yield return LightContent.Convert(show);
					}
				}
			}
		}

		public static IEnumerable<LightContent> DiscoverMovies()
		{
			foreach (var item in IO.Handler.GeneralFolders
				.Where(x => Directory.Exists(x.FullName))
				.SelectMany(x => x.EnumerateDirectories("*", SearchOption.AllDirectories))
				.Where(isValid))
			{
				var tuple = NameExtractor.GetMovieTitleYear(item.Name);

				if (!string.IsNullOrWhiteSpace(tuple.Item1))
				{
					var movies = Data.TMDbHandler.SearchMovie(tuple.Item1, year: tuple.Item2)?.Result?.Take(2);

					if (movies != null)
					{
						foreach (var movie in movies)
							yield return LightContent.Convert(movie);
					}
				}
			}
		}

		private static bool isValid(DirectoryInfo directory)
		{
			if (Regex.IsMatch(directory.Name, "season([^\\d]+)?\\d+", RegexOptions.IgnoreCase))
				return false;

			if (!directory.GetFilesByExtensions(SlickControls.IO.VideoExtensions).Any())
				return false;

			if (genericNames.Any(x => directory.Name.Equals(x, StringComparison.CurrentCultureIgnoreCase)))
				return false;

			return true;
		}

		private static readonly string[] genericNames = new[]
		{
			"desktop",
			"shows",
			"series",
			"tv shows",
			"tv series",
			"videos",
			"movies",
			"anime",
			"captures"
		};
	}
}