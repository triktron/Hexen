using UnityEngine;
using UnityEditor;
using MoveSystem;
using GameSystem.Modals;
using System.Collections.Generic;
using BoardSystem;
using GameSystem.MoveCommands;
using ReplaySystem;
using GameSystem.States;

namespace GameSystem.MoveCommandsProviders
{
    public class PawnMoveCommandProvider : AbstractMoveCommandProvider
    {
        public static readonly string Name = "Pawn";

        public PawnMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState, new PawnAreaMoveCommand(replayManager), new PawnBasicMoveCommand(replayManager), new PawnFirstMoveCommand(replayManager))
        {
        }
    }
}