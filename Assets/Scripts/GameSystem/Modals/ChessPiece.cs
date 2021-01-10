using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BoardSystem;
using UnityEngine.EventSystems;
using System;

namespace GameSystem.Modals
{
    public class ChessPieceMovedEventArgs : EventArgs
    {
        public Board<ChessPiece> Board { get; }

        public Tile From { get; }
        public Tile To { get; }

        public ChessPieceMovedEventArgs(Board<ChessPiece> board, Tile from, Tile to)
        {
            Board = board;
            From = from;
            To = to;
        }
    }

    public class ChessPiece : IPiece<ChessPiece>
    {
        public event EventHandler<ChessPieceMovedEventArgs> ChessPieceMoved;
        public event EventHandler ChessPieceCaptured;

        public bool HasMoved;

        public string MovementName  { get; internal set; }

        public int PlayerID { get; }
        public ChessPiece(int playerID, string name)
        {
            PlayerID = playerID;
            MovementName = name;
        }

        void IPiece<ChessPiece>.Moved(Board<ChessPiece> board, Tile fromPosition, Tile toPosition)
        {
            OnChessPieceMoved(new ChessPieceMovedEventArgs(board, fromPosition, toPosition));
        }
        void IPiece<ChessPiece>.Captured(Board<ChessPiece> board)
        {
            OnChessPieceCaptured();
        }

        protected virtual void OnChessPieceMoved(ChessPieceMovedEventArgs arg)
        {
            EventHandler<ChessPieceMovedEventArgs> handler = ChessPieceMoved;
            handler.Invoke(this, arg);
        }

        protected virtual void OnChessPieceCaptured()
        {
            EventHandler handler = ChessPieceCaptured;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}