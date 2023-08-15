using System.Drawing;

namespace ShowsCalendar
{
	public class ImageContent<T>
	{
		public T Item { get; set; }
		public Bitmap Image { get; set; }
		public bool Visible { get; set; }
	}
}