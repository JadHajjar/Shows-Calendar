using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.UserControls;

namespace TVShowsCalendar.Forms
{
	public partial class AddMediaForm : BaseForm
	{
		public Bitmap Poster { get; private set; }
		private bool Movie;

		public AddMediaForm(bool movie = false)
		{
			InitializeComponent();
			TB_SeriesName.TextChanged += TB_FolderPath_TextChanged;
			verticalScroll1.SizeSource = () => FLP_Results.Height;
			Movie = movie;

			if (movie)
			{
				Text = "Add Movie";
				TB_SeriesName.LabelText = "Movie Name:";
			}
			else
				Text = "Add Show";

			SlickTip.SetTo(ML_Info, $"Type in the name of a {(Movie ? "movie" : "show")} then select one from the results");
		}

		private void Info_Click(object sender, EventArgs e)
		{
			MessagePrompt.Show($"Type in the name of a {(Movie ? "movie" : "show")} then select one from the results.", "Info", PromptButtons.OK, PromptIcons.Info);
		}

		private WaitIdentifier LoadWaitIdentifier = new WaitIdentifier();
		private OneWayTask OneWayTask = new OneWayTask();
		private void TB_FolderPath_TextChanged(object sender, EventArgs e)
		{
			ML_Info.Image = FormDesign.Loader;
			ML_Info.Enabled = false;
			FLP_Results.Controls.Clear();

			LoadWaitIdentifier.Wait(() => OneWayTask.Run(LoadData), 750);
		}

		private TicketBooth ticketBooth = new TicketBooth();
		private async void LoadData()
		{
			if (string.IsNullOrWhiteSpace(TB_SeriesName.Text))
				return;

			var ticket = ticketBooth.GetTicket();

			if (!Movie)
			{
				var shows = (await Data.TMDbHandler.SearchTvShow(TB_SeriesName.Text)).Take(12);

				if (ticketBooth.IsLast(ticket) && InvokeRequired)
					Invoke(new Action(() =>
					{
						FLP_Results.SuspendLayout();
						foreach (var item in shows)
						{
							var mediaViewer = new MediaViewer(item) { Anchor = AnchorStyles.Top };
							FLP_Results.Controls.Add(mediaViewer);
							mediaViewer.Show();
						}
						FLP_Results.ResumeLayout(true);

						if (shows.Count() == 0)
						{
							System.Media.SystemSounds.Exclamation.Play();
							var label1 = new Label
							{
								Anchor = AnchorStyles.None,
								Size = new Size(145, 150),
								TextAlign = ContentAlignment.BottomCenter,
								Font = new Font("Century Gothic", 12F, (FontStyle.Bold | FontStyle.Italic), GraphicsUnit.Point, 0),
								Text = "No Results Found",
								ForeColor = FormDesign.Design.RedColor
							};

							FLP_Results.Controls.Add(label1);
						}

						ML_Info.Image = Properties.Resources.Icon_Info;
						ML_Info.Enabled = true;
					}));
			}
			else
			{
				var movies = (await Data.TMDbHandler.SearchMovie(TB_SeriesName.Text, 0, true)).Take(12);

				if (ticketBooth.IsLast(ticket) && InvokeRequired)
					Invoke(new Action(() =>
					{
						foreach (var item in movies)
						{
							var mediaViewer = new MediaViewer(item) { Anchor = AnchorStyles.None };
							FLP_Results.Controls.Add(mediaViewer);
							mediaViewer.Show();
						}

						if (movies.Count() == 0)
						{
							System.Media.SystemSounds.Exclamation.Play();
							var label1 = new Label
							{
								Anchor = AnchorStyles.None,
								Size = new Size(145, 150),
								TextAlign = ContentAlignment.BottomCenter,
								Font = new Font("Century Gothic", 12F, (FontStyle.Bold | FontStyle.Italic), GraphicsUnit.Point, 0),
								Text = "No Results Found",
								ForeColor = FormDesign.Design.RedColor
							};

							FLP_Results.Controls.Add(label1);
						}

						ML_Info.Image = Properties.Resources.Icon_Info;
						ML_Info.Enabled = true;
					}));
			}
		}

		private void TMDbForm_Layout(object sender, LayoutEventArgs e) => TMDbForm_Resize(sender, e);

		private void TMDbForm_Resize(object sender, EventArgs e)
		{
			tableLayoutPanel1.MaximumSize = new Size(panel1.Width, 9999);
			tableLayoutPanel1.MinimumSize = new Size(panel1.Width, 0);
		}
	}
}
