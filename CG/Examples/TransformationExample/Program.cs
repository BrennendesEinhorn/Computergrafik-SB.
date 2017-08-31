using DMS.OpenGL;
using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using DMS.Application;

namespace Example
{
	class MyVisual
	{
		private Texture texBird;
		private Vector2 rotCenter = new Vector2(-.9f, 0);
		private List<Box2D> birds = new List<Box2D>();

		private MyVisual()
		{
			//setup
			texBird = TextureLoader.FromBitmap(Resourcen.bird1);
			//background clear color
			GL.ClearColor(Color.White);
			//for transparency in textures
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Enable(EnableCap.Blend); // for transparency in textures
			//generate birds
			for (float delta = .1f; delta < .5f; delta += .1f)
			{
				birds.Add(Box2dExtensions.CreateFromCenterSize(rotCenter.X - delta, rotCenter.Y - delta, .1f, .1f));
				birds.Add(Box2dExtensions.CreateFromCenterSize(rotCenter.X - delta, rotCenter.Y + delta, .1f, .1f));
				birds.Add(Box2dExtensions.CreateFromCenterSize(rotCenter.X + delta, rotCenter.Y - delta, .1f, .1f));
				birds.Add(Box2dExtensions.CreateFromCenterSize(rotCenter.X + delta, rotCenter.Y + delta, .1f, .1f));
			}
		}

		private void Update(float updatePeriod)
		{
			rotCenter.X += updatePeriod * 0.1f;
			var t = Transformation2D.CreateRotationAround(rotCenter.X, rotCenter.Y, updatePeriod * 200f);
			foreach (var bird in birds)
			{
				bird.TransformCenter(t);
			}
		}

		private void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			foreach (var bird in birds)
			{
				DrawTexturedRect(bird, texBird);
			}
		}

		private static void DrawTexturedRect(Box2D Rectangle, Texture tex)
		{
			GL.Color3(Color.White);
			tex.Activate();
			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(Rectangle.X, Rectangle.Y);
			GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(Rectangle.MaxX, Rectangle.Y);
			GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(Rectangle.MaxX, Rectangle.MaxY);
			GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(Rectangle.X, Rectangle.MaxY);
			GL.End();
			tex.Deactivate();
		}

		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			var visual = new MyVisual();
			app.Update += visual.Update;
			app.Render += visual.Render;
			app.Run();
		}
	}
}