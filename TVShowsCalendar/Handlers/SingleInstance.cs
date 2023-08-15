using Extensions;

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public class SingleInstance
	{
		private const int HWND_BROADCAST = 0xFFFF;

		public static readonly int WM_MY_MSG = RegisterWindowMessage("WM_MY_MSG");

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegisterWindowMessage(string message);

		public static Guid GUID = Guid.Parse("{8E2BF123-6358-F7AB-3289-18140365A644}");

		private static readonly Mutex mutex = new Mutex(true, $"{{{GUID}}}");

		public static bool CheckSingleInstance(string message)
		{
			if (!mutex.WaitOne(TimeSpan.Zero, true))
			{
				new SingleInstanceMessage
				{
					ActionType = string.IsNullOrWhiteSpace(message) || !File.Exists(message) ? ActionType.ShowUp : ActionType.OpenFile,
					File = message
				}.Save(noBackup: true);

				return false;
			}

			var watcher = new FileSystemWatcher
			{
				Path = ISave.DocsFolder,
				NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
				Filter = "SingleInstanceMessage",
				EnableRaisingEvents = true
			};

			watcher.Changed += (s, e) =>
			{
				Thread.Sleep(300);
				var inf = ISave.Load<SingleInstanceMessage>("SingleInstanceMessage");

				Data.Mainform?.TryInvoke(() =>
				{
					switch (inf.ActionType)
					{
						case ActionType.ShowUp:
							Data.Mainform.ShowUp();
							break;

						case ActionType.OpenFile:
							Data.Mainform.ShowUp();
							Data.Mainform.Play(inf.File);
							break;

						default:
							break;
					}
				});
			};

			return true;
		}

		public static bool IsShowMessage(Message m)
			=> m.Msg == WM_MY_MSG && m.WParam.ToInt32() == 0xCDCD;
	}
}