using System.Collections.Generic;
using BoardSystem;
using System;

namespace GameSystem.Modals
{
    public class PieceMovedEventArgs : EventArgs
    {
        public Board<Piece> Board { get; }

        public Tile From { get; }
        public Tile To { get; }

        public PieceMovedEventArgs(Board<Piece> board, Tile from, Tile to)
        {
            Board = board;
            From = from;
            To = to;
        }
    }

    public class Piece : IPiece<Piece>
    {
        public event EventHandler<PieceMovedEventArgs> PieceMoved;
        public event EventHandler PieceCaptured;

        public bool HasMoved;

        public string MovementName  { get; internal set; }

        public int PlayerID { get; }

        public List<Tile> QueuedPath = new List<Tile>();

        public Piece(int playerID, string name)
        {
            PlayerID = playerID;
            MovementName = name;
        }

        void IPiece<Piece>.Moved(Board<Piece> board, Tile fromPosition, Tile toPosition)
        {
            OnPieceMoved(new PieceMovedEventArgs(board, fromPosition, toPosition));
        }
        void IPiece<Piece>.Captured(Board<Piece> board)
        {
            OnPieceCaptured();
        }

        protected virtual void OnPieceMoved(PieceMovedEventArgs arg)
        {
            EventHandler<PieceMovedEventArgs> handler = PieceMoved;
            handler.Invoke(this, arg);
        }

        protected virtual void OnPieceCaptured()
        {
            EventHandler handler = PieceCaptured;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}