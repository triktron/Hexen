using UnityEngine;
using UnityEditor;
using System;

namespace Utils
{
    public class MoveCommandProviderAttribute : Attribute
    {
        public string Name;

        public MoveCommandProviderAttribute(string name)
        {
            Name = name;
        }
    }
}