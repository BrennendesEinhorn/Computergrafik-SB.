using System.Numerics;

namespace DMS.Geometry
{
	public static class Box2dExtensions
	{
		public static Box2D CreateFromMinMax(float minX, float minY, float maxX, float maxY)
		{
			var rectangle = new Box2D(minX, minY, maxX - minX, maxY - minY);
			return rectangle;
		}

		public static Box2D CreateFromCenterSize(float centerX, float centerY, float sizeX, float sizeY)
		{
			var rectangle = new Box2D(0, 0, sizeX, sizeY);
			rectangle.CenterX = centerX;
			rectangle.CenterY = centerY;
			return rectangle;
		}

		public static bool PushXRangeInside(this Box2D rectangleA, Box2D rectangleB)
		{
			if (rectangleA.SizeX > rectangleB.SizeX) return false;
			if (rectangleA.X < rectangleB.X)
			{
				rectangleA.X = rectangleB.X;
			}
			if (rectangleA.MaxX > rectangleB.MaxX)
			{
				rectangleA.X = rectangleB.MaxX - rectangleA.SizeX;
			}
			return true;
		}

		public static bool PushYRangeInside(this Box2D rectangleA, Box2D rectangleB)
		{
			if (rectangleA.SizeY > rectangleB.SizeY) return false;
			if (rectangleA.Y < rectangleB.Y)
			{
				rectangleA.Y = rectangleB.Y;
			}
			if (rectangleA.MaxY > rectangleB.MaxY)
			{
				rectangleA.Y = rectangleB.MaxY - rectangleA.SizeY;
			}
			return true;
		}

		/// <summary>
		/// Calculates the AABR in the overlap
		/// Returns null if no intersection
		/// </summary>
		/// <param name="rectangleB"></param>
		/// <returns>AABR in the overlap</returns>
		public static Box2D Overlap(this Box2D rectangleA, Box2D rectangleB)
		{
			Box2D overlap = null;

			if (rectangleA.Intersects(rectangleB))
			{
				overlap = new Box2D(0.0f, 0.0f, 0.0f, 0.0f);

				overlap.X = (rectangleA.X < rectangleB.X) ? rectangleB.X : rectangleA.X;
				overlap.Y = (rectangleA.Y < rectangleB.Y) ? rectangleB.Y : rectangleA.Y;

				overlap.SizeX = (rectangleA.MaxX < rectangleB.MaxX) ? rectangleA.MaxX - overlap.X : rectangleB.MaxX - overlap.X;
				overlap.SizeY = (rectangleA.MaxY < rectangleB.MaxY) ? rectangleA.MaxY - overlap.Y : rectangleB.MaxY - overlap.Y;
			}

			return overlap;
		}

		public static void TransformCenter(this Box2D rectangle, Matrix3x2 M)
		{
			Vector2 center = new Vector2(rectangle.CenterX, rectangle.CenterY);
			var newCenter = Vector2.Transform(center, M);
			rectangle.CenterX = newCenter.X;
			rectangle.CenterY = newCenter.Y;
		}

		/// <summary>
		/// If an intersection with the frame occurs do the minimal translation to undo the overlap
		/// </summary>
		/// <param name="rectangleB">The AABR to check for intersect</param>
		public static void UndoOverlap(this Box2D rectangleA, Box2D rectangleB)
		{
			if (!rectangleA.Intersects(rectangleB)) return;

			Vector2[] directions = new Vector2[]
			{
				new Vector2(rectangleB.MaxX - rectangleA.X, 0), // push distance A in positive X-direction
				new Vector2(rectangleB.X - rectangleA.MaxX, 0), // push distance A in negative X-direction
				new Vector2(0, rectangleB.MaxY - rectangleA.Y), // push distance A in positive Y-direction
				new Vector2(0, rectangleB.Y - rectangleA.MaxY)  // push distance A in negative Y-direction
			};
			float[] pushDistSqrd = new float[4];
			for (int i = 0; i < 4; ++i)
			{
				pushDistSqrd[i] = directions[i].LengthSquared();
			}
			//find minimal positive overlap amount
			int minId = 0;
			for (int i = 1; i < 4; ++i)
			{
				minId = pushDistSqrd[i] < pushDistSqrd[minId] ? i : minId;
			}

			rectangleA.X += directions[minId].X;
			rectangleA.Y += directions[minId].Y;
		}
	}
}
