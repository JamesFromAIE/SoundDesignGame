using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //references
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip music_world;
    [SerializeField] ReactionScreen RS;
    [SerializeField] ReflexTest RB;

    //volume vars
    float startVolume;
    [SerializeField] float fadeTime;
    bool inactiveAudio;


    void Start()
    {
        musicSource.clip = music_world;
        musicSource.Play();
        startVolume = musicSource.volume;
    }

    void Update()
    {
        if (RS.RSActive || RB.RBActive)
        {
            if (musicSource.volume > 0)
            {
                musicSource.volume -= startVolume * Time.deltaTime / fadeTime;
            }
            inactiveAudio = true;
        }
        else if (inactiveAudio)
        {
            if (musicSource.volume < startVolume)
            {
                musicSource.volume += startVolume * Time.deltaTime / fadeTime;
            }
            else
            {
                inactiveAudio = false;
            }
            
        }
    }
}
