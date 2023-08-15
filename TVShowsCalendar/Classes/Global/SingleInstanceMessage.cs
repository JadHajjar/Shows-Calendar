using Extensions;

namespace ShowsCalendar
{
	public class SingleInstanceMessage : ISave
	{
		public override string Name { get; set; } = "SingleInstanceMessage";

		public ActionType ActionType { get; set; }

		public string File { get; set; }
	}

	public enum ActionType
	{
		ShowUp,
		OpenFile
	}
}