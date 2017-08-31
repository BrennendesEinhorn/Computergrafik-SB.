namespace DMS.TimeTools
{
	/// <summary>
	/// Invokes a registered callback in regular intervalls
	/// </summary>
	public class PeriodicUpdate : ITimedUpdate
	{
		public PeriodicUpdate(float interval)
		{
			Interval = interval;
			PeriodElapsedCount = 0;
			Enabled = false;
			PeriodRelativeTime = 0;
		}

		public uint PeriodElapsedCount { get; private set; }
		public float PeriodRelativeTime { get; private set; }
		public bool Enabled { get; private set; }
		public delegate void PeriodElapsedHandler(PeriodicUpdate sender, float absoluteTime);
		public event PeriodElapsedHandler PeriodElapsed;
		public float Interval { get; set; }

		public void Start(float startTime)
		{
			absoluteTime = startTime;
			Enabled = true;
		}

		public void Stop()
		{
			Enabled = false;
		}

		public void Update(float absoluteTime)
		{
			if (!Enabled)
			{
				this.absoluteTime = absoluteTime;
				PeriodRelativeTime = 0.0f;
				return;
			}
			PeriodRelativeTime = absoluteTime - this.absoluteTime;
			if (PeriodRelativeTime > Interval)
			{
				PeriodElapsed?.Invoke(this, absoluteTime);
				this.absoluteTime = absoluteTime;
				PeriodRelativeTime = 0.0f;
				++PeriodElapsedCount;
			}
		}

		private float absoluteTime = 0.0f;
	}
}
