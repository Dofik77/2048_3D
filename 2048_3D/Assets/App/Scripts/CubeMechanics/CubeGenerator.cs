using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.CubeMechanics
{
    public class CubeGenerator : MonoBehaviour
    {
        public event Action<Cube> Spawned;

        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private GameObject _platformCubeSpawn;
        [SerializeField] private List<int> _valueOfCubes;
        [SerializeField] private float _delay;
        [SerializeField] private CubeColorsSo _colorsSo;
        [SerializeField] private CubeValueSo _valueSo;

        public CubePool _cubePool;
        private Vector3 platformPosition;
        
        private void Awake()
        {
            _cubePool = new CubePool(_cubePrefab);
            platformPosition = _platformCubeSpawn.transform.position;
        }

        public void Generate()
        {
            StartCoroutine(GenerateCoroutine());
        }

        private IEnumerator GenerateCoroutine()
        {
            yield return new WaitForSeconds(_delay);
            
            var cube = _cubePool.GetPooledObject();
            var value = _valueSo.GetValue();
            PlaceCube(cube);
            cube.ChangeValue(value);
            cube.ColorCube = _colorsSo.GetColor(cube.Value);
            
            Spawned?.Invoke(cube);
        }

        private void PlaceCube(Cube cube)
        {
            cube.transform.position =
                new Vector3(platformPosition.x, platformPosition.y + 0.5f, platformPosition.z);
        }

        public void OnCubeCombined(Cube cube)
        {
            _cubePool.ReturnObjectToPool(cube);
        }
    }
}