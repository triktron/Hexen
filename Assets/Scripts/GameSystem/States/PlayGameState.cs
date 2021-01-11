using UnityEngine;
using System.Collections;
using GameSystem.Modals;
using BoardSystem;
using MoveSystem;
using GameSystem.Views;

namespace GameSystem.States
{
    public class PlayGameState : GameStateBase
    {
        private Modals.Piece _playerPiece;
        public Modals.Piece PlayerPiece => _playerPiece;

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
            var playerView = GameObject.FindGameObjectWithTag("Player");
            var playerPieceView = playerView.GetComponent<PieceView>();
            _playerPiece = playerPieceView.Modal;

            _moveManager.MoveComandProviderChanged += OnMoveComandManagerChanged;

            _moveManager.ActivateFor(_playerPiece);
        }

        public override void OnExit()
        {
            _moveManager.Deactivate();
            _currentMoveComand = null;
            _moveManager.MoveComandProviderChanged -= OnMoveComandManagerChanged;
        }

        override public void Select(Modals.Piece piece)
        {

        }

        override public void Select(Tile tile)
        {
            if (_playerPiece != null && _currentMoveComand != null)
            {

                var tiles = _currentMoveComand.Tiles(_board, _playerPiece);
                
                _board.UnHightlight(tiles);

                if (!tiles.Contains(tile)) return;

                _currentMoveComand.Execute(_board, _playerPiece, tile);

                //_moveManager.Deactivate();

                _currentMoveComand = null;
            }
        }
        override public void Select(IMoveCommand<Modals.Piece> moveComand)
        {
            if (_currentMoveComand != null)
                _board.UnHightlight(_currentMoveComand.Tiles(_board, _playerPiece));

            _currentMoveComand = moveComand;

            if (_currentMoveComand != null)
                _board.Highlight(_currentMoveComand.Tiles(_board, _playerPiece));
        }

        private void OnMoveComandManagerChanged(object sender, MoveCommandProviderChanged<Modals.Piece> e)
        {
            if (_currentMoveComand == null || _playerPiece == null) return;
            var tiles = _currentMoveComand.Tiles(_board, _playerPiece);

            _board.UnHightlight(tiles);
        }

        public override void Backward()
        {
            StateMachine.MoveTo(GameStates.Replay);
        }
    }
}