using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp_Playtest_PressToContinue : MonoBehaviour
{
    public string sceneName;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
