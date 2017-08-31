using System.Drawing;

namespace MvcSokoban
{
	public static class LevelHelper
	{
		public static ElementType GetElement(this ILevel level, Point position)
		{
			return level.GetElement(position.X, position.Y);
		}

		public static void SetElement(this Level level, Point position, ElementType type)
		{
			level.SetElement(position.X, position.Y, type);
		}

		//public static bool IsGoal(this ILevel level, Point position)
		//{
		//	ElementType type = level.GetElement(position);
		//	return ElementType.GOAL == type || ElementType.BOX_ON_GOAL == type || ElementType.MAN_ON_GOAL == type;
		//}

		//public static bool IsFree(this ILevel level, Point position)
		//{
		//	ElementType type = level.GetElement(position);
		//	return ElementType.FLOOR == type || ElementType.GOAL == type;
		//}

		public static void MoveBox(this Level level, Point oldPos, Point newPos)
		{
			ElementType type = level.GetElement(oldPos);
			switch (type)
			{
				case ElementType.Box: level.SetElement(oldPos, ElementType.Floor); break;
				case ElementType.BoaxOnGoal: level.SetElement(oldPos, ElementType.Goal); break;
				default: return;
			}
			ElementType type2 = level.GetElement(newPos);
			switch (type2)
			{
				case ElementType.Floor: level.SetElement(newPos, ElementType.Box); break;
				case ElementType.Goal: level.SetElement(newPos, ElementType.BoaxOnGoal); break;
				default: return;
			}
		}

		public static void MovePlayer(this Level level, Point oldPos, Point newPos)
		{
			ElementType type = level.GetElement(oldPos);
			switch (type)
			{
				case ElementType.Man: level.SetElement(oldPos, ElementType.Floor); break;
				case ElementType.ManOnGoal: level.SetElement(oldPos, ElementType.Goal); break;
				default: return;
			}
			ElementType type2 = level.GetElement(newPos);
			switch (type2)
			{
				case ElementType.Floor: level.SetElement(newPos, ElementType.Man); break;
				case ElementType.Goal: level.SetElement(newPos, ElementType.ManOnGoal); break;
				default: return;
			}
		}

		public static Point? FindPlayerPos(this ILevel level)
		{
			for (int x = 0; x < level.Width; ++x)
			{
				for (int y = 0; y < level.Height; ++y)
				{
					ElementType type = level.GetElement(x, y);
					if (ElementType.Man == type || ElementType.ManOnGoal == type)
					{
						return new Point(x, y);
					}
				}
			}
			return null;
		}

		public static bool IsWon(this ILevel level)
		{
			for (int x = 0; x < level.Width; ++x)
			{
				for (int y = 0; y < level.Height; ++y)
				{
					ElementType type = level.GetElement(x, y);
					//if a single goal without a box is found the game is not yet won.
					if (ElementType.Goal == type || ElementType.ManOnGoal == type)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
