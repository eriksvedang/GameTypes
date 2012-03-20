using System;
using NUnit.Framework;
using GameTypes;

namespace GameTypes.tests
{
	[TestFixture]
	public class GridMathTests
	{
		[SetUp]
		public void Setup ()
		{
		}
		
		[Test]
		public void DegreesToDirection() 
		{
			Assert.AreEqual(Direction.UP, GridMath.DegreesToDirection(0));
			Assert.AreEqual(Direction.RIGHT, GridMath.DegreesToDirection(90));
			Assert.AreEqual(Direction.DOWN, GridMath.DegreesToDirection(180));
			Assert.AreEqual(Direction.LEFT, GridMath.DegreesToDirection(270));
		}
	}
}

