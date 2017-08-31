using System;
using OpenTK.Graphics.OpenGL;
using DMS.Base;

namespace DMS.OpenGL
{
	/// <summary>
	/// Shader class
	/// </summary>
	public class Shader : Disposable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Shader"/> class.
		/// </summary>
		public Shader()
		{
			m_ProgramID = GL.CreateProgram();
		}

		public void Compile(string sShader, ShaderType type)
		{
			isLinked = false;
			int shaderObject = GL.CreateShader(type);
			if (0 == shaderObject) throw new ShaderCompileException(type, "Could not create " + type.ToString() + " object", string.Empty, sShader);
			// Compile vertex shader
			GL.ShaderSource(shaderObject, sShader);
			GL.CompileShader(shaderObject);
			int status_code;
			GL.GetShader(shaderObject, ShaderParameter.CompileStatus, out status_code);
			LastLog = GL.GetShaderInfoLog(shaderObject);
			if (1 != status_code)
			{
				throw new ShaderCompileException(type, "Error compiling  " + type.ToString(), LastLog, sShader);
			}
			GL.AttachShader(m_ProgramID, shaderObject);
			//shaderIDs.Add(shaderObject);
		}

		/// <summary>
		/// Begins this shader use.
		/// </summary>
		public void Activate()
		{
			GL.UseProgram(m_ProgramID);
		}

		/// <summary>
		/// Ends this shader use.
		/// </summary>
		public void Deactivate()
		{
			GL.UseProgram(0);
		}

		public int GetAttributeLocation(string name)
		{
			return GL.GetAttribLocation(m_ProgramID, name);
		}

		public int GetUniformLocation(string name)
		{
			return GL.GetUniformLocation(m_ProgramID, name);
		}

		public int GetShaderStorageBufferBindingIndex(string name)
		{
			var index = GL.GetProgramResourceIndex(m_ProgramID, ProgramInterface.ShaderStorageBlock, name);
			ProgramProperty[] prop = { ProgramProperty.BufferBinding };
			int length;
			int[] value = { -1 };
			GL.GetProgramResource(m_ProgramID, ProgramInterface.ShaderStorageBlock, index, 1, prop, 1, out length, value);
			return value[0];
		}

		public int GetUniformBufferBindingIndex(string name)
		{
			var index = GL.GetProgramResourceIndex(m_ProgramID, ProgramInterface.UniformBlock, name);
			ProgramProperty[] prop = { ProgramProperty.BufferBinding };
			int length;
			int[] value = { -1 };
			GL.GetProgramResource(m_ProgramID, ProgramInterface.UniformBlock, index, 1, prop, 1, out length, value);
			return value[0];
		}

		public bool IsLinked { get { return isLinked; } }

		public string LastLog { get; private set; }

		public void Link()
		{
			try
			{
				GL.LinkProgram(m_ProgramID);
			}
			catch (Exception)
			{
				throw new ShaderException("Unknown Link error!", string.Empty);
			}
			int status_code;
			GL.GetProgram(m_ProgramID, GetProgramParameterName.LinkStatus, out status_code);
			if (1 != status_code)
			{
				throw new ShaderException("Error linking shader", GL.GetProgramInfoLog(m_ProgramID));
			}
			isLinked = true;
		}

		protected override void DisposeResources()
		{
			if (0 != m_ProgramID)
			{
				GL.DeleteProgram(m_ProgramID);
			}
		}

		private int m_ProgramID = 0;
		private bool isLinked = false;

		//private List<int> shaderIDs = new List<int>();

		//private void DetachShaders()
		//{
		//	foreach (int id in shaderIDs)
		//	{
		//		GL.DetachShader(m_ProgramID, id);
		//	}
		//	shaderIDs.Clear();
		//}
	}
}
