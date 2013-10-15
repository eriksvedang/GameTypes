using System;
using NUnit.Framework;
using GameTypes;

namespace GameTypes.tests
{
	[TestFixture]
	public class IntPointTests
	{
		[SetUp]
		public void Setup ()
		{
		}
		
		[Test]
		public void ConvertDirectionToIntPoint() 
		{
			Assert.AreEqual(new IntPoint(0, 1), IntPoint.DirectionToIntPoint(Direction.UP));
			Assert.AreEqual(new IntPoint(1, 0), IntPoint.DirectionToIntPoint(Direction.RIGHT));
			Assert.AreEqual(new IntPoint(0, -1), IntPoint.DirectionToIntPoint(Direction.DOWN));
			Assert.AreEqual(new IntPoint(-1, 0), IntPoint.DirectionToIntPoint(Direction.LEFT));
		}
		
		[Test]
		public void ConvertIntPointToDegrees() 
		{
			Assert.AreEqual(90f, IntPoint.Right.Degrees(), 0.25f);
			Assert.AreEqual(180f, IntPoint.Down.Degrees(), 0.25f);
			Assert.AreEqual(270f, IntPoint.Left.Degrees(), 0.25f);
		}
		
		[Test]
		public void RotateIntPoints() 
		{
			Assert.AreEqual(IntPoint.Up, IntPoint.Right.RotatedWithDegrees(-90.0f));
			Assert.AreEqual(IntPoint.Left, IntPoint.Down.RotatedWithDegrees(90.0f));
			Assert.AreEqual(IntPoint.Right, IntPoint.Left.RotatedWithDegrees(180));
		}
		
		[Test]
		public void SubtractIntPoints() 
		{
			IntPoint p1 = new IntPoint(0, 0);
			IntPoint p2 = new IntPoint(1, 0);
			IntPoint p3 = new IntPoint(0, -1);
			IntPoint p4 = new IntPoint(0, -1);
			
			Assert.AreEqual(new IntPoint(1, 0), p2 - p1);
			Assert.AreEqual(new IntPoint(1, 1), p2 - p3);
			Assert.AreEqual(new IntPoint(0, 1), p1 - p4);
		}
	}
}

