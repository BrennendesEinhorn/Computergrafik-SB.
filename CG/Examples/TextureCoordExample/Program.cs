using DMS.Application;
using DMS.OpenGL;
using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;

namespace Example
{
	/// <summary>
	/// shows side scrolling setup by manipulating texture coordinates
	/// </summary>
	class MyVisual
	{
		private Texture texBackground;
		private Texture texPlayer;
		private Box2D texCoord = new Box2D(0, 0, 0.3f, 1);

		private MyVisual()
		{
			texPlayer = TextureLoader.FromBitmap(Resourcen.bird1);
			//two landscape resources are available in the Resourcen.resx file
			texBackground = TextureLoader.FromBitmap(Resourcen.forest);
			//set how texture coordinates outside of [0..1] are handled
			texBackground.WrapMode(TextureWrapMode.MirroredRepeat);
			//background clear color
			GL.ClearColor(Color.White);
			//for transparency in textures
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}

		private void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			//color is multiplied with texture color white == no change
			GL.Color3(Color.White);
			//draw background with changing texture coordinates
			DrawTexturedRect(new Box2D(-1, -1, 2, 2), texBackground, texCoord);

			GL.Enable(EnableCap.Blend); // enable transparency

			DrawTexturedRect(new Box2D(-.25f, -.1f, .2f, .2f), texPlayer, new Box2D(0, 0, 1, 1)); // draw player
			GL.Disable(EnableCap.Blend); // disable transparency
		}

		private void Update(float updatePeriod)
		{
			texCoord.X += updatePeriod * 0.1f; //scroll texture coordinates
		}

		private static void DrawTexturedRect(Box2D rect, Texture tex, Box2D texCoords)
		{
			tex.Activate();
			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(texCoords.X, texCoords.Y); GL.Vertex2(rect.X, rect.Y);
			GL.TexCoord2(texCoords.MaxX, texCoords.Y); GL.Vertex2(rect.MaxX, rect.Y);
			GL.TexCoord2(texCoords.MaxX, texCoords.MaxY); GL.Vertex2(rect.MaxX, rect.MaxY);
			GL.TexCoord2(texCoords.X, texCoords.MaxY); GL.Vertex2(rect.X, rect.MaxY);
			GL.End();
			tex.Deactivate();
		}

		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			var visual = new MyVisual();
			app.Render += visual.Render;
			app.Update += visual.Update;
			app.Run();
		}
	}
}