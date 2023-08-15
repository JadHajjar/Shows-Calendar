using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace ShowsCalendar
{
	public static class ImageHandler
	{
		internal static readonly Dictionary<bool, int[]> AvailableSizes = new Dictionary<bool, int[]> { { false, new[] { 0, 200, 300, 400, 500, 780, 1280 } }, { true, new[] { 0, 15, 30, 50, 60, 100 } } };
		private static readonly Dictionary<string, object> lockObjects = new Dictionary<string, object>();

		private static object lockObj(string path)
		{
			lock (lockObjects)
			{
				if (!lockObjects.ContainsKey(path))
					lockObjects.Add(path, new object());

				return lockObjects[path];
			}
		}

		public static FileInfo File(string path, int size, bool height = false, int? blur = 0)
			=> new FileInfo(Path.Combine(ISave.DocsFolder, "Thumbs", $"{(blur == 0 ? "" : "b")}{height.If("h")}{size}", path.TrimStart('/')));

		public static void GetImage(this SlickPictureBox pb, string path, int size, bool errorImage = true, bool height = false, int? blur = 0)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				fail(new Exception("Invalid Path"));
				return;
			}

			pb.Loading = true;

			if (size > 0)
				size = AvailableSizes[height].Where(x => x >= size * UI.FontScale * 1.15).FirstOrDefault();

			var filepath = Path.Combine(ISave.DocsFolder, "Thumbs", $"{(blur == 0 ? "" : "b")}{height.If("h")}{size}", path.TrimStart('/'));

			new BackgroundAction(() =>
			{
				lock (lockObj($"{(blur == 0 ? "" : "b")}{height.If("h")}{size}{path}"))
				{
					var imgPath = new FileInfo(filepath);

					if (imgPath.Exists)
					{
						try
						{
							pb.Image = Image.FromFile(filepath);

							pb.OnImageLoaded();
						}
						catch (Exception ex) { fail(ex); }
					}
					else if (blur != 0)
					{
						var blurPath = Path.Combine(ISave.DocsFolder, "Thumbs", $"{height.If("h")}{size}", path.TrimStart('/'));
						var blurFile = new FileInfo(blurPath);

						if (blurFile.Exists)
						{
							try
							{
								var image = Image.FromFile(blurPath).Blur(blur);

								Directory.GetParent(filepath).Create();

								if (filepath.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase))
									image.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
								else
									image.Save(filepath);

								pb.Image = image;

								pb.OnImageLoaded();
							}
							catch (Exception ex) { fail(ex); }
						}
						else if (!ConnectionHandler.WhenConnected(loadAsync))
							fail(new Exception("Not Connected"));
					}
					else if (!ConnectionHandler.WhenConnected(loadAsync))
						fail(new Exception("Not Connected"));
				}
			}).Run();

			void loadAsync()
			{
				var tries = 1;
			start:

				if (!ConnectionHandler.IsConnected)
				{
					ConnectionHandler.WhenConnected(loadAsync);
					fail(new Exception("Not Connected"));
				}

				try
				{
					using (var webClient = new WebClient())
					{
						var imageData = webClient.DownloadData($"https://image.tmdb.org/t/p/{size.If(0, "original", height ? $"h{size}" : $"w{size}")}{path}");

						using (var ms = new MemoryStream(imageData))
						{
							var image = Image.FromStream(ms);

							if (blur != 0)
								image = image.Blur(blur);

							Directory.GetParent(filepath).Create();

							if (filepath.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase))
								image.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
							else
								image.Save(filepath);

							pb.Image = image;
							pb.OnImageLoaded();
						}
					}
				}
				catch (Exception ex)
				{
					if (ex is WebException we && we.Response is HttpWebResponse hwr && hwr.StatusCode == HttpStatusCode.BadGateway)
					{
						Thread.Sleep(1000);
						goto start;
					}
					else if (tries < 3)
					{
						tries++;
						goto start;
					}
					else fail(ex);
				}
			}

			void fail(Exception ex)
			{
				if (errorImage)
					pb.Image = Properties.Resources.Icon_ErrorImage.Color(FormDesign.Design.RedColor);
				pb.OnImageLoaded(new AsyncCompletedEventArgs(ex, true, null));
			}
		}

		public static void GetImage(this SlickImageControl pb, string path, int size, bool errorImage = true, bool height = false, int? blur = 0)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				fail(new Exception("Invalid Path"));
				return;
			}

			if (pb is BorderedImage bpb)
				bpb.ImageUrl = path;

			pb.Loading = true;

			if (size > 0)
				size = AvailableSizes[height].Where(x => x >= size * UI.FontScale * 1.15).FirstOrDefault();

			var filepath = Path.Combine(ISave.DocsFolder, "Thumbs", $"{(blur == 0 ? "" : "b")}{height.If("h")}{size}", path.TrimStart('/'));

			new BackgroundAction(() =>
			{
				lock (lockObj($"{(blur == 0 ? "" : "b")}{height.If("h")}{size}{path}"))
				{
					var imgPath = new FileInfo(filepath);

					if (imgPath.Exists)
					{
						try
						{
							pb.Image = Image.FromFile(filepath);

							pb.OnImageLoaded();
						}
						catch (Exception ex) { fail(ex); }
					}
					else if (blur != 0)
					{
						var blurPath = Path.Combine(ISave.DocsFolder, "Thumbs", $"{height.If("h")}{size}", path.TrimStart('/'));
						var blurFile = new FileInfo(blurPath);

						if (blurFile.Exists)
						{
							try
							{
								var image = Image.FromFile(blurPath).Blur(blur);

								Directory.GetParent(filepath).Create();

								if (filepath.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase))
									image.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
								else
									image.Save(filepath);

								pb.Image = image;

								pb.OnImageLoaded();
							}
							catch (Exception ex) { fail(ex); }
						}
						else if (!ConnectionHandler.WhenConnected(loadAsync))
							fail(new Exception("Not Connected"));
					}
					else if (!ConnectionHandler.WhenConnected(loadAsync))
						fail(new Exception("Not Connected"));
				}
			}).Run();

			void loadAsync()
			{
				var tries = 1;
			start:

				if (!ConnectionHandler.IsConnected)
				{
					ConnectionHandler.WhenConnected(loadAsync);
					fail(new Exception("Not Connected"));
				}

				try
				{
					using (var webClient = new WebClient())
					{
						var imageData = webClient.DownloadData($"https://image.tmdb.org/t/p/{size.If(0, "original", height ? $"h{size}" : $"w{size}")}{path}");

						using (var ms = new MemoryStream(imageData))
						{
							var image = Image.FromStream(ms);

							if (blur != 0)
								image = image.Blur(blur);

							Directory.GetParent(filepath).Create();

							if (filepath.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase))
								image.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
							else
								image.Save(filepath);

							pb.Image = image;
							pb.OnImageLoaded();
						}
					}
				}
				catch (Exception ex)
				{
					if (ex is WebException we && we.Response is HttpWebResponse hwr && hwr.StatusCode == HttpStatusCode.BadGateway)
					{
						Thread.Sleep(1000);
						goto start;
					}
					else if (tries < 3)
					{
						tries++;
						goto start;
					}
					else fail(ex);
				}
			}

			void fail(Exception ex)
			{
				if (errorImage)
					pb.Image = Properties.Resources.Icon_ErrorImage.Color(FormDesign.Design.RedColor);
				pb.OnImageLoaded(new AsyncCompletedEventArgs(ex, true, null));
			}
		}

		public static Bitmap GetImage(string path, double size, bool errorImage = true, bool height = false, int? blur = 0, Size? desiredSize = null)
		{
			if (string.IsNullOrWhiteSpace(path))
				return fail(new Exception("Invalid Path"));

			if (size > 0)
				size = AvailableSizes[height].Where(x => x >= size * UI.FontScale * 1.15).FirstOrDefault();

			var filepath = Path.Combine(ISave.DocsFolder, "Thumbs", $"{(blur == 0 ? "" : "b")}{height.If("h")}{size}", path.TrimStart('/'));

			lock (lockObj($"{(blur == 0 ? "" : "b")}{height.If("h")}{size}{path}"))
			{
				var imgPath = new FileInfo(filepath);

				if (imgPath.Exists)
				{
					try
					{
						var image = (Bitmap)Image.FromFile(filepath);

						if (desiredSize != null)
						{
							var imgSize = image.Size;
							var sizeTransform = (double)imgSize.Width / imgSize.Height > (double)desiredSize?.Width / desiredSize?.Height
								? new Size((int)(imgSize.Width * desiredSize?.Height / imgSize.Height), (int)desiredSize?.Height)
								: new Size((int)desiredSize?.Width, (int)(imgSize.Height * desiredSize?.Width / imgSize.Width));

							if (imgSize != sizeTransform)
								image = new Bitmap(image, sizeTransform);
						}

						return image;
					}
					catch (Exception ex) { return fail(ex); }
				}
				else if (blur != 0)
				{
					var blurPath = Path.Combine(ISave.DocsFolder, "Thumbs", $"{height.If("h")}{size}", path.TrimStart('/'));
					var blurFile = new FileInfo(blurPath);

					if (blurFile.Exists)
					{
						try
						{
							var image = (Bitmap)Image.FromFile(blurPath).Blur(blur);

							if (desiredSize != null)
							{
								var imgSize = image.Size;
								var sizeTransform = (double)imgSize.Width / imgSize.Height > (double)desiredSize?.Width / desiredSize?.Height
									? new Size((int)(imgSize.Width * desiredSize?.Height / imgSize.Height), (int)desiredSize?.Height)
									: new Size((int)desiredSize?.Width, (int)(imgSize.Height * desiredSize?.Width / imgSize.Width));

								if (imgSize != sizeTransform)
									image = new Bitmap(image, sizeTransform);
							}

							Directory.GetParent(filepath).Create();

							if (filepath.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase))
								image.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
							else
								image.Save(filepath);

							return image;
						}
						catch (Exception ex) { fail(ex); }
					}
				}

				var tries = 1;
			start:

				if (!ConnectionHandler.IsConnected)
					fail(new Exception("Not Connected"));

				try
				{
					using (var webClient = new WebClient())
					{
						var imageData = webClient.DownloadData($"https://image.tmdb.org/t/p/{size.If(0, "original", height ? $"h{size}" : $"w{size}")}{path}");

						using (var ms = new MemoryStream(imageData))
						{
							var image = Image.FromStream(ms);

							if (desiredSize != null)
							{
								var imgSize = image.Size;
								var sizeTransform = (double)imgSize.Width / imgSize.Height > (double)desiredSize?.Width / desiredSize?.Height
									? new Size((int)(imgSize.Width * desiredSize?.Height / imgSize.Height), (int)desiredSize?.Height)
									: new Size((int)desiredSize?.Width, (int)(imgSize.Height * desiredSize?.Width / imgSize.Width));

								if (imgSize != sizeTransform)
									image = new Bitmap(image, sizeTransform);
							}

							if (blur != 0)
								image = image.Blur(blur);

							Directory.GetParent(filepath).Create();

							if (filepath.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase))
								image.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
							else
								image.Save(filepath);

							return (Bitmap)image;
						}
					}
				}
				catch (Exception ex)
				{
					if (ex is WebException we && we.Response is HttpWebResponse hwr && hwr.StatusCode == HttpStatusCode.BadGateway)
					{
						Thread.Sleep(1000);
						goto start;
					}
					else if (tries < 3)
					{
						tries++;
						goto start;
					}
					else
						return fail(ex);
				}
			}

			Bitmap fail(Exception ex)
			{
				if (errorImage)
					return Properties.Resources.Icon_ErrorImage.Color(FormDesign.Design.RedColor);
				return null;
			}
		}
	}
}