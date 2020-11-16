using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using BoardSystem;

namespace MoveSystem
{
    public interface IMoveCommandProvider<TPiece> where TPiece : class, IPiece
    {
        List<IMoveCommand<TPiece>> MoveCommands();
    }
}