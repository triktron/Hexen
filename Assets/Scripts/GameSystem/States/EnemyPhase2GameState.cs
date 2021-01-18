using HexGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.States
{
    class EnemyPhase2GameState : GameStateBase
    {
        public override void OnEnter()
        {
            RandomizeEnemyPositions();

            StateMachine.MoveTo(GameStates.Play);
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
                var tile = GameLoop.Instance.Board.TileOf(enemy);
                var tileright = tile.Position + new CubicHexCoord(1, -1, 0);
                var aboveTile = GameLoop.Instance.Board.TileAt(tileright);

                if (aboveTile != null)
                {
                    enemy.QueuedPath = new List<BoardSystem.Tile>() { tile, aboveTile };
                }
            }
        }
    }
}
