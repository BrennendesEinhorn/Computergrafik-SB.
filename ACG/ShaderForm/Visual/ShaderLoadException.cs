using System;

namespace ShaderForm.Visual
{
	[Serializable]
	public class ShaderLoadException : Exception
	{
		public ShaderLoadException(string msg) : base(msg) { }
	}
}
