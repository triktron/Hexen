using HexGrid;
using System;
using System.Collections.Generic;

namespace BoardSystem
{
    public class Tile
    {
        public event EventHandler HightlightStatusChanged;

        public CubicHexCoord Position { get; }
        public bool Highlight { get => _highlight; 
            internal set {
                _highlight = value;
                OnHighlightStatusChanged(EventArgs.Empty);
            }
        }
        private bool _highlight;

        public Tile(int x, int y, int z)
        {
            Position = new CubicHexCoord(x,y,z);
        }

        protected virtual void OnHighlightStatusChanged(EventArgs args)
        {
            EventHandler handler = HightlightStatusChanged;
            handler?.Invoke(this, args);
        }

        public override bool Equals(object obj)
        {
            return obj is Tile tile &&
                   EqualityComparer<CubicHexCoord>.Default.Equals(Position, tile.Position);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}