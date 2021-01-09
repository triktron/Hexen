namespace BoardSystem
{
    public struct Position
    {
        public int X;
        public int Y;

        public static Position operator +(Position a, Position b)
        => new Position { X = a.X + b.X, Y = a.Y + b.Y };

        public static Position operator -(Position a, Position b)
        => new Position { X = a.X - b.X, Y = a.Y - b.Y };
    }
}