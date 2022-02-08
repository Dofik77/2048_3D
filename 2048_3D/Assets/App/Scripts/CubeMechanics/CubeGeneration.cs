using System;
using System.Collections;
using UnityEngine;

namespace App.Scripts.CubeMechanics
{
    public class CubeGeneration : MonoBehaviour
    {
        public event Action<Cube> Spawned;

        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private GameObject _platformCubeSpawn;

        private CubePool _cubePool;

        private void Awake()
        {
            _cubePool = new CubePool(_cubePrefab);
        }

        private void Spawning()
        {
            var cube = _cubePool.GetPooledObject();
            PlaceCube(cube);
        }

        private void PlaceCube(Cube cube)
        {
            cube.transform.position = _platformCubeSpawn.transform.position;
            _cubePrefab.DestroyCube += OnCubeCollideWith;
        }

        private void OnCubeCollideWith(Cube cube)
        {
            cube.DestroyCube -= OnCubeCollideWith;
            _cubePool.ReturnObjectToPool(cube);
        }
    }
}