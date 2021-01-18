using System.Collections.Generic;
using UnityEngine;
using HexGrid;
using BoardSystem;

namespace GameSystem.Utils
{
    class RandomEnemyPlacer : MonoBehaviour
    {
        [SerializeField]
        private int _boardSize = 3;

        [SerializeField]
        private int _enemyAmount = 8;

        [SerializeField]
        private GameObject _enemyPrefab = null;

        public void PlaceRandomEnemies()
        {
            var spots = new List<CubicHexCoord>() { new CubicHexCoord() };
            var board = BoardPositionHelper.GenerateBoard(_boardSize);

            while (spots.Count <= _enemyAmount)
            {
                var go = Instantiate(_enemyPrefab);

                CubicHexCoord spot = GetRandomSpot(board);
                while (spots.Contains(spot)) spot = GetRandomSpot(board);

                go.transform.position = BoardPositionHelper.BoardToWorldPosition(spot);
                spots.Add(spot);
            }
        }

        private static CubicHexCoord GetRandomSpot(List<CubicHexCoord> board)
        {
            return board[UnityEngine.Random.Range(0, board.Count)];
        }
    }
}
