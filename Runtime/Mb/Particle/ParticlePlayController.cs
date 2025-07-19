using System.Collections.Generic;
using UnityEngine;

namespace UnityGameLib.Mb.Particle
{
    public class ParticlePlayController : MonoBehaviour
    {
        Dictionary<string,ParticleSystem> particles;
        private void Awake()
        {
            ParticleSystem[] system = GetComponentsInChildren<ParticleSystem>();
            particles = new Dictionary<string, ParticleSystem>(system.Length);
            foreach (ParticleSystem particleSystem1 in system)
            {
                particles.Add(particleSystem1.name, particleSystem1);
            }
        }

        public void StartParticle(string particle)
        {
            if(particles.TryGetValue(particle, out ParticleSystem particleSystem)) 
                StartParticle(particleSystem);
        }
        public void StopParticle(string particle)
        {
            if(particles.TryGetValue(particle, out ParticleSystem particleSystem)) 
                StopParticle(particleSystem);
        }
        public void PauseParticle(string particle)
        {
            if(particles.TryGetValue(particle, out ParticleSystem particleSystem))
                PauseParticle(particleSystem);  
        }
        
        public void StartParticle(ParticleSystem particle)
        {
            particle?.Play(true);
        }
        public void StopParticle(ParticleSystem particle)
        {
            particle?.Stop(true);
        }
        public void PauseParticle(ParticleSystem particle)
        {
            particle?.Pause(true);  
        }
    }
}