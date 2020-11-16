using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveProvider;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.MoveCommands
{
    public class BishopBasicMoveCommand : AbstractBasicMoveCommand
    {
        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .NorthEast(8)
                .NorthWest(8)
                .SouthWest(8)
                .EastSouth(8)
                .Generate();

            return validTiles;
        }
    }
}
