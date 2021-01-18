using UnityEngine;
using MoveSystem;
using GameSystem.Modals;
using System.Collections.Generic;
using GameSystem.Utils;

namespace GameSystem.Views
{
    [RequireComponent(typeof(ObjectPool))]
    public class MoveCommandProviderView : MonoBehaviour
    {
        private List<MoveComandView> _moveComandsViews = new List<MoveComandView>();
        private ObjectPool _pool;

        public IMoveCommandProvider<Piece> Modal
        {
            set
            {
                UpdateCommandProviderViews(value);
            }
        }

        private void UpdateCommandProviderViews(IMoveCommandProvider<Piece> moveCommandProvider)
        {
            if (_pool == null) _pool = GetComponent<ObjectPool>();

            foreach (var moveComandView in _moveComandsViews)
            {
                moveComandView.gameObject.SetActive(false);
            }

            _moveComandsViews.Clear();

            if (moveCommandProvider != null)
            {
                foreach (var moveComand in moveCommandProvider.MoveCommands())
                {
                    var go = _pool.GetPooledObject();
                    var view = go.GetComponent<MoveComandView>();

                    view.Modal = moveComand;
                    _moveComandsViews.Add(view);
                }
            }
        }
    }
}