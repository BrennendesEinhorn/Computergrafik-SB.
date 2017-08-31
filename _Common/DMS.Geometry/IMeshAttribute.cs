using System.Collections.Generic;

namespace DMS.Geometry
{
	public interface IMeshAttribute<TYPE>
	{
		string Name { get; }
		List<TYPE> List { get; }
	}
}
