using DMS.Geometry;
using OpenTK.Input;
using OpenTK.Platform;
using System;

namespace DMS.Application
{
	public static class GameWindowExtensions
	{
		public static void ConnectEvents(this IGameWindow gameWindow, CameraOrbit camera)
		{
			gameWindow.Resize += (s, e) => camera.Aspect = (float)gameWindow.Width / gameWindow.Height;
			gameWindow.MouseMove += (s, e) =>
			{
				if (ButtonState.Pressed == e.Mouse.LeftButton)
				{
					camera.Azimuth += 300 * e.XDelta / (float)gameWindow.Width;
					camera.Elevation += 300 * e.YDelta / (float)gameWindow.Height;
				}
			};
			gameWindow.MouseWheel += (s, e) =>
			{
				if (Keyboard.GetState().IsKeyDown(Key.ShiftLeft))
				{
					camera.FovY *= (float)Math.Pow(1.05, e.DeltaPrecise);
				}
				else
				{
					camera.Distance *= (float)Math.Pow(1.05, e.DeltaPrecise);
				}
			};
		}
	}
}
