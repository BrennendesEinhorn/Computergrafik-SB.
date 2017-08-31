using DMS.Application;
using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Example
{
	class Controller
	{
		private List<Collider> colliders = new List<Collider>();
		private Box2D windowBorders = new Box2D(-1.0f, -1.0f, 2.0f, 2.0f);
		private CollisionGrid collisionGrid;
		private Stopwatch time = new Stopwatch();
		private double lastBenchmark = 0;

		private Controller()
		{
			SetupColliders();
			time.Start();
		}

		private void SetupColliders()
		{
			float delta = 0.03f;
			float space = 0.01f;
			float distance2 = space / 2;
			float size = delta - space;
			int i = 0;
			for (float x =  -0.9f; x < 0.9f; x += delta)
			{
				for (float y = -0.9f; y < 0.9f; y += delta)
				{
					var collider = new Collider(x, y, size, size);
					collider.Velocity = RandomVectors.Velocity();
					colliders.Add(collider);
					++i;
				}
			}
			float scale = 2f;
			collisionGrid = new CollisionGrid(windowBorders, size * scale, size * scale);
		}

		private void Update(float updatePeriod)
		{
			//movement
			foreach (var collider in colliders)
			{
				collider.SaveBox();
				collider.Box.X += collider.Velocity.X * updatePeriod;
				collider.Box.Y += collider.Velocity.Y * updatePeriod;
				if (!collider.Box.Inside(windowBorders))
				{
					collider.Box.PushXRangeInside(windowBorders);
					collider.Box.PushYRangeInside(windowBorders);
					collider.Velocity = -collider.Velocity;
				}
			}

			var t1 = time.Elapsed.TotalMilliseconds; //get time before collision detection
			//handle collisions
			if(Keyboard.GetState().IsKeyDown(Key.Space))
			{
				BruteForceCollision();
			}
			else
			{
				GridCollision();
			}
			var t2 = time.Elapsed.TotalMilliseconds; //get time after collision detection

			if (t2 > lastBenchmark + 500.0)
			{
				Console.WriteLine((t2 - t1).ToString());
				lastBenchmark = t2;
			}
		}

		private void BruteForceCollision()
		{
			for (int i = 0; i < colliders.Count; ++i)
			{
				for (int j = i + 1; j < colliders.Count; ++j)
				{
					 HandleNarrowPhaseCollision(colliders[i], colliders[j]);
				}
			}
		}

		private void GridCollision()
		{
			collisionGrid.FindAllCollisions(colliders, (c1, c2) => HandleNarrowPhaseCollision(c1 as Collider, c2 as Collider));
		}

		private void HandleNarrowPhaseCollision(Collider collider1, Collider collider2)
		{
			var box1 = collider1.Box;
			var box2 = collider2.Box;
			if (box1.Intersects(box2))
			{
				//undo movement
				collider1.RestoreSavedBox();
				collider2.RestoreSavedBox();
				////set random velocity
				collider1.Velocity = RandomVectors.Velocity();
				collider2.Velocity = RandomVectors.Velocity();
			}
		}

		private void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			foreach (var collider in colliders)
			{
				GL.Color3(collider.Color);
				DrawBox(collider.Box);
			}
		}
		
		private void DrawBox(Box2D rect)
		{
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(rect.X, rect.Y);
			GL.Vertex2(rect.MaxX, rect.Y);
			GL.Vertex2(rect.MaxX, rect.MaxY);
			GL.Vertex2(rect.X, rect.MaxY);
			GL.End();
		}

		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			var controller = new Controller();
			app.Render += controller.Render;
			app.Update += controller.Update;
			app.Run();
		}
	}
}