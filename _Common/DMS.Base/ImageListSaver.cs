using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace DMS.Base
{
	public static class ImageListSaver
	{
		public static void SaveToDefaultDir(this IEnumerable<Bitmap> imageList)
		{
			var saveDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			saveDirectory += Path.DirectorySeparatorChar;
			saveDirectory += DateTime.Now.ToString("yyyyMMdd HHmmss");
			saveDirectory += Path.DirectorySeparatorChar;
			Directory.CreateDirectory(saveDirectory);
			int fileNumber = 0;
			foreach (var bmp in imageList)
			{
				//todo: why null Images?
				bmp?.Save(saveDirectory + fileNumber.ToString("00000") + ".png");
				++fileNumber;
			}

		}
	}
}
