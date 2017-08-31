using DMS.Base;
using OpenTK.Graphics.OpenGL;
using System;

namespace DMS.OpenGL
{
	/// <summary>
	/// Gl Texture class that allows loading from a file.
	/// </summary>
	public class Texture : Disposable
	{
		public enum FilterMode { NEAREST, LINEAR, MIPMAP };

		/// <summary>
		/// Initializes a new instance of the <see cref="Texture"/> class.
		/// </summary>
		public Texture()
		{
			//generate one texture and put its ID number into the "m_uTextureID" variable
			GL.GenTextures(1, out m_uTextureID);
			Width = 0;
			Height = 0;
		}

		public void WrapMode(TextureWrapMode mode)
		{
			Activate();
			GL.TexParameter(target, TextureParameterName.TextureWrapS, (int)mode);
			GL.TexParameter(target, TextureParameterName.TextureWrapT, (int)mode);
			Deactivate();
		}

		public void FilterLinear()
		{
			Activate();
			GL.TexParameter(target, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
			GL.TexParameter(target, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
			GL.TexParameter(target, TextureParameterName.GenerateMipmap, 0);
			Deactivate();
			filterMode = FilterMode.LINEAR;
		}

		public void FilterNearest()
		{
			Activate();
			GL.TexParameter(target, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Nearest);
			GL.TexParameter(target, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Nearest);
			GL.TexParameter(target, TextureParameterName.GenerateMipmap, 0);
			Deactivate();
			filterMode = FilterMode.NEAREST;
		}

		public void FilterMipmap()
		{
			Activate();
			GL.TexParameter(target, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
			GL.TexParameter(target, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.LinearMipmapLinear);
			GL.TexParameter(target, TextureParameterName.GenerateMipmap, 1);
			Deactivate();
			filterMode = FilterMode.MIPMAP;
		}

		public void Activate()
		{
			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(target, m_uTextureID);
		}

		public void Deactivate()
		{
			GL.BindTexture(target, 0);
			GL.Disable(EnableCap.Texture2D);
		}

		public FilterMode Filter
		{
			get { return filterMode; }
			set
			{
				switch ((FilterMode)(((int)value) % 3))
				{
					case FilterMode.NEAREST: FilterNearest(); break;
					case FilterMode.LINEAR: FilterLinear(); break;
					case FilterMode.MIPMAP: FilterMipmap(); break;
				}
			}
		}
		public void LoadPixels(IntPtr pixels, int width, int height, PixelInternalFormat internalFormat, PixelFormat inputPixelFormat, PixelType type)
		{
			Activate();
			GL.TexImage2D(target, 0, internalFormat, width, height, 0, inputPixelFormat, type, pixels);
			this.Width = width;
			this.Height = height;
			Deactivate();
		}

		public static Texture Create(int width, int height, PixelInternalFormat internalFormat = PixelInternalFormat.Rgba8
			, PixelFormat inputPixelFormat = PixelFormat.Rgba, PixelType type = PixelType.UnsignedByte)
		{
			var texture = new Texture();
			//create empty texture of given size
			texture.LoadPixels(IntPtr.Zero, width, height, internalFormat, inputPixelFormat, type);
			//set default parameters for filtering and clamping
			texture.FilterLinear();
			texture.WrapMode(TextureWrapMode.Repeat);
			return texture;
		}

		protected override void DisposeResources()
		{
			GL.DeleteTexture(m_uTextureID);
		}

		public int Width { get; private set; }

		public int Height { get; private set; }

		public uint ID { get { return m_uTextureID; } }
	
		private readonly uint m_uTextureID = 0;
		private FilterMode filterMode;
		private readonly TextureTarget target = TextureTarget.Texture2D;
	}
}
