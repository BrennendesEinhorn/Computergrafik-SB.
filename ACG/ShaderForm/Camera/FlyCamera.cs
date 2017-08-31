using System;
using System.Windows.Forms;
using OpenTK;

namespace ShaderForm.Camera
{
	public class FlyCamera
	{
		public FlyCamera()
		{
			Reset();
		}

		public Vector3 Position;
		public Vector3 Rotation;
		public float Speed;

		public bool IsActive { get { return fwd || left || right || back || up || down; } }

		public void Reset()
		{
			Position = new Vector3(0);
			Rotation = new Vector3(0);
			Speed = 0.05f;
		}

		public void Update(float mouseX, float mouseY, bool leftPressed)
		{
			float mouseXDelta = lastMouseX - mouseX;
			lastMouseX = mouseX;
			float mouseYDelta = lastMouseY - mouseY;
			lastMouseY = mouseY;

			Vector3 camLeft = new Vector3(-1, 0, 0);
			Vector3 camFwdTmp = new Vector3(0, 0, 1);

			/** X-Rotation **/
			float xRotation2 = -(float)(mouseYDelta * Math.PI / 180f);
			if (leftPressed) Rotation.X += xRotation2;
			Rotation.X = (float)(Rotation.X % (2.0 * Math.PI));
			camFwdTmp = RotateX(camFwdTmp, -Rotation.X);
			camLeft = RotateX(camLeft, -Rotation.X);

			/** Y-Rotation **/
			float yRotation2 = -(float)(mouseXDelta * Math.PI / 180f);
			if (leftPressed) Rotation.Y += yRotation2;
			Rotation.Y = (float)(Rotation.Y % (2.0 * Math.PI));
			camFwdTmp = RotateY(camFwdTmp, Rotation.Y);
			camLeft = RotateY(camLeft, Rotation.Y);

			var camUpTemp = -Vector3.Cross(camFwdTmp, camLeft);

			if (fwd) Position += camFwdTmp * Speed;
			if (back) Position -= camFwdTmp * Speed;
			if (left) Position += camLeft * Speed;
			if (right) Position -= camLeft * Speed;
			if (up) Position += camUpTemp * Speed;
			if (down) Position -= camUpTemp * Speed;

			camFwd = camFwdTmp;
		}

		public void KeyChange(Keys key, bool pressed)
		{
			switch (key)
			{
				case Keys.W: fwd = pressed; break;
				case Keys.A: left = pressed; break;
				case Keys.S: back = pressed; break;
				case Keys.D: right = pressed; break;
				case Keys.Q: up = pressed; break;
				case Keys.E: down = pressed; break;
				case Keys.Oemplus:
				case Keys.Add: if(pressed) Speed *= 2.0f; break;
				case Keys.OemMinus:
				case Keys.Subtract: if (pressed) Speed /= 2.0f; break;
			}
		}

		private Vector3 camFwd = new Vector3(0, 0, 1);
		private bool fwd, back, left, right, up, down;
		private float lastMouseX = 0;
		private float lastMouseY = 0;

		private Vector3 RotateX(Vector3 vec, float angle)
		{
			var rotateXM = Matrix3.CreateRotationX(angle);
			return Vector3.Transform(vec, rotateXM);
		}

		private Vector3 RotateY(Vector3 vec, float angle)
		{
			var rotateZM = Matrix3.CreateRotationY(angle);
			return Vector3.Transform(vec, rotateZM);
		}
	}
}
