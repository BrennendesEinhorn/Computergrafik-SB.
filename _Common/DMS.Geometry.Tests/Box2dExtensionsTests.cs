using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace DMS.Geometry.Tests
{
	[TestClass()]
	public class Box2dExtensionsTests
	{
		[TestMethod()]
		public void TransformCenterTestTranslate()
		{
			var a = new Box2D(0, 0, 2, 4);
			var m = Matrix3x2.CreateTranslation(-1, 1);
			var expectedA = new Box2D(-1, 1, a.SizeX, a.SizeY);
			a.TransformCenter(m);
			Assert.AreEqual(expectedA, a);
		}

		[TestMethod()]
		public void TransformCenterTestNull()
		{
			var a = new Box2D(-1, -2, 2, 4);
			var m = new Matrix3x2();
			var expectedA = new Box2D(a);
			a.TransformCenter(m);
			Assert.AreEqual(expectedA, a);
		}

		[TestMethod()]
		public void TransformCenterTestIdentity()
		{
			var a = new Box2D(1, -2, 3, 4);
			var m = Matrix3x2.Identity;
			var expectedA = new Box2D(a);
			a.TransformCenter(m);
			Assert.AreEqual(expectedA, a);
		}

		[TestMethod()]
		public void TransformCenterTestScale()
		{
			var a = new Box2D(-1, -2, 2, 4);
			var m = Matrix3x2.CreateScale(3);
			var expectedA = new Box2D(a);
			a.TransformCenter(m);
			Assert.AreEqual(expectedA, a);
		}

		[TestMethod()]
		public void TransformCenterTestScale2()
		{
			var a = new Box2D(0, 0, 2, 4);
			var m = Matrix3x2.CreateScale(3);
			var expectedA = new Box2D(2, 4, a.SizeX, a.SizeY);
			a.TransformCenter(m);
			Assert.AreEqual(expectedA, a);
		}

		[TestMethod()]
		public void UndoOverlapTestNoOverlap()
		{
			var a = new Box2D(-1, -3, 1, 2);
			var b = new Box2D(0, -1, 1, 1);
			var expectedA = new Box2D(a);
			a.UndoOverlap(b);
			Assert.AreEqual(expectedA, a);
			Assert.IsFalse(a.Intersects(b));
		}

		[TestMethod()]
		public void UndoOverlapTest2()
		{
			var aX = -1f;
			var a = new Box2D(aX + 0.1f, 0, 2, 2);
			var b = new Box2D(1, 0, 2, 2);
			a.UndoOverlap(b);
			Assert.AreEqual(aX, a.X);
			Assert.IsFalse(a.Intersects(b));
		}

		[TestMethod()]
		public void UndoOverlapTest3()
		{
			var a = new Box2D(0, 0, 0, 0);
			var b = new Box2D(a);
			a.UndoOverlap(b);
			Assert.AreEqual(b, a);
			Assert.IsFalse(a.Intersects(b));
		}

		[TestMethod()]
		public void UndoOverlapTest4()
		{
			var a = new Box2D(-1, -1, 2, 3);
			var b = new Box2D(a);
			var expectedA = new Box2D(a);
			expectedA.X += 2f;
			a.X += 0.01f; //to make it unambiguous in which direction to push
			a.UndoOverlap(b);
			Assert.AreEqual(expectedA, a);
			Assert.IsFalse(a.Intersects(b));
		}

		[TestMethod()]
		public void UndoOverlapTest5()
		{
			var a = new Box2D(-1, -1, 3, 2);
			var b = new Box2D(a);
			var expectedA = new Box2D(a);
			expectedA.Y += 2f;
			a.Y += 0.01f; //to make it unambiguous in which direction to push
			a.UndoOverlap(b);
			Assert.AreEqual(expectedA, a);
			Assert.IsFalse(a.Intersects(b));
		}

		[TestMethod()]
		public void UndoOverlapTest6()
		{
			var a = new Box2D(-1, -1, 2, 3);
			var b = new Box2D(a);
			var expectedA = new Box2D(a);
			expectedA.X -= 2f;
			a.X -= 0.01f; //to make it unambiguous in which direction to push
			a.UndoOverlap(b);
			Assert.AreEqual(expectedA, a);
			Assert.IsFalse(a.Intersects(b));
		}

		[TestMethod()]
		public void UndoOverlapTest7()
		{
			var a = new Box2D(-1, -1, 3, 2);
			var b = new Box2D(a);
			var expectedA = new Box2D(a);
			expectedA.Y = -3f;
			a.Y -= 0.01f; //to make it unambiguous in which direction to push
			a.UndoOverlap(b);
			Assert.AreEqual(expectedA, a);
			Assert.IsFalse(a.Intersects(b));
		}
		[TestMethod()]
		public void UndoOverlapTest8()
		{
			var a = new Box2D(-1, -1, 3, 2);
			var b = new Box2D(-4, -4, 10, 10);
			var expectedA = new Box2D(a);
			expectedA.Y = -6f;
			a.UndoOverlap(b);
			Assert.AreEqual(expectedA, a);
			Assert.IsFalse(a.Intersects(b));
		}

		[TestMethod()]
		public void PushXRangeInsideTest()
		{
			var a = new Box2D(-0.1f, 0, 0.5f, 0.5f);
			var b = new Box2D(0, 0, 2, 2);
			var expectedA = new Box2D(a);
			expectedA.X = 0;
			a.PushXRangeInside(b);
			Assert.AreEqual(expectedA, a);
			Assert.IsTrue(a.Intersects(b));
		}

		[TestMethod()]
		public void PushXRangeInsideTest2()
		{
			var a = new Box2D(1.6f, 0, 0.5f, 0.5f);
			var b = new Box2D(0, 0, 2, 2);
			var expectedA = new Box2D(a);
			expectedA.X = 1.5f;
			a.PushXRangeInside(b);
			Assert.AreEqual(expectedA, a);
			Assert.IsTrue(a.Intersects(b));
		}

		[TestMethod()]
		public void PushYRangeInsideTest()
		{
			var a = new Box2D(0, -0.1f, 0.5f, 0.5f);
			var b = new Box2D(0, 0, 2, 2);
			var expectedA = new Box2D(a);
			expectedA.Y = 0;
			a.PushYRangeInside(b);
			Assert.AreEqual(expectedA, a);
			Assert.IsTrue(a.Intersects(b));
		}

		[TestMethod()]
		public void PushYRangeInsideTest2()
		{
			var a = new Box2D(0, 1.6f, 0.5f, 0.5f);
			var b = new Box2D(0, 0, 2, 2);
			var expectedA = new Box2D(a);
			expectedA.Y = 1.5f;
			a.PushYRangeInside(b);
			Assert.AreEqual(expectedA, a);
			Assert.IsTrue(a.Intersects(b));
		}
	}
}