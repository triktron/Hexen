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

        // Use this for initialization
        void Start()
        {
            var gameLoop = GameLoop.Instance;

            gameLoop.Initialized += OnGameInitialized;
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            var gameLoop = GameLoop.Instance;

            var board = gameLoop.Board;

            board.PiecePlaced += OnPiecePlaced;
        }

        private void OnPiecePlaced(object sender, PiecePlacedEventArgs<ChessPiece> e)
        {
            var board = sender as Board<ChessPiece>;

            var position = board.TileOf(e.Piece);
            var piece = e.Piece;
            var gameLoop = GameLoop.Instance;

            var movementManager = gameLoop.MoveManager;
            var movementName = movementManager.MovementOf(piece);

            _chessPieceViewFactory.CreateChessPieceView(board, piece, movementName);

        }
    }
}