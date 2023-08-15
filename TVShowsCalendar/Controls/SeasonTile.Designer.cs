namespace ShowsCalendar
{
	partial class _SeasonTile
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
			// SeasonTile
			// 
			this.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.Name = "SeasonTile";
			this.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.Size = new System.Drawing.Size(410, 125);
			this.HoverStateChanged += new System.EventHandler<SlickControls.HoverState>(this.SeasonTile_HoverStateChanged);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
