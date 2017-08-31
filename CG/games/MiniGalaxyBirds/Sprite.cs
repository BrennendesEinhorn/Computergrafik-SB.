using DMS.OpenGL;
using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace MiniGalaxyBirds
{
	public class Sprite : IDrawable
	{
		public Sprite(Texture tex, Box2D extents)
		{
			this.tex = tex;
			this.Rect = extents;
		}

		public void Draw()
		{
			tex.Activate();
			GL.Begin(PrimitiveType.Quads);
			GL.Color3(Color.White);
			GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(Rect.X, Rect.Y);
			GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(Rect.MaxX, Rect.Y);
			GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(Rect.MaxX, Rect.MaxY);
			GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(Rect.X, Rect.MaxY);
			GL.End();
			tex.Deactivate();
		}

		public Box2D Rect { get; private set; }
		private Texture tex;
	}
}
