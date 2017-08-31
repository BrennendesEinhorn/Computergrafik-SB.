namespace MvcSokoban
{
	public class Level : ILevel
	{
		public delegate void EventHandlerTypeChange(int x, int y, ElementType newType);
		public event EventHandlerTypeChange OnTypeChange;

		public Level(int width, int height)
		{
			this.Width = width;
			this.Height = height;
			arrTile = new ElementType[Width, Height];
		}

		public ElementType GetElement(int x, int y)
		{
			return arrTile[x, y];
		}

		public void SetElement(int x, int y, ElementType value)
		{
			RaiseOnTypeChange(x, y, value);
			arrTile[x, y] = value;
		}

		public int Height { get; private set; }

		public int Width { get; private set; }

		private ElementType[,] arrTile;

		private void RaiseOnTypeChange(int x, int y, ElementType newType)
		{
			if (!ReferenceEquals(null,  OnTypeChange))
			{
				OnTypeChange(x, y, newType);
			}
		}
	}
}
