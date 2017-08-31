using DMS.Geometry;
using DMS.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;

namespace Reversi
{
	public class Visual
	{
		public Visual()
		{
			GL.ClearColor(Color.DarkGreen);
			texWhite = TextureLoader.FromBitmap(Resourcen.white);
			texBlack = TextureLoader.FromBitmap(Resourcen.black);
			texTable = TextureLoader.FromBitmap(Resourcen.pool_table);
		}

		public void Resize(IGameState gameState, int width, int height)
		{
			var size = Math.Max(gameState.GridWidth, gameState.GridHeight);
			aspect = width / (float)height;

			if (width >= height)
			{
				var emptyPart = (width / (float)height - 1.0f) * size * 0.5f;
				toClipSpace = Matrix4.CreateOrthographicOffCenter(-emptyPart, emptyPart + size, 0.0f, size, 0.0f, 1.0f);
			}
			else
			{
				var emptyPart = (height / (float)width - 1.0f) * size * 0.5f;
				toClipSpace = Matrix4.CreateOrthographicOffCenter(0.0f, size, -emptyPart, emptyPart + size, 0.0f, 1.0f);
			}
		}

		public Point CalcGridPosFromNormalized(Vector2 coord)
		{
			//calculate the grid coordinates
			var pos = new Vector4(2.0f * coord.X - 1.0f, 2.0f * coord.Y - 1.0f, 0.0f, 1.0f);
			var fromClipSpace = toClipSpace.Inverted();
			var gridPos = Vector4.Transform(pos, fromClipSpace);
			return new Point((int)Math.Floor(gridPos.X), (int)Math.Floor(gridPos.Y));
		}

		public void Render(IGameState gameState)
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref toClipSpace);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.Clear(ClearBufferMask.ColorBufferBit);
			DrawField(gameState);
			//which player moves?
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Color4(1.0, 1.0, 1.0, 1.0);
			GL.Disable(EnableCap.Blend);
		}

		public void PrintMessage(string message)
		{
			GL.MatrixMode(MatrixMode.Projection);
			var mtxAspect = Matrix4.CreateOrthographic(1 * aspect, 1, 0, 1);
			GL.LoadMatrix(ref mtxAspect);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Color3(Color.White);
			var size = 0.1f;
			font.Print(-0.5f * font.Width(message, size), 0, 0, size, message);
			GL.Disable(EnableCap.Blend);
		}

		private Matrix4 toClipSpace = new Matrix4();
		private TextureFont font = new TextureFont(TextureLoader.FromBitmap(Resourcen.Fire_2), 10, 32, 1.0f, 0.9f, 0.5f);
		private Texture texWhite;
		private Texture texBlack;
		private Texture texTable;
		private float aspect;

		private void DrawField(IGameState gameState)
		{
			//background
			var field = new Box2D(0, 0, gameState.GridWidth, gameState.GridHeight);
			texTable.Activate();
			field.DrawTexturedRect(new Box2D(0, 0, 8, 8));
			texTable.Deactivate();
			//grid
			GL.Color3(Color.Black);
			GL.LineWidth(3.0f);
			GL.Begin(PrimitiveType.Lines);
			for (int i = 0; i <= gameState.GridWidth; ++i)
			{
				GL.Vertex2(i, 0.0);
				GL.Vertex2(i, gameState.GridHeight);
			}
			for (int i = 0; i <= gameState.GridHeight; ++i)
			{
				GL.Vertex2(0.0, i);
				GL.Vertex2(gameState.GridWidth, i);
			}
			GL.End();
			//chips
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			for (int x = 0; x < gameState.GridWidth; ++x)
			{
				for (int y = 0; y < gameState.GridHeight; ++y)
				{
					var type = gameState[x, y];
					if (FieldType.EMPTY == type) continue;
					DrawSprite(x + 0.5f, y + 0.5f, FieldType.BLACK == type ? texBlack : texWhite, 0.45f);
				}
			}
			GL.Disable(EnableCap.Blend);
			DrawSelection(gameState.LastMoveX, gameState.LastMoveY);
		}

		static void DrawSelection(int x_, int y_)
		{
			float x = x_ + 0.5f;
			float y = y_ + 0.5f;
			float radius = 0.48f;
			GL.Color3(Color.Blue);
			GL.LineWidth(4.0f);
			GL.Begin(PrimitiveType.LineLoop);
			GL.Vertex2(x - radius, y - radius);
			GL.Vertex2(x + radius, y - radius);
			GL.Vertex2(x + radius, y + radius);
			GL.Vertex2(x - radius, y + radius);
			GL.End();
		}

		static void DrawSprite(float x, float y, Texture tex, float radius = 0.5f, float repeat = 1.0f)
		{
			tex.Activate();
			GL.Color3(Color.White);
			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(x - radius, y - radius);
			GL.TexCoord2(repeat, 0.0f); GL.Vertex2(x + radius, y - radius);
			GL.TexCoord2(repeat, repeat); GL.Vertex2(x + radius, y + radius);
			GL.TexCoord2(0.0f, repeat); GL.Vertex2(x - radius, y + radius);
			GL.End();
			tex.Deactivate();
		}
	}
}
