﻿using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Utils
{
    public class ObjectPool: MonoBehaviour
    {
        private List<GameObject> _pooledObjects = new List<GameObject>();

        [SerializeField]
        private int _amount = 10;

        [SerializeField]
        private GameObject _pooledObject = null;

        [SerializeField]
        private Transform _parent = null;

        private void Awake()
        {
            for (int count = _pooledObjects.Count; count < _amount; count++)
            {
                var go = GameObject.Instantiate(_pooledObject, _parent);
                go.SetActive(false);
                _pooledObjects.Add(go);
            }
        }

        public GameObject GetPooledObject()
        {
            foreach (var pooledObject in _pooledObjects)
            {
                if (!pooledObject.activeInHierarchy)
                {
                    pooledObject.SetActive(true);
                    return pooledObject;
                }
            }

            var go = GameObject.Instantiate(_pooledObject, _parent);
            _pooledObjects.Add(go);

            return go;
        }
    }
}
