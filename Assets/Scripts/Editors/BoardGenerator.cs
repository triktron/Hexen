using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using HexGrid;
using BoardSystem;

public class BoardGenerator : EditorWindow
{
    private GameObject _hexPrefab;
    private int _size = 5;

    [MenuItem("Tools/Generate Hex Board")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        BoardGenerator window = (BoardGenerator)EditorWindow.GetWindow(typeof(BoardGenerator));
        window.Show();
    }

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
        var positions = BoardPositionHelper.GenerateBoard(_size);

        foreach (var position in positions)
        {
            var hex = PrefabUtility.InstantiatePrefab(_hexPrefab) as GameObject;
            var pos = position.ToAxial().ToPixel();
            hex.transform.position = new Vector3(pos.x, 0, pos.y);
            hex.name = $"Hex (X:{position.x} Y:{position.y} Z:{position.z})";
        }
    }

    private void DeleteAllHex()
    {
        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++)
            if (gos[i].name.Contains("Hex"))
                DestroyImmediate(gos[i]);
    }
}