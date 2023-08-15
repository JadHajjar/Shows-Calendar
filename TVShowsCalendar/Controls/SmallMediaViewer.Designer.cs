namespace ShowsCalendar
{
	partial class SmallMediaViewer
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

			this.SuspendLayout();
			// 
			// MediaViewer
			// 
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Size = new System.Drawing.Size(410, 125);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MediaViewer_MouseClick);

			this.ResumeLayout(false);

		}

		#endregion
	}
}
