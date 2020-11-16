using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveProvider;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.MoveCommands
{
    public class KingBasicMoveCommand : AbstractBasicMoveCommand
    {
        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .North(1)
                .East(1)
                .South(1)
                .West(1)
                .NorthEast(1)
                .NorthWest(1)
                .SouthWest(1)
                .EastSouth(1)
                .Generate();

            return validTiles;
        }
    }
}
