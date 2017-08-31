using System;

namespace DMS.Base
{
	public abstract class Disposable : IDisposable
	{
		protected abstract void DisposeResources();

		public bool Disposed { get{ return disposed; } }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposed) return;
			if (disposing)
			{
				DisposeResources();
				disposed = true;
			}
		}

		private bool disposed = false;
	}
}
