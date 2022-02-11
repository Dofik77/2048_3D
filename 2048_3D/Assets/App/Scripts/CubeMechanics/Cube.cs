using System;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace App.Scripts.CubeMechanics
{
    public class Cube : MonoBehaviour
    {
        public event Action<Cube, Cube> CombineCube;
        public event Action<Cube> DeactiveCube;

        private float _speed;
        private Vector3 _vectorMove = Vector3.right;
        private int _valueOfCube;

        private bool _canLaunch = true;
        private bool _canMove = true;

        private TMP_Text _textValueOfCube;

        private void Awake()
        {
            var objectTextValueOfCube = GameObject.Find("textValueOfCube");
            _textValueOfCube = objectTextValueOfCube.GetComponent<TMP_Text>();
        }

        private void Update()
        {
            LaunchCube(CanLaunch());
        }

        public void Initilize(float speed, int valueOfCube)
        {
            _speed = speed;
            _valueOfCube = valueOfCube;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var strikeCube = other.gameObject.GetComponent<Cube>();

            if (strikeCube != null)
            {
                if(this._valueOfCube == strikeCube._valueOfCube)
                    CombineCube?.Invoke(this, strikeCube);
            }
        }

        public void ChangeCubeValue(Cube cube)
        {
            cube._valueOfCube *= 2;
            _textValueOfCube.text = cube._valueOfCube.ToString();
        }

        public void ChangeCubeColor(Cube cube)
        {
            var cubeMaterial = cube.GetComponent<Material>();
            cubeMaterial.DOColor(Color.blue, 0.5f);
        }
        
        private bool CanLaunch()
        {
            return _canLaunch && Input.GetMouseButtonDown(0);
        }

        private void LaunchCube(bool canLaunch)
        {
            if (canLaunch)
            {
                transform.Translate(_vectorMove * _speed * Time.deltaTime);
                _canLaunch = false;
            }
        }
    }
}