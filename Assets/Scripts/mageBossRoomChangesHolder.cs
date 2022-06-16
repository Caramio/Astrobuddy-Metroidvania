using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System.IO;

public class mageBossRoomChangesHolder : MonoBehaviour
{
    // Start is called before the first frame update



    public GameObject mageBossEntryHolder;

    void Start()
    {
        if (File.Exists(Application.dataPath + SceneManager.GetActiveScene().name + "mageBossRelated.txt"))
        {
            //Do load operations only if the file isnt empty
            if (new FileInfo(Application.dataPath + SceneManager.GetActiveScene().name + "mageBossRelated.txt").Length != 0)
            {

                string[] mageBossJSON = File.ReadAllLines(Application.dataPath + SceneManager.GetActiveScene().name + "mageBossRelated.txt");

                mageBossRoomSceneSwapHandler.mageBossInformation mageBossObj = JsonUtility.FromJson<mageBossRoomSceneSwapHandler.mageBossInformation>(mageBossJSON[0]);

                if (mageBossObj.greenGemWasPicked == true)
                {

                    mageBossEntryHolder.SetActive(false);

                }

            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
