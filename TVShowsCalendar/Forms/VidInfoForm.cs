using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using Extensions;

namespace TVShowsCalendar.Forms
{
	public partial class VidInfoForm : Form
	{
		private VidInfoForm(Vlc.DotNet.Forms.VlcControl vlcControl)
		{
			InitializeComponent();

			var vidInf = vlcControl.GetCurrentMedia().TracksInformations.FirstThat(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video;

			L_Duration.Text = TimeSpan.FromMilliseconds(vlcControl.Length).ToReadableString();
			L_Resolution.Text = $"{vidInf.Width} x {vidInf.Height} pixels";
			L_Subtitles.Text = vlcControl.SubTitles.Count.If(x => x == 0, x => $"None", x => $"{x} SubTitles");

			DesignChanged(FormDesign.Design);
		}

		public VidInfoForm(Episode ep, Vlc.DotNet.Forms.VlcControl vlcControl) : this(vlcControl)
		{
			L_Show.Text = $"{ep.Show.Name}, {ep.Show.TMDbData.Status}";
			L_Episode.Text = ep.ToString();
			L_Plot.Text = ep.TMDbData?.Overview;

			if (ep.AirState == AirStateEnum.Aired)
				L_AirDate.Text = $"Aired {ep.TMDbData.AirDate?.RelativeString()}";
			else if (ep.AirState == AirStateEnum.ToBeAired)
				L_AirDate.Text = $"Airing {ep.TMDbData.AirDate?.RelativeString()}";
			else
				L_AirDate.Visible = L_3.Visible = false;
		}

		public VidInfoForm(Movie movie, Vlc.DotNet.Forms.VlcControl vlcControl) : this(vlcControl)
		{
			L_Show.Text = $"{movie.Title}, {movie.TMDbData.Status}";
			L_1.Text = "Movie:";
			L_Episode.Text = movie.TMDbData.Tagline;
			L_2.Text = "Tag-line:";
			L_Plot.Text = movie.TMDbData?.Overview;

			if (movie.tMDbData.ReleaseDate != null)
			{
				if (movie.tMDbData.ReleaseDate < DateTime.Now)
					L_AirDate.Text = $"Aired {movie.tMDbData.ReleaseDate?.RelativeString()}";
				else 
					L_AirDate.Text = $"Airing {movie.tMDbData.ReleaseDate?.RelativeString()}";
			}
			else
				L_AirDate.Visible = L_3.Visible = false;
		}

		protected void DesignChanged(FormDesign design)
		{
			BackColor = design.ActiveColor;

			panel1.BackColor = verticalScroll1.BackColor = design.BackColor;

			ForeColor = design.ForeColor;

			L_1.ForeColor = L_2.ForeColor = L_3.ForeColor =
				L_4.ForeColor = L_5.ForeColor = L_6.ForeColor =
				 L_7.ForeColor = design.LabelColor;
		}

		private void panel1_Resize(object sender, EventArgs e)
		{
			tableLayoutPanel1.MaximumSize = new Size(panel1.Width, 9999);
			tableLayoutPanel1.MinimumSize = new Size(panel1.Width, 0);
		}

		private void VidInfoForm_Deactivate(object sender, EventArgs e)
		{
			Close();
		}

		private void VidInfoForm_Load(object sender, EventArgs e)
		{
			Height = tableLayoutPanel1.Height + 10;
		}
	}
}
