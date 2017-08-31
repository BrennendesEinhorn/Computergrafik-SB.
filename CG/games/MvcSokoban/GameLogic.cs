using System.Drawing;

namespace MvcSokoban
{
	class GameLogic
	{
		public enum Movement { NONE = 0, UP, DOWN, LEFT, RIGHT };

		private Point playerPos;
		private Level level;

		public GameLogic(Level level)
		{
			this.level = level;
			Point? playerPos = level.FindPlayerPos();
			if (playerPos.HasValue)
			{
				this.playerPos = playerPos.Value;
			}
		}

		public ILevel GetLevel() { return level; }

		public void Update(Movement movement)
		{
			UpdateMovables(movement);
		}

		private void UpdateMovables(Movement movement)
		{
			Point newPlayerPos = playerPos;
			newPlayerPos = CalcNewPosition(newPlayerPos, movement);
			ElementType type = level.GetElement(newPlayerPos.X, newPlayerPos.Y);
			if (ElementType.Wall == type) return;
			if (ElementType.Box == type ||ElementType.BoaxOnGoal == type)
			{
				//box will be moved
				Point newBoxPos = CalcNewPosition(newPlayerPos, movement);
				ElementType type2 = level.GetElement(newBoxPos.X, newBoxPos.Y);
				//is new box position invalid
				if (ElementType.Floor != type2 && ElementType.Goal != type2) return;
				//moving box
				level.MoveBox(newPlayerPos, newBoxPos);
			}
			Point oldPlayerPos = playerPos;
			playerPos = newPlayerPos;
			level.MovePlayer(oldPlayerPos, playerPos);
		}

		private static Point CalcNewPosition(Point pos, Movement movement)
		{
			Point newPos = pos;
			switch (movement)
			{
				case Movement.DOWN: newPos = new Point(pos.X, pos.Y - 1); break;
				case Movement.UP: newPos = new Point(pos.X, pos.Y + 1); break;
				case Movement.LEFT: newPos = new Point(pos.X - 1, pos.Y); break;
				case Movement.RIGHT: newPos = new Point(pos.X + 1, pos.Y); break;
			}

			return newPos;
		}
	}
}
