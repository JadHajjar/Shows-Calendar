using Extensions;

using SlickControls;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PC_About : PanelContent
	{
		public PC_About()
		{
			InitializeComponent();

			loadStorage();

			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ShowsCalendar.ChangeLog.json"))
			using (var reader = new StreamReader(stream))
				P_Changelog.Controls.Add(new ChangeLogVersion(Newtonsoft.Json.JsonConvert.DeserializeObject<VersionChangeLog[]>(reader.ReadToEnd()).FirstOrDefault(x => x.VersionString == ProductVersion)));
		}

		protected override void UIChanged()
		{
			base.UIChanged();

			slickTabControl1.Padding = new Padding(base_Text.Width + 32, base_Text.Height - UI.Font(8.25F).Height + 5, 0, 0);
		}

		private void B_ChangeLog_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Form.PushPanel(null, new PC_Changelog(Assembly.GetExecutingAssembly(), "ShowsCalendar.ChangeLog.json", new Version(ProductVersion)));
			Cursor.Current = Cursors.Default;
		}

		private void B_TMDb_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try { Process.Start("https://themoviedb.org/"); }
			catch (Exception) { Cursor.Current = Cursors.Default; ShowPrompt("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
			Cursor.Current = Cursors.Default;
		}

		private void B_Icons8_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try { Process.Start("https://icons8.com"); }
			catch (Exception) { Cursor.Current = Cursors.Default; ShowPrompt("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
			Cursor.Current = Cursors.Default;
		}

		private void B_Zooqle_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try { Process.Start("https://zooqle.com"); }
			catch (Exception) { Cursor.Current = Cursors.Default; ShowPrompt("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
			Cursor.Current = Cursors.Default;
		}

		private void B_ClearAll_Click(object sender, EventArgs e)
		{
			if (!B_ClearAll.Loading && !B_ClearRecent.Loading)
			{
				B_ClearAll.Loading = true;
				new BackgroundAction(() =>
				{
					EmptyFolder(new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Thumbs")));

					B_ClearAll.Loading = false;
					loadStorage();
				}).Run();
			}
		}

		private void B_ClearRecent_Click(object sender, EventArgs e)
		{
			if (!B_ClearAll.Loading && !B_ClearRecent.Loading)
			{
				B_ClearRecent.Loading = true;
				new BackgroundAction(() =>
				{
					EmptyFolder(new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Thumbs")), x => x.LastAccessTime < DateTime.Now.AddMonths(-1) && !Guid.TryParse(x.FileName(), out var guid));
					
					B_ClearRecent.Loading = false;
					loadStorage();
				}).Run();
			}
		}

		private void loadStorage() => L_Storage.Text = "Shows Calendar stores the thumbnails and images locally so they don\'t have to be downloaded every time they are needed.\r\n\r\nCurrently used storage;  "
			+ new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Thumbs")).If(x => x.Exists, x => x.GetFiles("*", SearchOption.AllDirectories).Sum(y => y.Length), x => 0).SizeString();


		private void EmptyFolder(DirectoryInfo directoryInfo, Func<FileInfo, bool> test = null)
		{
			if (!directoryInfo.Exists) return;

			foreach (var file in directoryInfo.GetFiles())
			{
				if (test == null || test(file))
				{ try { file.Delete(); } catch { } }
			}

			foreach (var subfolder in directoryInfo.GetDirectories())
			{
				EmptyFolder(subfolder, test);
			}

			try { directoryInfo.Delete(); } catch { }
		}
	}
}