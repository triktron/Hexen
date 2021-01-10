namespace BoardSystem
{
    public struct Position2
    {
        public int X;
        public int Y;

        public static Position2 operator +(Position2 a, Position2 b)
        => new Position2 { X = a.X + b.X, Y = a.Y + b.Y };

        public static Position2 operator -(Position2 a, Position2 b)
        => new Position2 { X = a.X - b.X, Y = a.Y - b.Y };
    }

    public struct Position3
    {
        public int X;
        public int Y;
        public int Z;

        public static Position3 operator +(Position3 a, Position3 b)
        => new Position3 { X = a.X + b.X, Y = a.Y + b.Y, Z = a.Z + b.Z };

        public static Position3 operator -(Position3 a, Position3 b)
        => new Position3 { X = a.X - b.X, Y = a.Y - b.Y , Z = a.Z + b.Z };
    }
}