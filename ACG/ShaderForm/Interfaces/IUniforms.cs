using System;
using System.Collections.Generic;

namespace ShaderForm.Interfaces
{
	public interface IUniforms
	{
		IEnumerable<string> Names { get; }

		event EventHandler<string> UniformAdded;
		event EventHandler<string> UniformRemoved;
		event EventHandler<string> ChangedKeyframes;

		bool Add(string uniformName);
		IKeyFrames GetKeyFrames(string uniformName);
		void Remove(string uniformName);
	}
}