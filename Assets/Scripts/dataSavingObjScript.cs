using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEngine.Audio;

public class dataSavingObjScript : MonoBehaviour
{

    public GameObject playerObj;

    //References to astrobuddy parts for the first time we hit play...
    public GameObject playerSwordButton;
    public GameObject redGemInventorySlot;


    //Audio Mixer for the game
    public AudioMixerGroup musicVolumeGroup;
    public AudioMixerGroup masterVolumeGroup;
    public AudioMixerGroup soundEffectsVolumeGroup;



    //Check if boss was dead or not, once the boss is dead it should change this var
    public static bool batBossWasDead;


    //Classes that hold the values for the given variables
    [Serializable]
    public class astroBuddyStatsInformation
    {

        public bool astroHasWings;
        public bool astroHasSword;
        public bool astroHasDash;

        public bool astroPickedRedGem;

    }

    [Serializable]
    public class controlKeysInformation
    {


        public KeyCode leftKey;
        public KeyCode rightKey;
        public KeyCode jumpKey;
        public KeyCode attackKey;
        public KeyCode specialOneKey;
        public KeyCode dashKey;

    }

    [Serializable]
    public class audioInformation
    {

        public float masterVolume;
        public float musicVolume;
        public float soundEffectsVolume;


    }

    


    // Start is called before the first frame update
    void Start()
    {  
        
        // NEED TO CHANGE LATER, swapping between options and titlescreen will create copies of this...

        // keep this object throughout all scenes       
        DontDestroyOnLoad(this.gameObject);

        //Setting audio related things to their default values if the file was never created
        musicVolumeGroup.audioMixer.SetFloat("musicVol", audioStaticClass.musicVolume);
        masterVolumeGroup.audioMixer.SetFloat("masterVol", audioStaticClass.masterVolume);
        soundEffectsVolumeGroup.audioMixer.SetFloat("soundEffectsVol", audioStaticClass.masterVolume);



        //Adjusting astrobuddy related stats and items etc for the first time...
        if (File.Exists(Application.dataPath + "astroBuddyStats.txt"))
        {

            string[] astroJSON = File.ReadAllLines(Application.dataPath + "astroBuddyStats.txt");

            astroBuddyStatsInformation astroStatObj = JsonUtility.FromJson<astroBuddyStatsInformation>(astroJSON[0]);

            astroBuddyStaticClass.astroHasSword = astroStatObj.astroHasSword;
            astroBuddyStaticClass.astroHasWings = astroStatObj.astroHasWings;
            astroBuddyStaticClass.astroPickedRedGem = astroStatObj.astroPickedRedGem;
            astroBuddyStaticClass.astroHasDash = astroStatObj.astroHasDash;

            if (astroStatObj.astroHasSword == true)
            {           
                playerSwordButton.SetActive(true);
            }

            // kind of redundant, but works for wings...
            if(astroStatObj.astroHasWings == true)
            {
                astroBuddyStaticClass.astroHasWings = true;
            }
            if(astroStatObj.astroPickedRedGem == true)
            {
                redGemInventorySlot.SetActive(true);
                astroBuddyStaticClass.astroPickedRedGem = true;
            }
        }


        //---------------------------------------------------------------------------------------------//
        //----------------------------- RELATED TO CONTROLS LOADING -----------------------------------//

        if (File.Exists(Application.dataPath + "keyControls.txt"))
        {

            string[] keysJSON = File.ReadAllLines(Application.dataPath + "keyControls.txt");

            controlKeysInformation controlsObj = JsonUtility.FromJson<controlKeysInformation>(keysJSON[0]);

            controlsStaticClass.moveLeftControl = controlsObj.leftKey;
            controlsStaticClass.moveRightControl = controlsObj.rightKey;
            controlsStaticClass.moveJumpControl = controlsObj.jumpKey;
            controlsStaticClass.attackControl = controlsObj.attackKey;
            controlsStaticClass.specialOneControl = controlsObj.specialOneKey;
            controlsStaticClass.dashControl = controlsObj.dashKey;


        }

        //---------------------------------------------------------------------------------------------//
        //----------------------------- RELATED TO AUDIO LOADING -----------------------------------//

        if (File.Exists(Application.dataPath + "audioInformation.txt"))
        {

            string[] audioJSON = File.ReadAllLines(Application.dataPath + "audioInformation.txt");

            audioInformation audioObj = JsonUtility.FromJson<audioInformation>(audioJSON[0]);

            // set variables in the audiostaticclass
            audioStaticClass.masterVolume = audioObj.masterVolume;
            audioStaticClass.musicVolume = audioObj.musicVolume;
            audioStaticClass.soundEffectsVolume = audioObj.soundEffectsVolume;

            //Also set the games volume once we open the game for the first time.

            musicVolumeGroup.audioMixer.SetFloat("musicVol", audioStaticClass.musicVolume);
            masterVolumeGroup.audioMixer.SetFloat("masterVol", audioStaticClass.masterVolume);
            soundEffectsVolumeGroup.audioMixer.SetFloat("soundEffectsVol", audioStaticClass.masterVolume);


        }


        //first time we open , assign the saveloadstaticclass variable...
        if (File.Exists(Application.dataPath + "lastVisitedScene.txt"))
        {
            string[] sceneName = File.ReadAllLines(Application.dataPath + "lastVisitedScene.txt");

            string currentSceneName = sceneName[0];


            saveloadStaticClass.currentSceneName = currentSceneName;

        }

    }

