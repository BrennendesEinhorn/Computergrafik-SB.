using System;

namespace ShaderForm.DemoModelFactory
{
	public interface IShaderFile : IDisposable
	{
		event EventHandler<string> Changed;

		void Load(string shaderFileName);
	}
}