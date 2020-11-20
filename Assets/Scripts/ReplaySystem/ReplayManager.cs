using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ReplaySystem
{
    public class ReplayManager 
    {
        private List<IReplayebleComand> _commands = new List<IReplayebleComand>();
        private int _seekPosition = -1;

        public void Execute(IReplayebleComand comand)
        {
            var wasAtEnd = (_commands.Count == _seekPosition + 1);
            _commands.Add(comand);
            if (wasAtEnd) Forward();
        }

        public void Forward()
        {
            if (_commands.Count <= _seekPosition + 1)
                return;

            _seekPosition++;

            _commands[_seekPosition].Forward();
        }

        public void Backward()
        {
            if (_seekPosition < 0) return;

            _commands[_seekPosition].Backward();
            _seekPosition--;
        }
    }
}