using ControlClassLibrary;
using ShaderForm.Interfaces;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShaderForm.Graph
{
	public class FacadeKeyframesVisualisation
	{
		public delegate void ChangedPositionHandler(float position);
		public event ChangedPositionHandler ChangedPosition;
		public event KeyEventHandler KeyDown;

		public FacadeKeyframesVisualisation(string uniformName, IKeyFrames kfs)
		{
			formGraph = new FormGraph(uniformName);
			formGraph.ChangedPoints += FormGraph_OnChangePoints;
			formGraph.ChangedPosition += (pos) => { ChangedPosition?.Invoke((float)pos); };
			formGraph.KeyDown += (sender, args) => { KeyDown?.Invoke(sender, args); };
			formGraph.CopyCommand += (sender, args) => { if (!ReferenceEquals(null,  kfs)) kfs.CopyKeyframesToClipboard(); };
			formGraph.PasteCommand += (sender, args) => { if (!ReferenceEquals(null,  kfs)) kfs.PasteKeyframesFromClipboard(); };
			currentUniform = uniformName;
			this.kfs = kfs;
			Update();
		}

		public bool IsYourForm(object sender)
		{
			return sender == formGraph;
		}

		public void AddInterpolatedKeyframe(float position)
		{
			if (ReferenceEquals(null,  kfs)) return;
			var value = kfs.Interpolate(position);
			kfs.AddUpdate(position, value);
		}

		public void Close()
		{
			SaveData();
			formGraph.Close();
		}

		public void Update()
		{
			if (!updating)
			{
				formGraph.SetPoints(kfs);
			}
		}

		public void SaveData()
		{
			formGraph.SaveLayout();
		}

		public void Show()
		{
			formGraph.Visible = true;
			Update();
		}

		public bool IsVisible { get { return formGraph.Visible; } }

		public void UpdatePosition(float position)
		{
			formGraph.UpdatePosition(position);
		}

		private FormGraph formGraph;
		private IKeyFrames kfs;
		private bool updating = false;
		private string currentUniform;

		private void FormGraph_OnChangePoints(IEnumerable<KeyValuePair<float, float>> points)
		{
			if (ReferenceEquals(null,  kfs)) return;
			updating = true;
			kfs.Clear();
			foreach (var p in points)
			{
				kfs.AddUpdate(p.Key, p.Value);
			}
			updating = false;
		}
	}
}
