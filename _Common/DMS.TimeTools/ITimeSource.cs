using System;

namespace DMS.TimeTools
{
	public delegate void TimeFinishedHandler();

	public interface ITimeSource : IDisposable
	{
		float Length { get; set; }
		bool IsLooping { get; set; }
		bool IsRunning { get; set; }
		float Position { get; set; }

		event TimeFinishedHandler TimeFinished;
	}
}