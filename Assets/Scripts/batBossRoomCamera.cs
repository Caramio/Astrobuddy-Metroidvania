using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batBossRoomCamera : MonoBehaviour
{
    private GameObject playerObj;

    public Camera bossCamera;
    public GameObject cameraHolder;

    public GameObject doorToClose;

    //Camera middle point
    public GameObject cameraMiddlePoint;


    public GameObject realBatBoss;

    //doors to open and close
    public GameObject entranceDoor;

    //cutscene related
    public GameObject cutsceneCameraMiddlePoint;

    public GameObject bossNameText;

    //Coroutine related
    private bool startedCutsceneRoutine;

    //Timers
    private float cutsceneTimer = 1f;
    private float cutsceneCounter;

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

    private IEnumerator batBossCutsceneRoutine()
    {

        playerObj.GetComponent<playerMovement>().canMove = false;
        playerObj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        startedCutsceneRoutine = true;

        //prevent exits
        entranceDoor.SetActive(true);

        bossCamera.GetComponent<cameraFollow>().enabled = false;
        cameraHolder.transform.position = cutsceneCameraMiddlePoint.transform.position;
        bossCamera.orthographicSize = 45f;

        

        while (cutsceneCounter <= cutsceneTimer)
        {

            bossNameText.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, Mathf.Lerp(0f, 1f, cutsceneCounter / cutsceneTimer));

            cutsceneCounter += Time.deltaTime;

            yield return null;
        }

        cutsceneCounter = 0f;

        // returning to the original game
        cameraHolder.transform.position = cameraMiddlePoint.transform.position;

        realBatBoss.SetActive(true);

        startedCutsceneRoutine = false;


        playerObj.GetComponent<playerMovement>().canMove = true;


        this.gameObject.SetActive(false);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            /*
            bossCamera.GetComponent<cameraFollow>().enabled = false;
            cameraHolder.transform.position = cameraMiddlePoint.transform.position;
            bossCamera.orthographicSize = 45f;
            */

            if(startedCutsceneRoutine == false)
            {
                StartCoroutine(batBossCutsceneRoutine());
            }
        }
    }
}
