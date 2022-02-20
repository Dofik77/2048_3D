using System;
using UnityEngine;
using TMPro;

namespace App.Scripts.CubeMechanics
{
    public class Cube : MonoBehaviour
    {
        public event Action<Cube, Cube> CollideWithCube;

        [SerializeField] private TextMeshPro _textField;
        [SerializeField] private Rigidbody _rigidbody;
        
        public int Value { get; private set; }
        
        public void ChangeValue(int value)
        {
            Value = value;
            _textField.text = Value.ToString();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            _rigidbody.velocity = Vector3.zero;
        }

        public void Push(Vector3 direction, float pushForce)
        {
            _rigidbody.AddForce(direction * pushForce, ForceMode.Impulse);
        }

        public void EnableKinematic()
        {
            _rigidbody.isKinematic = true;
        }
        
        public void DisableKinematic()
        {
            _rigidbody.isKinematic = false;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Cube cube))
            {
                if (Value == cube.Value)
                {
                    CollideWithCube?.Invoke(this, cube);
                }
            }
        }
    }
}