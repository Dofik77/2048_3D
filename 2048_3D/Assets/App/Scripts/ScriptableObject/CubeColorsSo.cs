using System;
using System.Collections.Generic;
using App.Scripts.CubeMechanics;
using UnityEngine;

namespace App.Scripts
{
    [CreateAssetMenu(menuName = "Color Value", fileName = "CubeColorSo")]
    public class CubeColorsSo : ScriptableObject
    {
        [SerializeField] private List<Color> _colors = new List<Color>();

        public Color GetColor(int value)
        {
            int degree = Convert.ToInt32(Math.Sqrt(value));
            return _colors[degree];
        }
    }
}