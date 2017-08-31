using System;
using System.Collections.Generic;

namespace ShaderForm.Interfaces
{
	public interface ITextures : IEnumerable<string>
	{
		event EventHandler<EventArgs> Changed;

		bool AddUpdate(string fileName);
		void Clear();
		void Remove(string fileName);
	}
}