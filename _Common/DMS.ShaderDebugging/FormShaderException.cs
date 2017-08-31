using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DMS.ShaderDebugging
{
	public partial class FormShaderException : Form
	{
		public FormShaderException()
		{
			InitializeComponent();
			listBox.DataSource = errors;
			listBox.MouseWheel += OnMouseWheel;
			richTextBox.MouseWheel += OnMouseWheel;
		}

		public BindingList<ShaderLogLine> Errors { get { return errors; } }

		public string ShaderSourceCode { get { return richTextBox.Text; } set { richTextBox.Text = value; } }

		public void Select(int id)
		{
			listBox.SelectedIndex = id;
			listBox_SelectedIndexChanged(null, null);
		}

		public float FontSize
		{
			get
			{
				return listBox.Font.Size;
			}
			set
			{
				var size = Math.Max(6, value);
				var font = new Font(listBox.Font.FontFamily, size);
				listBox.Font = font;
				richTextBox.Font = font;
			}
		}

		private BindingList<ShaderLogLine> errors = new BindingList<ShaderLogLine>();

		private void OnMouseWheel(object sender, MouseEventArgs e)
		{
			if (Keys.Control == ModifierKeys)
			{
				FontSize += Math.Sign(e.Delta) * 2;
			}
		}

		private void FormShaderError_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Escape: buttonCancel.PerformClick(); break;
				case Keys.S: if (e.Control) buttonSave.PerformClick(); break;
			}
		}

		private void listBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			richTextBox.Select(0, richTextBox.Text.Length);
			richTextBox.SelectionColor = Color.White;
			try
			{
				var logLine = errors[listBox.SelectedIndex];
				var nr = logLine.LineNumber - 1;
				int start = richTextBox.GetFirstCharIndexFromLine(nr);
				int length = richTextBox.Lines[nr].Length;
				richTextBox.Select(start, length);
				richTextBox.SelectionBackColor = Color.DarkRed;
				richTextBox.ScrollToCaret();
			}
			catch { }
		}

		private void FormShaderException_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.SaveLayout();
			RegistryLoader.SaveValue(Name, "fontSize", FontSize);
		}

		private void FormShaderException_Load(object sender, EventArgs e)
		{
			this.LoadLayout();
			FontSize = (float)Convert.ToDouble(RegistryLoader.LoadValue(Name, "fontSize", 12.0f));
		}

		private void FormShaderException_Shown(object sender, EventArgs e)
		{
			TopMost = false;
		}
	}
}
