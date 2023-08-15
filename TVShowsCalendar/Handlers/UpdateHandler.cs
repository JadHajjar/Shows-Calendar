using Extensions;

using SlickControls;

using System;
using System.Linq;
using System.Windows.Forms;

#pragma warning disable CS4014, CS1998

namespace ShowsCalendar
{
	public static class UpdateHandler
	{
		private static System.Timers.Timer Timer;

		public static void Start()
		{
			Timer = new System.Timers.Timer(TimeSpan.FromHours(12).TotalMilliseconds) { AutoReset = false };

			Timer.Elapsed += (s, e) => RunRefresh();

			new BackgroundAction(RunRefresh).Run();
		}

		private static async void RunRefresh() => ConnectionHandler.WhenConnected(() =>
		{
			try
			{
				foreach (var show in ShowManager.Shows.Where(x => x.LastRefresh < DateTime.Now.AddDays(-2)).ToList())
				{
					try { show.Refresh(); }
					catch (Exception ex)
					{
						MessagePrompt.Show($"Error occurred while updating the show {show.Name}\n\n{ex.Message}", "Error", icon: PromptIcons.Error, form: Data.Mainform);
						Clipboard.SetText(ex.ToString());
					}
				}
			}
			catch { }

			try
			{
				foreach (var movie in MovieManager.Movies.Where(x => x.LastRefresh < DateTime.Now.AddDays(-2)).ToList())
				{
					try { movie.Refresh(); }
					catch (Exception ex)
					{
						MessagePrompt.Show($"Error occurred while updating the movie {movie.Title}\n\n{ex.Message}", "Error", icon: PromptIcons.Error, form: Data.Mainform);
						Clipboard.SetText(ex.ToString());
					}
				}
			}
			catch { }

			Timer.Start();
		});
	}
}

#pragma warning restore CS4014, CS1998