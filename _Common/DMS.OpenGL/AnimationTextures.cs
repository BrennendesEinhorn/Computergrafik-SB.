using DMS.Geometry;
using System;
using System.Collections.Generic;

namespace DMS.OpenGL
{
	public class AnimationTextures : IAnimation
	{
		public AnimationTextures(float animationLength)
		{
			this.AnimationLength = animationLength;
		}

		public void AddFrame(Texture textureFrame)
		{
			textures.Add(textureFrame);
		}

		public float AnimationLength { get; set; }

		public IList<Texture> Textures
		{
			get
			{
				return textures;
			}
		}

		/// <summary>
		/// Calculates the frame id (the current frame of the animation) out of the given time
		/// </summary>
		/// <param name="time"></param>
		/// <returns>id of the current frame of the animation</returns>
		public uint CalcAnimationFrame(float time)
		{
			float normalizedDeltaTime = (time % AnimationLength) / AnimationLength;
			double idF = normalizedDeltaTime * (textures.Count - 1);
			uint id = (uint) Math.Max(0, Math.Round(idF));
			return id;
		}

		/// <summary>
		/// draws a GL quad, textured with an animation.
		/// </summary>
		/// <param name="rectangle">coordinates ofthe GL quad</param>
		/// <param name="totalSeconds">animation position in seconds</param>
		public void Draw(Box2D rectangle, float totalSeconds)
		{
			var id = (int)CalcAnimationFrame(totalSeconds);
			textures[id].Activate();
			rectangle.DrawTexturedRect(Box2D.BOX01);
			textures[id].Deactivate();
		}

		private List<Texture> textures = new List<Texture>();
	}
}
