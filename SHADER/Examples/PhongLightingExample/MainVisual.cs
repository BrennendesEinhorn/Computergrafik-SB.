using DMS.Geometry;
using DMS.OpenGL;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Example
{
	public class MainVisual
	{
		public MainVisual()
		{
			camera.FarClip = 50;
			camera.Distance = 5;
			camera.FovY = 30;

			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);
		}

		public CameraOrbit OrbitCamera { get { return camera; } }

		public void ShaderChanged(string name, Shader shader)
		{
			if (ShaderName != name) return;
			this.shader = shader;
			if (ReferenceEquals(shader, null)) return;
			UpdateGeometry(shader);
		}

		public void Render()
		{
			if (ReferenceEquals(shader, null)) return;
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			shader.Activate();
			GL.Uniform3(shader.GetUniformLocation("light1Direction"), new Vector3(-1, -1, -1).Normalized());
			GL.Uniform4(shader.GetUniformLocation("light1Color"), new Color4(1f, 1f, 1f, 1f));
			GL.Uniform3(shader.GetUniformLocation("light2Position"), new Vector3(-1, -1, 1));
			GL.Uniform4(shader.GetUniformLocation("light2Color"), new Color4(1f, .1f, .1f, 1f));
			GL.Uniform3(shader.GetUniformLocation("light3Position"), new Vector3(-2, 2, 2));
			GL.Uniform3(shader.GetUniformLocation("light3Direction"), new Vector3(1, -1, -1).Normalized());
			GL.Uniform1(shader.GetUniformLocation("light3Angle"), DMS.Geometry.MathHelper.DegreesToRadians(10f));
			GL.Uniform4(shader.GetUniformLocation("light3Color"), new Color4(0, 0, 1f, 1f));
			GL.Uniform4(shader.GetUniformLocation("ambientLightColor"), new Color4(.1f, .1f, .1f, 1f));
			GL.Uniform4(shader.GetUniformLocation("materialColor"), new Color4(.7f, .9f, .7f, 1f));
			var cam = camera.CalcMatrix().ToOpenTK();
			GL.UniformMatrix4(shader.GetUniformLocation("camera"), true, ref cam);
			GL.Uniform3(shader.GetUniformLocation("cameraPosition"), camera.CalcPosition().ToOpenTK());
			geometry.Draw();
			shader.Deactivate();
		}

		public static readonly string ShaderName = nameof(shader);
		private CameraOrbit camera = new CameraOrbit();

		private Shader shader;
		private VAO geometry;

		private void UpdateGeometry(Shader shader)
		{
			Mesh mesh = new Mesh();
			var roomSize = 8;
			var plane = Meshes.CreateQuad(roomSize, roomSize, 2, 2);
			var xform = new Transformation();
			xform.TranslateGlobal(0, -roomSize / 2, 0);
			mesh.Add(plane.Transform(xform));
			xform.RotateZGlobal(90f);
			mesh.Add(plane.Transform(xform));
			xform.RotateZGlobal(90f);
			mesh.Add(plane.Transform(xform));
			xform.RotateZGlobal(90f);
			mesh.Add(plane.Transform(xform));
			xform.RotateYGlobal(90f);
			mesh.Add(plane.Transform(xform));
			xform.RotateYGlobal(180f);
			mesh.Add(plane.Transform(xform));

			var sphere = Meshes.CreateSphere(1);
			mesh.Add(sphere);
			var suzanne = Obj2Mesh.FromObj(Resourcen.suzanne);
			mesh.Add(suzanne.Transform(System.Numerics.Matrix4x4.CreateTranslation(2, 2, -2)));
			geometry = VAOLoader.FromMesh(mesh, shader);
		}
	}
}
