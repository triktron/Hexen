using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveProvider;
using HexGrid;
using ReplaySystem;
using System.Collections.Generic;

namespace GameSystem.MoveCommands
{
    public class PlayerPushbackMoveCommand : AbstractBasicMoveCommand
    {
        public PlayerPushbackMoveCommand(ReplayManager replayManager) : base(replayManager, "Pushback")
        {
        }

        public override List<Tile> Tiles(Board<Piece> board, Piece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .Neigbours(1)
                .Generate();

            return validTiles;
        }

        public override List<Tile> Action(Board<Piece> board, Piece piece, Tile tile)
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

        public override void Execute(Board<Piece> board, Piece piece, Tile toTile)
        {
            var playerTile = board.TileOf(piece);

            var tilesToPush = Action(board, piece, toTile);

            foreach (var tileToPush in tilesToPush)
            {
                var dir = tileToPush.Position - playerTile.Position;
                var enemy = board.PieceAt(tileToPush);
                var tileBehind = board.TileAt(tileToPush.Position + dir);
                var enemyBehind = board.PieceAt(tileBehind);

                if (enemy != null && enemyBehind == null) board.Move(tileToPush, tileBehind);
            }
        }
    }
}
