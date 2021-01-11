using UnityEngine;
using UnityEditor;
using MoveSystem;
using GameSystem.Modals;
using System.Collections.Generic;
using BoardSystem;
using GameSystem.MoveCommands;
using ReplaySystem;
using GameSystem.States;
using Utils;

namespace GameSystem.MoveCommandsProviders
{
    [MoveCommandProvider(Name)]
    public class EnemyMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Enemy";

        public EnemyMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState)
        {
        }
    }
}