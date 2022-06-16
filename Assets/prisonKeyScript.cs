using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prisonKeyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            prisonDoorScript.playerPickedKey = true;
            Destroy(this.gameObject);
        }
    }
}
