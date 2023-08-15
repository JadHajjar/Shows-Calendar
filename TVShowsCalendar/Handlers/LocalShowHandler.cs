using Extensions;

using ShowsCalendar.IO;

using ShowsRenamer.Module.Classes;
using ShowsRenamer.Module.Handlers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShowsCalendar
{
	public static class LocalShowHandler
	{
		public static event EventHandler<TvShow> WatchInfoChanged;

		private static readonly object lockObj = new object();

		public static int Check(string showName, string folder, bool containCheck = false)
			=> NameExtractor.Match(showName, folder, containCheck);

		public static void GetCuration(out List<Episode> curation, TvShow refShow = null)
		{
			curation = new List<Episode>();

			foreach (var show in ShowManager.Shows.Where(x => refShow == null || refShow == x).ToList())
			{
				var firstEp = show.Episodes.FirstOrDefault(x => x.VidFiles.Any(y => y.Exists) && !x.Watched);

				if (firstEp != null)
				{
					var lastEp = show.Episodes.LastThat(x => x.Watched);

					if (!firstEp.Watched && firstEp.Started && (DateTime.Now - firstEp.WatchDate).TotalDays <= 45 && !((lastEp != null && lastEp.WatchDate > DateTime.Now.AddDays(-10)) || (firstEp.Started && firstEp.WatchDate > DateTime.Now.AddDays(-10))))
					{
						curation.Add(firstEp);
					}
					else if (!firstEp.Watched && !firstEp.Started && firstEp.VidFiles.Any(x => (DateTime.Now - x.Info.CreationTime).TotalDays <= 30))
						curation.Add(firstEp);
				}
			}
		}

		public static Episode GetCurrentWatchEpisode(this TvShow show, int? seasonNumber = null)
		{
			if (seasonNumber == null)
			{
				return show.Episodes.FirstOrDefault(x => (!x.Watched) && x.Playable)
					?? show.Episodes.FirstOrDefault(x => x.Playable);
			}

			return show.Episodes.FirstOrDefault(x => x.SN == seasonNumber && (!x.Watched) && x.Playable)
				?? show.Episodes.FirstOrDefault(x => x.SN == seasonNumber && x.Playable);
		}

		public static DateTime GetDateOrder(this Episode ep) => ep.Started ? ep.WatchDate : ep.Previous?.WatchDate ?? DateTime.MinValue;

		public static string GetShowFolder(TvShow show, EpisodeFile ep) => show.FoundDirectories
				.Where(x => Directory.Exists(x))
				.Select(x => new { folder = x, index = ep.CurrentFile.FullName.IndexOf(x, StringComparison.CurrentCultureIgnoreCase) })
				.OrderBy(x => x.index)
				.LastOrDefault()?
				.folder;

		public static void Load()
		{
			ShowManager.ShowAdded += ShowManager_ShowLoaded;
			ShowManager.ShowDataChanged += ShowManager_ShowLoaded;

			LoadFiles();

			OnWatchInfoChanged(null);

			Handler.FilesChanged += File_Changed;
		}

		public static void LoadFiles(TvShow _show = null)
		{
			if (Handler.Paused) return;

			lock (_show ?? lockObj)
			{
				var shows = _show != null ? new List<TvShow> { _show } : ShowManager.Shows.ToList();

				foreach (var show in shows)
					show.FoundDirectories.Clear();

				workFolder(Handler.Folders);

				if (Handler.FirstLoadFinished)
					OnWatchInfoChanged(null);

				void workFolder(Folder dir)
				{
					if (dir.Exists || string.IsNullOrWhiteSpace(dir.Path))
						foreach (var folder in dir.SubFolders)
						{
							var show = MatchFile(ShowManager.Shows.ToList(), folder);

							if (show != null && shows.Contains(show))
							{
								show.FoundDirectories.AddIfNotExist(folder.Path);

								foreach (var file in folder.AllFiles().Where(x => EpisodeFileHandler.Test(x.Name)))
								{
									var epNumbs = EpisodeFileHandler.GetEpNumbers(file.Info);
									if (epNumbs != null)
									{
										var episode = show[epNumbs.Item1, epNumbs.Item2];

										if (episode != null)
										{
											episode.AddFile(file);

											if (string.IsNullOrWhiteSpace(episode.StillPath))
												episode.LoadThumbnail(file.Info);
										}
										else
										{
											if (show[epNumbs.Item1] == null)
											{
												ShowManager.CreateSeason(show, new Season
												{
													SeasonNumber = epNumbs.Item1,
												});
											}

											ShowManager.CreateEpisode(show, new Episode
											{
												Season = show[epNumbs.Item1],
												EN = epNumbs.Item2,
												AirDate = file.Info.CreationTime,
												Name = NameExtractor.GetEpisodeName(file.Name.RegexRemove(show.Name.Replace(" ", ".")))
											});

											show[epNumbs.Item1, epNumbs.Item2].AddFile(file);

											show[epNumbs.Item1, epNumbs.Item2].LoadThumbnail(file.Info);

											show.Save(ChangeType.Data);
										}
									}
								}

								show.Save(ChangeType.Preferences);
								goto skip;
							}

							workFolder(folder);

						skip:;
						}
				}
			}
		}

		public static void LoadLibrary(out List<Episode> onDeck, out List<Episode> continueWatching, out List<Episode> startWatching, out List<Episode> lastWatched, TvShow refShow = null)
		{
			onDeck = new List<Episode>();
			continueWatching = new List<Episode>();
			startWatching = new List<Episode>();
			lastWatched = new List<Episode>();

			foreach (var show in ShowManager.Shows.Where(x => refShow == null || refShow == x).ToList())
			{
				var firstEp = show.Episodes.FirstOrDefault(x => x.VidFiles.Any(y => y.Exists) && !x.Watched);
				var lastEp = show.Episodes.LastThat(x => x.Watched);

				if (firstEp != null)
				{
					if ((lastEp != null && lastEp.WatchDate > DateTime.Now.AddDays(-10)) || (firstEp.Started && firstEp.WatchDate > DateTime.Now.AddDays(-10)))
						onDeck.Add(firstEp);
					else if (lastEp != null)
						continueWatching.Add(firstEp);

					if (lastEp == null)
						startWatching.Add(firstEp);
				}

				if (lastEp != null && lastEp.VidFiles.Any(y => y.Exists) && lastEp.WatchDate > DateTime.Today.AddDays(-14))
					lastWatched.Add(lastEp);
			}
		}

		public static void LoadLibrary(out List<Episode> onDeck, TvShow refShow = null, bool toDownload = false)
		{
			onDeck = new List<Episode>();

			foreach (var show in ShowManager.Shows.Where(x => refShow == null || refShow == x).ToList())
			{
				var lastEp = show.Episodes.LastThat(x => x.Watched);
				var firstEp = show.Episodes.FirstOrDefault(x => (lastEp == null || x > lastEp) && !x.Watched && x.Playable);

				if (!toDownload && firstEp != null && ((lastEp != null && lastEp.WatchDate > DateTime.Now.AddDays(-10)) || (firstEp.Started && firstEp.WatchDate > DateTime.Now.AddDays(-10))))
				{
					onDeck.Add(firstEp);
				}
				else if (toDownload && lastEp != null)
				{
					var next = show.Episodes.Where(x => x.SN > 0).Next(lastEp);

					if (next != null
						&& (lastEp.WatchDate > DateTime.Now.AddDays(-10) || next.AirDate > DateTime.Today.AddDays(-8))
						&& next.AirState == AirStateEnum.Aired
						&& !next.Playable)
					{
						onDeck.Add(next);
					}
				}
			}
		}

		public static bool Match(string showName, string folder, bool containCheck = false)
			=> NameExtractor.Match(showName, folder, containCheck) > 0;

		public static TvShow MatchFile(IEnumerable<TvShow> shows, Folder folder)
		{
			var bestScore = 0;
			var bestShow = (TvShow)null;
			var filename = folder.Name;

			foreach (var show in shows)
			{
				var score = Math.Max(Check(show.Name, filename), string.IsNullOrWhiteSpace(show.OriginalName) ? 0 : Check(show.OriginalName, filename));

				if (score > bestScore)
				{
					bestScore = score;
					bestShow = show;
				}
			}

			return bestShow;
		}

		public static Episode MatchFile(IEnumerable<TvShow> shows, FileInfo fileObject)
		{
			var epNumbs = EpisodeFileHandler.GetEpNumbers(fileObject);

			if (epNumbs != null)
			{
				var name = EpisodeFileHandler.GetSeriesName(fileObject) ?? NameExtractor.GetSeriesName(fileObject.FileName());

				foreach (var show in shows)
				{
					if (Match(show.Name, name))
					{
						var ep = show[epNumbs.Item1, epNumbs.Item2];
						if (ep != null)
							return ep;
					}
				}
			}

			return null;
		}

		public static void OnWatchInfoChanged(TvShow tvShow, object sender = null) => WatchInfoChanged?.Invoke(sender, tvShow);

		private static void File_Changed(object sender, Folder e)
		{
			if (Handler.FirstLoadFinished)
				new BackgroundAction(() => LoadFiles()).Run();
		}

		private static void ShowManager_ShowLoaded(TvShow show)
			=> new BackgroundAction(() => LoadFiles(show)).Run();
	}
}