using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeControlText : MonoBehaviour
{

    public static bool startedChangeTextRoutine;

    // Start is called before the first frame update
    void Start()
    {

        GameObject.Find("moveLeftControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.moveLeftControl.ToString();
        GameObject.Find("moveRightControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.moveRightControl.ToString();
        GameObject.Find("moveJumpControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.moveJumpControl.ToString();
        GameObject.Find("specialOneControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.specialOneControl.ToString();
        GameObject.Find("attackControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.attackControl.ToString();
        GameObject.Find("dashControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.dashControl.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator changeAllControlText()
    {

        startedChangeTextRoutine = true;

        Debug.Log("started");

        GameObject.Find("moveLeftControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.moveLeftControl.ToString();
        GameObject.Find("moveRightControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.moveRightControl.ToString();
        GameObject.Find("moveJumpControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.moveJumpControl.ToString();
        GameObject.Find("specialOneControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.specialOneControl.ToString();
        GameObject.Find("attackControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.attackControl.ToString();
        GameObject.Find("dashControlText").GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.dashControl.ToString();




        startedChangeTextRoutine = false;

        


        yield return null;


    }
}
