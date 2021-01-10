using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardSystem
{
    public class Tile
    {
        public event EventHandler HightlightStatusChanged;

        public Position2 Position { get; }
        public bool Highlight { get => _highlight; 
            internal set {
                _highlight = value;
                OnHighlightStatusChanged(EventArgs.Empty);
            }
        }
        private bool _highlight;

        public Tile(int x, int y)
        {
            Position = new Position2 { X = x, Y = y };
        }

        protected virtual void OnHighlightStatusChanged(EventArgs args)
        {
            EventHandler handler = HightlightStatusChanged;
            handler?.Invoke(this, args);
        }
    }
}