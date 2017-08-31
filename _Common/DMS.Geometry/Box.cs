namespace DMS.Geometry
{
	/// <summary>
	/// Represents an axis aligned bounding box
	/// </summary>
	public class Box
	{
		public Box(float x, float y, float z, float sizeX, float sizeY, float sizeZ)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.SizeX = sizeX;
			this.SizeY = sizeY;
			this.SizeZ = sizeZ;
		}

		public Box(Box box)
		{
			this.X = box.X;
			this.Y = box.Y;
			this.Z = box.Z;
			this.SizeX = box.SizeX;
			this.SizeY = box.SizeY;
			this.SizeZ = box.SizeZ;
		}

		public float SizeX { get; set; }
		public float SizeY { get; set; }
		public float SizeZ { get; set; }

		//public Vector3 Corner;

		//public float X { get { return Corner.X; } set { Corner.X = value; } }
		//public float Y { get { return Corner.Y; } set { Corner.Y = value; } }
		//public float Z { get { return Corner.Z; } set { Corner.Z = value; } }
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		public float CenterX { get { return X + 0.5f * SizeX; } set { X = value - 0.5f * SizeX; } }
		public float CenterY { get { return Y + 0.5f * SizeY; } set { Y = value - 0.5f * SizeY; } }
		public float CenterZ { get { return Y + 0.5f * SizeZ; } set { Y = value - 0.5f * SizeZ; } }

		public bool Intersects(Box box)
		{
			if (ReferenceEquals(null,  box)) return false;
			bool noXintersect = (MaxX < box.X) || (X > box.MaxX);
			bool noYintersect = (MaxY < box.Y) || (Y > box.MaxY);
			bool noZintersect = (MaxZ < box.Z) || (Z > box.MaxZ);
			return !(noXintersect || noYintersect || noZintersect);
		}

		public bool Contains(float x, float y, float z)
		{
			if (x < X || MaxX < x) return false;
			if (y < Y || MaxY < y) return false;
			if (z < Z || MaxZ < z) return false;
			return true;
		}

		public bool Inside(Box box)
		{
			if (X < box.X) return false;
			if (MaxX > box.MaxX) return false;
			if (Y < box.Y) return false;
			if (MaxY > box.MaxY) return false;
			if (Z < box.Z) return false;
			if (MaxZ > box.MaxZ) return false;
			return true;
		}

		public float MaxX { get { return X + SizeX; } set { X = value - SizeX; } }
		public float MaxY { get { return Y + SizeY; } set { Y = value - SizeY; } }
		public float MaxZ { get { return Z + SizeZ; } set { Z = value - SizeZ; } }

		public override string ToString()
		{
			return '(' + X.ToString() + ';' + Y.ToString() + ';' + Z.ToString() + ';' + SizeX.ToString() + ';' + SizeY.ToString() + ';' + SizeZ.ToString() + ')';
		}

	}
}
