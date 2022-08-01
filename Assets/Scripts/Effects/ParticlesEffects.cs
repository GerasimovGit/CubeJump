using System;
using UnityEngine;

namespace Effects
{
    public class ParticlesEffects : MonoBehaviour
    {
        [SerializeField] private ParticlesData[] _particles;

        public void Play(string id)
        {
            foreach (var particlesData in _particles)
            {
                if (particlesData.Id != id) continue;
                particlesData.Particle.Play();
                break;
            }
        }

        [Serializable]
        
        public class ParticlesData
        {
            [SerializeField] private string _id;
            [SerializeField] private ParticleSystem _particle;

            public string Id => _id;
            public ParticleSystem Particle => _particle;
        }
    }
}