using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class uiCanvasMethods : MonoBehaviour
{

    public GameObject canvasHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name != "startScene")
        {
            canvasHolder.SetActive(true);
        }

    }
}
