using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winterWeightPuzzleMassChecker : MonoBehaviour
{

    //public reference to the parent
    public GameObject weightPlatform;

    private bool startedgoDownRoutine;

    private bool rockInPlace;

    private float goDownTimer = 3f;
    private float goDownCounter;

    public static bool largeRockInPlace, mediumRockInPlace, smallRockInPlace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(rockInPlace == true && startedgoDownRoutine == false)
        {
            StartCoroutine(goDown());
        }


    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "puzzleFallingRockLarge" && this.gameObject.name == "largeWeightPlatform")
        {
            
            rockInPlace = true;
            largeRockInPlace = true;
        }

        if (collision.gameObject.name == "puzzleFallingRockMedium" && this.gameObject.name == "mediumWeightPlatform")
        {

            rockInPlace = true;
            mediumRockInPlace = true;

        }

        if (collision.gameObject.name == "puzzleFallingRockSmall" && this.gameObject.name == "smallWeightPlatform")
        {

            rockInPlace = true;
            smallRockInPlace = true;

        }



    }


    private IEnumerator goDown()
    {

        startedgoDownRoutine = true;

        while(goDownCounter <= goDownTimer)
        {

            weightPlatform.transform.localPosition = new Vector3(weightPlatform.transform.localPosition.x, -Mathf.Lerp(6.5f, 9f , goDownCounter / goDownTimer) , 0f);

            goDownCounter += Time.deltaTime;

            yield return null;

        }


        

    }
}
