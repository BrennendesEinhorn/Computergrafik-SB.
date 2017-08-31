using DMS.TimeTools;
using DMS.Sound;
using System;
using System.Diagnostics;
using System.IO;

namespace ShaderForm.Demo
{
	public class DemoTimeSource : ITimeSource
	{
		public string SoundFileName { get; private set; }

		public float Length
		{
			get { return timeSource.Length; }
			set { timeSource.Length = value; }
		}

		public bool IsLooping
		{
			get
			{
				return timeSource.IsLooping;
			}

			set
			{
				timeSource.IsLooping = value;
			}
		}

		public bool IsRunning
		{
			get
			{
				return timeSource.IsRunning;
			}

			set
			{
				timeSource.IsRunning = value;
			}
		}

		public float Position
		{
			get
			{
				return timeSource.Position;
			}

			set
			{
				timeSource.Position = value;
			}
		}

		public event EventHandler Loaded;
		public event TimeFinishedHandler TimeFinished;

		public DemoTimeSource(bool isLooping)
		{
			SoundFileName = string.Empty;
			timeSource = new TimeSource(10.0f);
			timeSource.IsLooping = isLooping;
			timeSource.TimeFinished += CallOnTimeFinished;
		}

		public static ITimeSource FromMediaFile(string fileName)
		{
			try
			{
				if (File.Exists(fileName))
				{
					var absoluteFileName = Path.GetFullPath(fileName);
					return new SoundTimeSource(absoluteFileName);
				}
				return null;
			}
			catch
			{
				return null;
			}
		}

		public void Load(ITimeSource newTimeSource, string soundFileName)
		{
			Debug.Assert(!ReferenceEquals(null,  timeSource));
			if (ReferenceEquals(null,  newTimeSource))
			{
				Clear();
			}
			else
			{
				newTimeSource.IsLooping = IsLooping;
				newTimeSource.TimeFinished += CallOnTimeFinished;
				timeSource.Dispose();
				timeSource = newTimeSource;
				SoundFileName = soundFileName;
				Loaded?.Invoke(this, EventArgs.Empty);
			}
		}

		public void Clear()
		{
			Debug.Assert(!ReferenceEquals(null,  timeSource));
			//keep looping state
			bool isLooping = timeSource.IsLooping;
			//remove old
			timeSource.Dispose();
			//create new
			timeSource = new TimeSource(10.0f);
			SoundFileName = string.Empty;
			timeSource.IsLooping = isLooping;
			timeSource.TimeFinished += CallOnTimeFinished;
			Loaded?.Invoke(this, EventArgs.Empty);
		}

		public void Dispose()
		{
			Debug.Assert(!ReferenceEquals(null, timeSource));
			timeSource.Dispose();
		}

		private ITimeSource timeSource;

		private void CallOnTimeFinished()
		{
			TimeFinished?.Invoke();
		}
	}
}
