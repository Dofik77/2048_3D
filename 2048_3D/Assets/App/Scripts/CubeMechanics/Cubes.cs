using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace App.Scripts.CubeMechanics
{
    public class Cubes : MonoBehaviour
    {
        [SerializeField] private CubeGeneration _generation;
        [SerializeField] private List<int> _valueOfCubes;
        [SerializeField] private CubeMouseControl _cubeMouseControl;

        public Cube _actualCube;

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
            _actualCube = cube;
            _cubeMouseControl.Initialize(cube);
            
            var value = _valueOfCubes[UnityEngine.Random.Range(0, _valueOfCubes.Count)]; // cube generate должен задавать Value
            cube.Initilize(value);

            cube.OnCubeStopedByObjects += CubeStopedByObjects;
            cube.OnCubeCombine += CubeCombine;
            
            _cubeMouseControl.StartMove();
            _cubeMouseControl.CubeLaunch += CubeOutControllOfMouse;
        }
        
        private void CubeStopedByObjects(Cube cube)
        {
            cube.OnCubeStopedByObjects -= CubeStopedByObjects;
            
            if(cube.gameObject == _actualCube.gameObject)
                _generation.Spawning();
        }

        private void CubeCombine(Cube launchedCube, Cube strikingCube)
        {
            _generation.OnCubeCombine(launchedCube);
            _generation.OnCubeCombine(strikingCube);

            Cube newCube = _generation._cubePool.GetPooledObject();
            newCube.ChangeCubeValue(launchedCube.ValueOfCube);
            newCube.transform.position = strikingCube.transform.position;

            newCube.OnCubeCombine += CubeCombine;
            //отписаться от CubeCombine 
        }
        
        
        
        private void CubeOutControllOfMouse(Cube cube)
        {
            _cubeMouseControl.StopMove();
            _cubeMouseControl.CubeLaunch -= CubeOutControllOfMouse;
        }
    }
}