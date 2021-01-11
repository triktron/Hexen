using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameSystem.Modals;
using BoardSystem;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultPieceViewFactory", menuName = "GameSystem/PieceView Factory")]
    public class PieceViewFactory : ScriptableObject
    {
        [SerializeField]
        private List<GameObject> _players = new List<GameObject>();


        [SerializeField]
        private List<string> _movementNames = new List<string>();

        public PieceView CreatePieceView(Board<Modals.Piece> board, Modals.Piece modal)
        {
            var index = _movementNames.IndexOf(modal.MovementName);
            var prefab = _players[index];
            var gameObject = Instantiate(prefab);
            var pieceView = gameObject.GetComponentInChildren<PieceView>();
            var tile = board.TileOf(modal);
            gameObject.transform.position = BoardPositionHelper.BoardToWorldPosition(tile.Position);
            gameObject.name = $"Spauwned piece ( { modal.MovementName } )";
            pieceView.Modal = modal;

            return pieceView;
        }
    }
}