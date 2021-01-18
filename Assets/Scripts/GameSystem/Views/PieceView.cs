using UnityEngine;
using GameSystem.Modals;
using System;
using BoardSystem;

namespace GameSystem.Views
{
    [SelectionBase]
    public class PieceView : MonoBehaviour
    {
        public string MovementName => _movementName;

        [SerializeField]
        private int _playerID = 0;

        public int PlayerID => _playerID;

        [SerializeField]
        private string _movementName = null;

        private Piece _modal;
        public Piece Modal
        {
            get => _modal;
            set
            {
                if (_modal != null)
                {
                    _modal.PieceMoved -= ModalMoved;
                    _modal.PieceCaptured -= ModalCaptured;
                }

                _modal = value;

                if (_modal != null)
                {
                    _modal.PieceMoved += ModalMoved;
                    _modal.PieceCaptured += ModalCaptured;
                }
            }
        }

        private void ModalCaptured(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }

        private void ModalMoved(object sender, PieceMovedEventArgs e)
        {
            transform.position = BoardPositionHelper.BoardToWorldPosition(e.To.Position);
        }

        private void OnDestroy()
        {
            Modal = null;
        }
    }
}