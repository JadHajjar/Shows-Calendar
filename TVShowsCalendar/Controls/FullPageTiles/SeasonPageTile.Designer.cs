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
	partial class SeasonPageTile
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
		private ImageLabelText EpisodeLabel;
		private ImageLabelText EpisodeRuntimeLabel;
		private ImageTagsPanel TagsPanel;
		private ImageContentControl<Episode> EpisodeControl;
		private SlickImageBackgroundFlowLayoutPanel EpisodesPanel;
		private SlickImageBackgroundPanel EpisodesParentPanel;
		private ImageLabelText FeaturesLabel;
		private SlickImageBackgroundFlowLayoutPanel FeaturesPanel;

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
					case Page.Episodes: btn.Image = ProjectImages.Icon_Seasons; break;
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

			_MainPanel.Controls.Add(EpisodesParentPanel = new SlickImageBackgroundPanel
			{
				Dock = DockStyle.Fill,
				Visible = false,
				AutoScroll = true
			});

			EpisodesParentPanel.Controls.Add(new ImageLabelText("Episodes") { Text = $"{(char)0x200B}" });
			EpisodesParentPanel.Controls.Add(EpisodesPanel = new SlickImageBackgroundFlowLayoutPanel
			{
				Dock = DockStyle.Top,
				AutoSize = true
			});

			MainPanel.Controls.Add(OverviewLabel = new ImageLabelText("Overview"));
			MainPanel.Controls.Add(EpisodeRuntimeLabel = new ImageLabelText("Episodes & Watch Time"));
			MainPanel.Controls.Add(TagsPanel = new ImageTagsPanel("Tags", true));
			MainPanel.Controls.Add(EpisodeLabel = new ImageLabelText("Last Aired Episode") { Text = $"{(char)0x200B}" });
			MainPanel.Controls.Add(EpisodeControl = new ImageContentControl<Episode>(null) { Dock = DockStyle.Top, Margin = new Padding(7, 0, 0, 0) });
			MainPanel.Controls.Add(FeaturesLabel = new ImageLabelText("Featuring") { Text = $"{(char)0x200B}" });
			MainPanel.Controls.Add(FeaturesPanel = new SlickImageBackgroundFlowLayoutPanel
			{
				Dock = DockStyle.Top,
				AutoSize = true
			});

			_SidePanel.Controls.Add(SocialLinksControl = new ImageSocialLinksControl());
			SidePanel.Controls.Add(StatusLabel = new ImageLabelText("Status"));
			SidePanel.Controls.Add(NetworksControl = new ImageNetworkIconsControl());
			SidePanel.Controls.Add(TypeLabel = new ImageLabelText("Type"));
		}

		private void actionButton_PrePaint(object sender, PaintEventArgs e)
		{
			ActionButton.Image = ShowManager.Show(ContentInfo.Show.Id) == null
				? ProjectImages.Icon_Plus : (!(ContentInfo.Show.GetCurrentWatchEpisode(ContentInfo.SeasonNumber)?.Watched ?? true))
				? ProjectImages.Icon_PlaySlick : ContentInfo.Episodes.Any(x => !x.Watched && x.AirState == AirStateEnum.Aired && !x.Playable)
				? ProjectImages.Icon_Download
				: ProjectImages.Icon_PlayBack;
		}

		private void btnPage_MouseClick(object sender, MouseEventArgs e)
		{
			CurrentPage = (Page)(sender as ImageSideButton).PageId;
		}
	}
}
