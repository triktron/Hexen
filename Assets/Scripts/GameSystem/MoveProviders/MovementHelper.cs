using BoardSystem;
using GameSystem.Modals;
using HexGrid;
using HexGrid.Enum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem.MoveProvider
{
    public class MovementHelper
    {
        public delegate bool Validator(Board<Modals.Piece> board, Modals.Piece piece, Tile toTile);

        private Board<Modals.Piece> _board;
        private Modals.Piece _piece;
        private List<Tile> _validTile = new List<Tile>();

        public MovementHelper(Board<Modals.Piece> board, Modals.Piece piece)
        {
            _board = board;
            _piece = piece;
        }

        public MovementHelper Collect(CubicHexCoord offset, int step = int.MaxValue, params Validator[] validators)
        {
            Tile lastTile = _board.TileOf(_piece);
            for (int count = 1; count <= step; count++)
            {
                Tile tile = lastTile;

                var pos = tile.Position - offset;
                tile = _board.TileAt(pos);

                if (tile == null) break;
                lastTile = tile;

                var pawn = _board.PieceAt(tile);
                //if (pawn != null && pawn.PlayerID == _piece.PlayerID) continue;
                var valid = validators.All(v => v(_board, _piece, tile));

                if (pawn == null || valid)
                {
                    _validTile.Add(tile);
                }

                if (!valid) break;
            }

            return this;
        }

        public List<Tile> Generate()
        {
            return _validTile;
        }

        public MovementHelper Neigbours(int maxDistance, params Validator[] validators)
        {
            CubicHexCoord[] DIRECTIONS = {
            new CubicHexCoord(  1, -1,  0 ),
            new CubicHexCoord(  0, -1,  1 ),
            new CubicHexCoord( -1,  0,  1 ),
            new CubicHexCoord( -1,  1,  0 ),
            new CubicHexCoord(  0,  1, -1 ),
            new CubicHexCoord(  1,  0, -1 )
            };

            for (int i = 0; i < DIRECTIONS.Length; i++)
            {
                Collect(DIRECTIONS[i], maxDistance, validators);
            }

            return this;
        }

        public MovementHelper Diagonal(DiagonalEnum direction,params Validator[] validators)
        {
            return Collect(CubicHexCoord.DiagonalDiff(direction), validators: validators);
        }

        public static bool CanCapture(Board<Modals.Piece> board, Modals.Piece piece, Tile tile)
        {
            return board.PieceAt(tile) != null;
        }

        public static bool IsEmpty(Board<Modals.Piece> board, Modals.Piece piece, Tile tile)
        {
            return board.PieceAt(tile) == null;
        }
    }

}