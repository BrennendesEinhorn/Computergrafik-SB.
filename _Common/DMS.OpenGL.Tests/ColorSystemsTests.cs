using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace DMS.OpenGL.Tests
{
	[TestClass()]
	public class ColorSystemsTests
	{
		[TestMethod()]
		public void Hsb2rgbTestBlack1()
		{

			var rgb = ColorSystems.Hsb2rgb(0, 0, 0);
			var expected = Vector3.Zero; //black
			Assert.AreEqual(expected, rgb);
		}

		[TestMethod()]
		public void Hsb2rgbTestBlack2()
		{

			var rgb = ColorSystems.Hsb2rgb(1, 0, 0);
			var expected = Vector3.Zero; //black
			Assert.AreEqual(expected, rgb);
		}

		[TestMethod()]
		public void Hsb2rgbTestBlack3()
		{

			var rgb = ColorSystems.Hsb2rgb(1, 1, 0);
			var expected = Vector3.Zero; //black
			Assert.AreEqual(expected, rgb);
		}

		[TestMethod()]
		public void Hsb2rgbTestWhite()
		{

			var rgb = ColorSystems.Hsb2rgb(0, 0, 1);
			var expected = Vector3.One; //white
			Assert.AreEqual(expected, rgb);
		}

		[TestMethod()]
		public void Hsb2rgbTestColor()
		{

			var rgb = ColorSystems.Hsb2rgb(0, 0.2f, 1);
			var expected = new Vector3(1, 0.8f, 0.8f);
			Assert.AreEqual(expected, rgb);
		}

		[TestMethod()]
		public void Hsb2rgbTestRed()
		{

			var rgb = ColorSystems.Hsb2rgb(0, 1, 1);
			var expected = Vector3.UnitX; //red
			Assert.AreEqual(expected, rgb);
		}

		[TestMethod()]
		public void Hsb2rgbTestYellow()
		{
			var rgb = ColorSystems.Hsb2rgb(1.0f / 6.0f, 1, 1);
			var expected = new Vector3(1, 1, 0);
			Assert.AreEqual(expected, rgb);
		}

		[TestMethod()]
		public void Hsb2rgbTestGreen()
		{
			var rgb = ColorSystems.Hsb2rgb(2.0f / 6.0f, 1, 1);
			var expected = Vector3.UnitY;
			Assert.AreEqual(expected, rgb);
		}

		[TestMethod()]
		public void Hsb2rgbCyan()
		{
			var rgb = ColorSystems.Hsb2rgb(3.0f / 6.0f, 1, 1);
			var expected = new Vector3(0, 1, 1);
			Assert.AreEqual(expected, rgb);
		}

		[TestMethod()]
		public void Hsb2rgbBlue()
		{
			var rgb = ColorSystems.Hsb2rgb(4.0f / 6.0f, 1, 1);
			var expected = new Vector3(0, 0, 1);
			Assert.AreEqual(expected, rgb);
		}

		[TestMethod()]
		public void Hsb2rgbMagenta()
		{
			var rgb = ColorSystems.Hsb2rgb(5.0f / 6.0f, 1, 1);
			var expected = new Vector3(1, 0, 1);
			Assert.AreEqual(expected, rgb);
		}
	}
}