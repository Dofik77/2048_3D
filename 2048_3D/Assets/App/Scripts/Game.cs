using System;
using System.Threading;
using App.Scripts.CubeMechanics;
using UnityEngine;

namespace App.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private Cubes _cubes;
        [SerializeField] private CubeGeneration _cubeGeneration;

        private Cube _actualCube;

        private void Start()
        {
            _cubeGeneration.Spawning();
        }
    }
}