﻿using UnityEngine;
using System.Collections;
using MoveSystem;
using GameSystem.Modals;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using BoardSystem;

namespace GameSystem.Views
{
    public class MoveComandView : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public IMoveCommand<Modals.Piece> Modal { get; set; }

        private Canvas _canvas;
        private Transform _originalParent;
        private int _originalSiblingIndex;
        private LayerMask _tileLayer;

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        private void Awake()
        {
            _tileLayer = LayerMask.GetMask("Tiles"); ;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // Start moving object from the beginning!
            transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) / transform.lossyScale.x; // Thanks to the canvas scaler we need to devide pointer delta by canvas scale to match pointer movement.
                                                                                                                      // We need a few references from UI.
            if (!_canvas)
            {
                _canvas = GetComponentInParent<Canvas>();
            }

            _originalParent = transform.parent;
            _originalSiblingIndex = transform.GetSiblingIndex();

            // Change parent of our item to the canvas.
            transform.SetParent(_canvas.transform, true);
            // And set it as last child to be rendered on top of UI.
            transform.SetAsLastSibling();

            GameLoop.Instance.Select(Modal);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Continue moving object around screen.
            transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) / transform.lossyScale.x; // Thanks to the canvas scaler we need to devide pointer delta by canvas scale to match pointer movement.

            GameLoop.Instance.Hover(GetTileUnderMouse());
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            GameLoop.Instance.Select(GetTileUnderMouse());


            transform.SetParent(_originalParent);
            transform.SetSiblingIndex(_originalSiblingIndex);
        }

        public Tile GetTileUnderMouse()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo, 100, _tileLayer))
            {
                var tileView = hitInfo.transform.GetComponent<TileView>();
                if (tileView == null) return null;
                
                return tileView.Modal;
            }

            return null;
        }
    }
}