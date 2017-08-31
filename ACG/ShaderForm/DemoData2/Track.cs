using System;
using System.Collections.Generic;

namespace ShaderForm.DemoData2
{
	[Serializable()]
	public class Track
	{
		//parameterless constructor for xml serialization needed
		public Track() { }

		public string Name;
		public List<ShaderKeyframe> ShaderKeyframes = new List<ShaderKeyframe>();
	}
}
