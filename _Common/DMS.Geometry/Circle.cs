using System;

namespace DMS.Geometry
{
	/// <summary>
	/// Represents a circle
	/// </summary>
	[Serializable]
	public class Circle : IEquatable<Circle>
	{
		public Circle(float centerX, float centerY, float radius)
		{
			CenterX = centerX;
			CenterY = centerY;
			Radius = radius;
		}

		public float CenterX { get; set; }
		public float CenterY { get; set; }
		public float Radius { get; set; }

		public bool Intersects(Circle circle)
		{
			var rr = circle.Radius + Radius;
			rr *= rr;
			var xx = circle.CenterX - CenterX;
			xx *= xx;
			var yy = circle.CenterY - CenterY;
			yy *= yy;
			return rr > xx + yy;  
		}

		public static bool operator ==(Circle a, Circle b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Circle a, Circle b)
		{
			return !a.Equals(b);
		}

		public bool Equals(Circle other)
		{
			if (ReferenceEquals(null, other)) return false;
			return CenterX == other.CenterX && CenterY == other.CenterY && Radius == other.Radius;
		}

		public override bool Equals(object other)
		{
			return Equals(other as Circle);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return '(' + CenterX.ToString() + ',' + CenterY.ToString() + ';' + Radius.ToString() + ')';
		}
	}
}
