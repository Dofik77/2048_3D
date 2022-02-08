using System;
using UnityEngine;

namespace App.Scripts.CubeMechanics
{
    public class Cube : MonoBehaviour
    {
        public event Action<Cube, Cube> CollideWithCube;
        public event Action<Cube> DestroyCube;

        private float _speed;
        public int _valueOfCube;


        public void Initilize(float speed, int valueOfCube)
        {
            _speed = speed;
            _valueOfCube = valueOfCube;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //проверку на то что это куб
            CollideWithCube?.Invoke(this, other.gameObject.GetComponent<Cube>());
            //DestroyCube?.Invoke(this);
        }
        
        public void ChangeCubeValue(Cube cube)
        {
            cube._valueOfCube *= 2;
            //change color this cube
        }
    }
}