using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turningWheelPuzzleScript : MonoBehaviour
{

    public GameObject Eindicator;

    public GameObject turnedWheel;

    private bool playerInRange;

    //Timers for routine
    private float wheelTurnCounter;
    private float wheelTurnTimer = 1.5f;

    // bool for routine
    private bool startedWheelTurnRoutine;


    //turned amount counter
    [HideInInspector]
    public int wheelTurnedAmount;

    // assign individually per wheel
    public int correctWheelTurnedAmount;

    public bool correctPlaceAchieved;

    //audio src
    private AudioSource wheelAudioSource;

    void Start()
    {
        wheelAudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (correctWheelTurnedAmount == wheelTurnedAmount)
        {
            correctPlaceAchieved = true;

        }
        else
        {
            correctPlaceAchieved = false;
        }

        
        if(playerInRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                if(startedWheelTurnRoutine == false)
                {
                    StartCoroutine(turnWheel());
                }
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {

            Eindicator.SetActive(true);

            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            Eindicator.SetActive(false);

            playerInRange = false;
        }
    }

    public AudioClip turningStoneClip;

    private IEnumerator turnWheel()
    {
        //audio related
        wheelAudioSource.clip = turningStoneClip;
        wheelAudioSource.Play();
        //---


        startedWheelTurnRoutine = true;

        float startZ = turnedWheel.transform.eulerAngles.z;

        while(wheelTurnCounter <= wheelTurnTimer)
        {

            turnedWheel.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(startZ, startZ + 45f, wheelTurnCounter / wheelTurnTimer));

            wheelTurnCounter += Time.deltaTime;
            yield return null;
        }

        //increase by one each time its turned
        wheelTurnedAmount += 1;

        if(wheelTurnedAmount == 8)
        {
            wheelTurnedAmount = 0;
        }

        wheelTurnCounter = 0f;
        startedWheelTurnRoutine = false;

    }
}
