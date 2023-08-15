using Extensions;

using SlickControls;

using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class Torrent
	{
		public string Magnet { get; private set; }
		public string URL { get; }
		public string Size { get; }
		public string Sound { get; }
		public string Subs { get; }
		public string Name { get; }
		public string Quality { get; }
		public int Seeders { get; }
		public int Leechers { get; }
		public Origin Source { get; }
		public Bitmap Signal { get; }
		public TorrentSortingData SortingData { get; }
		public double Health => Math.Max(30D * Math.Log(Math.Max(1, ((Seeders - Leechers + 4) / 4D) + (Seeders / 2D)), Math.E), 0);

		public TorrentTile Tile { get; internal set; }
		public bool Loading { get; private set; }

		public enum Origin
		{
			Zooqle = 0,
			PirateBay = 1,
			X1337 = 2,
			NyaaSi = 3,
			Rarbg = 4,
			YTS = 5
		}

		public Torrent(Match match, Origin origin)
		{
			switch (Source = origin)
			{
				case Origin.Zooqle:
					var seeders = Regex.Match(match.Value, "title=\"seeders.+?(\\d+).+?(\\d+)\"", RegexOptions.IgnoreCase);

					URL = "https://zooqle.com" + Regex.Match(match.Value, "href ?=\"(.+?)\"").Groups[1].Value;
					Size = Regex.Match(match.Value, @"[\d\.,]+ ?[TGMK]B").Value;
					Sound = Regex.Match(match.Value, @"<span.+?Audio.+?</i>(.+?)</span>").Groups[1].Value;
					Subs = Regex.Match(match.Value, @"<span.+?language.+?>(.+?)</span>").Groups[1].Value.ToUpper().RegexReplace(",", ", ");
					Name = Regex.Match(match.Value, @"<a.+?>(.+?)</a>").Groups[1].Value.RegexRemove("</?hl>");
					Magnet = Regex.Match(match.Value, "<a title=\"Magnet link\" (?:rel=\"nofollow\" )?href=\"(.+?)\"").Groups[1].Value;
					Seeders = seeders.Groups[1].Value.SmartParse();
					Leechers = seeders.Groups[2].Value.SmartParse();

					switch (Regex.Match(match.Value, "<span class=\"text-nowrap smallest trans90 text-muted hidden-md hidden-xs\">.+? (\\w+?)</span>").Groups[1].Value)
					{
						case "Std": Quality = "Low"; break;
						case "Med": Quality = "Low"; break;
						case "Ultra": Quality = "4K Ultra"; break;
						default: Quality = Regex.Match(match.Value, "<span class=\"text-nowrap smallest trans90 text-muted hidden-md hidden-xs\">.+? (\\w+?)</span>").Groups[1].Value; break;
					}
					break;

				case Origin.PirateBay:
					var size = Regex.Match(match.Value, @"Size ([\d\.,]+)(?:&nbsp;)?([TGMK])iB");
					var sizeAmt = size.Groups[1].Value.SmartParseD();

					URL = "https://pirateproxy.page/" + match.Groups[1].Value;
					Size = $"{sizeAmt.ToString(sizeAmt >= 100 ? "0" : "0.#")} {size.Groups[2].Value}B";
					Subs = string.Empty;
					Name = match.Groups[2].Value;
					Magnet = match.Groups[3].Value;
					Seeders = match.Groups[4].Value.SmartParse();
					Leechers = match.Groups[5].Value.SmartParse();
					break;

				case Origin.X1337:
					URL = "https://www.1377x.to" + match.Groups[1].Value;
					Size = match.Groups[5].Value;
					Subs = string.Empty;
					Name = match.Groups[2].Value;
					Magnet = string.Empty;
					Seeders = match.Groups[3].Value.SmartParse();
					Leechers = match.Groups[4].Value.SmartParse();
					break;

				case Origin.NyaaSi:
					var cols = Regex.Matches(match.Value, "<td.+?>(.+?)</td>");
					URL = "https://nyaa.si" + Regex.Match(cols[1].Groups[1].Value, "href=\"(/view/\\d+)").Groups[1].Value;
					Size = cols[3].Groups[1].Value.RegexReplace("([TKMG])iB", x => $"{x.Groups[1].Value}B");
					Subs = string.Empty;
					Name = Regex.Match(cols[1].Groups[1].Value, ".+\">(.+)</a>").Groups[1].Value;
					Magnet = Regex.Match(cols[2].Groups[1].Value, "<a href=\"(magnet:?.+?)\">").Groups[1].Value;
					Seeders = cols[5].Groups[1].Value.SmartParse();
					Leechers = cols[6].Groups[1].Value.SmartParse();
					break;

				case Origin.Rarbg:
					URL = "https://www.rarbggo.to/" + match.Groups[1].Value;
					Size = match.Groups[3].Value;
					Subs = string.Empty;
					Name = match.Groups[2].Value;
					Magnet = string.Empty;
					Seeders = Regex.Match(match.Groups[4].Value, ">(.+?)<").Value.SmartParse();
					Leechers = match.Groups[5].Value.SmartParse();
					break;

				case Origin.YTS:
					URL = "https://eztv.re/" + match.Groups[1].Value;
					Size = match.Groups[4].Value.RegexRemove("<.+?>").Trim();
					Subs = string.Empty;
					Name = match.Groups[2].Value;
					Magnet = match.Groups[3].Value;
					Seeders = match.Groups[5].Value.RegexRemove("<.+?>").SmartParse();
					Leechers = Seeders / 3;
					break;
			}

			if (!Magnet.StartsWith("magnet"))
				Magnet = string.Empty;

			if (string.IsNullOrWhiteSpace(Quality))
				Quality = FindQuality(match.Value);

			if (string.IsNullOrWhiteSpace(Sound))
				Sound = FindSound();

			if (Health < 25)
				Signal = Properties.Resources.Tiny_Signal_0;
			else if (Health < 50)
				Signal = Properties.Resources.Tiny_Signal_1;
			else if (Health < 75)
				Signal = Properties.Resources.Tiny_Signal_2;
			else
				Signal = Properties.Resources.Tiny_Signal_3;

			Name = Name.Replace('.', ' ').RemoveDoubleSpaces();

			SortingData = new TorrentSortingData
			{
				Name = Name,
				Subs = Subs,
				Sound = (int)(Sound.SmartParseD() * 10),
				Health = Health
			};

			switch (Quality)
			{
				case "3D": SortingData.Quality = QualityFilter.D3; break;
				case "4K Ultra": SortingData.Quality = QualityFilter.K4; break;
				case "Ultra": SortingData.Quality = QualityFilter.K4; break;
				case "1080p": SortingData.Quality = QualityFilter.p1080; break;
				case "720p": SortingData.Quality = QualityFilter.p720; break;
				default: SortingData.Quality = QualityFilter.Low; break;
			}

			var s = Size.SmartParseD();
			switch (Regex.Match(Size, "[TKMG]B").Value)
			{
				case "TB": SortingData.Size = (int)(s * Math.Pow(1024, 3)); break;
				case "GB": SortingData.Size = (int)(s * Math.Pow(1024, 2)); break;
				case "MB": SortingData.Size = (int)(s * Math.Pow(1024, 1)); break;
				case "KB": SortingData.Size = (int)(s * Math.Pow(1024, 0)); break;
				default: SortingData.Size = 0; break;
			}

			Size = $"{s:#,###} {Regex.Match(Size, "[TKMG]B").Value}";
		}

		private string FindSound()
		{
			var match = Regex.Match(Name, "(\\d+)ch", RegexOptions.IgnoreCase);
			if (match.Success)
			{
				if (match.Groups[1].Value == "2")
					return "2";
				return $"{match.Groups[1].Value.SmartParse() - 1}.1";
			}

			match = Regex.Match(Name, @"[^\d](\d\.\d)[^\d(GB)(MB)]", RegexOptions.IgnoreCase);
			if (match.Success)
				return match.Groups[1].Value;

			match = Regex.Match(Name, @"\s(\d\s\d)\s", RegexOptions.IgnoreCase);
			if (match.Success)
				return match.Groups[1].Value.Replace(' ', '.');

			match = Regex.Match(Name, @"ddp(\d\s\d)", RegexOptions.IgnoreCase);
			if (match.Success)
				return match.Groups[1].Value.Replace(' ', '.');

			return string.Empty;
		}

		private string FindQuality(string match)
		{
			if (match.ContainsWord("3D"))
				return "3D";
			if (match.ContainsWord("4K") || match.ContainsWord("UHD"))
				return "4K Ultra";
			if (match.Contains("1080p"))
				return "1080p";
			if (match.Contains("720p"))
				return "720p";
			return "Low";
		}

		public void Open()
		{
			Tile.Cursor = Cursors.WaitCursor;
			try { System.Diagnostics.Process.Start(URL); }
			catch
			{
				error("Could not open link because you do not have a default Web Browser Selected");
			}
			Tile.Invalidate();
		}

		public void Download()
		{
			Tile.Loading = Loading = true;
			new BackgroundAction(async () =>
			{
				try
				{
					if (string.IsNullOrEmpty(Magnet))
					{
						var html = await BrowserHandler.GetHTML(URL);
						Magnet = Regex.Match(html, "text-nowrap.{1,10}href=\"(magnet:\\?xt=urn:btih:.+?)\"", RegexOptions.IgnoreCase).Groups[1].Value;
						if (string.IsNullOrWhiteSpace(Magnet))
							Magnet = Regex.Match(html, "href=\"(magnet:.+?)\"", RegexOptions.IgnoreCase).Groups[1].Value;
					}

					System.Diagnostics.Process.Start(Magnet.IfEmpty(URL));
				}
				catch
				{
					if (string.IsNullOrWhiteSpace(Magnet))
						error("Could not open link because you do not have a default Web Browser Selected");
					else
						error("Something went wrong while trying to open the magnet link.\n\nCheck that you have a default browser set up and that you have an application to download torrents with.");
				}
				finally
				{
					Loading = false;
					Tile.Loading = Tile.Items.Any(x => x.Loading);
				}
			}).Run();
		}

		private void error(string text)
			=> Notification.Create("Error", text, PromptIcons.Error, null)
				.Show(Data.Mainform, 7);
	}
}