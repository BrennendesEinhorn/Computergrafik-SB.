using DMS.Base;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShaderForm.DemoData2
{
	[Serializable()]
	public class DemoData2
	{
		//parameterless constructor for xml serialization needed
		public DemoData2() { }

		public List<Uniform> Uniforms = new List<Uniform>();
		public List<Track> Tracks = new List<Track>();
		public List<string> Textures = new List<string>();

		public string SoundFileName { get; set; }

		public void ConvertToRelativePath(string relativeToDir)
		{
			SoundFileName = PathTools.GetRelativePath(relativeToDir, SoundFileName);
			foreach (var track in Tracks)
			{
				foreach (var element in track.ShaderKeyframes)
				{
					element.ShaderPath = PathTools.GetRelativePath(relativeToDir, element.ShaderPath);
				}
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
				foreach (var track in Tracks)
				{
					foreach (var element in track.ShaderKeyframes)
					{
						element.ShaderPath = PathTools.GetFullPath(element.ShaderPath);
					}
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
