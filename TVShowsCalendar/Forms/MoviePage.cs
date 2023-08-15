using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
	public partial class MoviePage : BaseForm
	{
		private enum Tabs { Info, Season, Cast, Similar };
		
		Movie LinkedMovie;

		public MoviePage(Movie movie)
		{
			InitializeComponent();
			
			LinkedMovie = movie;

			SelectedTab = L_Info;
			TLP_Contents.ColumnStyles[1].Width = TLP_Contents.ColumnStyles[2].Width = TLP_Contents.ColumnStyles[3].Width = 0;
			Scroll_Content.LinkedControl = TLP_Contents;

			movie.MovieLoaded += Movie_MovieLoaded;
			Movie_MovieLoaded(movie);
			
			L_Info_Click(this, new EventArgs());

			if(movie.Temporary)
				PB_Thumb.Color(FormDesign.Design.IconColor);

			if (!movie.SimilarMovies?.Any() ?? true)
				StartLoader();
		}

		private void LoadPoster(Extensions.ConnectionState state)
		{
			PB_Thumb.SizeMode = PictureBoxSizeMode.CenterImage;
			PB_Thumb.Image = FormDesign.Loader;
			PB_Thumb.LoadAsync($"https://image.tmdb.org/t/p/w200{LinkedMovie.TMDbData.PosterPath}");
		}

		private void PB_Thumb_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			PB_Thumb.SizeMode = e.Cancelled || e.Error != null ? PictureBoxSizeMode.CenterImage : PictureBoxSizeMode.Normal;
			if (!e.Cancelled && e.Error == null)
				LinkedMovie.Poster = (Bitmap)PB_Thumb.Image;
		}

		public void MovieAdded()
		{
			MovieManager.Add(LinkedMovie);
			LinkedMovie.Temporary = false;
			//Data.MoviesForm.FLP_Tiles.Controls.Add(new MovieTile(LinkedMovie));
		}

		private void Movie_MovieLoaded(Movie movie)
		{
			this.TryInvoke(() =>
			{
				FLP_Cast.SuspendDrawing();
				FLP_Crew.SuspendDrawing();
				FLP_Similar.SuspendDrawing();
				TLP_Contents.SuspendDrawing();

				Text = movie.Title;
				L_Status.Text = $"{movie.TMDbData.Status} • {movie.TMDbData.ReleaseDate.If(x => x == null | x == DateTime.MinValue, "No release date yet", movie.TMDbData.ReleaseDate?.ToReadableString())}";

				L_Overview.Text = movie.TMDbData.Overview.IfEmpty("No Overview");
				L_Rating.Text = movie.TMDbData.VoteCount > 0 ? $"{movie.TMDbData.VoteAverage} / 10" : "No Ratings";
				L_Genres.Text = movie.TMDbData.Genres == null || movie.TMDbData.Genres.Count == 0 ? "Unknown" : movie.TMDbData.Genres.ListStrings(x => x.Name + " • ").TrimEnd(3);
				L_Runtime.Text = movie.TMDbData.Runtime != null ? (TimeSpan.FromMinutes(movie.TMDbData.Runtime.Value).ToReadableString()) : "Unknown";
				L_Adult.Text = movie.TMDbData.Adult ? "Yes" : "No";
				L_Budget.Text = movie.TMDbData.Budget == 0 ? "Unknown" : $"$ {movie.TMDbData.Budget.ToString().Reverse().ListStrings(x => $"{x}").RegexReplace(@"(\d{3})(?=.)", x => x + "'").Reverse().ListStrings(x => $"{x}")}";
				L_Tagline.Text = movie.TMDbData.Tagline.IfEmpty("Unknown");
				L_Popularity.Text = $"{Math.Round(movie.TMDbData.Popularity / 5).Between(0, 100)}%";
				if (string.IsNullOrWhiteSpace(movie.TMDbData.Homepage))
				{
					ML_Homepage.Text = "N/A";
					ML_Homepage.Enabled = false;
				}
				else
				{
					ML_Homepage.Enabled = true;
					ML_Homepage.Text = Regex.Match(movie.TMDbData.Homepage, @"([a-z]{1,2}tps?):\/\/((?:(?!(?:\/|#|\?|&)).)+)(?:(\/(?:(?:(?:(?!(?:#|\?|&)).)+\/))?))?(?:((?:(?!(?:\.|$|\?|#)).)+))?(?:(\.(?:(?!(?:\?|$|#)).)+))?(?:(\?(?:(?!(?:$|#)).)+))?(?:(#.+))?").Groups[2].Value.RegexRemove("www\\.");
					SlickTip.SetTo(ML_Homepage, movie.TMDbData.Homepage.IfEmpty("N/A"));
				}

				if (LinkedMovie.Poster != null)
					PB_Thumb.Image = LinkedMovie.Poster;
				else
				{
					ConnectionHandler.Connected -= LoadPoster;
					ConnectionHandler.Connected += LoadPoster;
				}

				L_11.Visible = movie.Cast.Count > 0;
				L_12.Visible = movie.Crew.Count > 0;

				if (movie.Cast.Count + movie.Crew.Count == 0)
					TLP_Tabs.ColumnStyles[2].Width = 0;
				else
					TLP_Tabs.ColumnStyles[2].Width = 33.33F;

				if (movie.SimilarMovies == null || movie.SimilarMovies.Count == 0)
					TLP_Tabs.ColumnStyles[3].Width = 0;
				else
					TLP_Tabs.ColumnStyles[3].Width = 33.33F;

				FLP_Cast.Controls.Clear();
				foreach (var cast in movie.Cast)
					FLP_Cast.Controls.Add(new CharacterControl(cast));

				FLP_Crew.Controls.Clear();
				foreach (var crew in movie.Crew)
					FLP_Crew.Controls.Add(new CharacterControl(crew));

				FLP_Similar.Controls.Clear();
				if (movie.SimilarMovies != null)
					foreach (var sim in movie.SimilarMovies)
						FLP_Similar.Controls.Add(new MediaViewer(sim));

				FLP_Cast.ResumeDrawing();
				FLP_Crew.ResumeDrawing();
				FLP_Similar.ResumeDrawing();
				TLP_Contents.ResumeDrawing();

				StopLoader();
			});
		}

		private void PaintStars(FormDesign design)
		{
			PB_Star_1.Image = GetStar(PB_Star_1, 100 * (LinkedMovie.TMDbData.VoteAverage > 2 ? 1 : LinkedMovie.TMDbData.VoteAverage / 2D), design);
			PB_Star_2.Image = GetStar(PB_Star_2, 100 * (LinkedMovie.TMDbData.VoteAverage > 4 ? 1 : (LinkedMovie.TMDbData.VoteAverage - 2) / 2D), design);
			PB_Star_3.Image = GetStar(PB_Star_3, 100 * (LinkedMovie.TMDbData.VoteAverage > 6 ? 1 : (LinkedMovie.TMDbData.VoteAverage - 4) / 2D), design);
			PB_Star_4.Image = GetStar(PB_Star_4, 100 * (LinkedMovie.TMDbData.VoteAverage > 8 ? 1 : (LinkedMovie.TMDbData.VoteAverage - 6) / 2D), design);
			PB_Star_5.Image = GetStar(PB_Star_5, 100 * (LinkedMovie.TMDbData.VoteAverage == 10 ? 1 : (LinkedMovie.TMDbData.VoteAverage - 8) / 2D), design);
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			tableLayoutPanel8.BackColor = design.BackColor;
			TLP_Tabs.BackColor = design.MenuColor;
			selectedTab.BackColor = design.BackColor;
			selectedTab.ForeColor = design.ActiveColor;
			L_1.ForeColor = L_2.ForeColor = L_3.ForeColor = L_4.ForeColor = L_5.ForeColor = 
				L_6.ForeColor = L_7.ForeColor =  L_11.ForeColor = L_12.ForeColor = 
				L_13.ForeColor = L_14.ForeColor = L_15.ForeColor = design.LabelColor;
			
			PaintStars(design);
			PB_Thumb.Refresh();
		}

		Label selectedTab;
		Label SelectedTab { get => selectedTab; set { selectedTab = value; value.ForeColor = FormDesign.Design.ActiveColor; value.BackColor = FormDesign.Design.BackColor; } }

		private void L_Info_Click(object sender, EventArgs e)
		{
			Scroll_Content.SizeSource = null;
			TLP_Contents.ColumnStyles[0].Width = 100;
			TLP_Contents.ColumnStyles[1].Width = TLP_Contents.ColumnStyles[2].Width = 0;
			SelectedTab = L_Info;
			L_Crew.ForeColor = L_Crew.BackColor = L_Similar.ForeColor = L_Similar.BackColor = Color.Empty;
			TLP_Info.Visible = true;
			TLP_CastCrew.Visible = FLP_Similar.Visible = false;
		}

		private void L_Crew_Click(object sender, EventArgs e)
		{
			Scroll_Content.SizeSource = () => (FLP_Cast.Visible ? FLP_Cast.Height + 48 + 6 : 0) + (FLP_Crew.Visible ? FLP_Crew.Height + 48 + 6 : 0) + 15;
			TLP_Contents.ColumnStyles[1].Width = 100;
			TLP_Contents.ColumnStyles[0].Width = TLP_Contents.ColumnStyles[2].Width = 0;
			SelectedTab = L_Crew;
			L_Info.ForeColor = L_Info.BackColor = L_Similar.ForeColor = L_Similar.BackColor = Color.Empty;
			TLP_CastCrew.Visible = true;
			TLP_Info.Visible = FLP_Similar.Visible = false;
		}

		private void L_Similar_Click(object sender, EventArgs e)
		{
			Scroll_Content.SizeSource = () => FLP_Similar.Height + 20 + 15;
			TLP_Contents.ColumnStyles[2].Width = 100;
			TLP_Contents.ColumnStyles[0].Width = TLP_Contents.ColumnStyles[1].Width = 0;
			SelectedTab = L_Similar;
			L_Crew.ForeColor = L_Crew.BackColor = L_Info.ForeColor = L_Info.BackColor = Color.Empty;
			FLP_Similar.Visible = true;
			TLP_CastCrew.Visible = TLP_Info.Visible = false;
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

		private void PB_Thumb_Paint(object sender, PaintEventArgs e)
		{
			if (!LinkedMovie.Temporary && Data.Options.ShowUnwatchedOnThumb && !LinkedMovie.Watched && (LinkedMovie.TMDbData?.ReleaseDate ?? DateTime.MaxValue) < DateTime.Now)
			{
				if ((LinkedMovie.TMDbData?.ReleaseDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-30))
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

		private void MI_ShowMore_Click(object sender, EventArgs et)
		{
			new FlatToolStrip(new List<FlatStripItem>()
			{
				new FlatStripItem("Play", () => { }, image: ProjectImages.Icon_Play, show: LinkedMovie.VidFile?.Exists ?? false),

				new FlatStripItem("Add Show", MovieAdded, image: ProjectImages.Icon_Add, show: LinkedMovie.Temporary),

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
				new FlatStripItem("Reload", () => LinkedMovie.CheckForChanges(false, true, true), image: ProjectImages.Icon_Retry, show: !LinkedMovie.Temporary),
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

				new FlatStripItem("Delete", () => MovieManager.Remove(LinkedMovie), image: ProjectImages.Icon_Trash, show: !LinkedMovie.Temporary),

				new FlatStripItem("Mark as " + (LinkedMovie.Watched ? "Unwatched" : "Watched"), show: !LinkedMovie.Temporary, image: ProjectImages.Icon_OK, action: () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					LinkedMovie.Watched = !LinkedMovie.Watched;
					new Action(()=>
					{
						LocalMovieHandler.Refresh(LinkedMovie);
						MovieManager.Save(LinkedMovie);
					}).RunInBackground();
					PB_Thumb.Refresh();
					Cursor.Current = Cursors.Default;
				})
			}).ShowUp();
		}

		private void TLP_Info_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(LinkedMovie.TMDbData?.Homepage))
				Process.Start(LinkedMovie.TMDbData.Homepage);
		}
	}
}