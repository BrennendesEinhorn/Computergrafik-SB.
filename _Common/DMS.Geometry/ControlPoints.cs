using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DMS.Geometry
{
	public class ControlPoints<T>  : IEnumerable<KeyValuePair<float, T>>
	{
		public void AddUpdate(float t, T value)
		{
			controlPoints[t] = value;
		}

		public void Clear()
		{
			controlPoints.Clear();
		}

		public int Count { get { return controlPoints.Count; } }

		public IEnumerator<KeyValuePair<float, T>> GetEnumerator()
		{
			return controlPoints.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return controlPoints.GetEnumerator();
		}

		public KeyValuePair<float, T> FindInfimum(float t)
		{
			var firstItem = this.First();
			if (firstItem.Key > t) return firstItem;
			try
			{
				return this.Last((item) => item.Key <= t);
			}
			catch (InvalidOperationException)
			{
				return firstItem;
			}
		}

		public Tuple<T, T, float> FindPair(float t, float epsilon = 0.001f)
		{
			if (0 == Count) throw new ArgumentException("No control points to interpolate!");
			var first = FindInfimum(t);
			var second = FindSupremum(t);
			float timeDelta = second.Key - first.Key;
			//if too little time inbetween return data value of infimum 
			float factor = (epsilon > Math.Abs(timeDelta)) ? 0 : (t - first.Key) / timeDelta;
			return new Tuple<T, T, float>(first.Value, second.Value, factor);
		}

		public KeyValuePair<float, T> FindSupremum(float t)
		{
			var lastItem = this.Last();
			if (lastItem.Key < t) return lastItem;
			try
			{
				return this.First((item) => item.Key >= t);
			}
			catch (InvalidOperationException)
			{
				return lastItem;
			}
		}

		private SortedDictionary<float, T> controlPoints = new SortedDictionary<float, T>();
	}
}
