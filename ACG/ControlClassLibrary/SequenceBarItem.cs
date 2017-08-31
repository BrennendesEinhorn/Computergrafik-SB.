namespace ControlClassLibrary
{
	public class SequenceBarItem : ISequenceBarItem
	{
		public SequenceBarItem(string label, object data, float ratio)
		{
			Label = label;
			Ratio = ratio;
			Data = data;
		}

		public object Data { get; private set; }

		public string Label { get; private set; }

		public float Ratio { get; set; }
	};
}
