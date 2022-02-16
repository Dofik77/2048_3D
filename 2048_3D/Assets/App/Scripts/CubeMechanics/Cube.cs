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
        public event Action<Cube> OnCubeStopped;
        
        private int _valueOfCube;

        private TMP_Text _textValueOfCube;

        public void Initilize(int valueOfCube)
        {
            _valueOfCube = valueOfCube;

            TMP_Text objectTextValueOfCube = this.transform.GetComponentInChildren<TMP_Text>();
            _textValueOfCube = objectTextValueOfCube;
            
            _textValueOfCube.text = _valueOfCube.ToString();
        }

        private void OnCollisionEnter(Collision other)
        {

            if (other.gameObject.CompareTag("Border"))
            {
                OnCubeStopped?.Invoke(this);
            }

            if (other.gameObject.CompareTag("Cube"))
            {
                OnCubeStopped?.Invoke(this);
                
                
            }
            
            //may be stopped invoke sth another method? 
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