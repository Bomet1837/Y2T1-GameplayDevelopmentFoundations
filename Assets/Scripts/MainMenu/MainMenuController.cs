using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject startupLogos;

    [Header("Main Panel")] 
    public TMP_Text versionText;

    void Start()
    {
        startupLogos.SetActive(true);
        
        ClosePanels();
        mainPanel.SetActive(true);

        //Show the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        //Set the versionText
        versionText.text = Application.version;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeInHierarchy)
            {
                ClosePanels();
                mainPanel.SetActive(true);
            }
        }
    }

    public void ToggleSettings()
    {
        AudioManager.instance.PlayUIClick();
        
        if (!settingsPanel.activeInHierarchy)
        {
            ClosePanels();
            settingsPanel.SetActive(true);
        }
        else
        {
            ClosePanels();
            mainPanel.SetActive(true);
        }
    }

    public void LoadModelViewer()
    {
        AudioManager.instance.PlayUIClick();
        
        LoadingController.instance.LoadScene(SceneUtility.GetBuildIndexByScenePath("ModelViewer"));
    }

    public void QuitGame()
    {
        AudioManager.instance.PlayUIClick();
        
        Debug.Log("Quitting Game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadGame(int scene)
    {
        //For loading a new game only
        AudioManager.instance.PlayUIClick();
        
        PlayerPrefs.DeleteKey("LastSave");
        
        LoadingController.instance.LoadScene(scene);
    }

    //A coroutine to load the scene asynchronously
    

    void ClosePanels()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
}
