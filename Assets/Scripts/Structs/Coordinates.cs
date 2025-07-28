namespace Assets.Scripts.Structs
{
    public struct Coordinates
    {
        /// <summary>
        /// a wrapping property for the x field
        /// </summary>
        public int X { get; }

        /// <summary>
        /// a wrapping property for the y field
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Constructor of the Coordinates Struct
        /// </summary>
        /// <param name="x"> Horizantal Coordinate</param>
        /// <param name="y"> Vertical Coordinate</param>
        /// <exception cref="Exception">chess is 8x8 game , the numeration start from 1 not 0 </exception>
     public Coordinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
            if (x == 0 || y == 0) throw new System.Exception("Invalid coordinates");
          
        }
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}