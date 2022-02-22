using System;
using DG.Tweening;
using UnityEngine;
using TMPro;

namespace App.Scripts.CubeMechanics
{
    public class Cube : MonoBehaviour
    {
        public event Action<Cube, Cube> CollideWithCube;

        [SerializeField] private TextMeshPro _textField;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _meshRenderer;

        [SerializeField] private float _targetScale;    
        [SerializeField] private float _durationExtension;
        
        public int Value { get; private set; }
        public Material ColorCube { get => _meshRenderer.material; set => _meshRenderer.material = value; }
        // make like ChangeValue or isn't?

        public void ChangeScale()
        {
            transform.DOScale( _targetScale, _durationExtension);
        }
        public void ChangeValue(int value)
        {
            Value = value;
            TextMeshPro[] textFields = GetComponentsInChildren<TextMeshPro>();
            foreach (var textField in textFields)
            {
                textField.text = Value.ToString();
            }
        }       

        public void Deactivate()
        {
            gameObject.SetActive(false);
            _rigidbody.velocity = Vector3.zero;
        }

        public void Torque(Vector3 direction, float torueForce)
        {
            _rigidbody.AddTorque(direction * torueForce, ForceMode.Impulse);
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