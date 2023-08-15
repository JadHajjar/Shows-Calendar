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
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.UserControls;

namespace TVShowsCalendar.Forms
{
	public partial class DiscoverMediaForm : BaseForm
	{
		private bool IsMovie;

		public DiscoverMediaForm(bool isMovie = false)
		{
			InitializeComponent();
			IsMovie = isMovie;
			verticalScroll1.LinkedControl = FLP_Results;
			PB_Load.Image = FormDesign.Loader;
			PB_Load.BackColor = FormDesign.Design.BackColor;
			new Action(LoadData).RunInBackground();
			StartLoader();
		}

		private void B_Max_Click(object sender, EventArgs e) => WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;

		private void B_Min_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

		private async void LoadData()
		{
			if (IsMovie)
			{
				var media = (await Data.TMDbHandler.DiscoverMovieShow()).Where(x => MovieManager.Movies.All(y => y.TMDbData.Id != x.Id));

				Invoke(new Action(() =>
				{
					FLP_Results.SuspendDrawing();
					foreach (var item in media)
					{
						var mediaViewer = new MediaViewer(item) { Anchor = AnchorStyles.Top };
						FLP_Results.Controls.Add(mediaViewer);
						mediaViewer.Show();
					}
					FLP_Results.ResumeDrawing();

					if (media.Count() == 0)
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

					PB_Load.Dispose();
					StopLoader();
					verticalScroll1.Reset();
				}));
			}
			else
			{
				var media = (await Data.TMDbHandler.DiscoverTvShow()).Where(x => ShowManager.Shows.All(y => y.TMDbData.Id != x.Id));

				Invoke(new Action(() =>
				{
					FLP_Results.SuspendDrawing();
					foreach (var item in media)
					{
						var mediaViewer = new MediaViewer(item) { Anchor = AnchorStyles.Top };
						FLP_Results.Controls.Add(mediaViewer);
						mediaViewer.Show();
					}
					FLP_Results.ResumeDrawing();

					if (media.Count() == 0)
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

					PB_Load.Dispose();
					StopLoader();
					verticalScroll1.Reset();
				}));
			}
		}

		private void DiscoverShowsForm_Layout(object sender, LayoutEventArgs e)
		{
			FLP_Results.MaximumSize = new Size(panel1.Width, 9999);
			FLP_Results.Left = (panel1.Width - FLP_Results.Width) / 2;
		}

		private void DiscoverShowsForm_Resize(object sender, EventArgs e) => DiscoverShowsForm_Layout(null, null);

		private void B_Done_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}