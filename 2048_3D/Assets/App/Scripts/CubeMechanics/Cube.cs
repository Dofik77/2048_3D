using System;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;

namespace App.Scripts.CubeMechanics
{
    public class Cube : MonoBehaviour
    {
        public event Action<Cube, Cube> OnCubeCombine;
        public event Action<Cube> OnCubeStopedByObjects;
        public event Action<Cube> OnCubeStopedByCube;
        
        [SerializeField] private TMP_Text _textValueOfCube;
        [SerializeField] private Rigidbody _rb;

        private int _valueOfCube = 0;
        public Rigidbody Rb
        {
            get => _rb;
        }
        public int ValueOfCube
        {
            get => _valueOfCube;
            private set => _valueOfCube = value;
        }


        public void Initilize(int valueOfCube)
        {
            _valueOfCube = valueOfCube;

            TMP_Text objectTextValueOfCube = this.transform.GetComponentInChildren<TMP_Text>();
            _textValueOfCube = objectTextValueOfCube;
            
            _textValueOfCube.text = _valueOfCube.ToString();
        }

        private void OnCollisionEnter(Collision other)
        {
            var strikingCube = other.gameObject.GetComponent<Cube>();

            if (other.gameObject.CompareTag("Border") || other.gameObject.CompareTag("Cube"))
            {
                OnCubeStopedByObjects?.Invoke(this);
            }

            if (strikingCube != null)
            {
                if (this._valueOfCube == strikingCube._valueOfCube)
                {
                    OnCubeCombine?.Invoke(this, strikingCube);
                }
            }
            
            //may be stopped invoke sth another method? 
        }
        
        
        public void ChangeCubeValue(int value)
        {
            _valueOfCube = value * 2;
            _textValueOfCube.text = _valueOfCube.ToString();
        }
        
        public void ChangeCubeColor(Cube cube)
        {
            var cubeMaterial = cube.GetComponent<Material>();
            var color = new Color(Random.Range(10f, 10f), Random.Range(10f, 10f), Random.Range(10f, 10f));
            cubeMaterial.DOColor(color, 0.5f);
        }
    }
}