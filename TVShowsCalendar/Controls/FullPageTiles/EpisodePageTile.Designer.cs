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
	partial class EpisodePageTile
	{
		private ImageSideButton[] SideButtons;
		private ImageSideButton ActionButton;
		private ImageSideButton PreviousButton;
		private ImageSideButton NextButton;
		private ImageSocialLinksControl SocialLinksControl;
		private ImageNetworkIconsControl NetworksControl;
		private ImageLabelText OverviewLabel;
		private ImageLabelText StatusLabel;
		private ImageLabelText TypeLabel;
		private ImageLabelText SeasonLabel;
		private ImageLabelText ReleaseDateLabel;
		private ImageLabelText EpisodeRuntimeLabel;
		private ImageTagsPanel TagsPanel;
		private ImageContentControl<Season> SeasonControl;
		private ImageLabelText FeaturesLabel;
		private SlickImageBackgroundFlowLayoutPanel FeaturesPanel;
		private ImageLabelText GuestsLabel;
		private SlickImageBackgroundFlowLayoutPanel GuestsPanel;

		private void InitializeComponent()
		{
			ActionButton = new ImageSideButton
			{
				Dock = DockStyle.Top,
				Image = ProjectImages.Icon_PlaySlick,
				Cursor = Cursors.Hand,
			};
			ActionButton.PrePaint += actionButton_PrePaint;
			ActionButton.MouseClick += (s, e) => OnImageMouseClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));

			PreviousButton = new ImageSideButton
			{
				Dock = DockStyle.Top,
				Image = ProjectImages.Icon_Previous,
				Cursor = Cursors.Hand
			};
			PreviousButton.MouseClick += (s, e) => { if (e.Button == MouseButtons.Left) ParentPanel.JumpPrevious(); };

			NextButton = new ImageSideButton
			{
				Dock = DockStyle.Top,
				Image = ProjectImages.Icon_Next,
				Cursor = Cursors.Hand
			};
			NextButton.MouseClick += (s, e) => { if (e.Button == MouseButtons.Left) ParentPanel.JumpNext(); };

			ActionButtonsPanel.Controls.Add(ActionButton);
			ActionButtonsPanel.Controls.Add(PreviousButton);
			PageButtonsPanel.Controls.Add(NextButton);

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
					case Page.VidFiles: btn.Image = ProjectImages.Icon_VideoFile; break;
					case Page.Cast: btn.Image = ProjectImages.Icon_Cast; break;
					case Page.Crew: btn.Image = ProjectImages.Icon_Crew; break;
					case Page.Images: btn.Image = ProjectImages.Icon_Images; break;
					case Page.Videos: btn.Image = ProjectImages.Icon_Videos; break;
				}

				btn.Selected = item == currentPage;
				btn.MouseClick += btnPage_MouseClick;
				PageButtonsPanel.Controls.Add(btn);

				return btn;
			}).ToArray();

			MainPanel.Controls.Add(OverviewLabel = new ImageLabelText("Overview"));
			MainPanel.Controls.Add(EpisodeRuntimeLabel = new ImageLabelText("Episodes & Watch Time"));
			MainPanel.Controls.Add(TagsPanel = new ImageTagsPanel("Tags", true));
			MainPanel.Controls.Add(SeasonLabel = new ImageLabelText("Season") { Text = $"{(char)0x200B}" });
			MainPanel.Controls.Add(SeasonControl = new ImageContentControl<Season>(null, true) { Dock = DockStyle.Top, Margin = new Padding(7, 0, 0, 0), Padding=new  Padding(0,0,20,0) });
			MainPanel.Controls.Add(GuestsLabel = new ImageLabelText("\nGuest Staring"));
			MainPanel.Controls.Add(GuestsPanel = new SlickImageBackgroundFlowLayoutPanel
			{
				Dock = DockStyle.Top,
				AutoSize = true
			}); 
			MainPanel.Controls.Add(FeaturesLabel = new ImageLabelText("\nFeaturing"));
			MainPanel.Controls.Add(FeaturesPanel = new SlickImageBackgroundFlowLayoutPanel
			{
				Dock = DockStyle.Top,
				AutoSize = true
			});

			_SidePanel.Controls.Add(SocialLinksControl = new ImageSocialLinksControl());
			SidePanel.Controls.Add(StatusLabel = new ImageLabelText("Status"));
			SidePanel.Controls.Add(ReleaseDateLabel = new ImageLabelText("Release Date"));
			SidePanel.Controls.Add(NetworksControl = new ImageNetworkIconsControl());
			SidePanel.Controls.Add(TypeLabel = new ImageLabelText("Type"));
		}

		private void actionButton_PrePaint(object sender, PaintEventArgs e)
		{
			ActionButton.Image = ShowManager.Show(ContentInfo.Show.Id) == null
				? ProjectImages.Icon_Plus : ContentInfo.Playable
				? ProjectImages.Icon_PlaySlick : ContentInfo.AirState == AirStateEnum.Aired
				? ProjectImages.Icon_Download
				: null;

			ActionButton.Visible = CurrentPage == Page.Info && ActionButton.Image != null;
		}

		private void btnPage_MouseClick(object sender, MouseEventArgs e)
		{
			CurrentPage = (Page)(sender as ImageSideButton).PageId;
		}
	}
}
