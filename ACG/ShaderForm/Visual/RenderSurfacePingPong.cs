using DMS.Base;
using DMS.OpenGL;
using OpenTK.Graphics.OpenGL;

namespace ShaderForm.Visual
{
	public class RenderSurfacePingPong : Disposable
	{
		public RenderSurfacePingPong()
		{
			fboA = new FBO(CreateTexture(1, 1));
			fboB = new FBO(CreateTexture(1, 1));
			activeFBO = fboA;
		}

		public Texture Active { get { return activeFBO.Texture; } }
		public Texture Last {  get {  return LastFBO.Texture; } }

		public void Render()
		{
			activeFBO.Activate();
			//Drawing
			GL.DrawArrays(PrimitiveType.Quads, 0, 4);
			activeFBO.Deactivate();
		}

		public void SwapRenderBuffer()
		{
			activeFBO = LastFBO;
		}

		public void UpdateSurfaceSize(int width, int height)
		{
			if (0 == width || 0 == height) return;
			if (width != activeFBO.Texture.Width || height != activeFBO.Texture.Height)
			{
				var isActive = activeFBO == fboA;
				fboA.Texture.Dispose();
				fboB.Texture.Dispose();
				fboA.Dispose();
				fboB.Dispose();
				fboA = new FBO(CreateTexture(width, height));
				fboB = new FBO(CreateTexture(width, height));
				activeFBO = isActive ? fboA : fboB;
			}
		}

		protected override void DisposeResources()
		{
			activeFBO = null;
			fboA.Texture.Dispose();
			fboB.Texture.Dispose();
			fboA.Dispose();
			fboB.Dispose();
		}

		private FBO fboA, fboB, activeFBO;

		private FBO LastFBO { get { return (activeFBO == fboA) ? fboB : fboA; } }

		private Texture CreateTexture(int width, int height)
		{
			//return Texture.Create(width, height);
			//return Texture.Create(width, height, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.HalfFloat);
			return Texture.Create(width, height, PixelInternalFormat.Rgba32f, PixelFormat.Rgba, PixelType.Float);
		}
	}
}
