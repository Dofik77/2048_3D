using UnityEngine;

namespace App.Scripts.CubeMechanics
{
    public class CubeCombiner : MonoBehaviour
    {
        [SerializeField] private CubeGenerator _generator;
        [SerializeField] private float _forceOfPush;
        [SerializeField] private CubeColorsSo _colorsSo;
        
        public void CollideWithCube(Cube launchedCube, Cube strikingCube)
        {
            launchedCube.CollideWithCube -= CollideWithCube;
            strikingCube.CollideWithCube -= CollideWithCube;
            
            launchedCube.Deactivate();
            strikingCube.Deactivate();

            Cube newCube = _generator._cubePool.GetPooledObject();
            
            //вынести в отдельный метод
            newCube.ChangeValue(launchedCube.Value * 2);
            newCube.ColorCube = _colorsSo.GetColor(newCube.Value);
            newCube.transform.position = strikingCube.transform.position;
            newCube.CollideWithCube += CollideWithCube;
            newCube.Push(Vector3.up, _forceOfPush);
            //вынести в отдельный метод
            
            _generator.OnCubeCombined(launchedCube);
            _generator.OnCubeCombined(strikingCube);
        }
    }
}