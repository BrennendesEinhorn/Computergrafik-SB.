using ShaderForm.Interfaces;
using System;
using System.Collections.Generic;

namespace ShaderForm.Demo
{
	public class Uniforms : IUniforms
	{
		public event EventHandler<string> ChangedKeyframes;
		public event EventHandler<string> UniformAdded;
		public event EventHandler<string> UniformRemoved;

		public bool Add(string uniformName)
		{
			if(!UniformHelper.IsNameValid(uniformName)) return false;
			if (uniforms.ContainsKey(uniformName)) return true;
			try
			{
				var kf = new KeyFrames();
				kf.Changed += (sender, arg) => ChangedKeyframes?.Invoke(sender, uniformName);
				uniforms.Add(uniformName, kf);
				UniformAdded?.Invoke(this, uniformName);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void Clear()
		{
			if (!ReferenceEquals(null,  UniformRemoved))
			{
				foreach (var uniformName in uniforms.Keys)
				{
					UniformRemoved(this, uniformName);
				}
			}
			uniforms.Clear();
		}

		public delegate void UniformCommand(string name, float value);
		public void Interpolate(float currentTime, UniformCommand command)
		{
			if (ReferenceEquals(null,  command)) return;
			foreach (KeyValuePair<string, KeyFrames> item in uniforms)
			{
				var value = item.Value.Interpolate(currentTime);
				command(item.Key, value);
			}
		}

		public IEnumerable<string> Names { get { return uniforms.Keys; } }

		public IKeyFrames GetKeyFrames(string uniformName)
		{
			KeyFrames kfs;
			if (uniforms.TryGetValue(uniformName, out kfs)) return kfs;
			return null;
		}

		public void Remove(string uniformName)
		{
			uniforms.Remove(uniformName);
			UniformRemoved?.Invoke(this, uniformName);
		}

		private Dictionary<string, KeyFrames> uniforms = new Dictionary<string, KeyFrames>();
	}
}
