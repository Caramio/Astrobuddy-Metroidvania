using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burnDestruct : MonoBehaviour
{
    //Private references
    private SpriteRenderer burnedObjectSprite;
    void Start()
    {

        burnedObjectSprite = this.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        burnObjectToDestroy();

    }


    private void burnObjectToDestroy()
    {
        burnedObjectSprite.color = new Color(1f, 0f, 0f);
        Destroy(this.gameObject, 1f);
    }
}
