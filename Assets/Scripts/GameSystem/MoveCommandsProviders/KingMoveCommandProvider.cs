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
    public class KingMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "King";

        public KingMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState, new KingBasicMoveCommand(replayManager))
        {
        }
    }
}