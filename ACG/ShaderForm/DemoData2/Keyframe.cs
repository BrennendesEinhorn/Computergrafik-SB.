using System;

namespace ShaderForm.DemoData2
{
	[Serializable()]
	public class Keyframe
	{
		//parameterless constructor for xml serialization needed
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
