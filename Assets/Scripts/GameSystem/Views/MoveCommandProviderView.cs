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

        private void Start()
        {
            GameLoop.Instance.Initialized += OnGameInitialized;
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            var moveManager = GameLoop.Instance.MoveManager;

            moveManager.MoveComandProviderChanged += OnMoveManagerProviderChanged;
        }

        private void OnMoveManagerProviderChanged(object sender, MoveCommandProviderChanged<ChessPiece> e)
        {
            foreach (var moveComandView in _moveComandsViews)
            {
                GameObject.Destroy(moveComandView.gameObject);
            }

            _moveComandsViews.Clear();

            if (e.MoveCommandProvider != null)
            {
                var board = GameLoop.Instance.Board;
                var piece = GameLoop.Instance.SelectedPiece;
                foreach (var moveComand in e.MoveCommandProvider.MoveCommands())
                {
                    if (!moveComand.CanExecute(board, piece)) continue;
                    var view = GameObject.Instantiate(_moveComandView, transform);

                    view.Modal = moveComand;

                    _moveComandsViews.Add(view);
                }
            }
        }
    }
}