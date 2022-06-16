using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{

    //Public references
    public int enemyHP;


    //Private timers
    private float damageTakeCounter;
    private float damageTakeTimer = 0.2f;


    //first color
    private Color firstColor;



    void Start()
    {
        firstColor = this.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

        enemyDeath();

    }

    public void takeDamage()
    {

        enemyHP -= 1;
        this.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0f, 0f);

        StartCoroutine(damageColorChange());

    }

    private void enemyDeath()
    {

        if (enemyHP <= 0)
        {

            Destroy(this.gameObject);

        }

    }

    //Change color on damage
    private IEnumerator damageColorChange()
    {


        while (damageTakeCounter <= damageTakeTimer)
        {


            damageTakeCounter += Time.deltaTime;

            yield return null;
        }



        damageTakeCounter = 0f;

        this.GetComponent<SpriteRenderer>().color = firstColor;

    }
}
