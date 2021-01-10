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
            _meshRenderer = GetComponent<MeshRenderer>();
            _origianlMaterial = _meshRenderer.sharedMaterial;
            var cb = BoardPositionHelper.WorldToBoardPosition(transform.position);
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