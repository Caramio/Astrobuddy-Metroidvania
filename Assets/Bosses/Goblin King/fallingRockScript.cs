﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingRockScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Destroy(this, 5f);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {

            collision.GetComponentInParent<astroStats>().astroTakeDamage();
            Destroy(this.gameObject);

        }
    }
}