using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public static LoadingController instance;
    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private TMP_Text loadingPercentage;

    void Awake()
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

    public void LoadScene(int sceneIndex)
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneIndex));
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
                float newProgress = Mathf.RoundToInt(progress * 100);
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
}
