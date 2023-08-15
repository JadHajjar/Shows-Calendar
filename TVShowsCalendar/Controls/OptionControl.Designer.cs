namespace ShowsCalendar
{
	partial class OptionControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionControl));
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.CB_OptionList = new SlickControls.SlickDropdown();
			this.ML_CheckBox = new SlickControls.SlickPictureBox();
			this.SuspendLayout();
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 20000;
			this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.toolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(65)))), ((int)(((byte)(77)))));
			this.toolTip.InitialDelay = 600;
			this.toolTip.ReshowDelay = 100;
			// 
			// CB_OptionList
			// 
			this.CB_OptionList.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.CB_OptionList.Conversion = null;
			this.CB_OptionList.Font = new System.Drawing.Font("Nirmala UI", 9.75F);
			this.CB_OptionList.Image = ((System.Drawing.Image)(resources.GetObject("CB_OptionList.Image")));
			this.CB_OptionList.Items = null;
			this.CB_OptionList.LabelText = "Dropdown";
			this.CB_OptionList.Location = new System.Drawing.Point(12, 17);
			this.CB_OptionList.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
			this.CB_OptionList.MaximumSize = new System.Drawing.Size(900, 21);
			this.CB_OptionList.MaxLength = 32767;
			this.CB_OptionList.MinimumSize = new System.Drawing.Size(50, 21);
			this.CB_OptionList.Name = "CB_OptionList";
			this.CB_OptionList.Password = false;
			this.CB_OptionList.Placeholder = null;
			this.CB_OptionList.ReadOnly = false;
			this.CB_OptionList.Required = false;
			this.CB_OptionList.SelectAllOnFocus = false;
			this.CB_OptionList.SelectedItem = null;
			this.CB_OptionList.SelectedText = "";
			this.CB_OptionList.SelectionLength = 0;
			this.CB_OptionList.SelectionStart = 0;
			this.CB_OptionList.ShowLabel = false;
			this.CB_OptionList.Size = new System.Drawing.Size(140, 21);
			this.CB_OptionList.TabIndex = 5;
			this.CB_OptionList.Text = "slickDropdown1";
			this.CB_OptionList.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.CB_OptionList.Validation = SlickControls.ValidationType.None;
			this.CB_OptionList.ValidationRegex = "";
			this.CB_OptionList.TextChanged += new System.EventHandler(this.CB_OptionList_TextChanged);
			// 
			// ML_CheckBox
			// 
			this.ML_CheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.ML_CheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ML_CheckBox.Image = global::ShowsCalendar.Properties.Resources.Tiny_ToggleOn;
			this.ML_CheckBox.Location = new System.Drawing.Point(6, 12);
			this.ML_CheckBox.Name = "ML_CheckBox";
			this.ML_CheckBox.Padding = new System.Windows.Forms.Padding(7, 5, 5, 5);
			this.ML_CheckBox.Size = new System.Drawing.Size(26, 26);
			this.ML_CheckBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.ML_CheckBox.TabIndex = 4;
			this.ML_CheckBox.TabStop = false;
			this.ML_CheckBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OptionControl_MouseClick);
			// 
			// OptionControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.Name = "OptionControl";
			this.Size = new System.Drawing.Size(325, 100);
			this.SpaceTriggersClick = true;
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OptionControl_MouseClick);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ToolTip toolTip;
		private SlickControls.SlickDropdown CB_OptionList;
		private SlickControls.SlickPictureBox ML_CheckBox;
	}
}
