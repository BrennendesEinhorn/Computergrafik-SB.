using System;

namespace ShaderForm.DemoData2
{
	[Serializable()]
	public class ShaderKeyframe
	{
		public ShaderKeyframe() : this(0.0f, string.Empty) { }

		public ShaderKeyframe(float time, string shaderPath)
		{
			Time = time;
			ShaderPath = shaderPath;
		}

		public float Time;
		public string ShaderPath;
	}
}
