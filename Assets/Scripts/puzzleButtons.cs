using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class puzzleButtons : MonoBehaviour
{

    //Text that is to be changed
    public Text numberText;

    //Numbers references to be able to check numbers
    public Text entryOne, entryTwo, entryThree, entryFour;

    //Numbers that are supposed to be the solution
    public string numberOne, numberTwo, numberThree, numberFour;

    //GameObject to be changed once the puzzle is solved
    public GameObject interactedObject;
    //particles for when the portal is active...
    public GameObject portalOpenParticles;

    //This canvas
    public GameObject thisCanvas;

    //If the puzzle is solved or not for specific instances
    [HideInInspector]
    public bool puzzleSolved;

    //Check wheter or not the waterpuzzle was solved in the goblinWaterWorks scene
    public static bool waterPuzzleSolved;

    //audiosrc for this
    private AudioSource digitPuzzleAudioSource;

    void Start()
    {
        digitPuzzleAudioSource = this.GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }

    // Incrementing a slot number , only increments to digits and stops at 9
    public void incrementNumber()
    {

        numberText.text = (int.Parse(numberText.text) + 1).ToString();

        if(int.Parse(numberText.text) == 10)
        {

            numberText.text = "0";

        }


    }

    //Decrementing version
    public void decrementNumber()
    {

        numberText.text = (int.Parse(numberText.text) - 1).ToString();

        if (int.Parse(numberText.text) == -1)
        {

            numberText.text = "9";

        }


    }

    //If the input is the correct input and the accept button is clicked

    public AudioClip wrongChoiceSound;
    public AudioClip rightChoiceSound;
    public void enterInput()
    {

        if (entryOne.text == numberOne && entryTwo.text == numberTwo && entryThree.text == numberThree && entryFour.text == numberFour)
        {
            digitPuzzleAudioSource.clip = rightChoiceSound;
            digitPuzzleAudioSource.Play();


            // If the interactedObject is the start portal, check if the portalTurnOn script is enabled, if not enable it
            if (interactedObject.name.Contains("Portal")){

                if (interactedObject.GetComponent<portalTurnOn>().enabled == false)
                {
                    interactedObject.GetComponent<portalTurnOn>().enabled = true;
                    portalOpenParticles.SetActive(true);
                }

            }

            if(interactedObject.GetComponent<waterDrainHandler>() != null)
            {
                //use this for later
                waterPuzzleSolved = true;


                puzzleSolved = true;

                thisCanvas.SetActive(false);
                interactedObject.SetActive(true);
            }


            

        }
        else
        {
           
            digitPuzzleAudioSource.clip = wrongChoiceSound;
            digitPuzzleAudioSource.Play();

        }

    }
}
