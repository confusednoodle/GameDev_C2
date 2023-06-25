using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxHandler : MonoBehaviour
{
    [SerializeField] AudioSource HitSfx;
    [SerializeField] AudioSource SpecialSfx;

    public void PlayHitSfx()
    {
        HitSfx.Play();
    }

    public void PlaySpecialSfx()
    {
        SpecialSfx.Play();
    }
}
