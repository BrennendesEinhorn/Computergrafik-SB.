using DMS.Base;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.IO;

namespace DMS.Sound
{
	public class AudioPlaybackEngine : Disposable
	{
		private readonly IWavePlayer outputDevice;
		private readonly MixingSampleProvider mixer;

		public AudioPlaybackEngine(int sampleRate = 44100, int channelCount = 2)
		{
			outputDevice = new WaveOutEvent();
			mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
			mixer.ReadFully = true;
			outputDevice.Init(mixer);
			outputDevice.Play();
		}

		private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
		{
			if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
			{
				return input;
			}
			if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
			{
				return new MonoToStereoSampleProvider(input);
			}
			throw new NotImplementedException("Not yet implemented this channel count conversion");
		}

		public void PlaySound(string fileName, bool looped = false)
		{
			var input = new AudioFileReader(fileName);
			if (looped)
			{
				var reader = new SoundLoopStream(input);
				var sampleChannel = new SampleChannel(reader, false);
				AddMixerInput(sampleChannel);
			}
			else
			{
				AddMixerInput(new AutoDisposeSampleProvider(input, input));
			}
		}

		/// <summary>
		/// Plays sound from a stream; you get unbuffered access if you usse a file stream 
		/// and buffered access if you use a memory stream
		/// </summary>
		/// <param name="stream"></param>
		public void PlaySound(Stream stream, bool looped = false)
		{
			WaveStream reader = FindCorrectWaveStream(stream);
			if (looped)
			{
				reader = new SoundLoopStream(reader);
				var sampleChannel = new SampleChannel(reader, false);
				AddMixerInput(sampleChannel);
			}
			else
			{
				var sampleChannel = new SampleChannel(reader, false);
				AddMixerInput(new AutoDisposeSampleProvider(sampleChannel, reader));
			}
		}

		private static WaveStream FindCorrectWaveStream(Stream stream)
		{
			try
			{
				WaveStream readerStream = new WaveFileReader(stream);
				if (readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm && readerStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
				{
					readerStream = WaveFormatConversionStream.CreatePcmStream(readerStream);
					readerStream = new BlockAlignReductionStream(readerStream);
				}
				return readerStream;
			} catch { }
			try
			{
				return new Mp3FileReader(stream);
			}
			catch { }
			try
			{
				return new AiffFileReader(stream);
			}
			catch
			{
				return null;
			}
		}

		private void AddMixerInput(ISampleProvider input)
		{
			mixer.AddMixerInput(ConvertToRightChannelCount(input));
		}

		protected override void DisposeResources()
		{
			outputDevice.Dispose();
		}
	}
}