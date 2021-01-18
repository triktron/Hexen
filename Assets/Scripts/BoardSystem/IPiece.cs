namespace BoardSystem
{
    public interface IPiece<TPiece> where TPiece : class, IPiece<TPiece>
    {
        void Moved(Board<TPiece> board, Tile fromPosition, Tile toPosition);
        void Captured(Board<TPiece> board);
    }
}