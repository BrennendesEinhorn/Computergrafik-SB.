using DMS.OpenGL;
using DMS.Geometry;

namespace MiniGalaxyBirds
{
	public class AnimatedSprite : IDrawable
	{
		public AnimatedSprite(Texture tex, Box2D extents, IAnimation animation)
		{
			this.spriteSheet = new SpriteSheetAnimation(new SpriteSheet(tex, 5), 0, 24, animation.Length);
			this.Rect = extents;
			this.Animation = animation;
		}

		public void Draw()
		{
			spriteSheet.Draw(Rect, Animation.Time);
		}

		public IAnimation Animation { get; private set; }
		public Box2D Rect { get; private set; }
		private SpriteSheetAnimation spriteSheet;
	}
}
