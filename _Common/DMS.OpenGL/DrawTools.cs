using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DMS.OpenGL
{
	public static class DrawTools
	{
		public static void DrawTexturedRect(this Box2D rect, Box2D texCoords)
		{
			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(texCoords.X, texCoords.Y); GL.Vertex2(rect.X, rect.Y);
			GL.TexCoord2(texCoords.MaxX, texCoords.Y); GL.Vertex2(rect.MaxX, rect.Y);
			GL.TexCoord2(texCoords.MaxX, texCoords.MaxY); GL.Vertex2(rect.MaxX, rect.MaxY);
			GL.TexCoord2(texCoords.X, texCoords.MaxY); GL.Vertex2(rect.X, rect.MaxY);
			GL.End();
		}
		public static Vector3 ToOpenTK(this global::System.Numerics.Vector3 v)
		{
			return new Vector3(v.X, v.Y, v.Z);
		}
		public static Matrix4 ToOpenTK(this global::System.Numerics.Matrix4x4 m)
		{
			return new Matrix4(m.M11, m.M12, m.M13, m.M14,
				m.M21, m.M22, m.M23, m.M24,
				m.M31, m.M32, m.M33, m.M34,
				m.M41, m.M42, m.M43, m.M44);
		}
	}
}
