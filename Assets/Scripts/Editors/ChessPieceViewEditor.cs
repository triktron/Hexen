using UnityEngine;
using System.Collections;
using UnityEditor;
using GameSystem.Views;
using System;
using Utils;

namespace Editors
{
    [CustomEditor(typeof(ChessPieceView))]
    public class ChessPieceViewEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            var playerIDSp = serializedObject.FindProperty("_playerID");
            EditorGUILayout.PropertyField(playerIDSp);

            var movementNameSp = serializedObject.FindProperty("_movementName");
            var movementName = movementNameSp.stringValue;

            var typeNames = MoveComandProviderTypeHelper.FindMoveCommandProviderTypes();
            var selectedIndex = Array.IndexOf(typeNames, movementName);
            var newSelectedIndex = EditorGUILayout.Popup("Movement Name", selectedIndex, typeNames);

            if (selectedIndex != newSelectedIndex)
                movementNameSp.stringValue = typeNames[newSelectedIndex];

            serializedObject.ApplyModifiedProperties();
        }
    }
}