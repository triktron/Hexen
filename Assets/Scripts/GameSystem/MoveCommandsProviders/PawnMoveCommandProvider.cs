using UnityEngine;
using UnityEditor;
using MoveSystem;
using GameSystem.Modals;
using System.Collections.Generic;
using BoardSystem;
using GameSystem.MoveCommands;
using ReplaySystem;

namespace GameSystem.MoveCommandsProviders
{
    public class PawnMoveCommandProvider : AbstractMoveCommandProvider
    {
        public static readonly string Name = "Pawn";

        public PawnMoveCommandProvider(ReplayManager replayManager) : base(new PawnBasicMoveCommand(replayManager), new PawnFirstMoveCommand(replayManager))
        {
        }
    }
}