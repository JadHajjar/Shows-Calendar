using Extensions;

using SlickControls;

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PC_LibraryFolders : PanelContent
	{
		public PC_LibraryFolders()
		{
			InitializeComponent();
			LoadFolders();

			FirstFocusedControl = B_Add;

			if (B_Done.Visible = Data.FirstTimeSetup)
			{
				B_Refresh.Dispose();
				B_SearchMedia.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
				TLP_Main.SetColumn(B_SearchMedia, 1);
			}
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);
			label2.ForeColor = design.LabelColor;
		}

		private void B_Add_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TB_Path.Text) && (Directory.Exists(TB_Path.Text) || ShowPrompt("The selected folder does not currently exist.\n\nWould you like to add it anyway?",
				"Directory Unavailable", PromptButtons.YesNo, PromptIcons.Question) == DialogResult.Yes))
			{
				IO.Handler.AddGeneralFolder(TB_Path.Text);
				LoadFolders();
				TB_Path.Text = string.Empty;
			}
			else
			{
				var io = new IOSelectionDialog();
				if (io.PromptFolder(Form) == DialogResult.OK)
				{
					IO.Handler.AddGeneralFolder(io.SelectedPath);
					LoadFolders();
				}
			}
		}

		protected override void UIChanged()
		{
			base.UIChanged();

			label2.Font = UI.Font(9.75F, FontStyle.Bold);
		}

		private void LoadFolders()
		{
			FLP_Folders.SuspendDrawing();
			FLP_Folders.Controls.Clear(true);

			foreach (var item in IO.Handler.GeneralFolders)
			{
				var myL = new SlickLabel()
				{
					Text = item.FullName,
					Image = Properties.Resources.Tiny_Folder,
					Cursor = Cursors.Hand,
					Font = UI.Font(9F),
					Padding = new Padding(7, 6, 7, 6),
					ColorStyle = ColorStyle.Red	
				};
				myL.HoverStateChanged += (e, s) =>
				{
					switch (s)
					{
						case HoverState.Normal:
							myL.Image = Properties.Resources.Tiny_Folder;
							break;

						default:
							myL.Image = Properties.Resources.Tiny_Trash;
							break;
					}
				};
				myL.Click += (s, e) =>
				{
					if (ShowPrompt("Are you sure you want to remove this folder?", "Confirm Action", PromptButtons.OKCancel, PromptIcons.Hand) == DialogResult.OK)
					{
						IO.Handler.RemoveGeneralFolder(item.FullName);
						myL.Dispose();
					}
				};

				FLP_Folders.Controls.Add(myL);
			}

			FLP_Folders.ResumeDrawing();
		}

		public override bool CanExit(bool toBeDisposed)
		{
			if (!string.IsNullOrWhiteSpace(TB_Path.Text) && Directory.Exists(TB_Path.Text))
			{
				if (DialogResult.Yes == ShowPrompt($"You haven't added the folder:\n'{TB_Path.Text}'\n\nWould you like to add it?", "Add Folder?", PromptButtons.YesNo, PromptIcons.Question))
					B_Add_Click(null, null);
			}

			return true;
		}

		private void B_Done_Click(object sender, EventArgs e) => Data.Mainform.Setup(2);

		private void B_SearchMedia_Click(object sender, EventArgs e) => Form.PushPanel<PC_DiscoverLibrary>(null);

		private void B_Refresh_Click(object sender, EventArgs e)
		{
			new BackgroundAction(() =>
			{
				this.TryInvoke(() =>
				{
					B_Refresh.Loading = true;
					B_Refresh.Enabled = false;
				});

				IO.Handler.LoadFolders(false);

				foreach (var item in ShowManager.Shows.SelectMany(x => x.Episodes))
					item.RawVidFiles = Array.Empty<string>();

				LocalShowHandler.LoadFiles();

				foreach (var item in MovieManager.Movies)
					item.RawVidFiles = Array.Empty<string>();

				LocalMovieHandler.LoadFiles();

				this.TryInvoke(() =>
				{
					B_Refresh.Loading = false;
					B_Refresh.Enabled = true;
				});
			}).Run();
		}
	}
}