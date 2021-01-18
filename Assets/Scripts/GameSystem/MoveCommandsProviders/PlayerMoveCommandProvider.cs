using GameSystem.MoveCommands;
using ReplaySystem;
using GameSystem.States;
using Utils;

namespace GameSystem.MoveCommandsProviders
{
    [MoveCommandProvider(Name)]
    public class PlayerMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Player";

        public PlayerMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState, new PlayerSwipeMoveCommand(replayManager), new PlayerTeleportMoveCommand(replayManager), new PlayerPushbackMoveCommand(replayManager), new PlayerLineAttackMoveCommand(replayManager))
        {
        }
    }
}