using DMS.Geometry;

namespace DMS.OpenGL
{
	public class SpriteSheet
	{
		public SpriteSheet(Texture tex, uint spritesPerLine
			, float spriteBoundingBoxWidth = 1.0f, float spriteBoundingBoxHeight = 1.0f)
		{
			this.tex = tex;
			this.tex.FilterMipmap();
			this.spritesPerLine = spritesPerLine;
			this.spriteBoundingBoxWidth = spriteBoundingBoxWidth;
			this.spriteBoundingBoxHeight = spriteBoundingBoxHeight;
		}

		public Box2D CalcSpriteTexCoords(uint spriteID)
		{
			return CalcSpriteTexCoords(spriteID, SpritesPerLine, SpriteBoundingBoxWidth, SpriteBoundingBoxHeight);
		}

		public static Box2D CalcSpriteTexCoords(uint spriteID, uint spritesPerLine
			, float spriteBoundingBoxWidth = 1.0f, float spriteBoundingBoxHeight = 1.0f)
		{
			uint row = spriteID / spritesPerLine;
			uint col = spriteID % spritesPerLine;

			float centerX = (col + 0.5f) / spritesPerLine;
			float centerY = 1.0f - (row + 0.5f) / spritesPerLine;
			float height = spriteBoundingBoxHeight / spritesPerLine;
			float width = spriteBoundingBoxWidth / spritesPerLine;
			return new Box2D(centerX - 0.5f * width, centerY - 0.5f * height, width, height);
		}

		public void Activate()
		{
			tex.Activate();
		}

		public void Deactivate()
		{
			tex.Deactivate();
		}

		public void Draw(uint spriteID, Box2D rectangle)
		{
			Box2D texCoords = CalcSpriteTexCoords(spriteID);
			rectangle.DrawTexturedRect(texCoords);
		}

		public float SpriteBoundingBoxWidth
		{
			get
			{
				return spriteBoundingBoxWidth;
			}
		}

		public float SpriteBoundingBoxHeight
		{
			get
			{
				return spriteBoundingBoxHeight;
			}
		}

		public uint SpritesPerLine
		{
			get
			{
				return spritesPerLine;
			}
		}

		public Texture Tex
		{
			get
			{
				return tex;
			}
		}

		private readonly float spriteBoundingBoxWidth;
		private readonly float spriteBoundingBoxHeight;
		private readonly uint spritesPerLine;
		private readonly Texture tex;
	}
}
