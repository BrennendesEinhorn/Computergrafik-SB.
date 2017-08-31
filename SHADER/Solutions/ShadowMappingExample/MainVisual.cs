using DMS.OpenGL;
using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Example
{
	public class MainVisual
	{
		public MainVisual()
		{
			camera.FarClip = 50;
			camera.Distance = 8;
			camera.Elevation = 30;

			cameraLight.FarClip = 50;
			cameraLight.Distance = 8;
			cameraLight.Elevation = 44;
			cameraLight.Azimuth = -100;

			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);
			fboShadowMap.Texture.FilterNearest();
		}

		public static readonly string ShaderName = nameof(shader);
		public static readonly string ShaderDepthName = nameof(shaderDepth);
		public CameraOrbit OrbitCamera { get { return camera; } }

		public void ShaderChanged(string name, Shader shader)
		{
			if (ShaderName == name)
			{
				this.shader = shader;
				if (ReferenceEquals(shader, null)) return;
				Mesh mesh = Meshes.CreateQuad(10, 10, 10, 10);
				var sphere = Meshes.CreateSphere(0.5f, 2);
				sphere.SetConstantUV(new System.Numerics.Vector2(0.5f, 0.5f));
				var xform = new Transformation();
				xform.TranslateLocal(0, 2, -2);
				mesh.Add(sphere.Transform(xform));
				xform.TranslateGlobal(0, 0, 2);
				mesh.Add(sphere.Transform(xform));
				xform.TranslateGlobal(2, 0, -1);
				mesh.Add(sphere.Transform(xform));
				geometry = VAOLoader.FromMesh(mesh, shader);
			}
			else if(ShaderDepthName == name)
			{
				this.shaderDepth = shader;
				if (ReferenceEquals(shaderDepth, null)) return;
				//todo: radeon cards make errors with geometry bound to one shader and use in other shaders because of binding id changes
			}
		}

		public void Render()
		{
			if (ReferenceEquals(shader, null)) return;
			if (ReferenceEquals(shaderDepth, null)) return;
			shaderDepth.Activate();
			fboShadowMap.Activate();
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			var light = cameraLight.CalcMatrix().ToOpenTK();
			GL.UniformMatrix4(shaderDepth.GetUniformLocation("camera"), true, ref light);
			geometry.Draw();
			shaderDepth.Deactivate();
			fboShadowMap.Deactivate();

			if (ReferenceEquals(shader, null)) return;
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			shader.Activate();
			fboShadowMap.Texture.Activate();
			GL.Uniform3(shader.GetUniformLocation("ambient"), new Vector3(0.1f));
			var cam = camera.CalcMatrix().ToOpenTK();
			GL.UniformMatrix4(shader.GetUniformLocation("camera"), true, ref cam);
			GL.UniformMatrix4(shader.GetUniformLocation("light"), true, ref light);
			geometry.Draw();
			fboShadowMap.Texture.Deactivate();
			shader.Deactivate();
		}

		private CameraOrbit camera = new CameraOrbit();
		private CameraOrbit cameraLight = new CameraOrbit();
		private Shader shader;
		private Shader shaderDepth;
		private FBO fboShadowMap = new FBO(Texture.Create(512, 512, PixelInternalFormat.R32f, PixelFormat.Red, PixelType.Float), true);
		private VAO geometry;
	}
}
