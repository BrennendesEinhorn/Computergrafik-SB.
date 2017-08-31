using DMS.Application;
using DMS.Base;
using OpenTK.Input;
using System;
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
			app.GameWindow.MouseMove += (s, e) =>
			{
				if (ButtonState.Pressed == e.Mouse.LeftButton)
				{
					visual.CameraAzimuth += 300 * e.XDelta / (float)app.GameWindow.Width;
					visual.CameraElevation += 300 * e.YDelta / (float)app.GameWindow.Height;
				}
			};
			app.GameWindow.MouseWheel += (s, e) => visual.CameraDistance *= (float)Math.Pow(1.05, e.DeltaPrecise);
			app.Update += visual.Update;
			app.Render += visual.Render;
			app.Run();
		}

		private static void LoadResources(ResourceManager resourceManager)
		{
			var dir = Path.GetDirectoryName(PathTools.GetSourceFilePath()) + @"\Resources\";
			resourceManager.AddShader(MainVisual.ShaderName, dir + "vertex.glsl", dir + "fragment.glsl"
				, Resourcen.vertex, Resourcen.fragment);
		}
	}
}