    //Save needed data for the given scene
    public void saveData()
    {
        //If we are not on the title screen or the options scene, we can save data
        if (SceneManager.GetActiveScene().name != "startScene" && SceneManager.GetActiveScene().name != "optionsScene")
        {
            
            GameObject.Find("Astrobuddy").SetActive(true);

            //if the checkpoint is null(basically vector3.zero, so dont put checkpoints to 0 0 0), we will set it to the default position.
            //There will be "loadStartPoint" objects in each scene that will be the current checkpoint that the player entered.
            if(astroStats.deathStartPoint == Vector3.zero)
            {               
                GameObject.Find("Astrobuddy").transform.position = GameObject.Find("loadStartPoint").transform.position;
            }
            
            
            

            Debug.Log("was loaded");
            saveloadStaticClass.currentSceneName = SceneManager.GetActiveScene().name;



            //writing to a text file to save SCENE INFORMATION data for later aswell
            if (File.Exists(Application.dataPath + "lastVisitedScene.txt") == false)
            {
                Debug.Log("happened");
                File.Create(Application.dataPath + "lastVisitedScene.txt").Dispose();
            }

            //This will keep track of the level that the player last visited and save it for later use
            File.WriteAllText(Application.dataPath + "lastVisitedScene.txt", saveloadStaticClass.currentSceneName);



            //-----------------------------------------------------------------------------------------//
            //--------------------------PLAYER INFORMATION RELATED SAVING---------------------------//

            //writing to a text file to save PLAYER INFORMATION between scenes
            if (File.Exists(Application.dataPath + "astroBuddyStats.txt") == false)
            {
                File.Create(Application.dataPath + "astroBuddyStats.txt").Dispose();
            }

            //Information about the stats and items that the player has, these will be "false" initially, as the player progresses these will be changed
            astroBuddyStatsInformation astroStatsInf = new astroBuddyStatsInformation();

            astroStatsInf.astroHasSword = astroBuddyStaticClass.astroHasSword;
            astroStatsInf.astroHasWings = astroBuddyStaticClass.astroHasWings;
            astroStatsInf.astroPickedRedGem = astroBuddyStaticClass.astroPickedRedGem;
            astroStatsInf.astroHasDash = astroBuddyStaticClass.astroHasDash;

            string astroStatJSON = JsonUtility.ToJson(astroStatsInf);


            //write to file
            File.WriteAllText(Application.dataPath + "astroBuddyStats.txt", astroStatJSON);

            //-----------------------------------------------------------------------------------------//
            //--------------------------PLAYER CONTROLS INFORMATION RELATED---------------------------//
            if (File.Exists(Application.dataPath + "keyControls.txt") == false)
            {
                File.Create(Application.dataPath + "keyControls.txt").Dispose();
            }

            //Controls for the player, these can be changed through the menu, these will be saved and kept in a txt file for later use
            controlKeysInformation controlKeys = new controlKeysInformation();

            controlKeys.leftKey = controlsStaticClass.moveLeftControl;
            controlKeys.rightKey = controlsStaticClass.moveRightControl;
            controlKeys.jumpKey = controlsStaticClass.moveJumpControl;
            controlKeys.attackKey = controlsStaticClass.attackControl;
            controlKeys.specialOneKey = controlsStaticClass.specialOneControl;
            controlKeys.dashKey = controlsStaticClass.dashControl;

            string controlsJSON = JsonUtility.ToJson(controlKeys);

            //write to file
            File.WriteAllText(Application.dataPath + "keyControls.txt", controlsJSON);


            //-----------------------------------------------------------------------------------------//
            //--------------------------AUDIO INFORMATION RELATED---------------------------//
            if (File.Exists(Application.dataPath + "audioInformation.txt") == false)
            {
                File.Create(Application.dataPath + "audioInformation.txt").Dispose();
            }

            //audio levels for the game, these can be changed from the options menu and will be kept for later use
            audioInformation audioHolder = new audioInformation();

            audioHolder.masterVolume = audioStaticClass.masterVolume;
            audioHolder.musicVolume = audioStaticClass.musicVolume;
            audioHolder.soundEffectsVolume = audioStaticClass.soundEffectsVolume;

            string audioJSON = JsonUtility.ToJson(audioHolder);

            //write to file
            File.WriteAllText(Application.dataPath + "audioInformation.txt", audioJSON);

           

            //-----------------------------------------------------------------------------------------//





           

            

        }

    }
    //Once a level is loaded, we will call the "saveData" method, which will save every stat,audio,controls etc to their new values
    private void OnLevelWasLoaded(int level)
    {

        saveData();

    }
    



