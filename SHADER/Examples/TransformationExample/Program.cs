﻿using DMS.Application;
using DMS.Base;
using System;
using System.IO;

namespace Example
{
	public class Controller
	{
		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			//app.IsRecording = true;
			var visual = new MainVisual();
			app.ResourceManager.ShaderChanged += visual.ShaderChanged;
			LoadResources(app.ResourceManager);

			app.Render += visual.Render;
			app.Update += visual.Update;
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
