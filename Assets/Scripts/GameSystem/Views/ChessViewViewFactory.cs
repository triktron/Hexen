using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameSystem.Modals;
using BoardSystem;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultChessPieceViewFactory", menuName = "GameSystem/ChessPieceView Factory")]
    public class ChessViewViewFactory : ScriptableObject
    {
        [SerializeField]
        private List<GameObject> _players = new List<GameObject>();


        [SerializeField]
        private List<string> _movementNames = new List<string>();

        public ChessPieceView CreateChessPieceView(Board<ChessPiece> board, ChessPiece modal)
        {
            var index = _movementNames.IndexOf(modal.MovementName);
            var prefab = _players[index];
            var gameObject = Instantiate(prefab);
            var chessPieceView = gameObject.GetComponentInChildren<ChessPieceView>();
            var tile = board.TileOf(modal);
            gameObject.transform.position = BoardPositionHelper.BoardToWorldPosition(tile.Position);
            gameObject.name = $"Spauwned chess piece ( { modal.MovementName } )";
            chessPieceView.Modal = modal;

            return chessPieceView;
        }
    }
}