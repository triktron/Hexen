using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using BoardSystem;

namespace BoardSystem
{
    public interface IPiece
    {
        void Moved(Tile fromPosition, Tile toPosition);
        void Captured();
    }
}