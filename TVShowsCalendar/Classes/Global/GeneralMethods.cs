using Extensions;

using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace ShowsCalendar
{
	public static class GeneralMethods
	{
		public static Dictionary<int, string> QualityDicId2Txt = new Dictionary<int, string>()
		{
			{ 11  , "3D"      },
			{ 9   , "4K Ultra"},
			{ 7   , "1080p"   },
			{ 5   , "720p"    },
			{ 3   , "Low"     },
			{ 2   , "Low"     },
			{ 1   , "Low"     },
		};

		public static Dictionary<string, int> QualityDicTxt2Id = new Dictionary<string, int>()
		{
			{ "3D"   ,  11    },
			{ "Ultra",  9     },
			{ "1080p",  7     },
			{ "720p" ,  5     },
			{ "Low"  ,  1     },
		};

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr GetWindow(IntPtr hWnd, int nIndex);

		private const int GW_HWNDPREV = 3;

		public static long GetFileDuration(this FileInfo file)
		{
			using (var shell = ShellObject.FromParsingName(file.FullName))
			{
				var prop = shell.Properties.System.Media.Duration;
				var durString = prop.FormatForDisplay(PropertyDescriptionFormatOptions.None).Split(':');

				return (durString[0].SmartParse() * TimeSpan.TicksPerHour) + (durString[1].SmartParse() * TimeSpan.TicksPerMinute) + (durString[2].SmartParse() * TimeSpan.TicksPerSecond);
			}
		}

		public static System.Drawing.Color RatingColor(this double rating)
			=> FormDesign.Design.YellowColor
				.MergeColor(FormDesign.Design.RedColor, (int)(100 * (rating / 5).Between(0, 1)))
				.MergeColor(FormDesign.Design.GreenColor, (int)(100 * ((10 - rating) / 5).Between(0, 1)));

		public static string RelativeString(this DateTime dateTime)
		{
			if (dateTime == DateTime.Today)
			{
				return "Today";
			}
			else if (dateTime < DateTime.Today)
			{
				if (dateTime == DateTime.Today.AddDays(-1))
					return "Yesterday";
				else if (DateTime.Today.Ticks - dateTime.Ticks <= 30 * TimeSpan.TicksPerDay)
					return $"{new TimeSpan(DateTime.Today.Ticks - dateTime.Date.Ticks).ToReadableBigString()} ago";
				else
					return $"{(dateTime.Year == DateTime.Today.Year ? "last" : "on")} {dateTime.ToReadableString(dateTime.Year != DateTime.Today.Year, ExtensionClass.DateFormat.MDY)}";
			}
			else
			{
				if (dateTime == DateTime.Today.AddDays(1))
					return "Tomorrow";
				else if (dateTime.Ticks - DateTime.Today.Ticks <= 30 * TimeSpan.TicksPerDay)
					return $"{(dateTime.Ticks - DateTime.Today.Ticks <= 7 * TimeSpan.TicksPerDay ? $"next {dateTime.DayOfWeek} " : "")}in {new TimeSpan(dateTime.Ticks - DateTime.Today.Ticks).ToReadableBigString()}";
				else
					return $"{(dateTime.Year == DateTime.Today.Year ? "next" : "on")} {dateTime.ToReadableString(dateTime.Year != DateTime.Today.Year, ExtensionClass.DateFormat.MDY)}";
			}
		}

		private static bool? isadministrator;

		public static bool IsAdministrator
		{
			get
			{
				if (isadministrator == null)
				{
					var identity = WindowsIdentity.GetCurrent();
					var principal = new WindowsPrincipal(identity);
					isadministrator = principal.IsInRole(WindowsBuiltInRole.Administrator);
				}
				return (bool)(isadministrator);
			}
		}

		public static void CreateShortcut(string Shortcut, string TargetPath, string arguments = "")
		{
			var t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")); //Windows Script Host Shell Object
			dynamic shell = Activator.CreateInstance(t);
			try
			{
				var lnk = shell.CreateShortcut(Shortcut);
				try
				{
					lnk.Arguments = arguments;
					lnk.TargetPath = TargetPath;
					lnk.Save();
				}
				finally
				{
					Marshal.FinalReleaseComObject(lnk);
				}
			}
			finally
			{
				Marshal.FinalReleaseComObject(shell);
			}
		}
	}
}