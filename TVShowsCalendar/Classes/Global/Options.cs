using Extensions;

namespace ShowsCalendar
{
	public class Options : ISave
	{
		public int PrefferedQuality { get; set; } = 7;
		public bool ShowAllDownloads { get; set; } = true;
		public bool StartupMode { get; set; } = true;
		public bool LaunchWithWindows { get; set; } = true;
		public bool DownloadBehavior { get; set; } = true;
		public bool NotificationSound { get; set; } = true;
		public bool FinaleWarning { get; set; } = false;
		public bool FullScreenPlayer { get; set; } = true;
		public int ForwardTime { get; set; } = 15;
		public int BackwardTime { get; set; } = 5;
		public bool AutoCleaner { get; set; } = false;
		public bool EpisodeNotification { get; set; } = true;
		public bool OpenAllPagesForEp { get; set; } = true;
		public bool AutomaticEpisodeSwitching { get; set; } = true;
		public MediaSortOptions ShowSorting { get; set; }
		public MediaSortOptions MovieSorting { get; set; }
		public bool AutoPauseOnInfo { get; set; } = true;
		public bool SpoilerThumbnail { get; set; } = false;
		public bool IgnoreSpecialsSeason { get; set; } = false;
		public bool KeepPlayerOpen { get; set; } = false;
		public bool StickyMiniPlayer { get; set; } = true;
		public bool NoAnimations { get; set; } = false;
		public bool TopMostPlayer { get; set; } = true;
		public bool AlwaysShowBanners { get; set; } = true;
		public bool PauseWhenOutOfFocusFullScreen { get; set; } = true;
	}
}