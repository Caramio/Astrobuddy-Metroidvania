using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGliderInteraction : MonoBehaviour
{

    private AudioSource windsAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<AudioSource>() != null)
        {
            
            windsAudioSource = this.GetComponent<AudioSource>();

           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //audioclip
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            if(collision.GetComponentInParent<astroAbilities>().usingWings == true)
            {
                Debug.Log("fire is gliding");
                collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0f, 850f));

                if (windsAudioSource.isPlaying == false)
                {
                    windsAudioSource.Play();
                }

            }
            else
            {             
                windsAudioSource.Stop();
            }
        }
    }

   


}
