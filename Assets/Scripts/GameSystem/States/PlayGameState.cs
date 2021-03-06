﻿using UnityEngine;
using GameSystem.Modals;
using BoardSystem;
using MoveSystem;
using GameSystem.Views;

namespace GameSystem.States
{
    public class PlayGameState : GameStateBase
    {
        private Piece _playerPiece;
        public Piece PlayerPiece => _playerPiece;

        private IMoveCommand<Piece> _currentMoveComand;

        Board<Piece> _board;
        MoveManager<Piece> _moveManager;

        public Board<Piece> Board => _board;

        private float _movesAllowed = 2;
        private float _movesLeft;

        public PlayGameState(Board<Piece> board, MoveManager<Piece> moveManager)
        {
            _moveManager = moveManager;
            _board = board;
        }

        public override void OnEnter()
        {
            InitializePlayer();

            _moveManager.MoveComandProviderChanged += OnMoveComandManagerChanged;

            _moveManager.ActivateFor(_playerPiece);

            _movesLeft = _movesAllowed;
        }

        private void InitializePlayer()
        {
            if (_playerPiece == null)
            {
                var playerView = GameObject.FindGameObjectWithTag("Player");
                var playerPieceView = playerView.GetComponent<PieceView>();
                _playerPiece = playerPieceView.Modal;
            }
        }

        public override void OnExit()
        {
            _moveManager.Deactivate();
            _currentMoveComand = null;
            _moveManager.MoveComandProviderChanged -= OnMoveComandManagerChanged;
        }

        override public void Select(Tile tile)
        {
            if (_playerPiece != null && _currentMoveComand != null)
            {

                var tiles = _currentMoveComand.Tiles(_board, _playerPiece);
                
                _board.UnHightlight(tiles);

                if (tiles.Contains(tile))
                {
                    _currentMoveComand.Execute(_board, _playerPiece, tile);

                    GameLoop.Instance.Board.Deck.Take((_currentMoveComand).GetCard());
                    _movesLeft--;
                }

                

                _currentMoveComand = null;

                _moveManager.ActivateFor(_playerPiece);

                
                if (_movesLeft == 0) StateMachine.MoveTo(GameStates.EnemyPhase1);
            }
        }
        override public void Select(IMoveCommand<Piece> moveComand)
        {
            if (_currentMoveComand != null)
                _board.UnHightlight(_currentMoveComand.Tiles(_board, _playerPiece));

            _currentMoveComand = moveComand;

            if (_currentMoveComand != null)
                _board.Highlight(_currentMoveComand.Tiles(_board, _playerPiece));
        }

        private void OnMoveComandManagerChanged(object sender, MoveCommandProviderChanged<Piece> e)
        {
            if (_currentMoveComand == null || _playerPiece == null) return;
            var tiles = _currentMoveComand.Tiles(_board, _playerPiece);

            _board.UnHightlight(tiles);
        }

        public override void Backward()
        {
            StateMachine.MoveTo(GameStates.Replay);
        }

        public override void Hover(Tile tile)
        {
            _board.UnHightlight(_board.Tiles);

            var piece = _board.PieceAt(tile);

            if (piece != null && piece.PlayerID == 1 && _currentMoveComand == null)
            {
                _board.Highlight(piece.QueuedPath);
            } else if (_playerPiece != null && _currentMoveComand != null)
            {
                var tiles = _currentMoveComand.Tiles(_board, _playerPiece);
                var action = _currentMoveComand.Action(_board, _playerPiece, tile);


                if (tile != null && tiles.Contains(tile))
                {
                    _board.Highlight(action);
                }
                else
                {
                    _board.Highlight(tiles);
                }
            }
        }
    }
}