using BoardSystem;
using GameSystem.Modals;
using System.Collections.Generic;
using System.Linq;

namespace GameSystem.States
{
    class EnemyPhase1GameState : GameStateBase
    {
        private Board<Piece> _board;
        public EnemyPhase1GameState(Board<Piece> board)
        {
            _board = board;
        }


        public override void OnEnter()
        {
            RandomizeEnemyPositions();

            StateMachine.MoveTo(GameStates.EnemyPhase2);
        }

        private void RandomizeEnemyPositions()
        {
            var enemies = new List<Piece>();

            foreach (var piece in _board.Pieces)
            {
                if (piece.PlayerID == 1) enemies.Add(piece);
            }

            foreach (var enemy in enemies)
            {
                if (enemy.QueuedPath.Count == 0) continue;

                var origin = enemy.QueuedPath.First();
                var destination = enemy.QueuedPath.Last();
                var destinationPiece = _board.PieceAt(destination);

                if (destinationPiece == null)
                {
                    _board.Move(origin, destination);
                }

                enemy.QueuedPath.Clear();
            }
        }
    }
}
