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

        private void OnEnable()
        {
            _generation.Spawned += OnCubeSpawn;
        }

        private void OnDisable()
        {
            _generation.Spawned -= OnCubeSpawn;
        }

        public void GenerateCube()
        {
            _generation.Spawning();
        }
        
        private void OnCubeSpawn(Cube cube)
        {
            _cubeMouseControl.Initialize(cube);
            
            var value = _valueOfCubes[UnityEngine.Random.Range(0, _valueOfCubes.Count - 1)]; // cube generate должен задавать Value
            cube.Initilize(value);
            
            cube.OnCubeCombine += CubeCombine;
            cube.OnCubeStopped += CubeStopped;
            
            _cubeMouseControl.StartMove();
            _cubeMouseControl.CubeIsLauched += CubeLaucnhedByMouse;
        }

        private void CubeLaucnhedByMouse(Cube cube)
        {
            _cubeMouseControl.StopMove();
            _cubeMouseControl.CubeIsLauched -= CubeLaucnhedByMouse;
        }

        private void CubeStopped(Cube cube)
        {
            cube.OnCubeStopped -= CubeStopped;
            GenerateCube();
        }
        
        private void CubeCombine(Cube launchedCube, Cube strikingCube)
        {
            Cube newCube = _generation._cubePool.GetPooledObject();

            newCube.ChangeCubeColor(newCube);
            newCube.ChangeCubeValue(newCube);
            
            newCube.transform.position = strikingCube.transform.position;

            launchedCube.OnCubeCombine -= CubeCombine;
            strikingCube.OnCubeCombine -= CubeCombine;
            
            _generation.OnCubeCombine(launchedCube);
            _generation.OnCubeCombine(strikingCube);
        }
    }
}