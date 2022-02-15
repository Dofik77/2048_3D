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
        public event Action OnCubeStopped;
        
        private int _valueOfCube;

        private TMP_Text _textValueOfCube;

        private void Awake()
        {
            var objectTextValueOfCube = GameObject.Find("textValueOfCube");
            _textValueOfCube = objectTextValueOfCube.GetComponent<TMP_Text>();
        }

        public void Initilize(int valueOfCube)
        {
            _valueOfCube = valueOfCube;
            _textValueOfCube.text = _valueOfCube.ToString();
        }

        private void OnCollisionEnter(Collision other)
        {
            var strikeCube = other.gameObject.GetComponent<Cube>();
            
            if (TryGetComponent<Cube>(out strikeCube)) 
            {
                if (this._valueOfCube == strikeCube._valueOfCube)
                {
                    OnCubeCombine?.Invoke(this, strikeCube);
                }
                else OnCubeStopped?.Invoke();
            }

            if (other.gameObject.CompareTag("Border"))
            {
                OnCubeStopped?.Invoke();
            }
            
            //rewrite
            //check stopped by speed/etc
        }
        public void ChangeCubeValue(Cube cube)
        {
            cube._valueOfCube *= 2;
            _textValueOfCube.text = cube._valueOfCube.ToString();
        }

        public void ChangeCubeColor(Cube cube)
        {
            var cubeMaterial = cube.GetComponent<Material>();
            var color = new Color(Random.Range(10f, 10f), Random.Range(10f, 10f), Random.Range(10f, 10f));
            cubeMaterial.DOColor(color, 0.5f);
        }
    }
}