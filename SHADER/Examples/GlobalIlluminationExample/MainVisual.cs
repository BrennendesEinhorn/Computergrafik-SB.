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
			camera.Distance = 1.8f;
			camera.TargetY = -0.3f;
			camera.FovY = 70;

			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);
		}

		public static readonly string ShaderName = nameof(shader);
		public CameraOrbit OrbitCamera { get { return camera; } }

		public void ShaderChanged(string name, Shader shader)
		{
			if (ShaderName != name) return;
			this.shader = shader;
			if (ReferenceEquals(shader, null)) return;
			Mesh mesh = Meshes.CreateCornellBox();
			geometry = VAOLoader.FromMesh(mesh, shader);
			bufferMaterials.Set(Meshes.CreateCornellBoxMaterial(), BufferUsageHint.StaticDraw);
		}

		public void Render()
		{
			if (ReferenceEquals(shader, null)) return;
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			shader.Activate();
			GL.Uniform3(shader.GetUniformLocation("ambient"), new Vector3(0.1f));
			GL.Uniform3(shader.GetUniformLocation("lightPosition"), new Vector3(0, 0.9f, -0.5f));
			GL.Uniform3(shader.GetUniformLocation("lightColor"), new Vector3(0.8f));
			var cam = camera.CalcMatrix().ToOpenTK();
			GL.UniformMatrix4(shader.GetUniformLocation("camera"), true, ref cam);
			GL.Uniform3(shader.GetUniformLocation("cameraPosition"), camera.CalcPosition().ToOpenTK());
			var bindingIndex = shader.GetUniformBufferBindingIndex("bufferMaterials");
			bufferMaterials.ActivateBind(bindingIndex);
			geometry.Draw();
			bufferMaterials.Deactivate();
			shader.Deactivate();
		}

		private CameraOrbit camera = new CameraOrbit();
		private BufferObject bufferMaterials = new BufferObject(BufferTarget.UniformBuffer);
		private Shader shader;
		private VAO geometry;
	}
}
