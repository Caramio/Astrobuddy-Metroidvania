using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class entranceSceneSceneSwapHandler : MonoBehaviour
{

    private GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        var playerObjTry = GameObject.Find("Astrobuddy");

        if (playerObjTry != null)
        {

            playerObj = playerObjTry;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        SceneManager.LoadScene("Prison");

    }


    private IEnumerator cutsceneSwapRoutine()
    {


        SceneManager.LoadScene("Prison");

        yield return null;


    }
}
