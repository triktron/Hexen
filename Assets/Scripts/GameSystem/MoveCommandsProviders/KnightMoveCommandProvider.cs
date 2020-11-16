using UnityEngine;
using UnityEditor;
using MoveSystem;
using GameSystem.Modals;
using System.Collections.Generic;
using BoardSystem;
using GameSystem.MoveCommands;
using Utils;

namespace GameSystem.MoveCommandsProviders
{
    [MoveCommandProvider(Name)]
    public class KnightMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Knight";

        public KnightMoveCommandProvider() : base(new KnightBasicMoveCommand())
        {
        }
    }
}