using Deck;
using HexGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

namespace BoardSystem
{
    public class PiecePlacedEventArgs<TPiece> : EventArgs where TPiece : class, IPiece<TPiece>
    {
        public TPiece Piece { get;  }

        public PiecePlacedEventArgs(TPiece piece)
        {
            Piece = piece;
        }
    }

    public class Board<TPiece> where TPiece : class, IPiece<TPiece>
    {
        public event EventHandler<PiecePlacedEventArgs<TPiece>> PiecePlaced;

        private Dictionary<CubicHexCoord, Tile> _tiles = new Dictionary<CubicHexCoord, Tile>();

        private List<Tile> _keys = new List<Tile>();
        private List<TPiece> _values = new List<TPiece>();

        public readonly int Size;

        public List<Tile> Tiles => _tiles.Values.ToList();
        public List<TPiece> Pieces => _values;

        public DeckSystem Deck;

        public void UnHightlight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                if (tile != null) tile.Highlight = false;
            }
        }

        public Board(int size)
        {
            Size = size;

            InitTiles();
        }

        public void Highlight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.Highlight = true;
            }
        }

        
        public Tile TileAt(CubicHexCoord position)
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

            piece.Captured(this);

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

            piece.Moved(this, fromTile, toTile);
        }

        public void Place(Tile toTile, TPiece piece)
        {
            if (_keys.Contains(toTile))
                return;

            if (_values.Contains(piece))
                return;

            _keys.Add(toTile);
            _values.Add(piece);

            OnPiecePlaced(new PiecePlacedEventArgs<TPiece>(piece));
        }

        private void InitTiles()
        {
            var positions = BoardPositionHelper.GenerateBoard(Size);

            foreach (var position in positions)
            {
                _tiles.Add(position, new Tile(position.x, position.y, position.z));
            }
        }

        protected virtual void OnPiecePlaced(PiecePlacedEventArgs<TPiece> args)
        {
            EventHandler<PiecePlacedEventArgs<TPiece>> handler = PiecePlaced;
            handler?.Invoke(this, args);
        }
    }
}