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
        
        private List<Cube> _cubes = new List<Cube>();
        

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
            cube.CollideWithCube += OnCubeCollideCube;
            _cubes.Add(cube);
        
            var value = _valueOfCubes[UnityEngine.Random.Range(0, _valueOfCubes.Count - 1)];
            cube.Initilize(_speed, value);
        }

        private void OnCubeCollideCube(Cube launchedCube, Cube strikingCube)
        {
            launchedCube.CollideWithCube -= OnCubeCollideCube;
            //передавать launchedCube генератору и возвращать в кучу его

            _cubes.Remove(launchedCube);
            
            strikingCube.ChangeCubeValue(strikingCube);
            
        }
        
    }
}