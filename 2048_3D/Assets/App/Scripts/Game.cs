using App.Scripts.CubeMechanics;
using UnityEngine;

namespace App.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private CubeGenerator _generator;
        [SerializeField] private Slingshot _slingshot;
        [SerializeField] private CubeCombiner _combiner;
        
        private Cube _actualCube;

        private void Start()
        {
            _generator.Generate();
        }

        private void OnEnable()
        {
            _generator.Spawned += OnCubeSpawn;
        }

        private void OnDisable()
        {
            _generator.Spawned -= OnCubeSpawn;
        }

        private void OnCubeSpawn(Cube cube)
        {
            _actualCube = cube;
            cube.CollideWithCube += _combiner.CollideWithCube;
            
            _slingshot.Attach(cube);
            _slingshot.StartAiming();
            _slingshot.CubeLaunched += OnCubeLaunched;
        }

        private void OnCubeLaunched(Cube cube)
        {
            _slingshot.StopAiming();
            _slingshot.CubeLaunched -= OnCubeLaunched;
            
            _generator.Generate();
        }
    }
}