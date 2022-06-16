using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Audio;

public class soundEffectsVolumeSlider : MonoBehaviour
{
    public Slider sfxSlider;
    public TMPro.TextMeshProUGUI sliderText;

    public AudioMixerGroup soundEffectsVolumeGroup;



    void Start()
    {

        sfxSlider.value = audioStaticClass.soundEffectsVolume;


        //also adjust at the start...
        if (Mathf.Sign(sfxSlider.value) == 1)
        {
            sliderText.text = Mathf.Round(50 + (sfxSlider.value * 2.5f)).ToString();

        }
        if (Mathf.Sign(sfxSlider.value) == -1)
        {
            sliderText.text = Mathf.Round(50 - (Mathf.Abs(sfxSlider.value) * 2.5f)).ToString();

        }

    }

    // Update is called once per frame
    void Update()
    {


        Debug.Log(audioStaticClass.soundEffectsVolume);

        soundEffectsVolumeGroup.audioMixer.SetFloat("sfxVol", sfxSlider.value);

        

        //Also adjust the audiostaticclass value


        if (Mathf.Sign(sfxSlider.value) == 1)
        {
            sliderText.text = Mathf.Round(50 + (sfxSlider.value * 2.5f)).ToString();
            //
            audioStaticClass.soundEffectsVolume = sfxSlider.value;
        }
        if (Mathf.Sign(sfxSlider.value) == -1)
        {
            sliderText.text = Mathf.Round(50 - (Mathf.Abs(sfxSlider.value) * 2.5f)).ToString();
            //
            audioStaticClass.soundEffectsVolume = sfxSlider.value;
        }


    }
}
