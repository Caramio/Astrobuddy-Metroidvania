using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingSnowAudioScript : MonoBehaviour
{

    public AudioClip rollingSnowClip;

    private AudioSource thisAudioSource;

    void Start()
    {
        thisAudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void playAudio()
    {

        if(thisAudioSource.isPlaying == false)
        {

        }

    }
}
