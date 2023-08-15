using System.ComponentModel;

namespace ShowsCalendar
{
	public enum MediaSortOptions
	{
		[Description("Relative Release Date")]
		Default,

		[Description("Year")]
		Year,

		[Description("Name")]
		Name,

		[Description("Genre")]
		Genre
	};
}