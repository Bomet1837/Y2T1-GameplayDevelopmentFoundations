using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC_MainMenu : MonoBehaviour
{
    public GameObject WorldObject;

    private void Start()
    {
        //Check if the world object is still temporary
        foreach (Transform child in WorldObject.transform)
        {
            //If the object is still the grey box version
            if (child.name == "Courtroom-GreyBox")
            {
                Debug.LogError("Main Menu world is still the grey box court. Please update it to the final world.");
            }
        }
    }
}
