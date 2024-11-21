using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLastSave : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    private int currentSave;
    void Start()
    {
        currentSave = PlayerPrefs.GetInt("LastSave");

        //Enabling the continue button
        if (currentSave != 0)
        {
            continueButton.SetActive(true);
        }
    }

    public void LoadSave()
    {
        LoadingController.instance.LoadScene(currentSave);
    }
}
