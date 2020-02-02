using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;

    void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();
    }
}
