using DMS.Sound;
using DMS.Base;
using MvcSpaceInvaders;

namespace SpaceInvaders
{
	public class Sound : Disposable
	{
		public Sound()
		{
			soundEngine = new AudioPlaybackEngine();
		}

		public void Background()
		{
			//soundEngine.PlaySound(@"sound\Jamie xx - You've Got the Love.mp3");
		}

		public void DestroyEnemy()
		{
			//var memStream = new MemoryStream(Resourcen.EVAXDaughters);
			//soundEngine.PlaySound(memStream);
		}

		public void Lost()
		{
		}

		public void Shoot()
		{
			soundEngine.PlaySound(Resourcen.laser);
		}

		protected override void DisposeResources()
		{
			soundEngine.Dispose();
		}

		private AudioPlaybackEngine soundEngine;
	}
}
