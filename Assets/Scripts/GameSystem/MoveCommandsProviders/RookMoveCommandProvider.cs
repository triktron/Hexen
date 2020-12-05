using UnityEngine;
using UnityEditor;
using MoveSystem;
using GameSystem.Modals;
using System.Collections.Generic;
using BoardSystem;
using GameSystem.MoveCommands;
using Utils;
using ReplaySystem;
using GameSystem.States;

namespace GameSystem.MoveCommandsProviders
{
    [MoveCommandProvider(Name)]
    public class RookMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Rook";

        public RookMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState, new RookBasicMoveCommand(replayManager))
        {
        }
    }
}