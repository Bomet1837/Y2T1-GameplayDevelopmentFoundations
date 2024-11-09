using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool cursorStartState;
    [SerializeField] private float speedStartState = 1f;

    public static GameManager instance;

    private bool cursorState;

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
        cursorState = cursorStartState;

        GameSpeed(speedStartState);
    }

    public void ToggleCursor(bool toggleState)
    {
        if (toggleState)
        {
            //Debug.Log("Cursor: Enabled");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            cursorState = true;
        }
        else
        {
            //Debug.Log("Cursor: Disabled");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            cursorState = false;
        }
    }

    public void GameSpeed(float speed)
    {
        Time.timeScale = speed;
    }

    public float GetGameSpeed()
    {
        return Time.timeScale;
    }
}
