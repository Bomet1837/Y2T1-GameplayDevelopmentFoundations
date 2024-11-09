using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

//This has been taken and modified from https://docs.unity3d.com/Manual/UnityWebRequest-RetrievingTextBinaryData.html#:~:text=To%20retrieve%20simple%20data%20such,from%20which%20data%20is%20retrieved.

public class DevLogLoader : MonoBehaviour
{
    public TMP_Text devLogText;

    public string gitUrl = "https://raw.githubusercontent.com/AlekMunroe/Y2T1-GameplayDevelopmentFoundations/refs/heads/main/README.md";

    void Start()
    {
        _getLog();
    }

    public void _getLog()
    {
        devLogText.text = "";

        StartCoroutine(GetLog());
    }

    IEnumerator GetLog()
    {
        UnityWebRequest www = UnityWebRequest.Get(gitUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            devLogText.text = www.error;
            Debug.LogError(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            devLogText.text = www.downloadHandler.text;
        }
    }
}