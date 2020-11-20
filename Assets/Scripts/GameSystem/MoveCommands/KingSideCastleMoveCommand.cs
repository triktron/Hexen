using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BoardSystem;
using GameSystem.Modals;
using ReplaySystem;

namespace GameSystem.MoveCommands
{
    public class KingSideCastleMoveCommand : AbstractBasicMoveCommand
    {
        public KingSideCastleMoveCommand(ReplayManager replayManager) : base(replayManager)
        {
        }

        public override bool CanExecute(Board<ChessPiece> board, ChessPiece piece)
        {
            if (piece.HasMoved) return false;

            var tile = board.TileOf(piece);

            var rookPosition = tile.Position;
            rookPosition.X += 3;

            var rookTile = board.TileAt(rookPosition);
            if (rookTile == null) return false;

            var rookPiece = board.PieceAt(rookTile);
            if (rookPiece == null || rookPiece.HasMoved) return false;

            var intermediatePosition = tile.Position;
            for (int i = 1; i < 3; i++)
            {
                intermediatePosition.X += i;
                var intermediateTile = board.TileAt(intermediatePosition);

                if (intermediateTile == null) return false;

                var intermediatePiece = board.PieceAt(intermediateTile);
                if (intermediatePiece != null) return false;
            }

            return true;
        }

        public override void Execute(Board<ChessPiece> board, ChessPiece piece, Tile toTile)
        {
            var fromTile = board.TileOf(piece);
            board.Move(fromTile, toTile);
            var rookToPosition = toTile.Position;
            rookToPosition.X -= 1;

            var rookFromPosition = toTile.Position;
            rookFromPosition.X += 1;

            var rookToTile = board.TileAt(rookToPosition);
            var rookFromTile = board.TileAt(rookFromPosition);
            board.Move(rookFromTile, rookToTile);
        }

        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece)
        {
            var tile = board.TileOf(piece);
            var targetPosition = tile.Position;
            targetPosition.X += 1;
            var targetTile = board.TileAt(targetPosition);

            return new List<Tile>() { targetTile };
        }
    }
}