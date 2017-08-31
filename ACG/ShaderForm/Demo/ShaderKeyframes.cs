using System;
using System.Collections.Generic;
using System.Linq;

namespace ShaderForm.Demo
{
	public class ShaderKeyframes
	{
		public event EventHandler<EventArgs> Changed;
		public IEnumerable<KeyValuePair<float, string>> Items { get { return keyframes; } }

		public void AddUpdate(float time, string shaderPath)
		{
			var clippedTime = Math.Max(0.0f, time);
			keyframes[clippedTime] = shaderPath;
			CallOnChange();
		}

		public void Clear()
		{
			keyframes.Clear();
			CallOnChange();
		}

		public string GetCurrentShader(float currentTime)
		{
            //if empty return empty string
            if(0 == keyframes.Count) return string.Empty;
			var firstItem = keyframes.First();
			if (firstItem.Key > currentTime) return firstItem.Value;
			try
			{
				return keyframes.Last((item) => item.Key <= currentTime).Value;
			}
			catch
			{
				return firstItem.Value;
			}
		}

		public IEnumerable<Tuple<float, string>> CalculateRatios(float length)
		{
			var ratios = new List<float>();
			var names = new List<string>();
			float last = 0.0f;
			foreach (var shaderKeyframe in keyframes)
			{
				ratios.Add((shaderKeyframe.Key - last) / length);
				last = shaderKeyframe.Key;
				names.Add(shaderKeyframe.Value);
			}
			ratios.Add((length - last) / length);
			ratios.RemoveAt(0);
			var result = new List<Tuple<float, string>>();
			if (0 == ratios.Count) return result;

			var enumRatios = ratios.GetEnumerator();
			var enumNames = names.GetEnumerator();
			enumRatios.MoveNext();
			enumNames.MoveNext();
			do
			{
				result.Add(new Tuple<float, string>(enumRatios.Current, enumNames.Current));
			} while (enumRatios.MoveNext() && enumNames.MoveNext());
			return result;
		}

		public static IEnumerable<Tuple<float, string>> CalculatePosFromRatios(IEnumerable<Tuple<float, string>> ratios, float length)
		{
			float pos = 0.0f;
			var result = new List<Tuple<float, string>>();
			foreach (var tuple in ratios)
			{
				result.Add(new Tuple<float, string>(pos, tuple.Item2));
				pos += tuple.Item1 * length;
			}
			return result;
		}

		public void RemoveAllWithName(string shaderFileName)
		{
			var result = keyframes.Where(pair => pair.Value == shaderFileName).ToList();
			foreach (var item in result)
			{
				keyframes.Remove(item.Key);
			}
			CallOnChange();
		}

		protected void CallOnChange()
		{
			Changed?.Invoke(this, EventArgs.Empty);
		}

		private SortedDictionary<float, string> keyframes = new SortedDictionary<float, string>();
	}
}
