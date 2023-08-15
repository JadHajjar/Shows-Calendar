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
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using TVShowsCalendar.Classes;
using TVShowsCalendar.Forms;
using SlickControls.Forms;
using SlickControls.Classes;

namespace TVShowsCalendar
{
	public partial class NotifyForm : BaseForm
	{
		public NotifyForm(IEnumerable<Episode> episodes)
		{
			InitializeComponent();

			foreach (var item in episodes)
			{
				var tile = new EpisodeTile(item);
				tile.DownloadClicked += Tile_DownloadClicked;
				FLP_Episodes.Controls.Add(tile);
			}
			
			Size = new Size(episodes.Count() >= 6 ? 1000 : 500, GetHeight());

			verticalScroll1.LinkedControl = tableLayoutPanel1;
			SetTooltips();
			SetLocation();
		}

		public NotifyForm(IEnumerable<Movie> movies)
		{
			InitializeComponent();

			foreach (var item in movies)
			{
				//var tile = new EpisodeTile(item, new Size(450, 70));
				//tile.DownloadClicked += Tile_DownloadClicked;
				//FLP_Episodes.Controls.Add(tile);
			}

			Size = new Size(movies.Count() >= 6 ? 1000 : 500, GetHeight());

			verticalScroll1.LinkedControl = tableLayoutPanel1;
			SetTooltips();
			SetLocation();
		}

		private void Tile_DownloadClicked(object sender, EventArgs e)
		{
			FLP_Episodes.Controls.Remove((Control)sender);

			if (FLP_Episodes.Controls.Count == 0)
				Dispose();
			else
				Height = GetHeight();
			SetLocation();
		}

		private void SetLocation()
		{
			var w = SystemInformation.VirtualScreen.Width;
			var h = SystemInformation.VirtualScreen.Height;

			switch (GeneralMethods.GetTaskbarLocation())
			{
				case TaskbarLocation.Top:
					Location = new Point(w - Width - 80, 90);  break;
				case TaskbarLocation.Left:
					Location = new Point(90, h - Height - 80); break;
				case TaskbarLocation.Bottom:
					Location = new Point(w - Width - 80, h - Height - 90); break;
				case TaskbarLocation.Right:
					Location = new Point(w - Width - 90, h - Height - 80); break;
				default:
					Location = new Point((w - Width) / 2, (h - Height) / 2); break;
			}
		}

		private void NotifyForm_Load(object sender, EventArgs e)
		{
			if (Data.Options.NotificationSound)
				new System.Media.SoundPlayer(Properties.Resources.TodayNotification).Play();
		}

		private void SetTooltips()
		{
			toolTip.AdvancedSetTooltip(B_Done, "Close this Window");
			toolTip.AdvancedSetTooltip(B_OpenAll, "Open all of the episodes' download window and close this window");
		}

		private void B_OpenAll_Click(object sender, EventArgs e)
		{
			Hide();
			foreach (EpisodeTile item in FLP_Episodes.Controls.Where(x => (x as EpisodeTile)?.Episode?.DownloadAvailable ?? false))
				(new DownloadForm(item.Episode)).ShowDialog();
			Close();
		}

		private int GetHeight()
		{
			return Math.Min(420, 90 + (FLP_Episodes.Controls.Count.If(x => x >= 6, FLP_Episodes.Controls.Count / 2) * 80));
		}

		private void NotifyForm_Resize(object sender, EventArgs e)
		{
			tableLayoutPanel1.MaximumSize = new Size(panel1.Width, 9999);
			tableLayoutPanel1.MinimumSize = new Size(panel1.Width, 0);
		}

        private void B_Done_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
