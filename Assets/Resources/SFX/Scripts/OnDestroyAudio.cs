using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyAudio : MonoBehaviour
{
    public AudioSource soundToBePlayed;


    void OnDestroy()
    {
        if (soundToBePlayed != null)
        {
            soundToBePlayed.Play();
        }
    }
}
