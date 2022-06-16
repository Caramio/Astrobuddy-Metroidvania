using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playNote : MonoBehaviour
{

    public GameObject musicalOrderObject;

    private AudioSource thisAudioSource;

    public static int musicalOrderCounter = 0;
    void Start()
    {
        thisAudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "PlayerHitbox")
        {

            if (!thisAudioSource.isPlaying)
            {
                
                addToOrderCheckerList();
                thisAudioSource.Play();

                
            }
            Debug.Log("played sound");
        }

    }

    private void addToOrderCheckerList()
    {

        musicalOrderObject.GetComponent<musicalOrderChecker>().musicalOrderCheckerList.Add(this.gameObject.name);

        if(musicalOrderObject.GetComponent<musicalOrderChecker>().musicalOrderCheckerList[musicalOrderCounter] ==
            musicalOrderObject.GetComponent<musicalOrderChecker>().musicalPlatformCorrectOrderList[musicalOrderCounter].name)
        {

            musicalOrderCounter += 1;
            Debug.Log("good");
        }
        else
        {
            musicalOrderCounter = 0;
            Debug.Log("bad");
            musicalOrderObject.GetComponent<musicalOrderChecker>().musicalOrderCheckerList.Clear();
        }

        



    }
}
