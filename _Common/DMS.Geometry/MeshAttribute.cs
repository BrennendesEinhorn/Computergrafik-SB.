using System.Collections.Generic;

namespace DMS.Geometry
{
	public class MeshAttribute<TYPE> : IMeshAttribute<TYPE>
	{
		public MeshAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; private set; }
		public List<TYPE> List { get { return list; } }

		private readonly List<TYPE> list = new List<TYPE>();
	}
}
