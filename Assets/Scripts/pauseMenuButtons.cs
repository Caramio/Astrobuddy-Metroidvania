using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuButtons : MonoBehaviour
{

    private dataSavingObjScript saveObj = new dataSavingObjScript();

    public GameObject optionsHolder;
    public GameObject pauseMenuHolder;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quitButton()
    {
        Debug.Log("quit the app");

        saveObj.saveAndQuit();

     


    }

    public void resumeButton()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;
        this.gameObject.SetActive(false);


    }

    public void optionsButton()
    {

        optionsHolder.SetActive(true);

        pauseMenuHolder.SetActive(false);



    }
}
