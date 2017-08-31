using DMS.TimeTools;
using DMS.Geometry;
using System;
namespace MiniGalaxyBirds
{
	public class ComponentPlayer : IComponent, ITimedUpdate
	{
		public ComponentPlayer(Box2D frame, Box2D clipFrame)
		{
			if (ReferenceEquals(null, frame))
			{
				throw new Exception("Valid frame needed");
			}
			this.frame = frame;
			this.clipFrame = clipFrame;
			this.Shoot = false;
		}

		public void Update(float absoluteTime)
		{
			float timeDelta = absoluteTime - lastUpdate;
			lastUpdate = absoluteTime;
			//player movement
			frame.X += 0.9f * axisLeftRight * timeDelta;
			frame.Y -= 0.5f * axisUpDown * timeDelta;
			//limit player position
			frame.PushXRangeInside(clipFrame);
			frame.PushYRangeInside(clipFrame);

			if (Shoot && shootCoolDown < 0.0f && !ReferenceEquals(null, OnCreateBullet))
			{
				OnCreateBullet(absoluteTime, frame.X, frame.Y);
				OnCreateBullet(absoluteTime, frame.MaxX, frame.Y);
				shootCoolDown = 0.1f;
			}
			else
			{
				shootCoolDown -= timeDelta;
			}
		}

		public void SetPlayerState(float axisUpDown, float axisLeftRight, bool shoot)
		{
			this.axisUpDown = axisUpDown;
			this.axisLeftRight = axisLeftRight;
			this.Shoot = shoot;
		}
		public bool Shoot { get; private set; }

		public delegate void CreateBullet(float time, float x, float y);
		public event CreateBullet OnCreateBullet;

		private float axisUpDown = 0.0f;
		private float axisLeftRight = 0.0f;
		private float shootCoolDown = 0.0f;
		private Box2D frame;
		private Box2D clipFrame;
		private float lastUpdate = 0.0f;
	}
}
