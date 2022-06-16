using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingameChangeControlsButtons : MonoBehaviour
{
    public GameObject leftMoveText, rightMoveText, jumpText, specialOneText, attackText,dashText;

    //prompt to be enabled
    public GameObject questionPromptObject;

    //reference object
    changeControlText changeControlTextObj = new changeControlText();


    //List for key values


    //Coroutine bools
    public static bool startedAssignKeyRoutine;

    private bool keyInputWasGiven;


    void Start()
    {
        // assigning all controls to what they were set to at the start
        leftMoveText.GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.moveLeftControl.ToString();
        rightMoveText.GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.moveRightControl.ToString();
        jumpText.GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.moveJumpControl.ToString();
        attackText.GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.attackControl.ToString();
        dashText.GetComponent<TMPro.TextMeshProUGUI>().text = controlsStaticClass.dashControl.ToString();


    }


    void Update()
    {

     
    }
    // Left mapping button 
    public void clickLeftMappingButton()
    {

        if (startedAssignKeyRoutine == false)
        {
            StartCoroutine(assignKey(leftMoveText, controlsStaticClass.moveLeftControl));
        }


    }
    // Right mapping button
    public void clickRightMappingButton()
    {

        if (startedAssignKeyRoutine == false)
        {
            StartCoroutine(assignKey(rightMoveText, controlsStaticClass.moveRightControl));
        }

    }

    // Jump mapping button
    public void clickJumpMappingButton()
    {

        if (startedAssignKeyRoutine == false)
        {
            StartCoroutine(assignKey(jumpText, controlsStaticClass.moveJumpControl));
        }

    }

    //special One mapping button
    public void clickspecialOneMappingButton()
    {

        if (startedAssignKeyRoutine == false)
        {
            StartCoroutine(assignKey(specialOneText, controlsStaticClass.specialOneControl));
        }

    }

    public void clickAttackMappingButton()
    {

        if (startedAssignKeyRoutine == false)
        {
            StartCoroutine(assignKey(attackText, controlsStaticClass.attackControl));
        }

    }

    public void clickDashMappingButton()
    {

        if (startedAssignKeyRoutine == false)
        {
            StartCoroutine(assignKey(dashText, controlsStaticClass.dashControl));
        }

    }


    private IEnumerator assignKey(GameObject relatedText, KeyCode controlClassCode)
    {

        questionPromptObject.SetActive(true);

        startedAssignKeyRoutine = true;


        while (keyInputWasGiven == false)
        {


            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    
                    // if the key is escape, exit without doing any assigning
                    

                    // In case of another entry also being the same key as this
                    if (controlsStaticClass.moveJumpControl == kcode)
                    {
                        
                        KeyCode holder = controlClassCode;
                        controlClassCode = kcode;
                        controlsStaticClass.moveJumpControl = holder;
                    }
                    if (controlsStaticClass.moveLeftControl == kcode)
                    {
                        KeyCode holder = controlClassCode;
                        controlClassCode = kcode;
                        controlsStaticClass.moveLeftControl = holder;
                    }
                    if (controlsStaticClass.moveRightControl == kcode)
                    {
                        KeyCode holder = controlClassCode;
                        controlClassCode = kcode;
                        controlsStaticClass.moveRightControl = holder;
                    }
                    if (controlsStaticClass.specialOneControl == kcode)
                    {
                        KeyCode holder = controlClassCode;
                        controlClassCode = kcode;
                        controlsStaticClass.specialOneControl = holder;
                    }
                    if (controlsStaticClass.attackControl == kcode)
                    {
                        KeyCode holder = controlClassCode;
                        controlClassCode = kcode;
                        controlsStaticClass.attackControl = holder;
                    }
                    if (controlsStaticClass.dashControl == kcode)
                    {
                        KeyCode holder = controlClassCode;
                        controlClassCode = kcode;
                        controlsStaticClass.dashControl = holder;
                    }




                    //If no duplicates exist, do the rest...
                    // if the given input is not escape...
                    if (kcode != KeyCode.Escape)
                    {

                        if (relatedText == leftMoveText)
                        {


                            controlsStaticClass.moveLeftControl = kcode;

                            if (changeControlText.startedChangeTextRoutine == false)
                            {
                                StartCoroutine(changeControlTextObj.changeAllControlText());
                            }

                            keyInputWasGiven = true;
                        }

                        if (relatedText == rightMoveText)
                        {

                            controlsStaticClass.moveRightControl = kcode;

                            if (changeControlText.startedChangeTextRoutine == false)
                            {
                                StartCoroutine(changeControlTextObj.changeAllControlText());
                            }

                            keyInputWasGiven = true;
                        }

                        if (relatedText == jumpText)
                        {

                            controlsStaticClass.moveJumpControl = kcode;

                            if (changeControlText.startedChangeTextRoutine == false)
                            {
                                StartCoroutine(changeControlTextObj.changeAllControlText());
                            }

                            keyInputWasGiven = true;
                        }

                        if (relatedText == specialOneText)
                        {

                            controlsStaticClass.specialOneControl = kcode;

                            if (changeControlText.startedChangeTextRoutine == false)
                            {
                                StartCoroutine(changeControlTextObj.changeAllControlText());
                            }

                            keyInputWasGiven = true;
                        }

                        if (relatedText == attackText)
                        {

                            controlsStaticClass.attackControl = kcode;

                            if (changeControlText.startedChangeTextRoutine == false)
                            {
                                StartCoroutine(changeControlTextObj.changeAllControlText());
                            }

                            keyInputWasGiven = true;
                        }

                        if (relatedText == dashText)
                        {

                            controlsStaticClass.dashControl = kcode;

                            if (changeControlText.startedChangeTextRoutine == false)
                            {
                                StartCoroutine(changeControlTextObj.changeAllControlText());
                            }

                            keyInputWasGiven = true;
                        }
                    }




                }


            }
            yield return null;

        }

        questionPromptObject.SetActive(false);

        startedAssignKeyRoutine = false;

        keyInputWasGiven = false;

    }
}
