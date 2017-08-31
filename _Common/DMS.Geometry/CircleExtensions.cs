using System;
using System.Numerics;

namespace DMS.Geometry
{
	public static class CircleExtensions
	{
		public static Circle CreateFromBox(Box2D box)
		{
			var circle = new Circle(box.CenterX, box.CenterY, 0.5f * Math.Min(box.SizeX, box.SizeY));
			return circle;
		}

		public static Circle CreateFromMinMax(float minX, float minY, float maxX, float maxY)
		{
			var box = Box2dExtensions.CreateFromMinMax(minX, minY, maxX, maxY);
			return CreateFromBox(box);
		}

		public static bool PushXRangeInside(this Circle circle, float minX, float maxX)
		{
			if (circle.Radius > maxX - minX) return false;
			if (circle.CenterX - circle.Radius < minX)
			{
				circle.CenterX = minX + circle.Radius;
			}
			if (circle.CenterX + circle.Radius > maxX)
			{
				circle.CenterX = maxX - circle.Radius;
			}
			return true;
		}

		public static bool PushYRangeInside(this Circle circle, float minY, float maxY)
		{
			if (circle.Radius > maxY - minY) return false;
			if (circle.CenterY - circle.Radius < minY)
			{
				circle.CenterY = minY + circle.Radius;
			}
			if (circle.CenterY + circle.Radius > maxY)
			{
				circle.CenterY = maxY - circle.Radius;
			}
			return true;
		}

		public static void UndoOverlap(this Circle a, Circle b)
		{
			Vector2 cB = new Vector2(b.CenterX, b.CenterY);
			Vector2 diff = new Vector2(a.CenterX, a.CenterY);
			diff -= cB;
			diff /= diff.Length();
			diff *= a.Radius + b.Radius;
			var newA = cB + diff;
			a.CenterX = newA.X;
			a.CenterY = newA.Y;
		}
	}
}
