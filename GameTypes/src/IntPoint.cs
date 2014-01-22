using System;

namespace GameTypes
{
    [System.Flags]
    public enum Direction : int
    {
        ZERO            = 0x000,
        RIGHT           = 0x001,
        UP_RIGHT        = 0x002,
        UP              = 0x004,
        UP_LEFT         = 0x008,
        LEFT            = 0x010,
        DOWN_LEFT       = 0x020,
        DOWN            = 0x040,
        DOWN_RIGHT      = 0x080
    }
	
    [Serializable]
	public struct IntPoint : IPoint
    {
        public int x;
        public int y;
        public IntPoint( int pX, int pY )
        {
            x = pX;
            y = pY;
        }

        public float EuclidianDistanceTo( IntPoint pOtherPoint )
        {
            IntPoint deltaPoint = pOtherPoint - this;
            return (float)Math.Sqrt(deltaPoint.x * deltaPoint.x + deltaPoint.y * deltaPoint.y);
        }

        public int ManhattanDistanceTo( IntPoint pOtherPoint )
        {
            IntPoint deltaPoint = pOtherPoint - this;
            deltaPoint.x = deltaPoint.x < 0 ? -deltaPoint.x : deltaPoint.x;
            deltaPoint.y = deltaPoint.y < 0 ? -deltaPoint.y : deltaPoint.y;
            return deltaPoint.x + deltaPoint.y;
        }

        // UNITY unit circle is reversed
        public static readonly IntPoint Zero = new IntPoint(0, 0);
        public static readonly IntPoint Up = new IntPoint(0, 1);
        public static readonly IntPoint Right = new IntPoint(1, 0);
        public static readonly IntPoint Left = new IntPoint(-1, 0);
        public static readonly IntPoint Down = new IntPoint(0, -1);
        public static readonly IntPoint UpRight = new IntPoint(1, 1);
        public static readonly IntPoint UpLeft = new IntPoint(-1, 1);
        public static readonly IntPoint DownRight = new IntPoint(1, -1);
        public static readonly IntPoint DownLeft = new IntPoint(-1, -1);
        public static readonly IntPoint One = new IntPoint(1, 1);

        public static IntPoint DirectionToIntPoint(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    return Up;
                case Direction.DOWN:
                    return Down;
                case Direction.RIGHT:
                    return Right;
                case Direction.LEFT:
                    return Left;
                case Direction.UP_RIGHT:
                    return UpRight;
                case Direction.UP_LEFT:
                    return UpLeft;
                case Direction.DOWN_RIGHT:
                    return DownRight;
                case Direction.DOWN_LEFT:
                    return DownLeft;
                default:
                    return Zero;
            }
        }

		public static Direction Turn(Direction pDirection, int pDegrees) {
			int degrees = (int)IntPoint.DirectionToIntPoint(pDirection).Degrees();
			degrees += pDegrees;	
			return GridMath.DegreesToDirection((int)degrees);
		}

        public Direction ToDirection()
        {
            if (Clamped() == IntPoint.Up ) { return Direction.UP; }
            else if (Clamped() == IntPoint.UpLeft ) { return Direction.UP_LEFT; }
            else if (Clamped() == IntPoint.UpRight) { return Direction.UP_RIGHT; }
            else if (Clamped() == IntPoint.Right) { return Direction.RIGHT; }
            else if (Clamped() == IntPoint.Left) { return Direction.LEFT; }
            else if (Clamped() == IntPoint.Down) { return Direction.DOWN; }
            else if (Clamped() == IntPoint.DownLeft) { return Direction.DOWN_LEFT; }
            else if (Clamped() == IntPoint.DownRight) { return Direction.DOWN_RIGHT; }
            else { return Direction.ZERO; }
        }
		
		public IntPoint Clamped()
		{
			int newX = 0;
			if(x >= 1) newX = 1;
			if(x <= -1) newX = -1;
			
			int newY = 0;
			if(y >= 1) newY = 1;
			if(y <= -1) newY = -1;
			
			return new IntPoint(newX, newY);
		}
		
		/*
        public static float Degrees(IntPoint pCenter, IntPoint pTarget)
        {
            pTarget -= pCenter;
            return (float)(Math.Atan2((double)pTarget.x, (double)pTarget.y) * 180 / Math.PI);
        }*/
		
		public float Degrees()
        {
			float deg = (float)(Math.Atan2((double)(-y), (double)(x)) * GridMath.RADIANS_TO_DEGREES) + 90f;
			if(deg < 0) { deg += 360.0f; }
            return deg;
        }

		public IntPoint RotatedWithDegrees (float pDegrees)
		{
			float otherWay = -pDegrees;
			float radians = GridMath.DEGREES_TO_RADIANS * otherWay;
			float oldAngle = (float)Math.Atan2(y, x);
			float newAngle = oldAngle + radians;
			
			return new IntPoint(
				(int) Math.Round(Math.Cos (newAngle)), 
				(int) Math.Round(Math.Sin (newAngle)));
		}
        
		public static IntPoint Max
        {
            get { return new IntPoint(int.MaxValue, int.MaxValue); }
        }
       
		public static IntPoint Min
        {
            get { return new IntPoint(int.MinValue, int.MinValue); }
        }
      
		public static IntPoint operator-( IntPoint pFirst, IntPoint pSecond )
        {
            return new IntPoint(pFirst.x - pSecond.x, pFirst.y - pSecond.y);
        }
       
		public static IntPoint operator+(IntPoint pFirst, IntPoint pSecond)
        {
            return new IntPoint(pFirst.x + pSecond.x, pFirst.y + pSecond.y);
        }
      
		public static IntPoint operator *(int pFirst, IntPoint pSecond)
        {
            return pSecond * pFirst;
        }
        
		public static IntPoint operator *(IntPoint pFirst, int pSecond)
        {
            return new IntPoint(pFirst.x * pSecond, pFirst.y * pSecond);
        }
       
		public static IntPoint operator /(IntPoint pFirst, int pSecond)
        {
            return new IntPoint(pFirst.x / pSecond, pFirst.y / pSecond);
        }
       
		public static bool operator ==(IntPoint pFirst, IntPoint pSecond)
        {
            return (pFirst.x == pSecond.x && pFirst.y == pSecond.y);
        }
       
		public static bool operator !=(IntPoint pFirst, IntPoint pSecond)
        {
            return !(pFirst == pSecond);
        }
    
		public override bool Equals(object obj)
        {
            if (!(obj is IntPoint))
                return false;
            return (IntPoint)obj == this;
        }
       
		public override int GetHashCode()
        {
            return BitCruncher.PackTwoShorts(x, y);
        }
        
		public override string ToString()
        {
            return "(" + x + "," + y + ")";
        }

		public IntPoint scale(float amount)
		{
			return new IntPoint((int)(x * amount), (int)(y * amount));
		}

        #region IPoint Members

        public float DistanceTo(IPoint pPoint)
        {
#if DEBUG
            D.assert(pPoint is IntPoint, "Must a point of the same type!");
#endif
            return EuclidianDistanceTo((IntPoint)pPoint);
        }

        #endregion

        public void Set(int pX, int pY)
        {
            x = pX; y = pY;
        }

    }
}
