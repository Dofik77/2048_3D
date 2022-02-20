﻿using System;
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
            PlaceCube(cube);
            var value = _valueOfCubes[UnityEngine.Random.Range(0, _valueOfCubes.Count)];
            cube.ChangeValue(value); // в Scriptable Object
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