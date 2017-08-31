using DMS.OpenGL;
using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;
using System.Drawing;
using DMS.Application;

namespace Example
{
	class MyVisual
	{
		private SpriteSheetAnimation explosion;
		private AnimationTextures alienShip;
		private Stopwatch timeSource = new Stopwatch();

		private MyVisual()
		{
			//animation using a single SpriteSheet
			explosion = new SpriteSheetAnimation(new SpriteSheet(TextureLoader.FromBitmap(Resourcen.explosion), 5), 0, 24, 1);
			//animation using a bitmap for each frame
			alienShip = new AnimationTextures(.5f);
			//art from http://millionthvector.blogspot.de/p/free-sprites.html
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10001));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10002));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10003));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10004));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10005));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10006));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10007));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10008));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10009));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10010));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10011));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10012));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10013));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10014));
			alienShip.AddFrame(TextureLoader.FromBitmap(Resourcen.alien10015));

			//for transparency in textures
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			//start game time
			timeSource.Start();
		}

		private void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);

			//color is multiplied with texture color white == no change
			GL.Color3(Color.White);

			GL.Enable(EnableCap.Blend); // for transparency in textures
			explosion.Draw(new Box2D(-.7f, -.2f, .4f, .4f), (float)timeSource.Elapsed.TotalSeconds);

			alienShip.Draw(new Box2D(.3f, -.2f, .4f, .4f), (float)timeSource.Elapsed.TotalSeconds);
			GL.Disable(EnableCap.Blend); // for transparency in textures
		}

		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			//run the update loop, which calls our registered callbacks
			var visual = new MyVisual();
			app.Render += visual.Render;
			app.Run();
		}
	}
}