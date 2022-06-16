using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicBoxScript : MonoBehaviour
{
    
    public List<AudioClip> audioClipList;

    private AudioSource thisAudioSource;

   
    private int clipCounter = 0;
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
            

            StartCoroutine("playSounds");

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {

            
            StopCoroutine("playSounds");
            clipCounter = 0;

        }

    }


    private IEnumerator playSounds()
    {

        while (clipCounter < audioClipList.Count)
        {

            thisAudioSource.clip = audioClipList[clipCounter];
            thisAudioSource.Play();

            yield return new WaitForSeconds(thisAudioSource.clip.length);

            clipCounter += 1;


            thisAudioSource.clip = audioClipList[clipCounter];
            thisAudioSource.Play();

        }

        
       

        


    }

    
}
