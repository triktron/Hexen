using System.Collections.Generic;

namespace GameSystem.Utils
{
    public class BreadthFirstAreaSearch<TPosition>
    {
        public delegate List<TPosition> NeighbourStrategy(TPosition from);

        public delegate float DistanceStrategy(TPosition from, TPosition to);

        private readonly NeighbourStrategy _neighbour;
        private readonly DistanceStrategy _distance;

        public BreadthFirstAreaSearch(NeighbourStrategy neighbour, DistanceStrategy distance)
        {
            _neighbour = neighbour;
            _distance = distance;
        }

        public List<TPosition> Area(TPosition centerPosition, float maxDistance)
        {
            var nearbyPositions = new List<TPosition>();
            nearbyPositions.Add(centerPosition);

            var nodeToVisit = new Queue<TPosition>();
            nodeToVisit.Enqueue(centerPosition);

            while (nodeToVisit.Count > 0)
            {
                var currentNode = nodeToVisit.Dequeue();

                var neigbours = _neighbour(currentNode);
                foreach (var neighbour in neigbours)
                {
                    if (nearbyPositions.Contains(neighbour)) continue;
                    if (_distance(centerPosition, neighbour) < maxDistance)
                    {
                        nearbyPositions.Add(neighbour);
                        nodeToVisit.Enqueue(neighbour);
                    }
                }
            }

            return nearbyPositions;
        }
    }
}
