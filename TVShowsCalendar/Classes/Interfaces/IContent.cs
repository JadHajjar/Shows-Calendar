using SlickControls;

using System;
using System.Drawing;

namespace ShowsCalendar
{
	public interface IContent
	{
		event EventHandler InfoChanged;

		event EventHandler ContentRemoved;

		string Name { get; }
		string Overview { get; }
		string PosterPath { get; }
		string BackdropPath { get; }
		string SubInfo { get; }
		string ToolTipText { get; }
		double VoteAverage { get; }
		int VoteCount { get; }
		ContentType Type { get; }
		DateTime? AirDate { get; }
		Bitmap TinyIcon { get; }
		Bitmap BigIcon { get; }
		Bitmap HugeIcon { get; }
		RatingInfo Rating { get; set; }
		Banner NewBanner { get; }

		void Save(ChangeType change);

		void MarkAs(bool watched);
	}
}