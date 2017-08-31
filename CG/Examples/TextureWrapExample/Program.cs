using DMS.Application;
using DMS.Geometry;
using DMS.OpenGL;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace Example
{
	/// <summary>
	/// shows side scrolling setup by manipulating texture coordinates
	/// </summary>
	class MyVisual
	{
		private Texture texBackground;
		private Box2D texCoord = new Box2D(-1, -1, 3, 3);

		private MyVisual()
		{
			texBackground = TextureLoader.FromBitmap(Resourcen.mountains);
			//background clear color
			GL.ClearColor(Color.Black);
		}

		private void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			//color is multiplied with texture color white == no change
			GL.Color3(Color.White); //todo: i) change color
			//draw with different wrap modes - defines how texture coordinates outside of [0..1]² are handled
			texBackground.WrapMode(TextureWrapMode.ClampToBorder);
			DrawTexturedRect(new Box2D(-1, 0, 1, 1), texBackground, texCoord);
			texBackground.WrapMode(TextureWrapMode.Repeat);
			DrawTexturedRect(new Box2D(0, 0, 1, 1), texBackground, texCoord);
			texBackground.WrapMode(TextureWrapMode.ClampToEdge);
			DrawTexturedRect(new Box2D(-1, -1, 1, 1), texBackground, texCoord);
			texBackground.WrapMode(TextureWrapMode.MirroredRepeat);
			DrawTexturedRect(new Box2D(0, -1, 1, 1), texBackground, texCoord);
		}

		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			var visual = new MyVisual();
			app.Render += visual.Render;
			app.Run();
		}

		private static void DrawTexturedRect(Box2D rect, Texture tex, Box2D texCoords)
		{
			tex.Activate();
			rect.DrawTexturedRect(texCoords);
			tex.Deactivate();
		}
	}
}