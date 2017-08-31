using DMS.OpenGL;
using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders
{
	public class Visual
	{
		public Visual()
		{
			texPlayer = TextureLoader.FromBitmap(Resourcen.blueships1);
			texEnemy = TextureLoader.FromBitmap(Resourcen.redship4);
			texBullet = TextureLoader.FromBitmap(Resourcen.blueLaserRay);
		}

        public void DrawScreen(IEnumerable<Box2D> enemies, IEnumerable<Box2D> bullets, Box2D player)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.LoadIdentity();
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			texEnemy.Activate();
			foreach (Box2D enemy in enemies)
			{
				Draw(enemy);
			}
			texEnemy.Deactivate();
			texBullet.Activate();
			foreach (Box2D bullet in bullets)
			{
				Draw(bullet);
			}
			texBullet.Deactivate();
			texPlayer.Activate();
			Draw(player);
			texPlayer.Deactivate();
			GL.Disable(EnableCap.Blend);
		}

		private readonly Texture texEnemy;
		private readonly Texture texPlayer;
		private readonly Texture texBullet;

		private void Draw(Box2D Rectanlge)
		{
			GL.Color3(Color.White);
			GL.Begin(PrimitiveType.Quads);
				GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(Rectanlge.X, Rectanlge.Y);
				GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(Rectanlge.MaxX, Rectanlge.Y);
				GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(Rectanlge.MaxX, Rectanlge.MaxY);
				GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(Rectanlge.X, Rectanlge.MaxY);
			GL.End();
		}
	}
}
