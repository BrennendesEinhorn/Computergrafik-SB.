using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		const int trackHeight = 100;

		private void movableControlsLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{
			var pen = new Pen(Color.Black, 3);
			var delta = trackHeight;
			for (var y = delta; y < e.ClipRectangle.Height; y += trackHeight)
			{
				e.Graphics.DrawLine(pen, 0, y, e.ClipRectangle.Right, y);
			}
		}

		private void movableControlsLayoutPanel1_ControlBoundsChanging(object sender, ControlClassLibrary.NewControlBoundsArgs e)
		{
			var bounds = e.NewBounds;
			bounds.Y = (int)Math.Round(Math.Round(bounds.Y / (float)trackHeight) * trackHeight);
			e.NewBounds = bounds;
			
		}

		private void panel8_Enter(object sender, EventArgs e)
		{
			var ctrl = sender as Control;
			ctrl.BackColor = Color.Green;
		}

		private void panel8_Leave(object sender, EventArgs e)
		{
			var ctrl = sender as Control;
			ctrl.BackColor = Color.Gray;
		}
	}
}
