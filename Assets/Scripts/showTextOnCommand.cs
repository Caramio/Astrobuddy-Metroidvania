using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class showTextOnCommand : MonoBehaviour
{
    // If this is true, other items can not be inspected.
    public static bool inspectTextCooldown = true;

    public TMPro.TextMeshProUGUI relatedText;

    //Public reference for the text to be written
    public string givenText;

    //Current form of the string
    private string currentString;

    //Delay between typing
    public float delay;

    //Timer
    private float textTimerCounter;
    public float maxTextTime;

    public GameObject textBorderDesignObj;
    void Start()
    {

      
    }

    // Once this gameObject is enabled, the coroutine will start
    private void OnEnable()
    {
        //if there should be a text border
        if (textBorderDesignObj != null)
        {
            textBorderDesignObj.SetActive(true);
        }

        inspectTextCooldown = false;
        StartCoroutine(slowText());
    }

    void Update()
    {
        // Timer to set the informationText to "", disable current script to be able to start again...
        textTimerCounter += Time.deltaTime;

        if(maxTextTime <= textTimerCounter)
        {
            textTimerCounter = 0;
            relatedText.text = "";

            inspectTextCooldown = true;

            if (textBorderDesignObj != null)
            {
                textBorderDesignObj.SetActive(false);
            }

            this.gameObject.GetComponent<showTextOnCommand>().enabled= false;
        }
        
    

    }

   

    IEnumerator slowText()
    {


        for (int i = 0; i <= givenText.Length; i++)
        {

            currentString = givenText.Substring(0, i);
            relatedText.text = currentString;

            yield return new WaitForSeconds(delay);

        }
    }

   

}
