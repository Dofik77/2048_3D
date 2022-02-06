using System;
using UnityEngine;

namespace App.Scripts.IInput
{
    public class GeneralInput : IInput
    {
        public event Action<ButtonName, ButtonState> ButtonStateChanged;
        
        public void Update()
        {
            CheckButton();
        }

        private void CheckButton()
        {
            if (Input.GetMouseButtonDown(0))
                ButtonStateChanged?.Invoke(ButtonName.Button, ButtonState.OnDown);
        }
    }
}