using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowballDroppingLever : MonoBehaviour
{
    private bool playerInRange;

    public List<GameObject> snowPlatformList;

    public static int snowPlatformToOpenCounter = 0;

    private bool leverOnCooldown;


    //Coroutine bool
    private bool startedOpenCooldownRoutine;

    private float cooldownCounter;
    private float cooldownTimer = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {

            if (Input.GetKeyDown(KeyCode.E) && leverOnCooldown == false)
            {

                this.transform.Rotate(0, 180, 0);

                snowPlatformList[snowPlatformToOpenCounter].SetActive(false);

                StartCoroutine(cooldownRoutine());

                

            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {

            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox")
        {

            playerInRange = false;

        }

    }

    private IEnumerator cooldownRoutine()
    {
        snowPlatformToOpenCounter += 1;

        leverOnCooldown = true;

        startedOpenCooldownRoutine = true;

        while(cooldownCounter <= cooldownTimer)
        {
            cooldownCounter += Time.deltaTime;
            yield return null;
        }

        cooldownCounter = 0f;

        startedOpenCooldownRoutine = false;

        leverOnCooldown = false;
    }
}
