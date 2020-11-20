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
    public class BishopMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Bishop";

        public BishopMoveCommandProvider(ReplayManager replayManager) : base(new BishopBasicMoveCommand(replayManager))
        {
        }
    }
}