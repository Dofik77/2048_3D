using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.CubeMechanics
{
    public class CubePool
    {
        private readonly Stack<Cube> _pooledObjects;
        private readonly Cube _prefab;

        public Stack<Cube> PooledObjects => _pooledObjects;

        public CubePool(Cube prefab)
        {
            _prefab = prefab;
            _pooledObjects = new Stack<Cube>();
        }

        private bool HasObjects => _pooledObjects.Count > 0;

        public Cube GetPooledObject()
        {
            return HasObjects
                ? ReturnObjectFromStack()
                : ReturnNewObject();
        }
        
        public void ReturnObjectToPool(Cube poolObject)
        {
            poolObject.gameObject.SetActive(false);
            _pooledObjects.Push(poolObject);
        }
        
        private Cube ReturnObjectFromStack()
        {
            var cube = _pooledObjects.Pop();
            cube.gameObject.SetActive(true);
            
            return cube;
        }

        private Cube ReturnNewObject()
        {
            return Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        }
    }
}