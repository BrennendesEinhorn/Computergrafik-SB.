﻿using DMS.Application;
using DMS.Base;
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
			var controller = new Controller();
			var visual = new MainVisual();
			app.ResourceManager.ShaderChanged += visual.ShaderChanged;
			LoadResources(app.ResourceManager);
			var timeSource = new Stopwatch();
			app.GameWindow.ConnectEvents(visual.OrbitCamera);
			app.Render += visual.Render;
			app.Update += (t) => visual.Update((float)timeSource.Elapsed.TotalSeconds);
			timeSource.Start();
			app.Run();
		}

		private static void LoadResources(ResourceManager resourceManager)
		{
			var dir = Path.GetDirectoryName(PathTools.GetSourceFilePath()) + "/Resources/";
			resourceManager.AddShader(VisualSmoke.ShaderName, dir + "smoke.vert", dir + "smoke.frag"
				, Resourcen.smoke_vert, Resourcen.smoke_frag);
			resourceManager.AddShader(VisualWaterfall.ShaderName, dir + "smoke.vert", dir + "smoke.frag"
				, Resourcen.smoke_vert, Resourcen.smoke_frag);
		}
	}
}