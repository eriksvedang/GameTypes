using System;

namespace GameTypes
{
    public struct Float3
    {
        public float x;
        public float y;
        public float z;
        public Float3( float pX, float pY, float pZ )
        {
            x = pX;
            y = pY;
            z = pZ;
        }
		
		public override string ToString()
		{
			return string.Format("Float3({0}, {1}, {2})", x, y, z);
		}
    }
}
