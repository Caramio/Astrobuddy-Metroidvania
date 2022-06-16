using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashTutorialEntry : MonoBehaviour
{

    public GameObject tutorialCanvasHolder;

    private GameObject playerObj;

    void Start()
    {
        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {


           if(startedDashTutorialRoutine == false)
            {
                StartCoroutine(dashTutorialRoutine());
            }

        }
    }


    private bool startedDashTutorialRoutine;

    private float tutTimer = 5f;
    private float tutCounter;
    private IEnumerator dashTutorialRoutine()
    {


        playerObj.GetComponent<playerMovement>().enabled = false;
        

        startedDashTutorialRoutine = true;

        tutorialCanvasHolder.SetActive(true);

        while(tutCounter <= tutTimer)
        {

            tutCounter += Time.deltaTime;

            yield return null;  

        }

        tutorialCanvasHolder.SetActive(false);

        playerObj.GetComponent<playerMovement>().enabled = true;
    }
}
