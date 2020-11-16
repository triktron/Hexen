using GameSystem.Modals;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem.MoveCommandsProviders
{
    public abstract class AbstractMoveCommandProvider : IMoveCommandProvider<ChessPiece>
    {
        //public const string Name = "";
        private List<IMoveCommand<ChessPiece>> _commands;

        public AbstractMoveCommandProvider(params IMoveCommand<ChessPiece>[] commands)
        {
            _commands = commands.ToList();
        }

        public List<IMoveCommand<ChessPiece>> MoveCommands()
        {
            return _commands;
        }
    }
} 