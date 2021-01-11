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
    public class PlayerTeleportMoveCommand : AbstractBasicMoveCommand
    {
        public PlayerTeleportMoveCommand(ReplayManager replayManager) : base(replayManager, "Teleport")
        {
        }

        public override List<Tile> Tiles(Board<Modals.Piece> board, Modals.Piece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .All(MovementHelper.IsEmpty)
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
