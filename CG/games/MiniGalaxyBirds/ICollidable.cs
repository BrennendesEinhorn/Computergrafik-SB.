using DMS.OpenGL;
using DMS.Geometry;

namespace MiniGalaxyBirds
{
	interface ICollidable
	{
		Box2D Frame { get; }
	}
}
