using App.Scripts.CubeMechanics;
using UnityEngine;

namespace App.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private CubeGenerator _generator;
        [SerializeField] private Slingshot _slingshot;
        
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
            _slingshot.Attach(cube);
            
            cube.CollideWithCube += CollideWithCube;
            
            _slingshot.StartAiming();
            _slingshot.CubeLaunched += OnCubeLaunched;
        }

        private void CollideWithCube(Cube launchedCube, Cube strikingCube) // вынести в отдельный файл? 
        {
            Debug.Log(launchedCube.Value + " and " +  strikingCube.Value);
            
            launchedCube.CollideWithCube -= CollideWithCube;
            strikingCube.CollideWithCube -= CollideWithCube;
            
            launchedCube.Deactivate();
            strikingCube.Deactivate();

            Cube newCube = _generator._cubePool.GetPooledObject();
            newCube.ChangeValue(launchedCube.Value * 2);
            newCube.transform.position = strikingCube.transform.position;

            newCube.CollideWithCube += CollideWithCube;
            newCube.Push(Vector3.up, 5);
            
            _generator.OnCubeCombined(launchedCube);
            _generator.OnCubeCombined(strikingCube);
        }

        private void OnCubeLaunched(Cube cube)
        {
            _slingshot.StopAiming();
            _slingshot.CubeLaunched -= OnCubeLaunched;
            
            _generator.Generate();
        }
    }
}