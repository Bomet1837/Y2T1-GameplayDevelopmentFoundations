using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class FadeController : MonoBehaviour
{
    public static FadeController instance;
    
    [SerializeField] private TMP_Text sceneText;
    [SerializeField] private bool playFadeOnStart = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    public void FadeOut(string textInfo)
    {
        if (textInfo != null)
        {
            sceneText.gameObject.SetActive(true);
            sceneText.text = textInfo;
        }
        else
        {
            sceneText.gameObject.SetActive(false);
        }
        
        this.gameObject.GetComponent<Animation>().Play("FadeOutAnim");
    }
}
