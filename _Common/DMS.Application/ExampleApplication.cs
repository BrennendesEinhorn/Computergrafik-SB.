using DMS.Base;
using DMS.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;
using System;

namespace DMS.Application
{
	public class ExampleApplication
	{
		public ExampleApplication(int width = 512, int height = 512, double updateRate = 60)
		{
			gameWindow = new GameWindow(width, height);
			gameWindow.TargetUpdateFrequency = updateRate;
			gameWindow.TargetRenderFrequency = updateRate;
			gameWindow.VSync = VSyncMode.On;
			//register callback for resizing of window
			gameWindow.Resize += GameWindow_Resize;
			//register callback for keyboard
			gameWindow.KeyDown += GameWindow_KeyDown;
			gameWindow.KeyDown += (sender, e) => { if (Key.Escape == e.Key) gameWindow.Exit(); };
			ResourceManager = new ResourceManager();
		}

		public IGameWindow GameWindow { get { return gameWindow; } }

		public event Action Render;

		public delegate void ResizeHandler(int width, int height);
		public event ResizeHandler Resize;

		public delegate void UpdateHandler(float updatePeriod);
		public event UpdateHandler Update;

		public bool IsRecording
		{
			get { return !ReferenceEquals(null, frameListCreator); }
			set
			{
				if (!ReferenceEquals(null, frameListCreator)) return;
				frameListCreator = value ? new FrameListCreator(gameWindow.Width, gameWindow.Height) : null;
			}
		}

		public ResourceManager ResourceManager { get; private set; }

		public Vector2 CalcNormalized(int pixelX, int pixelY)
		{
			return new Vector2(pixelX / (gameWindow.Width - 1f), 1f - pixelY / (gameWindow.Height - 1f));
		}

		public void Run()
		{
			//register a callback for updating the game logic
			gameWindow.UpdateFrame += (sender, e) => Update?.Invoke((float)gameWindow.TargetUpdatePeriod);
			//registers a callback for drawing a frame
			gameWindow.RenderFrame += (sender, e) => GameWindowRender();
			//run the update loop, which calls our registered callbacks
			gameWindow.Run();
		}

		private GameWindow gameWindow;
		private FrameListCreator frameListCreator;

		private void GameWindowRender()
		{
			ResourceManager?.CheckForShaderChange();
			//record frame
			frameListCreator?.Activate();
			//render
			Render?.Invoke();
			//stop recording frame
			frameListCreator?.Deactivate();
			//buffer swap of double buffering (http://gameprogrammingpatterns.com/double-buffer.html)
			gameWindow.SwapBuffers();
			//save frame
			frameListCreator?.SaveFrame();
		}

		private void GameWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Escape:
					if (!ReferenceEquals(null, frameListCreator)) frameListCreator.Frames.SaveToDefaultDir();
					gameWindow.Exit();
					break;
				case Key.F11:
					gameWindow.WindowState = WindowState.Fullscreen == gameWindow.WindowState ? WindowState.Normal : WindowState.Fullscreen;
					break;
			}
		}

		private void GameWindow_Resize(object sender, EventArgs e)
		{
			GL.Viewport(0, 0, gameWindow.Width, gameWindow.Height);
			if (!ReferenceEquals(null, frameListCreator))
			{
				frameListCreator.Dispose();
				frameListCreator = new FrameListCreator(gameWindow.Width, gameWindow.Height);
			}
			Resize?.Invoke(gameWindow.Width, gameWindow.Height);
		}
	}
}
