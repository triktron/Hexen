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
        private List<GameObject> _darkChessPieceViews = new List<GameObject>();


        [SerializeField]
        private List<GameObject> _lightChessPieceViews = new List<GameObject>();

        [SerializeField]
        private List<string> _movementNames = new List<string>();

        public ChessPieceView CreateChessPieceView(Board<ChessPiece> board, ChessPiece modal, string movementName)
        {
            var list = modal.PlayerID == 0 ? _lightChessPieceViews : _darkChessPieceViews;

            var index = _movementNames.IndexOf(movementName);
            var prefab = list[index];
            var gameObject = Instantiate(prefab);
            var chessPieceView = gameObject.GetComponentInChildren<ChessPieceView>();
            var tile = board.TileOf(modal);
            gameObject.transform.position = BoardPositionHelper.BoardToWorldPosition(tile.Position);
            gameObject.name = $"Spauwned chess piece ( { movementName } )";
            chessPieceView.Modal = modal;

            return chessPieceView;
        }
    }
}