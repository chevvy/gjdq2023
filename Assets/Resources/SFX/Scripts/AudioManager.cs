using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
    {
        public static AudioManager
            Instance = null; //Allows other scripts to call functions from SoundManager.                

        public float lowPitchRange = .95f; //The lowest a sound effect will be randomly pitched.
        public float highPitchRange = 1.05f; //The highest a sound effect will be randomly pitched.

        public bool isPlayingWalkSfx = false;
        void Awake()
        {
            //Check if there is already an instance of SoundManager
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
            DontDestroyOnLoad(gameObject);
        }

        private float GetRandomPitch()
        {
            return Random.Range(lowPitchRange, highPitchRange);
        }

        //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
        public void PlayRandomSfxWithRandomizedPitch (AudioClip[] clips, AudioSource audioSource)
        {
            //Generate a random number between 0 and the length of our array of clips passed in.
            int randomIndex = Random.Range(0, clips.Length);

            //Choose a random pitch to play back our clip at between our high and low pitch ranges.
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);

            //Set the pitch of the audio source to the randomly chosen pitch.
            audioSource.pitch = randomPitch;

            //Set the clip to the clip at our randomly chosen index.
            audioSource.clip = clips[randomIndex];

            //Play the clip.
            audioSource.Play();
        }

        public void PlayRandomSfx(AudioClip[] clips, AudioSource audioSource)
        {
            //Generate a random number between 0 and the length of our array of clips passed in.
            int randomIndex = Random.Range(0, clips.Length);
            audioSource.clip = clips[randomIndex];

            //Play the clip.
            audioSource.Play();
        }

        public void PlaySfxWithRandomizedPitch(AudioSource audioSource)
        {
            //Choose a random pitch to play back our clip at between our high and low pitch ranges.
            float randomPitch = GetRandomPitch();

            //Set the pitch of the audio source to the randomly chosen pitch.
            audioSource.pitch = randomPitch;

            //Play the clip.
            audioSource.Play();
        }

        public void StartPlayingWalkSfx(AudioClip[] clips, AudioSource audioSource)
        {
            audioSource.loop = true;
            PlayRandomSfxWithRandomizedPitch(clips, audioSource);
        }

        public void StopPlayingWalkSfx(AudioSource audioSource)
        {
            audioSource.Stop();
        }
    }
