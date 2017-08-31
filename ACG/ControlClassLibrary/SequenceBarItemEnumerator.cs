using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ControlClassLibrary
{
	public class SequenceBarItemEnumerator : IEnumerable<SequenceBarItem>, IEnumerator<SequenceBarItem>
	{
		private IEnumerator enumerator;

		public SequenceBarItemEnumerator(IEnumerator enumerator)
		{
			this.enumerator = enumerator;
		}

		public SequenceBarItem Current
		{
			get
			{
				var menu = enumerator.Current as ToolStripMenuItem;
				if (ReferenceEquals(null,  menu)) return null;
				var item = menu.Tag as SequenceBarItem;
				return item;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		public void Dispose()
		{
		}

		public IEnumerator<SequenceBarItem> GetEnumerator()
		{
			return this;
		}

		public bool MoveNext()
		{
			return enumerator.MoveNext();
		}

		public void Reset()
		{
			enumerator.Reset();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}
	}
}
