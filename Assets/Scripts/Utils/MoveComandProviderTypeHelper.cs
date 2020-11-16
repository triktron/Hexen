using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Utils
{
    public class MoveComandProviderTypeHelper 
    {
        private static string[] _movementTypes = new string[0];

        public static string[] FindMoveCommandProviderTypes()
        {
            if (_movementTypes.Length == 0)
                _movementTypes = InternalFindMoveCommandProviderTypes();

            return _movementTypes;
        }

        private static string[] InternalFindMoveCommandProviderTypes()
        {
            var types = new List<string>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var attribute = type.GetCustomAttribute<MoveCommandProviderAttribute>();
                    if (attribute != null)
                    {
                        types.Add(attribute.Name);
                    }
                }
            }

            return types.ToArray();
        }
    }
}