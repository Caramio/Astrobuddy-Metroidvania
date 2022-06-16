using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class controlsStaticClass
{

    //Controller mappings with default values
    public static KeyCode moveLeftControl = KeyCode.A;
    public static KeyCode moveRightControl = KeyCode.D;
    public static KeyCode moveJumpControl = KeyCode.Space;
    public static KeyCode specialOneControl = KeyCode.Mouse1;
    public static KeyCode attackControl = KeyCode.Mouse0;
    public static KeyCode dashControl = KeyCode.LeftShift;





    public static void setControlByName(string controlName, KeyCode assignedKey)
    {

        if (controlName == "moveLeftControl")
        {
            moveLeftControl = assignedKey;
        }

        if (controlName == "moveRightControl")
        {
            moveRightControl = assignedKey;
        }

        if (controlName == "moveJumpControl")
        {
            moveJumpControl = assignedKey;
        }

    }




}
   
