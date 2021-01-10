using BoardSystem;
using HexGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardPositionHelper
{
    public static CubicHexCoord WorldToBoardPosition(Vector3 worldPos)
    {
        return AxialHexCoord.FromPixel(new Vector2(worldPos.x, worldPos.z));
    }

    public static Vector3 BoardToWorldPosition(CubicHexCoord boardPos)
    {
        var worldPos = boardPos.ToAxial().ToPixel();
        return new Vector3 { x = worldPos.x, z = worldPos.y };
    }
}
