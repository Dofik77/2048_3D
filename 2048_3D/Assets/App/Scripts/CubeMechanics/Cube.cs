using System;
using UnityEngine;

namespace App.Scripts.CubeMechanics
{
    public class Cube : MonoBehaviour
    {
        public event Action<Cube> CollisionCube;
        public float _speed;


        private Cube(float speed)
        {
            _speed = speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            
            CollisionCube?.Invoke(this);
        }
    }
}