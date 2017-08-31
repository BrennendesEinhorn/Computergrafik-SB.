using System.IO;
using System.Linq;
using ShaderForm.DemoData2;
using System;
using System.Collections.Generic;
using DMS.Base;

namespace ShaderForm.Demo
{
	public class ProgressEventArgs : EventArgs
	{
		public ProgressEventArgs(string message)
		{
			Message = message;
			Cancel = false;
		}

		public bool Cancel { get; set; }
		public string Message { get; }
	}

	public delegate void ErrorEventHandler(object sender, ProgressEventArgs args);

	public class DemoLoader
	{
		public static void LoadFromFile(DemoModel demo, string fileName, ErrorEventHandler progressHandler = null)
		{
			var data = Serialize.ObjFromXMLFile(fileName, typeof(DemoData2.DemoData2)) as DemoData2.DemoData2;
			data.ConvertToAbsolutePath(Path.GetDirectoryName(Path.GetFullPath(fileName)));
			demo.Clear();
			if (!LoadSound(data.SoundFileName, demo, progressHandler)) return;
			foreach (var track in data.Tracks)
			{
				//todo1: load track.Name;
				foreach (var shaderKeyframe in track.ShaderKeyframes)
				{
					demo.Shaders.AddUpdateShader(shaderKeyframe.ShaderPath);
					demo.ShaderKeyframes.AddUpdate(shaderKeyframe.Time, shaderKeyframe.ShaderPath);
				}
			}
			if (!LoadTextures(data.Textures, demo, progressHandler)) return;

			LoadUniforms(data.Uniforms, demo);
		}

		public static void SaveToFile(DemoModel demo, string fileName)
		{
			var data = new DemoData2.DemoData2();
			Save(demo, data);
			data.ConvertToRelativePath(Path.GetDirectoryName(Path.GetFullPath(fileName)));
			data.ObjIntoXMLFile(fileName);
		}

		private static void LoadUniforms(IEnumerable<Uniform> uniforms, DemoModel demo)
		{
			foreach (var uniform in uniforms)
			{
				demo.Uniforms.Add(uniform.UniformName);
				var kfs = demo.Uniforms.GetKeyFrames(uniform.UniformName);
				foreach (var kf in uniform.Keyframes)
				{
					kfs.AddUpdate(kf.Time, kf.Value);
				}
			}
		}

		private static bool LoadSound(string soundFileName, DemoModel demo, ErrorEventHandler progressHandler)
		{
			if (!string.IsNullOrWhiteSpace(soundFileName))
			{
				var sound = DemoTimeSource.FromMediaFile(soundFileName);
				if (ReferenceEquals(null,  sound) && !ReferenceEquals(null,  progressHandler))
				{
					var args = new ProgressEventArgs("Could not load sound file '" + soundFileName + "'");
					progressHandler(demo, args);
					if (args.Cancel) return false;
				}
				demo.TimeSource.Load(sound, soundFileName);
				if (!ReferenceEquals(null,  sound) && !ReferenceEquals(null,  progressHandler))
				{
					var args = new ProgressEventArgs("Sound file '" + soundFileName + "' loaded");
					progressHandler(demo, args);
					if (args.Cancel) return false;
				}
			}
			return true;
		}

		private static bool LoadTextures(IEnumerable<string> textures, DemoModel demo, ErrorEventHandler progressHandler)
		{
			foreach (var tex in textures)
			{
				bool success = demo.Textures.AddUpdate(tex);
				if(!ReferenceEquals(null,  progressHandler))
				{
					var msg = success ? "Texture file '" + tex + "' loaded" : "Could not load texture file '" + tex + "'";
					var args = new ProgressEventArgs(msg);
					progressHandler(demo, args);
					if (args.Cancel) return false;
				}
			}
			return true;
		}

		private static void Save(DemoModel demo, DemoData2.DemoData2 data)
		{
			data.SoundFileName = demo.TimeSource.SoundFileName;
			data.Textures = demo.Textures.ToList();
			var track = new Track();
			track.Name = "sum";
			data.Tracks.Add(track);
			foreach (var element in demo.ShaderKeyframes.Items)
			{
				track.ShaderKeyframes.Add(new ShaderKeyframe(element.Key, element.Value));
			}
			foreach (var uniform in demo.Uniforms.Names)
			{
				var un = new Uniform(uniform, demo.Uniforms.GetKeyFrames(uniform));
				data.Uniforms.Add(un);
			}
		}
	}
}