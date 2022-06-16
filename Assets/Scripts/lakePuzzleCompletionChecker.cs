using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lakePuzzleCompletionChecker : MonoBehaviour
{
    public GameObject wheelOne, wheelTwo, wheelThree, wheelFour;

    public GameObject openedDoor;
    private float openedDoorstartYPos;

    //static var to see if puzzle was solved once
    public static bool wheelPuzzleSolved;


    //Coroutine timers
    private float openWallTimer = 4f;
    private float openWallCounter;



    //Coroutine bools
    private bool startedOpenWallRoutine;

    //This audiosrc
    private AudioSource thisAudioSource;

    void Start()
    {
        thisAudioSource = this.GetComponent<AudioSource>();

        openedDoorstartYPos = openedDoor.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        // if all of the wheels are in the correct place

        if(wheelOne.GetComponent<turningWheelPuzzleScript>().correctPlaceAchieved == true && wheelTwo.GetComponent<turningWheelPuzzleScript>().correctPlaceAchieved == true 
            && wheelThree.GetComponent<turningWheelPuzzleScript>().correctPlaceAchieved == true && wheelFour.GetComponent<turningWheelPuzzleScript>().correctPlaceAchieved == true)
        {

            Debug.Log("All in correct place");

            if(startedOpenWallRoutine == false)
            {
                wheelPuzzleSolved = true;

                StartCoroutine(openClosedWall());
            }

        }



    }

    //opening door sound and thisaudiosource
    public AudioClip doorOpeningAudioClip;
    private IEnumerator openClosedWall()
    {
        //
        if (thisAudioSource.isPlaying == false)
        {
            thisAudioSource.clip = doorOpeningAudioClip;
            thisAudioSource.Play();
        }
        //
        startedOpenWallRoutine = true;

        while(openWallCounter <= openWallTimer)
        {


            openedDoor.transform.position = new Vector3(openedDoor.transform.position.x, Mathf.Lerp(openedDoorstartYPos, openedDoorstartYPos - 30f, openWallCounter / openWallTimer) , 0f);

            openWallCounter += Time.deltaTime;

            yield return null;

        }

        thisAudioSource.Stop();

        openWallCounter = 0f;

        

    }


}
