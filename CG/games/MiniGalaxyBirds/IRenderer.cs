using DMS.Geometry;
namespace MiniGalaxyBirds
{
	public interface IRenderer
	{
		IDrawable CreateDrawable(string type, Box2D frame);
		IDrawable CreateDrawable(string type, Box2D frame, IAnimation animation);
		void DeleteDrawable(IDrawable drawable);
	}
}
