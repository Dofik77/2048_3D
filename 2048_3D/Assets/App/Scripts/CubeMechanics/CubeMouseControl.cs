using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace App.Scripts.CubeMechanics
{
    public class CubeMouseControl : MonoBehaviour
    {
        public event Action<Cube> CubeLaunch;
    
        [SerializeField] private Camera _camera;
        [SerializeField] private Collider _cubePlane;
        [SerializeField] private float _offsetBorderPlane;
        [SerializeField] private Cubes _cubes;
        
        private Rigidbody _rb;
        private Vector3 _vectorMove = Vector3.forward;

        private Cube _actualCube;

        private Coroutine _moveCoroutine;

        public void Initialize(Cube cube)
        {
            _actualCube = cube;
        }
        
        public void StartMove()
        {
            _moveCoroutine = StartCoroutine(CubeMouseMove());
        }
        
        public void StopMove()
        {
            StopCoroutine(_moveCoroutine);
        }

        private IEnumerator CubeMouseMove()
        {
            while (true)
            {
                yield return null;

                CubeMouseMove(_actualCube);
                LaunchCube(CanLaunch(), _actualCube);
            }
        }
        

        private void CubeMouseMove(Cube cube)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit raycastHit) && CheckBoundsOfPlane(raycastHit, _cubePlane))
            {
                var position = cube.transform.position;
                position = new Vector3(raycastHit.point.x,  position.y, position.z);
                cube.transform.position = position;
            }
        }

        private bool CheckBoundsOfPlane(RaycastHit raycastHit, Collider plane)
        {
            var leftBorder = plane.bounds.min.x + _offsetBorderPlane;
            var rightBorder = plane.bounds.max.x - _offsetBorderPlane;
            return raycastHit.point.x > leftBorder && raycastHit.point.x < rightBorder ;
        }
        
        private void LaunchCube(bool canLaunch, Cube cube)
        {
            if (canLaunch)
            {
                _rb = cube.GetComponent<Rigidbody>();
                _rb.AddForce(transform.forward);
                CubeLaunch?.Invoke(cube);
            }
        }
        
        private bool CanLaunch()
        {
            return Input.GetMouseButtonDown(0);
        }

    }
}