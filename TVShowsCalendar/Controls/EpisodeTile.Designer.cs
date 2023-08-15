namespace ShowsCalendar
{
	partial class EpisodeTile
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
			this.components = new System.ComponentModel.Container();
			this.I_Action = new SlickControls.SlickIconComponent(this.components);
			this.SuspendLayout();
			// 
			// I_Dots
			// I_Action
			// 
			this.I_Action.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.I_Action.Bounds = new System.Drawing.Rectangle(24, 6, 16, 16);
			this.I_Action.Enabled = true;
			this.I_Action.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Info;
			this.I_Action.Location = new System.Drawing.Point(24, 4);
			this.I_Action.Parent = this;
			this.I_Action.Size = new System.Drawing.Size(16, 16);
			this.I_Action.MouseHoverChanged += new System.Windows.Forms.MouseEventHandler(this.EpisodeTile_MouseMove);
			this.I_Action.Click += new System.Windows.Forms.MouseEventHandler(this.I_Action_Click);
			// 
			// EpisodeTile
			// 
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Size = new System.Drawing.Size(400, 125);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EpisodeTile_MouseMove);
			this.ResumeLayout(false);

		}

		#endregion

		private SlickControls.SlickIconComponent I_Action;
	}
}
