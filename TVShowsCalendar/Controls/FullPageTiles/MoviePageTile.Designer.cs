using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using ProjectImages = ShowsCalendar.Properties.Resources;
using SlickControls;
using Extensions;

namespace ShowsCalendar
{
	partial class MoviePageTile
	{
		private SlickImageBackgroundTableLayoutPanel LanguagePanel;
		private ImageSideButton[] SideButtons;
		private ImageSideButton ActionButton;
		private ImageSideButton TrailerButton;
		private ImageSocialLinksControl SocialLinksControl;
		private ImageNetworkIconsControl NetworksControl;
		private ImageLabelText OverviewLabel;
		private ImageLabelText StatusLabel;
		private ImageLabelText ReleaseDateLabel;
		private SlickImageBackgroundControl TaglineControl;
		private ImageLabelText DateAddedLabel;
		private ImageLabelText LanguageLabel;
		private ImageLabelText CountryLabel;
		private ImageLabelText OriginalTitleLabel;
		private ImageTagsPanel TagsPanel;
		private ImageTagsPanel KeywordsPanel;
		private ImageLabelText FeaturesLabel;
		private SlickImageBackgroundFlowLayoutPanel FeaturesPanel;
		private ImageLabelText DirectorLabel;
		private SlickImageBackgroundFlowLayoutPanel DirectorPanel;

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
					case Page.Cast: btn.Image = ProjectImages.Icon_Cast; break;
					case Page.Crew: btn.Image = ProjectImages.Icon_Crew; break;
					case Page.Images: btn.Image = ProjectImages.Icon_Images; break;
					case Page.Videos: btn.Image = ProjectImages.Icon_Videos; break;
					case Page.Similar: btn.Image = ProjectImages.Icon_Similar; break;
					case Page.VidFiles: btn.Image = ProjectImages.Icon_VideoFile; break;
				}

				btn.Selected = item == currentPage;
				btn.MouseClick += btnPage_MouseClick;
				PageButtonsPanel.Controls.Add(btn);

				return btn;
			}).ToArray();

			MainPanel.Controls.Add(TaglineControl = new SlickImageBackgroundControl());
			MainPanel.Controls.Add(OverviewLabel = new ImageLabelText("Overview"));
			MainPanel.Controls.Add(LanguagePanel = new SlickImageBackgroundTableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Margin = new Padding(0, 0, 0, 10) });
			MainPanel.Controls.Add(TagsPanel = new ImageTagsPanel("Tags", true));
			MainPanel.Controls.Add(KeywordsPanel = new ImageTagsPanel("Keywords", false));
			MainPanel.Controls.Add(FeaturesLabel = new ImageLabelText("Featuring") { Text = $"{(char)0x200B}" });
			MainPanel.Controls.Add(FeaturesPanel = new SlickImageBackgroundFlowLayoutPanel
			{
				Dock = DockStyle.Top,
				AutoSize = true
			});
			MainPanel.Controls.Add(DirectorLabel = new ImageLabelText("Directed By") { Text = $"{(char)0x200B}" });
			MainPanel.Controls.Add(DirectorPanel = new SlickImageBackgroundFlowLayoutPanel
			{
				Dock = DockStyle.Top,
				AutoSize = true
			});

			_SidePanel.Controls.Add(SocialLinksControl = new ImageSocialLinksControl());
			SidePanel.Controls.Add(StatusLabel = new ImageLabelText("Status"));
			SidePanel.Controls.Add(ReleaseDateLabel = new ImageLabelText("Release Date"));
			SidePanel.Controls.Add(NetworksControl = new ImageNetworkIconsControl());
			SidePanel.Controls.Add(DateAddedLabel = new ImageLabelText("Date Added"));

			LanguagePanel.RowStyles.Add(new RowStyle());
			LanguagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
			LanguagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
			LanguagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));

			LanguagePanel.Add(LanguageLabel = new ImageLabelText("Language"), 0, 0);
			LanguagePanel.Add(CountryLabel = new ImageLabelText("Country"), 1, 0);
			LanguagePanel.Add(OriginalTitleLabel = new ImageLabelText("Original Title"), 2, 0);

			TaglineControl.Dock = DockStyle.Top;
			TaglineControl.CalculateAutoSize += taglineControl_CalculateAutoSize;
			TaglineControl.Paint += taglineControl_Paint;
		}

		private void taglineControl_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawFancyText(ContentInfo.Tagline, UI.Font("Candara", 12F, FontStyle.Italic | FontStyle.Bold), TaglineControl.DrawBounds, new StringFormat { Alignment = StringAlignment.Center });
		}

		private void taglineControl_CalculateAutoSize(object sender, PaintEventArgs e)
		{
			TaglineControl.Visible = !string.IsNullOrWhiteSpace(ContentInfo.Tagline);
			TaglineControl.Height = 5 + (int)e.Graphics.Measure(ContentInfo.Tagline, UI.Font("Candara", 12F, FontStyle.Italic | FontStyle.Bold), TaglineControl.Width).Height;
		}

		private void actionButton_PrePaint(object sender, PaintEventArgs e)
		{
			ActionButton.Image = MovieManager.Movie(ContentInfo.Id) == null
					? ProjectImages.Icon_Plus : ContentInfo.Playable
					? ProjectImages.Icon_PlaySlick : ContentInfo.AirState == AirStateEnum.Aired
					? ProjectImages.Icon_Download
					: null;

			ActionButton.Visible = ActionButton.Image != null;
		}

		private void btnPage_MouseClick(object sender, MouseEventArgs e)
		{
			CurrentPage = (Page)(sender as ImageSideButton).PageId;
		}
	}
}
