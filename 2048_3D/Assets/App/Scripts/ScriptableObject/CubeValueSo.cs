using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.CubeMechanics
{
    [CreateAssetMenu(menuName = "Get Start Cube Value", fileName = "CubeValueSo")]
    public class CubeValueSo : ScriptableObject
    {
        [SerializeField] private List<int> _commonValue = new List<int>();

        public int GetValue()
        {
            var cubeValue = _commonValue[Random.Range(0, _commonValue.Count)];
            return cubeValue;
        }


    }
}