using OpenTK;
using System;
using System.Collections.Generic;

namespace DMS.OpenGL
{
	public class CatmullRomSpline
	{
		public static float H1(float t)
		{
			return 2 * t * t * t - 3 * t * t + 1;
		}

		public static float H2(float t)
		{
			return -2 * t * t * t + 3 * t * t;
		}

		public static float H3(float t)
		{
			return t * t * t - 2 * t * t + t;
		}

		public static float H4(float t)
		{
			return t * t * t - t * t;
		}

		public static float EvaluateSegment(float point0, float point1, float tangent0, float tangent1, float t)
		{
			return H1(t) * point0 + H2(t) * point1 + H3(t) * tangent0 + H4(t) * tangent1;
		}

		public static Vector2 EvaluateSegment(Vector2 point0, Vector2 point1, Vector2 tangent0, Vector2 tangent1, float t)
		{
			return H1(t) * point0 + H2(t) * point1 + H3(t) * tangent0 + H4(t) * tangent1;
		}

		public static Vector3 EvaluateSegment(Vector3 point0, Vector3 point1, Vector3 tangent0, Vector3 tangent1, float t)
		{
			return H1(t) * point0 + H2(t) * point1 + H3(t) * tangent0 + H4(t) * tangent1;
		}

		public static Tuple<int, int> FindSegment(float t, int pointCount)
		{
			var id = (int)Math.Floor(t);
			return new Tuple<int, int>(id % pointCount, (id + 1) % pointCount);
		}

		public static Vector2 FiniteDifference(Vector2 pointL, Vector2 pointR)
		{
			return 0.5f * (pointR - pointL);
		}
		public static List<Vector2> FiniteDifferenceLoop(IList<Vector2> points)
		{
			var output = new List<Vector2>();
			if (points.Count < 3) return output;
			//first tangent
			output.Add(FiniteDifference(points[points.Count - 1], points[1]));
			//the rest except last
			for (int i = 0; i < points.Count - 2; ++i)
			{
				output.Add(FiniteDifference(points[i], points[i + 2]));
			}
			//add last
			output.Add(FiniteDifference(points[points.Count - 2], points[0]));
			return output;
		}
	}
}
