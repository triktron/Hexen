using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using GameSystem.Modals;
using System;

namespace GameSystem.Views
{
    public class ChessPieceView : MonoBehaviour, IPointerClickHandler
    {
        public string MovementName => _movementName;

        [SerializeField]
        private int _playerID = 0;

        [SerializeField]
        private string _movementName = null;

        public int PlayerID => _playerID;

        [SerializeField]
        private bool _facingBack = false;
        public bool FacingBack => _facingBack;

        private ChessPiece _modal;
        public ChessPiece Modal
        {
            get => _modal;
            set
            {
                if (_modal != null)
                {
                    _modal.ChessPieceMoved -= ModalMoved;
                    _modal.ChessPieceCaptured -= ModalCaptured;
                }

                _modal = value;

                if (_modal != null)
                {
                    _modal.ChessPieceMoved += ModalMoved;
                    _modal.ChessPieceCaptured += ModalCaptured;
                }
            }
        }

        private void ModalCaptured(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }

        private void ModalMoved(object sender, ChessPieceMovedEventArgs e)
        {
            transform.position = BoardPositionHelper.BoardToWorldPosition(e.To.Position);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameLoop.Instance.Select(Modal);
        }

        private void OnDestroy()
        {
            Modal = null;
        }
    }
}