using System.Drawing;
using System.Windows.Forms;

namespace ShaderForm.Graph
{
	public partial class InputBox : Form
	{
		public InputBox()
		{
			InitializeComponent();
		}

		public static float Show(float value)
		{
			var inputBox = new InputBox();
			inputBox.textBox1.Text = value.ToString();
			inputBox.textBox1.SelectAll();
			if (DialogResult.OK == inputBox.ShowDialog())
			{
				float result;
				if (float.TryParse(inputBox.textBox1.Text, out result))
				{
					return result;
				}
			}
			return value;
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter: DialogResult = DialogResult.OK; Close(); break;
				case Keys.Escape: DialogResult = DialogResult.Cancel; Close(); break;
			}
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			float result;
			var style = System.Globalization.NumberStyles.AllowDecimalPoint;
			var culture = System.Globalization.CultureInfo.CurrentCulture;
			var ok = float.TryParse(textBox1.Text, style, culture, out result);
			textBox1.BackColor = ok ? Color.PaleGreen : Color.LightSalmon;
		}
	}
}
