using DMS.TimeTools;
using DMS.Geometry;
using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
	public class GameLogic
	{
		public event EventHandler OnShoot;
		public event EventHandler OnEnemyDestroy;
		public event EventHandler OnLost;

		private Box2D player = new Box2D(0.0f, -1.0f, 0.0789f, 0.15f);
		private List<Box2D> enemies = new List<Box2D>();
		private List<Box2D> bullets = new List<Box2D>();
		private PeriodicUpdate shootCoolDown = new PeriodicUpdate(0.1f);
		private float enemySpeed = 0.05f;

		public GameLogic()
		{
			shootCoolDown.PeriodElapsed += (s, t) => shootCoolDown.Stop();
			CreateEnemies();
		}

		public void Update(float absoluteTime, float axisUpDown, bool shoot)
		{
			if (Lost) return;
			shootCoolDown.Update(absoluteTime);
			//remove outside bullet - lazy remove
			foreach (Box2D bullet in bullets)
			{
				if (bullet.Y > 1.0f)
				{
					bullets.Remove(bullet);
					return;
				}
			}
			HandleCollisions();

			var timeDelta = absoluteTime - lastUpdateTime;
			lastUpdateTime = absoluteTime;
			UpdatePlayer(absoluteTime, timeDelta, axisUpDown, shoot);
			MoveEnemies(timeDelta);
			MoveBullets(timeDelta);

			if (0 == enemies.Count && 0 == bullets.Count)
			{
				//game is won -> start new, but faster
				CreateEnemies();
				enemySpeed += 0.05f;
			}
		}

		public IEnumerable<Box2D> Enemies { get { return enemies; } }

		public IEnumerable<Box2D> Bullets { get { return bullets; } }

		public Box2D Player { get { return player; } }

		private bool Lost { get; set; }
		private float lastUpdateTime = 0.0f;

		private void CreateEnemies()
		{
			//create enemies
			for (float y = 0.1f; y < 1.0f; y += 0.2f)
			{

				for (float x = -0.85f; x < 0.9f; x += 0.2f)
				{
					enemies.Add(new Box2D(x, y, 0.06f, 0.1f));
				}
			}
		}
		
		private void UpdatePlayer(float absoluteTime, float timeDelta, float axisUpDown, bool shoot)
		{
			player.X += timeDelta * axisUpDown;
			//limit player position [left, right]
			player.X = Math.Min(1.0f - player.SizeX, Math.Max(-1.0f, player.X));

			if (shoot && !shootCoolDown.Enabled)
			{
				OnShoot?.Invoke(this, null);
				bullets.Add(new Box2D(player.X, player.Y, 0.02f, 0.04f));
				bullets.Add(new Box2D(player.MaxX, player.Y, 0.02f, 0.04f));
				shootCoolDown.Start(absoluteTime);
			}
		}

		private void HandleCollisions()
		{
			//intersections
			foreach (Box2D enemy in enemies)
			{
				if (enemy.Y < - 0.8f)
				{
					//game lost
					Lost = true;
					if (!ReferenceEquals(null, OnLost)) OnLost(this, null);
				}
				foreach (Box2D bullet in bullets)
				{
					if (bullet.Intersects(enemy))
					{
						//delete bullet and enemy
						OnEnemyDestroy?.Invoke(this, null);
						bullets.Remove(bullet);
						enemies.Remove(enemy);
						return;
					}
				}
			}
		}

		private void MoveEnemies(float timeDelta)
		{
			foreach (Box2D enemy in enemies)
			{
				enemy.Y -= enemySpeed * timeDelta;
			}
		}

		private void MoveBullets(float timeDelta)
		{
			foreach (Box2D bullet in bullets)
			{
				bullet.Y += timeDelta;
			}
		}
	}
}
