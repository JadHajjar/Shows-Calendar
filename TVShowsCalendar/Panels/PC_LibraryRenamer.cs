using Extensions;

using ShowsRenamer.Module.Classes;
using ShowsRenamer.Module.Handlers;
using ShowsRenamer.Module.Interfaces;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

using RenamerOptions = ShowsRenamer.Module.Classes.Options;

namespace ShowsCalendar
{
	public partial class PC_LibraryRenamer : PanelContent
	{
		private readonly EpisodeFile sampleEp = new EpisodeFile(null) { SeasonNumber = 11, EpisodeNumber = 5, Name = "Ep Name", Show = new ShowFiles() { Name = "Show" } };

		public PC_LibraryRenamer()
		{
			InitializeComponent();

			FirstFocusedControl = B_Run;

			RB_Style1.Data = new NamingStyle(" - S", "E", " - ");
			RB_Style2.Data = new NamingStyle(" ", "x", " ");
			RB_Style3.Data = new NamingStyle(" Season ", " - E", " - ");
			RB_Style4.Data = new NamingStyle(" S", " - Episode ", " - ");
			RB_Custom.Data = new NamingStyle(" • ", "x", " • ");

			CB_ShowSeries.Checked = RenamerOptions.Current.ShowSeries;
			CB_AddZero.Checked = RenamerOptions.Current.AddZero;
			CB_IncSubs.Checked = RenamerOptions.Current.IncSubs;
			CB_SyncOnline.Checked = RenamerOptions.Current.SubsInSeperateFolder;
			CB_CleanFolders.Checked = RenamerOptions.Current.CleanFolders;
			CB_Auto.Checked = Data.Options.AutoCleaner;

			if (RenamerOptions.Current.NamingStyle == null)
			{
				RB_Style1.Checked = true;
			}
			else if (RB_Style1.RadioGroup.Any(x => RenamerOptions.Current.NamingStyle.Equals(x.Data)))
			{
				RB_Style1.RadioGroup.FirstOrDefault(x => RenamerOptions.Current.NamingStyle.Equals(x.Data)).Checked = true;
			}
			else if (RenamerOptions.Current.NamingStyle != null)
			{
				TB_CN_1.Text = RenamerOptions.Current.NamingStyle.PreSeason;
				TB_CN_2.Text = RenamerOptions.Current.NamingStyle.PreEpisode;
				TB_CN_3.Text = RenamerOptions.Current.NamingStyle.PreName;
			}

			foreach (var radio in RB_Style1.RadioGroup)
			{
				radio.Text = sampleEp.GetName(radio.Data as NamingStyle);
			}

			if (Data.RenameHandler != null)
			{
				SetEnabled(false);
			}
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			L_CN_1.ForeColor = L_CN_2.ForeColor = L_CN_3.ForeColor = L_CN_4.ForeColor =
				L_AutoIInfo.ForeColor = design.InfoColor;
		}

		private void PC_LibraryRenamer_Resize(object sender, EventArgs e)
		{
			//P_Tabs.MaximumSize = new Size(panel2.Width, 9999);
			//P_Tabs.MinimumSize = new Size(panel2.Width, 0);
		}

		private void CB_AddZero_CheckChanged(object sender, EventArgs e)
		{
			RenamerOptions.Current.AddZero = CB_AddZero.Checked;

			foreach (var radio in RB_Style1.RadioGroup)
			{
				radio.Text = sampleEp.GetName(radio.Data as NamingStyle);
			}
		}

		public override bool CanExit(bool toBeDisposed)
		{
			RenamerOptions.Current.ShowSeries = CB_ShowSeries.Checked;
			RenamerOptions.Current.AddZero = CB_AddZero.Checked;
			RenamerOptions.Current.IncSubs = CB_IncSubs.Checked;
			RenamerOptions.Current.SubsInSeperateFolder = CB_SyncOnline.Checked;
			RenamerOptions.Current.CleanFolders = CB_CleanFolders.Checked;
			RenamerOptions.Current.NamingStyle = (NamingStyle)RB_Style1.RadioGroup.GetSelectedData();

			RenamerOptions.Current.Save();

			Data.Options.AutoCleaner = CB_Auto.Checked;
			Data.Options.Save();

			return true;
		}

		private void TB_CN_TextChanged(object sender, EventArgs e)
		{
			RB_Custom.Data = new NamingStyle(TB_CN_1.Text, TB_CN_2.Text, TB_CN_3.Text);
			RB_Custom.Text = sampleEp.GetName(RB_Custom.Data as NamingStyle);
			RB_Custom.Checked = true;
		}

		private void CB_ShowSeries_CheckChanged(object sender, EventArgs e)
		{
			RenamerOptions.Current.ShowSeries = CB_ShowSeries.Checked;

			foreach (var radio in RB_Style1.RadioGroup)
			{
				radio.Text = sampleEp.GetName(radio.Data as NamingStyle);
			}
		}

