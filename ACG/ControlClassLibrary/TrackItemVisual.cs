using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlClassLibrary
{
	public class TrackItemVisual : Button
	{
		public TrackItemVisual(TrackView trackView, ITrackItem trackItem)
		{
			this.trackView = trackView;
			TrackItem = trackItem;

			BackColor = trackView.NewColor();
			Parent = trackView;
			Init(this, trackView, trackItem);
			UpdateControl(this, trackItem, trackView);
		}

		public ITrackItem TrackItem { get; }
			
		private TrackView trackView;

		private static void Init(Control control, TrackView trackView, ITrackItem item)
		{
			bool Dragging = false;
			Point DragStart = Point.Empty;
			control.MouseDown += delegate (object sender, MouseEventArgs e)
			{
				Dragging = true;
				DragStart = e.Location;
				control.Capture = true;
			};
			control.MouseUp += delegate (object sender, MouseEventArgs e)
			{
				Dragging = false;
				control.Capture = false;
			};
			control.MouseMove += delegate (object sender, MouseEventArgs e)
			{
				if (Dragging)
				{
					//left min
					var left = Math.Max(0, e.X + control.Left - DragStart.X);
					//left max
					left = Math.Min(left, trackView.Width - control.Width);
					control.Left = left;
					//top min
					var top = Math.Max(0, e.Y + control.Top - DragStart.Y);
					//top max
					top = Math.Min(top, trackView.Height - control.Height);
					//top in track granularity
					control.Top = (top / trackView.GetTrackHeight()) * trackView.GetTrackHeight();
					UpdateItem(control, item, trackView);
				}
			};
		}

		private static void UpdateControl(Control control, ITrackItem item, TrackView trackView)
		{
			control.Left = (int)Math.Round(trackView.Width * (item.Start / trackView.Length));
			control.Width = (int)Math.Round(trackView.Width * (item.Length / trackView.Length));
			control.Top = item.Track * trackView.GetTrackHeight();
			control.Text = item.Label;
		}

		private static void UpdateItem(Control control, ITrackItem item, TrackView trackView)
		{
			item.Label = control.Text;
			item.Start = control.Left * trackView.Length / trackView.Width;
			item.Length = control.Width * trackView.Length / trackView.Width;
			item.Track = control.Top / trackView.GetTrackHeight();
		}
	}
}
