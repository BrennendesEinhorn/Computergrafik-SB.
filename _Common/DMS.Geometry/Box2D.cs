using System;

namespace DMS.Geometry
{
    /// <summary>
    /// Represents an 2D axis aligned bounding box
    /// </summary>
    [Serializable]
    public class Box2D : IEquatable<Box2D>
    {
        /// <summary>
        /// creates an AABR, an 2D axis aligned bounding box
        /// </summary>
        /// <param name="x">left side x coordinate</param>
        /// <param name="y">bottom side y coordinate</param>
        /// <param name="sizeX">width</param>
        /// <param name="sizeY">height</param>
        public Box2D(float x, float y, float sizeX, float sizeY)
        {
            this.X = x;
            this.Y = y;
            this.SizeX = sizeX;
            this.SizeY = sizeY;
        }

        public Box2D(Box2D rectangle)
        {
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            this.SizeX = rectangle.SizeX;
            this.SizeY = rectangle.SizeY;
        }

        public static readonly Box2D BOX01 = new Box2D(0, 0, 1, 1);
        public static readonly Box2D EMPTY = new Box2D(0, 0, 0, 0);

        public float SizeX { get; set; }

        public float SizeY { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float MaxX { get { return X + SizeX; } set { SizeX = value - X; } }

        public float MaxY { get { return Y + SizeY; } set { SizeY = value - Y; } }

        public float CenterX { get { return X + 0.5f * SizeX; } set { X = value - 0.5f * SizeX; } }

        public float CenterY { get { return Y + 0.5f * SizeY; } set { Y = value - 0.5f * SizeY; } }

        public static bool operator ==(Box2D a, Box2D b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Box2D a, Box2D b)
        {
            return !a.Equals(b);
        }

        public bool Equals(Box2D other)
        {
            if (ReferenceEquals(null, other)) return false;
            return X == other.X && Y == other.Y && SizeX == other.SizeX && SizeY == other.SizeY;
        }

        public override bool Equals(object other)
        {
            return Equals(other as Box2D);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Intersects(Box2D rectangle)
        {
            bool noXintersect = (MaxX <= rectangle.X) || (X >= rectangle.MaxX);
            bool noYintersect = (MaxY <= rectangle.Y) || (Y >= rectangle.MaxY);
            return !(noXintersect || noYintersect);
        }

        public bool Inside(Box2D rectangle)
        {
            if (X < rectangle.X) return false;
            if (MaxX > rectangle.MaxX) return false;
            if (Y < rectangle.Y) return false;
            if (MaxY > rectangle.MaxY) return false;
            return true;
        }

        public override string ToString()
        {
            return '(' + X.ToString() + ';' + Y.ToString() + ';' + SizeX.ToString() + ';' + SizeY.ToString() + ')';
        }
    }
}
