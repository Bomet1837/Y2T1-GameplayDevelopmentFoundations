using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevLogButtonsController : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateLog()
    {
        this.GetComponent<DevLogLoader>()._getLog();
    }
}
