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
    public class PlayerJumpMoveCommand : AbstractBasicMoveCommand
    {
        public PlayerJumpMoveCommand(ReplayManager replayManager) : base(replayManager, "Jump")
        {
        }

        public override List<Tile> Tiles(Board<Modals.Piece> board, Modals.Piece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .Diagonal(DiagonalEnum.N)
                .Diagonal(DiagonalEnum.S)
                //.Collect(new CubicHexCoord(1,0,0), 2, MovementHelper.IsEmpty)
                //.Collect(new CubicHexCoord(-1,0,0), 2, MovementHelper.IsEmpty)
                .Generate();

            return validTiles;
        }

        public override List<Tile> Action(Board<Modals.Piece> board, Modals.Piece _piece, Tile tile)
        {
            var validTiles = new List<Tile>() { tile };

            return validTiles;
        }
    }
}
