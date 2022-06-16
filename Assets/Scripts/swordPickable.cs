using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordPickable : MonoBehaviour
{

    public GameObject swordTutorialObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject Eindicator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            Eindicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            Eindicator.SetActive(false);
        }
    }

   
}
