using System;

namespace ShowsCalendar
{
	[Flags]
	public enum ChangeType
	{
		All = Data | Preferences,
		Data = 1,
		Preferences = 2
	}
}