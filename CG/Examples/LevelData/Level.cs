using DMS.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LevelData
{
	[Serializable]
	public class Element
	{
		public Element(string name)
		{
			Name = name;
		}
		public string Name { get; set; }
	}

	[Serializable]
	public class ColliderCircle : Element
	{
		public ColliderCircle(string name, Circle bounds): base(name)
		{
			Bounds = bounds;
		}

		public Circle Bounds { get; set; }
	}

	[Serializable]
	public class Sprite : Element
	{
		public Sprite(string name, Box2D renderBounds, int layer) : base(name)
		{
			Layer = layer;
			RenderBounds = renderBounds;
		}

		public int Layer { get; set; }
		public Box2D RenderBounds { get; set; }
		public string TextureName { get; set; }
		public Bitmap Bitmap { get; set; }
	}

	[Serializable]
	public class Level
	{
		public Box2D Bounds = new Box2D(Box2D.BOX01);

		public void Add(ColliderCircle collider)
		{
			colliders.Add(collider);
		}

		public List<Sprite> Sprites = new List<Sprite>();
		public List<ColliderCircle> colliders = new List<ColliderCircle>();
	}
}
