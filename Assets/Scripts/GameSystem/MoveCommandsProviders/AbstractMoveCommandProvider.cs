using GameSystem.Modals;
using GameSystem.States;
using MoveSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem.MoveCommandsProviders
{
    public abstract class AbstractMoveCommandProvider : IMoveCommandProvider<Piece>
    {
        //public const string Name = "";
        private List<IMoveCommand<Piece>> _commands;

        public AbstractMoveCommandProvider(PlayGameState playGameState, params IMoveCommand<Piece>[] commands)
        {
            _commands = commands.ToList();
            _playGameState = playGameState;
        }

        public PlayGameState _playGameState { get; }

        public List<IMoveCommand<Piece>> MoveCommands()
        {
            return _commands.Where((command) => command.CanExecute(_playGameState.Board, _playGameState.PlayerPiece)).ToList();
        }
    }
} 