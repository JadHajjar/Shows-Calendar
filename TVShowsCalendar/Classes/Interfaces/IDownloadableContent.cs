namespace ShowsCalendar
{
	public interface IDownloadableContent
	{
		bool CanBeDownloaded { get; }

		void Download();
	}
}