using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Example
{
	public class Canvas
	{
		public Canvas()
		{
			GL.ClearColor(Color.White);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.PolygonSmooth);
		}

		public void Draw()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.Color3((byte)55, (byte)96, (byte)146);
			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2(-0.7, 0.7);
			GL.Vertex2(0.7, 0.2);
			GL.Vertex2(0, -0.7);
			GL.End();
		}
	}
}
