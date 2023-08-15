using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TVShowsCalendar.HandlerClasses;
using Extensions;
using SlickControls.Forms;
using SlickControls.Enums;

namespace TVShowsCalendar.Forms
{
	public partial class ShowFolderSelection : BaseForm
	{
		public ShowFolderSelection()
		{
			InitializeComponent();
			LoadData();
		}

		protected override void DesignChanged(FormDesign design)
		{
            base.DesignChanged(design);
			label2.ForeColor = design.LabelColor;
		}

		private void B_Add_Click(object sender, EventArgs e)
		{
			if(!string.IsNullOrWhiteSpace(TB_Path.Text) && (Directory.Exists(TB_Path.Text) || MessagePrompt.Show("The selected folder does not currently exist.\n\nWould you like to add it anyway?",
				"Directory Unavailable", PromptButtons.YesNo, PromptIcons.Question) == DialogResult.Yes))
			{
				LocalShowHandler.AddGeneralFolder(TB_Path.Text);
				LoadData();
				TB_Path.Text = string.Empty;
			}
		}

		private void B_Browse_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
				TB_Path.Text = folderBrowserDialog1.SelectedPath;
		}

		private void LoadData()
		{
			FLP_Folders.Controls.Clear(true);

			foreach (var item in LocalShowHandler.GeneralFolders)
			{
                var myL = new SlickControls.Controls.SlickLabel()
                {
                    Text = item.FullName,
                    Image = Properties.Resources.Icon_Folder,
                    IconSize = 16,
                    Cursor = Cursors.Hand,
                    Font = new Font("Century Gothic", 9),
                    Padding = new Padding(7, 6, 7, 6),
					ActiveColor = () => FormDesign.Design.RedColor
				};
				myL.HoverStateChanged += (s) =>
				{
					switch (s)
					{
						case HoverState.Normal:
							myL.Image = Properties.Resources.Icon_Folder;
							break;
						default:
							myL.Image = Properties.Resources.Icon_Trash;
							break;
					}
				};
				myL.Click += (s, e) =>
				{
					if (MessagePrompt.Show("Are you sure you want to remove this folder?", "Confirm Action", PromptButtons.OKCancel, PromptIcons.Hand) == DialogResult.OK)
					{
						LocalShowHandler.RemoveGeneralFolder(item.FullName);
						myL.Dispose();
					}
				};

				FLP_Folders.Controls.Add(myL);
			}
		}

        private void B_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if(!string.IsNullOrWhiteSpace(TB_Path.Text))
			{
				if (DialogResult.Yes == MessagePrompt.Show("You haven't added the folder:\n'{TB_Path.Text}'\nWould you like to add it?", "Add Folder?", PromptButtons.YesNo, PromptIcons.Question))
					B_Add_Click(null, null);
			}

			base.OnFormClosing(e);
		}

		private void slickButton1_Load(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                TB_Path.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
