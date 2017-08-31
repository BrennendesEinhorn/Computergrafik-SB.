using DMS.Application;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Reversi
{
	class Controller
	{
		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			var logic = new GameLogic(); //todo: load the game state
			var visual = new Visual();
			app.GameWindow.Closing += (s, e) => { /*todo: save the game state */ };
			app.Resize += (w, h) => visual.Resize(logic.GameState, w, h);
			app.Render += () =>
			{
				var score = "white:" + logic.CountWhite.ToString() + " black:" + logic.CountBlack.ToString(); ;
				app.GameWindow.Title = score;
				visual.Render(logic.GameState);
				if (GameLogic.Result.PLAYING != logic.CurrentGameResult)
				{
					var winner = logic.CurrentGameResult.ToString();
					visual.PrintMessage(winner);
				}
			};
			app.GameWindow.MouseDown += (s, e) =>
			{
				if (e.Button != MouseButton.Left) return; //only accept left mouse button
				var coord = app.CalcNormalized(e.X, e.Y); //convert mouse coordinates from pixel to [0,1]²
				var gridPos = visual.CalcGridPosFromNormalized(coord); //convert normalized mouse coordinates into grid coordinates
				logic.Move(gridPos); //do move
			};
			app.Run();
		}
	}
}