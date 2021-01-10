﻿using BoardSystem;
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
    public class PawnBasicMoveCommand : AbstractBasicMoveCommand
    {
        public PawnBasicMoveCommand(ReplayManager replayManager) : base(replayManager)
        {
        }

        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece _piece)
        {
            var validTiles = new MovementHelper(board, _piece)
                .Neigbours()
                //.Collect(new CubicHexCoord(1,0,0), 2, MovementHelper.IsEmpty)
                //.Collect(new CubicHexCoord(-1,0,0), 2, MovementHelper.IsEmpty)
                .Generate();

            return validTiles;
        }
    }
}
