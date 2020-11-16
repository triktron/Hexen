using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

namespace BoardSystem
{
    public class Board<TPiece> where TPiece : class, IPiece
    {
        private Dictionary<Position, Tile> _tiles = new Dictionary<Position, Tile>();

        private List<Tile> _keys = new List<Tile>();
        private List<TPiece> _values = new List<TPiece>();

        public readonly int Rows;
        public readonly int Columns;

        public List<Tile> Tiles => _tiles.Values.ToList();

        public void UnHightlight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.Highlight = false;
            }
        }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            InitTiles();
        }

        public void Highlight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.Highlight = true;
            }
        }

        
        public Tile TileAt(Position position)
        {
            if (_tiles.TryGetValue(position, out var tile))
                return tile;


            return null;
        }

        public TPiece PieceAt(Tile tile)
        {
            var idx = _keys.IndexOf(tile);
            if (idx == -1)
                return default(TPiece);

            return _values[idx];
        }

        public Tile TileOf(TPiece piece)
        {
            var idx = _values.IndexOf(piece);
            if (idx == -1)
                return null;

            return _keys[idx];
        }

        public TPiece Take(Tile fromTile)
        {
            var idx = _keys.IndexOf(fromTile);
            if (idx == -1)
                return default(TPiece);

            var piece = _values[idx];

            piece.Captured();

            _values.RemoveAt(idx);
            _keys.RemoveAt(idx);

            return piece;
        }

        public void Move(Tile fromTile, Tile toTile)
        {
            var idx = _keys.IndexOf(fromTile);
            if (idx == -1)
                return;

            var toPiece = PieceAt(toTile);
            if (toPiece != null)
                return;

            _keys[idx] = toTile;

            var piece = _values[idx];

            piece.Moved(fromTile, toTile);
        }

        public void Place(Tile toTile, TPiece piece)
        {
            if (_keys.Contains(toTile))
                return;

            if (_values.Contains(piece))
                return;

            _keys.Add(toTile);
            _values.Add(piece);
        }

        private void InitTiles()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    _tiles.Add(new Position { X = x, Y = y }, new Tile(x, y));
                }
            }
        }
    }
}