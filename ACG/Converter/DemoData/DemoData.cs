using DMS.Base;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShaderForm.DemoData
{
	[Serializable()]
	public class DemoData
	{
		//parameterless constructor for xml serialization needed
		public DemoData() { }

		public List<SerializableUniform> Uniforms = new List<SerializableUniform>();
		public List<ShaderPathRatio> ShaderRatios = new List<ShaderPathRatio>();
		public List<string> Textures = new List<string>();

		public string SoundFileName { get; set; }

		public void ConvertToRelativePath(string relativeToDir)
		{
			SoundFileName = PathTools.GetRelativePath(relativeToDir, SoundFileName);
			foreach (var element in ShaderRatios)
			{
				element.ShaderPath = PathTools.GetRelativePath(relativeToDir, element.ShaderPath);
			}
			var temp = new List<string>();
			foreach (var element in Textures)
			{
				temp.Add(PathTools.GetRelativePath(relativeToDir, element));
			}
			Textures = temp;
		}

		public void ConvertToAbsolutePath(string relativeToDir)
		{
			try
			{
				string oldDir = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(relativeToDir);
				SoundFileName = PathTools.GetFullPath(SoundFileName);
				foreach (var element in ShaderRatios)
				{
					element.ShaderPath = PathTools.GetFullPath(element.ShaderPath);
				}
				var temp = new List<string>();
				foreach (var element in Textures)
				{
					temp.Add(PathTools.GetFullPath(element));
				}
				Textures = temp;
				Directory.SetCurrentDirectory(oldDir);
			}
			catch { }
		}
	}
}
