using Extensions;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShowsCalendar
{
	public partial class TvShow
	{
		public TvShow()
		{
		}

		public static async Task<TvShow> Create(int id, bool temporary = false)
		{
			var dat = await Data.TMDbHandler.GetTvShow(id);

			var show = new TvShow
			{
				TMDbData = dat,
				Id = dat.Id,
			};

			show.Seasons = dat.Seasons.Select(x => new Season(x, show)).ToList();

			if (show.Temporary = temporary)
			{
				ShowManager.TemporaryShows.Add(show);
				_ = show.startFirstLoad();
			}
			else
				await show.startFirstLoad();

			return show;
		}

		public TvShow(LightContent lightShow, bool temporary = false)
		{
			Id = lightShow.Id;
			Name = lightShow?.Name;
			Genres = lightShow?.GenreIds?.Select(x => Data.TMDbHandler.GetTvGenre(x)).ToList();
			FirstAirDate = lightShow?.ReleaseDate;
			Overview = lightShow?.Overview;
			PosterPath = lightShow?.PosterPath;
			BackdropPath = lightShow?.BackdropPath;
			VoteAverage = lightShow?.VoteAverage ?? 0;
			VoteCount = lightShow?.VoteCount ?? 0;

			if (Temporary = temporary) ShowManager.TemporaryShows.Add(this);

			_ = startFirstLoad();
		}

		private async Task startFirstLoad()
		{
			var dat = await Data.TMDbHandler.GetTvShow(Id);

			TMDbData = dat;

			Seasons = dat.Seasons.Select(x => new Season(x, this)).ToList();

			InfoChanged?.Invoke(this, EventArgs.Empty);

			for (var i = 0; i < Seasons.Count; i++)
				Seasons[i].TMDbData = await Data.TMDbHandler.GetTvSeason(Id, Seasons[i].SeasonNumber);

			SimilarShows = SimilarShows.Concat((await Data.TMDbHandler.GetTvShowSimilar(Id, 1))?.Select(LightContent.Convert) ?? Array.Empty<LightContent>()).Distinct(x => x.Id).ToArray();

			InfoChanged?.Invoke(this, EventArgs.Empty);
			ShowManager.OnShowDataChanged(this);
			Save(ChangeType.All);
		}
	}
}