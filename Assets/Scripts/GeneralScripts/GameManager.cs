using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool cursorStartState;

    public static GameManager instance;

    //TODO: Make this script manage general audio levels
    //TODO: Make this script manage game speed

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //Set the initial settings
        ToggleCursor(cursorStartState);
    }

    public void ToggleCursor(bool toggleState)
    {
        if (toggleState)
        {
            Debug.Log("Cursor: Enabled");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.Log("Cursor: Disabled");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
