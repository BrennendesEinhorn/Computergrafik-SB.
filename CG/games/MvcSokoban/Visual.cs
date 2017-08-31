using DMS.OpenGL;
using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace MvcSokoban
{
	class Visual
	{
		public Visual()
		{
			//this.spriteSheet = new SpriteSheet(TextureLoader.FromBitmap(Resourcen.skin36), 4, 0.98f, 0.97f);
			this.spriteSheet = new SpriteSheet(TextureLoader.FromBitmap(Resourcen.borgar), 4, 0.95f, 0.95f);
		}

		public void DrawScreen(ILevel level)
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0.0, level.Width, 0.0, level.Height, 0.0, 1.0);
			GL.MatrixMode(MatrixMode.Modelview);

			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.LoadIdentity();
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			spriteSheet.Activate();
			GL.Color3(Color.White);
			for (int x = 0; x < level.Width; ++x)
			{
				for (int y = 0; y < level.Height; ++y)
				{
					spriteSheet.Draw((uint)level.GetElement(x, y), new Box2D(x, y, 1.0f, 1.0f));
				}
			}

			spriteSheet.Deactivate();
			GL.Disable(EnableCap.Blend);
		}

		public void ResizeWindow(int width, int height)
		{
			GL.Viewport(0, 0, width, height);
		}

		private readonly SpriteSheet spriteSheet;
	}
}
