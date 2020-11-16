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
        public Tile From { get; }
        public Tile To { get; }

        public ChessPieceMovedEventArgs(Tile from, Tile to)
        {
            From = from;
            To = to;
        }
    }

    public class ChessPiece : IPiece
    {
        public event EventHandler<ChessPieceMovedEventArgs> ChessPieceMoved;
        public event EventHandler ChessPieceCaptured;

        public bool HasMoved;

        public int PlayerID { get; }
        public bool FacingBack { get; }

        public ChessPiece(int playerID, bool facingBack)
        {
            PlayerID = playerID;
            FacingBack = facingBack;
        }

        void IPiece.Moved(Tile fromPosition, Tile toPosition)
        {
            OnChessPieceMoved(new ChessPieceMovedEventArgs(fromPosition, toPosition));
        }
        void IPiece.Captured()
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
            handler.Invoke(this, EventArgs.Empty);
        }
    }
}