using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveProvider;
using MoveSystem;
using ReplaySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.MoveCommands
{
    public class PawnBasicMoveCommand : AbstractBasicMoveCommand
    {
        public PawnBasicMoveCommand(ReplayManager replayManager) : base(replayManager)
        {
        }

        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .North(1, MovementHelper.IsEmpty)
                //.North(2, MovementHelper.IsEmpty, (_board, piece, tile) => !piece.HasMoved)
                .NorthEast(1, MovementHelper.CanCapture)
                .NorthWest(1, MovementHelper.CanCapture)
                .Generate();

            return validTiles;
        }
    }
}
