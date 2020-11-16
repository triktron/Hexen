using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveProvider;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.MoveCommands
{
    public class QueenBasicMoveCommand : AbstractBasicMoveCommand
    {
        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .North(16)
                .East(16)
                .South(16)
                .West(16)
                .NorthEast(16)
                .NorthWest(16)
                .SouthWest(16)
                .EastSouth(16)
                .Generate();

            return validTiles;
        }
    }
}
