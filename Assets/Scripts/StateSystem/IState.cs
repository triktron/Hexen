using UnityEngine;
using UnityEditor;

namespace StateSystem
{
    public interface IState<TState> where TState : IState<TState>
    {
        void OnEnter();

        void OnExit();

        StateMachine<TState> StateMachine { set; get;  }
    }
}