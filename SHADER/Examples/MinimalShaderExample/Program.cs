using DMS.Application;
using DMS.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Example
{
	class MyVisual
	{
		private MyVisual()
		{
			string sVertexShader = @"
				#version 430 core				
				out vec3 pos; 
				void main() {
					const vec3 vertices[4] = vec3[4](vec3(-0.9, -0.8, 0.5),
                                    vec3( 0.9, -0.9, 0.5),
                                    vec3( 0.9,  0.8, 0.5),
                                    vec3(-0.9,  0.9, 0.5));
					pos = vertices[gl_VertexID];
					gl_Position = vec4(pos, 1.0);
				}";
			string sFragmentShd = @"
			#version 430 core
			in vec3 pos;
			out vec4 color;
			void main() {
				color = vec4(pos + 1.0, 1.0);
			}";
			//read shader from file
			//string fileName = "Hello world.glsl";
			//try
			//{
			//	using (StreamReader sr = new StreamReader(fileName))
			//	{
			//		sFragmentShd = sr.ReadToEnd();
			//		sr.Dispose();
			//	}
			//}
			//catch { };
			shader = ShaderLoader.FromStrings(sVertexShader, sFragmentShd);
		}

		private Shader shader;

		private void Render()
		{
			if (ReferenceEquals(shader, null)) return;
			GL.Clear(ClearBufferMask.ColorBufferBit);
			shader.Activate();
			GL.DrawArrays(PrimitiveType.Quads, 0, 4);
			shader.Deactivate();
		}

		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			var visual = new MyVisual();
			app.Render += visual.Render;
			app.Run();
		}
	}
}