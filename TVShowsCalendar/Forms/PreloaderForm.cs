using Extensions;

using SlickControls;

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public partial class PreloaderForm : Form
	{
		public PreloaderForm()
		{
			InitializeComponent();

			FormDesign.Load();

			Size = UI.Scale(Size, UI.UIScale);
			BackColor = FormDesign.Design.MenuColor;
			ForeColor = FormDesign.Design.MenuForeColor;
			base_L_Text.Font = UI.Font(12.5F);
			L_Version.Font = UI.Font(6.75F);
			L_Version.Text = "v " + ProductVersion;
			L_Version.ForeColor = FormDesign.Design.InfoColor;
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(Point.Empty, base_PB_Icon.Size));
			e.Graphics.DrawImage(Properties.Resources.Icon_64_Back.Color(FormDesign.Design.MenuForeColor), new Rectangle(Point.Empty, base_PB_Icon.Size));
			e.Graphics.DrawImage(Properties.Resources.Icon_64_Shadow.Color(FormDesign.Design.MenuColor), new Rectangle(Point.Empty, base_PB_Icon.Size));
			e.Graphics.DrawImage(Properties.Resources.Icon_64_Play.Color(FormDesign.Design.ActiveColor), new Rectangle(Point.Empty, base_PB_Icon.Size));
		}
	}
}