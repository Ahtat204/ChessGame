using System;

namespace Assets.Scripts.Structs
{
    /// <summary>
    /// this struct will be used to represent piece coordinates (not used in moving piece) , 
    /// </summary>
    public readonly struct Coordinates
    {
        /// <summary>
        /// X coordinate property
        /// </summary>
        public char X { get;  }

        /// <summary>
        /// Y coordinate property
        /// </summary>
        public uint Y { get; }


        /// <summary>
        /// Constructor of the Coordinates Struct
        /// </summary>
        /// <param name="x"> Horizantal Coordinate</param>
        /// <param name="y"> Vertical Coordinate</param>
        /// <exception cref="Exception">chess is 8x8 game , the numeration start from 1 not 0 </exception>
        public Coordinates(char x, uint y)
        {
            X = x;
            Y = y;
            throw new ArgumentException(x is < 'a' or > 'z' || y > 8 ? "invalid coordinates" : "");
        }

        public override string ToString()
        {
            return $"({X}{Y})";
        }
    }
}