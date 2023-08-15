using System.Drawing;

namespace ShowsCalendar
{
	public interface IInteractableContent
	{
		void ShowInfoPage();

		void ShowStrip(Point? location = null, bool fromInfoPage = false);
	}
}