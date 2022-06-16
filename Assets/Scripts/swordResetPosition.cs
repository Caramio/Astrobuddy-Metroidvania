using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordResetPosition : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // resetting the rotation once we equip the item again
    private void OnEnable()
    {

        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y,
                  70f);

    }

    void Update()
    {
        
    }
}
