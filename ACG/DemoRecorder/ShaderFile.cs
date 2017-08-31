using ShaderForm;
using ShaderForm.DemoModelFactory;
using ShaderForm.Visual;
using System;
using System.IO;

namespace DemoRecorder
{
	public class ShaderFile : IShaderFile
	{
		public ShaderFile(VisualContext visualContext)
		{
			this.visualContext = visualContext;
		}

		public event EventHandler<string> Changed;

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public void Load(string shaderFileName)
		{
			if (!File.Exists(shaderFileName)) throw new ShaderLoadException("Non existent shader file '" + shaderFileName + "'!");
			visualContext.AddUpdateFragmentShader(shaderFileName);
			Changed?.Invoke(this, "Loading '+" + shaderFileName + "' with success!");
		}

		private VisualContext visualContext;
	}
}
