using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicalOrderChecker : MonoBehaviour
{

    public List<GameObject> musicalPlatformCorrectOrderList;

    [HideInInspector]
    public List<string> musicalOrderCheckerList;

    public GameObject doorToOpen;

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
     
        if(musicalOrderCheckerList.Count == 8)
        {
            Debug.Log("Done");
            doorToOpen.SetActive(!doorToOpen.activeInHierarchy);
            this.GetComponent<musicalOrderChecker>().enabled = false;
        }


    }
}
