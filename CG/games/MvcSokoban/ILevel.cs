namespace MvcSokoban
{
	public enum ElementType { Floor = 0, Wall = 10, Man = 1, Box = 2, Goal = 4, BoaxOnGoal = 7, ManOnGoal = 5};

	public interface ILevel
	{
		int Height { get; }
		int Width { get; }

		ElementType GetElement(int x, int y);
	}
}