using UnityEngine;
using System.Collections;
using MoveSystem;
using GameSystem.Modals;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class MoveComandView : MonoBehaviour, IPointerClickHandler
    {
        public IMoveCommand<ChessPiece> Modal { get; set; }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameLoop.Instance.Select(Modal);
        }
    }
}