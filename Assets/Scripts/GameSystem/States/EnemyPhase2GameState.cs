using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveProvider;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace GameSystem.States
{
    class EnemyPhase2GameState : GameStateBase
    {
        private Board<Piece> _board;
        public EnemyPhase2GameState(Board<Piece> board)
        {
            _board = board;
        }

        public override void OnEnter()
        {
            GeneratePaths();

            StateMachine.MoveTo(GameStates.Play);
        }

        private void GeneratePaths()
        {
            var enemies = new List<Piece>();
            Piece player = null;

            foreach (var piece in _board.Pieces)
            {
                if (piece.PlayerID == 0) player = piece;
                if (piece.PlayerID == 1) enemies.Add(piece);
            }

            var availebleEnemies = enemies.ToList();
            var playerTile = _board.TileOf(player);
            var playerNeigberingTiles = Neighbours(_board, playerTile);

            List<Tile> NeighbourStrategy(Tile t) => Neighbours(_board, t);
            var pf = new AStarPathFinding<Tile>(NeighbourStrategy, Distance, Distance);

            foreach (var toTile in playerNeigberingTiles)
            {
                var toPosition = toTile.Position;
                var path = new List<Tile>();

                for (int i = 0; i < availebleEnemies.Count; i++)
                {
                    var fromTile = _board.TileOf(availebleEnemies[i]);

                    path = pf.Path(fromTile, toTile);

                    if (path.Count > 0)
                    {
                        availebleEnemies[i].QueuedPath = path;
                        availebleEnemies.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private float Distance(Tile from, Tile to)
        {
            return from.Position.DistanceTo(to.Position);
        }

        private List<Tile> Neighbours(Board<Piece> board, Tile from)
        {
            var validTiles = new MovementHelper(board, from)
                .Neigbours(1, MovementHelper.IsEmpty)
                .Generate();

            return validTiles;
        }
    }
}
