using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prisonDoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(playerInRange == true && playerPickedKey == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.gameObject.SetActive(false);
            }
        }


    }
    //player picking key will be adjusted from the key item itself
    public static bool playerPickedKey;
    private bool playerInRange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
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
}
