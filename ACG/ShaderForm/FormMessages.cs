using ControlClassLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShaderForm
{
	public partial class FormMessages : Form
	{
		public FormMessages()
		{
			InitializeComponent();
			errorLog.MouseWheel += ErrorLog_MouseWheel;
			try
			{
				this.LoadLayout();
				checkBoxOnTop.Checked = TopMost;
				fontSize = (float)Convert.ToDouble(RegistryLoader.LoadValue(Name, "fontSize", 12.0f));
				FontSize = fontSize;
			}
			catch (Exception e)
			{
				Append(e.Message);
			}
		}

		public void SaveData()
		{
			try
			{
				this.SaveLayout();
				RegistryLoader.SaveValue(Name, "fontSize", fontSize);
			}
			catch (Exception e)
			{
				Append(e.Message);
			}
		}

		public void Append(string message)
		{
			if (!string.IsNullOrWhiteSpace(message))
			{
				string newEntry = DateTime.Now.ToString("HH:mm:ss.fff") + ' ' + message + Environment.NewLine;
				errorLog.Text = newEntry + errorLog.Text;
			}
		}

		public void Clear()
		{
			errorLog.Clear();
		}

		private void ErrorLog_MouseWheel(object sender, MouseEventArgs e)
		{
			if (Control.ModifierKeys != Keys.Control) return;
			FontSize += e.Delta * SystemInformation.MouseWheelScrollLines / 120;
		}

		private float fontSize = 12.0f;
		private float FontSize
		{
			get { return fontSize; }
			set
			{
				fontSize = Math.Max(4.0f, value);
				errorLog.Font = new Font(errorLog.Font.FontFamily, fontSize);
			}
		}

		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			errorLog.Clear();
		}

		private void fontBiggerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FontSize += 3.0f;
		}

		private void fontSmallerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FontSize -= 3.0f;
		}

		private void FormMessages_FormClosing(object sender, FormClosingEventArgs e)
		{
			Visible = false;
			e.Cancel = true;
		}

		private void checkBoxOnTop_CheckedChanged(object sender, EventArgs e)
		{
			TopMost = checkBoxOnTop.Checked;
		}
	}
}
