using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ShowsCalendar
{
	public interface IPlayableContent
	{
		event EventHandler<double> WatchTimeChanged;

		event EventHandler FilesChanged;

		bool Watched { get; set; }

		double Progress { get; set; }

		DateTime WatchDate { get; set; }

		long WatchTime { get; set; }

		bool Started { get; }

		IEnumerable<IO.File> VidFiles { get; }

		bool Playable { get; }

		Bitmap GetThumbnail();

		bool Play(FileInfo file = null);

		void SetThumbnail(Guid guid);
	}
}