using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidHeraldStates : MonoBehaviour
{

    public voidBossStates voidBossState;

    public enum voidBossStates
    {
        Idle,
        CloneAttack

    }


    void Start()
    {

        voidBossState = voidBossStates.Idle;


    }

    // Update is called once per frame
    void Update()
    {

        stateChanger();

    }

    private void stateChanger()
    {
        
        //if the current state is idle
        if(voidBossState == voidBossStates.Idle)
        {

            voidBossState = voidBossStates.CloneAttack;

        }

        //Clone attack state
        if (voidBossState == voidBossStates.CloneAttack)
        {

            if(startedCloneAttackRoutine == false)
            {
                StartCoroutine(cloneAttackRoutine());
            }

        }



    }






    //Clones
    public List<GameObject> clonesList;

    //Points that the clone and boss can appear at
    public List<Transform> clonePoints;

    private bool startedCloneAttackRoutine;
    private IEnumerator cloneAttackRoutine()
    {

        startedCloneAttackRoutine = true;

        //assign random position to boss
        int assignedCloneNum = 0;
        float realBossPoint = Random.Range(0, 4);

        for(int i = 0; i < 4; i++)
        {

            //if the mainboss occupies that point, skip that point
            if(i == realBossPoint)
            {
                this.GetComponent<Animator>().SetBool("isCasting", true);


                //assign rotations based on where we are
                if (i == 1 || i == 3)
                {
                    this.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                if (i == 2 || i == 4)
                {
                    this.transform.eulerAngles = new Vector3(0, 180, 0);
                }


                this.transform.position = clonePoints[i].transform.position;
                continue;
            }

            Debug.Log("shoulda assigned");

            //assign rotations based on where we are
            if (i == 1 || i == 3)
            {
                clonesList[assignedCloneNum].transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if(i == 2 || i == 4)
            {
                clonesList[assignedCloneNum].transform.eulerAngles = new Vector3(0, 180, 0);
            }

            clonesList[assignedCloneNum].SetActive(true);
            clonesList[assignedCloneNum].transform.position = clonePoints[i].transform.position;

            assignedCloneNum += 1;

            
        }


        yield return null;


        
    }



}
