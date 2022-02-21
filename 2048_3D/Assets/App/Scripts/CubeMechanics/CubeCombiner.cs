using UnityEngine;

namespace App.Scripts.CubeMechanics
{
    public class CubeCombiner : MonoBehaviour
    {
        [SerializeField] private CubeGenerator _generator;
        [SerializeField] private float _forceOfPush;
        [SerializeField] private float _forceOfTorque;
        [SerializeField] private CubeColorsSo _colorsSo;
        
        public void CollideWithCube(Cube launchedCube, Cube strikingCube)
        {
            launchedCube.CollideWithCube -= CollideWithCube;
            strikingCube.CollideWithCube -= CollideWithCube;
            
            launchedCube.Deactivate();
            strikingCube.Deactivate();
            
            _generator.OnCubeCombined(launchedCube);
            _generator.OnCubeCombined(strikingCube);

            CreateCombineCube(launchedCube, strikingCube);
        }

        public void CreateCombineCube(Cube launchedCube, Cube strikingCube)
        {
            Cube newCube = _generator._cubePool.GetPooledObject();
            
            newCube.ChangeValue(launchedCube.Value * 2);
            newCube.ColorCube = _colorsSo.GetColor(newCube.Value);
            
            newCube.transform.position = strikingCube.transform.position;
            newCube.CollideWithCube += CollideWithCube;
            newCube.Push(Vector3.up, _forceOfPush);
            newCube.Torque(Vector3.up, _forceOfTorque);
            //random rotate after push and Torque, realization in cube class
        }
    }
}