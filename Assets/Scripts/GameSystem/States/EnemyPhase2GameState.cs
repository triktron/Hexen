using BoardSystem;
using GameSystem.Modals;
using GameSystem.MoveProvider;
using HexGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

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
            var enemies = new List<Piece>();
            Piece player = null;

            foreach (var piece in GameLoop.Instance.Board.Pieces)
            {
                if (piece.PlayerID == 0) player = piece;
                if (piece.PlayerID == 1) enemies.Add(piece);
            }

            var availebleEnemies = enemies.ToList();
            var playerNeigberingTiles = GetPositionsAroundPlayer(player);


            List<Tile> NeighbourStrategy(Tile t) => Neighbours(GameLoop.Instance.Board, t);
            var pf = new AStarPathFinding<Tile>(NeighbourStrategy, Distance, Distance);

            foreach (var toTile in playerNeigberingTiles)
            {
                var toPosition = toTile.Position;
                var path = new List<Tile>();

                for (int i = 0; i < availebleEnemies.Count; i++)
                {
                    var fromTile = GameLoop.Instance.Board.TileOf(availebleEnemies[i]);

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

        private List<Tile> GetPositionsAroundPlayer(Piece player)
        {
            var validTiles = new MovementHelper(GameLoop.Instance.Board, player)
                .Neigbours(1, MovementHelper.IsEmpty)
                .Generate();

            return validTiles;
        }
    }
}
