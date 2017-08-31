namespace ControlClassLibrary
{
	public interface ITrackItem
	{
		string Label { get; set; }
		float Length { get; set; }
		float Start { get; set; }
		int Track { get; set; }
	}
}