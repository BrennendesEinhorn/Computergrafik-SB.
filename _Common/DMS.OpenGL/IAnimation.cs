using DMS.Geometry;

namespace DMS.OpenGL
{
	public interface IAnimation
	{
		float AnimationLength { get; set; }

		void Draw(Box2D rectangle, float totalSeconds);
	}
}