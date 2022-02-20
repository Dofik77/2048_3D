using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

namespace App.Scripts.CubeMechanics
{
    public class Cubes : MonoBehaviour
    {
        [SerializeField] private CubeGeneration _generation;
        [SerializeField] private List<int> _valueOfCubes;
        [SerializeField] private Slingshot slingshot;

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
            slingshot.Attach(cube);
            
            var value = _valueOfCubes[UnityEngine.Random.Range(0, _valueOfCubes.Count)]; // cube generate должен задавать Value
            cube.ChangeValue(value);

            cube.CubeStopped += CubeStopped;
            cube.CubeCollided += CubeCollided;
            
            slingshot.StartAiming();
            slingshot.CubeLaunched += CubeOutControllOfMouse;
        }
        
        private void CubeStopped(Cube cube)
        {
            cube.CubeStopped -= CubeStopped;
            
            if(cube == _actualCube)
                _generation.StartSpawning();
        }

        private void CubeCollided(Cube launchedCube, Cube strikingCube)
        {
            Debug.Log(launchedCube.Value + " and " +  strikingCube.Value);
            
            launchedCube.CubeCollided -= CubeCollided;
            strikingCube.CubeCollided -= CubeCollided;
            
            launchedCube.CubeStopped -= CubeStopped;
            strikingCube.CubeStopped -= CubeStopped;
            
            launchedCube.Deactive(launchedCube);
            strikingCube.Deactive(strikingCube);

            Cube newCube = _generation._cubePool.GetPooledObject();
            newCube.ChangeValue(launchedCube.Value * 2);
            newCube.transform.position = strikingCube.transform.position;

            newCube.CubeCollided += CubeCollided;
            newCube.Jump(newCube);
            
            _generation.OnCubeCombined(launchedCube);
            _generation.OnCubeCombined(strikingCube);
        }

        private void CubeOutControllOfMouse(Cube cube)
        {
            slingshot.StopAiming();
            slingshot.CubeLaunched -= CubeOutControllOfMouse;
        }
    }
}