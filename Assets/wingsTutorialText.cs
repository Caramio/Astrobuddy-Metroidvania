using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wingsTutorialText : MonoBehaviour
{
    private GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }

        if (startedWingsTutorialRoutine == false)
        {
            StartCoroutine(wingsTutorial());
        }


    }

    // Update is called once per frame
    void Update()
    {

    }


    private bool startedWingsTutorialRoutine;
    public GameObject tutorialCanvasHolder;

    private float tutorialTimer = 5f;
    private float tutorialCounter;
    private IEnumerator wingsTutorial()
    {

        startedWingsTutorialRoutine = true;

        playerObj.GetComponent<playerMovement>().enabled = false;

        tutorialCanvasHolder.SetActive(true);

        while (tutorialCounter <= tutorialTimer)
        {



            tutorialCounter += Time.deltaTime;

            yield return null;

        }

        playerObj.GetComponent<playerMovement>().enabled = true;

        tutorialCanvasHolder.SetActive(false);
    }
}
