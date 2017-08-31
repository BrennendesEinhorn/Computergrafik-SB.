using System.Diagnostics;

namespace DMS.TimeTools
{
	public class FPSCounter
	{
		private Stopwatch sw = new Stopwatch();
		private uint frames = 0;
		private long lastTime = 0;

		public FPSCounter()
		{
			FPS = 1;
			sw.Start();
		}

		public float FPS { get; private set; }

		public void NewFrame()
		{
			++frames;
			long newTime = sw.ElapsedMilliseconds;
			long diff = newTime - lastTime;
			if (diff > 1000)
			{
				FPS = (1000.0f * frames) / diff;
				lastTime = newTime;
				frames = 0;
			}
		}
	}
}