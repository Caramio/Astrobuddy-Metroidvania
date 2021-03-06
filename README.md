# Astrobuddy-Metroidvania


#                                         ---------------------------GAME INFORMATION---------------------------
<br />
"Astrobuddy: Lost in Space" is a 2D metroidvania style game prototype.

The game is not fully finished, it was a training project to get into bigger scale projects that require many different components.

The game includes:

1-) NPC and Boss AI using state machine behaviour.

2-) A save system using JSON files that will record the progress of the player and allow the player to 
continue their game from where they left.

3-) simple implementations of particle systems, 2D lights and animations.

4-) A working inventory system.

5-) An options menu to allow the player to save, pause, change control keybinds and audio settings.

6-) A working combat system.

7-) The game used to contain a second playable character that the user can swap between the main character and the second character,
it was later removed because the game took a different direction.

                            

#                                                             THE MEANING BEHIND CERTAIN SCRIPTS

Here, a general explanation to the logic behind the game will be given. This project started as a hobby project to learn how to implement a metroidvania type game,
It is not fully commented and optimized due to it being a learning experience for myself as I worked on the game solo. Commenting each line with maximum detail at this
point would be a bigger commitment rather than explaining certain specific scripts here, so the main scripts will be explained here.

<br />
<br />

##                                                                 SwapHandler Scripts
<br />
<scene name>SwapHandler scripts(batBossRoomSwapHandler,goblinCitySceneSwapHandler etc...) are methods that allow the player to travel between different scenes
in the game. This script will be attached to an object at the edge of the screen, that is invisible to the player(It will be at the boundary of the screen), this object will have a trigger collider that will call the SwapHandler script once it collides with the player character.
  
The SwapHandler methods will simply load the scene that is linked to the entered doors name, for example, if the player enters "entryTobatBossChaseFromBossRoom", the "batBossChaseRoom" scene will be loaded while we are in the "batBossRoom" scene.
  
While swapping between scenes, the SwapHandler will create or overwrite a JSON file that contains the relevant information for the scene that was swapped FROM. For example, if we are in the "batBossRoom" scene and we killed the main boss inside this scene, the JSON file will store the variable "bossWasKilled" as "true" so we don't encounter the boss again when we reload the scene.
  
Each individual scene has its own SwapHandler, it will be named after the respective scene. All of these SwapHandlers have the same methods inside them, the only different is the data to save, which is different for each scene.
  
###                                                      Summary of the methods inside the SwapHandler

**WriteToJSON():**<br />
Create or overwrite an already existing JSON file with the relevant saved data from the scene.
   
**handleSceneSwap():**<br />
Contains a reference to the targeted scene by name, for example, if the GameObject that the SwapHandler is attachted to is called "entryTobatBossChaseFromBossRoom"
the "sceneToLoad" string will  be set as "bossChaseBat", which means that scene will be loaded later.
  
**IEnumerator loadAsyncScene():**<br />
The next scene referenced from "sceneToLoad" will start to load async to save some time between loading screens.
  
**IEnumerator fadeScreenRoutine():**<br />
The player characters RigidBody.constraints will be frozen and the alpha component of a dark background will slowly be set from 0 to 1 to replicate a screen fade effect. Once the screen is fully faded, since the scene was already loaded async, our player will be transfered to the targeted scene.  
  
 <br /><br /><br /><br /><br /> 

 ##                                                                Entry Handler Scripts
  
Entry handlers are simply used to determine where the player should be when entering the scene. For example, the "Prison" scene has 2 different entrances, which means it can be accessed from 2 other scenes, the upper entrace and lower entrace need to be distinguished, this is done in the "Entry Handler" scripts.

###                                                      Summary of the methods inside the SwapHandler
  
**entranceHandler():**<br />
This method will simply access the "sceneSwapHolder" class and access the public static string called enteredWay, which is a simple reference to the "Scene Swap Handler" GameObject that the player last touched. Once the entered way is clear, the location that the player should end up can be determined. Once the method runs, it will put the player in the proper position and unfreeze its previously frozen RigidBody.constraints(frozen from the "Scene Swap Handlers").

 <br /><br /><br /><br /><br /> 
  
##                                                                Changes Holder Scripts
  <br />
  
Changes Holder scripts check for the data from the saved JSON files and adjusts the current scene that the play entered to reflect the progress of the game. For example, if a boss was killed, the changes holder will execute inside "Start()" and adjust certain variables (boss alive status, door open status etc...) according to the progress of the player.

There is no specific method inside these changes holder scripts, they are all written inside the "Start()" method instead.
  
 
 <br /><br /><br /><br /><br /> 
  
  ##                                                              Boss/NPC AI Scripts
  <br />
  
The Boss/NPC AI are made using state machine behaviour. The scripts have different enums in them that act as states for out state machine behaviour, for example, the script will start the NPC in the idle state, while in this state, the NPC will be raycasting and checking around itself to see if the player is near, if the player is within the NPC's radius, the state machine will move from the "Idle" state to the "Combat" state, and within the combat state there will be different options that the NPC can use, for example, the Bat Boss can shoot a laser, or disappear and then dive towards the player, this will be determined by a random number generator.

  
 <br />
  
---------------------------------  ---------------------------------  ---------------------------------  
The above scripts are the general scripts that I found important to mention, there are of course many other scripts that help form this game, from the player controls to the inventory system, but these are the ones that needed specific descriptions.
---------------------------------  ---------------------------------  ---------------------------------    
   <br /><br /><br /><br /><br /> 
  
  ##                                                              Video Demonstrations
  
  There will be YouTube videos that briefly show/explain some of the concepts that are within the game, this will act as a quick preview of the game.
  
**NOTE: The videos are taken through the Unity editor rather than the full Unity build to make it easier to navigate through the many levels and take the recordings(There are around 30 levels). Although the game has a Dev Console to access whichever level the user wants, it is still more efficient to use the Unity Editor to take the recordings, the only problem is that the Unity Editor in maximized mode distorts the resolution and some of the UI elements etc look out of place(the game was not made for dynamic resolution yet, even though it was in the plans).**
  
 <br />  <br />

#                                                              Boss Videos 
**[Goblin King Boss Video](https://www.youtube.com/watch?v=3wArXUJ80jU&feature=youtu.be&ab_channel=Caramio)** 
<br />
<br />
**[Ancient Matriarch Boss Video](https://youtu.be/OX1Cttlo1d8)** 
<br />
<br />
**[Mage Council Boss Video](https://youtu.be/nlRatg-Wq7M)**   
<br />
#                                                              Puzzle Videos
**[Clone Puzzle Cavern of Illusions](https://youtu.be/SoGVpOJSIDU)**   
<br />
<br />
**[Musical and Questions Puzzle with Cutscene](https://youtu.be/YRskA8IHTo0)**   
<br />
<br />
**[Snowball Puzzle](https://youtu.be/X2sH9ylXb3Y)** 
<br />
<br />
**[Snowy Lake Puzzle](https://youtu.be/iULK4lOXrcI)**   
<br />
<br />
**[Starting Level with Puzzle](https://youtu.be/DB36q-fjp2M)**   
<br />
#                                                              Platforming Videos
**[Ruined Kingdom Platforming](https://youtu.be/fGP1BBD2zrY)**     
<br />
<br />
**[Town Platforming](https://youtu.be/GXafXro9P1w)**       
<br />
#                                                              Miscellaneous Videos
**[Options Menu](https://youtu.be/sruf0dXFM-o)**    
<br />
<br />
**[Scene Passages](https://youtu.be/FCTaY9Y_vxk)**      

