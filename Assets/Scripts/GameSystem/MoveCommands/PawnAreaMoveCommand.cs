using BoardSystem;
using GameSystem.Modals;
using GameSystem.Utils;
using ReplaySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.MoveCommands
{
    class PawnAreaMoveCommand : AbstractBasicMoveCommand
    {
        public PawnAreaMoveCommand(ReplayManager replayManager) : base(replayManager)
        {
        }

        public override List<Tile> Tiles(Board<ChessPiece> board, ChessPiece piece)
        {
            var tile = board.TileOf(piece);


            List<Tile> NeighbourStrategy(Tile t) =>  Neighbours(board, t);

            var bfs = new BreadthFirstAreaSearch<Tile>(NeighbourStrategy, Distance);

            return bfs.Area(tile, 3.5f);
        }

        private float Distance(Tile from, Tile to)
        {
            var fromPosition = from.Position;
            var toPosition = to.Position;

            var delta = fromPosition - toPosition;

            var distance = Mathf.Sqrt(delta.X * delta.X + delta.Y * delta.Y);

            return distance;
        }

        private List<Tile> Neighbours(Board<ChessPiece> board, Tile from)
        {
            var neigbours = new List<Tile>();
            
            var position = from.Position;

            var upPosition = position;
            upPosition.Y += 1;
            var upTile = board.TileAt(upPosition);
            if (upTile != null) neigbours.Add(upTile);

            var downPosition = position;
            downPosition.Y -= 1;
            var downTile = board.TileAt(downPosition);
            if (downTile != null) neigbours.Add(downTile);

            var rightPosition = position;
            rightPosition.X += 1;
            var rightTile = board.TileAt(rightPosition);
            if (rightTile != null) neigbours.Add(rightTile);

            var leftPosition = position;
            leftPosition.X -= 1;
            var leftTile = board.TileAt(leftPosition);
            if (leftTile != null) neigbours.Add(leftTile);

            return neigbours;
        }
    }
}
