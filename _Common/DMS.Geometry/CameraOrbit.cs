using System;
using System.Numerics;

namespace DMS.Geometry
{
	public class CameraOrbit
	{
		public CameraOrbit()
		{
			Aspect = 1;
			Distance = 1;
			FarClip = 1;
			FovY = 90;
			Azimuth = 0;
			NearClip = 0.1f;
			Target = Vector3.Zero;
			Elevation = 0;
		}

		public float Aspect { get; set; }
		public float Azimuth { get; set; }
		public float Distance { get; set; }
		public float Elevation { get; set; }
		public float FarClip { get; set; }
		public float FovY { get { return fovY; } set { fovY = MathHelper.Clamp(value, 0f, 179.9f); } }
		public float NearClip { get; set; }
		public Vector3 Target { get { return target; } set { target = value; } }
		public float TargetX { get { return Target.X; } set { target.X = value; } }
		public float TargetY { get { return Target.Y; } set { target.Y = value; } }
		public float TargetZ { get { return Target.Z; } set { target.Z = value; } }

		public Matrix4x4 CalcViewMatrix()
		{
			Distance = MathHelper.Clamp(Distance, NearClip, FarClip);
			var mtxDistance = Matrix4x4.Transpose(Matrix4x4.CreateTranslation(0, 0, -Distance));
			var mtxElevation = Matrix4x4.Transpose(Matrix4x4.CreateRotationX(MathHelper.DegreesToRadians(Elevation)));
			var mtxAzimut = Matrix4x4.Transpose(Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(Azimuth)));
			var mtxTarget = Matrix4x4.Transpose(Matrix4x4.CreateTranslation(-Target));
			return mtxDistance * mtxElevation * mtxAzimut * mtxTarget;
		}

		public Matrix4x4 CalcProjectionMatrix()
		{
			return Matrix4x4.Transpose(Matrix4x4.CreatePerspectiveFieldOfView(
				MathHelper.DegreesToRadians(FovY),
				Aspect, NearClip, FarClip));
		}

		public Matrix4x4 CalcMatrix()
		{
			return CalcProjectionMatrix() * CalcViewMatrix();
		}

		public Vector3 CalcPosition()
		{
			var view = CalcViewMatrix();
			Matrix4x4 inverse;
			if (!Matrix4x4.Invert(view, out inverse)) throw new ArithmeticException("Could not invert matrix");
			return new Vector3(inverse.M14, inverse.M24, inverse.M34);
		}

		private float fovY;
		private Vector3 target;
	}
}
