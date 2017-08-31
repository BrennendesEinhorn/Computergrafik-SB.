using DMS.Base;
using ShaderForm.Interfaces;
using ShaderForm.Visual;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShaderForm.DemoModelFactory
{
	public class Textures : Disposable, ITextures
	{
		public event EventHandler<EventArgs> Changed;

		public Textures(VisualContext visual)
		{
			this.visual = visual;
		}

		public bool AddUpdate(string fileName)
		{
			if (visual.AddUpdateTexture(fileName))
			{
				CallOnChange();
				return true;
			}
			CallOnChange();
			return false;
		}

		public void Clear()
		{
			foreach (var tex in visual.GetTextureNames().ToList()) visual.RemoveTexture(tex);
			CallOnChange();
		}

		public void Remove(string fileName)
		{
			visual.RemoveTexture(fileName);
			CallOnChange();
		}

		public IEnumerator<string> GetEnumerator()
		{
			return visual.GetTextureNames().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return visual.GetTextureNames().GetEnumerator();
		}

		protected void CallOnChange()
		{
			Changed?.Invoke(this, EventArgs.Empty);
		}

		protected override void DisposeResources()
		{
			Clear();
		}

		private VisualContext visual;
	}
}
