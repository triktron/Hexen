using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using HexGrid;

public class BoardGenerator : EditorWindow
{
    public struct Position3
    {
        public int X;
        public int Y;
        public int Z;
    }

    public struct Position2
    {
        public int X;
        public int Y;
    }

    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/Generate Hex Board")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        BoardGenerator window = (BoardGenerator)EditorWindow.GetWindow(typeof(BoardGenerator));
        window.Show();
    }

    GameObject _hexPrefab;
    int _size = 5;

    void OnGUI()
    {
        _hexPrefab = EditorGUILayout.ObjectField("Hex Prefab", _hexPrefab, typeof(GameObject), false) as GameObject;
        _size = EditorGUILayout.IntField("Board Size", _size);

        if (GUILayout.Button("Delete and Generate"))
        {
            DeleteAllHex();
            GenerateGrid();
        }
    }

    private void GenerateGrid()
    {
        var positions = GetPositions(_size);

        foreach (var position in positions)
        {
            var hex = PrefabUtility.InstantiatePrefab(_hexPrefab) as GameObject;
            var pos = position.ToAxial().ToPixel();
            hex.transform.position = new Vector3(pos.x, 0, pos.y);
            hex.name = $"Hex (X:{position.x} Y:{position.y} Z:{position.z})";
        }
    }

    private List<CubicHexCoord> GetPositions(int size)
    {
        // generate axial cordinates
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

    private void DeleteAllHex()
    {
        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++)
            if (gos[i].name.Contains("Hex"))
                DestroyImmediate(gos[i]);
    }
}