using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class titleScreenButtons : MonoBehaviour
{


    public GameObject dataSavingObject;

    //options related
    public GameObject optionsRelatedHolder;


    void Start()
    {

       

    }

    
    void Update()
    {
        
    }

    public void optionsSceneButton()
    {

        //SceneManager.LoadScene("optionsScene");
        optionsRelatedHolder.SetActive(true);
        this.gameObject.SetActive(false);


    }

    public void playButton()
    {

        if (saveloadStaticClass.currentSceneName == null)
        {
            SceneManager.LoadScene("entranceScene");
        }
        else
        {
            SceneManager.LoadScene(saveloadStaticClass.currentSceneName);

      


        }
    }
}
