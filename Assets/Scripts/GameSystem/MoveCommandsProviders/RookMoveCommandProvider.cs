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
    public class RookMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Rook";

        public RookMoveCommandProvider(ReplayManager replayManager) : base(new RookBasicMoveCommand(replayManager))
        {
        }
    }
}