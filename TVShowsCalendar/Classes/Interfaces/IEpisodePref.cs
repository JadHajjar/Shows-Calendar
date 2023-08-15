using System;

namespace ShowsCalendar
{
	public class IEpisodePref
	{
		public int EN { get; set; }
		public RatingInfo Rating { get; set; }
		public bool Watched { get; set; }
		public double Progress { get; set; }
		public DateTime WatchDate { get; set; }
		public DateTime LastReminder { get; set; }
		public long WatchTime { get; set; }
		public string[] RawVidFiles { get; set; }
	}
}