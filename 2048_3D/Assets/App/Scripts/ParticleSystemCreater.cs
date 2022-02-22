using App.Scripts.CubeMechanics;
using UnityEngine;

namespace App.Scripts
{
    public class ParticleSystemCreater : MonoBehaviour
    {
        public void CreateParticle(Cube cube, ParticleSystem particlePrefab)
        {
            ParticleSystem _particleInst =  Instantiate(particlePrefab, cube.transform.position, Quaternion.identity);
            var main = _particleInst.main;
            main.startColor = new ParticleSystem.MinMaxGradient(cube.ColorCube.color);
            
            Destroy(_particleInst.gameObject, 3f);
        }
    }
}
