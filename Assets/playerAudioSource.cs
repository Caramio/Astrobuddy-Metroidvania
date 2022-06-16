using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudioSource : MonoBehaviour
{
    //audio sources
    public AudioSource sourceSwordSwing,sourceWingsSound,sourceFootstepGround,sourceDashSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSwordSound()
    {
        sourceSwordSwing.Play();
    }

    //wings----
    public void playWingsSound()
    {
        if (sourceWingsSound.isPlaying == false)
        {
            sourceWingsSound.Play();
        }
    }
    public void stopWingsSound()
    {
        sourceWingsSound.Stop();
    }
    //-------
    //------ foosteps
    public void playFootstepGroundSound()
    {
        if (sourceFootstepGround.isPlaying == false)
        {
            sourceFootstepGround.Play();
        }
    }
    public void stopFootstepGroundSound()
    {
        sourceFootstepGround.Stop();
    }

    //------
    /////---Dash sound
    public void playDashSound()
    {
        if(sourceDashSound.isPlaying == false)
        {
            sourceDashSound.Play();
        }
    }
    
    ///---
    ///---




}
