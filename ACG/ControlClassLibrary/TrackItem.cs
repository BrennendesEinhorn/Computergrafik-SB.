namespace ControlClassLibrary
{
	public class TrackItem : ITrackItem
	{
		public TrackItem(string label, float start, float length, int track)
		{
			Label = label;
			Start = start;
			Length = length;
			Track = track;
		}

		public string Label	{ get; set; }
		public float Start { get; set; }
		public float Length { get; set; }
		public int Track { get; set; }
	}
}
