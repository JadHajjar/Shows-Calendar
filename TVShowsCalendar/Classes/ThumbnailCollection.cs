using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ShowsCalendar
{
	internal class ThumbnailCollection : IDisposable
	{
		private bool disposed;
		private readonly WaitIdentifier waitIdentifier = new WaitIdentifier();
		private readonly FileInfo file;
		private readonly Dictionary<float, Tuple<bool, Bitmap>> thumbs = new Dictionary<float, Tuple<bool, Bitmap>>();

		public event EventHandler ImageLoaded;

		public Bitmap this[float time]
		{
			get
			{
				if (disposed) return null;

				var key = thumbs.Keys.Aggregate((x, y) => Math.Abs(x - time) < Math.Abs(y - time) ? x : y);

				if (!thumbs[key].Item1 && thumbs[key].Item2 == null)
					waitIdentifier.Wait(() => new BackgroundAction(() => loadThumb(key)).Run(), 150);
				else
					waitIdentifier.Cancel();

				return thumbs[key].Item2;
			}
		}

		public ThumbnailCollection(FileInfo file, long length)
		{
			this.file = file;

			for (var i = 1F; i < length / 1000; i += 15)
				thumbs.Add(i, new Tuple<bool, Bitmap>(false, null));
		}

		private void loadThumb(float t)
		{
			var times = new[] { t, t + 15, t - 15, t + 30, t - 30 };

			foreach (var item in times)
				load(item);

			void load(float time)
			{
				time = thumbs.Keys.Aggregate((x, y) => Math.Abs(x - time) < Math.Abs(y - time) ? x : y);

				if (!thumbs[time].Item1 && thumbs[time].Item2 == null)
				{
					thumbs[time] = new Tuple<bool, Bitmap>(true, null);

					using (var img = file.GetThumbnail(time))
					{
						if (img == null) return;

						thumbs.TryAdd(time, new Tuple<bool, Bitmap>(true, new Bitmap(img, UI.Scale(new Size(img.Width * 92 / img.Height, 92), UI.UIScale))));

						ImageLoaded?.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		public void Dispose()
		{
			if (!disposed)
			{
				disposed = true;
				waitIdentifier.Cancel();
				foreach (var item in thumbs)
					item.Value.Item2?.Dispose();

				thumbs.Clear();
			}
		}
	}
}