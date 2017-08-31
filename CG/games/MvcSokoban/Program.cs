using OpenTK;
using OpenTK.Input;
using System;

namespace MvcSokoban
{
	class MyApplication
	{
		[STAThread]
		private static void Main()
		{
			MyApplication app = new MyApplication();
		}

		public MyApplication()
		{
			gameWindow = new GameWindow(800, 800);
			visual = new Visual();
			gameWindow.Load += GameWindow_Load;
			gameWindow.Resize += GameWindow_Resize;
			gameWindow.KeyDown += GameWindow_KeyDown;
			gameWindow.UpdateFrame += GameWindow_UpdateFrame;
			gameWindow.RenderFrame += GameWindow_RenderFrame;
			gameWindow.RenderFrame += (sender, e) => { gameWindow.SwapBuffers(); };
			LoadLevel();
			gameWindow.Run(60.0);
		}

		private uint levelNr = 1;

		private void LoadLevel()
		{
			var levelString = Resourcen.ResourceManager.GetString("level" + levelNr.ToString());
			var level = LevelLoader.FromString(levelString);
			if (ReferenceEquals(null, level)) return;
			logic = new GameLogic(level);
		}

		private void GameWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Escape: gameWindow.Exit(); break;
				case Key.R: LoadLevel(); break;
				case Key.N: ++levelNr; LoadLevel(); break;
				case Key.Left: logic.Update(GameLogic.Movement.LEFT); break;
				case Key.Right: logic.Update(GameLogic.Movement.RIGHT); break;
				case Key.Up: logic.Update(GameLogic.Movement.UP); break;
				case Key.Down: logic.Update(GameLogic.Movement.DOWN); break;
			};
		}

		private void GameWindow_Load(object sender, EventArgs e)
		{
			//gameWindow.WindowState = WindowState.Fullscreen;
			gameWindow.VSync = VSyncMode.On;
		}

		private void GameWindow_Resize(object sender, EventArgs e)
		{
			visual.ResizeWindow(gameWindow.Width, gameWindow.Height);
		}

		private void GameWindow_UpdateFrame(object sender, FrameEventArgs e)
		{
			float updatePeriod = (float)gameWindow.UpdatePeriod;
			if (logic.GetLevel().IsWon())
			{
				++levelNr;
				LoadLevel();
			}
		}

		private void GameWindow_RenderFrame(object sender, FrameEventArgs e)
		{
			visual.DrawScreen(logic.GetLevel());
		}

		private GameWindow gameWindow;
		private GameLogic logic;
		private Visual visual;
	}
}