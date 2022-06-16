using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cavernPuzzleWrongRoute : MonoBehaviour
{

    public GameObject startPoint;

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

            collision.transform.parent.position = startPoint.transform.position;
            // update later, character will fade out and the level will restart;
           // collision.GetComponentInParent<SpriteRenderer>().color = new Color(1f, 1f, 1f,0);

        }
    }
}
