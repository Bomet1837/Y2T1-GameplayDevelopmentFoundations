using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject GraphicsPanel;
    [SerializeField] private GameObject AudioPanel;
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private GameObject GameplayPanel;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown windowModeDropdown;
    [SerializeField] private TMP_Dropdown maxFramerateDropdown;
    [SerializeField] private Toggle vSyncToggle;
    [SerializeField] private TMP_Dropdown graphicsQualityDropdown;
    [SerializeField] private TMP_Dropdown antiAliasingDropdown;
    [SerializeField] private TMP_Dropdown effectsQualityDropdown;
    [SerializeField] private TMP_Dropdown globalIlluminationQualityDropdown;
    [SerializeField] private TMP_Dropdown shadowQualityDropdown;
    [SerializeField] private TMP_Dropdown reflectionQualityDropdown;
    [SerializeField] private TMP_Dropdown textureQualityDropdown;
    [SerializeField] private TMP_Dropdown postProcessingQualityDropdown;
    [SerializeField] private Slider motionBlurSlider;

    [Header("Audio Settings")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider voicesVolumeSlider;
    [SerializeField] private Slider footstepsVolumeSlider;
    [SerializeField] private Slider uiVolumeSlider;

    [Header("UI Settings")]
    [SerializeField] private TMP_Dropdown textSide;
    [SerializeField] private Toggle useDialogueSubtitlesToggle;
    [SerializeField] private Toggle useSpeakerNamesToggle;
    [SerializeField] private Slider subtitleBackgroundOpacity;

    [Header("Gameplay Settings")]
    [SerializeField] private Toggle invertXToggle;
    [SerializeField] private Toggle invertYToggle;
    [SerializeField] private Slider cameraSensitivitySlider;


    private void Start()
    {
        if (PlayerPrefs.GetInt("Settings_HasLoadedGame") == 0)
        {
            SaveDefaultSettings();
        }
        LoadSettings();
        ActivatePanel(GraphicsPanel);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSettings();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Set settings to default
            PlayerPrefs.SetInt("Settings_HasLoadedGame", 0);
        }
    }
    private void LoadSettings()
    {
        Debug.Log("Settings: UI Loaded");

        //Graphics
        brightnessSlider.value = PlayerPrefs.GetInt("Settings_Graphics_Brightness");
        resolutionDropdown.value = PlayerPrefs.GetInt("Settings_Graphics_Resolution");
        windowModeDropdown.value = PlayerPrefs.GetInt("Settings_Graphics_WindowMode");
        maxFramerateDropdown.value = PlayerPrefs.GetInt("Settings_Graphics_MaxFramerate");
        vSyncToggle.isOn = PlayerPrefs.GetInt("Settings_Graphics_VSync") == 1;
        graphicsQualityDropdown.value = PlayerPrefs.GetInt("Settings_Graphics_Quality");
        antiAliasingDropdown.value = PlayerPrefs.GetInt("Settings_Graphics_AntiAliasing");
        effectsQualityDropdown.value = PlayerPrefs.GetInt("Settings_Graphics_EffectsQuality");
        globalIlluminationQualityDropdown.value = PlayerPrefs.GetInt("Settings_Graphics_GlobalIlluminationQuality");
        shadowQualityDropdown.value = PlayerPrefs.GetInt("Settings_Graphics_ShadowQuality");
        reflectionQualityDropdown.value = PlayerPrefs.GetInt("Settings_Graphics_ReflectionQuality");
        textureQualityDropdown.value = PlayerPrefs.GetInt("Settings_Graphics_TextureQuality");
        postProcessingQualityDropdown.value = PlayerPrefs.GetInt("Settings_Graphics_PostProcessingQuality");
        motionBlurSlider.value = PlayerPrefs.GetFloat("Settings_Graphics_MotionBlur");

        //Audio
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Settings_Audio_MasterVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Settings_Audio_MusicVolume");
        voicesVolumeSlider.value = PlayerPrefs.GetFloat("Settings_Audio_VoicesVolume");
        footstepsVolumeSlider.value = PlayerPrefs.GetFloat("Settings_Audio_FootstepsVolume");
        uiVolumeSlider.value = PlayerPrefs.GetFloat("Settings_Audio_UIVolume");

        //UI
        textSide.value = PlayerPrefs.GetInt("Settings_UI_TextSize");
        useDialogueSubtitlesToggle.isOn = PlayerPrefs.GetInt("Settings_UI_UseDialogueSubtitles") == 1;
        useSpeakerNamesToggle.isOn = PlayerPrefs.GetInt("Settings_UI_UseSpeakerNames") == 1;
        subtitleBackgroundOpacity.value = PlayerPrefs.GetFloat("Settings_UI_SubtitleBackgroundOpacity");

        //Gameplay
        invertXToggle.isOn = PlayerPrefs.GetInt("Settings_Gameplay_InvertX") == 1;
        invertYToggle.isOn = PlayerPrefs.GetInt("Settings_Gameplay_InvertY") == 1;
        cameraSensitivitySlider.value = PlayerPrefs.GetFloat("Settings_Gameplay_CameraSensitivity");
    }

    public void SaveSettings()
    {
        Debug.Log("Settings: Saved");

        //Graphics
        PlayerPrefs.SetInt("Settings_Graphics_Brightness", (int)brightnessSlider.value);
        PlayerPrefs.SetInt("Settings_Graphics_Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Settings_Graphics_WindowMode", windowModeDropdown.value);
        PlayerPrefs.SetInt("Settings_Graphics_MaxFramerate", maxFramerateDropdown.value);
        PlayerPrefs.SetInt("Settings_Graphics_VSync", vSyncToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Settings_Graphics_Quality", graphicsQualityDropdown.value);
        PlayerPrefs.SetInt("Settings_Graphics_AntiAliasing", antiAliasingDropdown.value);
        PlayerPrefs.SetInt("Settings_Graphics_EffectsQuality", effectsQualityDropdown.value);
        PlayerPrefs.SetInt("Settings_Graphics_GlobalIlluminationQuality", globalIlluminationQualityDropdown.value);
        PlayerPrefs.SetInt("Settings_Graphics_ShadowQuality", shadowQualityDropdown.value);
        PlayerPrefs.SetInt("Settings_Graphics_ReflectionQuality", reflectionQualityDropdown.value);
        PlayerPrefs.SetInt("Settings_Graphics_TextureQuality", textureQualityDropdown.value);
        PlayerPrefs.SetInt("Settings_Graphics_PostProcessingQuality", postProcessingQualityDropdown.value);
        PlayerPrefs.SetFloat("Settings_Graphics_MotionBlur", motionBlurSlider.value);

        //Audio
        PlayerPrefs.SetFloat("Settings_Audio_MasterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("Settings_Audio_MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("Settings_Audio_VoicesVolume", voicesVolumeSlider.value);
        PlayerPrefs.SetFloat("Settings_Audio_FootstepsVolume", footstepsVolumeSlider.value);
        PlayerPrefs.SetFloat("Settings_Audio_UIVolume", uiVolumeSlider.value);

        //UI
        PlayerPrefs.SetInt("Settings_UI_TextSize", textSide.value);
        PlayerPrefs.SetInt("Settings_UI_UseDialogueSubtitles", useDialogueSubtitlesToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Settings_UI_UseSpeakerNames", useSpeakerNamesToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("Settings_UI_SubtitleBackgroundOpacity", subtitleBackgroundOpacity.value);

        //Gameplay
        PlayerPrefs.SetInt("Settings_Gameplay_InvertX", invertXToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Settings_Gameplay_InvertY", invertYToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("Settings_Gameplay_CameraSensitivity", cameraSensitivitySlider.value);
    }

    private void SaveDefaultSettings()
    {
        Debug.Log("Settings: Set to Default");
        //These are the default values
        PlayerPrefs.SetInt("Settings_HasLoadedGame", 1); //Used to check if SaveDefaultSettings() needs to be called

        //Graphics
        PlayerPrefs.SetInt("Settings_Graphics_Brightness", 100); //Slider: 50-150
        PlayerPrefs.SetString("Settings_Graphics_Resolution", "null"); //Dropdown: The default Unity value
        PlayerPrefs.SetString("Settings_Graphics_WindowMode", "fullscreen"); //Dropdown
        PlayerPrefs.SetInt("Settings_Graphics_MaxFramerate", 0); //Dropdown: If 0, set to no limit
        PlayerPrefs.SetString("Settings_Graphics_Quality", "high"); //High
        PlayerPrefs.SetInt("Settings_Graphics_VSync", 0); //Bool: 0 = off, 1 = on
        PlayerPrefs.SetString("Settings_Graphics_AntiAliasing", "medium"); //Dropdown
        PlayerPrefs.SetString("Settings_Graphics_EffectsQuality", "medium"); //Dropdown
        PlayerPrefs.SetString("Settings_Graphics_GlobalIlluminationQuality", "medium"); //Dropdown
        PlayerPrefs.SetString("Settings_Graphics_ShadowQuality", "medium"); //Dropdown
        PlayerPrefs.SetString("Settings_Graphics_ShadowQuality", "medium"); //Dropdown
        PlayerPrefs.SetString("Settings_Graphics_ReflectionQuality", "medium"); //Dropdown
        PlayerPrefs.SetString("Settings_Graphics_TextureQuality", "meduim"); //Dropdown
        PlayerPrefs.SetInt("Settings_Graphics_MotionBlur", 0); //Slider

        //Audio
        PlayerPrefs.SetInt("Settings_Audio_MasterVolume", 100); //Slider
        PlayerPrefs.SetInt("Settings_Audio_MusicVolume", 50); //Slider
        PlayerPrefs.SetInt("Settings_Audio_VoicesVolume", 50); //Slider
        PlayerPrefs.SetInt("Settings_Audio_FootstepsVolume", 50); //Slider
        PlayerPrefs.SetInt("Settings_Audio_UIVolume", 50); //Slider

        //UI
        PlayerPrefs.SetString("Settings_UI_TextSize", "medium"); //Dropdown
        PlayerPrefs.SetInt("Settings_UI_UseDialogueSubtitles", 1); //Toggle: 0 = off, 1 = on
        PlayerPrefs.SetInt("Settings_UI_UseSpeakerNames", 1); //Toggle: 0 = off, 1 = on
        PlayerPrefs.SetInt("Settings_UI_SubtitleBackgroundOpacity", 100); //Slider: 0-255

        //Gameplay
        PlayerPrefs.SetInt("Settings_Gameplay_InvertX", 0); //Toggle: 0 = off, 1 = on
        PlayerPrefs.SetInt("Settings_Gameplay_InvertY", 0); //Toggle: 0 = off, 1 = on
        PlayerPrefs.SetInt("Settings_Gameplay_CameraSensitivity", 6); //Slider: 
    }

    //Panels
    public void ActivatePanel(GameObject activePanel)
    {
        GraphicsPanel.SetActive(false);
        AudioPanel.SetActive(false);
        UIPanel.SetActive(false);
        GameplayPanel.SetActive(false);

        activePanel.SetActive(true);
    }
    
}
