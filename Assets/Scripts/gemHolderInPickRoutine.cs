using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemHolderInPickRoutine : MonoBehaviour
{

    public GameObject redGemPlaceHolder, greenGemPlaceHolder, blueGemPlaceHolder, purpleGemPlaceHolder;

    void Start()
    {
        
        if(astroBuddyStaticClass.astroPickedRedGem == true)
        {
            Debug.Log("set to traff");
            redGemPlaceHolder.SetActive(true);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(astroBuddyStaticClass.astroPickedRedGem);
    }

    
}
