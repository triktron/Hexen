using UnityEngine;
using System.Collections;
using System;

namespace ReplaySystem
{
    public class DelegateReplayComand : IReplayebleComand
    {
        private Action _forward;
        private Action _backward;

        public DelegateReplayComand(Action forward, Action backward)
        {
            _forward = forward;
            _backward = backward;
        }

        public void Backward()
        {
            _backward.Invoke();
        }

        public void Forward()
        {
            _forward.Invoke();
        }
    }
}