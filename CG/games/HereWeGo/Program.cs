using DMS.Application;
using OpenTK.Input;
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
	class Controller
	{
		public Controller()
		{
			logic = new GameLogic();
			visual = new Visual();
			sound = new Sound();
			logic.OnShoot += (sender, args) => { sound.Shoot(); };
			logic.OnEnemyDestroy += (sender, args) => { sound.DestroyEnemy(); };
			logic.OnLost += (sender, args) => { sound.Lost(); };
			sound.Background();
			timeSource.Start();
		}

		private void Render()
		{
			visual.DrawScreen(logic.Enemies, logic.Bullets, logic.Player);
		}

		private void Update(float updatePeriod)
		{
			float axisLeftRight = Keyboard.GetState()[Key.Left] ? -1.0f : Keyboard.GetState()[Key.Right] ? 1.0f : 0.0f;
			bool shoot = Keyboard.GetState()[Key.Space];
			logic.Update((float)timeSource.Elapsed.TotalSeconds, axisLeftRight, shoot);
		}

		private GameLogic logic;
		private Visual visual;
		private Sound sound;
		private Stopwatch timeSource = new Stopwatch();

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