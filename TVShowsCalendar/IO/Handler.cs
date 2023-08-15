using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;

namespace ShowsCalendar.IO
{
	public static class Handler
	{
		public static event EventHandler<Folder> FilesChanged;

		private static readonly Dictionary<string, FileSystemWatcher> watchers = new Dictionary<string, FileSystemWatcher>();
		private static readonly WaitIdentifier changeWaitIdentifier = new WaitIdentifier();
		private static readonly TicketBooth ticketBooth = new TicketBooth();
		private static bool loading;

		public static bool FirstLoadFinished { get; internal set; }
		public static List<DirectoryInfo> GeneralFolders { get; private set; } = new List<DirectoryInfo>();
		public static Folder Folders { get; private set; } = new Folder(string.Empty);
		public static bool Paused { get; private set; }

		public static bool Loading
		{
			get => loading;
			private set
			{
				loading = value;
			}
		}

		public static void LoadFolders(bool notifyEvent)
		{
			var ticket = ticketBooth.GetTicket();

			lock (ticketBooth)
			{
				try
				{
					Loading = true;
					var startFolder = new Folder(string.Empty)
					{
						SubFolders = GeneralFolders.Select(x => new Folder(x.FullName)).ToList()
					};

					run(startFolder);

					if (!Paused && ticketBooth.IsLast(ticket))
					{
						Folders = startFolder;

						if (notifyEvent)
							FilesChanged?.Invoke(Folders, Folders);
					}
				}
				finally
				{ Loading = false; }

				void run(Folder folder)
				{
					if (Paused || !ticketBooth.IsLast(ticket)) return;

					try
					{
						if (folder.Exists)
						{
							foreach (var f in folder.Info.EnumerateDirectories())
							{
								if (Paused || !ticketBooth.IsLast(ticket)) return;

								folder.SubFolders.Add(new Folder(f.FullName));
							}

							foreach (var f in folder.Info.GetFilesByExtensions(SearchOption.TopDirectoryOnly, SlickControls.IO.VideoExtensions))
							{
								if (Paused || !ticketBooth.IsLast(ticket)) return;

								folder.Files.Add(new File(f.FullName, folder));
							}

							if (Paused || !ticketBooth.IsLast(ticket)) return;
						}
					}
					catch { }

					foreach (var item in folder.SubFolders)
						run(item);
				}
			}
		}

		public static bool FileExists(File file) => file?.Exists ?? false;

		public static void AddGeneralFolder(string path)
		{
			try
			{
				if (!GeneralFolders.Any(x => x.FullName.Equals(path, StringComparison.CurrentCultureIgnoreCase)))
				{
					GeneralFolders.Add(new DirectoryInfo(path));

					ISave.Save(GeneralFolders, "GeneralFolders.tf");

					if (Directory.Exists(path))
						addWatcher(new DirectoryInfo(path));

					if (!Paused)
						new BackgroundAction(() => LoadFolders(true)).Run();
				}
			}
			catch { }
		}

		public static void Initialize()
		{
			ISave.Load(out List<DirectoryInfo> folders, "GeneralFolders.tf");

			foreach (var item in GeneralFolders = folders ?? new List<DirectoryInfo>())
				addWatcher(item);

			var driveWatcher = new ManagementEventWatcher();
			driveWatcher.EventArrived += (s, e) => file_Changed(null, null);
			driveWatcher.Query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");
			driveWatcher.Start();
		}

		public static void Pause() => Paused = true;

		public static void RemoveGeneralFolder(string path)
		{
			GeneralFolders.Remove(GeneralFolders.FirstOrDefault(x => x.FullName.Equals(path, StringComparison.CurrentCultureIgnoreCase)));

			ISave.Save(GeneralFolders, "GeneralFolders.tf");

			removeWatcher(new DirectoryInfo(path));

			if (!Paused)
				new BackgroundAction(() => LoadFolders(true)).Run();
		}

		public static void Resume()
		{
			Paused = false;
			new BackgroundAction(() => LoadFolders(true)).Run();
		}

		private static void addWatcher(DirectoryInfo item)
		{
			if (item.Exists)
			{
				var watcher = new FileSystemWatcher
				{
					Path = item.FullName,
					NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size,
					Filter = "*.*",
					EnableRaisingEvents = true,
					IncludeSubdirectories = true
				};

				watcher.Changed += file_Changed;
				watcher.Created += file_Changed;
				watcher.Deleted += file_Changed;

				watchers.TryAdd(item.FullName.ToLower(), watcher);
			}
		}

		private static void file_Changed(object sender, FileSystemEventArgs e)
		{
			if (!Paused && (e == null || Path.GetExtension(e.Name).ToLower().IfEmpty(".mp4").AnyOf(SlickControls.IO.VideoExtensions)))
				changeWaitIdentifier.Wait(() => LoadFolders(true), 1500);
		}

		private static void removeWatcher(DirectoryInfo item)
		{
			watchers.TryGetValue(item.FullName.ToLower(), out var watcher);

			if (watcher != null)
			{
				watcher.Dispose();
				watchers.Remove(item.FullName.ToLower());
			}
		}

		public static void LoadThumbnail(this IPlayableContent content, FileInfo file, bool wait = false)
		{
			var guid = Guid.NewGuid();

			BackgroundAction.Method action = () =>
			{
				using (var bitmap = file.GetThumbnail())
				{
					if (bitmap != null)
					{
						foreach (var item in ImageHandler.AvailableSizes[false])
						{
							var path = Path.Combine(ISave.DocsFolder, "Thumbs", item.ToString());

							Directory.CreateDirectory(path);

							using (var newBitmap = item == 0 ? new Bitmap(bitmap) : new Bitmap(bitmap, item, item * bitmap.Height / bitmap.Width))
							using (var filestream = System.IO.File.OpenWrite(Path.Combine(path, $"{guid}.jpg")))
								newBitmap.Save(filestream, ImageFormat.Jpeg);
						}

						content.SetThumbnail(guid);
					}
				}
			};

			if (wait)
				action();
			else
			new BackgroundAction(	action).Run();
		}
	}
}