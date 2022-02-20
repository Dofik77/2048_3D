using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.CubeMechanics
{
    public class DictionaryOfColor : MonoBehaviour
    {
        private static Color _lastCubeColor => new Color(0.3f, 0.8f, 1f, 1f);
        
        public Dictionary<int, Color> colors = new Dictionary<int, Color>()
        {
            [2] = Color.gray,
            [4] = Color.blue,
            [8] = Color.magenta,
            [16] = Color.red,
            [32] = Color.yellow,
            [64] = Color.green,
            [128] = Color.black,
            [256] = Color.white,
            [512] = Color.cyan,
            [1024] = _lastCubeColor
        };
    } // SO - массив цветов ( в зависимости от степени все дела ) 
}
