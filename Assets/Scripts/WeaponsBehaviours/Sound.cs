using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] protected AudioClip soundFire;
    [Range(0, 1)]
    [SerializeField] protected float volumeFire = 1f;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (audioSource)
        {
            audioSource.transform.position = gameObject.transform.position;
        }
    }

    public void PlayAtPoint()
    {
        AudioSource.PlayClipAtPoint(soundFire, transform.position, volumeFire);
    }

    public void Play()
    {
        audioSource.Play();
    }
}
