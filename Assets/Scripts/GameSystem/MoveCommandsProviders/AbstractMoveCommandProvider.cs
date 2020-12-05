using GameSystem.Modals;
using GameSystem.States;
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

        public AbstractMoveCommandProvider(PlayGameState playGameState, params IMoveCommand<ChessPiece>[] commands)
        {
            _commands = commands.ToList();
            _playGameState = playGameState;
        }

        public PlayGameState _playGameState { get; }

        public List<IMoveCommand<ChessPiece>> MoveCommands()
        {
            return _commands.Where((command) => command.CanExecute(_playGameState.Board, _playGameState.SelectedPiece)).ToList();
        }
    }
} 