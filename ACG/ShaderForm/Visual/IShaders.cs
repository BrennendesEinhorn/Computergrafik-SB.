using System;
using System.Collections.Generic;

namespace ShaderForm.Demo
{
	public interface IShaders : IEnumerable<string>
	{
		event EventHandler<string> Changed;

		void AddUpdateShader(string shaderFileName);
		void Clear();
		void RemoveShader(string shaderFileName);
	}
}