using Extensions;

using SlickControls;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace ShowsCalendar
{
	public enum OptionType { Checkbox, OptionList }

	public partial class OptionControl : SlickControl
	{
		private bool _checked = true;
		private OptionType optionType = OptionType.Checkbox;

		public OptionControl()
		{
			InitializeComponent();
		}

		public event EventHandler ValueChanged;

		[Category("Data")]
		public bool Checked { get => _checked; set { _checked = value; CheckUpdate(); } }

		[Category("Data")]
		public string[] OptionList
		{
			get => CB_OptionList.Items as string[];
			set => CB_OptionList.Items = value;
		}

		[Category("Behavior")]
		public OptionType OptionType { get => optionType; set { optionType = value; SetOptionType(); } }

		[Category("Data")]
		public string SelectedOption { get => CB_OptionList.Text; set => CB_OptionList.Text = value; }

		[Category("Appearance"), DisplayName("Checked Checkbox Text")]
		public string Text_CheckBox_Checked { get; set; }

		[Category("Appearance"), DisplayName("Unchecked Checkbox Text")]
		public string Text_CheckBox_Unchecked { get; set; }

		[Category("Appearance"), DisplayName("Info Text")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Text_Info { get; set; }

		[Category("Appearance"), DisplayName("Title Text")]
		public string Text_Title { get; set; }

		public void Disable()
		{
			ML_CheckBox.Enabled = false;
			CB_OptionList.Enabled = false;
		}

		public void Enable()
		{
			ML_CheckBox.ForeColor = Color.Empty;
			ML_CheckBox.Enabled = true;
			CB_OptionList.Enabled = true;
		}

		private void CheckUpdate()
		{
			ML_CheckBox.Image = (_checked ? Properties.Resources.Tiny_ToggleOn : Properties.Resources.Tiny_ToggleOff).Color(FormDesign.Design.IconColor);
			Invalidate();
			ValueChanged?.Invoke(this, new EventArgs());
		}

		protected override void UIChanged()
		{
			Size = UI.Scale(new Size(325, 100), UI.UIScale);
			Margin = UI.Scale(new Padding(15, 15, 0, 0), UI.UIScale);
			CB_OptionList.Width = (int)(140 * UI.UIScale);
			CB_OptionList.Location = new Point(Width - CB_OptionList.Width - 7, 7);
			ML_CheckBox.Location = new Point(Width - ML_CheckBox.Width - 7, 7);
		}

		protected override void DesignChanged(FormDesign design)
		{
			ML_CheckBox.Image = (_checked ? Properties.Resources.Tiny_ToggleOn : Properties.Resources.Tiny_ToggleOff).Color(design.IconColor);
			BackColor = design.BackColor.MergeColor(design.AccentBackColor, 70);
			if (!CB_OptionList.Enabled)
				ML_CheckBox.ForeColor = design.Type.If(FormDesignType.Dark, design.BackColor.Tint(Lum: 20), design.ForeColor.Tint(Lum: 30));
		}

		private void SetOptionType()
		{
			if (!DesignMode)
			{
				CB_OptionList.Parent = optionType == OptionType.Checkbox ? null : this;
				ML_CheckBox.Parent = optionType == OptionType.OptionList ? null : this;
			}
		}

		private void CB_OptionList_TextChanged(object sender, EventArgs e) => ValueChanged?.Invoke(this, e);

		protected override void OnPaint(PaintEventArgs e)
		{
			var back = HoverState.HasFlag(HoverState.Focused) ? BackColor.MergeColor(FormDesign.Design.ActiveColor, 95) : BackColor;

			CB_OptionList.BackColor = ML_CheckBox.BackColor = back;

			e.Graphics.Clear(back);

			e.Graphics.FillRectangle(new SolidBrush(HoverState.HasFlag(HoverState.Focused) ? FormDesign.Design.ActiveColor : FormDesign.Design.AccentColor),
				0, Height - (int)(2 * UI.FontScale), Width, Height - (int)(2 * UI.FontScale));

			e.Graphics.DrawString(Text_Title, UI.Font(9.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.LabelColor), 8, 8);

			e.Graphics.DrawString(Text_Info,
						 UI.Font(8.25F),
						 new SolidBrush(FormDesign.Design.InfoColor),
						 new Rectangle(8, 16 + UI.Font(9.75F, FontStyle.Bold).Height, Width - 24, Height - 21 - UI.Font(9F, FontStyle.Bold).Height),
						 new StringFormat { Trimming = StringTrimming.EllipsisCharacter });

			if (OptionType == OptionType.Checkbox)
			{
				e.Graphics.DrawString(Checked ? Text_CheckBox_Checked : Text_CheckBox_Unchecked,
					UI.Font(7.25F, FontStyle.Italic),
					new SolidBrush(FormDesign.Design.InfoColor),
					new Rectangle(0, 0, Width - 5, Height - 5),
					new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far });
			}
		}

		private void OptionControl_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
			{
				switch (OptionType)
				{
					case OptionType.Checkbox:
						Checked = !Checked;
						break;

					case OptionType.OptionList:
						CB_OptionList.ShowDropdown();
						break;
				}
			}
		}
	}
}