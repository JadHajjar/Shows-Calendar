using Extensions;

using Microsoft.Win32;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using TMDbLib.Objects.Movies;

namespace ShowsCalendar
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			if (args.TryGet(0) == "/shortcut")
			{
				if (bool.TryParse(args.TryGet(1), out var add) && GeneralMethods.IsAdministrator)
				{
					const string path = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk";

					if (add)
						GeneralMethods.CreateShortcut(path, Application.ExecutablePath, "/startup");
					else if (File.Exists(path))
						File.Delete(path);
				}

				return;
			}

			ISave.AppName = "Shows Calendar";

			ISave.CustomSaveDirectory = ISave.LoadRaw("CustomSaveDirectory.tf") as string;

			Data.FirstTimeSetup = !Directory.Exists(ISave.DocsFolder);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (Environment.OSVersion.Version.Major == 6)
				SetProcessDPIAware();

#if !DEBUG
			try
			{
				if (args.TryGet(0) == "/uninstallprompt")
				{
					RunUninstaller();
					return;
				}

				if (!SingleInstance.CheckSingleInstance(args.Length > 0 ? args[0] : string.Empty))
					return;
#endif

				if (args.Length == 0)
				{
					Data.PreloaderForm = new PreloaderForm();
					Data.PreloaderForm.Show();

					Application.DoEvents();
				}

				try { Directory.CreateDirectory(ISave.DocsFolder); }
				catch
				{
					MessagePrompt.Show("Could not access your Documents folder to create a folder for Shows Calendar.\r\n\r\nPlease check your security settings.", "Error", PromptButtons.OK, PromptIcons.Error);
					return;
				}

				UpdateHandler.Start();
			ConnectionHandler.Start();
			SlickCursors.Initialize();

			Data.TMDbHandler = new TMDbHandler();

			Data.Preferences = ISave.Load<Preferences>("Preferences.tf");
			Data.Options = ISave.Load<Options>("Options.tf");
			AnimationHandler.NoAnimations = Data.Options.NoAnimations;
			Notification.PlaySounds = Data.Options.NotificationSound;
			SlickAdvancedImageControl.AlwaysShowBanners = Data.Options.AlwaysShowBanners;

			if (!Data.FirstTimeSetup)
			{
				Directory.CreateDirectory(Path.Combine(ISave.DocsFolder, "Shows"));
				Directory.CreateDirectory(Path.Combine(ISave.DocsFolder, "Movies"));
				Directory.CreateDirectory(Path.Combine(ISave.DocsFolder, "Thumbs"));

				new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Shows")).Attributes |= FileAttributes.System;
				new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Movies")).Attributes |= FileAttributes.System;
				new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Thumbs")).Attributes |= FileAttributes.System | FileAttributes.Hidden;

				IO.Handler.Initialize();
				ShowManager.LoadAllShows();
				MovieManager.LoadAllMovies();

				new BackgroundAction(() =>
				{
					IO.Handler.LoadFolders(false);
					LocalShowHandler.Load();
					LocalMovieHandler.Load();

					IO.Handler.FirstLoadFinished = true;

					if (Data.Mainform?.PI_Library != null)
					{
						ShowManager.RunReminder();
						MovieManager.RunReminder();
					}
				}).Run();

				//if (!Debugger.IsAttached)
					//ConnectionHandler.WhenConnected(() => new BackgroundAction(async () =>
					//{
					//	await Task.Delay(5000);

					//	Parallelism.ForEach(ShowManager.Shows.OrderByDescending(x => x.Episodes.LastOrDefault()?.AirDate ?? DateTime.MinValue).ToList(), async show =>
					//	{
					//		await show.startRefresh();
					//	}, 3);

					//	Parallelism.ForEach(MovieManager.Movies.OrderByDescending(x => x.ReleaseDate ?? DateTime.MinValue).ToList(), async movie =>
					//	{
					//		await movie.startRefresh();
					//	}, 3);
					//}).Run());
			}

#if !DEBUG
				if (GeneralMethods.IsAdministrator)
				{
					foreach (var ext in SlickControls.IO.VideoExtensions)
					{
						try
						{
							var imgKey = Registry.ClassesRoot.OpenSubKey(ext);

							if (imgKey == null) continue;

							var imgType = imgKey.GetValue("");
							var myExecutable = Assembly.GetEntryAssembly().Location;
							var command = $"\"{myExecutable}\" \"%1\"";
							var keyName = $@"{imgType}\shell\Open\command";
							using (var key = Registry.ClassesRoot.CreateSubKey(keyName))
								key.SetValue("", command);
						}
						catch { }
					}

					CreateUninstaller();
				}
#endif

			Application.Run(Data.Mainform = new Dashboard(args));
#if !DEBUG
            }
            catch (Exception ex)
            {
				if (!(ex is System.Threading.AbandonedMutexException))
					ExceptionHandler(ex);
            }
