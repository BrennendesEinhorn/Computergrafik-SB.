using System;

namespace ShaderForm.DemoData
{
	[Serializable()]
	public class ShaderPathRatio
	{
		public ShaderPathRatio() : this(string.Empty, 0.0f) { }

		public ShaderPathRatio(string shaderPath, float ratio)
		{
			ShaderPath = shaderPath;
			Ratio = ratio;
		}

		public string ShaderPath;
		public float Ratio;
	}
}
