using System;
using NUnit.Framework;
using GameTypes;

namespace GameTypes.tests
{
	[TestFixture]
	public class RandomizerTests
	{
		[SetUp]
		public void Setup ()
		{
		}
		
		[Test]
		public void RandomValue() 
		{
			for(int i = 0; i < 100; i++) {
				float r = Randomizer.GetValue(5f, 7f);
				//Console.WriteLine(r);
				Assert.Greater(r, 4.9f);
				Assert.Less(r, 7.0f);
			}
		}
	}
}