#endif
		}

		private static void RunUninstaller()
		{
		}

		private static void CreateUninstaller()
		{
			using (var parent = Registry.LocalMachine.OpenSubKey(
						 @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true))
			{
				if (parent == null)
				{
					throw new Exception("Uninstall registry key not found.");
				}
				try
				{
					RegistryKey key = null;

					try
					{
						var guidText = SingleInstance.GUID.ToString("B");
						key = parent.OpenSubKey(guidText, true) ??
							  parent.CreateSubKey(guidText);

						if (key == null)
						{
							throw new Exception(string.Format("Unable to create uninstaller '{0}'", guidText));
						}

						var asm = Assembly.GetExecutingAssembly();
						var v = asm.GetName().Version;
						var exe = "\"" + asm.CodeBase.Substring(8).Replace("/", "\\\\") + "\"";

						key.SetValue("DisplayName", "Shows Calendar");
						key.SetValue("ApplicationVersion", v.ToString());
						key.SetValue("Publisher", "Slick Apps");
						key.SetValue("DisplayIcon", exe);
						key.SetValue("DisplayVersion", v.ToString(2));
						key.SetValue("URLInfoAbout", "");
						key.SetValue("Contact", "jad.g.hajjar@hotmail.com");
						key.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
						key.SetValue("UninstallString", exe + " /uninstallprompt");
					}
					finally
					{
						if (key != null)
						{
							key.Close();
						}
					}
				}
				catch (Exception ex)
				{
					throw new Exception(
						"An error occurred writing uninstall information to the registry.  The service is fully installed but can only be uninstalled manually through the command line.",
						ex);
				}
			}
		}

		private static void ExceptionHandler(Exception ex)
		{
			if (ex.Message == "Cannot access a disposed object.\r\nObject name: 'Dashboard'.")
				return;

			var send = DialogResult.Yes == MessagePrompt.Show("Shows Calendar ran into a fatal error.\n\nWould you like to send the error report to the App Owner?", "Fatal Error", PromptButtons.YesNo, PromptIcons.Error);
			var restart = DialogResult.Yes == MessagePrompt.Show("Would you like to Restart the App?", "Restart App?", PromptButtons.YesNo, PromptIcons.Question);

			if (send && ConnectionHandler.State != ConnectionState.Disconnected)
			{
				try
				{
					using (var client = new SmtpClient("smtp.live.com")
					{
						UseDefaultCredentials = false,
						Credentials = new NetworkCredential("ShowsCalendar@hotmail.com", "iPhone0X", "hotmail.com"),
						Port = 587,
						EnableSsl = true,
						DeliveryMethod = SmtpDeliveryMethod.Network
					})
					{
						var mailMessage = new MailMessage
						{
							From = new MailAddress("ShowsCalendar@hotmail.com"),
							IsBodyHtml = true,
							Body = GetBody(ex),
							Subject = "Error Report",
						};
						mailMessage.To.Add("dotca@hotmail.co.uk");
						client.Send(mailMessage);
					}
				}
				catch
				{
					MessagePrompt.Show("There was an error sending the message to the App Owner.\n\nThe Error was copied to your clipboard", "Message Failed", PromptButtons.OK, PromptIcons.Error);
					Clipboard.SetText(ex.ToString());
				}
			}
			else if (send)
			{
				MessagePrompt.Show("You aren't connected to the Internet right now.\n\nThe Error was copied to your clipboard", "Message Failed", PromptButtons.OK, PromptIcons.Error);
				Clipboard.SetText(ex.ToString());
			}

			if (restart)
				Application.Restart();
		}

		private static string GetBody(Exception ex)
		{
			var json = Regex.Matches(Newtonsoft.Json.JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented), "\"(.+?)\": \"?(.+)")
				.Cast<Match>().ConvertDictionary(x => new KeyValuePair<string, string>(x.Groups[1].Value.FormatWords(), x.Groups[2].Value.TrimEnd('"', ',', '\r').RegexReplace("(\\\\r)?\\\\n", "<br>")));

			if (json.ContainsKey("Stack Trace String"))
				json.Remove("Stack Trace String");

			if (json.ContainsKey("Watson Buckets"))
				json.Remove("Watson Buckets");

			return
				$"<h3>PC Info</h3>" +
				$"<table style=\"text-align: left; padding-left: 10pt;\">" +
				$"	<tbody>" +
				$"		<tr>" +
				$"			<th style=\"text-align: left;\">User:</th>" +
				$"			<td>{SystemInformation.UserName}</td>" +
				$"		</tr>" +
				$"		<tr>" +
				$"			<th style=\"text-align: left;\">Domain:</th>" +
				$"			<td>{SystemInformation.UserDomainName}</td>" +
				$"		</tr>" +
				$"		<tr>" +
				$"			<th style=\"text-align: left;\">OS:</th>" +
				$"			<td>{GetOSFriendlyName()}</td>" +
				$"		</tr>" +
				$"		<tr>" +
				$"			<th style=\"text-align: left;\">PC Name:</th>" +
				$"			<td>{SystemInformation.ComputerName}</td>" +
				$"		</tr>" +
				$"	</tbody>" +
				$"</table>" +
				$"<hr />" +
				$"<h3>Exception Trace</h3>" +
				$"<p>{ex.ToString().Replace("was thrown.", "was thrown.<br>").Replace("  at", "&emsp;&emsp;at").Replace("\r\n", "<br />").RegexRemove("(?<= in ).+solutions\\\\")}</p>" +
				$"<hr />" +
				$"<h3>Exception Info</h3>" +
				$"<table style=\"text-align: left; padding-left: 10pt;\">" +
				$"	<tbody>" +
					json.Where(x => x.Value != "null").ListStrings(x =>
						$"		<tr>" +
						$"			<th style=\"text-align: left;\">{x.Key}:</th>" +
						$"			<td>{x.Value}</td>" +
						$"		</tr>") +
				$"	</tbody>" +
				$"</table>";
		}

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern bool SetProcessDPIAware();

		public static string GetOSFriendlyName()
		{
			using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
			{
				foreach (ManagementObject os in searcher.Get())
					return $"{os["Caption"]} v{os["Version"]}";
			}

			return "Unknown";
		}
	}
}