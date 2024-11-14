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
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject backgroundMusic;
    private bool isGamePaused;

    public UnityEvent<bool> OnPausedStatusChanged;
    
    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private TMP_Text loadingPercentage;
    
    public static PauseMenuManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Destroying");
            Destroy(this);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                //Pause Game
                optionsMenu.SetActive(false);
                pauseMenuUI.SetActive(true);

                TogglePause(0);
            }
            else
            {
                //Unpause Game
                optionsMenu.SetActive(false);
                pauseMenuUI.SetActive(false);

                TogglePause(1);
            }
        }
    }
    
    //Button functions
    public void ResumeButton(bool pause)
    {
        AudioManager.instance.PlayUIClick();
        
        pauseMenuUI.SetActive(pause);
        
        backgroundMusic.SetActive(pause);

        TogglePause(1);
    }

    public void ToggleOptions(bool toggle)
    {
        AudioManager.instance.PlayUIClick();
        
        pauseMenuUI.SetActive(!toggle);
        optionsMenu.SetActive(toggle);
    }

    public void LoadMainMenu()
    {
        AudioManager.instance.PlayUIClick();
        
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
    public void TogglePause(float TimeScale)
    {
        isGamePaused = !isGamePaused; //Quicker than using if statement

        ToggleAllAudio(); //Pause/Unpause audio

        if (TimeScale == 1)
        {
            //Stop background music
            backgroundMusic.SetActive(false);
        }
        else
        {
            //Play background music
            backgroundMusic.SetActive(true);
        }

        Time.timeScale = TimeScale;

        OnPausedStatusChanged.Invoke(isGamePaused);
    }

    void ToggleAllAudio()
    {
        //Find each audio source in the scene
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            if (isGamePaused)
            {
                //Pause the audio if it is playing
                if (audioSource.isPlaying)
                {
                    audioSource.Pause();
                }
            }
            else
            {
                //Unpause the audio if it was previously paused
                if (!audioSource.isPlaying && audioSource.time > 0)
                {
                    audioSource.UnPause();
                }
            }
        }
    }
}
