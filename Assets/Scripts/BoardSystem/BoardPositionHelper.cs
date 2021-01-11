using BoardSystem;
using HexGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardSystem
{
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

        public static List<CubicHexCoord> GenerateBoard(int size)
        {
            var options = new List<CubicHexCoord>();

            for (int q = -size; q <= size; q++)
            {
                int r1 = Mathf.Max(-size, -q - size);
                int r2 = Mathf.Min(size, -q + size);
                for (int r = r1; r <= r2; r++)
                {
                    options.Add(new CubicHexCoord(q, r, -q - r));
                }
            }

            return options;
        }
    }

}