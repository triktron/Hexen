using UnityEngine;
using System.Collections;
using GameSystem.Modals;
using BoardSystem;
using MoveSystem;

namespace GameSystem.States
{
    public class PlayGameState : GameStateBase
    {
        private ChessPiece _selectedPiece;
        public ChessPiece SelectedPiece => _selectedPiece;

        private int _currentPlayerID;
        public int CurrentPlayerID => _currentPlayerID;
        private IMoveCommand<ChessPiece> _currentMoveComand;

        Board<ChessPiece> _board;
        MoveManager<ChessPiece> _moveManager;

        public Board<ChessPiece> Board => _board;

        public PlayGameState(Board<ChessPiece> board, MoveManager<ChessPiece> moveManager)
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

        override public void Select(ChessPiece chessPiece)
        {
            if (chessPiece == null || _selectedPiece == chessPiece) return;

            if (chessPiece != null && chessPiece.PlayerID != CurrentPlayerID)
            {
                var tile = _board.TileOf(chessPiece);
                Select(tile);
                return;
            }


            _moveManager.Deactivate();


            _selectedPiece = chessPiece;

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
        override public void Select(IMoveCommand<ChessPiece> moveComand)
        {
            if (_currentMoveComand != null)
                _board.UnHightlight(_currentMoveComand.Tiles(_board, _selectedPiece));

            _currentMoveComand = moveComand;

            if (_currentMoveComand != null)
                _board.Highlight(_currentMoveComand.Tiles(_board, _selectedPiece));
        }

        private void OnMoveComandManagerChanged(object sender, MoveCommandProviderChanged<ChessPiece> e)
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