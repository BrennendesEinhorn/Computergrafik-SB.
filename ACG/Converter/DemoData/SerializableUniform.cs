using System;
using System.Collections.Generic;

namespace ShaderForm.DemoData
{
	[Serializable()]
	public class SerializableUniform
	{
		//parameterless constructor for xml serialization needed
		public SerializableUniform() { }

		public SerializableUniform(string uniformName)
		{
			UniformName = uniformName;
		}

		public SerializableUniform(string uniformName, IEnumerable<KeyValuePair<float, float>> uniformKeyFrames) : this(uniformName)
		{
			foreach (var kf in uniformKeyFrames)
			{
				Keyframes.Add(new Keyframe(kf.Key, kf.Value));
			}
		}

		public string UniformName;
		public List<Keyframe> Keyframes = new List<Keyframe>();
	}
}
