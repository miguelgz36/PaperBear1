using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWeapon : MonoBehaviour
{
    [SerializeField] private AudioClip soundFire;
    [Range(0, 1)]
    [SerializeField] private float volumeFire = 1f;

    public void PlaySoundFire()
    {
        AudioSource.PlayClipAtPoint(soundFire, transform.position, volumeFire);
    }
}
