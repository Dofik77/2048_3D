using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace App.Scripts.CubeMechanics
{
    public class Cubes : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private CubeGeneration _generation;
        [SerializeField] private List<int> _valueOfCubes;
        [SerializeField] private CubeMouseControl _cubeMouseControl;

        private void OnEnable()
        {
            _generation.Spawned += OnCubeSpawn;
        }

        private void OnDisable()
        {
            _generation.Spawned -= OnCubeSpawn;
        }
        
        private void OnCubeSpawn(Cube cube)
        {
            cube.CombineCube += CubeCombine;
            
            _cubeMouseControl.Initialize(cube);
                
            var value = _valueOfCubes[UnityEngine.Random.Range(0, _valueOfCubes.Count - 1)];
            cube.Initilize(_speed, value);
        }

        private void CubeCombine(Cube launchedCube, Cube strikingCube)
        {
            Cube cube = _generation._cubePool.GetPooledObject();

            cube.ChangeCubeColor(cube);
            cube.ChangeCubeValue(cube);
            
            
            cube.transform.position = strikingCube.transform.position;

            launchedCube.CombineCube -= CubeCombine;
            strikingCube.CombineCube -= CubeCombine;
            
            _generation.OnCubeCombine(launchedCube);
            _generation.OnCubeCombine(strikingCube);
            
            
        }
    }
}