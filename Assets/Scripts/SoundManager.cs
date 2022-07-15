using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> //SoundManager class controls Music/sfx
{
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip rotateSfx;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

   public void PlayRotationSfx()
   {
        audioSource.PlayOneShot(rotateSfx, 0.5f);
   }
}
