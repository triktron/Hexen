using UnityEngine;
using System.Collections;
using System;
using MoveSystem;
using GameSystem.Modals;
using System.Collections.Generic;

namespace GameSystem.Views
{
    public class MoveCommandProviderView : MonoBehaviour
    {
        [SerializeField]
        private MoveComandView _moveComandView = null;

        private List<MoveComandView> _moveComandsViews = new List<MoveComandView>();

        public IMoveCommandProvider<ChessPiece> Modal
        {
            set
            {
                UpdateCommandProviderViews(value);
            }
        }

        private void UpdateCommandProviderViews(IMoveCommandProvider<ChessPiece> moveCommandProvider)
        {
            foreach (var moveComandView in _moveComandsViews)
            {
                GameObject.Destroy(moveComandView.gameObject);
            }

            _moveComandsViews.Clear();

            if (moveCommandProvider != null)
            {
                foreach (var moveComand in moveCommandProvider.MoveCommands())
                {
                    var view = GameObject.Instantiate(_moveComandView, transform);

                    view.Modal = moveComand;

                    _moveComandsViews.Add(view);
                }
            }
        }
    }
}