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
	partial class ShowPageTile
	{
		private SlickImageBackgroundTableLayoutPanel LanguagePanel;
		private SlickImageBackgroundTableLayoutPanel EpisodePanel;
		private ImageSideButton[] SideButtons;
		private ImageSideButton ActionButton;
		private ImageSideButton TrailerButton;
		private ImageSocialLinksControl SocialLinksControl;
		private ImageNetworkIconsControl NetworksControl;
		private ImageLabelText OverviewLabel;
		private ImageLabelText StatusLabel;
		private ImageLabelText TypeLabel;
		private ImageLabelText DateAddedLabel;
		private ImageLabelText LanguageLabel;
		private ImageLabelText CountryLabel;
		private ImageLabelText OriginalTitleLabel;
		private ImageLabelText EpisodeRuntimeLabel;
		private ImageTagsPanel TagsPanel;
		private ImageTagsPanel KeywordsPanel;
		private ImageContentControl<Episode> EpisodeControl;
		private ImageSeasonControl SeasonControl;
		private ImageLabelText SeasonLabel;
		private ImageLabelText EpisodeLabel;
		private SlickImageBackgroundFlowLayoutPanel SeasonsPanel;
		private SlickImageBackgroundPanel SeasonsParentPanel;
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

			TrailerButton = new ImageSideButton
			{
				Dock = DockStyle.Top,
				Image = ProjectImages.Icon_Trailer,
				Cursor = Cursors.Hand
			};
			TrailerButton.MouseClick += (s, e) => { if (e.Button == MouseButtons.Left) ContentInfo.WatchTrailer(); };

			ActionButtonsPanel.Controls.Add(ActionButton);
			ActionButtonsPanel.Controls.Add(TrailerButton);

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
					case Page.Seasons: btn.Image = ProjectImages.Icon_Seasons; break;
					case Page.Cast: btn.Image = ProjectImages.Icon_Cast; break;
					case Page.Crew: btn.Image = ProjectImages.Icon_Crew; break;
					case Page.Images: btn.Image = ProjectImages.Icon_Images; break;
					case Page.Videos: btn.Image = ProjectImages.Icon_Videos; break;
					case Page.Similar: btn.Image = ProjectImages.Icon_Similar; break;
				}

				btn.Selected = item == currentPage;
				btn.MouseClick += btnPage_MouseClick;
				PageButtonsPanel.Controls.Add(btn);

				return btn;
			}).ToArray();

			_MainPanel.Controls.Add(SeasonsParentPanel = new SlickImageBackgroundPanel
			{
				Dock = DockStyle.Fill,
				Visible = false,
				AutoScroll = true
			});

			SeasonsParentPanel.Controls.Add(new ImageLabelText("Seasons") { Text = $"{(char)0x200B}" });
			SeasonsParentPanel.Controls.Add(SeasonsPanel = new SlickImageBackgroundFlowLayoutPanel
			{
				Dock = DockStyle.Top,
				AutoSize = true
			});

			MainPanel.Controls.Add(OverviewLabel = new ImageLabelText("Overview"));
			MainPanel.Controls.Add(LanguagePanel = new SlickImageBackgroundTableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Margin = new Padding(0, 0, 0, 10) });
			MainPanel.Controls.Add(EpisodeRuntimeLabel = new ImageLabelText("Episodes & Watch Time"));
			MainPanel.Controls.Add(TagsPanel = new ImageTagsPanel("Tags", true));
			MainPanel.Controls.Add(KeywordsPanel = new ImageTagsPanel("Keywords", false));
			MainPanel.Controls.Add(EpisodePanel = new SlickImageBackgroundTableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(0, 0, 0, 10) });
			MainPanel.Controls.Add(FeaturesLabel = new ImageLabelText("Featuring") { Text = $"{(char)0x200B}" });
			MainPanel.Controls.Add(FeaturesPanel = new SlickImageBackgroundFlowLayoutPanel
			{
				Dock = DockStyle.Top,
				AutoSize = true
			});

			EpisodePanel.RowStyles.Add(new RowStyle());
			EpisodePanel.RowStyles.Add(new RowStyle());
			EpisodePanel.ColumnStyles.Add(new ColumnStyle());
			EpisodePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			EpisodePanel.ColumnStyles.Add(new ColumnStyle());
			EpisodePanel.Add(EpisodeLabel = new ImageLabelText("Last Aired Episode") { Text = $"{(char)0x200B}" }, 2, 0);
			EpisodePanel.Add(EpisodeControl = new ImageContentControl<Episode>(null) { Dock = DockStyle.Right, Margin = new Padding(0, 10, 0, 0), Padding = new Padding(0, 0, 5, 0) }, 2, 1);
			EpisodePanel.Add(SeasonLabel = new ImageLabelText("Last Season") { Text = $"{(char)0x200B}" }, 0, 0);
			EpisodePanel.Add(SeasonControl = new ImageSeasonControl(null) { Dock = DockStyle.Left, Margin = new Padding(7, 10, 0, 0) }, 0, 1);

			_SidePanel.Controls.Add(SocialLinksControl = new ImageSocialLinksControl());
			SidePanel.Controls.Add(StatusLabel = new ImageLabelText("Status"));
			SidePanel.Controls.Add(NetworksControl = new ImageNetworkIconsControl());
			SidePanel.Controls.Add(TypeLabel = new ImageLabelText("Type"));
			SidePanel.Controls.Add(DateAddedLabel = new ImageLabelText("Date Added"));

			LanguagePanel.RowStyles.Add(new RowStyle());
			LanguagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
			LanguagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
			LanguagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));

			LanguagePanel.Add(LanguageLabel = new ImageLabelText("Language"), 0, 0);
			LanguagePanel.Add(CountryLabel = new ImageLabelText("Country"), 1, 0);
			LanguagePanel.Add(OriginalTitleLabel = new ImageLabelText("Original Title"), 2, 0);
		}

		private void actionButton_PrePaint(object sender, PaintEventArgs e)
		{
			ActionButton.Image = ShowManager.Show(ContentInfo.Id) == null
				? ProjectImages.Icon_Plus : (!(ContentInfo.GetCurrentWatchEpisode()?.Watched ?? true))
				? ProjectImages.Icon_PlaySlick : (ContentInfo.Episodes.FirstOrDefault(x => !x.Watched && !x.Playable)?.AirState ?? AirStateEnum.Unknown) == AirStateEnum.Aired
				? ProjectImages.Icon_Download
				: ProjectImages.Icon_PlayBack;
		}

		private void btnPage_MouseClick(object sender, MouseEventArgs e)
		{
			CurrentPage = (Page)(sender as ImageSideButton).PageId;
		}
	}
}
