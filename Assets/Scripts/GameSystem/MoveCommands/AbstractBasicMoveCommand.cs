using BoardSystem;
using Deck;
using GameSystem.Modals;
using MoveSystem;
using ReplaySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.MoveCommands
{
    public abstract class AbstractBasicMoveCommand : IMoveCommand<Modals.Piece>
    {
        protected ReplayManager ReplayManager;
        public string Name;
        internal Card Card;

        protected AbstractBasicMoveCommand(ReplayManager replayManager, string name)
        {
            ReplayManager = replayManager;
            Name = name;
        }

        public virtual bool CanExecute(Board<Modals.Piece> board, Modals.Piece piece)
        {
            if (Tiles(board, piece).Count == 0) return false;
            return true;
        }

        public virtual void Execute(Board<Modals.Piece> board, Modals.Piece piece, Tile toTile)
        {
            var toPiece = board.PieceAt(toTile);
            var fromTile = board.TileOf(piece);
            var hasMoved = piece.HasMoved;

            Action forward = () =>
            {
                if (toPiece != null)
                {
                    board.Take(toTile);
                }

                
                board.Move(fromTile, toTile);

                piece.HasMoved = true;
            };

            Action backward = () =>
            {
                piece.HasMoved = hasMoved;
                board.Move(toTile, fromTile);
                if (toPiece != null)
                {
                    board.Place(toTile, toPiece);
                }
            };

            var replayComand = new DelegateReplayComand(forward, backward);

            ReplayManager.Execute(replayComand);
        }

        public abstract List<Tile> Tiles(Board<Modals.Piece> board, Modals.Piece piece);
        public abstract List<Tile> Action(Board<Modals.Piece> board, Modals.Piece piece, Tile tile);
    }
}