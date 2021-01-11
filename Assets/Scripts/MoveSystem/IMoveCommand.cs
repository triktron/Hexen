using BoardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveSystem
{
    public interface IMoveCommand<TPiece> where TPiece: class, IPiece<TPiece>
    {
        bool CanExecute(Board<TPiece> board, TPiece piece);
        List<Tile> Tiles(Board<TPiece> board, TPiece piece);
        List<Tile> Action(Board<TPiece> board, TPiece piece, Tile tile);

        void Execute(Board<TPiece> board, TPiece piece, Tile toTile);
    }
}
