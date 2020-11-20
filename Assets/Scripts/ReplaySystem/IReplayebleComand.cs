using UnityEngine;
using UnityEditor;

namespace ReplaySystem
{
    public interface IReplayebleComand
    {
        void Forward();

        void Backward();
    }
}