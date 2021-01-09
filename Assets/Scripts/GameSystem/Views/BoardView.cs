using BoardSystem;
using GameSystem.Modals;
using System;
using UnityEngine;

namespace GameSystem.Views
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField]
        private ChessViewViewFactory _chessPieceViewFactory = null;

        private Board<ChessPiece> _modal;
        public Board<ChessPiece> Modal
        {
            set
            {
                if (_modal != null)
                    _modal.PiecePlaced -= OnPiecePlaced;

                _modal = value;

                if (_modal != null)
                    _modal.PiecePlaced += OnPiecePlaced;
            }

            get => _modal;
        }

        private void OnPiecePlaced(object sender, PiecePlacedEventArgs<ChessPiece> e)
        {
            var board = sender as Board<ChessPiece>;
            var piece = e.Piece;

            _chessPieceViewFactory.CreateChessPieceView(board, piece);
        }

        private void OnDestroy()
        {
            Modal = null;
        }
    }
}