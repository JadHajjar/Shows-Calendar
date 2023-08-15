using System.Windows.Forms;

namespace ShowsCalendar
{
	public class FullPageContent<T> : FullPageTile where T : IInteractableContent
	{
		public T ContentInfo { get; protected set; }
		public ImageSideButton DotsButton { get; }

		public FullPageContent()
		{
			DotsButton = new ImageSideButton
			{
				Dock = DockStyle.Top,
				Image = Properties.Resources.Icon_Dots_H,
				Cursor = Cursors.Hand
			};

			PageButtonsPanel.Controls.Add(DotsButton);
			DotsButton.MouseClick += DotsButton_MouseClick;
		}

		private void DotsButton_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				ContentInfo.ShowStrip(PointToClient(e.Location), true);
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				ContentInfo.ShowStrip(PointToClient(e.Location), true);
		}
	}
}