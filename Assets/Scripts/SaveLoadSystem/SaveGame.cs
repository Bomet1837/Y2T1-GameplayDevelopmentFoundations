using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("LastSave", SceneManager.GetActiveScene().buildIndex);
    }
}
