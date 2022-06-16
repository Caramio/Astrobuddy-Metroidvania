using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lakePuzzlePlatformScript : MonoBehaviour
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
        if (collision.tag == "PlayerHitbox")
        {
            if (lakePuzzleHandler.lakeSteppedPlatformName != this.transform.parent.parent.gameObject.name)
            {
                
                lakePuzzleHandler.lakeSteppedPlatformName = this.transform.parent.gameObject.name;
                lakePuzzleHandler.platformJumpCounter += 1;
            }
        }

    }
}
