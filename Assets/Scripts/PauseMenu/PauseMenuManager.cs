using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuUI;
    private bool isGamePaused;

    public UnityEvent<bool> OnPausedStatusChanged;

    public GameObject optionsMenu;
    
    [Header("Loading Screen")]
    public GameObject loadingPanel;
    public TMP_Text loadingPercentage;
    
    public static PauseMenuManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //TODO: Make this simpler
            if (!isGamePaused)
            {
                //Pause Game
                optionsMenu.SetActive(false);
                pauseMenuUI.SetActive(true);

                TogglePause();
            }
            else
            {
                //Unpause Game
                optionsMenu.SetActive(false);
                pauseMenuUI.SetActive(false);

                TogglePause();
            }
        }
    }
    
    //Button functions
    public void ResumeButton(bool pause)
    {
        pauseMenuUI.SetActive(pause);

        TogglePause();
    }

    public void EnableOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void LoadMainMenu()
    {
        loadingPanel.SetActive(true);

        Debug.Log("Loading Scene: " + SceneManager.GetSceneByBuildIndex(0));

        StartCoroutine(LoadSceneAsync(0));
    }
    
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
    
    //The event
    public void TogglePause()
    {
        isGamePaused = !isGamePaused; //Quicker than using if statement

        OnPausedStatusChanged.Invoke(isGamePaused);
    }
}
