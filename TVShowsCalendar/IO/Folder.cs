using System;
using System.Collections.Generic;
using System.IO;

namespace ShowsCalendar.IO
{
	public class Folder
	{
		public string Path { get; }
		public string Name { get; }
		public DirectoryInfo Info { get; }
		public List<Folder> SubFolders { get; set; }
		public List<File> Files { get; set; }
		public bool Exists => Directory.Exists(Path);

		public Folder(string path)
		{
			Path = path;
			Name = System.IO.Path.GetFileName(path);
			SubFolders = new List<Folder>();
			Files = new List<File>();

			try { Info = new DirectoryInfo(Path); } catch { }
		}

		public IEnumerable<File> AllFiles()
		{
			foreach (var file in Files)
				yield return file;

			foreach (var item in SubFolders)
				foreach (var file in item.AllFiles())
					yield return file;
		}

		public File FindFile(string path)
		{
			foreach (var file in Files)
				if (file.Path.Equals(path, StringComparison.InvariantCultureIgnoreCase))
					return file;

			foreach (var item in SubFolders)
			{
				if (path.IndexOf(item.Path, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					var file = item.FindFile(path);

					if (file != null)
						return file;
				}
			}

			return null;
		}

		public override string ToString() => Name;
	}
}