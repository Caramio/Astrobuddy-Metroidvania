using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossRoomCamera : MonoBehaviour
{

    public GameObject bossLeftHand, bossRightHand, bossEye;

    public GameObject doorToClose;

    public float cameraSize;

    public Camera bossCamera;
    public GameObject cameraHolder;

    //Middle of the bossRoom
    public Transform cameraMiddleSpot;




    //Cutscene related references
    public GameObject cutsceneCameraSpot;






    //Coroutine related
    private bool startedCutsceneRoutine;

    //Timers
    private float cutsceneTimer = 10f;
    private float cutsceneCounter;


    void Start()
    {

    }


    void Update()
    {
        /*
        if (cameraFollow.fightingBoss)
        {
            fixCamera();
        }
        */
    }


    private void fixCamera()
    {

        bossCamera.orthographicSize = cameraSize;

        cameraHolder.transform.position = cameraMiddleSpot.position;
        bossCamera.transform.position = cameraHolder.transform.position;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            /*
            bossEye.SetActive(true);
            bossLeftHand.SetActive(true);
            bossRightHand.SetActive(true);

            doorToClose.SetActive(true);
            */

            if (startedCutsceneRoutine == false)
            {
                StartCoroutine(bossEntranceCutscene());
            }

            Debug.Log("BALALBALBLAB");
            cameraFollow.fightingBoss = true;

        }
    }


    private IEnumerator bossEntranceCutscene()
    {

        startedCutsceneRoutine = true;

        bossCamera.GetComponent<cameraFollow>().enabled = false;
        bossCamera.orthographicSize = 45f;

        cameraHolder.transform.position = cutsceneCameraSpot.transform.position;
        bossCamera.transform.position = cameraHolder.transform.position;

        while (cutsceneCounter <= cutsceneTimer)
        {


            cutsceneCounter += Time.deltaTime;

            yield return null;
        }

        bossCamera.GetComponent<cameraFollow>().enabled = true;


        cutsceneCounter = 0f;

        startedCutsceneRoutine = false;


    }
}
