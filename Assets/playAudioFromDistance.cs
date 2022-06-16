using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudioFromDistance : MonoBehaviour
{
    private GameObject playerObj;

    private AudioSource thisAudioSource;

    public float circleDistance;

    private bool shouldPlayAudio;
    void Start()
    {

        if(this.GetComponent<AudioSource>() != null)
        {
            thisAudioSource = this.GetComponent<AudioSource>();
        }

        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {
            playerObj = playerObjTry;


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, circleDistance);

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private bool playerInRange;
   
    private void FixedUpdate()
    {
        if(Vector3.Distance(this.transform.position,playerObj.transform.position) < circleDistance)
        {
            if (shouldPlayAudio == false)
            {
                thisAudioSource.Play();
                shouldPlayAudio = true;
            }
        }
        else
        {
            shouldPlayAudio = false;
            thisAudioSource.Stop();
        }
    }
}
