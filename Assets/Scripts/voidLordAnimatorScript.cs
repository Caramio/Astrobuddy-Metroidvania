using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidLordAnimatorScript : MonoBehaviour
{
    public static bool finishedShieldDestroyCast;

    public static bool finishedKillSpirit;

    

    public GameObject snowSpirit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stopCastingAnim()
    {

        this.GetComponent<Animator>().SetBool("startedCasting", false);
        finishedShieldDestroyCast = true;

    }

    public void stopAttackAnim()
    {

        this.GetComponent<Animator>().SetBool("startedMeeleAttack", false);

        if(startedFadeRoutine == false)
        {
            StartCoroutine(fadeRoutine());
        }
        

    }

    //coroutine to make the snowspirit disappear
    private bool startedFadeRoutine;

    private float fadeTimer = 3f;
    private float fadeCounter;
    private IEnumerator fadeRoutine()
    {

        startedFadeRoutine = true;

        Material shaderMat = snowSpirit.GetComponent<SpriteRenderer>().material;

        while (fadeCounter <= fadeTimer)
        {

            shaderMat.SetFloat("_Fade", Mathf.Lerp(1f, 0f, fadeCounter / fadeTimer));

            fadeCounter += Time.deltaTime;

            yield return null;

        }

        finishedKillSpirit = true;
    }
}
