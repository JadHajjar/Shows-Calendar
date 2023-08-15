using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
	public partial class VideoFileControl<T> : SlickAdvancedImageControl where T : IPlayableContent, IContent
	{
		public T Content { get; private set; }
		private readonly FileInfo VidFile;
		private Bitmap quality;

		private string[] infoStrings;

		public bool PictureLoaded = false;

		public VideoFileControl(FileInfo fileInfo, T content)
		{
			InitializeComponent();
			VidFile = fileInfo;
			Content = content;
			EnableDots = true;
			DefaultImage = ProjectImages.Huge_Play;
			this.GetImage(content.PosterPath, 82, false);

			var vidprops = 0;
			var audprops = (NReco.VideoInfo.MediaInfo.StreamInfo)null;
			var subs = Array.Empty<NReco.VideoInfo.MediaInfo.StreamInfo>();
			var dur = "File Read Failed";

			SlickTip.SetTo(this, fileInfo.FileName());

			infoStrings = new[]
			{
				fileInfo.FileName(),
				string.Empty,
				fileInfo.Length.SizeString(),
				"Loading Info..",
				$"Created on {fileInfo.CreationTime.ToReadableString(true, ExtensionClass.DateFormat.TDMY)}",
				fileInfo.AbreviatedPath(true).Replace("\\..\\", "\\ ..\\"),
				audprops?.CodecLongName ?? string.Empty,
				subs.Any() ? $"{subs.Length} Subtitle".Plural(subs) : string.Empty
			};

			new BackgroundAction(() =>
			{
				try
				{
					var fileprops = new NReco.VideoInfo.FFProbe().GetMediaInfo(fileInfo.FullName);
					vidprops = fileprops.Streams.FirstOrDefault(x => x.CodecType == "video")?.Height ?? 0;
					audprops = fileprops.Streams.FirstOrDefault(x => x.CodecType == "audio");
					subs = fileprops.Streams.Where(x => x.CodecType == "subtitle").ToArray();
					dur = fileprops.Duration.ToReadableString();
				}
				catch { }

				var vidQ = "Low";

				if (vidprops == 0)
				{ vidQ = string.Empty; }
				else if (vidprops > 2250)
				{ vidQ = "8K"; quality = ProjectImages.Tiny_4K; }
				else if (vidprops > 1700)
				{ vidQ = "4K UHD"; quality = ProjectImages.Tiny_4K; }
				else if (vidprops > 775)
				{ vidQ = "1080p HD"; quality = ProjectImages.Tiny_1080; }
				else if (vidprops > 550)
				{ vidQ = "720p HQ"; quality = ProjectImages.Tiny_720; }
				else
					quality = ProjectImages.Tiny_SD;

				infoStrings = new[]
				{
					fileInfo.FileName(),
					vidQ,
					fileInfo.Length.SizeString(),
					dur,
					$"Created on {fileInfo.CreationTime.ToReadableString(true, ExtensionClass.DateFormat.TDMY)}",
					fileInfo.AbreviatedPath(true).Replace("\\..\\", "\\ ..\\"),
					audprops?.CodecLongName ?? string.Empty,
					subs.Any() ? $"{subs.Length} Subtitle".Plural(subs) : string.Empty
				};

				this.TryInvoke(Invalidate);
			}).Run();
		}

		protected override void UIChanged()
		{
			Size = UI.Scale(new Size(350, 135), UI.FontScale);
			ImageBounds = new Rectangle(new Point(1, 1), UI.Scale(new Size(88, 133), UI.FontScale));
		}

		protected override void OnDotsMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				SlickToolStrip.Show(Data.Mainform,
					new SlickStripItem("Play", () => Content.Play(VidFile), ProjectImages.Tiny_Play),
					new SlickStripItem("View In Explorer", () => Process.Start("explorer.exe", $"/select, \"{VidFile.FullName}\""), ProjectImages.Tiny_Folder),
					new SlickStripItem("Delete", deleteFile, ProjectImages.Tiny_Trash));
		}

		protected override void OnImageMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				Content.Play(VidFile);
			else
				SlickToolStrip.Show(Data.Mainform, PointToScreen(e.Location),
					new SlickStripItem("Play", () => Content.Play(VidFile), ProjectImages.Tiny_Play),
					new SlickStripItem("View In Explorer", () => Process.Start("explorer.exe", $"/select, \"{VidFile.FullName}\""), ProjectImages.Tiny_Folder),
					new SlickStripItem("Delete", deleteFile, ProjectImages.Tiny_Trash));
		}

		private void deleteFile()
		{
			if (MessagePrompt.Show("Are you sure you want to delete this file?", PromptButtons.YesNo, PromptIcons.Warning, Data.Mainform)
								== DialogResult.Yes)
			{
				new BackgroundAction(() => FileOperationAPIWrapper.MoveToRecycleBin(VidFile.FullName)).Run();
				Dispose();
			}
		}

		protected override IEnumerable<Bitmap> HoverIcons { get; } = new[] { ProjectImages.Icon_PlaySlick };

		protected override IEnumerable<Banner> Banners
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(infoStrings[1]))
					yield return new Banner(infoStrings[1], BannerStyle.Active, quality);

				if (!string.IsNullOrWhiteSpace(infoStrings[7]))
					yield return new Banner(infoStrings[7], BannerStyle.Green, ProjectImages.Tiny_CC);
			}
		}

		protected override void OnDraw(PaintEventArgs e)
		{
			if (!File.Exists(VidFile.FullName)) { BeginInvoke(new Action(Dispose)); return; }

			DrawTextOnImage(e, "PLAY THIS", false);

			DrawText(e, infoStrings[0], UI.Font(9.75F, FontStyle.Bold), FormDesign.Design.ForeColor, rigthPad: 40);

			DrawText(e, infoStrings[2], UI.Font(8.25F), FormDesign.Design.LabelColor);

			DrawText(e, infoStrings[3], UI.Font(6.75F), FormDesign.Design.InfoColor);

			DrawText(e, infoStrings[6], UI.Font(6.75F), FormDesign.Design.InfoColor);

			DrawText(e, infoStrings[4], UI.Font(6.75F), FormDesign.Design.InfoColor);

			DrawText(e, infoStrings[5], UI.Font(6.75F), FormDesign.Design.InfoColor, bottom: true);
		}
	}
}