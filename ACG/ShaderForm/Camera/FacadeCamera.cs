using ShaderForm.Interfaces;
using System.Windows.Forms;

namespace ShaderForm.Camera
{
	public class FacadeCamera
	{
		public FacadeCamera()
		{
			adapter = new AdapterCamera(camera);
			formCamera.Set(adapter);
		}

		public void AddKeyFrames(float time, IUniforms uniforms)
		{
			//todo1: event recursions handle all with mediator pattern or similar
			var position = camera.Position;
			var rotation = camera.Rotation;
			for (int i = 0; i < 3; ++i)
			{
				uniforms.Add(posUniformNames[i]);
				var kfsPos = uniforms.GetKeyFrames(posUniformNames[i]);
				kfsPos.AddUpdate(time, position[i]);

				uniforms.Add(rotUniformNames[i]);
				var kfsRot = uniforms.GetKeyFrames(rotUniformNames[i]);
				kfsRot.AddUpdate(time, rotation[i]);
			}
		}

		public void SaveLayout()
		{
			formCamera.SaveData();
		}

		public void KeyChange(Keys keyCode, bool pressed)
		{
			camera.KeyChange(keyCode, pressed);
			if (IsActive)
			{
				formCamera.Set(new AdapterCamera(camera));
			}
			Redraw?.Invoke(this);
		}

		public bool IsActive { get { return camera.IsActive; } }

		public delegate void ChangedHandler(FacadeCamera camera);
		public event ChangedHandler Redraw;

		public void Reset()
		{
			camera = new FlyCamera();
		}

		public void Show()
		{
			formCamera.Visible = true;
		}

		public void Update(float mouseX, float mouseY, bool mouseDown)
		{
			camera.Update(mouseX, mouseY, mouseDown);
		}

		public bool UpdateFromUniforms(IUniforms uniforms, float time)
		{
			for (int i = 0; i < 3; ++i)
			{
				var kfsPos = uniforms.GetKeyFrames(posUniformNames[i]);
				if (ReferenceEquals(null, kfsPos)) return false;
				var value = kfsPos.Interpolate(time);
				camera.Position[i] = value;

				var kfsRot = uniforms.GetKeyFrames(rotUniformNames[i]);
				if (ReferenceEquals(null, kfsRot)) return false;
				var valueRot = kfsRot.Interpolate(time);
				camera.Rotation[i] = valueRot;
			}
			return true;
		}

		public void SetUniforms(ISetUniform visualContext)
		{
			for (int i = 0; i < 3; ++i)
			{
				visualContext.SetUniform(posUniformNames[i], camera.Position[i]);
				visualContext.SetUniform(rotUniformNames[i], camera.Rotation[i]);
			}
		}

		private FlyCamera camera = new FlyCamera();
		private AdapterCamera adapter;
		private FormCamera formCamera = new FormCamera();
		private string[] posUniformNames = { "iCamPosX", "iCamPosY", "iCamPosZ" };
		private string[] rotUniformNames = { "iCamRotX", "iCamRotY", "iCamRotZ" };
	}
}
