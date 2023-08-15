using System;

namespace ShowsCalendar
{
	public class MoviePref
	{
		public DateTime LastReminder { get; set; }
		public DateTime LastUpcomingReminder { get; set; }
		public RatingInfo Rating { get; set; }
		public DateTime DateAdded { get; set; }
		public bool Watched { get; set; }
		public double Progress { get; set; }
		public DateTime WatchDate { get; set; }
		public long WatchTime { get; set; }
		public string[] RawVidFiles { get; set; }
	}

	public interface IMoviePref
	{
		DateTime LastReminder { get; set; }
		DateTime LastUpcomingReminder { get; set; }
		RatingInfo Rating { get; set; }
		DateTime DateAdded { get; set; }
		bool Watched { get; set; }
		double Progress { get; set; }
		DateTime WatchDate { get; set; }
		long WatchTime { get; set; }
		string[] RawVidFiles { get; set; }
	}
}