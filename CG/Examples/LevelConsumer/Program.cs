using DMS.Application;
using DMS.Base;
using LevelData;
using OpenTK.Input;
using System;
using System.IO;

namespace Example
{
	class Controller
	{
		[STAThread]
		private static void Main()
		{
			var app = new ExampleApplication();
			var controller = new Controller();
			var logic = new GameLogic();
			var renderer = new Renderer();
			logic.NewPosition += (name, x, y) => renderer.UpdateSprites(name, x, y);
			LoadLevelData(LevelData.level1, logic, renderer);

			app.Resize += (width, height) => renderer.Resize(width, height);
			app.Render += () => renderer.Render(logic.Bounds);
			app.Update += (updatePeriod) => HandleInput(updatePeriod, logic);
			app.Run();
		}

		private static void LoadLevelData(byte[] levelData, GameLogic logic, Renderer renderer)
		{
			using (var stream = new MemoryStream(levelData))
			{
				Level level = Serialize.ObjFromBinStream(stream) as Level;
				//set level bounds
				logic.Bounds = level.Bounds;
				//load colliders
				foreach (var collider in level.colliders)
				{
					logic.AddCollider(collider.Name, collider.Bounds);
				}
				//load sprites
				foreach (var sprite in level.Sprites)
				{
					renderer.AddSprite(sprite.Name, sprite.Layer, sprite.RenderBounds, sprite.TextureName, sprite.Bitmap);
				}
			}
		}

		private static void HandleInput(float updatePeriod, GameLogic logic)
		{
			float axisLeftRight = Keyboard.GetState()[Key.Left] ? -1.0f : Keyboard.GetState()[Key.Right] ? 1.0f : 0.0f;
			float axisUpDown = Keyboard.GetState()[Key.Down] ? -1.0f : Keyboard.GetState()[Key.Up] ? 1.0f : 0.0f;
			logic.Update(updatePeriod, axisLeftRight, axisUpDown);
		}
	}
}