using BoardSystem;
using HexGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.States
{
    class EnemyPhase1GameState : GameStateBase
    {
        public override void OnEnter()
        {
            RandomizeEnemyPositions();

            StateMachine.MoveTo(GameStates.EnemyPhase2);
        }

        private void RandomizeEnemyPositions()
        {
            var enemies = new List<Modals.Piece>();

            foreach (var piece in GameLoop.Instance.Board.Pieces)
            {
                if (piece.PlayerID == 1) enemies.Add(piece);
            }

            foreach (var enemy in enemies)
            {
                if (enemy.QueuedPath.Count == 0) continue;

                var origin = enemy.QueuedPath.First();
                var destination = enemy.QueuedPath.Last();
                var destinationPiece = GameLoop.Instance.Board.PieceAt(destination);

                if (destinationPiece == null)
                {
                    GameLoop.Instance.Board.Move(origin, destination);
                }

                enemy.QueuedPath.Clear();
            }
        }
    }
}
