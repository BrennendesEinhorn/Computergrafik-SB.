using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlClassLibrary
{
	public partial class ShaderNodeControl : UserControl
	{
		public ShaderNodeControl()
		{
			InitializeComponent();
		}

		private void ShaderNodeControl_Enter(object sender, System.EventArgs e)
		{
			this.BackColor = Color.Green;
		}

		private void ShaderNodeControl_Leave(object sender, System.EventArgs e)
		{
			this.BackColor = DefaultBackColor;
		}

		private void ShaderNodeControl_Move(object sender, System.EventArgs e)
		{
			if (!ReferenceEquals(null, Parent))
			{
				var factor = 1.0 / Parent.Bounds.Width;
				var start = Math.Round(Bounds.X * factor, 2);
				var end = Math.Round(Bounds.Right * factor, 2);
				var length = Math.Round(Bounds.Width * factor, 2);
				label2.Text = start.ToString() + '-' + end.ToString() 
					+ " (" + length.ToString() + ')';
			}
			textBoxTimeSpan.Text = Bounds.Location.ToString();
		}
	}
}
