using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BoardSystem;
using System.Linq;
using System;

namespace MoveSystem
{
    public class MoveCommandProviderChanged<TPiece> : EventArgs where TPiece : class, IPiece<TPiece>
    {
        public IMoveCommandProvider<TPiece> MoveCommandProvider { get; }

        public MoveCommandProviderChanged(IMoveCommandProvider<TPiece> moveCommandProvider)
        {
            MoveCommandProvider = moveCommandProvider;
        }
    }

    public class MoveManager<TPiece> where TPiece : class, IPiece<TPiece>
    {
        public event EventHandler<MoveCommandProviderChanged<TPiece>> MoveComandProviderChanged;

        private Dictionary<string, IMoveCommandProvider<TPiece>> _providers = new Dictionary<string, IMoveCommandProvider<TPiece>>();

        private Dictionary<TPiece, string> _pieceMovements = new Dictionary<TPiece, string>();

        private IMoveCommandProvider<TPiece> _activeProvider;

        private Board<TPiece> _board;
        private List<Tile> _validTile = new List<Tile>();

        public MoveManager(Board<TPiece> board)
        {
            _board = board;
        }

        public string MovementOf(TPiece piece)
        {
            return _pieceMovements[piece];
        }

        public void Register(string name, IMoveCommandProvider<TPiece> provider)
        {
            if (_providers.ContainsKey(name))
                return;

            _providers.Add(name, provider);
        }

        public void Register(TPiece piece, string name)
        {
            if (_pieceMovements.ContainsKey(piece))
                return;

            _pieceMovements.Add(piece, name);
        }

        public IMoveCommandProvider<TPiece> Provider(TPiece piece)
        {
            if (piece == null) return null;
            if (_pieceMovements.TryGetValue(piece, out var name))
            {
                if (_providers.TryGetValue(name, out var moveCommandProvider))
                    return moveCommandProvider;
            }

            return null;
        }

        public void ActivateFor(TPiece currentPiece)
        {
            _activeProvider = Provider(currentPiece);

            if (_activeProvider != null)
            {
                _validTile = _activeProvider.MoveCommands()
               .Where((command) => command.CanExecute(_board, currentPiece))
               .SelectMany((command) => command.Tiles(_board, currentPiece)).ToList();
            } else
            {
                _validTile.Clear();
            }
           

            OnMoveComandProviderChanged(new MoveCommandProviderChanged<TPiece>(_activeProvider ));
        }

        public void Deactivate()
        {
            _validTile.Clear();
            _activeProvider = null;
            OnMoveComandProviderChanged(new MoveCommandProviderChanged<TPiece>(null));
        }

        public void Excecute(TPiece piece, Tile tile)
        {
            if (_validTile.Contains(tile))
            {
                var foundCommand = _activeProvider.MoveCommands().Find((command) => command.Tiles(_board, piece).Contains(tile));

                if (foundCommand == null)
                    return;

                foundCommand.Execute(_board, piece, tile);
                _activeProvider = null;
            }
        }

        public List<Tile> Tiles()
        {
            return _validTile;
        }

        protected virtual void OnMoveComandProviderChanged(MoveCommandProviderChanged<TPiece> arg)
        {
            EventHandler<MoveCommandProviderChanged<TPiece>> handler = MoveComandProviderChanged;

            handler?.Invoke(this, arg);
        }
    }
}