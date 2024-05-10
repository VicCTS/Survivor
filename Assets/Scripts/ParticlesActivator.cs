using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesActivator : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;

    public void ActivateParticles()
    {
        _particles.Play();
    }
}
