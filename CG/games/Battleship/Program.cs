using Battleship;
using DMS.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Reversi
{
	class MyApplication
	{
		[STAThread]
		private static void Main()
		{
			MyApplication app = new MyApplication();
			app.gameWindow.Run();
		}

		public MyApplication()
		{
			gameWindow.Resize += GameWindow_Resize;
			gameWindow.KeyDown += (sender, e) => { if (Key.Escape == e.Key)	gameWindow.Exit(); };
			gameWindow.MouseDown += GameWindow_MouseDown;
			gameWindow.RenderFrame += GameWindow_RenderFrame;
			gameWindow.RenderFrame += (sender, e) => { gameWindow.SwapBuffers(); };
			GL.ClearColor(Color.Black);
			for (int x = 0; x < gridRes; ++x)
			{
				for (int y = 0; y < gridRes; ++y)
				{
					grid[x, y] = FieldType.EMPTY;
				}
			}
			texWhite = TextureLoader.FromBitmap(Resourcen.white);
			texBlack = TextureLoader.FromBitmap(Resourcen.black);
			texWater = TextureLoader.FromBitmap(Resourcen.water);
			font = new TextureFont(TextureLoader.FromBitmap(Resourcen.Fire_2), 10, 32, 1.0f, 0.9f, 0.5f);
		}

		private const int gridRes = 10;
		private enum FieldType { EMPTY, BLACK, WHITE };
		private FieldType[,] grid = new FieldType[gridRes, gridRes];
		private GameWindow gameWindow = new GameWindow(1024, 1024);
		private Matrix4 toClipSpace = new Matrix4();
		private TextureFont font;
		private Texture texWhite;
		private Texture texBlack;
		private Texture texWater;

		private void GameWindow_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.Button != MouseButton.Left) return;
			//calculate the grid coordinates
			var pos = new Vector4(2.0f * e.X / (gameWindow.Width - 1.0f) - 1.0f, -2.0f * e.Y / (gameWindow.Height - 1.0f) + 1.0f, 0.0f, 1.0f);
			var fromClipSpace = toClipSpace.Inverted();
			var gridPos = Vector4.Transform(pos, fromClipSpace);
			//do the move
			//Move((int)Math.Floor(gridPos.X), (int)Math.Floor(gridPos.Y));
		}

		private void GameWindow_Resize(object sender, EventArgs e)
		{
			var w = gameWindow.Width;
			var h = gameWindow.Height;

			GL.Viewport(0, 0, w, h);
			if (w >= h)
			{
				var emptyPart = (w / (float)h - 1.0f) * gridRes * 0.5f;
				toClipSpace = Matrix4.CreateOrthographicOffCenter(-emptyPart, emptyPart + gridRes, 0.0f, gridRes, 0.0f, 1.0f);
			}
			else
			{
				var emptyPart = (h / (float)w - 1.0f) * gridRes * 0.5f;
				toClipSpace = Matrix4.CreateOrthographicOffCenter(0.0f, gridRes, -emptyPart, emptyPart + gridRes, 0.0f, 1.0f);
			}
		}
		
		private void GameWindow_RenderFrame(object sender, FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			DrawField();
		}

		private void DrawField()
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			DrawSprite(0.0f, 0.0f, texWater, 1.0f, 1.0f);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref toClipSpace);
			GL.MatrixMode(MatrixMode.Modelview);

			//grid
			GL.Color3(Color.Black);
			GL.LineWidth(3.0f);
			GL.Begin(PrimitiveType.Lines);
			for (int i = 0; i < gridRes + 1; ++i)
			{
				GL.Vertex2(i, 0.0);
				GL.Vertex2(i, gridRes);
			}
			for (int i = 0; i < gridRes + 1; ++i)
			{
				GL.Vertex2(0.0, i);
				GL.Vertex2(gridRes, i);
			}
			GL.End();
			//chips
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			for (int x = 0; x < gridRes; ++x)
			{
				for (int y = 0; y < gridRes; ++y)
				{
					var type = grid[x, y];
					if (FieldType.EMPTY == type) continue;
					DrawSprite(x + 0.5f, y + 0.5f, FieldType.BLACK == type ? texBlack : texWhite, 0.45f);
				}
			}
			GL.Disable(EnableCap.Blend);
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