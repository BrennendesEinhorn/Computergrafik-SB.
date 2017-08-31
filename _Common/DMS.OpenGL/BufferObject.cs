using DMS.Base;
using OpenTK.Graphics.OpenGL;
using System;
using System.Runtime.InteropServices;

namespace DMS.OpenGL
{
	public class BufferObject : Disposable
	{
		public BufferObject(BufferTarget bufferTarget)
		{
			BufferTarget = bufferTarget;
			GL.GenBuffers​(1, out bufferID);
		}

		public BufferTarget BufferTarget { get; private set; }

		public void Activate()
		{
			GL.BindBuffer​(BufferTarget, bufferID);
		}

		public void ActivateBind(int index)
		{
			Activate();
			BufferRangeTarget target = (BufferRangeTarget)BufferTarget;
			GL.BindBufferBase​(target, index, bufferID);
		}

		public void Deactivate()
		{
			GL.BindBuffer​(BufferTarget, 0);
		}

		public void Set<DataElement>(DataElement[] data, BufferUsageHint usageHint) where DataElement : struct
		{
			Activate();
			int elementBytes = Marshal.SizeOf(typeof(DataElement));
			int bufferByteSize = data.Length * elementBytes;
			// set buffer data
			GL.BufferData(BufferTarget, (IntPtr)bufferByteSize, data, usageHint);
			//cleanup state
			Deactivate();
		}

		protected override void DisposeResources()
		{
			if (-1 == bufferID) return;
			GL.DeleteBuffer(bufferID);
			bufferID = -1;
		}

		private int bufferID;
	}
}
