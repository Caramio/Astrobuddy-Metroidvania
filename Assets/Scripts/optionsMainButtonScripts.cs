using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class optionsMainButtonScripts : MonoBehaviour
{
    public GameObject controlsCanvas;
    public GameObject audioCanvas;

    public GameObject masterSlider,musicSlider;

    public GameObject optionsHolder;
    public GameObject startUIHolder;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void audioOptionsButton()
    {

        if (controlsCanvas.activeInHierarchy)
        {
            controlsCanvas.SetActive(false);
        }
        audioCanvas.SetActive(true);

    }

    public void controlsOptionsButton()
    {
        if (audioCanvas.activeInHierarchy)
        {
            audioCanvas.SetActive(false);
        }
        controlsCanvas.SetActive(true);


    }

    public void exitButton()
    {

        audioStaticClass.masterVolume = masterSlider.GetComponent<Slider>().value;
        audioStaticClass.musicVolume = musicSlider.GetComponent<Slider>().value;

        startUIHolder.SetActive(true);
        optionsHolder.SetActive(false);
        

        //SceneManager.LoadScene("startScene");

    }
}
