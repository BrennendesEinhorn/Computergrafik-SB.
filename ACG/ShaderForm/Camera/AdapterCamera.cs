using System.ComponentModel;

namespace ShaderForm.Camera
{
	public class AdapterCamera : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public float PositionX { get { return cam.Position.X; } set { cam.Position.X = value; Update(); } }
		public float PositionY { get { return cam.Position.Y; } set { cam.Position.Y = value; Update(); } }
		public float PositionZ { get { return cam.Position.Z; } set { cam.Position.Z = value; Update(); } }
		public float RotationX { get { return cam.Rotation.X; } set { cam.Rotation.X = value; Update(); } }
		public float RotationY { get { return cam.Rotation.Y; } set { cam.Rotation.Y = value; Update(); } }
		public float RotationZ { get { return cam.Rotation.Z; } set { cam.Rotation.Z = value; Update(); } }
		public float Speed { get { return cam.Speed; } set { cam.Speed = value; Update(); } }

		public AdapterCamera(FlyCamera cam)
		{
			this.cam = cam;
		}

		private FlyCamera cam;

		private void Update()
		{
			PropertyChanged?.Invoke(this, null);
		}

		internal void Reset()
		{
			cam.Reset();
		}
	}
}
