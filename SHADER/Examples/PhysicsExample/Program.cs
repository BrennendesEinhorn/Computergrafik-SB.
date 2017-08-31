using DMS.Application;
using System;
using System.Text;

namespace Example
{
	class Controller
	{
		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			var model = new Model();
			var visual = new MainVisual();
			app.ResourceManager.ShaderChanged += visual.ShaderChanged;
			LoadResources(app.ResourceManager);
			app.Render += () => visual.Render(model.Bodies);
			app.Update += model.Update;
			app.GameWindow.ConnectEvents(visual.Camera);
			app.Run();
		}

		private static void LoadResources(ResourceManager resourceManager)
		{
			//todo: resourceManager.AddShader(MainVisual.ShaderName, Encoding.UTF8.GetString(Resourcen.vertex), Encoding.UTF8.GetString(Resourcen.fragment));
		}
	}
}