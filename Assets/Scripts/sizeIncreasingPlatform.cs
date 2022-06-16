using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizeIncreasingPlatform : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "PlayerHitbox")
        {
            if (collision.transform.parent.localScale.x < 2f)
            {
                collision.transform.parent.localScale += new Vector3(1 * Time.deltaTime, 1 * Time.deltaTime, 1f);
            }

        }


        // pushable blocks...
        if(collision.GetComponent<pushableByAstro>() != null)
        {

            if(collision.transform.localScale.x < 40f)
            {

                collision.transform.localScale += new Vector3(3 * Time.deltaTime, 3 * Time.deltaTime, 1f);

            }

        }
    }
}
