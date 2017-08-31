using System.Drawing;
using System.Windows.Forms;

namespace ControlClassLibrary
{
	public class NewControlBoundsArgs : ControlEventArgs
	{
		public NewControlBoundsArgs(Control control, Rectangle newBounds): base(control)
		{
			NewBounds = newBounds;
		}

		public Rectangle NewBounds { get; set; }
	}
}
