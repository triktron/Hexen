using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateSystem
{
    public class StateMachine<TState> where TState : IState<TState>
    {
        public TState CurrentSate { get; internal set; }

        private Dictionary<string, TState> _states = new Dictionary<string, TState>();

        public void RegisterState(string name, TState state)
        {
            state.StateMachine = this;
            _states.Add(name, state);
        }

        public void MoveTo(string name)
        {
            CurrentSate?.OnExit();

            CurrentSate = _states[name];

            CurrentSate?.OnEnter();
        }
    }
}
