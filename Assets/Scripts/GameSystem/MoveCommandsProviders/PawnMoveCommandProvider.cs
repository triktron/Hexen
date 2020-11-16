using UnityEngine;
using UnityEditor;
using MoveSystem;
using GameSystem.Modals;
using System.Collections.Generic;
using BoardSystem;
using GameSystem.MoveCommands;

namespace GameSystem.MoveCommandsProviders
{
    public class PawnMoveCommandProvider : AbstractMoveCommandProvider
    {
        public static readonly string Name = "Pawn";

        public PawnMoveCommandProvider() : base(new PawnBasicMoveCommand(), new PawnFirstMoveCommand())
        {
        }
    }
}