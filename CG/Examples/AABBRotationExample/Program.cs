using DMS.Application;
using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace Example
{
	using Line = Tuple<Vector2, Vector2>;

	class Visual
	{
		private const float size = 0.7f;
		private Line stick = new Line(new Vector2(-size, -size), new Vector2(size, size));
		private Box2D stickAABB = Box2D.EMPTY;

		private Visual()
		{
			GL.LineWidth(5.0f);
			GL.ClearColor(1, 1, 1, 0);
			GL.Enable(EnableCap.LineSmooth);

			GL.Enable(EnableCap.Blend);
			//setup blending equation Color = Color_s · alpha + Color_d · (1 - alpha)
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.BlendEquation(BlendEquationMode.FuncAdd);
		}

		private Line RotateLine(Line stick, float rotationAngle)
		{
			var mtxRotation = Matrix2.CreateRotation(rotationAngle);
			Vector2 a;
			a.X = Vector2.Dot(mtxRotation.Column0, stick.Item1);
			a.Y = Vector2.Dot(mtxRotation.Column1, stick.Item1);
			Vector2 b;
			b.X = Vector2.Dot(mtxRotation.Column0, stick.Item2);
			b.Y = Vector2.Dot(mtxRotation.Column1, stick.Item2);
			return new Line(a, b);
		}

		private void DrawLine(Line stick)
		{
			GL.Begin(PrimitiveType.Lines);
			GL.Vertex2(stick.Item1);
			GL.Vertex2(stick.Item2);
			GL.End();
		}

		private void DrawAABB(Box2D rect)
		{
			GL.Begin(PrimitiveType.LineLoop);
			GL.Vertex2(rect.X, rect.Y);
			GL.Vertex2(rect.MaxX, rect.Y);
			GL.Vertex2(rect.MaxX, rect.MaxY);
			GL.Vertex2(rect.X, rect.MaxY);
			GL.End();
		}

		private void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.Color3(Color.CornflowerBlue);
			DrawLine(stick);

			GL.Color3(Color.YellowGreen);
			DrawAABB(stickAABB);

			GL.Color3(Color.Black);
			DrawAABB(Box2dExtensions.CreateFromCenterSize(0, 0, 0.02f, 0.02f));
		}

		private void Update(float updatePeriod)
		{
			float angle = -updatePeriod * 0.6f;

			stick = RotateLine(stick, angle);
			var minX = Math.Min(stick.Item1.X, stick.Item2.X);
			var maxX = Math.Max(stick.Item1.X, stick.Item2.X);
			var minY = Math.Min(stick.Item1.Y, stick.Item2.Y);
			var maxY = Math.Max(stick.Item1.Y, stick.Item2.Y);
			stickAABB = Box2dExtensions.CreateFromMinMax(minX, minY, maxX, maxY);
		}

		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			var visual = new Visual();
			app.Render += visual.Render;
			app.Update += visual.Update;
			app.Run();
		}
	}
}