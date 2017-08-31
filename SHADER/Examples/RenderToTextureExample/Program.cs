using DMS.Application;
using DMS.Base;
using OpenTK.Input;
using System;
using System.Diagnostics;
using System.IO;

namespace Example
{
	class Controller
	{
		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			var visual = new MainVisual();
			app.ResourceManager.ShaderChanged += visual.ShaderChanged;
			LoadResources(app.ResourceManager);

			Stopwatch globalTime = new Stopwatch();
			bool doPostProcessing = false;

			app.Render += () =>
			{
				float time = (float)globalTime.Elapsed.TotalSeconds;
				if (doPostProcessing)
				{
					visual.DrawWithPostProcessing(time);
				}
				else
				{
					visual.Draw();
				}
			};

			app.Update += (t) => doPostProcessing = !Keyboard.GetState()[Key.Space];
			app.Resize += visual.Resize;
			app.GameWindow.ConnectEvents(visual.OrbitCamera);

			globalTime.Start();
			app.Run();
		}

		private static void LoadResources(ResourceManager resourceManager)
		{
			var dir = Path.GetDirectoryName(PathTools.GetSourceFilePath()) + "/Resources/";
			resourceManager.AddShader(MainVisual.ShaderName, dir + "vertex.vert", dir + "fragment.frag", Resources.vertex, Resources.fragment);
			//change fragment shader to switch between post-processing methods
			resourceManager.AddShader(MainVisual.ShaderPostProcessName, dir + "vertexPostProcess.vert", dir + "Grayscale.glsl", null, null);
		}
	}
}