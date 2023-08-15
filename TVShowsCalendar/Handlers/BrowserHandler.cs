using System.Linq;
using System.Threading.Tasks;

namespace ShowsCalendar
{
	public static class BrowserHandler
	{
		public static async Task<string> GetHTML(string URL, int? Season = null, int? Episode = null, bool wait = true)
		{
			try
			{
				string HTML;
				using (var wp = new WebProcessor())
				{
				load: HTML = wp.GetGeneratedHTML(URL + (Season != null ? (URL.Last() == '/' ? "" : "/") + Season + (Episode != null ? "x" + Episode : "") + ".html" : ""));

					if (wait && HTML.Contains("Please try again in a few minutes."))
					{
						await Task.Delay(30000);
						goto load;
					}
				}

				return HTML;
			}
			catch { return string.Empty; }
		}
	}
}