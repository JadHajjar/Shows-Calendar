using Extensions;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShowsCalendar
{
	public partial class TvShow
	{
		public void Refresh() => ConnectionHandler.WhenConnected(() => new BackgroundAction(async () => await startRefresh()).Run());

		internal async Task startRefresh()
		{
			try
			{
				var dat = await Data.TMDbHandler.GetTvShow(Id);

				TMDbData = dat;

				Seasons = Seasons.Where(x => x.Custom || dat.Seasons.Any(y => y.SeasonNumber == x.SeasonNumber)).Distinct(x => x.SeasonNumber).ToList();

				InfoChanged?.Invoke(this, EventArgs.Empty);

				Parallelism.ForEach(dat.Seasons, async season =>
				{
					var sn = season.SeasonNumber;
					var s = Seasons.FirstOrDefault(y => sn == y.SeasonNumber);

					if (s != null)
						s.TMDbData = await Data.TMDbHandler.GetTvSeason(Id, sn);
					else
						Seasons.Add(new Season(await Data.TMDbHandler.GetTvSeason(Id, sn), this));
				}, 2);

				SimilarShows = SimilarShows.Concat((await Data.TMDbHandler.GetTvShowSimilar(Id, 1))?.Select(LightContent.Convert) ?? Array.Empty<LightContent>()).Distinct(x => x.Id).ToArray();
				LastRefresh = DateTime.Now;

				InfoChanged?.Invoke(this, EventArgs.Empty);
				ShowManager.OnShowDataChanged(this);
				Save(ChangeType.Data);
			}
			catch { }
		}
	}
}