using System;

namespace Assets.Scripts.Structs
{
    
    /// <summary>
    /// a struct represent the position in the chess board
    /// </summary>
    public struct Coordinates
    {
        public int Y { get; }
        public char X { get; }

        public Coordinates(char x, int y)
        {
            if (x < 'a' || x > 'h') throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 1 || y > 8) throw new ArgumentOutOfRangeException(nameof(y));
            X = x;
            Y = y;
        }
    }
}