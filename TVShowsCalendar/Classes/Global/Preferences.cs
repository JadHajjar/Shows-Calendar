using Extensions;

using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ShowsCalendar
{
	public class Preferences : ISave
	{
		public bool DashMax { get; set; } = false;

		public Rectangle DashBounds { get; set; } = Rectangle.Empty;

		public List<LightContent> DislikedShows { set { ShowsDisliked = value.Select(x => x.Id).ToList(); } }
		public List<LightContent> DislikedMovies { set { MoviesDisliked = value.Select(x => x.Id).ToList(); } }
		public List<int> ShowsDisliked { get; set; } = new List<int>();
		public List<int> MoviesDisliked { get; set; } = new List<int>();
		public ViewType ShowsView { get; set; }
		public ViewType MoviesView { get; set; }
	}
}