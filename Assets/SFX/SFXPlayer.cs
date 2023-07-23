using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource source;

    public void PlaySFX(AudioClip SFX)
    {
        source.PlayOneShot(SFX);
    }
}
