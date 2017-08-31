using DMS.OpenGL;
using OpenTK.Graphics.OpenGL;
using System;
using System.Numerics;
using System.Diagnostics;

namespace Example
{
	struct Particle
	{
		public Vector2 position;
		public Vector2 velocity;
		//public Vector4 color; //make it vec4  not vec3 because of alignment in std430
	}

	public class MainVisual
	{
		public MainVisual()
		{
			InitParticles();
			GL.Enable(EnableCap.ProgramPointSize);
			GL.Enable(EnableCap.PointSprite);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One);
			timeSource.Start();
		}

		public static readonly string ShaderName = nameof(shader);

		public void ShaderChanged(string name, Shader shader)
		{
			if (ShaderName != name) return;
			this.shader = shader;
		}

		public void Render()
		{
			if (ReferenceEquals(null, shader)) return;
			var time = (float)timeSource.Elapsed.TotalSeconds;
			var deltaTime = time - lastRenderTime;
			lastRenderTime = time;
			Console.WriteLine(timeQuery.ResultLong * 1e-9);
			timeQuery.Activate(QueryTarget.TimeElapsed);
			GL.PointSize(1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit);
			shader.Activate();
			GL.Uniform1(shader.GetUniformLocation("deltaTime"), deltaTime);
			GL.Uniform1(shader.GetUniformLocation("particelCount"), particelCount);
			var bindingIndex = shader.GetShaderStorageBufferBindingIndex("BufferParticle");
			bufferParticles.ActivateBind(bindingIndex);
			GL.DrawArrays(PrimitiveType.Points, 0, particelCount);
			bufferParticles.Deactivate();
			shader.Deactivate();
			timeQuery.Deactivate();
		}

		private Shader shader;
		private BufferObject bufferParticles;
		private QueryObject timeQuery = new QueryObject();
		private Stopwatch timeSource = new Stopwatch();
		private float lastRenderTime = 0f;
		private const int particelCount = (int)1e5;

		private void InitParticles()
		{
			var rnd = new Random(12);
			Func<float> Rnd01 = () => (float)rnd.NextDouble();
			Func<float> RndCoord = () => (Rnd01() - 0.5f) * 2.0f;
			Func<float> RndSpeed = () => (Rnd01() - 0.5f) * 0.1f;

			bufferParticles = new BufferObject(BufferTarget.ShaderStorageBuffer);

			var data = new Particle[particelCount];
			for (int i = 0; i < particelCount; ++i)
			{
				data[i].position = new Vector2(RndCoord(), RndCoord());
				data[i].velocity = new Vector2(RndSpeed(), RndSpeed());
				//var polar = MathHelper.ToPolar(data[i].position);
				//data[i].color = new Vector4(ColorSystems.Hsb2rgb(polar.X / MathHelper.TWO_PI + 0.5f, polar.Y, 1), 1);
			}
			bufferParticles.Set(data, BufferUsageHint.StaticCopy);
		}
	}
}
