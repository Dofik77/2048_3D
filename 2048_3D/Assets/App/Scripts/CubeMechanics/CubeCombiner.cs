using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.CubeMechanics
{
    public class CubeCombiner : MonoBehaviour
    {
        [SerializeField] private CubeGenerator _generator;
        [SerializeField] private float _forceOfPush;
        [SerializeField] private float _forceOfTorque;
        [SerializeField] private CubeColors colors;
        [SerializeField] private ParticleSystem _particlePrefab;
        [SerializeField] private ParticleSystemCreater _particleSystemCreater;
        
        public void Combine(Cube cube1, Cube cube2)
        {
            cube1.Deactivate();
            _generator.ReturnToPoolOnCubeCombined(cube1);

            cube2.ChangeValue(cube2.Value * 2);
            cube2.ColorCube = colors.GetColor(cube2.Value);
            InitializeParticle(cube2);
            ApplyForce(cube2);
        }

        private void InitializeParticle(Cube cube)
        {
            _particleSystemCreater.CreateParticle(cube, _particlePrefab);
        }

        private void ApplyForce(Cube strikingCube)
        {
            strikingCube.Push(Vector3.up, _forceOfPush);
            strikingCube.Torque(Random.rotation.eulerAngles, _forceOfTorque);
        }
    }
}