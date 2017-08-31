using DMS.Base;
using OpenTK.Graphics.OpenGL;
using System;

namespace DMS.OpenGL
{
	[Serializable]
	public class FBOException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FBOException"/> class.
		/// </summary>
		/// <param name="msg">The error msg.</param>
		public FBOException(string msg) : base(msg) { }
	}

	public class FBO : Disposable
	{
		public FBO(Texture texture, bool depthBuffer = false)
		{
			if (ReferenceEquals(null, texture)) throw new FBOException("Texture is null");
			this.texture = texture;

			// Create an FBO object
			GL.GenFramebuffers(1, out m_FBOHandle);
			Activate();

			if (depthBuffer)
			{
				depth = GL.GenRenderbuffer();
				GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, depth);
				GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent32, texture.Width, texture.Height);
				GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, depth);
			}
			GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, texture.ID, 0);

			string status = GetStatusMessage();
			Deactivate();
			if (!string.IsNullOrEmpty(status))
			{
				throw new FBOException(status);
			}
		}

		public Texture Texture { get { return texture; } }

		public void Activate()
		{
			GL.PushAttrib(AttribMask.ViewportBit);
			lastFBO = currentFrameBufferHandle;
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, m_FBOHandle);
			GL.Viewport(0, 0, Texture.Width, Texture.Height);
			currentFrameBufferHandle = m_FBOHandle;
		}

		public void Deactivate()
		{
			GL.BindFramebuffer(FramebufferTarget.FramebufferExt, lastFBO);
			GL.PopAttrib();
			currentFrameBufferHandle = lastFBO;
		}

		private Texture texture;
		private uint m_FBOHandle = 0;
		private uint lastFBO = 0;
		private int depth = -1;
		private static uint currentFrameBufferHandle = 0;

		private string GetStatusMessage()
		{
			switch (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer))
			{
				case FramebufferErrorCode.FramebufferComplete: return string.Empty;
				case FramebufferErrorCode.FramebufferIncompleteAttachment: return "One or more attachment points are not framebuffer attachment complete. This could mean there’s no texture attached or the format isn’t renderable. For color textures this means the base format must be RGB or RGBA and for depth textures it must be a DEPTH_COMPONENT format. Other causes of this error are that the width or height is zero or the z-offset is out of range in case of render to volume.";
				case FramebufferErrorCode.FramebufferIncompleteMissingAttachment: return "There are no attachments.";
				case FramebufferErrorCode.FramebufferIncompleteDimensionsExt: return "Attachments are of different size. All attachments must have the same width and height.";
				case FramebufferErrorCode.FramebufferIncompleteFormatsExt: return "The color attachments have different format. All color attachments must have the same format.";
				case FramebufferErrorCode.FramebufferIncompleteDrawBuffer: return "An attachment point referenced by GL.DrawBuffers() doesn’t have an attachment.";
				case FramebufferErrorCode.FramebufferIncompleteReadBuffer: return "The attachment point referenced by GL.ReadBuffers() doesn’t have an attachment.";
				case FramebufferErrorCode.FramebufferUnsupported: return "This particular FBO configuration is not supported by the implementation.";
				default: return "Status unknown. (yes, this is really bad.)";
			}
		}

		protected override void DisposeResources()
		{
			GL.DeleteFramebuffers(1, ref m_FBOHandle);
			if(-1 != depth) GL.DeleteRenderbuffer(depth);
		}
	}
}
