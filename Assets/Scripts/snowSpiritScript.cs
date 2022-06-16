using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class snowSpiritScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI relatedText;

    private float fadeRoutineTimer = 3.5f;
    private float fadeRoutineCounter;

    private bool startedFadeRoutine;


    private SpriteRenderer spiritSpriteRenderer;
    private Color startColor;

    void Start()
    {
        spiritSpriteRenderer = this.GetComponent<SpriteRenderer>();
        startColor = this.GetComponent<SpriteRenderer>().color;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {

            this.GetComponent<showTextOnCommand>().enabled = true;

            if(startedFadeRoutine == false)
            {
                StartCoroutine(fadeRoutine());
            }

        }
    }


    private IEnumerator fadeRoutine()
    {

        startedFadeRoutine = true;

        while(fadeRoutineCounter <= fadeRoutineTimer)
        {
            fadeRoutineCounter += Time.deltaTime;

            spiritSpriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(0.7f, 0f, fadeRoutineCounter / fadeRoutineTimer));

            yield return null;
        }

        fadeRoutineCounter = 0f;

        startedFadeRoutine = false;

        relatedText.text = "";

        this.gameObject.SetActive(false);

    }
}

