using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidPortalScript : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openPortalFully()
    {

        this.GetComponent<Animator>().SetBool("portalHasOpened", true);

    }
}
