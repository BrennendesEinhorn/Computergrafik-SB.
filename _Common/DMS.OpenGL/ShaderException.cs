using OpenTK.Graphics.OpenGL;
using System;

namespace DMS.OpenGL
{
	public class ShaderException : Exception
	{
		public string ShaderLog { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ShaderException"/> class.
		/// </summary>
		/// <param name="msg">The error msg.</param>
		public ShaderException(string msg, string log) : base(msg)
		{
			ShaderLog = log;
		}
	}

	public class ShaderCompileException : ShaderException
	{
		public ShaderType ShaderType { get; private set; }
		public string ShaderCode { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ShaderException"/> class.
		/// </summary>
		/// <param name="msg">The error msg.</param>
		public ShaderCompileException(ShaderType shaderType, string msg, string log, string shaderCode) : base(msg, log)
		{
			ShaderType = shaderType;
			ShaderCode = shaderCode.Replace("\r", string.Empty);
		}
	}
}
