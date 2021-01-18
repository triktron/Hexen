using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.States
{
    class EnemyPhase2GameState : GameStateBase
    {
        public override void OnEnter()
        {
            StateMachine.MoveTo(GameStates.Play);
        }
    }
}
