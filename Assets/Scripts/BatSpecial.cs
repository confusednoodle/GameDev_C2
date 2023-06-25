using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpecial : MonoBehaviour
{
    [SerializeField] ParticleSystem ParticleSystem;

    public void StartParticles()
    {
        ParticleSystem.Play();
    }

    public void StopParticles()
    {
        ParticleSystem.Stop();
    }
}
