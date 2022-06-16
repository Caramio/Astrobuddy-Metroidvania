using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainRoomCutscene : MonoBehaviour
{

    private bool firstTextWasShown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startedShowTextRoutine == false && firstTextWasShown == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                relatedText.text = "";
                eIndicator.SetActive(false);
                textBackground.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (startedShowTextRoutine == false && firstTextWasShown == false)
        {
            StartCoroutine(showText("Alright, to show that you are mentally well, we need you to find the code to activate the time machine yourself. Good luck...", 0.01f));
            firstTextWasShown = true;
        }


    }


    public TMPro.TextMeshProUGUI relatedText;
    public GameObject textBackground;
    private bool startedShowTextRoutine;
    private string currentString = "";
    private int textNumberTracker = 0;
    public GameObject eIndicator;

    private IEnumerator showText(string givenString, float delay)
    {


        textBackground.SetActive(true);
        

        startedShowTextRoutine = true;

        while (currentString.Length < givenString.Length)
        {

            for (int i = 0; i <= givenString.Length; i++)
            {

                currentString = givenString.Substring(0, i);
                relatedText.text = currentString;

                yield return new WaitForSeconds(delay);

            }

        }



        eIndicator.SetActive(true);

        currentString = "";

        textNumberTracker += 1;

        startedShowTextRoutine = false;
    }
}
