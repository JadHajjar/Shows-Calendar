using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using TMDbLib.Objects.General;

using ProjectImages = ShowsCalendar.Properties.Resources;
using YouTubeVideo = YoutubeExplode.Models.Video;

namespace ShowsCalendar
{
	public partial class YoutubeControl : SlickAdvancedImageControl
	{
		public string BannerName { get; set; }
		public YouTubeVideo Video { get; private set; }
		public string Id { get; }
		public string Title { get; }
		public bool Error { get; private set; }

		public Movie Movie { get; }
		public Episode Episode { get; }
		public Season Season { get; }
		public TvShow TvShow { get; }

		public YoutubeControl(Movie movie, Video video, string banner = null) : this(video, banner) => Movie = movie;

		public YoutubeControl(TvShow show, Video video, string banner = null) : this(video, banner) => TvShow = show;

		public YoutubeControl(Season season, Video video, string banner = null) : this(video, banner) => Season = season;

		public YoutubeControl(Episode episode, Video video, string banner = null) : this(video, banner) => Episode = episode;

		private YoutubeControl(Video video, string banner = null)
		{
			InitializeComponent();
			Id = video.Key;
			BannerName = banner ?? video.Type;
			Title = video.Name;
			SlickTip.SetTo(this, Title);

			new BackgroundAction(async () =>
			{
				try
				{
					LoadImage($"https://img.youtube.com/vi/{Id}/hqdefault.jpg");
					Video = await Data.YoutubeClient.GetVideoAsync(video.Key);
					Image = null;
				}
				catch { Error = true; }
			}).Run();
		}

		public YoutubeControl(YouTubeVideo video, string banner = null)
		{
			InitializeComponent();
			Id = video?.Id;
			Title = video?.Title;
			SlickTip.SetTo(this, Title);
			BannerName = banner;
			Video = video;
			Image = null;
			Enabled = false;
			LoadImage(Video?.Thumbnails.HighResUrl);
		}

		private void InitializeComponent()
		{
			Cursor = Cursors.Hand;
			Margin = new Padding(10);
			Loading = true;
			EnableDots = true;
			ImageSizeMode = ImageSizeMode.CenterScaled;
			DefaultImage = ProjectImages.Big_Youtube;
		}

		protected override void UIChanged()
		{
			Size = UI.Scale(new Size(300, 230), UI.FontScale);
			ImageBounds = new Rectangle(1, 0, Width - 2, (Width + 2) * 9 / 16);
		}

		protected override void OnDotsMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				SlickToolStrip.Show(Data.Mainform,
						new SlickStripItem("Play", play, ProjectImages.Tiny_Play),
						new SlickStripItem("View on Youtube", () =>
						{
							Cursor.Current = Cursors.WaitCursor;
							try { System.Diagnostics.Process.Start($"https://www.youtube.com/watch?v={Id}"); }
							catch { Cursor.Current = Cursors.Default; MessagePrompt.Show("Could not open the link because you do not have a default browser selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
							Cursor.Current = Cursors.Default;
						}, ProjectImages.Tiny_Youtube)
					);
			}
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
				play();
			else
				base.OnMouseClick(e);
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				play();
			else if (e.Button == MouseButtons.Right)
			{
				SlickToolStrip.Show(Data.Mainform, PointToScreen(e.Location),
					new SlickStripItem("Play", play, ProjectImages.Tiny_Play),
					new SlickStripItem("View on Youtube", () =>
					{
						Cursor.Current = Cursors.WaitCursor;
						try { System.Diagnostics.Process.Start($"https://www.youtube.com/watch?v={Id}"); }
						catch { Cursor.Current = Cursors.Default; MessagePrompt.Show("Could not open the link because you do not have a default browser selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
						Cursor.Current = Cursors.Default;
					}, ProjectImages.Tiny_Youtube)
				);
			}
		}

		private void play()
		{
			var vid = Video ?? new YouTubeVideo(Id, string.Empty, DateTimeOffset.UtcNow, Title, string.Empty, new YoutubeExplode.Models.ThumbnailSet(Id), TimeSpan.Zero, new List<string>(), new YoutubeExplode.Models.Statistics(0, 0, 0));
			Play(vid, Movie, TvShow, Season, Episode);
		}

		public static void Play(string vid, string title = "", Movie movie = null, TvShow tvShow = null, Season season = null, Episode episode = null)
			=> Play(new YouTubeVideo(vid, string.Empty, DateTimeOffset.UtcNow, title, string.Empty, new YoutubeExplode.Models.ThumbnailSet(vid), TimeSpan.Zero, new List<string>(), new YoutubeExplode.Models.Statistics(0, 0, 0)), movie, tvShow, season, episode);

		public static void Play(YouTubeVideo vid, Movie movie = null, TvShow tvShow = null, Season season = null, Episode episode = null)
		{
			Cursor.Current = Cursors.WaitCursor;

			try
			{ System.Diagnostics.Process.Start($"https://www.youtube.com/watch?v={vid.Id}"); }
			catch { Cursor.Current = Cursors.Default; MessagePrompt.Show("Could not open the link because you do not have a default browser selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }

			Cursor.Current = Cursors.Default;
		}

		protected override IEnumerable<Bitmap> HoverIcons { get; } = new[] { ProjectImages.Huge_Play };
		protected override IEnumerable<Banner> Banners { get; } = new[] { new Banner(string.Empty, BannerStyle.Text, ProjectImages.Tiny_Youtube) };

		protected override void OnDraw(PaintEventArgs e)
		{
			DrawTextOnImage(e, "YOUTUBE", true);

			DrawTextOnImage(e, $"PLAY {BannerName.ToUpper()}", false);

			DrawText(e, Title, UI.Font(9.75F, FontStyle.Bold), FormDesign.Design.ForeColor, rigthPad: 21);

			DrawText(e, BannerName, UI.Font(8.25F), FormDesign.Design.LabelColor);
		}
	}
}