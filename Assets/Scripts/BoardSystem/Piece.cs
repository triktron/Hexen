using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BoardSystem
{
    public class Piece
    {
        public int PlayerID { get; private set; }

        public bool HasMoved { get; private set; } = false;

        public Piece(int playerID)
        {
            PlayerID = playerID;
        }
    }
}
