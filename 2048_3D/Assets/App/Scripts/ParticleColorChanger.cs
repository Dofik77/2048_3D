using UnityEngine;

namespace App.Scripts
{
    public class ParticleColorChanger : MonoBehaviour
    {
        private ParticleSystem.MainModule _settings;

        private void Start()
        {
            _settings = GetComponent<ParticleSystem>().main;
        }

        public void ChangeColor(Color cubeColor)
        {
            _settings.startColor = new ParticleSystem.MinMaxGradient(cubeColor);
        }
        
        //TODO
    }
}
