using BoardSystem;
using GameSystem.Modals;
using MoveSystem;
using ReplaySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.MoveCommands
{
    public abstract class AbstractBasicMoveCommand : IMoveCommand<ChessPiece>
    {
        protected ReplayManager ReplayManager;

        protected AbstractBasicMoveCommand(ReplayManager replayManager)
        {
            ReplayManager = replayManager;
        }

        public virtual bool CanExecute(Board<ChessPiece> board, ChessPiece piece)
        {
            if (Tiles(board, piece).Count == 0) return false;
            return true;
        }

        public virtual void Execute(Board<ChessPiece> board, ChessPiece piece, Tile toTile)
        {
            var toPiece = board.PieceAt(toTile);
            var fromTile = board.TileOf(piece);

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
                piece.HasMoved = false;
                board.Move(toTile, fromTile);
                if (toPiece != null)
                {
                    board.Place(toTile, toPiece);
                }
            };

            var replayComand = new DelegateReplayComand(forward, backward);

            ReplayManager.Execute(replayComand);
        }

        public abstract List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece);
    }
}