using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Raytracer
{
	class Visual
	{
		public int m_iMultiSamples = 1;

		public Color renderPixel(Scene scene, Camera cam, float x_, float y_)
		{
			if (1 == m_iMultiSamples)
			{
				return RayTracer.TraceRay(cam.Pos, cam.PerspectiveRayDir(x_, y_), scene, 0);
			}
			Color color = Color.Black();
			float delta = 1.0f / ((float)Math.Sqrt(m_iMultiSamples));
			int count = 0;
			for (float x = x_ - 0.5f; x < x_ + 0.5f; x += delta)
			{
				for (float y = y_ - 0.5f; y < y_ + 0.5f; y += delta)
				{
					color += RayTracer.TraceRay(cam.Pos, cam.PerspectiveRayDir(x, y), scene, 0);
					++count;
				}
			}
			return color * (1.0f / m_iMultiSamples);
		}

		public void renderImage(Scene scene, Camera cam, Action<int, int, Color> setPixel)
		{
			PointF[] pixels = createPoints(cam.ViewportWidth, cam.ViewportHeight);
			pixels.Shuffle();
			foreach (PointF pixel in pixels)
			{
				Color color = renderPixel(scene, cam, pixel.X, pixel.Y);
				setPixel(Convert.ToInt32(pixel.X), Convert.ToInt32(pixel.Y), color);
			}
		}

		private Random m_rnd = new Random();

		private static PointF[] createJitteredPoints(int width, int height)
		{
			Random rnd = new Random();
			PointF[] pixels = new PointF[width * height];
			int i = 0;
			for (int y = 0; y < height; ++y)
			{
				for (int x = 0; x < width; ++x)
				{
					double deltaX = (rnd.NextDouble() - 0.5) * 0.7;
					double deltaY = (rnd.NextDouble() - 0.5) * 0.7;
					pixels[i] = new PointF(x + (float)deltaX, y + (float)deltaY);
					++i;
				}
			}
			return pixels;
		}

		private static PointF[] createPoints(int width, int height)
		{
			PointF[] pixels = new PointF[width * height];
			int i = 0;
			for (int y = 0; y < height; ++y)
			{
				for (int x = 0; x < width; ++x)
				{
					pixels[i] = new PointF(x, y);
					++i;
				}
			}
			return pixels;
		}
	}
}
