using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using BoardSystem;
using System;

namespace GameSystem.Views
{
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        private Tile _modal;
        public Tile Modal 
        {
            get => _modal;
            set
            {
                if (_modal != null)
                    _modal.HightlightStatusChanged -= ModalHightlightStatusChanged;

                _modal = value;

                if (_modal != null)
                    _modal.HightlightStatusChanged += ModalHightlightStatusChanged;
            }
        }

        [SerializeField]
        private Material _highlightMaterial = null;

        private MeshRenderer _meshRenderer;
        private Material _origianlMaterial;

        private void ModalHightlightStatusChanged(object sender, EventArgs e)
        {
            _meshRenderer.material = Modal.Highlight ? _highlightMaterial : _origianlMaterial;
        }

        void Start()
        {
            GameLoop.Instance.Initialized += OnGameInitialized;

            _meshRenderer = GetComponent<MeshRenderer>();
            _origianlMaterial = _meshRenderer.sharedMaterial;
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            var tilePosition = BoardPositionHelper.WorldToBoardPosition(this.transform.position);
            Modal = GameLoop.Instance.Board.TileAt(tilePosition);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameLoop.Instance.Select(_modal);
        }

        private void OnDestroy()
        {
            Modal = null;
        }
    }
}