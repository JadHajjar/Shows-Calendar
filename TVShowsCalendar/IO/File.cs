using Extensions;

using System;
using System.Collections.Generic;
using System.IO;

namespace ShowsCalendar.IO
{
	public class File : IEquatable<File>
	{
		public string Path { get; }
		public string Name { get; }
		public FileInfo Info { get; private set; }
		public Folder Parent { get; }
		public bool Exists { get { try { return System.IO.File.Exists(Path); } catch { return false; } } }

		public File(string path, Folder parent = null)
		{
			Path = path;
			Name = System.IO.Path.GetFileName(path).RegexRemove(@"\.\w+$");
			Info = new FileInfo(Path);
			Parent = parent ?? new Folder(Directory.GetParent(path).FullName);
		}

		public override string ToString() => Name;

		public override bool Equals(object obj)
		{
			return Equals(obj as File);
		}

		public bool Equals(File other)
		{
			return other != null &&
				   Path.Equals(other.Path, StringComparison.InvariantCultureIgnoreCase);
		}

		public override int GetHashCode()
		{
			return 467214278 + EqualityComparer<string>.Default.GetHashCode(Path);
		}

		public static bool operator ==(File left, File right)
		{
			return EqualityComparer<File>.Default.Equals(left, right);
		}

		public static bool operator !=(File left, File right)
		{
			return !(left == right);
		}
	}
}