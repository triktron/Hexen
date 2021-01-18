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