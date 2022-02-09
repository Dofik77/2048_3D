using System;
using UnityEngine;

namespace App.Scripts.CubeMechanics
{
    public class Cube : MonoBehaviour
    {
        public event Action<Cube, Cube> CollideWithCube;
        public event Action<Cube> DeactiveCube;

        private float _speed;
        private Vector3 _vectorMove = Vector3.right;
        private int _valueOfCube;

        private bool _canLaunch = true;

        private void Update()
        {
            if (CanLaunch())
            {
                transform.Translate(_vectorMove * _speed * Time.deltaTime);
                _canLaunch = false;
            }
        }

        public void Initilize(float speed, int valueOfCube)
        {
            _speed = speed;
            _valueOfCube = valueOfCube;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var strikeCube = other.gameObject.GetComponent<Cube>();
            
            if(this._valueOfCube == strikeCube._valueOfCube)
                CollideWithCube?.Invoke(this, strikeCube);
        }
        
        public void ChangeCubeValue(Cube cube)
        {
            cube._valueOfCube *= 2;
            //менять цвет и задавать полёт 
        }

        private bool CanLaunch()
        {
            return _canLaunch && Input.GetMouseButtonDown(0);
        }

        private float ChangeCubePosition()
        {
            float pos;
            return pos =  Input.GetAxis("Horizontal"); 
        }
    }
}