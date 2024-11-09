using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip uiClick;

    void Start()
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
    public void PlayUIClick()
    {
        //Create a temporary object for the audio source
        GameObject UIAudioObject = new GameObject("UIAudioObject");
        AudioSource uiAudioSource = UIAudioObject.AddComponent<AudioSource>();
        
        //Set the clip to the audio source
        uiAudioSource.clip = uiClick;
        
        //Play the audio
        uiAudioSource.Play();
        
        //Destroy the temporary object after the clip length + 0.5 seconds
        Destroy(UIAudioObject, uiClick.length + 0.5f);
    }
}
