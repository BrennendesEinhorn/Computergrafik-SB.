using OpenTK.Graphics.OpenGL;
using DMS.Base;

namespace DMS.OpenGL
{
	public class TextureToFrameBuffer : Disposable
	{
		public delegate void SetUniforms(Shader currentShader);

		public TextureToFrameBuffer(string fragmentShader = FragmentShaderCopy, string vertexShader = VertexShaderScreenQuad)
		{
			shader = ShaderLoader.FromStrings(vertexShader, fragmentShader);
		}

		public void Draw(Texture texture, SetUniforms setUniformsHandler = null)
		{
			shader.Activate();
			texture.Activate();
			setUniformsHandler?.Invoke(shader);
			GL.DrawArrays(PrimitiveType.Quads, 0, 4);
			texture.Deactivate();
			shader.Deactivate();
		}

		public const string VertexShaderScreenQuad = @"
				#version 130		
				uniform vec2 iResolution;		
				out vec2 uv; 
				out vec2 fragCoord;
				void main() {
					const vec2 vertices[4] = vec2[4](vec2(-1.0, -1.0),
                                    vec2( 1.0, -1.0),
                                    vec2( 1.0,  1.0),
                                    vec2(-1.0,  1.0));
					vec2 pos = vertices[gl_VertexID];
					uv = pos * 0.5 + 0.5;
					fragCoord = uv * iResolution;
					gl_Position = vec4(pos, 0.0, 1.0);
				}";

		public const string FragmentShaderCopy = @"
			#version 430 core
			uniform sampler2D image;
			in vec2 uv;
			void main() {
				gl_FragColor = texture(image, uv);
			}";

		public const string FragmentShaderChecker = @"
			#version 430 core
			in vec2 uv;
			out vec4 color;
			void main() {
				vec2 uv10 = floor(uv * 10.0f);
				if(1.0 > mod(uv10.x + uv10.y, 2.0f))
					discard;		
				color = vec4(1, 1, 0, 0);
			}";
		private Shader shader;

		protected override void DisposeResources()
		{
			shader.Dispose();
		}
	}
}
