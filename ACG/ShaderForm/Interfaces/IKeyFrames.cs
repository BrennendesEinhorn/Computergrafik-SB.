using System;
using System.Collections.Generic;

namespace ShaderForm.Interfaces
{
	public interface IKeyFrames : IEnumerable<KeyValuePair<float, float>>
	{
		void AddUpdate(float time, float value);
		void Clear();
		float Interpolate(float currentTime);
	}
}