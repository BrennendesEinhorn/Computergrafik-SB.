using ShaderForm.Interfaces;
using System;
using System.Windows.Forms;

namespace ShaderForm.Graph
{
	public static class KeyFramesHelper
	{
		public static void CopyKeyframesToClipboard(this IKeyFrames uniformKeyFrames)
		{
			string data = string.Empty;
			foreach (var keyframe in uniformKeyFrames)
			{
				data += keyframe.Key.ToString() + '\t' + keyframe.Value.ToString() + Environment.NewLine;
			}
			Clipboard.SetText(data);
		}

		public static void PasteKeyframesFromClipboard(this IKeyFrames uniformKeyFrames)
		{
			string data = Clipboard.GetText();
			var chunks = data.Split(new string[] { "\t", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			if (chunks.Length % 2 != 0) return;
			uniformKeyFrames.Clear();
			for (int i = 0; i < chunks.Length; ++i)
			{
				var time = ConvertToFloat(chunks[i]);
				++i;
				var value = ConvertToFloat(chunks[i]);
				uniformKeyFrames.AddUpdate(time, value);
			}
		}

		private static float ConvertToFloat(string data)
		{
			float result = 0.0f;
			if (float.TryParse(data, out result)) return result;
			return 0.0f;
		}
	}
}