    //Method to access from the quit button, same as the topside method with only some differences, application.quit() weill be called here

    //This method will act like the saveData method but in the end, application.quit will be called to close the game
    //In hindsight, this could have easily been done without copy pasting most of the code and just adding an "if" condition
    //on application.quit on the initial saveData method
    public void saveAndQuit()
    {
        if (SceneManager.GetActiveScene().name != "startScene" && SceneManager.GetActiveScene().name != "optionsScene")
        {
            GameObject.Find("Astrobuddy").SetActive(true);
            GameObject.Find("Astrobuddy").transform.position = GameObject.Find("loadStartPoint").transform.position;

            Debug.Log("was loaded");
            saveloadStaticClass.currentSceneName = SceneManager.GetActiveScene().name;



            //writing to a text file to save SCENE INFORMATION data for later aswell
            if (File.Exists(Application.dataPath + "lastVisitedScene.txt") == false)
            {
                Debug.Log("happened");
                File.Create(Application.dataPath + "lastVisitedScene.txt").Dispose();
            }

            File.WriteAllText(Application.dataPath + "lastVisitedScene.txt", saveloadStaticClass.currentSceneName);


            //-----------------------------------------------------------------------------------------//

            //--------------------------PLAYER INFORMATION RELATED SAVING---------------------------//

            //writing to a text file to save PLAYER INFORMATION between scenes
            if (File.Exists(Application.dataPath + "astroBuddyStats.txt") == false)
            {
                File.Create(Application.dataPath + "astroBuddyStats.txt").Dispose();
            }

            astroBuddyStatsInformation astroStatsInf = new astroBuddyStatsInformation();

            astroStatsInf.astroHasSword = astroBuddyStaticClass.astroHasSword;
            astroStatsInf.astroHasWings = astroBuddyStaticClass.astroHasWings;
            astroStatsInf.astroPickedRedGem = astroBuddyStaticClass.astroPickedRedGem;
            astroStatsInf.astroHasDash = astroBuddyStaticClass.astroHasDash;

            string astroStatJSON = JsonUtility.ToJson(astroStatsInf);


            //write to file
            File.WriteAllText(Application.dataPath + "astroBuddyStats.txt", astroStatJSON);

            //-----------------------------------------------------------------------------------------//

            //--------------------------PLAYER CONTROLS INFORMATION RELATED---------------------------//
            if (File.Exists(Application.dataPath + "keyControls.txt") == false)
            {
                File.Create(Application.dataPath + "keyControls.txt").Dispose();
            }

            controlKeysInformation controlKeys = new controlKeysInformation();

            controlKeys.leftKey = controlsStaticClass.moveLeftControl;
            controlKeys.rightKey = controlsStaticClass.moveRightControl;
            controlKeys.jumpKey = controlsStaticClass.moveJumpControl;
            controlKeys.attackKey = controlsStaticClass.attackControl;
            controlKeys.specialOneKey = controlsStaticClass.specialOneControl;
            controlKeys.dashKey = controlsStaticClass.dashControl;


            string controlsJSON = JsonUtility.ToJson(controlKeys);

            //write to file
            File.WriteAllText(Application.dataPath + "keyControls.txt", controlsJSON);


            //-----------------------------------------------------------------------------------------//
            //--------------------------AUDIO INFORMATION RELATED---------------------------//
            if (File.Exists(Application.dataPath + "audioInformation.txt") == false)
            {
                File.Create(Application.dataPath + "audioInformation.txt").Dispose();
            }

            audioInformation audioHolder = new audioInformation();

            audioHolder.masterVolume = audioStaticClass.masterVolume;
            audioHolder.musicVolume = audioStaticClass.musicVolume;
            audioHolder.soundEffectsVolume = audioStaticClass.soundEffectsVolume;

         

            string audioJSON = JsonUtility.ToJson(audioHolder);

            //write to file
            File.WriteAllText(Application.dataPath + "audioInformation.txt", audioJSON);

           

          

            //-----------------------------------------------------------------------------------------//





            //----------------------------------------------------------------------------//
            //--------------------------SPECIFIC BOSSES RELATED---------------------------//
            //--------------------------BAT BOSS RELATED----------------------------------//
            if (File.Exists(Application.dataPath + "batBossRoom" + "batBossRoomRelated.txt") == false)
            {             
                File.Create(Application.dataPath + "batBossRoom" + "batBossRoomRelated.txt").Dispose();

                // save to the file only once if the file was not created ever
                batBossRoomSwapHandler.batBossInformation batBossInf = new batBossRoomSwapHandler.batBossInformation();

                batBossInf.bossWasKilled = batBossWasDead;

                string batBossJSON = JsonUtility.ToJson(batBossInf);

                File.WriteAllText(Application.dataPath + "batBossRoom" + "batBossRoomRelated.txt", batBossJSON);
            }

            //----------------------------------------------------------------------------//
            //--------------------------GOBLIN KING BOSS RELATED----------------------------------//
            if (File.Exists(Application.dataPath + "goblinKingBoss" + "bossEntryRelated.txt") == false)
            {
                File.Create(Application.dataPath + "goblinKingBoss" + "bossEntryRelated.txt").Dispose();

                // save to the file only once if the file was not created ever
                goblinKingSceneSwapHandler.entrySceneInformation goblinBossInf = new goblinKingSceneSwapHandler.entrySceneInformation();

                //if the boss is dead, we will set the entranceactive state to false
                goblinBossInf.entranceSceneActiveState = !goblinBossEyeStates.goblinKingDeathState;

                string goblinBossJSON = JsonUtility.ToJson(goblinBossInf);

                File.WriteAllText(Application.dataPath + "goblinKingBoss" + "bossEntryRelated.txt", goblinBossJSON);
            }









            //----------------------------------------------------------------------------------------//
            //--------------------------OUTSIDE FIRST PUZZLE RELATED----------------------------------//
            if (File.Exists(Application.dataPath + "outsideFirst" + "snowPuzzleList.txt") == false)
            {
                File.Create(Application.dataPath + "outsideFirst" + "snowPuzzleList.txt").Dispose();

                // save to the file only once if the file was not created ever
                outsideFirstSceneSwapHandler.snowPuzzle snowPuzzleInf = new outsideFirstSceneSwapHandler.snowPuzzle();

                //if the boss is dead, we will set the entranceactive state to false
                snowPuzzleInf.puzzleCompletionStatus = snowPuzzleOpener.puzzleWasSolved;

                string snowPuzzleJSON = JsonUtility.ToJson(snowPuzzleInf);

                File.WriteAllText(Application.dataPath + "outsideFirst" + "snowPuzzleList.txt", snowPuzzleJSON);
            }

            //----------------------------------------------------------------------------------------//
            //--------------------------SNOWY LAKE PUZZLE RELATED-------------------------------------//
            if (File.Exists(Application.dataPath + "snowyLake" + "wheelPuzzleList.txt") == false)
            {
                File.Create(Application.dataPath + "snowyLake" + "wheelPuzzleList.txt").Dispose();

                // save to the file only once if the file was not created ever
                snowyLakeSceneSwapHandler.wheelPuzzleInformation wheelPuzzleInf = new snowyLakeSceneSwapHandler.wheelPuzzleInformation();

                //if the boss is dead, we will set the entranceactive state to false
                wheelPuzzleInf.wheelPuzzleSolvedStatus = lakePuzzleCompletionChecker.wheelPuzzleSolved;

                string wheelPuzzleJSON = JsonUtility.ToJson(wheelPuzzleInf);

                File.WriteAllText(Application.dataPath + "snowyLake" + "wheelPuzzleList.txt", wheelPuzzleJSON);
            }

            //----------------------------------------------------------------------------------------//
            //--------------------------DASH RECEIEVAL RELATED-------------------------------------//
            if (File.Exists(Application.dataPath + "ruinedKingdomEntry" + "ruinedKingdomEntryRelated.txt") == false)
            {
                File.Create(Application.dataPath + "ruinedKingdomEntry" + "ruinedKingdomEntryRelated.txt").Dispose();

                // save to the file only once if the file was not created ever
                ruinedKingdomEntrySceneSwapHandler.ruinedKingdomEntryInformation ruinedKingdomEntryInf = new ruinedKingdomEntrySceneSwapHandler.ruinedKingdomEntryInformation();

                //if the boss is dead, we will set the entranceactive state to false
                ruinedKingdomEntryInf.dashWasReceived = snowSpiritLastShowScript.playerReceivedDash;

                string ruinedKingdomEntryJSON = JsonUtility.ToJson(ruinedKingdomEntryInf);

                File.WriteAllText(Application.dataPath + "ruinedKingdomEntry" + "ruinedKingdomEntryRelated.txt", ruinedKingdomEntryJSON);
            }




            //quit at the end
            Application.Quit();


        }

    }

    //Work later if needed
    public IEnumerator putPlayerObj()
    {


        yield return null;

    }

  



    // Update is called once per frame
    void Update()
    {
       

    }
}