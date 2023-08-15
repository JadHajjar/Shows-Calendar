using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PC_AddMedia : PanelContent
	{
		private readonly bool isMovie;
		private string lastSearch;
		private int page;

		public PC_AddMedia(bool movie = false, string search = null)
		{
			InitializeComponent();
			isMovie = movie;
			TB_SeriesName.Text = search;
			B_ViewMore.Parent = null;

			if (movie)
			{
				Text = "Add Movies";
				TB_SeriesName.LabelText = "Movie Name:";
				TB_SeriesName.Placeholder = "Type the name of a Movie to search";
			}
			else
			{
				Text = "Add Shows";
			}

			SlickTip.SetTo(ML_Info, $"Search for a {(isMovie ? "movie" : "tv show")} or scroll through the discovery then just click on one to add it to your library");
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);
			P_Spacer.BackColor = design.AccentColor;
		}

		private void Info_Click(object sender, EventArgs e) => ShowPrompt($"Search for a {(isMovie ? "movie" : "tv show")} or scroll through the discovery then just click on one to add it to your library", "Info", PromptButtons.OK, PromptIcons.Info);

		private void TB_FolderPath_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(TB_SeriesName.Text) && string.IsNullOrWhiteSpace(TB_SeriesName.Text))
				TB_SeriesName.Text = string.Empty;
			else if (TB_SeriesName.Text != lastSearch)
			{
				lastSearch = TB_SeriesName.Text;
				page = 1;
				B_ViewMore.Parent = null;
				FLP_Results.SuspendDrawing();
				FLP_Results.Controls.Clear(true);
				FLP_Results.ResumeDrawing();
				AbortLoad();
				StartDataLoad();
			}
		}

		private readonly TicketBooth ticketBooth = new TicketBooth();

		protected override bool LoadData()
		{
			this.TryInvoke(() =>
			{
				ML_Info.Loading = true;
			});

			var ticket = ticketBooth.GetTicket();

			var data = getData(isMovie);

			if (ticketBooth.IsLast(ticket))
			{
				showResults(data, false);

				if (data.Count() <= 4)
				{
					data = getData(!isMovie);

					if (ticketBooth.IsLast(ticket))
						showResults(data, true);
				}
			}

			IEnumerable<dynamic> getData(bool movie)
			{
				if (!movie)
				{
					return string.IsNullOrWhiteSpace(TB_SeriesName.Text)
					   ? Data.TMDbHandler.DiscoverTvShow(page).Result.Select(LightContent.Convert)
					   : Data.TMDbHandler.SearchTvShow(TB_SeriesName.Text, page).Result.Select(LightContent.Convert);
				}
				else
				{
					return string.IsNullOrWhiteSpace(TB_SeriesName.Text)
						? Data.TMDbHandler.DiscoverMovies(page).Result.Select(LightContent.Convert)
						: Data.TMDbHandler.SearchMovie(TB_SeriesName.Text.Trim().RegexRemove(@"\s\d{4}$").Trim(), page, true, Regex.Match(TB_SeriesName.Text.Trim(), @"\s\d{4}$").Value.SmartParse()).Result.Select(LightContent.Convert);
				}
			}

			return true;
		}

		private void showResults(IEnumerable<dynamic> items, bool extra) => this.TryInvoke(() =>
		{
			if (items.Any())
			{
				if (extra)
				{
					FLP_Results.Controls.Add(new SlickSpacer
					{
						MinimumSize = new Size(FLP_Results.Parent.Width, 0),
						Margin = new Padding(0, 40, 0, 40),
						Padding = new Padding(150, 0, 150, 0),
					});

					FLP_Results.Controls.Add(new Label
					{
						Text = $"Were you looking for {isMovie.If("TV Shows", "Movies")}?",
						ForeColor = FormDesign.Design.ForeColor,
						AutoSize = true,
						MinimumSize = new Size(FLP_Results.Parent.Width, 0),
						Font = UI.Font(9.75F, FontStyle.Bold),
						TextAlign = ContentAlignment.MiddleCenter,
						Margin = new Padding(0),
					});

					FLP_Results.Controls.Add(new Label
					{
						Text = $"Here are some that match your search",
						ForeColor = FormDesign.Design.InfoColor,
						AutoSize = true,
						MinimumSize = new Size(FLP_Results.Parent.Width, 0),
						Font = UI.Font(8.25F),
						TextAlign = ContentAlignment.MiddleCenter,
						Margin = new Padding(0, 7, 0, 40),
					});
				}

				var current = -FLP_Results.Parent.Top;
				FLP_Results.SuspendLayout();
				if (FLP_Results.Controls.Count > 1)
					FLP_Results.SetFlowBreak(FLP_Results.Controls[FLP_Results.Controls.Count - 2], false);
				var ind = FLP_Results.Controls.Count - 2;
				foreach (var item in items)
				{
					var mediaViewer = new MediaViewer(item) { Anchor = AnchorStyles.Top };
					FLP_Results.Controls.Add(mediaViewer);
				}
				FLP_Results.SetFlowBreak(FLP_Results.Controls[FLP_Results.Controls.Count - 1], true);

				if (items.Count() >= 20)
				{
					B_ViewMore.Parent = FLP_Results;
					B_ViewMore.SendToBack();
					B_ViewMore.Enabled = true;
				}
				else
					B_ViewMore.Parent = null;

				FLP_Results.ResumeLayout(true);

				verticalScroll.SetPercentage(100D * current / (FLP_Results.Height - FLP_Results.Parent.Parent.Height), true);
				if (current != 0)
					verticalScroll.ScrollTo(100D * (current + FLP_Results.Parent.Parent.Height - B_ViewMore.Height - B_ViewMore.Margin.Vertical) / (FLP_Results.Height - FLP_Results.Parent.Parent.Height), 5);
			}
			else
				B_ViewMore.Parent = null;

			if (FLP_Results.Controls.Count == 0)
			{
				System.Media.SystemSounds.Exclamation.Play();
				FLP_Results.Controls.Add(new Label
				{
					Text = "No Results Found",
					ForeColor = FormDesign.Design.ForeColor,
					AutoSize = true,
					MinimumSize = new Size(FLP_Results.Parent.Width, 0),
					Font = UI.Font(9.75F, FontStyle.Bold),
					TextAlign = ContentAlignment.MiddleCenter,
					Margin = new Padding(0, 150, 0, 7),
				});
				FLP_Results.Controls.Add(new Label
				{
					Text = "Make sure you typed your search correctly\r\nor try to shorten it to a word or two",
					ForeColor = FormDesign.Design.InfoColor,
					AutoSize = true,
					MinimumSize = new Size(FLP_Results.Parent.Width, 0),
					Font = UI.Font(8.25F),
					TextAlign = ContentAlignment.MiddleCenter,
					Margin = new Padding(0),
				});

				FLP_Results.SetFlowBreak(FLP_Results.Controls[0], true);
				FLP_Results.SetFlowBreak(FLP_Results.Controls[1], true);
			}

			ML_Info.Image = Properties.Resources.Icon_Info;
		});

		private void B_ViewMore_Click(object sender, EventArgs e)
		{
			page++;
			B_ViewMore.Enabled = false;
			AbortLoad();
			StartDataLoad();
		}

		private void PC_AddMedia_Resize(object sender, EventArgs e)
		{
			B_ViewMore.Margin = new Padding((FLP_Results.Width - B_ViewMore.Width) / 2, 10, (FLP_Results.Width - B_ViewMore.Width) / 2, 10);
		}

		private void TB_SeriesName_Leave(object sender, EventArgs e) => TB_SeriesName.Focus();

		private void verticalScroll_Scroll(object sender, ScrollEventArgs e)
		{
			P_Spacer.Visible = verticalScroll.Percentage > 0;
		}

		private void panel1_LocationChanged(object sender, EventArgs e)
		{
			verticalScroll.Padding = new Padding(0, panel1.Top, 0, 0);
		}
	}
}