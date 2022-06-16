using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEndingCutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(startedEndingCutsceneRoutine == false)
        {
            StartCoroutine(endingCutsceneRoutine());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool startedEndingCutsceneRoutine;

    public float endingCutsceneTimer = 20f;
    public float endingCutsceneCounter;

    public GameObject gameNameText,gameDeveloperText,playTestersText,endingText;
    private bool firstCreditWasShown,secondCreditWasShown,thirdCreditWasShown,fourthCreditWasShown;

    private IEnumerator endingCutsceneRoutine()
    {
        startedEndingCutsceneRoutine = true;

        while (endingCutsceneCounter <= endingCutsceneTimer)
        {


            endingCutsceneCounter += Time.deltaTime;

            if(endingCutsceneCounter <= 6f && gameNameText.activeInHierarchy == false && firstCreditWasShown == false)
            {
                gameNameText.SetActive(true);
                firstCreditWasShown = true;
            }

            if (endingCutsceneCounter <= 12f && endingCutsceneCounter >= 6f && gameDeveloperText.activeInHierarchy == false && secondCreditWasShown == false)
            {
                gameNameText.SetActive(false);
                gameDeveloperText.SetActive(true);
                secondCreditWasShown = true;
            }

            if (endingCutsceneCounter <= 16f && endingCutsceneCounter > 12f && playTestersText.activeInHierarchy == false && thirdCreditWasShown == false)
            {
                gameDeveloperText.SetActive(false);
                playTestersText.SetActive(true);
                thirdCreditWasShown = true;
            }

            if (endingCutsceneCounter <= 20f && endingCutsceneCounter > 16f && endingText.activeInHierarchy == false && fourthCreditWasShown == false)
            {
                playTestersText.SetActive(false);
                endingText.SetActive(true);
                fourthCreditWasShown = true;
            }

            yield return null;
        }


        Application.Quit();


     

    }
}
