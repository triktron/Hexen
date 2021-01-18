using ReplaySystem;

namespace GameSystem.States
{
    public class ReplayGameState : GameStateBase
    {
        ReplayManager _replayManager;

        public ReplayGameState(ReplayManager replayManager)
        {
            _replayManager = replayManager;
        }

        public override void OnEnter()
        {
            _replayManager.Backward();
        }

        public override void Backward()
        {
            _replayManager.Backward();
        }

        public override void Forward()
        {
            _replayManager.Forward();
            if (_replayManager.isAtEnd)
                StateMachine.MoveTo(GameStates.Play);
        }
    }
}