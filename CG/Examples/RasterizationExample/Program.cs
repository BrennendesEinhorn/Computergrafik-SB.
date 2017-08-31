using DMS.Application;
using DMS.OpenGL;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Example
{
	class MyApplication
	{
		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			var canvas = new Canvas();
			var rasterizer = new Rasterizer(10, 10, canvas.Draw);
			app.Render += rasterizer.Render;
			//app.Render += () => App_Render(app.GameWindow.Width, app.GameWindow.Height);
			app.Run();
		}

		private static void App_Render(int width, int height)
		{
			var format = System.Drawing.Imaging.PixelFormat.Format24bppRgb;
			var bmp = new Bitmap(width, height);
			BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, format);
			GL.ReadPixels(0, 0, width, height, TextureLoader.SelectPixelFormat(format), PixelType.UnsignedByte, data.Scan0);
			bmp.UnlockBits(data);
			bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
			Clipboard.SetImage(bmp);
			bmp.Save(@"d:\test.png");
		}
	}
}