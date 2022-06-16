using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinKingBossCamera : MonoBehaviour
{
    //reference to playerobj
    private GameObject playerObj;

    //reference to cutscene versions
    public GameObject bossLeftHand, bossRightHand, bossEye;
    public GameObject bossNameText;

    //Reference to the real goblin eye to check for its health
    public GameObject realGoblinEye;

    public GameObject realGoblinBossHolder;

    public Sprite eyeHalfOpen, eyeOpen;

    public GameObject doorToClose;

    public float cameraSize;

    public Camera bossCamera;
    public GameObject cameraHolder;

    //Middle of the bossRoom for the cutscene
    public Transform cameraMiddleSpot;

    //middle of the bosss room for the boss
    public Transform cameraRealMiddleSpot;



    //Cutscene related references
    public GameObject cutsceneCameraSpot;






    //Coroutine related
    private bool startedCutsceneRoutine;

    private bool startedFadeInRoutine;

    //Timers
    private float cutsceneTimer = 1f;
    private float cutsceneCounter;

    private float fadeTimer = 5f;
    private float fadeCounter;


    void Start()
    {

        playerObj = GameObject.Find("Astrobuddy");

    }


    void Update()
    {
        
        
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            

            if (startedCutsceneRoutine == false)
            {
                StartCoroutine(bossEntranceCutscene());
            }


            cameraFollow.fightingBoss = true;

        }
    }

    private IEnumerator bossFadeIntoScene()
    {
        startedFadeInRoutine = true;

        //freeze player for now
        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        while(fadeCounter <= fadeTimer)
        {


            bossLeftHand.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, fadeCounter / fadeTimer));
            bossRightHand.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, fadeCounter / fadeTimer));
            bossEye.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, fadeCounter / fadeTimer));

            bossNameText.GetComponent<SpriteRenderer>().color = new Color(0.002091496f, 0.4433962f, 0.01430646f, Mathf.Lerp(0f, 1f, fadeCounter / fadeTimer));




            fadeCounter += Time.deltaTime;

            yield return null;

        }

        

        bossEye.GetComponent<SpriteRenderer>().sprite = eyeHalfOpen;
        
        fadeCounter = 0f;

    }

    //---------------------------------------------------------------------------------------------------------------------
    //---------------------------------STARTS CUTSCENE AND ENABLES THE BOSS FIGHT ---------------------------------------------
    //---------------------------------------------------------------------------------------------------------------------
    private IEnumerator bossEntranceCutscene()
    {
        //allow boss parts to slowly fade into the screen
        if (startedFadeInRoutine == false)
        {
            StartCoroutine(bossFadeIntoScene());
        }

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

        bossCamera.orthographicSize = 35f;

        cameraHolder.transform.position = cameraRealMiddleSpot.transform.position;
        bossCamera.transform.position = cameraHolder.transform.position;

        //open the constraints of the player
        playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;


        realGoblinBossHolder.SetActive(true);

        cutsceneCounter = 0f;


        startedFadeInRoutine = false;
        startedCutsceneRoutine = false;

        this.GetComponent<Collider2D>().enabled = false;

        doorToClose.SetActive(true);

    }
}
