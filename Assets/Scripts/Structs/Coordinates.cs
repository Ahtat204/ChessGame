namespace Assets.Scripts.Structs
{
    public struct Coordinates
    {
        private int x, y;

        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }

     public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
            if (x == 0 || y == 0) throw new System.Exception("Invalid coordinates");
          
        }
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}