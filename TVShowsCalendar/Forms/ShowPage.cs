using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.Forms;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.UserControls;
using ProjectImages = TVShowsCalendar.Properties.Resources;

namespace TVShowsCalendar
{
	public partial class ShowPage_old : BaseForm
	{
		private enum Tabs { Info, Season, Cast, Similar };
		
		Show LinkedShow;
		EpisodeTile currentEpisode;
		Season currentSeason;
		bool seasonThumbLoaded = false;

		public ShowPage_old(Show show)
		{
			InitializeComponent();
			
			LinkedShow = show;

			SelectedTab = L_Info;
			TLP_Contents.ColumnStyles[1].Width = TLP_Contents.ColumnStyles[2].Width = TLP_Contents.ColumnStyles[3].Width = 0;
			Scroll_Seasons.LinkedControl = FLP_Seasons;
			Scroll_Episodes.LinkedControl = FLP_Episodes;
			Scroll_Content.LinkedControl = TLP_Contents;
			ML_Zooqle.HoverStateChanged += ML_Zooqle_HoverStateChanged;
			ML_Zooqle.Click += (s, e) => ML_Zooqle_Click();

			show.ShowLoaded += Show_ShowLoaded;
			Show_ShowLoaded(show);

			SetSeason(show.Seasons.Last());
			L_Info_Click(this, new EventArgs());

			if(show.Temporary)
			{
                PB_Thumb.Color(FormDesign.Design.IconColor);
				ML_Zooqle.Visible = false;
				L_13.Visible = false;
			}

            if (!show.SimilarShows?.Any() ?? true)
                StartLoader();
		}

