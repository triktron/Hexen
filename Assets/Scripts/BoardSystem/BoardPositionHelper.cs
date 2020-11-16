using BoardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardPositionHelper
{
    public static Position WorldToBoardPosition(Vector3 worldPos)
    {
        return new Position { X = (int)worldPos.x, Y = (int)worldPos.z };
    }

    public static Vector3 BoardToWorldPosition(Position boardPos)
    {
        return new Vector3 { x = boardPos.X, z = boardPos.Y };
    }
}