		private void B_Run_Click(object sender, EventArgs e)
		{
			SetEnabled(false);
			CanExit(false);

			var notif = Notification.Create("Library Cleaner", "Finding & matching video files in your library folders", PromptIcons.Loading, null).Show(Form);

			Data.RenameHandler = new RenameHandler(new CleaningSessionInfo()
			{
				Paths = IO.Handler.GeneralFolders.Select(x => x.FullName),
				RunningForm = Form
			});

			Data.RenameHandler.Log += (p, m) =>
			{
				notif.Notification.Description = m;
			};

			new BackgroundAction(() =>
			{
				try { RunCleaner(); }
				finally
				{
					notif.Close();
					SetEnabled(true);
				}
			}).Run();
		}

		public static void RunCleaner()
		{
			IO.Handler.LoadFolders(false);
			LocalShowHandler.LoadFiles();
			LocalMovieHandler.LoadFiles();

			var files = new List<IRenameFile>();

			foreach (var show in ShowManager.Shows)
			{
				var showfiles = new ShowFiles()
				{
					Name = show.Name,
					Episodes = new List<EpisodeFile>()
				};

				var eps = show.Seasons.SelectMany(x => x.Episodes).Where(x => x.VidFiles.Any(y => y.Exists)).SelectMany(
					x => x.VidFiles.Select(item => new EpisodeFile(item.Info)
					{
						SeasonNumber = x.SN,
						EpisodeNumber = x.EN,
						Name = x.Name,
						Show = showfiles
					})).ToArray();

				foreach (var epGrps in eps.GroupBy(x => LocalShowHandler.GetShowFolder(show, x)))
				{
					var path = epGrps.Key;

					if (Directory.Exists(path))
					{
						foreach (var ep in epGrps)
						{
							ep.GenerateNewFile(Directory.GetParent(path).FullName, RenamerOptions.Current.NamingStyle);
						}

						showfiles.Episodes.AddRange(epGrps);

						files.AddRange(epGrps);

						foreach (var item in epGrps)
						{
							files.AddRange(item.GetSubtitleFiles(path, RenamerOptions.Current.NamingStyle));
						}

						foreach (var file in new DirectoryInfo(path).EnumerateFiles("*.*", SearchOption.AllDirectories)
							.Where(x => !files.Any(y => y.CurrentFile.FullName == x.FullName) && JunkFilesHandler.Validate(x)))
						{
							files.Add(new JunkFile(file));
						}
					}
				}
			}

			var movies = MovieManager.Movies.Where(x => x.VidFiles.Any(y => y.Exists)).SelectMany(
				x => x.VidFiles.Select(item => new MovieFile(item.Info)
				{
					Title = x.Title,
					Year = x.ReleaseDate?.Year ?? 0
				})).ToArray();

			files.AddRange(movies);

			foreach (var movie in movies)
			{
				movie.GenerateNewFile(movie.CurrentFile.Directory.Parent.FullName, RenamerOptions.Current.NamingStyle);

				files.AddRange(movie.GetSubtitleFiles(RenamerOptions.Current.NamingStyle));

				foreach (var file in new DirectoryInfo(movie.CurrentFile.Directory.Parent.FullName).EnumerateFiles("*.*", SearchOption.AllDirectories)
					.Where(x => !files.Any(y => y.CurrentFile.FullName == x.FullName) && JunkFilesHandler.Validate(x)))
				{
					files.Add(new JunkFile(file));
				}
			}

			Data.RenameHandler.Session.Files = files;

			try
			{
				IO.Handler.Pause();

				Data.RenameHandler.RunCleaner();
			}
			catch { }
			finally
			{
				Data.RenameHandler = null;
				IO.Handler.Resume();
			}
		}

		private void PaintNotification(SlickPictureBox pb, Graphics g)
		{
			pb.DrawLoader(g, new Rectangle(8, (pb.Height - 32) / 2, 30, 30));

			g.DrawString("Library Cleaner", UI.Font(9.75F), new SolidBrush(FormDesign.Design.ForeColor), 50, 4);

			g.DrawString(pb.Text, UI.Font(8.25F), new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(50, 6 + UI.Font(9.75F).Height, pb.Width - 50, pb.Height - 29), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });
		}

		private void SetEnabled(bool val)
		{
			this.TryInvoke(() =>
{
	TLP_Options.Enabled =
	RB_Style1.Enabled =
	RB_Style2.Enabled =
	RB_Style3.Enabled =
	RB_Style4.Enabled =
	RB_Custom.Enabled =
	TB_CN_1.Enabled =
	TB_CN_2.Enabled =
	TB_CN_3.Enabled =
	B_Run.Enabled = val;
});
		}
	}
}