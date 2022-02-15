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

        public CubePool _cubePool;
        private Vector3 platformPosition;

        private Cube _actualCube;
        
        private void Awake()
        {
            _cubePool = new CubePool(_cubePrefab);
            platformPosition = _platformCubeSpawn.transform.position;
        }

        public Cube Spawning()
        {
            var cube = _cubePool.GetPooledObject();
            PlaceCube(cube);
            Spawned?.Invoke(cube);

            return cube;
        }

        private void PlaceCube(Cube cube)
        {
            cube.transform.position = 
                new Vector3(platformPosition.x, platformPosition.y + 0.5f, platformPosition.z);
        }

        public void OnCubeCombine(Cube cube)
        {
            _cubePool.ReturnObjectToPool(cube);
        }
    }
}