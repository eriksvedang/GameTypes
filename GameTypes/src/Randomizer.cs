using System;

namespace GameTypes
{
	public class Randomizer {

		static Random _random;

		public static float GetValue(float pMin, float pMax) {
#if DEBUG
			if(pMin > pMax) {
				throw new ArgumentException("pMin > pMax");
			}
#endif
			if(_random == null) {
				int seed = DateTime.Now.Millisecond * DateTime.Now.Second * DateTime.Now.Minute;
				//Console.WriteLine("Seed: " + seed);
				_random = new Random(seed);
			}
			return pMin + ((pMax - pMin) * (float)_random.NextDouble());
		}
	}
}

