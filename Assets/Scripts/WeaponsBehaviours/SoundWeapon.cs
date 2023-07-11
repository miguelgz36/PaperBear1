using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWeapon : MonoBehaviour
{
    [SerializeField] protected AudioClip soundFire;
    [Range(0, 1)]
    [SerializeField] protected float volumeFire = 1f;

    public void PlaySoundFire()
    {
        AudioSource.PlayClipAtPoint(soundFire, transform.position, volumeFire);
    }
}
