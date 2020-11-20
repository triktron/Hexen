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
    public class RookBasicMoveCommand : AbstractBasicMoveCommand {
        public RookBasicMoveCommand(ReplayManager replayManager) : base(replayManager)
        {
        }

        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .North(8)
                .East(8)
                .South(8)
                .West(8)
                .Generate();

            return validTiles;
        }
    }
}
