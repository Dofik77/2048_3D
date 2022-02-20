using System;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace App.Scripts.CubeMechanics
{
    public class Cube : MonoBehaviour
    {
        public event Action<Cube, Cube> CubeCollided;
        public event Action<Cube> CubeStopped;

        [SerializeField] private TextMeshPro _textField;
        [SerializeField] private Rigidbody _rigidbody;

        private Rigidbody Rigidbody => _rigidbody;

        public int Value { get; private set; }
        
        public void ChangeValue(int value)
        {
            Value = value;
            _textField.text = Value.ToString();
        }
        
        public void ChangeCubeColor(Cube cube)
        {
            var cubeMaterial = cube.GetComponent<Material>();
            var color = new Color(Random.Range(10f, 10f), Random.Range(10f, 10f), Random.Range(10f, 10f));
            cubeMaterial.DOColor(color, 0.5f);
        }

        public void Deactive(Cube cube)
        {
            cube.gameObject.SetActive(false);
            cube.Rigidbody.velocity = Vector3.zero;
        }

        public void Jump(Cube cube)
        {
            cube.Rigidbody.AddForce(Vector3.up * 0.3f, ForceMode.Force);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Plane _))  
                return;

            if (other.gameObject.TryGetComponent(out Cube cube))
            {
                if (Value == cube.Value)
                {
                    CubeStopped?.Invoke(this);
                    CubeCollided?.Invoke(this, cube);
                    return;
                }
            }
            
            CubeStopped?.Invoke(this);//переписать на poling (проверка позиция активного куда > line ? spawn cube : false)
        }
    }
}