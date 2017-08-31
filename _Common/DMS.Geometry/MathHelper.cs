using System;
using System.Numerics;

namespace DMS.Geometry
{
	public static class MathHelper
	{
		public static float PI = (float)Math.PI;
		public static float TWO_PI = (float)(Math.PI * 2.0);

		public static float Clamp(float x, float min, float max)
		{
			return Math.Min(max, Math.Max(min, x));
		}

		public static double Clamp(double x, double min, double max)
		{
			return Math.Min(max, Math.Max(min, x));
		}

		public static float DegreesToRadians(float angle)
		{
			return (angle * TWO_PI) / 360.0f;
		}

		/// <summary>
		/// Linear interpolation of arguments a and b according to weight
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="weight"></param>
		/// <returns></returns>
		public static float Lerp(float a, float b, float weight)
		{
			return a * (1 - weight) + b * weight;
		}

		/// <summary>
		/// Linear interpolation of arguments a and b according to weight
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="weight"></param>
		/// <returns></returns>
		public static double Lerp(double a, double b, double weight)
		{
			return a * (1 - weight) + b * weight;
		}

		public static float Floor(this float x)
		{
			return (float)Math.Floor(x);
		}

		public static Vector3 Clamp(this Vector3 v, float min, float max)
		{
			return new Vector3(Clamp(v.X, min, max), Clamp(v.Y, min, max), Clamp(v.Z, min, max));
		}

		public static Vector3 Floor(this Vector3 v)
		{
			return new Vector3(Floor(v.X), Floor(v.Y), Floor(v.Z));
		}

		public static Vector3 Mod(this Vector3 x, float y)
		{
			var div = x / y;
			return x - y * Floor(div);
		}

		/// <summary>
		/// Copy matrix elements into array in column major style
		/// </summary>
		/// <param name="input">matrix to convert</param>
		/// <returns>array of matrix elements</returns>
		public static float[] ToArray(this Matrix4x4 input)
		{
			int i = 0;
			var a = new float[16];
			
			a[i++] = input.M11;
			a[i++] = input.M21;
			a[i++] = input.M31;
			a[i++] = input.M41;

			a[i++] = input.M12;
			a[i++] = input.M22;
			a[i++] = input.M32;
			a[i++] = input.M42;

			a[i++] = input.M13;
			a[i++] = input.M23;
			a[i++] = input.M33;
			a[i++] = input.M43;

			a[i++] = input.M14;
			a[i++] = input.M24;
			a[i++] = input.M34;
			a[i++] = input.M44;

			return a;
		}

		/// <summary>
		/// Converts given cartesian coordinates into plar coordinates
		/// </summary>
		/// <param name="cartesian">cartesian coordinates</param>
		/// <returns>a vector with x component beeing the angle [-PI, PI] and y component the radius</returns>
		public static Vector2 ToPolar(this Vector2 cartesian)
		{
			float angle = (float)Math.Atan2(cartesian.Y, cartesian.X);
			float radius = cartesian.Length();
			return new Vector2(angle, radius);
		}
	}
}
