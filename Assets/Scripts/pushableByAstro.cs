using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableByAstro : MonoBehaviour
{
    private GameObject playerObj;

    private bool startedXvelDecreaseRoutine;

    private float decreaseVelTimer = 0.5f;
    private float decreaseVelCounter;
    private float startXvel;



    void Start()
    {
        playerObj = GameObject.Find("Astrobuddy");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localScale.x / playerObj.transform.localScale.x <= 22f)
        {
            this.GetComponent<Rigidbody2D>().mass = 70f;

        }

        if (this.transform.localScale.x / playerObj.transform.localScale.x > 22f)
        {
            this.GetComponent<Rigidbody2D>().mass = 5000f;


            if (startedXvelDecreaseRoutine == false)
            {
                StartCoroutine(decreaseVel());
            }
        }

    }




    private IEnumerator decreaseVel()
    {
        float startXvel = this.GetComponent<Rigidbody2D>().velocity.x;
        startedXvelDecreaseRoutine = true;

        while (decreaseVelCounter <= decreaseVelTimer)
        {
            decreaseVelCounter += Time.deltaTime;

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(startXvel, 0f, decreaseVelCounter / decreaseVelTimer), this.GetComponent<Rigidbody2D>().velocity.y);

            yield return null;
        }

        decreaseVelCounter = 0f;
        startedXvelDecreaseRoutine = false;
    }
}
