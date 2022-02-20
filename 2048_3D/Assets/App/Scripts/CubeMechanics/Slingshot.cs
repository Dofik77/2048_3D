using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace App.Scripts.CubeMechanics
{
    public class Slingshot : MonoBehaviour
    {
        public event Action<Cube> CubeLaunched;
    
        [SerializeField] private Camera _camera;
        [SerializeField] private Collider _cubePlane;
        [SerializeField] private float _offsetBorderPlane;
        [SerializeField] private float _pushForce;
        
        private Cube _actualCube;
        private Coroutine _aimCoroutine;

        public void Attach(Cube cube)
        {
            _actualCube = cube;
            _actualCube.EnableKinematic();
        }
        
        public void StartAiming()
        {
            _aimCoroutine = StartCoroutine(AimingCoroutine());
        }
        
        public void StopAiming()
        {
            StopCoroutine(_aimCoroutine);
        }

        private IEnumerator AimingCoroutine()
        {
            while (true)
            {
                yield return null;

                MoveByMouse(_actualCube);
                
                if (CanLaunch())
                    Launch(_actualCube);
            }
        }

        private void MoveByMouse(Cube cube)
        {
            if (TryGetMousePointInWorldSpace(out Vector3 point))
            {
                var position = cube.transform.position;
                position = new Vector3(point.x, position.y, position.z);
                cube.transform.position = position;
            }
        }

        public bool TryGetMousePointInWorldSpace(out Vector3 point)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var hasHit = Physics.Raycast(ray, out var raycastHit);
            point = raycastHit.point;

            return hasHit && IsHitInPlaneBorders(raycastHit, _cubePlane);
        }
        

        private bool IsHitInPlaneBorders(RaycastHit raycastHit, Collider plane)
        {
            var leftBorder = plane.bounds.min.x + _offsetBorderPlane;
            var rightBorder = plane.bounds.max.x - _offsetBorderPlane;
            
            return raycastHit.point.x > leftBorder 
                && raycastHit.point.x < rightBorder;
        }
        
        private bool CanLaunch()
        {
            return Input.GetMouseButtonDown(0);
        }
        private void Launch(Cube cube)
        {
            _actualCube.DisableKinematic();
            cube.Push(transform.forward, _pushForce);

            CubeLaunched?.Invoke(cube);
        }
    }
}