namespace ControlClassLibrary
{
	public interface ISequenceBarItem
	{
		string Label { get; }
		object Data { get; }
		float Ratio { get; }
	}
}