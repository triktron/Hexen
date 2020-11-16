using BoardSystem;
using GameSystem.Modals;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.MoveCommands
{
    public abstract class AbstractBasicMoveCommand : IMoveCommand<ChessPiece>
    {
        public virtual bool CanExecute(Board<ChessPiece> board, ChessPiece piece)
        {
            if (Tiles(board, piece).Count == 0) return false;
            return true;
        }

        public virtual void Execute(Board<ChessPiece> board, ChessPiece piece, Tile toTile)
        {
            var toPiece = board.PieceAt(toTile);
            if (toPiece != null)
            {
                board.Take(toTile);
            }

            var fromTile = board.TileOf(piece);
            board.Move(fromTile, toTile);

            piece.HasMoved = true;
        }

        public abstract List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece);
    }
}