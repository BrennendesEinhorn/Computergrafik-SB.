using OpenTK;
using System;
using System.Linq;
using System.Windows.Forms;
using OpenTK.Input;
using ShaderForm.Visual;
using ShaderForm.Demo;
using ShaderForm.DemoModelFactory;

namespace DemoPlayer
{
	class MyApplication
	{
		[STAThread]
		private static void Main()
		{
			var app = new MyApplication();
			app.Run();
		}

		private GameWindow gameWindow = new GameWindow();
		private VisualContext visualContext;
		private DemoModel demo;
		private int bufferWidth;
		private int bufferHeight;

		private void Run()
		{
			demo.TimeSource.IsRunning = true;
			gameWindow.Run(60.0);
		}

		private MyApplication()
		{
			gameWindow.Load += GameWindow_Load;
			gameWindow.KeyDown += GameWindow_KeyDown;
			gameWindow.RenderFrame += game_RenderFrame;
			visualContext = new VisualContext();
			var textures = new Textures(visualContext);
			var shaders = new Shaders(NewShaderFile);
			demo = new DemoModel(visualContext, shaders, textures, false);
			demo.TimeSource.TimeFinished += () => gameWindow.Close();

			var arguments = Environment.GetCommandLineArgs();
			if (1 == arguments.Length)
			{
				MessageBox.Show("DemoPlayer <configfile> [<resX> <resY>]" 
					+ Environment.NewLine 
					+ " Please give the demo config file name you wish to play as application parameter followed by the render buffer resolution.");
				gameWindow.Close();
			}
			bufferWidth = gameWindow.Width;
			bufferHeight = gameWindow.Height;
			try
			{
				bufferWidth = int.Parse(arguments.ElementAt(2));
				bufferHeight = int.Parse(arguments.ElementAt(3));
			}
			catch
			{
				bufferWidth = gameWindow.Width;
				bufferHeight = gameWindow.Height;
			}
			try
			{
				DemoLoader.LoadFromFile(demo, arguments.ElementAt(1));
			}
			catch (Exception e)
			{
				MessageBox.Show("Error loading demo '" + arguments.ElementAt(1) + '"'
					+ Environment.NewLine + e.Message);
				gameWindow.Close();
			}
		}

	private void GameWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (Key.Escape == e.Key)
			{
				gameWindow.Close();
			}
		}

		private IShaderFile NewShaderFile()
		{
			return new ShaderFile(visualContext);
		}

		void GameWindow_Load(object sender, EventArgs e)
		{
			//fullscreen
			gameWindow.WindowBorder = WindowBorder.Hidden;
			gameWindow.WindowState = WindowState.Fullscreen;
			gameWindow.VSync = VSyncMode.On;
		}

		private void game_RenderFrame(object sender, FrameEventArgs e)
		{
			demo.UpdateBuffer(0, 0, 0, bufferWidth, bufferHeight);
			demo.Draw(gameWindow.Width, gameWindow.Height);
			gameWindow.SwapBuffers();
		}
	}
}