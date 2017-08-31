using DMS.TimeTools;
using DMS.Geometry;

namespace MiniGalaxyBirds
{
	public class ComponentClipper : IComponent, ITimedUpdate
	{
		public ComponentClipper(Box2D clipFrame, Box2D frame, Clip clip)
		{
			this.ClipFrame = clipFrame;
			this.Frame = frame;
			this.OnClip = clip;
		}

		public void Update(float absoluteTime)
		{
			if (!ReferenceEquals(null, OnClip) && !this.ClipFrame.Intersects(this.Frame))
			{
				OnClip();
			}
		}

		public Box2D ClipFrame { get; private set; }
		public Box2D Frame { get; private set; }

		public delegate void Clip();
		public event Clip OnClip;
	}
}
