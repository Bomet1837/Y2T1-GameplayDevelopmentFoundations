using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    [SerializeField] private TMP_Text sceneText;
    [SerializeField] private bool playFadeOnStart = true;

    private void Start()
    {
        string sceneCommonName = GameManager.instance.GetSceneCommonName();
        
        if (sceneCommonName != null && playFadeOnStart)
        {
            sceneText.gameObject.SetActive(true);
            sceneText.text = sceneCommonName;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
