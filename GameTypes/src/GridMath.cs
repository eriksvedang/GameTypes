using System;

namespace GameTypes
{
    public class GridMath
    {
        public const float TWO_PI = 6.28318531f;
        public const float TWO_PI_INVERTED = 1 / TWO_PI;
        public const float EIGHTH = 1 / (TWO_PI / 8);
        public const float DEGREES_TO_RADIANS = 0.0174532925f;
        public const float RADIANS_TO_DEGREES = 360f / TWO_PI;

        public static Direction RadiansToDirection(float pRadians) 
        {
            int dir = (int)(System.Math.Round(pRadians * EIGHTH, 0));
            // cap to eight directions
            return GridMath.DirectionIndexToDirection(dir & 7);
        }

        public static Direction DegreesToDirection(int pDegrees)
        {
            return RadiansToDirection(pDegrees * DEGREES_TO_RADIANS);
        }

        internal static Direction DirectionIndexToDirection(int direction)
        {
            switch (direction)
            {
                case 0:
                    return Direction.UP;
                case 1:
                    return Direction.UP_RIGHT;
                case 2:
                    return Direction.RIGHT;
                case 3:
                    return Direction.DOWN_RIGHT;
                case 4:
                    return Direction.DOWN;
                case 5:
                    return Direction.DOWN_LEFT;
                case 6:
                    return Direction.LEFT;
                case 7:
                    return Direction.UP_LEFT;
                default:
                    D.isNull(null, "direction error: " + direction);
                    return Direction.ZERO;
            }
        }
    }
}
