using BoardSystem;
using GameSystem.Modals;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem.MoveProvider
{
    public class MovementHelper
    {
        public delegate bool Validator(Board<ChessPiece> board, ChessPiece piece, Tile toTile);

        private Board<ChessPiece> _board;
        private ChessPiece _piece;
        private List<Tile> _validTile = new List<Tile>();

        public MovementHelper(Board<ChessPiece> board, ChessPiece piece)
        {
            _board = board;
            _piece = piece;
        }

        public MovementHelper Collect(int xOffset, int yOffset, int step = int.MaxValue, params Validator[] validators)
        {
            // rotate offset if needed
            if (_piece.FacingBack) yOffset *= -1;

            Tile lastTile = _board.TileOf(_piece);
            for (int count = 1; count <= step; count++)
            {
                Tile tile = lastTile;
                // move forward or backwards
                if (xOffset != 0)
                    for (int x = 0; x < Mathf.Abs(xOffset); x++)
                    {
                        if (tile == null) break;

                        tile = _board.TileAt(tile.Position + new Position { X = (int)Mathf.Sign(xOffset) });
                    }

                // move left or right
                if (yOffset != 0)
                    for (int y = 0; y < Mathf.Abs(yOffset); y++)
                    {
                        if (tile == null) break;

                        tile = _board.TileAt(tile.Position + new Position { Y = (int)Mathf.Sign(yOffset) });
                    }


                if (tile == null) break;
                lastTile = tile;

                var pawn = _board.PieceAt(tile);
                if ((pawn == null || pawn.PlayerID != _piece.PlayerID) && validators.All(v => v(_board, _piece, tile)))
                {
                    _validTile.Add(tile);
                }

                if (pawn != null) break;
            }

            return this;
        }

        public List<Tile> Generate()
        {
            return _validTile;
        }

        public MovementHelper North(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(0, 1, step, validators);
        }

        public MovementHelper NorthEast(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(1, 1, step, validators);
        }

        public MovementHelper East(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(1, 0, step, validators);
        }

        public MovementHelper EastSouth(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(1, -1, step, validators);
        }

        public MovementHelper South(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(0, -1, step, validators);
        }

        public MovementHelper SouthWest(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(-1, -1, step, validators);
        }

        public MovementHelper West(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(-1, 0, step, validators);
        }

        public MovementHelper NorthWest(int step = int.MaxValue, params Validator[] validators)
        {
            return Collect(-1, 1, step, validators);
        }

        public static bool CanCapture(Board<ChessPiece> board, ChessPiece piece, Tile tile)
        {
            return board.PieceAt(tile) != null;
        }

        public static bool IsEmpty(Board<ChessPiece> board, ChessPiece piece, Tile tile)
        {
            return board.PieceAt(tile) == null;
        }
    }

}