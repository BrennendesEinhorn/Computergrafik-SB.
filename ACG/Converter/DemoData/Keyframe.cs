using System;

namespace ShaderForm.DemoData
{
	[Serializable()]
	public class Keyframe
	{
		public Keyframe() { }

		public Keyframe(float time, float value)
		{
			Time = time;
			Value = value;
		}

		public float Time;
		public float Value;
	}
}