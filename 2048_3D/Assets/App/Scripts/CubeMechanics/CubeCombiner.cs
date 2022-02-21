using TheDeveloper.ColorChanger;
using UnityEngine;

namespace App.Scripts.CubeMechanics
{
    public class CubeCombiner : MonoBehaviour
    {
        [SerializeField] private CubeGenerator _generator;
        [SerializeField] private float _forceOfPush;
        [SerializeField] private float _forceOfTorque;
        [SerializeField] private CubeColorsSo _colorsSo;
        [SerializeField] private ParticleColorChanger _particlePrefab;
        
        public void CollideWithCube(Cube launchedCube, Cube strikingCube)
        {
            launchedCube.CollideWithCube -= CollideWithCube;
            strikingCube.CollideWithCube -= CollideWithCube;
            
            launchedCube.Deactivate();
            
            //InstantiateParticle(launchedCube);
            
            _generator.ReturnToPoolOnCubeCombined(launchedCube);

            CombineCube(strikingCube);
        }

        private void InstantiateParticle(Cube cube)
        {
            _particlePrefab.ChangeColor(cube.ColorCube);
            //TODO
        }

        public void CombineCube(Cube strikingCube)
        {
            strikingCube.ChangeValue(strikingCube.Value * 2);
            strikingCube.ColorCube = _colorsSo.GetColor(strikingCube.Value);  
            
            strikingCube.CollideWithCube += CollideWithCube;
            
            strikingCube.Push(Vector3.up, _forceOfPush);
            strikingCube.Torque(Random.rotation.eulerAngles, _forceOfTorque);
        }
    }
}