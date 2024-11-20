using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Scene Information")]
    [SerializeField] private string sceneCommonName;
    [SerializeField] private string sceneIdentifierName;
    
    [Header("Cursor")]
    [SerializeField] private bool cursorStartState;
    [SerializeField] private float speedStartState = 1f;
    private bool cursorState;
    
    [Header("Start Objects")]
    [SerializeField] private GameObject[] enableObjects;

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

        if (enableObjects != null)
        {
            StartEnablingObjects();
        }
    }

    private void Start()
    {
        //Set the initial settings
        ToggleCursor(cursorStartState);
        cursorState = cursorStartState;

        GameSpeed(speedStartState);

        if (sceneCommonName == null || (sceneCommonName == "Template Scene" && SceneManager.GetActiveScene().name != "EmptyWorldTemplate"))
        {
            Debug.LogError("Scene Common Name is missing.");
        }

        if (sceneIdentifierName == null || (sceneIdentifierName == "X_X" && SceneManager.GetActiveScene().name != "EmptyWorldTemplate"))
        {
            Debug.LogError("Scene Identifier is missing.");
        }
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

    void StartEnablingObjects()
    {
        foreach (GameObject obj in enableObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
            else
            {
                Debug.LogWarning("An object is null");
            }
        }
    }

    public string GetSceneCommonName()
    {
        return sceneCommonName;
    }
}
