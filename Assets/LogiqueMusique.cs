using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogiqueMusique : MonoBehaviour
{
    public AudioClip Intro;
    public AudioClip Loop;
    private AudioSource[] Musique;
 

    // Start is called before the first frame update
    void Start()
    {

        //Aller chercher mes composantes 

        Musique = GetComponents<AudioSource>();

        //Jouer la musique au debut de la scene
        Musique[0].clip = Intro;
        Musique[0].Play();

        //Debuter la loop a la fin de l intro

        Musique[1].clip = Loop;
        Musique[1].loop = true;
        Musique[1].PlayDelayed(Musique[0].clip.length);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
