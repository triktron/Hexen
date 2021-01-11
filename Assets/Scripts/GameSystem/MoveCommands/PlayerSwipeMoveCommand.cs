using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveProvider;
using HexGrid;
using HexGrid.Enum;
using MoveSystem;
using ReplaySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.MoveCommands
{
    public class PlayerSwipeMoveCommand : AbstractBasicMoveCommand
    {
        public PlayerSwipeMoveCommand(ReplayManager replayManager) : base(replayManager, "Swipe")
        {
        }

        public override List<Tile> Tiles(Board<Modals.Piece> board, Modals.Piece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .Neigbours(1)
                .Generate();

            return validTiles;
        }

        public override List<Tile> Action(Board<Modals.Piece> board, Modals.Piece piece, Tile tile)
        {
            var validTiles = new List<Tile>();
            if (piece == null || tile == null) return validTiles;

            validTiles.Add(tile);
            var playerTile = board.TileOf(piece);

            CubicHexCoord clockwisePosition = CubicHexCoord.RotateOnceLeft(playerTile.Position, tile.Position);
            Tile clockwiseTile = board.TileAt(clockwisePosition);
            while (clockwiseTile == null)
            {
                clockwisePosition = CubicHexCoord.RotateOnceLeft(playerTile.Position, clockwisePosition);
                clockwiseTile = board.TileAt(clockwisePosition);
            }

            CubicHexCoord counterClockwisePosition = CubicHexCoord.RotateOnceRight(playerTile.Position, tile.Position);
            Tile counterClockwiseTile = board.TileAt(counterClockwisePosition);
            while (counterClockwiseTile == null)
            {
                counterClockwisePosition = CubicHexCoord.RotateOnceRight(playerTile.Position, counterClockwisePosition);
                counterClockwiseTile = board.TileAt(counterClockwisePosition);
            }

            validTiles.Add(clockwiseTile);
            validTiles.Add(counterClockwiseTile);

            return validTiles;
        }

        public override void Execute(Board<Modals.Piece> board, Modals.Piece piece, Tile toTile)
        {
            var tilesToSwipe = Action(board, piece, toTile);

            foreach (var tile in tilesToSwipe)
            {
                var enemy = board.PieceAt(tile);
                if (enemy != null) board.Take(tile);
            }
        }
    }
}
