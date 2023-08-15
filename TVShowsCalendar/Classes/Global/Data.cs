using ShowsRenamer.Module.Handlers;

using SlickControls;

using System;
using System.Windows.Forms;

using YoutubeExplode;

namespace ShowsCalendar
{
	public static class Data
	{
		public static Form PreloaderForm { get; set; }
		public static Dashboard Mainform { get; set; }

		public static TMDbHandler TMDbHandler { get; set; }

		public static YoutubeClient YoutubeClient { get; } = new YoutubeClient(new System.Net.Http.HttpClient { Timeout = TimeSpan.FromSeconds(100) });

		public static bool FirstTimeSetup { get; set; } = false;

		public static Options Options { get; set; } = new Options();
		public static Preferences Preferences { get; set; } = new Preferences();

		public static RenameHandler RenameHandler { get; set; }

		public static PC_Player ActivePlayer { get; set; }

		public static void Open(string URL)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				System.Diagnostics.Process.Start(URL);
			}
			catch
			{
				MessagePrompt.Show("Could not open link because you do not have a default Web Browser selected", PromptButtons.OK, PromptIcons.Error, Mainform);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}
	}
}