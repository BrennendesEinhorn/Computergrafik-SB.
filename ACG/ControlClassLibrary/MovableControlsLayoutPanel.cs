using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ControlClassLibrary
{
	public partial class MovableControlsLayoutPanel : Panel
	{
		public MovableControlsLayoutPanel()
		{
			ControlAdded += MovableControlsLayoutPanel_ControlAdded;
			AllowDrop = true;
			AllowOverlap = false;
			RestrictToPanel = true;
			DoubleBuffered = true;
		}

		public bool AllowOverlap { get; set; }
		public bool RestrictToPanel { get; set; }
		public Control SelectedControl { get; private set; }

		public event EventHandler<NewControlBoundsArgs> ControlBoundsChanging;

		private void MovableControlsLayoutPanel_ControlAdded(object sender, ControlEventArgs e)
		{
			e.Control.Parent = this;
			AddDragDropHandler(e.Control);
		}

		private void AddDragDropHandler(Control control)
		{
			bool Dragging = false;
			Point DragStart = Point.Empty;
			control.MouseEnter += (s, e) => Cursor = Cursors.SizeAll;
			control.MouseLeave += (s, e) => Cursor = Cursors.Default;
			control.MouseDown += delegate (object sender, MouseEventArgs e)
			{
				Dragging = true;
				SelectedControl = control;
				control.Select();
				DragStart = e.Location;
				control.Capture = true;
				Cursor = Cursors.Hand;
			};
			control.MouseUp += delegate (object sender, MouseEventArgs e)
			{
				Dragging = false;
				control.Capture = false;
				Cursor = Cursors.Default;
			};
			control.MouseMove += delegate (object sender, MouseEventArgs e)
			{
				if (Dragging)
				{
					//restrict to parent bounds
					var left = e.X + control.Left - DragStart.X;
					var top  = e.Y + control.Top - DragStart.Y;

					if (RestrictToPanel)
					{
						left = Math.Max(0, left); //left min
						left = Math.Min(left, Width - control.Width); //left max
						top = Math.Max(0, top); //top min
						top = Math.Min(top, Height - control.Height); //top max
					}

					var bounds = new Rectangle(left, top, control.Width, control.Height);
					var args = new NewControlBoundsArgs(control, bounds);

					ControlBoundsChanging?.Invoke(this, args);

					if (!AllowOverlap)
					{
						foreach (var ctrl in Controls.OfType<Control>())
						{
							if (ReferenceEquals(ctrl, control)) continue;
							if (ctrl.Bounds.IntersectsWith(args.NewBounds))
							{
								Cursor = Cursors.No;
								return;
							}
						}
					}
					Cursor = Cursors.Hand;
					control.Left = args.NewBounds.Left;
					control.Top = args.NewBounds.Top;
					Invalidate(false);
				}
			};
		}
	}
}
