using Extensions;

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public class WebProcessor : IDisposable
	{
		private string GeneratedSource { get; set; }
		private string URL { get; set; }

		[DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
		internal static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

		[DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
		internal static extern IntPtr GetCurrentProcess();

		public string GetGeneratedHTML(string url)
		{
			URL = url;

			if (ConnectionHandler.State == ConnectionState.Disconnected)
			{
				while (ConnectionHandler.State == ConnectionState.Disconnected)
					Thread.Sleep(1000);
			}
			var t = new Thread(new ThreadStart(WebBrowserThread)) { Name = $"{url} [Web Browser]" };
			t.SetApartmentState(ApartmentState.STA);
			t.Start();
			t.Join();

			try { return GeneratedSource != "" ? GeneratedSource : GetGeneratedHTML(url); }
			finally { GeneratedSource = null; }
		}

		[System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
		private void WebBrowserThread()
		{
			try
			{
				var wb = new WebBrowser() { ScriptErrorsSuppressed = true };
				wb.Navigate(URL);

				wb.DocumentCompleted +=
					 new WebBrowserDocumentCompletedEventHandler(
						  Wb_DocumentCompleted);

				while (wb.ReadyState != WebBrowserReadyState.Complete)
					Application.DoEvents();

				//Added this line, because the final HTML takes a while to show up
				GeneratedSource = wb.Document.Body.InnerHtml;

				wb.Dispose();
				var pHandle = GetCurrentProcess();
				SetProcessWorkingSetSize(pHandle, -1, -1);
			}
			catch (AccessViolationException) { Thread.Sleep(500); WebBrowserThread(); }
		}

		[System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
		private void Wb_DocumentCompleted(object sender,
			 WebBrowserDocumentCompletedEventArgs e)
		{
			try
			{
				var wb = (WebBrowser)sender;
				GeneratedSource = wb.Document.Body.InnerHtml;
			}
			catch (AccessViolationException) { GeneratedSource = string.Empty; }
		}

		#region IDisposable Support

		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.
				GeneratedSource = null;
				URL = null;

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~WebProcessor() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose() =>
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);// TODO: uncomment the following line if the finalizer is overridden above.// GC.SuppressFinalize(this);

		#endregion IDisposable Support
	}
}