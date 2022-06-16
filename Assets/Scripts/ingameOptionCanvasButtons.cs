using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingameOptionCanvasButtons : MonoBehaviour
{
    public GameObject pauseMenuHolder;
    public GameObject optionsMenuHolder;

    public GameObject audioCanvasRelated;
    public GameObject controlsCanvasRelated;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitButton()
    {

        pauseMenuHolder.SetActive(true);
        optionsMenuHolder.SetActive(false);

    }

    public void audioButton()
    {

        audioCanvasRelated.SetActive(true);
        controlsCanvasRelated.SetActive(false);
    }

    public void controlsButton()
    {
    
        controlsCanvasRelated.SetActive(true);
        audioCanvasRelated.SetActive(false);
    }
}
