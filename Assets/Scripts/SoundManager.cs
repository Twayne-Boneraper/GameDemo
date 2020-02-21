using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audi;
    public AudioClip gameOverClip, jumpClip;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audi = GetComponent<AudioSource>();
    }


    public void PlayAudio(AudioClip clip)
    {
        audi.clip = clip;
        audi.Play();
    }

}
