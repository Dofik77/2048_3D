using System;
using UnityEngine;

namespace App.Scripts.CubeMechanics
{
    public class CubeMouseControl : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Collider _cubePlane;
        [SerializeField] private float _offsetBorderPlane;
        
        private Transform _cube;
        
        public void Initialize(Cube currentCube)
        {
            _cube = currentCube.transform;
        }

        private void Update()
        {
            CubeTransform();
        }

        private void CubeTransform()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit raycastHit) && CheckBoundsOfPlane(raycastHit, _cubePlane))
            {
                _cube.transform.position = new Vector3( _cube.position.x,  _cube.position.y, raycastHit.point.z);
            }
        }

        private bool CheckBoundsOfPlane(RaycastHit raycastHit, Collider plane)
        {
            var leftBorder = plane.bounds.min.z + _offsetBorderPlane;
            var rightBorder = plane.bounds.max.z - _offsetBorderPlane;
            return raycastHit.point.z > leftBorder && raycastHit.point.z < rightBorder ;
        }
        
    }
}