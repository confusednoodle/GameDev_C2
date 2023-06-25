using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpecial : MonoBehaviour
{
    [SerializeField] ParticleSystem FireParticleSystem;
    [SerializeField] ParticleSystem ExplosionParticleSystem;

    public void StartFire()
    {
        FireParticleSystem.Play();
    }

    public void StopFire()
    {
        FireParticleSystem.Stop();
    }

    public void Explode()
    {
        ExplosionParticleSystem.Play();
    }
}
