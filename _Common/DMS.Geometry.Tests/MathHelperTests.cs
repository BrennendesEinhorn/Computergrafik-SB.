using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

namespace DMS.Geometry.Tests
{
	[TestClass()]
	public class MathHelperTests
	{
		[TestMethod()]
		public void ToPolarTestVector0()
		{
			var a = new Vector2(0, 0);
			var expectedA = new Vector2(0, 0);
			Assert.AreEqual(expectedA, MathHelper.ToPolar(a));
		}

		[TestMethod()]
		public void ToPolarTest0Grad()
		{
			var a = new Vector2(1, 0);
			var expectedA = new Vector2(0, 1);
			Assert.AreEqual(expectedA, MathHelper.ToPolar(a));
		}

		[TestMethod()]
		public void ToPolarTest90Grad()
		{
			var a = new Vector2(0, 1);
			var expectedA = new Vector2((float)(0.5 * Math.PI), 1);
			Assert.AreEqual(expectedA, MathHelper.ToPolar(a));
		}

		[TestMethod()]
		public void ToPolarTest180Grad()
		{
			var a = new Vector2(-1, 0);
			var expectedA = new Vector2((float)(Math.PI), 1);
			Assert.AreEqual(expectedA, MathHelper.ToPolar(a));
		}


		[TestMethod()]
		public void ToPolarTest270Grad()
		{
			var a = new Vector2(0, -1);
			var expectedA = new Vector2((float)(- 1.0 / 2.0 * Math.PI), 1);
			Assert.AreEqual(expectedA, MathHelper.ToPolar(a));
		}

		[TestMethod()]
		public void ToPolarTest270Grad2()
		{
			var a = new Vector2(0, -2);
			var expectedA = new Vector2((float)(-1.0 / 2.0 * Math.PI), 2);
			Assert.AreEqual(expectedA, MathHelper.ToPolar(a));
		}
	}
}