		private void MI_ShowMore_Click(object sender, EventArgs et)
		{
			new FlatToolStrip(new List<FlatStripItem>()
			{
				new FlatStripItem("Play", () => LinkedShow.GetCurrentWatchEpisode().Play(), image: ProjectImages.Icon_Play, show: LinkedShow.GetCurrentWatchEpisode()?.VidFile?.Exists ?? false),

				new FlatStripItem("Add Show", ShowAdded, image: ProjectImages.Icon_Add, show: LinkedShow.Temporary),

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
				new FlatStripItem("Reload", () => LinkedShow.CheckForChanges(false, true, true), image: ProjectImages.Icon_Retry, show: !LinkedShow.Temporary),
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

				new FlatStripItem("Delete", () => ShowManager.Remove(LinkedShow), image: ProjectImages.Icon_Trash, show: !LinkedShow.Temporary),

				new FlatStripItem("Mark as " + (LinkedShow.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched)) ? "Unwatched" : "Watched"), show: !LinkedShow.Temporary, image: ProjectImages.Icon_OK, action: () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					var set = !LinkedShow.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched));
					LinkedShow.Seasons.ForEach(x => x.Episodes.ForEach(e => e.Watched = e.AirState == AirStateEnum.Aired ? set : e.Watched));
					new Action(()=>
					{
						LocalShowHandler.Refresh(LinkedShow);
						ShowManager.Save(LinkedShow);
					}).RunInBackground();
					PB_Thumb.Refresh();
					Cursor.Current = Cursors.Default;
				})
			}).ShowUp();
		}

		private void More_Click(object sender, EventArgs ea)
		{
			new FlatToolStrip(new List<FlatStripItem>()
			{
				new FlatStripItem("Mark", image: ProjectImages.Icon_OK, fade: true),
				new FlatStripItem("  Season as " + (currentSeason.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched) ? "Unwatched" : "Watched"),() =>
				{
					var watched = !(currentSeason.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched));
					Cursor.Current = Cursors.WaitCursor;
					foreach (var item in currentSeason.Episodes.Where(x => x.AirState == AirStateEnum.Aired)) 
						item.Watched=watched;
					new Action(()=>
					{
						LocalShowHandler.Refresh(LinkedShow);
						ShowManager.Save(LinkedShow);
					}).RunInBackground();
					PB_Thumb.Refresh();
					PB_SeasonPoster.Refresh();
					Cursor.Current = Cursors.Default;
				})
			}).ShowUp();
		}

		public void ShowAdded()
		{
			ShowManager.Add(LinkedShow);
			LinkedShow.Temporary = false;
			Data.ShowForm.FLP_Tiles.Controls.Add(new ShowTile(LinkedShow));
			ML_Zooqle.Visible = true;
			L_13.Visible = true;
		}

		private void Show_ShowLoaded(Show show)
		{
			this.TryInvoke(() =>
			{
				FLP_Seasons.SuspendDrawing();
				FLP_Cast.SuspendDrawing();
				FLP_Crew.SuspendDrawing();
				FLP_Similar.SuspendDrawing();
				TLP_Contents.SuspendDrawing();

				Text = show.Name;
				L_Status.Text = $"{show.TMDbData.Status} • {(show.TMDbData.FirstAirDate.HasValue ? $"Started in {show.TMDbData.FirstAirDate?.Year}" : "")} {(show.TMDbData.LastAirDate.HasValue ? $"{(show.TMDbData.FirstAirDate.HasValue ? " with its most recent release on " : "")}{show.TMDbData.LastAirDate?.ToReadableString()}" : "")}";


				{
					ConnectionHandler.Connected -= LoadPoster;
					ConnectionHandler.Connected += LoadPoster;
				}

				SetLastEp();
				CreateSeasons();

				L_Overview.Text = show.TMDbData.Overview.IfEmpty("No Overview");
				L_Rating.Text = show.TMDbData.VoteCount > 0 ? $"{show.TMDbData.VoteAverage} / 10" : "No Ratings";
				L_Genres.Text = show.TMDbData.Genres.Count == 0 ? "Unknown" : show.TMDbData.Genres.ListStrings(x => x.Name + " • ").TrimEnd(3);
				L_Runtime.Text = show.TMDbData.EpisodeRunTime.Count > 0 && show.TMDbData.EpisodeRunTime.Average() > 0 ? $"{show.TMDbData.EpisodeRunTime.Average()} minutes" : "Unknown";

				if (string.IsNullOrWhiteSpace(LinkedShow.ZooqleURL))
				{
					ML_Zooqle.Text = "Link";
					ML_Zooqle.Image = ProjectImages.Icon_Link;
				}
				else
				{
					ML_Zooqle.Text = "Linked";
					ML_Zooqle.Image = ProjectImages.Icon_OK;
				}

				L_11.Visible = show.Cast.Count > 0;
				L_12.Visible = show.Crew.Count > 0;

				if (show.Cast.Count + show.Crew.Count == 0)
					TLP_Tabs.ColumnStyles[2].Width = 0;
				else
					TLP_Tabs.ColumnStyles[2].Width = 25;

				if (show.SimilarShows == null || show.SimilarShows.Count == 0)
					TLP_Tabs.ColumnStyles[3].Width = 0;
				else
					TLP_Tabs.ColumnStyles[3].Width = 25;

				L_LastEp.Click += (s, e) => SetSeason(LinkedShow.LastEpisode.Season, LinkedShow.LastEpisode);
				L_NextEp.Click += (s, e) => SetSeason(LinkedShow.NextEpisode.Season, LinkedShow.NextEpisode);

				FLP_Cast.Controls.Clear();
				foreach (var cast in show.Cast)
					FLP_Cast.Controls.Add(new CharacterControl(cast));

				FLP_Crew.Controls.Clear();
				foreach (var crew in show.Crew)
					FLP_Crew.Controls.Add(new CharacterControl(crew));

				FLP_Similar.Controls.Clear();
				if (show.SimilarShows != null)
					foreach (var sim in show.SimilarShows)
						FLP_Similar.Controls.Add(new MediaViewer(sim));

				FLP_Seasons.ResumeDrawing();
				FLP_Cast.ResumeDrawing();
				FLP_Crew.ResumeDrawing();
				FLP_Similar.ResumeDrawing();
				TLP_Contents.ResumeDrawing();

				StopLoader();
			});
		}

		public ShowPage_old(Season season) : this(season.Show)
			=> SetSeason(season);

		public ShowPage_old(Episode episode) : this(episode.Show)
			=> SetSeason(episode.Season, episode);

		private void CreateSeasons()
		{
			FLP_Seasons.Controls.Clear();
			foreach (var season in LinkedShow.Seasons)
			{
				var label = new SeasonLabel(season);

				label.Click += (s, e) => SetSeason(season);

				FLP_Seasons.Controls.Add(label);
			}
		}

		private void LoadPoster(Extensions.ConnectionState state)
		{
			PB_Thumb.SizeMode = PictureBoxSizeMode.CenterImage;
			PB_Thumb.Image = FormDesign.Loader;
			PB_Thumb.LoadAsync($"https://image.tmdb.org/t/p/w200{LinkedShow.TMDbData.PosterPath}");
		}

		private void PaintStars(FormDesign design)
		{
			PB_Star_1.Image = GetStar(PB_Star_1, 100 * (LinkedShow.TMDbData.VoteAverage > 2 ? 1 : LinkedShow.TMDbData.VoteAverage / 2D), design);
			PB_Star_2.Image = GetStar(PB_Star_2, 100 * (LinkedShow.TMDbData.VoteAverage > 4 ? 1 : (LinkedShow.TMDbData.VoteAverage - 2) / 2D), design);
			PB_Star_3.Image = GetStar(PB_Star_3, 100 * (LinkedShow.TMDbData.VoteAverage > 6 ? 1 : (LinkedShow.TMDbData.VoteAverage - 4) / 2D), design);
			PB_Star_4.Image = GetStar(PB_Star_4, 100 * (LinkedShow.TMDbData.VoteAverage > 8 ? 1 : (LinkedShow.TMDbData.VoteAverage - 6) / 2D), design);
			PB_Star_5.Image = GetStar(PB_Star_5, 100 * (LinkedShow.TMDbData.VoteAverage == 10 ? 1 : (LinkedShow.TMDbData.VoteAverage - 8) / 2D), design);
		}

		protected override void DesignChanged(FormDesign design)
		{
            base.DesignChanged(design);
            
			tableLayoutPanel8.BackColor = design.BackColor;
			TLP_Tabs.BackColor = design.MenuColor;
			selectedTab.BackColor = design.BackColor;
			selectedTab.ForeColor = design.ActiveColor;
			L_1.ForeColor = L_2.ForeColor = L_3.ForeColor = L_4.ForeColor = L_5.ForeColor = L_6.ForeColor =
				L_7.ForeColor = L_8.ForeColor = L_9.ForeColor = L_10.ForeColor = L_11.ForeColor = 
				L_12.ForeColor = L_13.ForeColor = design.LabelColor;
			
			PB_SeasonPoster.InitialImage = FormDesign.Loader;

			PaintStars(design);
            PB_Thumb.Refresh();
        }

		internal void SetNextEp() => SetLastEp();

		internal void SetLastEp()
		{
			if (InvokeRequired)
			{
				Invoke(new Action(SetLastEp));
				return;
			}

			if (LinkedShow.LastEpisode == null || LinkedShow.LastEpisode.Empty)
			{
				if (LinkedShow.NextEpisode == null || LinkedShow.NextEpisode.Empty)
				{
					L_Act1.Text = "Check back later in case something new happens!";
					L_Act1.Visible = true;
					L_Act2.Visible = L_Act3.Visible = L_LastEp.Visible = L_NextEp.Visible = false;
				}
				else
				{
					L_Act2.Text = "Next Episode";
					L_NextEp.Text = LinkedShow.NextEpisode.ToString();
					L_Act3.Text = $"is airing {LinkedShow.NextEpisode.TMDbData.AirDate?.RelativeString()}";
					L_Act2.Visible = L_NextEp.Visible = L_Act3.Visible = true;
					L_Act1.Visible =  L_LastEp.Visible =  false;
				}
			}
			else
			{
				if (LinkedShow.NextEpisode == null || LinkedShow.NextEpisode.Empty)
				{
					L_Act1.Text = "Episode";
					L_LastEp.Text = LinkedShow.LastEpisode.ToString();
					L_Act2.Text = $"concluded its Season {LinkedShow.LastEpisode.TMDbData.AirDate?.RelativeString()}";
					L_Act1.Visible = L_LastEp.Visible = L_Act2.Visible = true;
					L_Act3.Visible = L_NextEp.Visible = false;
				}
				else
				{
					L_Act1.Text = "Episode";
					L_LastEp.Text = LinkedShow.LastEpisode.ToString();
					L_Act2.Text = $"aired {LinkedShow.LastEpisode.TMDbData.AirDate?.RelativeString()} with Episode";
					L_NextEp.Text = LinkedShow.NextEpisode.ToString();
					L_Act3.Text = $"airing {LinkedShow.NextEpisode.TMDbData.AirDate?.RelativeString()}";
					L_Act1.Visible = L_LastEp.Visible = L_Act2.Visible = L_NextEp.Visible = L_Act3.Visible = true;
				}
			}
		}

		public void SetSeason(Season season, Episode episode = null)
		{
			L_Season_Click(this, new EventArgs());
			this.ShowUp();
			var S = FLP_Seasons.Controls.Where(x => (x as SeasonLabel).Held).Count() > 0 ? (FLP_Seasons.Controls.Where(x => (x as SeasonLabel).Held).First() as SeasonLabel)?.Season : null;
			if (S != null && season == S)
				ShowEpisode(episode);
			else
			{
				currentSeason = season;

				foreach (SeasonLabel item in FLP_Seasons.Controls.Where(x => (x as SeasonLabel).Held))
					item.Release();

				FLP_Seasons.Controls.ThatAre<SeasonLabel>().FirstOrDefault(x => x.Season == season)?.Hold();

				L_SeasonName.Text = season.TMDbData.Name.IfEmpty($"Season {season.SeasonNumber}");
				L_SeasonOverview.Text = season.TMDbData.Overview.IfEmpty("No Overview");

				if (season.Poster != null)
					PB_SeasonPoster.Image = season.Poster;
				else
				{

					if (ConnectionHandler.State == Extensions.ConnectionState.Connected)
						SetSeasonPoster(ConnectionHandler.State);
					else
						ConnectionHandler.Connected += SetSeasonPoster;
				}

				FLP_Episodes.Controls.Clear();
				P_Episode.Controls.Clear();
				currentEpisode = null;

				foreach (var ep in season.Episodes.OrderBy(x => x.EN))
				{
					var label = new SeasonLabel(ep);

					label.Click += (s, e) => ShowEpisode(ep);

					FLP_Episodes.Controls.Add(label);

					if (ep == episode)
					{
						label.Hold();
						ShowEpisode(ep);
					}
				}
			}
		}

		private void SetSeasonPoster(Extensions.ConnectionState newState)
		{
			if (!string.IsNullOrWhiteSpace(currentSeason.TMDbData.PosterPath))
			{
				PB_SeasonPoster.Image = null;
				PB_SeasonPoster.LoadAsync($"https://image.tmdb.org/t/p/w200{currentSeason.TMDbData.PosterPath}");
			}
			else if (!string.IsNullOrWhiteSpace(currentSeason.Show.TMDbData.PosterPath))
			{
				PB_SeasonPoster.Image = null;
				PB_SeasonPoster.LoadAsync($"https://image.tmdb.org/t/p/w200{currentSeason.Show.TMDbData.PosterPath}");
			}
		}

		private void ShowEpisode(Episode ep)
		{
			if (ep == null)
				return;

			foreach (SeasonLabel item in FLP_Episodes.Controls.Where(x => (x as SeasonLabel).Held))
				item.Release();

			var label = (SeasonLabel)(FLP_Episodes.Controls.Where(x => (x as SeasonLabel).Episode == ep).FirstOrDefault());

			label.Hold();
			Scroll_Episodes.SetX(label.Left - (FLP_Episodes.Width / 2));

			P_Episode.SuspendDrawing();
			P_Episode.Controls.Clear();
			P_Episode.Controls.Add(currentEpisode = new EpisodeTile(ep) { Size = new Size((Width * 75 / 100).Between(500, 1000), 150) });
			P_Episode.ResumeDrawing();

			Scroll_Content.SetPerc(100);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
				e.Cancel = true;

			if (e.Cancel)
				Hide();

			base.OnFormClosing(e);
		}

		public void Form_Resize(object sender, EventArgs e)
		{
			if (currentEpisode != null)
				currentEpisode.Size = new Size((Width * 75 / 100).Between(500, 1000), 150);
		}

		private void ML_Zooqle_Click()
		{
			var result = MessagePrompt.ShowInput(
				$"Enter a Zooqle TV Show link for {LinkedShow.Name}",
				"Zooqle Link",
				LinkedShow.ZooqleURL.IfEmpty("https://zooqle.com/tv/"),
				PromptButtons.OKCancel,
				PromptIcons.Input,
				s => Regex.IsMatch(s, @"(https?://)?zooqle.com/tv/[^/]+", RegexOptions.IgnoreCase));

			if(result.DialogResult == DialogResult.OK)
			{
				LinkedShow.ZooqleURL = Regex.Match(result.Input, @"(https?://)?zooqle.com/tv/[^/]+", RegexOptions.IgnoreCase).Value;
				ML_Zooqle.Text = "Linked";
				ML_Zooqle.Image = ProjectImages.Icon_OK;
				ShowManager.Save(LinkedShow);
			}
		}

		private void ML_Zooqle_HoverStateChanged(HoverState newState)
		{
			if(!string.IsNullOrWhiteSpace(LinkedShow.ZooqleURL))
			{
				if (newState != HoverState.Normal)
				{
					ML_Zooqle.Text = "Change";
					ML_Zooqle.Image = ProjectImages.Icon_Link;
				}
				else
				{
					ML_Zooqle.Text = "Linked";
					ML_Zooqle.Image = ProjectImages.Icon_OK;
				}
			}
		}

		Label selectedTab;
		Label SelectedTab { get => selectedTab; set { selectedTab = value; value.ForeColor = FormDesign.Design.ActiveColor; value.BackColor = FormDesign.Design.BackColor; } }

		private void L_Info_Click(object sender, EventArgs e)
		{
			Scroll_Content.SizeSource = null;
			TLP_Contents.ColumnStyles[0].Width = 100;
			TLP_Contents.ColumnStyles[1].Width = TLP_Contents.ColumnStyles[2].Width = TLP_Contents.ColumnStyles[3].Width = 0;
			SelectedTab = L_Info;
			L_Season.ForeColor = L_Season.BackColor = L_Crew.ForeColor = L_Crew.BackColor = L_Similar.ForeColor = L_Similar.BackColor = Color.Empty;
			TLP_Info.Visible = true;
			TLP_Season.Visible = TLP_CastCrew.Visible = FLP_Similar.Visible = false;
            Scroll_Content.Reset();
        }

		private void L_Season_Click(object sender, EventArgs e)
		{
			Scroll_Content.SizeSource = null;
			TLP_Contents.ColumnStyles[1].Width = 100;
			TLP_Contents.ColumnStyles[0].Width = TLP_Contents.ColumnStyles[2].Width = TLP_Contents.ColumnStyles[3].Width = 0;
			SelectedTab = L_Season;
			L_Info.ForeColor = L_Info.BackColor = L_Crew.ForeColor = L_Crew.BackColor = L_Similar.ForeColor = L_Similar.BackColor = Color.Empty;
			TLP_Season.Visible = true;
			TLP_Info.Visible = TLP_CastCrew.Visible = FLP_Similar.Visible = false;
			Scroll_Episodes.ReEvaluate();
			Scroll_Episodes.SetPerc(0);
            Scroll_Content.Reset();
        }

		private void L_Crew_Click(object sender, EventArgs e)
		{
			Scroll_Content.SizeSource = () => (FLP_Cast.Visible ? FLP_Cast.Height + 48 + 6 : 0) + (FLP_Crew.Visible ? FLP_Crew.Height + 48 + 6 : 0) + 15;
			TLP_Contents.ColumnStyles[2].Width = 100;
			TLP_Contents.ColumnStyles[0].Width = TLP_Contents.ColumnStyles[1].Width = TLP_Contents.ColumnStyles[3].Width = 0;
			SelectedTab = L_Crew;
			L_Season.ForeColor = L_Season.BackColor = L_Info.ForeColor = L_Info.BackColor = L_Similar.ForeColor = L_Similar.BackColor = Color.Empty;
			TLP_CastCrew.Visible = true;
			TLP_Season.Visible = TLP_Info.Visible = FLP_Similar.Visible = false;
            Scroll_Content.Reset();
        }

		private void L_Similar_Click(object sender, EventArgs e)
		{
			Scroll_Content.SizeSource = () => FLP_Similar.Height + 20 + 15;
			TLP_Contents.ColumnStyles[3].Width = 100;
			TLP_Contents.ColumnStyles[0].Width = TLP_Contents.ColumnStyles[1].Width = TLP_Contents.ColumnStyles[2].Width = 0;
			SelectedTab = L_Similar;
			L_Season.ForeColor = L_Season.BackColor = L_Crew.ForeColor = L_Crew.BackColor = L_Info.ForeColor = L_Info.BackColor = Color.Empty;
			FLP_Similar.Visible = true;
			TLP_Season.Visible = TLP_CastCrew.Visible = TLP_Info.Visible = false;
			Scroll_Content.SetPerc(0);
            Scroll_Content.Reset();
        }

		private Bitmap GetStar(PictureBox picture, double perc, FormDesign design)
		{
			if (perc == 100)
				return ProjectImages.Icon_Star.Color(design.ActiveColor);
			else if (perc == 0)
				return ProjectImages.Icon_Star.Color(design.InfoColor);

			var bitmap = new Bitmap(ProjectImages.Icon_Star, new Size(28, 28));

			for (int x = 0; x < 28; x++)
			{
				for (int y = 0; y < 28; y++)
					bitmap.SetPixel(x, y, Color.FromArgb(bitmap.GetPixel(x, y).A, 100D * x / 28D <= perc ? design.ActiveColor : design.InfoColor));
			}

			return bitmap;
		}

		private void P_Content_Resize(object sender, EventArgs e)
		{
			TLP_Contents.MinimumSize = new Size(P_Content.Width, 0);
			TLP_Contents.MaximumSize = new Size(P_Content.Width, 9999);
		}

		private void PB_SeasonPoster_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			seasonThumbLoaded = !e.Cancelled && e.Error == null;
			PB_SeasonPoster.SizeMode = seasonThumbLoaded ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
		}

		private void PB_Thumb_Paint(object sender, PaintEventArgs e)
		{
			if (!LinkedShow.Temporary && Data.Options.ShowUnwatchedOnThumb && LinkedShow.Episodes.Any(x => !x.Watched && x.AirState == AirStateEnum.Aired))
			{
				if (LinkedShow.LastEpisode?.TMDbData != null && !LinkedShow.LastEpisode.Watched && (LinkedShow.LastEpisode.TMDbData.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-7))
				{
					var fontSize = 11F;
					var size = e.Graphics.MeasureString("NEW", new Font("Century Gothic", fontSize, FontStyle.Bold));
					var rect = new RectangleF(0, PB_Thumb.Height - (size.Height + 2) - (20), PB_Thumb.Width, size.Height + 4);

					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(220, FormDesign.Design.ActiveColor)), rect);
					e.Graphics.DrawString("NEW", new Font("Century Gothic", fontSize, FontStyle.Bold), new SolidBrush(Color.FromArgb(220, FormDesign.Design.ActiveForeColor)),
						new PointF((PB_Thumb.Width - size.Width + 4) / 2, rect.Y + (7 / 2) + 0));
				}
				else
				{
					var fontSize = 10.5F;
					var size = e.Graphics.MeasureString("UNWATCHED", new Font("Century Gothic", fontSize, FontStyle.Bold));
					var rect = new RectangleF(0, PB_Thumb.Height - (size.Height + 2) - (20), PB_Thumb.Width, size.Height + 4);

					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(220, FormDesign.Design.YellowColor)), rect);
					e.Graphics.DrawString("UNWATCHED", new Font("Century Gothic", fontSize, FontStyle.Bold), new SolidBrush(Color.FromArgb(220, FormDesign.Design.MenuColor)),
						new PointF((PB_Thumb.Width - size.Width + 4) / 2, rect.Y + (7 / 2) + 0));
				}
			}
		}

		private void PB_SeasonPoster_Paint(object sender, PaintEventArgs e)
		{
			if (!LinkedShow.Temporary && Data.Options.ShowUnwatchedOnThumb && currentSeason.Episodes.Any(x => !x.Watched && x.AirState == AirStateEnum.Aired))
			{
				if (currentSeason.Episodes.Any(x => !x.Watched && (x.TMDbData.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-7)))
				{
					var fontSize = 11F;
					var size = e.Graphics.MeasureString("NEW", new Font("Century Gothic", fontSize, FontStyle.Bold));
					var rect = new RectangleF(0, PB_SeasonPoster.Height - (size.Height + 2) - (20), PB_SeasonPoster.Width, size.Height + 4);

					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(220, FormDesign.Design.ActiveColor)), rect);
					e.Graphics.DrawString("NEW", new Font("Century Gothic", fontSize, FontStyle.Bold), new SolidBrush(Color.FromArgb(220, FormDesign.Design.ActiveForeColor)),
						new PointF((PB_SeasonPoster.Width - size.Width + 4) / 2, rect.Y + (7 / 2) + 0));
				}
				else
				{
					var fontSize = 10.5F;
					var size = e.Graphics.MeasureString("UNWATCHED", new Font("Century Gothic", fontSize, FontStyle.Bold));
					var rect = new RectangleF(0, PB_SeasonPoster.Height - (size.Height + 2) - (20), PB_SeasonPoster.Width, size.Height + 4);

					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(220, FormDesign.Design.YellowColor)), rect);
					e.Graphics.DrawString("UNWATCHED", new Font("Century Gothic", fontSize, FontStyle.Bold), new SolidBrush(Color.FromArgb(220, FormDesign.Design.MenuColor)),
						new PointF((PB_SeasonPoster.Width - size.Width + 4) / 2, rect.Y + (7 / 2) + 0));
				}
			}
		}
	}
}