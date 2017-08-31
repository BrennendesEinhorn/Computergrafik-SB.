using DMS.Base;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;
using Imaging = System.Drawing.Imaging;

namespace DMS.OpenGL
{
	/// <summary>
	/// Writes rendered data into bitmaps. Usefull for creation of videos
	/// </summary>
	public class FrameListCreator : Disposable
	{
		public FrameListCreator(int width, int height, Imaging.PixelFormat format = Imaging.PixelFormat.Format24bppRgb, bool drawToFrameBuffer = true, bool needZbuffer = true)
		{
			this.format = format;
			render2tex = new FBO(Texture.Create(width, height,
				TextureLoader.SelectInternalPixelFormat(format),
				TextureLoader.SelectPixelFormat(format)), needZbuffer);
			if(drawToFrameBuffer) tex2fb = new TextureToFrameBuffer();
		}

		public void Activate()
		{
			render2tex.Activate();
		}

		public void Deactivate()
		{
			render2tex.Deactivate();
		}

		public void SaveFrame()
		{
			var bmp = TextureLoader.SaveToBitmap(render2tex.Texture, format);
			frames.Add(bmp);
			if (!ReferenceEquals(null, tex2fb))
			{
				//todo: if blending is used we have to clear the framebuffer
				GL.PushAttrib(AttribMask.DepthBufferBit);
				GL.Disable(EnableCap.DepthTest);
				tex2fb.Draw(render2tex.Texture);
				GL.PopAttrib();
			}
		}

		protected override void DisposeResources()
		{
			render2tex.Texture.Dispose();
			render2tex.Dispose();
			tex2fb?.Dispose();
		}

		public IEnumerable<Bitmap> Frames { get { return frames; } }

		private FBO render2tex;
		private TextureToFrameBuffer tex2fb;
		private List<Bitmap> frames = new List<Bitmap>();
		private Imaging.PixelFormat format;
	}
}
