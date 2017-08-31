using OpenTK;
using System;
using System.Linq;
using System.Windows.Forms;
using OpenTK.Input;
using System.IO;
using ShaderForm.Visual;
using ShaderForm.Demo;
using ShaderForm.DemoModelFactory;

namespace DemoRecorder
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
		private string saveDirectory;
		private int fileNumber;
		private int frameRate;

		private MyApplication()
		{
			gameWindow.KeyDown += GameWindow_KeyDown;
			gameWindow.RenderFrame += game_RenderFrame;
			visualContext = new VisualContext();
			var textures = new Textures(visualContext);
			var shaders = new Shaders(NewShaderFile);
			demo = new DemoModel(visualContext, shaders, textures, false);
			demo.TimeSource.TimeFinished += () => gameWindow.Close();

			var arguments = Environment.GetCommandLineArgs();
			if (3 > arguments.Length)
			{
				MessageBox.Show("DemoRecorder <configfile> <saveDirectory> [<resX> <resY> <frameRate>]"
					+ Environment.NewLine 
					+ " Please give the demo config file name as application parameter followed by the render buffer resolution.");
				gameWindow.Close();
			}
			bufferWidth = gameWindow.Width;
			bufferHeight = gameWindow.Height;
			try
			{
				bufferWidth = int.Parse(arguments.ElementAt(3));
				bufferHeight = int.Parse(arguments.ElementAt(4));
			}
			catch
			{
				bufferWidth = gameWindow.Width;
				bufferHeight = gameWindow.Height;
			}
			try
			{
				frameRate = int.Parse(arguments.ElementAt(5));
			}
			catch
			{
				frameRate = 25;
			}
			try
			{
				DemoLoader.LoadFromFile(demo, arguments.ElementAt(1));
				saveDirectory = Directory.CreateDirectory(arguments.ElementAt(2)).FullName;
				saveDirectory += Path.DirectorySeparatorChar;
				saveDirectory += DateTime.Now.ToString("yyyyMMdd HHmmss");
				saveDirectory += Path.DirectorySeparatorChar;
				Directory.CreateDirectory(saveDirectory);
				fileNumber = 0;
			}
			catch (Exception e)
			{
				MessageBox.Show("Error loading demo '" + arguments.ElementAt(1) + '"'
					+ Environment.NewLine + e.Message);
				gameWindow.Close();
			}
		}

		private void Run()
		{
			gameWindow.VSync = VSyncMode.Off;
			gameWindow.Run(200.0);
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

		private void game_RenderFrame(object sender, FrameEventArgs e)
		{
			demo.UpdateBuffer(0, 0, 0, bufferWidth, bufferHeight);
			demo.Draw(gameWindow.Width, gameWindow.Height);
			demo.SaveBuffer(saveDirectory + fileNumber.ToString("00000") + ".png");
			gameWindow.SwapBuffers();
			++fileNumber;
			demo.TimeSource.Position += 1.0f / frameRate; //step 1/25 of a second
		}
	}
}