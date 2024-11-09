using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    [Header("UI Settings")]
    [SerializeField] private GameObject subtitleUI;
    [SerializeField] private Image subtitleBackground;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text subtitleText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void ApplySettings()
    {
        ApplyGraphicsSettings();
        ApplyAudioSettings();
        ApplyUISettings();
        ApplyGameplaySettings();
    }

    public void ApplyGraphicsSettings()
    {
        //Brightness
        Screen.brightness = PlayerPrefs.GetFloat("Settings_Graphics_Brightness", 50) / 100.0f;

        //Resolution
        string resolution = PlayerPrefs.GetString("Settings_Graphics_Resolution", "1920x1080@60"); //Default: 1920x1080 at 60 Hz
        string[] resParts = resolution.Split('x', '@');
        if (resParts.Length == 3)
        {
            int width = int.Parse(resParts[0]);
            int height = int.Parse(resParts[1]);
            int refreshRate = int.Parse(resParts[2]);
            Screen.SetResolution(width, height, Screen.fullScreen, refreshRate);
        }

        //Window Mode
        string windowMode = PlayerPrefs.GetString("Settings_Graphics_WindowMode", "Fullscreen");
        switch (windowMode)
        {
            case "Fullscreen":
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case "Windowed":
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case "Borderless":
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
        }

        //VSync
        int vSync = PlayerPrefs.GetInt("Settings_Graphics_VSync", 0); //0 = off, 1 = on
        QualitySettings.vSyncCount = vSync;

        // Set graphical quality
        string quality = PlayerPrefs.GetString("Settings_Graphics_Quality", "High");
        int qualityLevel = Array.IndexOf(QualitySettings.names, quality);
        if (qualityLevel != -1)
        {
            QualitySettings.SetQualityLevel(qualityLevel, true);
        }

        //Anti-Aliasing
        int aaLevel = PlayerPrefs.GetInt("Settings_Graphics_AntiAliasing", 2); //0, 2, 4, 8
        QualitySettings.antiAliasing = aaLevel;

        //Effects Quality
        string effectsQuality = PlayerPrefs.GetString("Settings_Graphics_EffectsQuality", "Medium");
        switch (effectsQuality)
        {
            case "Low":
                QualitySettings.shadows = ShadowQuality.Disable;
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;
            case "Medium":
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
            case "High":
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.High;
                break;
        }

        //Texture Quality
        string textureQuality = PlayerPrefs.GetString("Settings_Graphics_TextureQuality", "High");
        switch (textureQuality)
        {
            case "Low":
                QualitySettings.masterTextureLimit = 2; //Higher number means lower quality
                break;
            case "Medium":
                QualitySettings.masterTextureLimit = 1;
                break;
            case "High":
                QualitySettings.masterTextureLimit = 0;
                break;
        }
    }


    public void ApplyAudioSettings()
    {
        //Master volume
        float masterVolume = PlayerPrefs.GetFloat("Settings_Audio_MasterVolume", 100) / 100.0f;
        AudioListener.volume = masterVolume;

        //Get the audio sources
        AudioSource musicSource = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<AudioSource>();
        AudioSource voiceSource = GameObject.FindGameObjectWithTag("VoiceSource").GetComponent<AudioSource>();
        AudioSource footstepsSource = GameObject.FindGameObjectWithTag("FootstepsSource").GetComponent<AudioSource>();
        AudioSource uiSource = GameObject.FindGameObjectWithTag("UISource").GetComponent<AudioSource>();

        //Music volume
        float musicVolume = PlayerPrefs.GetFloat("Settings_Audio_MusicVolume", 50) / 100.0f;
        if (musicSource != null)
        {
            musicSource.volume = musicVolume * masterVolume;
        }

        //Voices volume
        float voicesVolume = PlayerPrefs.GetFloat("Settings_Audio_VoicesVolume", 50) / 100.0f;
        if (voiceSource != null)
        {
            voiceSource.volume = voicesVolume * masterVolume;
        }

        //Footsteps volume
        float footstepsVolume = PlayerPrefs.GetFloat("Settings_Audio_FootstepsVolume", 50) / 100.0f;
        if (footstepsSource != null)
        {
            footstepsSource.volume = footstepsVolume * masterVolume;
        }

        //UI volume
        float uiVolume = PlayerPrefs.GetFloat("Settings_Audio_UIVolume", 50) / 100.0f;
        if (uiSource != null)
        {
            uiSource.volume = uiVolume * masterVolume;
        }
    }


    public void ApplyUISettings()
    {
        //Subtitle visibility
        bool useDialogueSubtitles = PlayerPrefs.GetInt("Settings_UI_UseDialogueSubtitles", 1) == 1;
        subtitleUI.SetActive(useDialogueSubtitles);

        //Subtitle background opacity
        float subtitleBackgroundOpacity = PlayerPrefs.GetFloat("Settings_UI_SubtitleBackgroundOpacity", 100) / 100.0f;
        subtitleBackground.color = new Color(subtitleBackground.color.r, subtitleBackground.color.g, subtitleBackground.color.b, subtitleBackgroundOpacity);

        //Speaker name visibility
        bool useSpeakerNames = PlayerPrefs.GetInt("Settings_UI_UseSpeakerNames", 1) == 1;
        speakerNameText.gameObject.SetActive(useSpeakerNames);

        //Text size
        string textSize = PlayerPrefs.GetString("Settings_UI_TextSize", "Medium");
        if (subtitleText != null)
        {
            switch (textSize)
            {
                case "Small":
                    subtitleText.fontSize = 14;
                    break;
                case "Medium":
                    subtitleText.fontSize = 18;
                    break;
                case "Large":
                    subtitleText.fontSize = 22;
                    break;
            }
        }
    }


    public void ApplyGameplaySettings()
    {
        
    }

}
