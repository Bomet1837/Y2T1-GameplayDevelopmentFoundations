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
    
    [Header("Loading Screen")]
    public GameObject loadingPanel;
    public TMP_Text loadingPercentage;

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
        AudioManager.instance.PlayUIClick();
        
        ClosePanels();
        loadingPanel.SetActive(true);

        Debug.Log("Loading Scene: " + SceneManager.GetSceneByBuildIndex(scene));

        StartCoroutine(LoadSceneAsync(scene));
    }

    //A coroutine to load the scene asynchronously
    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); //Start loading the scene

        operation.allowSceneActivation = false; //Making sure the scene does not load until completed

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            //Update the loading bar with progress
            if (loadingPercentage != null)
            {
                float newProgress = progress * 100;
                loadingPercentage.text = "Loading: " + newProgress.ToString() + "%";
            }

            //If loading is done (progress = 0.9)
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true; //Load the scene
            }

            yield return null; //Error correction
        }
    }

    void ClosePanels()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        loadingPanel.SetActive(false);
    }
}
