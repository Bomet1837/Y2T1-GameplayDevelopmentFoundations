using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_ToggleConsole : MonoBehaviour
{
    public GameObject devConsole;

    bool isLeftShiftPressed;
    bool isRightShiftPressed;
    bool isFivePressed;

    bool isConsoleActive;

    void Start()
    {
        devConsole.SetActive(false);
    }

    void Update()
    {
        ToggleDevConsole();
    }

    public void ToggleDevConsole()
    {
        //Checking if the keys are pressed together
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isLeftShiftPressed = true;
        }

        if (isLeftShiftPressed && Input.GetKey(KeyCode.RightShift))
        {
            isRightShiftPressed = true;
        }

        if (isLeftShiftPressed && isRightShiftPressed && Input.GetKey(KeyCode.Alpha5))
        {
            isFivePressed = true;
        }

        //If the combination left shift, right shift, 5, 7 are pressed
        if (isLeftShiftPressed && isRightShiftPressed && isFivePressed && Input.GetKeyDown(KeyCode.Alpha7))
        {
            //Enable the dev console
            isConsoleActive = !isConsoleActive;
            devConsole.SetActive(isConsoleActive);

            //Reset bools
            isLeftShiftPressed = false;
            isRightShiftPressed = false;
            isFivePressed = false;
        }

        //Reset bools
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift) ||
            Input.GetKeyUp(KeyCode.Alpha5) || Input.GetKeyUp(KeyCode.Alpha7))
        {
            isLeftShiftPressed = false;
            isRightShiftPressed = false;
            isFivePressed = false;
        }
    }
}
