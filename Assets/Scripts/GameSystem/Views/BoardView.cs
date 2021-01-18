using BoardSystem;
using GameSystem.Modals;
using UnityEngine;

namespace GameSystem.Views
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField]
        private PieceViewFactory _PieceViewFactory = null;

        private Board<Piece> _modal;

        public Board<Piece> Modal
        {
            set
            {
                if (_modal != null)
                    _modal.PiecePlaced -= OnPiecePlaced;

                _modal = value;

                if (_modal != null)
                    _modal.PiecePlaced += OnPiecePlaced;
            }

            get => _modal;
        }

        private void OnPiecePlaced(object sender, PiecePlacedEventArgs<Piece> e)
        {
            var board = sender as Board<Piece>;
            var piece = e.Piece;

            _PieceViewFactory.CreatePieceView(board, piece);
        }

        private void OnDestroy()
        {
            Modal = null;
        }

#if UNITY_EDITOR
        static public void drawString(string text, Vector3 worldPos, Color? colour = null)
        {
            UnityEditor.Handles.BeginGUI();

            var restoreColor = GUI.color;

            if (colour.HasValue) GUI.color = colour.Value;
            var view = UnityEditor.SceneView.currentDrawingSceneView;
            Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

            if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
            {
                GUI.color = restoreColor;
                UnityEditor.Handles.EndGUI();
                return;
            }

            Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
            GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height, size.x, size.y), text);
            GUI.color = restoreColor;
            UnityEditor.Handles.EndGUI();
        }

        private void OnDrawGizmos()
        {
            if (GameLoop.Instance == null) return;
            foreach (var tile in GameLoop.Instance.Board.Tiles)
            {
                var worldpos = BoardPositionHelper.BoardToWorldPosition(tile.Position);
                drawString($"{tile.Position.x} {tile.Position.y} {tile.Position.z}", worldpos);
            }
        } 
#endif
    }
}