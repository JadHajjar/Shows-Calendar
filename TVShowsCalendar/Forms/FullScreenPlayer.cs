using System.Drawing;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public class FullScreenPlayer : Form
	{
		public FullScreenPlayer(PC_Player player)
		{
			Player = player;
			BackColor = Color.Black;
			FormBorderStyle = FormBorderStyle.None;
			WindowState = FormWindowState.Maximized;
			TopMost = true;
			ShowInTaskbar = false;
			ShowIcon = false;
			Icon = (Icon)new System.ComponentModel.ComponentResourceManager(typeof(FullScreenPlayer)).GetObject("$this.Icon");
			ShowIcon = false;
			ShowInTaskbar = false;
		}

		public PC_Player Player { get; }

		private void InitializeComponent()
		{
			this.SuspendLayout();
			//
			// FullScreenPlayer
			//
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "FullScreenPlayer";
			this.ResumeLayout(false);
		}
	}
}