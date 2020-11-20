using UnityEngine;
using UnityEditor;
using MoveSystem;
using GameSystem.Modals;
using System.Collections.Generic;
using BoardSystem;
using GameSystem.MoveCommands;
using Utils;
using ReplaySystem;

namespace GameSystem.MoveCommandsProviders
{
    [MoveCommandProvider(Name)]
    public class KingMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "King";

        public KingMoveCommandProvider(ReplayManager replayManager) : base(new KingBasicMoveCommand(replayManager))
        {
        }
    }
}