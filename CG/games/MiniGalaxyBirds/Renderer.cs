using DMS.OpenGL;
using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace MiniGalaxyBirds
{
	public class Renderer : IRenderer
	{
		public Renderer() { }

		public void Register(string type, Texture texture)
		{
			this.registeredTypes[type] = texture;
		}

		public void RegisterFont(TextureFont textureFont)
		{
			this.font = textureFont;
		}

		public void ResizeWindow(int width, int height)
		{
			float deltaX = 0.5f * ((width / (float)height) - 1.0f);
			GL.Viewport(0, 0, width, height);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(-deltaX, 1.0 + deltaX, 0.0, 1.0, 0.0, 1.0);
			GL.MatrixMode(MatrixMode.Modelview);
		}

		public IDrawable CreateDrawable(string type, Box2D frame)
		{
			Texture tex;
			if (this.registeredTypes.TryGetValue(type, out tex))
			{
				IDrawable drawable = new Sprite(tex, frame);
				drawables.Add(drawable);
				return drawable;
			}
			throw new Exception("Unregisterd type " + type.ToString());
		}

		public IDrawable CreateDrawable(string type, Box2D frame, IAnimation animation)
		{
			Texture tex;
			if (this.registeredTypes.TryGetValue(type, out tex))
			{
				IDrawable drawable = new AnimatedSprite(tex, frame, animation);
				drawables.Add(drawable);
				return drawable;
			}
			throw new Exception("Unregisterd type " + type.ToString());
		}

		public void DeleteDrawable(IDrawable drawable)
		{
			drawables.Remove(drawable);
		}

		public void DrawScreen(Box2D clipFrame, uint points)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.LoadIdentity();
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			if (!ReferenceEquals(null,  clipFrame))
			{
				foreach (IDrawable drawable in drawables)
				{
					if (clipFrame.Intersects(drawable.Rect))
					{
						drawable.Draw();
					}
				}
			}
			else
			{
				foreach (IDrawable drawable in drawables)
				{
					drawable.Draw();
				}
			}
			Print(-0.15f, 0.0f, 0.0f, 0.04f, points.ToString());
			GL.Disable(EnableCap.Blend);
		}

		public void Print(float x, float y, float z, float size, string text)
		{
			if (ReferenceEquals(null,  font))
			{
				throw new Exception("No font registered!");
			}
			font.Print(x, y, z, size, text);
		}

		private readonly Dictionary<string, Texture> registeredTypes = new Dictionary<string, Texture>();
		private readonly HashSet<IDrawable> drawables = new HashSet<IDrawable>();

		private TextureFont font = null;
	}
}
