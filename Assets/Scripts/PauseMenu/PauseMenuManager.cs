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

        LoadingController.instance.LoadScene(0);
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
