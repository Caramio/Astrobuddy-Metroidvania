using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroPauseMenu : MonoBehaviour
{
    public GameObject pauseRelatedHolder;
    public GameObject optionsMenuRelated;

    

  
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // if escape is pressed , close or open the pause canvas
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            AudioListener.pause = true;



            if (pauseRelatedHolder.activeInHierarchy == false && optionsMenuRelated.activeInHierarchy == false)
            {
               
                Time.timeScale = 0;
                pauseRelatedHolder.SetActive(true);
            }

            else
            {
                // if assigning buttons are complete , we can close the screen
                if (ingameChangeControlsButtons.startedAssignKeyRoutine == false)
                {
                    Time.timeScale = 1;
                    pauseRelatedHolder.SetActive(false);

                    AudioListener.pause = false;

                    if (optionsMenuRelated.activeInHierarchy == true)
                    {
                        optionsMenuRelated.SetActive(false);
                    }
                }
            }

        }


    }
}
