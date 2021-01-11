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
    public class PlayerLineAttackMoveCommand : AbstractBasicMoveCommand
    {
        public PlayerLineAttackMoveCommand(ReplayManager replayManager) : base(replayManager, "LineAttack")
        {
        }

        public override List<Tile> Tiles(Board<Modals.Piece> board, Modals.Piece _piece)
        {

            var validTiles = new MovementHelper(board, _piece)
                .Neigbours(int.MaxValue)
                .Generate();

            return validTiles;
        }

        public override List<Tile> Action(Board<Modals.Piece> board, Modals.Piece piece, Tile tile)
        {
            if (piece == null || tile == null) return new List<Tile>();

            var playerTile = board.TileOf(piece);
            var dir = playerTile.Position - tile.Position;
            dir = new CubicHexCoord(Mathf.Clamp(dir.x, -1, 1), Mathf.Clamp(dir.y, -1, 1), Mathf.Clamp(dir.z, -1, 1));


            var validTiles = new MovementHelper(board, piece)
                .Collect(dir)
                .Generate();

            return validTiles;
        }

        public override void Execute(Board<Modals.Piece> board, Modals.Piece piece, Tile toTile)
        {
            var inLineTiles = Action(board, piece, toTile);

            foreach (var tile in inLineTiles)
            {
                var enemy = board.PieceAt(tile);
                if (enemy != null) board.Take(tile);
            }
        }
    }
}
