using System;
using NUnit.Framework;
using GameTypes;

namespace GameTypes.tests
{
	[TestFixture]
	public class GameTimeTests
	{
		[SetUp]
		public void Setup ()
		{

		}
		
		void SecondsAddsUp(float pTotalSeconds, int pExpectedDays, int pExpectedHours, int pExpectedMinutes, float pExpectedSeconds)
		{
			GameTime gt = new GameTime(pTotalSeconds);
			
			Assert.AreEqual(gt.days, pExpectedDays);
			Assert.AreEqual(gt.hours, pExpectedHours);
			Assert.AreEqual(gt.minutes, pExpectedMinutes);
			Assert.AreEqual(gt.seconds, pExpectedSeconds, 0.05f);
		}
		
		
		[Test]
		public void Zero()
        {
			SecondsAddsUp(0f, 0, 0, 0, 0f);
		}
		
		[Test]
		public void OneSecond()
        {
			SecondsAddsUp(1f, 0, 0, 0, 1f);
		}
		
		[Test]
		public void OneMinute()
        {
			SecondsAddsUp(60f, 0, 0, 1, 0f);
		}
		
		[Test]
		public void OneHour()
        {
			SecondsAddsUp(3600f, 0, 1, 0, 0f);
		}
		
		[Test]
		public void OneDay()
        {
			SecondsAddsUp(86400f, 1, 0, 0, 0f);
		}
		
		[Test]
		public void SomeComplexOnes()
        {
			SecondsAddsUp(3800f, 0, 1, 3, 20f);
		}
	
		[Test]
		public void Tick()
        {
			GameTime gt = new GameTime(0f);
			gt.Tick(1f);
			Assert.AreEqual(1f, gt.totalSeconds);
			gt.Tick(1f);
			Assert.AreEqual(2f, gt.totalSeconds);
		}
		
		
		[Test]
		public void Subtraction()
        {
			GameTime g1 = new GameTime(5, 4, 3, 2f);
			GameTime g2 = new GameTime(1, 1, 1, 1f);
			GameTime result = g1 - g2;
			Assert.AreEqual(4, result.days);
			Assert.AreEqual(3, result.hours);
			Assert.AreEqual(2, result.minutes);
			Assert.AreEqual(1f, result.seconds, 0.1f);
		}
	
	}
}

