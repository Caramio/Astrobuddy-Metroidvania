using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Audio;

public class musicVolumeSlider : MonoBehaviour
{
    public Slider musicSlider;
    public TMPro.TextMeshProUGUI sliderText;

    public AudioMixerGroup musicVolumeGroup;

   

    void Start()
    {

        musicSlider.value = audioStaticClass.musicVolume;


        //also adjust at the start...
        if (Mathf.Sign(musicSlider.value) == 1)
        {
            sliderText.text = Mathf.Round(50 + (musicSlider.value * 2.5f)).ToString();
            
        }
        if (Mathf.Sign(musicSlider.value) == -1)
        {
            sliderText.text = Mathf.Round(50 - (Mathf.Abs(musicSlider.value) * 2.5f)).ToString();
            
        }

    }

    // Update is called once per frame
    void Update()
    {




        musicVolumeGroup.audioMixer.SetFloat("musicVol", musicSlider.value);
        
        //Also adjust the audiostaticclass value
        

        if (Mathf.Sign(musicSlider.value) == 1)
        {
            sliderText.text = Mathf.Round(50 + (musicSlider.value * 2.5f)).ToString();
            //
            audioStaticClass.musicVolume = musicSlider.value;
        }
        if (Mathf.Sign(musicSlider.value) == -1)
        {
            sliderText.text = Mathf.Round(50 - (Mathf.Abs(musicSlider.value) * 2.5f)).ToString();
            //
            audioStaticClass.musicVolume = musicSlider.value;
        }


    }
}
