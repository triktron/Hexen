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
    public class QueenMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Queen";

        public QueenMoveCommandProvider(ReplayManager replayManager) : base(new QueenBasicMoveCommand(replayManager))
        {
        }
    }
}