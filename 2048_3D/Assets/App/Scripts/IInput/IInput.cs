using System;

namespace App.Scripts.IInput
{
    public interface IInput {
    
        event Action<ButtonName, ButtonState> ButtonStateChanged;
        void Update();
    }

    public enum ButtonName
    {
        Button
    }

    public enum ButtonState
    {
        OnDown
    }
}