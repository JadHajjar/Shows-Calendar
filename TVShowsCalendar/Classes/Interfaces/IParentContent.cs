namespace ShowsCalendar
{
	public interface IParentContent
	{
		bool Playable { get; }
		int UnwatchedContent { get; }
		ContentType ContentType { get; }
		double ContentRating { get; }

		void Play();

		void ContentInfoChanged();
	}
}