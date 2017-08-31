using System.IO;
using OpenTK.Graphics.OpenGL;
using System.Text.RegularExpressions;
using System;

namespace DMS.OpenGL
{
	public static class ShaderLoader
	{
		public const string ExceptionDataFileName = "fileName";

		/// <summary>
		/// Compiles and links vertex and fragment shaders from strings.
		/// </summary>
		/// <param name="sVertexShd_">The s vertex SHD_.</param>
		/// <param name="sFragmentShd_">The s fragment SHD_.</param>
		/// <returns>a new instance</returns>
		public static Shader FromStrings(string sVertexShd_, string sFragmentShd_)
		{
			Shader shd = new Shader();
			try
			{
				shd.Compile(sVertexShd_, ShaderType.VertexShader);
				shd.Compile(sFragmentShd_, ShaderType.FragmentShader);
				shd.Link();
			}
			catch (Exception e)
			{
				shd.Dispose();
				throw e;
			}
			return shd;
		}

		/// <summary>
		/// Compiles and links vertex and fragment shaders from files.
		/// </summary>
		/// <param name="sVertexShdFile_">The s vertex SHD file_.</param>
		/// <param name="sFragmentShdFile_">The s fragment SHD file_.</param>
		/// <returns>a new instance</returns>
		public static Shader FromFiles(string sVertexShdFile_, string sFragmentShdFile_)
		{
			string sVertexShd = ShaderStringFromFileWithIncludes(sVertexShdFile_, false);
			string sFragmentShd = ShaderStringFromFileWithIncludes(sFragmentShdFile_, false);
			try
			{
				var shader = FromStrings(sVertexShd, sFragmentShd);
				if (!shader.IsLinked)
				{
					sVertexShd = ShaderStringFromFileWithIncludes(sVertexShdFile_, true);
					sFragmentShd = ShaderStringFromFileWithIncludes(sFragmentShdFile_, true);
					return FromStrings(sVertexShd, sFragmentShd);
				}
				return shader;
			}
			catch (ShaderCompileException sce)
			{
				if (sce.Data.Contains(ExceptionDataFileName)) throw sce;
				switch (sce.ShaderType)
				{
					case ShaderType.VertexShader: sce.Data.Add(ExceptionDataFileName, sVertexShdFile_); break;
					case ShaderType.FragmentShader: sce.Data.Add(ExceptionDataFileName, sFragmentShdFile_); break;
					default: throw new ArgumentOutOfRangeException("FromFiles called with unexpected shader type", sce);
				}
				throw sce;
			}
		}

		/// <summary>
		/// Reads the contents of a file into a string
		/// </summary>
		/// <param name="shaderFile">path to the shader file</param>
		/// <param name="precompileInclude">should includes be compiled (for error checking) before beeing pasted into the including shader</param>
		/// <returns>string with contents of shaderFile</returns>
		public static string ShaderStringFromFileWithIncludes(string shaderFile, bool precompileInclude)
		{
			string sShader = null;
			if (!File.Exists(shaderFile))
			{
				throw new FileNotFoundException("Could not find shader file '" + shaderFile + "'");
			}
			sShader = File.ReadAllText(shaderFile);

			//handle includes
			string sCurrentPath = Path.GetDirectoryName(shaderFile) + Path.DirectorySeparatorChar; // get path to current shader
			string sName = Path.GetFileName(shaderFile);
			//split into lines
			var lines = sShader.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			var pattern = @"^\s*#include\s+" + '"' + "(.+)" + '"';
			int lineNr = 1;
			foreach (var line in lines)
			{
				// Search for include pattern (e.g. #include raycast.glsl) (nested not supported)
				foreach (Match match in Regex.Matches(line, pattern, RegexOptions.Singleline))
				{
					string sFullMatch = match.Value;
					string sIncludeFileName = match.Groups[1].ToString(); // get the filename to include
					string sIncludePath = sCurrentPath + sIncludeFileName; // build path to file

					if (!File.Exists(sIncludePath))
					{
						throw new FileNotFoundException("Could not find include-file '" + sIncludeFileName + "' for shader '" + shaderFile + "'.");
					}
					string sIncludeShd = File.ReadAllText(sIncludePath); // read include as string
					if (precompileInclude)
					{
						using (var shader = new Shader())
						{
							try
							{
								shader.Compile(sIncludeShd, ShaderType.FragmentShader); //test compile include shader
							}
							catch (ShaderCompileException e)
							{
								var ce = new ShaderCompileException(e.ShaderType,
									"include compile '" + sIncludePath + "'",
									e.ShaderLog, sIncludeShd);
								ce.Data.Add(ExceptionDataFileName, sIncludePath);
								throw ce;
							}
						}
					}
					sIncludeShd += Environment.NewLine + "#line " + lineNr.ToString() + Environment.NewLine;
					sShader = sShader.Replace(sFullMatch, sIncludeShd); // replace #include with actual include
				}
				++lineNr;
			}
			return sShader;
		}

		public static string ExtractFileName(this ShaderException e)
		{
			if (e.Data.Contains(ExceptionDataFileName))
			{
				return e.Data[ExceptionDataFileName] as string;
			}
			return string.Empty;
		}
	}
}
