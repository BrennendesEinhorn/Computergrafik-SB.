using System;
using System.Collections.Generic;
using System.Linq;

namespace ShaderForm.DemoData2
{
	[Serializable()]
	public class Uniform
	{
		//parameterless constructor for xml serialization needed
		public Uniform() { }

		public Uniform(string uniformName)
		{
			this.UniformName = uniformName;
		}

		public Uniform(string uniformName, IEnumerable<KeyValuePair<float, float>> uniformKeyFrames) : this(uniformName)
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
