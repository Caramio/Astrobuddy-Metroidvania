using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preLastBossScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera sceneCamera;
    public GameObject cameraHolder;

    public Transform bossCameraSpot;

    private GameObject playerObj;
    
    void Start()
    {
        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }


        if (startedShowBackgroundRoutine == false)
        {
            StartCoroutine(showBackground());
        }


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(showCounter);
    }


    public GameObject voidBackground;

    private float showTimer = 10f;
    private float showCounter;

    private float backgroundSetTimer = 4f;
    private float fadeTimer = 6f;

    private bool startedShowBackgroundRoutine;

    public GameObject finalBoss;
    public GameObject gameEndCutsceneObj;

    public GameObject fadeScreen;
    private IEnumerator showBackground()
    {
       


        
        startedShowBackgroundRoutine = true;

        voidBackground.SetActive(true);

        sceneCamera.orthographicSize = 65f;
        cameraHolder.transform.position = bossCameraSpot.transform.position;
        

        while (showCounter <= showTimer)
        {
            
            showCounter += Time.deltaTime;

            if(showCounter <= backgroundSetTimer)
            {
                voidBackground.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, showCounter / backgroundSetTimer));
            }

            if(showCounter <= 10f  && showCounter >= backgroundSetTimer)
            {
                if(fadeScreen.activeInHierarchy == false)
                {
                    fadeScreen.SetActive(true);
                }
                fadeScreen.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, (showCounter - backgroundSetTimer)  / fadeTimer));
            }




            yield return null;
        }


        //set the camera to inactive
        sceneCamera.GetComponent<cameraFollow>().enabled = false;
        

        cameraHolder.transform.position = bossCameraSpot.position;

        //reset player movement etc
        //playerObj.GetComponent<playerMovement>().enabled = true;
        //playerObj.GetComponent<astroAbilities>().enabled = true;
        //

        //activate the combat states for the final boss
        //finalBoss.GetComponent<voidHeraldStates>().enabled = true; 

        gameEndCutsceneObj.SetActive(true);
    }
}
