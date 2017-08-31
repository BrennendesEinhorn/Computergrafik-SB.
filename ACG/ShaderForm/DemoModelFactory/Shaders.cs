using DMS.Base;
using ShaderForm.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ShaderForm.DemoModelFactory
{
	public class Shaders : Disposable, IShaders
	{
		public event EventHandler<string> Changed;

		public delegate IShaderFile ShaderFileCreator();
		public Shaders(ShaderFileCreator shaderCreator)
		{
			this.shaderCreator = shaderCreator;
		}

		public void AddUpdateShader(string shaderFileName)
		{
			if (!File.Exists(shaderFileName))
			{
				CallOnChange("Could not find shader '" + shaderFileName + "'");
				return;
			}
			if (shaders.ContainsKey(shaderFileName)) return;
			var shader = shaderCreator();
			shader.Changed += (sender, message) => CallOnChange(message);
			shaders[shaderFileName] = shader;
			shader.Load(shaderFileName);
		}

		public void Clear()
		{
			try
			{
				foreach (var shader in shaders.Values) shader.Dispose();
				shaders.Clear();
			}
			finally
			{
				CallOnChange(string.Empty);
			}
		}

		public void RemoveShader(string shaderFileName)
		{
			IShaderFile shader;
			if (shaders.TryGetValue(shaderFileName, out shader))
			{
				shader.Dispose();
			}
			shaders.Remove(shaderFileName);

			CallOnChange(string.Empty);
		}

		public IEnumerator<string> GetEnumerator()
		{
			return shaders.Keys.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return shaders.Keys.GetEnumerator();
		}

		protected void CallOnChange(string message)
		{
			Changed?.Invoke(this, message);
		}

		protected override void DisposeResources()
		{
			shaderCreator = null;
		}

		private Dictionary<string, IShaderFile> shaders = new Dictionary<string, IShaderFile>();

		private ShaderFileCreator shaderCreator;
	}
}
