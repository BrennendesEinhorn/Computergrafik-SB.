using System.Numerics;

namespace DMS.Geometry
{
	/// <summary>
	/// Transformation 2D class that is based on row-major matrices
	/// </summary>
	public class Transformation2D
	{
		public Transformation2D()
		{
			Reset();
		}

		public static implicit operator Matrix3x2(Transformation2D t)
		{
			return t.matrix;
		}

		/// <summary>
		/// create a rotation matrix that rotates around a given rotation center (pivot point)
		/// </summary>
		/// <param name="pivotX">rotation center x</param>
		/// <param name="pivotY">rotation center y</param>
		/// <param name="angle">radiant</param>
		/// <returns></returns>
		public static Transformation2D CreateRotationAround(float pivotX, float pivotY, float degrees)
		{
			var t = new Transformation2D();
			t.TranslateGlobal(-pivotX, -pivotY);
			t.RotateGlobal(degrees);
			t.TranslateGlobal(pivotX, pivotY);
			return t;
		}

		public static Transformation2D CreateScaleAround(float pivotX, float pivotY, float scaleX, float scaleY)
		{
			var t = new Transformation2D();
			t.TranslateGlobal(-pivotX, -pivotY);
			t.ScaleGlobal(scaleX, scaleY);
			t.TranslateGlobal(pivotX, pivotY);
			return t;
		}

		public void Reset()
		{
			matrix = Matrix3x2.Identity;
		}

		/// <summary>
		/// Rotate Transform
		/// </summary>
		/// <param name="degrees"></param>
		public void RotateGlobal(float degrees)
		{
			TransformGlobal(Matrix3x2.CreateRotation(MathHelper.DegreesToRadians(degrees)));
		}

		public void RotateLocal(float degrees)
		{
			TransformLocal(Matrix3x2.CreateRotation(MathHelper.DegreesToRadians(degrees)));
		}

		public void ScaleGlobal(Vector2 scales)
		{
			TransformGlobal(Matrix3x2.CreateScale(scales));
		}

		public void ScaleGlobal(float x, float y)
		{
			TransformGlobal(Matrix3x2.CreateScale(x, y));
		}

		public void ScaleLocal(Vector2 scales)
		{
			TransformLocal(Matrix3x2.CreateScale(scales));
		}

		public void ScaleLocal(float x, float y)
		{
			TransformLocal(Matrix3x2.CreateScale(x, y));
		}

		public void TranslateGlobal(Vector2 translation)
		{
			TransformGlobal(Matrix3x2.CreateTranslation(translation));
		}

		public void TranslateGlobal(float x, float y)
		{
			TransformGlobal(Matrix3x2.CreateTranslation(x, y));
		}

		public void TranslateLocal(Vector2 translation)
		{
			TransformLocal(Matrix3x2.CreateTranslation(translation));
		}

		public void TranslateLocal(float x, float y)
		{
			TransformLocal(Matrix3x2.CreateTranslation(x, y));
		}

		public Vector2 Transform(Vector2 position)
		{
			return Vector2.Transform(position, matrix);
		}

		public void TransformGlobal(Matrix3x2 transform)
		{
			matrix *= transform;
		}

		public void TransformLocal(Matrix3x2 transform)
		{
			matrix = transform * matrix;
		}

		private Matrix3x2 matrix;
	}
}
