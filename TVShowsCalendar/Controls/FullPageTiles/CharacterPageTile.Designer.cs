using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using ProjectImages = ShowsCalendar.Properties.Resources;
using SlickControls;

namespace ShowsCalendar
{
	partial class CharacterPageTile
	{
		private SlickImageBackgroundFlowLayoutPanel CreditsPanel;
		private ImageSideButton[] SideButtons;
		private ImageSocialLinksControl SocialLinksControl;
		private ImageLabelText OverviewLabel;
		private ImageLabelText BirthdayLabel;
		private ImageLabelText DeathdayLabel;
		private ImageLabelText CreditsLabel;
		private ImageLabelText GenderLabel;
		private ImageLabelText AKALabel;
		private SlickImageBackgroundControl LoadingControl;

		private void InitializeComponent()
		{
			ActionButtonsPanel.Visible = false;
			PosterImage.Cursor = null;
			SideButtons = Enum.GetValues(typeof(Page)).Cast<Page>().Select(item =>
			{
				var btn = new ImageSideButton
				{
					Dock = DockStyle.Bottom,
					Cursor = Cursors.Hand,
					PageId = (int)item,
				};

				switch (item)
				{
					case Page.Info: btn.Image = ProjectImages.Icon_Info; break;
					case Page.Images: btn.Image = ProjectImages.Icon_Images; break;
					case Page.Local: btn.Image = ProjectImages.Icon_Library; break;
					case Page.Credits: btn.Image = ProjectImages.Icon_Career; break;
				}

				btn.Selected = item == currentPage;
				btn.MouseClick += btnPage_MouseClick;
				PageButtonsPanel.Controls.Add(btn);

				return btn;
			}).ToArray();

			MainPanel.Controls.Add(LoadingControl = new SlickImageBackgroundControl { Loading = true, Dock = DockStyle.Fill });
			MainPanel.Controls.Add(OverviewLabel = new ImageLabelText("Biography"));
			MainPanel.Controls.Add(CreditsLabel = new ImageLabelText("Popular Credits"));
			MainPanel.Controls.Add(CreditsPanel = new SlickImageBackgroundFlowLayoutPanel
			{
				Dock = DockStyle.Top,
				AutoSize = true
			});

			_SidePanel.Controls.Add(SocialLinksControl = new ImageSocialLinksControl());
			SidePanel.Controls.Add(BirthdayLabel = new ImageLabelText("Born"));
			SidePanel.Controls.Add(DeathdayLabel = new ImageLabelText("Passed away"));
			SidePanel.Controls.Add(GenderLabel = new ImageLabelText("Gender"));
			SidePanel.Controls.Add(AKALabel = new ImageLabelText("Also known as"));
		}

		private void btnPage_MouseClick(object sender, MouseEventArgs e)
		{
			CurrentPage = (Page)(sender as ImageSideButton).PageId;
		}
	}
}
