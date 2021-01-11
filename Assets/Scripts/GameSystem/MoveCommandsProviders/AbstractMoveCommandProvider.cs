using GameSystem.Modals;
using GameSystem.MoveCommands;
using GameSystem.States;
using MoveSystem;
using System;
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
            var types = _commands.Where((command) => command.CanExecute(_playGameState.Board, _playGameState.PlayerPiece)).ToArray();

            var commands = new List<IMoveCommand<Piece>>();

            var deck = GameLoop.Instance.Board.Deck;

            for (int i = 0; i < Mathf.Min(5, deck.Cards.Count); i++)
            {
                var cardName = deck.Cards[i].Name;

                var command = Array.Find(types, type => type.GetName() == cardName);

                command.SetCard(deck.Cards[i]);

                commands.Add(command);
            }

            return commands;
        }
    }
} 