using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowsCalendar.Classes
{
	internal class SearchQuery
	{
		public string Query { get; set; }
		public bool SpacedOut { get; set; }

		public SearchQuery(string query, bool spacedOut)
		{
			Query = query;
			SpacedOut = spacedOut;
		}
	}
}
