using ShaderForm.Demo;
using ShaderForm.Visual;
using System.ComponentModel;

namespace ShaderForm.DemoModelFactory
{
	public static class DemoModelFactory
	{
		public static DemoModel Create(ISynchronizeInvoke syncObject)
		{
			var visualContext = new VisualContext();
			var shaders = new Shaders(() => new ShaderFile(visualContext, syncObject));
			var textures = new Textures(visualContext);
			return new DemoModel(visualContext, shaders, textures, true);
		}
	}
}
