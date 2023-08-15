using Extensions;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShowsCalendar
{
	public partial class Movie
	{
		public void Refresh() => ConnectionHandler.WhenConnected(() => new BackgroundAction(async () => await startRefresh()).Run());

		internal async Task startRefresh()
		{
			try
			{
				var dat = await Data.TMDbHandler.GetMovie(Id);

				TMDbData = dat;

				SimilarMovies = SimilarMovies.Concat((await Data.TMDbHandler.GetMovieSimilar(Id, 1))?.Select(LightContent.Convert) ?? Array.Empty<LightContent>()).Distinct(x => x.Id).ToArray();
				LastRefresh = DateTime.Now;

				InfoChanged?.Invoke(this, EventArgs.Empty);
				MovieManager.OnMovieDataChanged(this);
				Save(ChangeType.Data);
			}
			catch { }
		}
	}
}