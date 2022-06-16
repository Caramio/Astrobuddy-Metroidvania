using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Audio;

public class masterVolumeSlider : MonoBehaviour
{

    public Slider masterSlider;
    public TMPro.TextMeshProUGUI sliderText;

    public AudioMixerGroup masterVolumeGroup;

    private float volumeNum;


    void Start()
    {

        masterSlider.value = audioStaticClass.masterVolume;


        //Adjust the volume at the start aswell...
        if (Mathf.Sign(masterSlider.value) == 1)
        {
            sliderText.text = Mathf.Round(50 + (masterSlider.value * 2.5f)).ToString();
          
        }
        if (Mathf.Sign(masterSlider.value) == -1)
        {
            sliderText.text = Mathf.Round(50 - (Mathf.Abs(masterSlider.value) * 2.5f)).ToString();
          
        }
    }

    // Update is called once per frame
    void Update()
    {

        
         masterVolumeGroup.audioMixer.SetFloat("masterVol", masterSlider.value);
        /*
         if (Mathf.Sign(masterSlider.value) == 1)
         {
             sliderText.text = Mathf.Round(50 + (masterSlider.value * 2.5f)).ToString();
             //
             audioStaticClass.masterVolume = masterSlider.value;
         }
         if(Mathf.Sign(masterSlider.value) == -1)
         {
             sliderText.text = Mathf.Round(50 - (Mathf.Abs(masterSlider.value) * 2.5f)).ToString();
             //
             audioStaticClass.masterVolume = masterSlider.value;
         }
        */

        if(masterSlider.value >= -50)
        {

            sliderText.text = Mathf.Round(50f + (Mathf.Abs((masterSlider.value - -50f) * 1.66f))).ToString();
            audioStaticClass.masterVolume = masterSlider.value;
        }
        
        if (masterSlider.value < -50)
        {

            sliderText.text = Mathf.Round(50f - (Mathf.Abs((masterSlider.value - -50f) * 1.66f))).ToString();
            audioStaticClass.masterVolume = masterSlider.value;
        }
        


    }
}
