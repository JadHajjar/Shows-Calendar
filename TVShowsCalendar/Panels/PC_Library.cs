using SlickControls;

using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PC_Library : PanelContent
	{
		public PC_Library()
		{
			InitializeComponent();

			FirstFocusedControl = TB_Search;

			videoLibraryViewer.TopFolders = IO.Handler.GeneralFolders.Select(x => x.FullName).ToArray();
			videoLibraryViewer.Extensions = SlickControls.IO.VideoExtensions;
			videoLibraryViewer.SearchCleared += (s, e) => TB_Search.Text = string.Empty;
			videoLibraryViewer.LoadStarted += (s, e) => StartLoader();
			videoLibraryViewer.LoadEnded += (s, e) => StopLoader();
			videoLibraryViewer.FileOpened += (s, e) => Data.Mainform.Play(e.File.FullName);
			videoLibraryViewer.RightClickContext = FileRightClick;
		}

		private SlickStripItem[] FileRightClick(IOControl arg)
		{
			if (arg.FileObject == null) return null;

			var ep = ShowManager.Shows.SelectMany(x => x.Seasons).SelectMany(x => x.Episodes).FirstOrDefault(x => x.VidFiles.Any(y => y.Path.Equals(arg.FileObject.FullName, StringComparison.InvariantCultureIgnoreCase)));
			var mov = MovieManager.Movies.FirstOrDefault(x => x.VidFiles.Any(y => y.Path.Equals(arg.FileObject.FullName, StringComparison.InvariantCultureIgnoreCase)));

			return new[]
			{
				new SlickStripItem("Play Video", () => Data.Mainform.Play(arg.FileObject.FullName), Properties.Resources.Tiny_Play),

				new SlickStripItem("View Episode", () => Data.Mainform.PushPanel(null, new PC_EpisodeView(ep)), Properties.Resources.Tiny_TV, ep != null),

				new SlickStripItem("View Movie", () => Data.Mainform.PushPanel(null, new PC_MoviePage(mov)), Properties.Resources.Tiny_Movie, mov != null),
			};
		}

		public override bool OnWndProc(Message m)
		{
			if (m.Msg == 0x210 && m.WParam == (IntPtr)0x1020b)
				return videoLibraryViewer.GoBack();

			return false;
		}

		public override bool KeyPressed(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape || (keyData == Keys.Back && string.IsNullOrWhiteSpace(TB_Search.Text)))
			{
				if (videoLibraryViewer.GoBack())
					return true;
			}

			if (keyData == (Keys.Control | Keys.F))
			{
				if (TB_Search.Focus())
					return true;
			}

			return false;
		}

		private void TB_Search_TextChanged(object sender, EventArgs e) => videoLibraryViewer.Search(TB_Search.Text);

		private void PC_Library_Shown(object sender, EventArgs e)
		{
			using (var g = CreateGraphics())
			{
				var bnds = g.Measure(SP_Library.Text, UI.Font(9.75F, FontStyle.Bold));

				TB_Search.Top = (int)bnds.Height + 25 + 8 - TB_Search.Height;
			}

			var newWid = (int)(TB_Search.Width * UI.FontScale);
			TB_Search.Left -= newWid - TB_Search.Width;
			TB_Search.Width = newWid;
		}
	}
}