using UnityEngine;
using System.Collections;
using GameSystem.Modals;
using BoardSystem;
using MoveSystem;

namespace GameSystem.States
{
    public class PlayGameState : GameStateBase
    {
        private Modals.Piece _selectedPiece;
        public Modals.Piece SelectedPiece => _selectedPiece;

        private int _currentPlayerID = 0;
        public int CurrentPlayerID => _currentPlayerID;
        private IMoveCommand<Modals.Piece> _currentMoveComand;

        Board<Modals.Piece> _board;
        MoveManager<Modals.Piece> _moveManager;

        public Board<Modals.Piece> Board => _board;

        public PlayGameState(Board<Modals.Piece> board, MoveManager<Modals.Piece> moveManager)
        {
            _moveManager = moveManager;
            _board = board;
        }

        public override void OnEnter()
        {
            _moveManager.MoveComandProviderChanged += OnMoveComandManagerChanged;
        }

        public override void OnExit()
        {
            _moveManager.Deactivate();
            _currentMoveComand = null;
            _selectedPiece = null;
            _moveManager.MoveComandProviderChanged -= OnMoveComandManagerChanged;
        }

        override public void Select(Modals.Piece piece)
        {
            if (piece == null || _selectedPiece == piece) return;

            if (piece != null && piece.PlayerID != CurrentPlayerID)
            {
                var tile = _board.TileOf(piece);
                Select(tile);
                return;
            }


            _moveManager.Deactivate();


            _selectedPiece = piece;

            _moveManager.ActivateFor(_selectedPiece);
        }

        override public void Select(Tile tile)
        {
            if (_selectedPiece != null && _currentMoveComand != null)
            {
                var tiles = _currentMoveComand.Tiles(_board, _selectedPiece);
                if (!tiles.Contains(tile)) return;

                _board.UnHightlight(tiles);

                _currentMoveComand.Execute(_board, _selectedPiece, tile);

                _moveManager.Deactivate();

                _selectedPiece = null;
                _currentMoveComand = null;
            }
        }
        override public void Select(IMoveCommand<Modals.Piece> moveComand)
        {
            if (_currentMoveComand != null)
                _board.UnHightlight(_currentMoveComand.Tiles(_board, _selectedPiece));

            _currentMoveComand = moveComand;

            if (_currentMoveComand != null)
                _board.Highlight(_currentMoveComand.Tiles(_board, _selectedPiece));
        }

        private void OnMoveComandManagerChanged(object sender, MoveCommandProviderChanged<Modals.Piece> e)
        {
            if (_currentMoveComand == null || _selectedPiece == null) return;
            var tiles = _currentMoveComand.Tiles(_board, _selectedPiece);

            _board.UnHightlight(tiles);
        }

        public override void Backward()
        {
            StateMachine.MoveTo(GameStates.Replay);
        }
    }
}