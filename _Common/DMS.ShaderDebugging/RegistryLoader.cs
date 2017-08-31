using Microsoft.Win32;
using OpenTK;
using System;

namespace DMS.ShaderDebugging
{
	public static class RegistryLoader
	{
		public static void LoadLayout(this GameWindow window)
		{
			RegistryKey keyApp = RegistryLoaderForm.GetAppKey();
			if (ReferenceEquals(null, keyApp)) return;
			var key = keyApp.CreateSubKey("GameWindow");
			if (ReferenceEquals(null, key)) return;
			window.WindowState = (WindowState)Convert.ToInt32(key.GetValue("WindowState", (int)window.WindowState));
			window.Visible = Convert.ToBoolean(key.GetValue("visible", window.Visible));
			window.Width = Convert.ToInt32(key.GetValue("Width", window.Width));
			window.Height = Convert.ToInt32(key.GetValue("Height", window.Height));
			window.X = Convert.ToInt32(key.GetValue("X", window.X));
			window.Y = Convert.ToInt32(key.GetValue("Y", window.Y));
		}

		public static void SaveLayout(this GameWindow window)
		{
			RegistryKey keyApp = RegistryLoaderForm.GetAppKey();
			if (ReferenceEquals(null, keyApp)) return;
			var key = keyApp.CreateSubKey("GameWindow");
			if (ReferenceEquals(null, key)) return;
			key.SetValue("WindowState", (int)window.WindowState);
			key.SetValue("visible", window.Visible);
			key.SetValue("Width", window.Width);
			key.SetValue("Height", window.Height);
			key.SetValue("X", window.X);
			key.SetValue("Y", window.Y);
		}

		public static object LoadValue(string keyName, string name, object defaultValue)
		{
			RegistryKey keyApp = RegistryLoaderForm.GetAppKey();
			if (ReferenceEquals(null, keyApp)) return null;
			var key = keyApp.CreateSubKey(keyName);
			if (ReferenceEquals(null, key)) return null;
			return key.GetValue(name, defaultValue);
		}

		public static void SaveValue(string keyName, string name, object value)
		{
			RegistryKey keyApp = RegistryLoaderForm.GetAppKey();
			if (ReferenceEquals(null, keyApp)) return;
			var key = keyApp.CreateSubKey(keyName);
			if (ReferenceEquals(null, key)) return;
			key.SetValue(name, value);
		}
	}
}
