using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using UnityEngine.Experimental.Rendering.Universal;

public class lightPuzzleCompletionChecker : MonoBehaviour
{

    public GameObject goblinKingDoor;
    public Sprite goblinKingOpnerDoor;

    public List<GameObject> lightList;

    public static bool goblinDoorOpened;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkCompletion();
    }


    private void checkCompletion()
    {

        if (lightList.All(light => light.GetComponent<lightPuzzleCorrectOrder>().correctColor == light.GetComponent<SpriteRenderer>().color))
        {

            Debug.Log("Puzzle is complete yoo");

            goblinKingDoor.GetComponent<SpriteRenderer>().sprite = goblinKingOpnerDoor;
            goblinDoorOpened = true;

        }
        

    }
}
