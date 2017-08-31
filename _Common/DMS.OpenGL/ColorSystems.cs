using DMS.Geometry;
using System.Numerics;
	using SysColor = System.Drawing.Color;

namespace DMS.OpenGL
{
	public static class ColorSystems
	{
		/// <summary>
		/// Converts hsb (Hue, Saturation and Brightness) color value into rgb
		/// </summary>
		/// <param name="h">Hue</param>
		/// <param name="s">Saturation</param>
		/// <param name="b">Brightness</param>
		/// <returns>rgb color</returns>
		public static Vector3 Hsb2rgb(float h, float s, float b)
		{
			s = MathHelper.Clamp(s, 0, 1);
			b = MathHelper.Clamp(b, 0, 1);
			var v3 = new Vector3(3.0f);
			var i = h * 6.0f;
			var j = new Vector3(i, i + 4.0f, i + 2.0f).Mod(6.0f);
			var k = Vector3.Abs(j - v3);
			var l = k - Vector3.One;
			var rgb = l.Clamp(0.0f, 1.0f);
			return b * Vector3.Lerp(Vector3.One, rgb, s);
		}

		public static global::System.Drawing.Color ToSystemColor(this Vector3 color)
		{
			color *= 255;
			return global::System.Drawing.Color.FromArgb((int)color.X, (int)color.Y, (int)color.Z);
		} 
	}
}
