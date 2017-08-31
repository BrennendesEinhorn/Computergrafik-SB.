using DMS.Base;
using DMS.TimeTools;
using NAudio.Wave;
using System;

namespace DMS.Sound
{
	public class SoundTimeSource : Disposable, ITimeSource
	{
		public event TimeFinishedHandler TimeFinished;

		public SoundTimeSource(string fileName)
		{
			waveOutDevice = new WaveOut();
			audioFileReader = new AudioFileReader(fileName);
			loopingWaveStream = new SoundLoopStream(audioFileReader);
			loopingWaveStream.EnableLooping = false;
			waveOutDevice.Init(loopingWaveStream);
			waveOutDevice.PlaybackStopped += (s, a) => playing = false;
			length = (float)audioFileReader.TotalTime.TotalSeconds;
		}

		public float Length
		{
			get { return length; }
			set { throw new ArgumentException("NAudioFacade cannot change Length"); }
		}

		public bool IsLooping
		{
			get { return loopingWaveStream.EnableLooping; }
			set { loopingWaveStream.EnableLooping = value; }
		}

		public bool IsRunning
		{
			get { return playing; }
			set { playing = value; if (playing) waveOutDevice.Play(); else waveOutDevice.Pause(); }
		}

		public float Position
		{
			get { return (float)audioFileReader.CurrentTime.TotalSeconds; }
			set
			{
				if (Length < value)
				{
					TimeFinished?.Invoke();
				}
				audioFileReader.CurrentTime = TimeSpan.FromSeconds(value);
			}
		}

		private IWavePlayer waveOutDevice;
		private AudioFileReader audioFileReader;
		private SoundLoopStream loopingWaveStream;

		private bool playing = false;
		private float length = 10.0f;

		protected override void DisposeResources()
		{
			if (waveOutDevice != null)
			{
				waveOutDevice.Stop();
			}
			if (audioFileReader != null)
			{
				audioFileReader.Dispose();
				audioFileReader = null;
			}
			if (waveOutDevice != null)
			{
				waveOutDevice.Dispose();
				waveOutDevice = null;
			}
		}
	}
}
