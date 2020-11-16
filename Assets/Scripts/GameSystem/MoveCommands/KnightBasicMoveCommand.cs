using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveProvider;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.MoveCommands
{
    public class KnightBasicMoveCommand : AbstractBasicMoveCommand
    {
        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .Collect(1, 2, 1)
                .Collect(-1, 2, 1)
                .Collect(1, -2, 1)
                .Collect(-1, -2, 1)
                .Collect(2, 1, 1)
                .Collect(-2, 1, 1)
                .Collect(2, -1, 1)
                .Collect(-2, -1, 1)
                .Generate();

            return validTiles;
        }
    }
}
